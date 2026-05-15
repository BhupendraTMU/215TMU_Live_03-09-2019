<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="StudentAttendance.aspx.cs" Inherits="Faculty_StudentAttendance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .buttonstyleCss {
            border-radius: 5px;
            border: 0;
            border-radius: 5px;
            box-shadow: 1px 2px 5px #666;
            background-color: yellow;
            width: 400px;
            height: 100px;
        }

        .upCase {
            text-transform: uppercase;
        }

        .myTableClass tr th {
            padding: 1px;
        }

        tr td {
            padding: 1px;
        }



        .style1 {
            height: 203px;
        }

        a.greenButton {
            color: #000;
            text-decoration: none;
            margin: 20px;
            padding: 10px 20px 10px 20px;
            display: inline-block;
        }

            a.greenButton:hover {
                background-color: #5078B3;
            }

        .modalbackground {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }

        .modalPopupWhite {
            background-color: #ffffff;
            border-width: 3px;
            border-style: solid;
            border-color: Gray;
            padding: 3px;
            width: 500px;
        }

        .modalprogress {
            opacity: 0.7;
            filter: alpha(opacity=60);
            background-color: #ededed;
        }

        .btnaddmaincattt {
            color: #fff;
            text-decoration: none;
            padding: 10px 20px 10px 20px;
            display: inline-block;
            font-weight: bold;
            background-color: #5078B3;
            cursor: pointer;
            border-radius: 10px;
        }

        .hidden {
            display: none;
        }

        .block1 {
            visibility: visible;
        }

        .redBorder {
            border: 1px solid red;
        }

        .loader {
            position: fixed;
            left: 45%;
            top: 45%;
            width: 100px;
            height: 100px;
            z-index: 9999;
            background: url('../images/loader.gif') 50% 50% no-repeat rgb(249,249,249);
        }

        tr.spaceUnder {
            padding-bottom: 5px;
        }

        .example-print {
            display: none;
        }

        @media print {
            .example-screen {
                display: none;
            }

            .example-print {
                display: block;
            }
        }


        .example-print1 {
            display: block;
        }

        @media print1 {
            .example-screen {
                display: block;
            }

            .example-print1 {
                display: none;
            }
        }
    
        .red-border {
            border: 1px solid red;
        }

        .JainStudentList {
            font-family: Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

            .JainStudentList td, .JainStudentList th {
                border: 1px solid #ddd;
                padding: 8px;
            }

            .JainStudentList tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            .JainStudentList tr:hover {
                background-color: #ddd;
            }

            .JainStudentList th {
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: left;
                background-color: #04AA6D;
                color: white;
            }

        <script type="text/javascript" > function callFeedbackMessage(inputType, inputText) {
            if (inputType == 'Error');

        {
            alertify .error(inputText);
            return false;
        }

        else if (inputType == 'Success') {
            alertify .success("Save Successfully");
            return false;
        }

        else {
            alertify .log(inputText, "", 10000);
            return false;
        }

        }

        function Count() {
            var j = 0;
            var k = 0;
            var GridId = "<%=grdAttendanceDetails.ClientID %>";
            var grid = document.getElementById(GridId);
            rowscount = grid.rows.length - 1;
            for (i = 0; i < parseInt(rowscount) ; i++);

        {
            var checkBox = document.getElementById('grdAttendanceDetails_chkboxAttendance_' + i + '');
            if (checkBox.checked == true);

        {
            j ++;
        }

        k + +;
        }
        document.getElementById('lblNoOfStudent').text = j; document.getElementById('lblTotalNoOfStudent').text = k; $('#' + myModal2).dialog();
        }

        function Save() {
            var elem = document.getElementById("Loader1");
            elem .style.display = "block";
            $(".loader").fadeIn("slow");
            $('[id$=btnSave2]').click();
        }

        function Validate() {
            var drpCurrent = document.getElementById('<%= drpUnit.ClientID %>').value;
            if (drpCurrent == "");

        {
            alert("Please Select Unit.");
            return false;
        }

        return true;
        }

        </script > <style type="text/css" > @media (min-width: 1000px) {
            .container;

        {
            width: 1000px;
        }

        }

        @media (min-width: 1000px) {
            .container {
                width: 1000px;
            }
        }

        @media (min-width: 1200px) {
            .container {
                width: 1170px;
            }
        }

        .container-fluid {
            padding-right: 15px;
            padding-left: 15px;
            margin-right: auto;
            margin-left: auto;
        }

        .ChkBoxClass input {
            width: 85px;
            height: 85px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Panel ID="pnlApproval" runat="server" Visible="false" CssClass="leftBackground">
        <div class="container">
            <!-- NOTICE HERE:inside div declare class as container/container-fluid-->
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div>

                        <h1 class="head-line">Student Details &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        <asp:LinkButton ID="btnMarkAtten" runat="server" Text="Back Date Mark Attendance " Visible="false" OnClick="btnMarkAtten_Click"></asp:LinkButton>
                        </h1>
                        <br />
                        <br />

                        <table>
                            <tr>
                                <td>Course Code </td>
                                <td>
                                    <asp:DropDownList ID="drpCourse" runat="server" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged"></asp:DropDownList></td>
                                <td style="width: 20px"></td>
                                <td>Semester
                                </td>
                                <td>
                                    <asp:DropDownList ID="drpSemester" runat="server"></asp:DropDownList>
                                </td>
                                <td style="width: 20px"></td>
                                <td>
                                    <asp:CheckBox ID="chkOpen" runat="server" Text="OpenElective" />

                                </td>
                                <td>
                                    <asp:Button ID="btnView" runat="server" Text="VIEW" Width="100px" OnClick="btnView_Click" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />

                        <asp:UpdatePanel ID="pnlMain" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grdTimetable" DataKeyNames="Entry No_,Subject Code,Course Code,lecture,Date,Semester" AutoGenerateColumns="false" runat="server" BackColor="White"
                                    BorderColor="#E7E7FF" BorderStyle="None" CssClass="JainStudentList" BorderWidth="1px" CellPadding="3" GridLines="Horizontal"
                                    EmptyDataText="There are no data records to display." HeaderStyle-BackColor="SteelBlue">
                                    <AlternatingRowStyle BackColor="#F7F7F7" />
                                    <Columns>
                                        <%--                                    <asp:TemplateField HeaderText="Sr. No." ControlStyle-Width="300px" >
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle Width="3%" />
                                    </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Lecture" ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lbllecture" runat="server" Text='<%# Eval("[lecture]") %>' />

                                            </ItemTemplate>
                                            <ItemStyle Width="3%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Course" ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("[Course Code]") %>' />

                                            </ItemTemplate>
                                            <ItemStyle Width="3%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Subject" ControlStyle-Width="150px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubject" runat="server" Text='<%# Eval("[Subject Code]") %>' />

                                            </ItemTemplate>
                                            <ItemStyle Width="3%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date" ControlStyle-Width="200px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("[Date]") %>' />

                                            </ItemTemplate>
                                            <ItemStyle Width="3%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Semester" ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSem" runat="server" Text='<%# Eval("[Semester]") %>' />

                                            </ItemTemplate>
                                            <ItemStyle Width="3%" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Principal Status" ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPStatus" runat="server" Text='<%# Eval("[Pstatus]") %>' />

                                        </ItemTemplate>
                                        <ItemStyle Width="3%" />
                                    </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%-- Enabled='<%# Eval("Pstatus").ToString() == "Sent" ? true : true %>'--%>
                                                <asp:Button ID="btnSelect" runat="server" Text='Select' OnClick="btnSelect_Click" />

                                            </ItemTemplate>
                                            <ItemStyle Width="3%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>

                        </asp:UpdatePanel>
                        <asp:Panel ID="pnlGridViewDetails" CssClass="modalPopup" Width="50%" runat="server" Height="600px" Style="display: none; left: 50%;">




                            <div class="header">
                                <b>
                                    <asp:Label ID="lblNotification" runat="server" Text="Attendance Detail"></asp:Label></b><div class="close">
                                        <asp:Button ID="btnclose" OnClick="btnclose_Click" runat="server" Text="X" />
                                    </div>
                            </div>
                            <div id="Div1" runat="server" style="height: 500px;">
                                <asp:Label ID="lblUnit" Text="UNIT :" runat="server" Visible="false"></asp:Label>
                                <asp:DropDownList ID="drpUnit" runat="server" Width="200px" Visible="false"></asp:DropDownList>
                                <asp:Button ID="btnSubmit" runat="server" Text="SUBMIT" class="buttonstyleCss" Visible="false" OnClick="btnSubmit_Click" />
                                <div style="width: 100%; height: 500px; overflow: auto;">
                                    <center>




                                        <br />


                                        <asp:GridView ID="grdAttendanceDetails" DataKeyNames="No_" runat="server" AutoGenerateColumns="False"
                                            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal"
                                            EmptyDataText="There are no data records to display.">
                                            <AlternatingRowStyle BackColor="#F7F7F7" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="SR.NO." ControlStyle-Width="400px">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="3%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Student NAME" HeaderStyle-HorizontalAlign="Left" ControlStyle-Width="200px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("[Student Name]") %>' />

                                                    </ItemTemplate>
                                                    <ItemStyle Width="3%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Enroll. No." ControlStyle-Width="200px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEnroll" runat="server" Text='<%# Eval("[Enrollment No_]") %>' />

                                                    </ItemTemplate>
                                                    <ItemStyle Width="3%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Present" HeaderStyle-HorizontalAlign="Left" ControlStyle-Width="50px">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkboxAttendance" Checked="true" TabIndex="1" runat="server" />
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Student No" ControlStyle-Width="200px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNo" runat="server" Text='<%# Eval("[No_]") %>' />

                                                    </ItemTemplate>

                                                </asp:TemplateField>




                                            </Columns>
                                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />

                                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                            <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />
                                            <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
                                            <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                            <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                            <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                            <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                        </asp:GridView>
                                        <br />
                                    </center>
                                </div>


                                <br />
                            </div>

                            <%-- <div id="divApproval" runat="server">
                            <table>
                                <tr>
                                    <td style="font-size: larger; color: red">You are not Authorized to mark attendance for this date , Kindly make Approval from Principal Sir.
                                    </td>

                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Button ID="btnsendApproval" runat="server" Text="Send Request" OnClick="btnsendApproval_Click" />
                                    </td>
                                </tr>
                            </table>

                        </div>--%>
                            <div id="div2" runat="server" visible="false">
                                <table>
                                    <tr>
                                        <td style="font-size: larger; color: green">You have already applied for this date.
                                        </td>

                                    </tr>
                                </table>

                            </div>
                        </asp:Panel>
                        <asp:Button ID="btnDummy" runat="server" Style="display: none;" />
                        <asp:ModalPopupExtender ID="GridViewDetails" runat="server" TargetControlID="btnDummy"
                            PopupControlID="pnlGridViewDetails" BackgroundCssClass="modalBackground" />


                        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                   <asp:Label ID="lblstu" Text="STUDENT LIST:-"  Visible="false" runat="server"  Font-Bold="true"></asp:Label>
                                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                   <asp:Label ID="lblUnit" Text="UNIT :" runat="server" Visible="false"></asp:Label> <asp:DropDownList ID="drpUnit" runat="server" Width="200px" Visible="false" ></asp:DropDownList>
                                     <br />
                                      <br />
                                      

                                    <asp:GridView ID="grdAttendanceDetails" DataKeyNames="No_" runat="server" AutoGenerateColumns="False"
                                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"  GridLines="Horizontal"
                                        EmptyDataText="There are no data records to display." >
                                        <AlternatingRowStyle BackColor="#F7F7F7" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="SR.NO." ControlStyle-Width="400px">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                                <ItemStyle Width="3%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Student NAME" HeaderStyle-HorizontalAlign="Left" ControlStyle-Width="200px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("[Student Name]") %>' />

                                                </ItemTemplate>
                                                <ItemStyle Width="3%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Enroll. No." ControlStyle-Width="200px" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEnroll" runat="server" Text='<%# Eval("[Enrollment No_]") %>' />

                                                </ItemTemplate>
                                                <ItemStyle Width="3%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Present"  HeaderStyle-HorizontalAlign="Left" ControlStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkboxAttendance" Checked="true" TabIndex="1"  runat="server"  />
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Student No" ControlStyle-Width="200px"  HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNo" runat="server" Text='<%# Eval("[No_]") %>' />

                                                </ItemTemplate>
                                               
                                            </asp:TemplateField>



                                     
                                        </Columns>
                                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />

                                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                        <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />
                                        <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                        <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                        <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>--%>
                    </div>
                    <div style="text-align: right;">

                        <%-- OnClientClick="return Validate();"--%>
                        <br />
                        <br />
                        <br />
                        <asp:Button ID="BtnHideQuotation" runat="server" Style="display: none" />
                        <asp:ModalPopupExtender runat="server" ID="ModalPopupMsg" TargetControlID="BtnHideQuotation"
                            PopupControlID="PanelQuotation" BackgroundCssClass="modalbackground" RepositionMode="RepositionOnWindowScroll"
                            PopupDragHandleControlID="PanelQuotation" X="300" Y="150">
                        </asp:ModalPopupExtender>
                        <asp:Panel runat="server" ID="PanelQuotation" CssClass="modalPopupWhite">
                            <br />
                            <div id="divmsg" runat="server" style="text-align: center; padding-left: 10px; font: bold; color: green; margin-bottom: 10px;">
                            </div>
                            <div style="clear: both">
                            </div>
                            <div style="text-align: center">
                                <asp:Button ID="btncancelpopup" Text="Ok" Width="200px" OnClick="btncancelpopup_Click" runat="server" />
                            </div>
                        </asp:Panel>

                        <%-- <asp:Button runat="server" ID="btnSave2" BackColor="White" Height="0px" Width="0px" OnClick="btnSave1_Click" Visible="false" BorderColor="White" />
                        --%>
                    </div>

                    <div class="modal fade" id="myModal2" runat="server" visible="false">
                        <div class="modal-dialog" style="width: 400px; height: 100px">
                            <div class="modal-content">
                                <div class="modal-header" style="background-color: #88CCFF;">
                                    <div>
                                        <button type="button" class="close">&times;</button>
                                        <h4 class="modal-title"><b style="font-family: Arial; font-size: 15px; font-weight: bold">Number of Student - Present</b></h4>
                                    </div>
                                </div>
                                <div class="modal-body">
                                    <br />
                                    <center>
                                        <asp:Label runat="server" ID="lblNoOfStudent" Font-Bold="true" Font-Size="15px"></asp:Label>
                                        <asp:Label runat="server" ID="Label3" Font-Bold="true" Font-Size="15px" Text=" out of "></asp:Label></u>
                                    <asp:Label runat="server" ID="lblTotalNoOfStudent" Font-Bold="true" Font-Size="15px"></asp:Label></u>
                                    </center>
                                </div>
                                <div class="modal-footer" style="border-top-width: 0px">
                                    <asp:Button runat="server" ID="btnSave1" CssClass="btn-sm btn-primary" OnClientClick="Save();" class="close" data-dismiss="modal" Width="20%" Text="Ok"></asp:Button>
                                </div>
                            </div>

                        </div>
                    </div>


                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlmsg" runat="server" Visible="false" CssClass="leftBackground">

        <fieldset class="boxBody">
            <asp:Label ID="Label11" runat="server"
                Text="You are not Authorized for this page." Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

        </fieldset>
    </asp:Panel>
</asp:Content>

