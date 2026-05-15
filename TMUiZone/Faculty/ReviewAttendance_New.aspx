<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="ReviewAttendance_New.aspx.cs" Inherits="Faculty_ReviewAttendance_New" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function preventBackspace(e) {
            var evt = e || window.event;
            if (evt) {
                var keyCode = evt.charCode || evt.keyCode;
                if (keyCode === 8) {
                    if (evt.preventDefault) {
                        evt.preventDefault();
                    } else {
                        evt.returnValue = false;
                    }
                }
            }
        }
        function checkDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("You cannot select greater than current date!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
    </script>
    <script type="text/javascript">

        function onUpdating() {
            // get the divImage

            var panelProg = $get('divImage');
            // set it to visible
            panelProg.style.display = '';

            // hide label if visible     
            var lbl = $get('<%= this.lblText.ClientID %>');
                lbl.innerHTML = '';

            }
    </script>

    <style type="text/css">
        .modalPopup {
            background-color: #ffffdd;
            /*border-width: 3px;*/
            /*border-style: solid;*/
            /*border-color: Gray;*/
            padding: 3px;
            width: auto;
        }

            .modalPopup .header {
                background-color: #2FBDF1;
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }

            .modalPopup .body {
                min-height: 50px;
                line-height: 30px;
                text-align: center;
                padding: 5px;
            }

            .modalPopup .footer {
                padding: 3px;
            }

            .modalPopup .button {
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
                background-color: red;
                /*border: 1px solid #5C5C5C;*/
            }

            .modalPopup td {
                text-align: left;
            }

        .redBorder {
            /*border: 1px solid red;*/
        }

        .CSS1 {
            background-color: yellow !important;
        }

        .CSS2 {
            background-color: #E7E7FF !important;
        }
           .MyClass {
        border:none 
    }
    .MyClass td{
        font-size:12px;
    }
    .MyClass tr th{
            font-size: 14px;
    }
    .cssGridheaderfont1 {
    font-size: 9.2px;
    font-family: Georgia, "Times New Roman", Times;
     }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1"
        TargetControlID="UpdatePanel1" runat="server">
        <Animations>
            <OnUpdating>
               <Parallel duration="0">
                    <ScriptAction Script="onUpdating();" />
                    <EnableAction AnimationTarget="ddlReaapear" Enabled="false" /> 
                </Parallel>
            </OnUpdating>
        </Animations>
    </asp:UpdatePanelAnimationExtender>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <fieldset class="boxBody">

                <asp:Label ID="Label3" runat="server"
                    Text="Review Attendance" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    Academic Year:&nbsp&nbsp
                    <asp:DropDownList ID="ddlAcademicYear" Width="100px" Height="20px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Font-Size="13px" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="ddlAcademicYear" ValidationGroup="g1" ErrorMessage="Please select the Academic Year!"></asp:RequiredFieldValidator>
                <asp:Label ID="lblText" runat="server" Text="" Height="10px"></asp:Label>
                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                   Type:&nbsp&nbsp
                    <asp:DropDownList ID="ddlTypeClass" Width="100px" Height="20px" runat="server" OnSelectedIndexChanged="ddlTypeClass_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="0,4">Regular</asp:ListItem>
                        <asp:ListItem Value="1">Remedial</asp:ListItem>
                        <asp:ListItem Value="2">GD/PD Class</asp:ListItem>
                        <asp:ListItem Value="4">Extra Class</asp:ListItem>
                    </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Font-Size="13px" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="ddlAcademicYear" ValidationGroup="g1" ErrorMessage="Please select the Academic Year!"></asp:RequiredFieldValidator>

                <id id="divImage" style="display: none;" class="fa fa-spinner fa-pulse fa-2x fa-fw"></id>
                <span class="sr-only">Loading...</span>
            </fieldset>
            <fieldset class="boxBodyHeader">
            </fieldset>
            <fieldset class="boxBodyInner">
                <center>
                    <br />
                    <div class="pull-left">
                        <asp:CheckBox ID="Chkdetained" runat="server" OnCheckedChanged="Chkdetained_CheckedChanged" AutoPostBack="true" Text="Detained/Supplementary" Font-Size="12pt"></asp:CheckBox>&nbsp&nbsp&nbsp&nbsp</div>
                    <div class="pull-left">
                        <asp:CheckBox ID="chkOpen" runat="server" Text="Open Elective" AutoPostBack="true" OnCheckedChanged="chkOpen_CheckedChanged" />
                    </div>

                    <br />
                    <br />
                    <div class="pull-left">
                        <asp:CheckBox runat="server" ID="chkboxPrinciple" AutoPostBack="true" Font-Bold="true" Visible="false" Text="As Principal" OnCheckedChanged="chkboxPrinciple_CheckedChanged" />

                    </div>
                    <div class="clearfix"></div>
                    <table style="border-style: outset; border-width: thin;" width="95%">
                        <tr style="height: 10px; background-color: rgba(0, 0, 0, 0.06);">
                            <td colspan="17"></td>
                        </tr>
                        <tr style="height: 10px; background-color: rgba(0, 0, 0, 0.06);">
                            <td style="width: 10px"></td>
                            <td>Course:</td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:DropDownList ID="drpCourse" runat="server" AutoPostBack="true" Width="150px" Height="20px" BackColor="#e8e8e8" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged"></asp:DropDownList></td>
                            <td style="width: 10px"></td>
                            <td>Semester/Year:</td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:DropDownList ID="drpSemester" runat="server" AutoPostBack="true" Width="150px" Height="20px" BackColor="#e8e8e8" OnSelectedIndexChanged="drpSemester_SelectedIndexChanged"></asp:DropDownList></td>
                            <td style="width: 10px"></td>
                            <td>Section:</td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:DropDownList ID="drpSection" runat="server" AutoPostBack="true" Width="150px" Height="20px" BackColor="#e8e8e8" OnSelectedIndexChanged="drpSection_SelectedIndexChanged"></asp:DropDownList></td>
                            <td style="width: 10px"></td>
                            <td>Subject:</td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:DropDownList ID="drpSubject" runat="server" Width="150px" Height="20px" BackColor="#e8e8e8"></asp:DropDownList></td>
                            <td style="width: 70px"></td>
                        </tr>
                        <tr style="background-color: rgba(0, 0, 0, 0.06);">
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Font-Size="13px" runat="server" Display="Dynamic" ErrorMessage="Please select the Course!" ValidationGroup="g1" ForeColor="Red" ControlToValidate="drpCourse"></asp:RequiredFieldValidator></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Font-Size="13px" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="drpSemester" ValidationGroup="g1" ErrorMessage="Please select the Semester!"></asp:RequiredFieldValidator></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>

                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Font-Size="13px"  runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="drpSubject" ValidationGroup="g1" ErrorMessage="Please select the Subject!"></asp:RequiredFieldValidator></td>--%>
                                    <td style="width: 70px"></td>
                        </tr>
                        <tr style="height: 10px; background-color: rgba(0, 0, 0, 0.06);">
                            <td colspan="17"></td>
                        </tr>
                        <tr style="background-color: rgba(0, 0, 0, 0.06);">
                            <td></td>
                            <td>Group&nbsp:</td>
                            <td></td>
                            <td>
                                <asp:DropDownList ID="ddlGroup" Width="150px" Height="20px" BackColor="#e8e8e8" runat="server"></asp:DropDownList>

                            </td>
                            <td></td>
                            <td>Batch</td>
                            <td></td>
                            <td>
                                <asp:DropDownList ID="ddlBatch" Width="150px" Height="20px" BackColor="#e8e8e8" runat="server"></asp:DropDownList>

                            </td>
                            <td></td>
                            <td>From </td>
                            <td></td>
                            <td>
                                <asp:TextBox ID="txtDateFrom" BackColor="#e8e8e8" Width="120px" Height="25px" runat="server" onkeypress="return false" placeholder="From" onKeyDown="preventBackspace();"></asp:TextBox>
                                <asp:Image src="../Images/Calendar.png" runat="server" Height="25px" Width="25px" alt="" ID="fdate" />
                                <asp:CalendarExtender ID="CalendarExtender2" Format="dd MMM yyyy" runat="server"
                                    CssClass="cal_Theme1" PopupButtonID="fdate" OnClientDateSelectionChanged="checkDate" Enabled="true" TargetControlID="txtDateFrom">
                                </asp:CalendarExtender>
                            </td>
                            <td></td>
                            <td>To</td>
                            <td></td>
                            <td>
                                <asp:TextBox ID="txtDateTo" Width="120px" BackColor="#e8e8e8" Height="25px" runat="server" placeholder="To" onkeypress="return false" onKeyDown="preventBackspace();"></asp:TextBox>
                                <asp:Image src="../Images/Calendar.png" runat="server" Height="25px" Width="25px" alt="" ID="fdate1" />
                                <asp:CalendarExtender ID="CalendarExtender1" Format="dd MMM yyyy" runat="server"
                                    CssClass="cal_Theme1" PopupButtonID="fdate1" OnClientDateSelectionChanged="checkDate" Enabled="true" TargetControlID="txtDateTo">
                                </asp:CalendarExtender>
                            </td>
                            <td>
                                <center>
                                    <asp:LinkButton ID="btnShow" ValidationGroup="g1" class="btn btn-info btn-sm" runat="server" OnClick="btnShow_Click">
                         <span class="glyphicon glyphicon-upload"></span> Show 
                                    </asp:LinkButton>&nbsp
                                     <asp:LinkButton ID="btnShow1" class="btn btn-info btn-sm" runat="server" Visible="false" OnClick="btnShow1_Click">
                         <span class="glyphicon glyphicon-upload"></span> Show 
                                     </asp:LinkButton>
                                </center>
                            </td>
                        </tr>

                        <tr style="height: 10px">
                            <td colspan="17"></td>
                        </tr>
                        <tr>
                            <td colspan="17">
                                <asp:HiddenField ID="hfR_FromDate" runat="server" />
                                <asp:HiddenField ID="hfR_ToDate" runat="server" />
                                <asp:HiddenField ID="hfR_Session" runat="server" />
                                <asp:HiddenField ID="hfR_Course" runat="server" />
                                <asp:HiddenField ID="hfR_SemYear" runat="server" />
                                <asp:HiddenField ID="hfR_Section" runat="server" />
                                <asp:HiddenField ID="hfR_Subject" runat="server" />
                                <asp:HiddenField ID="hfR_Group" runat="server" />
                                <asp:HiddenField ID="hfR_Batch" runat="server" />
                            </td>
                        </tr>
                        <tr style="height: 10px">
                            <td colspan="17"></td>
                        </tr>
                        <tr>
                          
                            <td colspan="17" align="center">
                                <div style="height:400px;overflow-x:scroll">
                                <asp:GridView ID="grdStudentAttendance" runat="server" Font-Size="20px" AutoGenerateColumns="False" CssClass="MyClass" BackColor="White" BorderColor="#E7E7FF" 
                                    DataKeyNames="Student No_"
                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="1100px" GridLines="Horizontal"
                                    EmptyDataText="There are no data records to display.">
                                    <AlternatingRowStyle BackColor="#F7F7F7" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl. No." ItemStyle-Width="1%"  HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle Width="7%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField SortExpression="ApplicantName">

                                            <ItemTemplate>
                                                <asp:Label ID="lblStudent" runat="server" CssClass='<%# Bind("css") %>' Text='<%# Eval("[Student No_]") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="visible-lg" />
                                            <ItemStyle CssClass="visible-lg" Font-Size="0pt" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Enrollment No_" HeaderText="Enrollment No" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                            <HeaderStyle CssClass="visible-lg" />
                                            <ItemStyle CssClass="visible-lg" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Student Name" SortExpression="ApplicantName">

                                            <ItemTemplate>
                                                <asp:Label ID="lblStudentName" runat="server"  Text='<%# Eval("[Student Name]") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="visible-lg" />
                                            <ItemStyle CssClass="visible-lg" />
                                        </asp:TemplateField>
                                         <asp:BoundField DataField="Total" HeaderText="Delivered Lecture" SortExpression="Delivered Lecture" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                            <HeaderStyle CssClass="visible-lg" />
                                            <ItemStyle CssClass="visible-lg" />
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Present" SortExpression="ApplicantName">

                                            <ItemTemplate>
                                                <asp:LinkButton ID="BtnPresent" runat="server" Text='<%# Eval("Present") %>' OnClick="BtnPresent_Click"></asp:LinkButton>


                                            </ItemTemplate>
                                            <HeaderStyle CssClass="visible-lg" />
                                            <ItemStyle CssClass="visible-lg" />
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="ECA" SortExpression="ApplicantName">

                                            <ItemTemplate>
                                                <asp:LinkButton ID="BtnECA" runat="server" Text='<%# Eval("ECA") %>' ></asp:LinkButton>


                                            </ItemTemplate>
                                            <HeaderStyle CssClass="visible-lg" />
                                            <ItemStyle CssClass="visible-lg" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Absent" SortExpression="ApplicantName">

                                            <ItemTemplate>
                                                <asp:LinkButton ID="BtnAbsent" runat="server" Text='<%# Eval("Absent") %>' OnClick="BtnAbsent_Click"></asp:LinkButton>
                                                <asp:HiddenField ID="HfEventType" runat="server" Value='<%# Eval("[Event Type]") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="visible-lg" />
                                            <ItemStyle CssClass="visible-lg" />
                                        </asp:TemplateField>
                                       
                                        <asp:BoundField DataField="Percentage" HeaderText="Percentage(%)" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">

                                            <HeaderStyle CssClass="visible-lg" />
                                            <ItemStyle CssClass="visible-lg" />
                                        </asp:BoundField>
                                       
                                         <asp:TemplateField HeaderText="Hosteller" SortExpression="Hosteller">

                                            <ItemTemplate>
                                                <asp:Label ID="lblHosteler" runat="server"  Text='<%# Eval("[Hosteler]") %>'></asp:Label>
                                                  

                                            </ItemTemplate>
                                            <HeaderStyle CssClass="visible-lg" />
                                            <ItemStyle CssClass="visible-lg" />
                                        </asp:TemplateField>
                                    </Columns>
                                     <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                        <HeaderStyle BackColor="LightGray" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" Font-Size="Large" Height="40px" VerticalAlign="Bottom" />
                                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                        <RowStyle ForeColor="#4A3C8C" Font-Bold="true" Font-Size="Medium" BorderStyle="Solid" BorderColor="Black" />
                                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                        <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                        <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                </asp:GridView>
                                </div>


                                <asp:Panel ID="pnlGridViewDetails" CssClass="modalPopup" runat="server" Style="display: none;">
                                    <%--Add other controls here--%>
                                    <div class="header">
                                        <b>
                                            <asp:Label ID="lblNotification" runat="server" Text="Attendance Detail"></asp:Label></b><div class="close">
                                                <asp:Button ID="btnclose" OnClick="btnclose_Click" runat="server" Text="X" />
                                            </div>
                                    </div>
                                    <div id="Div1" runat="server" style="max-height: 500px; overflow: auto;">

                                        <div class="body">
                                            <div style="width: 100%">
                                                <center>
                                                    <asp:Label ID="lblStudent" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Label ID="lblCourse" runat="server" Text="Label"></asp:Label>
                                                    <br />
                                                    <asp:GridView ID="grdAttandanceDetails" Width="1050px" EmptyDataText="There are no data records to display." runat="server">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl. No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="7%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                        <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont1" />
                                                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                                        <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont1" />
                                                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                        <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                                        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                                        <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                                        <SortedDescendingHeaderStyle BackColor="#3E3277" />

                                                    </asp:GridView>
                                                </center>
                                            </div>

                                        </div>

                                    </div>
                                </asp:Panel>
                                <asp:Button ID="btnDummy" runat="server" Style="display: none;" />
                                <asp:ModalPopupExtender ID="GridViewDetails" runat="server" TargetControlID="btnDummy"
                                    PopupControlID="pnlGridViewDetails" BackgroundCssClass="modalBackground" />

                            </td>

                        </tr>
                        <tr style="height: 10px">
                            <td colspan="17"></td>
                        </tr>
                        <tr>
                            <td colspan="15"></td>
                            <td>
                                <asp:Button ID="btnExport" runat="server" Text="Export To Excel" Visible="false" OnClick="ExportToExcel" />
                            </td>
                        </tr>

                        <tr style="height: 10px">
                            <td colspan="17"></td>
                        </tr>
                    </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>
    </center>

        </fieldset>
</asp:Content>

