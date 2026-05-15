<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="HandOutUpload.aspx.cs" Inherits="Faculty_HandOutUpload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <style type="text/css">
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
            height: 83px;
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
            width: 250px;
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


    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        .Grid td {
            background-color: #A1DCF2;
            color: black;
            font-size: 10pt;
            line-height: 200%;
        }

        .Grid th {
            background-color: #3AC0F2;
            color: White;
            font-size: 10pt;
            line-height: 200%;
        }

        .ChildGrid td {
            background-color: #eee !important;
            color: black;
            font-size: 10pt;
            line-height: 200%;
        }

        .ChildGrid th {
            background-color: #6C6C6C !important;
            color: White;
            font-size: 10pt;
            line-height: 200%;
        }

        .sendbtn {
            display: block;
            font-size: 26px;
            padding: 3px 6px;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="mrak" runat="server">
        <ContentTemplate>
            <fieldset class="boxBody">
    <table cellpadding="0px" cellspacing="0px" width="99%">
        <tr>
            <td style="width: 25%">
                <asp:Label ID="Label1" runat="server"
                    Text="HandOut Upload" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
            </td>
            <td style="width: 25%">
                <asp:Label ID="lbltotalst" runat="server" Visible="false"></asp:Label><asp:Label ID="lblpresentst" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblabsentst" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="LblUFMCount" runat="server" Visible="false"></asp:Label>
   
            </td>

            <td style="width: 50%; text-align: right;visibility:hidden">
                <asp:ImageButton ID="Btnprint" runat="server" ImageUrl="~/images/pdf.jpg" OnClientClick="PrintDiv();" Width="40px" Height="30px" Visible="false"></asp:ImageButton>
            </td>
        </tr>
    </table>
</fieldset>
            <fieldset class="boxBodyInner">
                <asp:Panel ID="pnlMain" runat="server">
                    <table>
                        <tr>
                            <td>Academic Year</td>
                            <td>
                                <asp:DropDownList ID="drpAcademicYear" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="drpAcademicYear_SelectedIndexChanged"
                                    Width="100px" />
                            </td>
                            <td>Program</td>
                            <td>
                                <asp:DropDownList ID="drpCourse" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="drpCourse_SelectedIndexChanged"
                                    Width="150px" />
                            </td>
                            <td>Course</td>
                            <td>
                                <asp:DropDownList ID="ddlSubject" runat="server" Width="120px" />
                            </td>
                            <td>Upload File</td>
                            <td>
                                <asp:FileUpload ID="FileUpload1" runat="server" Width="220px" accept=".pdf" />
                            </td>
                            <td>
                                <asp:Button ID="btnShow" runat="server" Text="Upload" CssClass="btn btn-primary"
                                    ValidationGroup="g1" OnClick="btnUpload_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </fieldset>

                        <fieldset class="boxBody">
                            <asp:GridView ID="gvHandOuts" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered"
                                AllowPaging="true" PageSize="10" OnPageIndexChanging="gvHandOuts_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="AcademicYear" HeaderText="Academic Year" />
                                    <asp:BoundField DataField="Course" HeaderText="Program" />
                                    <asp:BoundField DataField="Subject" HeaderText="Subject" />
                                    <asp:BoundField DataField="CreateBy" HeaderText="Uploaded By" />
                                    <asp:BoundField DataField="CreatedAt" HeaderText="Uploaded On" DataFormatString="{0:dd-MMM-yyyy HH:mm}" />
                                <asp:TemplateField HeaderText="File">
                    <ItemTemplate>
                        <asp:HyperLink ID="lnkDownload" runat="server" 
                            Text="Download" 
                            NavigateUrl='<%# Eval("FilePath") %>' 
                            Target="_blank" />
                    </ItemTemplate>
                </asp:TemplateField>
                                    </Columns>
                            </asp:GridView>

                        </fieldset>


        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnShow" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>