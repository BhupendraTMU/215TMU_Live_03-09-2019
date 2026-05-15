<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="StudentFeedBackList.aspx.cs" Inherits="Faculty_StudentFeedBackList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "images/minus.png");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "../images/plus.png");
            $(this).closest("tr").next().remove();
        });
    </script>
    <script type="text/javascript">
        function PrintDiv() {
            var divToPrint = document.getElementById('printarea');
            var popupWin = window.open('', '_blank', 'width=300,height=400,location=no,left=200px');
            popupWin.document.open();

            // Grab all <link> and <style> tags from the main document and add them to the new window
            var styles = "";
            Array.from(document.querySelectorAll("link[rel='stylesheet'], style")).forEach(function (styleNode) {
                styles += styleNode.outerHTML;
            });

            popupWin.document.write(`
        <html>
            <head>
                <title>Print Preview</title>
                ${styles}
            </head>
            <body onload="window.print(); window.close();">
                ${divToPrint.innerHTML}
            </body>
        </html>
    `);
            popupWin.document.close();
        }
    </script>
    <style>
        html, body {
            height: 100%;
        }

        body {
            margin: 0;
            background: #f3f3f3;
            font-family: Arial, Helvetica, sans-serif;
            color: #000;
        }

        .sheet {
            width: 794px;
            min-height: 600px;
            margin: 24px auto;
            background: #fff;
            padding: 28px 36px 40px;
            box-sizing: border-box;
            box-shadow: 0 2px 8px rgba(0,0,0,.12);
        }

        h1.title {
            text-align: center;
            font-size: 18px;
            margin: 0 0 16px 0;
            text-transform: none;
            font-weight: 700;
        }


        .two-col {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 12px;
            margin-bottom: 12px;
        }

        .block h4 {
            margin: 0 0 4px;
            font-size: 14px;
            font-weight: 700;
        }

        .block p {
            margin: 2px 0;
            font-size: 14px;
            line-height: 1.4;
        }

        .gridmargin {
            margin-left: -10px;
        }

        /*.salute {
            margin: 16px 0 6px;
            font-size: 14px;
        }
        .intro {
            font-size: 14px;
            line-height: 1.5;
            text-align: justify;
            margin: 0 0 14px;
        }

    
        .section-title {
            font-size: 14px;
            font-weight: 700;
            margin: 14px 0 8px;
            text-decoration: none;
        }*/


        table {
            /*width: 100%;*/
            /*border-collapse: collapse;*/
            table-layout: fixed;
        }

        .criteria th, .criteria td {
            border: 1px solid #333;
            padding: 8px 10px;
            font-size: 14px;
        }

        .criteria th {
            background: #efefef;
            font-weight: 700;
            text-align: left;
        }

        .criteria .col-q {
            width: 110px;
        }

        .criteria .col-param {
            width: auto;
        }

        .criteria .col-mark {
            width: 80px;
            text-align: center;
        }

        .scale-note {
            margin: 14px 0 8px;
            font-size: 12px;
        }

        .scale {
            width: 520px;
        }

            .scale th, .scale td {
                border: 1px solid #333;
                padding: 10px;
                font-size: 12px;
                text-align: center;
            }

        .muted {
            color: #555;
        }

        @media print {
            body {
                background: #fff;
            }

            .sheet {
                width: auto;
                min-height: auto;
                margin: 0;
                box-shadow: none;
                padding: 20mm;
            }

            .gridmargin {
                margin-left: -10px;
            }

            .scale {
                width: 100%;
            }

            .page-number:after {
                counter-increment: page;
                position: fixed;
                bottom: 0;
                left: 0;
                text-align: center;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <br />
    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="Feedback List" Font-Size="15pt" ForeColor="#093A62"
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    Academic Year:&nbsp&nbsp
                    <asp:DropDownList ID="ddlAcademicYear" Width="100px" Height="20px" runat="server"></asp:DropDownList>
    </fieldset>
    <fieldset class="boxBodyHeader">
    </fieldset>
    <center>
        <fieldset class="boxBodyInner">
            <fieldset class="boxBodyInner">
                <table width="100%">
                    <tr>
                        <td align="left" width="40%" id="tdCollege" runat="server">
                            <asp:DropDownList ID="ddcollege" runat="server" Width="100%" Height="35px"></asp:DropDownList>
                        </td>
                        <td align="left" width="20%">
                            <asp:DropDownList ID="ddsem" runat="server" Height="35px" Width="100%">
                                <asp:ListItem Value="--- Select Semester/Year ----">--- Select Semester/Year----</asp:ListItem>
                                <asp:ListItem Value="Even">Even Sem</asp:ListItem>
                                <asp:ListItem Value="Odd">Odd Sem</asp:ListItem>
                                <asp:ListItem Value="Year">Year</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td width="2%">&nbsp;</td>
                        <td width="18%" align="left">
                            <asp:Button ID="btnshow" runat="server" Visible="true" Text="Show" OnClick="btnshow_Click" Width="100px" Height="32px" /></td>
                        <td width="20%" align="right">
                            <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/images/excel.jpg" Height="35px" OnClick="btnExport_Click"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
                <table id="gridtbl">
                    <tr>
                        <td>
                            <%--<label  style="font-size:15px; font-weight:bold; font-family:Century Schoolbook">&nbsp;&nbsp;(Rate of Dimensions) as P-Poor, A-Average, G-Good, VG-Very Good & E-Excellent.</label>--%>
     
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="EduGridView" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
                                EmptyDataText="There are no data records to display." BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="1100px"
                                GridLines="Horizontal">
                                <Columns>

                                    <asp:TemplateField HeaderText="Sl. No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Faculty" SortExpression="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="txtname" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Emp Code" SortExpression="Emp Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpCode" runat="server" Text='<%# Eval("Emp Code") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="D.O.J" HeaderText="D.O.J" DataFormatString="{0:dd-MMM-yyyy}" />
                                    <asp:BoundField HeaderText="Designation" DataField="Designation" />

                                    <asp:TemplateField HeaderText="Emp Code" SortExpression="Emp Code">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEmpCode" runat="server" Text="Generate Report" OnClick="lblEmpCode_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                </Columns>
                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                <SortedDescendingHeaderStyle BackColor="#3E3277" />
                            </asp:GridView>

                            <asp:Panel ID="pnlGridViewDetails" CssClass="modalPopup" runat="server" Style="display: none; width: 800px; height: 700px">
                                <div class="header">
                                    <b>
                                        <asp:Label ID="lblNotification" runat="server" Text="Feedback Detail"></asp:Label></b><div class="close">

                                            <asp:Button ID="btnclose" runat="server" Text="X" />
                                        </div>
                                </div>
                                <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="PrintDiv();" />
                                <div id="printarea" class="sheet" style="overflow: scroll; height: 400px">
                                    <div class="page-number"></div>
                                    <fieldset class="boxBody">
                                        <div style="width: 100%; margin-bottom: 10px; margin-left: 1%; margin-right: 1%; margin-top: 5px;">
                                            <table style="width: 98%;">
                                                <tr>
                                                    <td style="width: 1%"></td>
                                                    <td style="width: 12%; text-align: left">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/tmulogo.png" Width="55%" />
                                                    </td>
                                                    <td style="width: 65%; text-align: center">
                                                        <strong>
                                                            <asp:Label ID="Label28" Font-Size="X-Large" Text="TEERTHANKER MAHAVEER UNIVERSITY" Font-Names="Times New Roman" runat="server"></asp:Label></strong>
                                                        <br />
                                                        <br />
                                                        <strong>
                                                            <asp:Label ID="Label29" Font-Size="Large" Text="(Established under Govt. of U. P. Act No. 30, 2008)" Font-Names="Times New Roman" runat="server"></asp:Label></strong>

                                                        <br />
                                                        <br />
                                                        <strong>
                                                            <asp:Label ID="Label30" runat="server" Font-Size="Large" Font-Names="Times New Roman" Text="Delhi Road, Moradabad - 244001, U.P."></asp:Label></strong>

                                                    </td>

                                                    <td style="width: 10%; text-align: center"></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </fieldset>
                                    <h1 class="title" style="text-align: center">Inter Office Memo</h1>

                                    <div class="two-col">
                                        <div class="block" style="text-align: left;">
                                            <h4>To,</h4>
                                            <p>
                                                <asp:Label ID="empName" runat="server"></asp:Label>
                                            </p>
                                            <p>
                                                <asp:Label ID="lblDep" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                        <div class="block" style="text-align: right;">
                                            <p>
                                                Date............................&nbsp&nbsp&nbsp&nbsp&nbsp
                                            </p>
                                        </div>
                                    </div>

                                    <p class="salute">
                                        <asp:Label ID="lblEmplName" runat="server" Font-Bold="true"></asp:Label>,
                                    </p>
                                    <p class="intro">
                                        In order to create the best Teaching–Learning in the University, a system of feedback is
                developed. A faculty member has been evaluated by student on the following parameters.
                                    </p>

                                    <div class="section-title" style="font-weight: bold; font-size: 12px;">Assessment Criteria</div>

                                    <table class="criteria" style="width: 100%">
                                        <thead>
                                            <tr>
                                                <th class="col-q">Question. No</th>
                                                <th class="col-param">Parameters</th>
                                                <th class="col-q">Question. No</th>
                                                <th class="col-param">Parameters</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td style="text-align: center">1</td>
                                                <td>Subject Knowledge (P1)</td>
                                                <td style="text-align: center">6</td>
                                                <td>Participation in Class (P6)</td>

                                            </tr>
                                            <tr>
                                                <td style="text-align: center">2</td>
                                                <td>Explanation Power (P2)</td>
                                                <td style="text-align: center">7</td>
                                                <td>Presentation in Skills (P7)</td>

                                            </tr>
                                            <tr>
                                                <td style="text-align: center">3</td>
                                                <td>Speed of Teaching (P3)</td>
                                                <td style="text-align: center">8</td>
                                                <td>Quality of Assignments (P8)</td>

                                            </tr>
                                            <tr>
                                                <td style="text-align: center">4</td>
                                                <td>Problem Solving Ability (P4)</td>
                                                <td style="text-align: center">9</td>
                                                <td>Understanding of the Content (P9)</td>

                                            </tr>
                                            <tr>
                                                <td style="text-align: center">5</td>
                                                <td>Punctuality in Class (P5)</td>
                                                <td style="text-align: center">10</td>
                                                <td>Comfort Level with Faculty (P10)</td>
                                            </tr>





                                        </tbody>
                                    </table>

                                    <p class="scale-note" style="font-weight: bold; font-size: 12px;">Each parameter has been evaluated on the scale of 01 to 05 from:</p>

                                    <table class="scale" style="width: 100%;">
                                        <tr>
                                            <td><strong>Excellent</strong><br />
                                                <span class="muted">(5)</span></td>
                                            <td><strong>Good</strong><br />
                                                <span class="muted">(4)</span></td>
                                            <td><strong>Average</strong><br />
                                                <span class="muted">(3)</span></td>
                                            <td><strong>Needs Improvement</strong><br />
                                                <span class="muted">(2)</span></td>
                                            <td><strong>Poor</strong><br />
                                                <span class="muted">(1)</span></td>
                                        </tr>
                                    </table>

                                    <p class="scale-note">
                                        <asp:Label ID="lblfeedback" runat="server" Font-Bold="true"></asp:Label>
                                    </p>
                                    <table class="scale" style="width: 100%">
                                        <tr>
                                            <td colspan="5" style="border: none">
                                                <asp:GridView ID="grdParameters" CssClass="gridmargin" ShowHeader="false" ShowFooter="True" OnDataBound="grdParameters_DataBound" OnRowCreated="grdParameters_RowCreated" runat="server">
                                                    <FooterStyle BackColor="#B5C7DE" Font-Bold="true" ForeColor="#4A3C8C" />
                                                </asp:GridView>

                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <%--<p class="scale-note" style="font-weight: bold; font-size: 13px;"><u>Remarks</u></p>--%>
                                    <table class="scale" style="width: 100%;">
                                        <tr style="text-align: left">
                                            <td style="text-align: left; height: 150px"><strong>Remarks</strong></td>

                                        </tr>
                                    </table>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <div class="section-title" style="font-weight: bold; font-size: 12px;">
                                        Prof. (Dr.) V.K.Jain
                                        <br />
                                        (Vice Chancellor)
                                    </div>
                                    <br />
                                    <br />

                                    <div class="section-title" style="font-weight: bold; font-size: 12px;">
                                        CC:
                                        
                                        &nbsp&nbsp
                                        <ul>
                                            <li>Personal File of Faculty
                                            </li>
                                            <li>Office File of the College/Department
                                            </li>
                                            <li>VC Office File/IQAC File
                                            </li>
                                        </ul>
                                    </div>
                                </div>


                            </asp:Panel>
                            <asp:Button ID="btnDummy" runat="server" Style="display: none;" />
                            <asp:ModalPopupExtender ID="MpaDetails" runat="server" TargetControlID="btnDummy" PopupControlID="pnlGridViewDetails" BackgroundCssClass="modalBackground" />
                        </td>
                    </tr>

                </table>
            </fieldset>
        </fieldset>
    </center>



</asp:Content>

