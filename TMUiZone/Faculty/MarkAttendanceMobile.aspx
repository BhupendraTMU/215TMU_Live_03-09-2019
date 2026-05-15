<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MarkAttendanceMobile.aspx.cs" Inherits="Faculty_MarkAttendanceMobile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

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
            height: 333px;
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
            width:1000px;
           
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
    </style>



    <script type="text/javascript">

        function callFeedbackMessage(inputType, inputText) {

            if (inputType == 'Error') {
                alertify.error(inputText);
                return false;
            }
            else if (inputType == 'Success') {
                alertify.success("Save Successfully");
                return false;
            }
            else {
                alertify.log(inputText, "", 10000);
                return false;
            }
        }

        function Count() {
            var j = 0;
            var k = 0;

            var GridId = "<%=grdAttendanceDetails.ClientID %>";
            var grid = document.getElementById(GridId);
            rowscount = grid.rows.length - 1;

            for (i = 0; i < parseInt(rowscount) ; i++) {
                var checkBox = document.getElementById('grdAttendanceDetails_chkboxAttendance_' + i + '');


                if (checkBox.checked == true) {

                    j++;
                }
                k++;
            }
            document.getElementById('lblNoOfStudent').text = j;
            document.getElementById('lblTotalNoOfStudent').text = k;

            $('#' + myModal2).dialog();

        }
        function Save() {
            var elem = document.getElementById("Loader1");
            elem.style.display = "block";
            $(".loader").fadeIn("slow");
            $('[id$=btnSave2]').click();
        }
        function Validate() {

            var drpCurrent = document.getElementById('<%= drpUnit.ClientID %>').value;
           
            if (drpCurrent == "") {
                alert("Please Select Unit.");
                return false;
            }
            return true;
        }
    </script>
    <style type="text/css">
        @media (min-width: 1000px) {
            .container {
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
</head>
<body>

    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="below-slideshow" style="padding-bottom: 0px">
            <div class="container">
                <!-- NOTICE HERE:inside div declare class as container/container-fluid-->
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div>

                            <h1 class="head-line" style="font-size:50px">STUDENT Details</h1>
                            <asp:GridView ID="grdTimetable" DataKeyNames="Entry No_,Subject Code,Course Code,Semester" AutoGenerateColumns="false" runat="server" BackColor="White"
                                BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="128%" GridLines="Horizontal"
                                EmptyDataText="There are no data records to display." Font-Size="70px" HeaderStyle-BackColor="SteelBlue">
                                <AlternatingRowStyle BackColor="#F7F7F7" />
                                <Columns>
<%--                                    <asp:TemplateField HeaderText="Sr. No." ControlStyle-Width="300px" >
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle Width="3%" />
                                    </asp:TemplateField>--%>
                                  
                                    <asp:TemplateField HeaderText="Lect" ControlStyle-Width="160px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lbllecture" runat="server" Text='<%# Eval("[lecture]") %>' />

                                        </ItemTemplate>
                                        <ItemStyle Width="3%" />
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Course" ControlStyle-Width="300px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("[Course Code]") %>' />

                                        </ItemTemplate>
                                        <ItemStyle Width="3%" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Subject" ControlStyle-Width="350px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubject" runat="server" Text='<%# Eval("[Subject Code]") %>' />

                                        </ItemTemplate>
                                        <ItemStyle Width="3%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date" ControlStyle-Width="400px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDate" Font-Size="60px" runat="server" Text='<%# Eval("[Date]") %>' />

                                        </ItemTemplate>
                                        <ItemStyle Width="3%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sem" ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSem" runat="server" Text='<%# Eval("[Semester]") %>' />

                                        </ItemTemplate>
                                        <ItemStyle Width="3%" />
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Button ID="btnSelect" Width="200px" Height="70px" Font-Size="50px" runat="server" Text='Select' OnClick="btnSelect_Click" />

                                        </ItemTemplate>
                                        <ItemStyle Width="3%" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <br />
                            <br />

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                   <asp:Label ID="lblstu" Text="STUDENT LIST:-"  Visible="false" runat="server" Font-Size="50px" Font-Bold="true"></asp:Label>
                                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                   <asp:Label ID="lblUnit" Text="UNIT :" Font-Size="50px" runat="server" Visible="false"></asp:Label> <asp:DropDownList ID="drpUnit" runat="server" Width="200px" Visible="false" Font-Size="50px"></asp:DropDownList>
                                     <br />
                                    <asp:GridView ID="grdAttendanceDetails" DataKeyNames="No_" runat="server" AutoGenerateColumns="False"
                                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="165%" GridLines="Horizontal"
                                        EmptyDataText="There are no data records to display." Font-Size="50px">
                                        <AlternatingRowStyle BackColor="#F7F7F7" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="SR.NO." ControlStyle-Width="400px">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                                <ItemStyle Width="3%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="NAME" HeaderStyle-HorizontalAlign="Left" ControlStyle-Width="450px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("[Student Name]") %>' />

                                                </ItemTemplate>
                                                <ItemStyle Width="3%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ENROLL. NO." ControlStyle-Width="400px" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEnroll" runat="server" Text='<%# Eval("[Enrollment No_]") %>' />

                                                </ItemTemplate>
                                                <ItemStyle Width="3%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="P"  HeaderStyle-HorizontalAlign="Left" ControlStyle-Width="100px">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkboxAttendance" Checked="true" TabIndex="1" Font-Size="150px" runat="server" CssClass="ChkBoxClass" />
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Student No" ControlStyle-Width="350px"  HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNo" runat="server" Text='<%# Eval("[No_]") %>' />

                                                </ItemTemplate>
                                                <ItemStyle Width="3%" />
                                            </asp:TemplateField>



                                            <%--       <asp:TemplateField>
                                    <ItemTemplate >
                                        <asp:LinkButton ID="lnkPercentage" OnClientClick="return BindGrid(this);"   HeaderText="Today"  CommandArgument='<%# Eval("Percentage") %>' runat="server" />                                                               
                                    </ItemTemplate>
                                        <HeaderTemplate>Percentage</HeaderTemplate>
                                    </asp:TemplateField>--%>
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
                            </asp:UpdatePanel>
                        </div>
                        <div style="text-align: center; width: 1500px">
                            <asp:Button ID="btnSubmit" runat="server" Text="SUBMIT" class="buttonstyleCss" Font-Size="70px" Visible="false"  OnClick="btnSubmit_Click" />
                             <%-- OnClientClick="return Validate();"--%>
                                
                            <asp:Button ID="BtnHideQuotation" runat="server" Style="display: none" />
                            <asp:ModalPopupExtender runat="server" ID="ModalPopupMsg" TargetControlID="BtnHideQuotation"
                                PopupControlID="PanelQuotation" BackgroundCssClass="modalbackground" RepositionMode="RepositionOnWindowScroll"
                                PopupDragHandleControlID="PanelQuotation" X="370"  Y="700" >
                            </asp:ModalPopupExtender>
                            <asp:Panel runat="server" ID="PanelQuotation" CssClass="modalPopupWhite" >
                                <br />
                                <div id="divmsg" runat="server" style="text-align: center; padding-left:50px; font: bold; font-size: 50px; color: green; margin-bottom: 20px;">
                                </div>
                                <div style="clear: both">
                                </div>
                                <div style="text-align: center">
                                    <asp:Button ID="btncancelpopup" Text="Ok" Width="200px" Font-Size="50" OnClick="btncancelpopup_Click" runat="server" />
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
                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
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
    </form>
</body>
</html>
