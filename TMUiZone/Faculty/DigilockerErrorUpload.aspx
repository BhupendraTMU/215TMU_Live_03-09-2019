<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="DigilockerErrorUpload.aspx.cs" Inherits="Faculty_DigilockerErrorUpload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Excel Upload & Preview</title>

    <style>
        body {
            font-family: Arial;
            background: #f4f6f9;
            margin: 0;
            padding: 20px;
        }

        .container {
            width: 95%;
            margin: auto;
            background: white;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0px 0px 10px #ccc;
        }

        .heading {
            font-size: 24px;
            font-weight: bold;
            color: #2c3e50;
            margin-bottom: 20px;
        }

        .upload-section {
            margin-bottom: 20px;
        }

        .btn {
            padding: 8px 18px;
            border: none;
            cursor: pointer;
            color: white;
            font-weight: bold;
            border-radius: 4px;
        }

        .btnPreview {
            background-color: #007bff;
        }

        .btnPost {
            background-color: #007bff;
            color: white;
            border: none;
        }

        .btnSelected {
            background-color: #28a745 !important;
            color: white !important;
            font-weight: bold;
            border: 2px solid #155724;
        }

        .btnClear {
            background-color: #dc3545;
        }

        .grid {
            width: 100%;
            margin-top: 15px;
        }

            .grid th {
                background-color: #343a40;
                color: white;
                padding: 8px;
            }

            .grid td {
                padding: 6px;
            }

        .msg {
            font-weight: bold;
            color: green;
            margin-top: 10px;
        }

        .count {
            color: #003366;
            font-weight: bold;
            margin-top: 10px;
        }

        .grid {
            font-size: 11px;
        }

            .grid th {
                font-size: 11px;
                font-weight: bold;
            }

            .grid td {
                font-size: 11px;
            }
    </style>
    <script type="text/javascript">
        function SelectAll(source) {
            var gv = document.getElementById('<%= gvData.ClientID %>');
            var checkBoxes = gv.querySelectorAll("input[type='checkbox']");

            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i] != source) {
                    checkBoxes[i].checked = source.checked;
                }
            }
        }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">

        <div class="heading">
            Student ABC Account Error Upload
       
        </div>

        <div class="upload-section">

            <table>
                <tr>
                    <td id="tdExcel" runat="server">Select Excel File :</td>

                    <td>
                        <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="true" />
                    </td>

                    <td>
                        <asp:Button ID="btnPreview"
                            runat="server"
                            Text="Preview Data"
                            CssClass="btn btnPreview"
                            OnClick="btnPreview_Click" />
                    </td>

                    <td>
                        <asp:Button ID="btnPost"
                            runat="server"
                            Text="Post Data"
                            Visible="false"
                            CssClass="btn btnPost"
                            OnClick="btnPost_Click" />
                    </td>

                    <td>
                        <asp:Button ID="btnClear"
                            runat="server"
                            Text="Clear"
                            CssClass="btn btnClear"
                            OnClick="btnClear_Click" />
                    </td>

                    <td>
                        <asp:Button ID="btnReport"
                            runat="server"
                            Text="Uploaded Data Report"
                            CssClass="btn btnPost"
                            OnClick="btnReport_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnPosted"
                            runat="server"
                             Visible="false"
                            Text="Posted Data Report"
                            CssClass="btn btnPost"
                            OnClick="btnPosted_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnCollectionReport"
                            runat="server"
                            Text="Collected Data Report"
                            CssClass="btn btnPost"
                             Visible="false"
                            OnClick="btnCollectionReport_Click" />
                    </td>
                </tr>
            </table>

        </div>

        <hr />

        <asp:Panel ID="pnlReport" runat="server" Visible="false">

            <table>
                <tr>

                    <td>Enrollment/College :
                    </td>

                    <td>
                        <asp:TextBox ID="txtEnrollmentNo"
                            runat="server"
                            Width="200px">
                        </asp:TextBox>
                    </td>

                    <td>
                        <asp:Button ID="btnSearch"
                            runat="server"
                            Text="Search"
                            CssClass="btn btnPreview"
                            OnClick="btnSearch_Click" />
                    </td>

                    <td>
                        <asp:Button ID="btnShowAll"
                            runat="server"
                            Text="Show All"
                            CssClass="btn btnPost"
                            OnClick="btnShowAll_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnFPost"
                            runat="server"
                            Text="Final Post"
                            CssClass="btn btnPost"
                            OnClick="btnFPost_Click" />
                    </td>

                </tr>
            </table>

        </asp:Panel>

        <br />

        <asp:Label ID="lblTotalRecords"
            runat="server"
            CssClass="count"></asp:Label>

        <br />

        <asp:Label ID="lblMsg"
            runat="server"
            CssClass="msg"></asp:Label>

        <br />
        <br />
        <div style="overflow: scroll">
            <asp:GridView ID="gvData"
                runat="server"
                AutoGenerateColumns="False"
                CssClass="grid"
                GridLines="Both"
                DataKeyNames="REGN_NO">

                <Columns>

                    <asp:TemplateField HeaderText="Select">
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkAll" runat="server"
                                onclick="SelectAll(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="INVALID_COLUMNS"
                        HeaderText="Invalid Columns" />

                    <asp:BoundField DataField="ORG_NAME"
                        HeaderText="Organization" />

                    <asp:BoundField DataField="ACADEMIC_COURSE_ID"
                        HeaderText="Course ID" />

                    <asp:BoundField DataField="SESSION"
                        HeaderText="Session" />

                    <asp:BoundField DataField="REGN_NO"
                        HeaderText="Enrollment No" />

                    <asp:BoundField DataField="CNAME"
                        HeaderText="Student Name" />

                    <asp:BoundField DataField="collegeCode"
                        HeaderText="College Code" />

                    <asp:BoundField DataField="GENDER"
                        HeaderText="Gender" />

                    <asp:BoundField DataField="DOB"
                        HeaderText="DOB" />

                    <asp:BoundField DataField="FNAME"
                        HeaderText="Father Name" />

                    <asp:BoundField DataField="MNAME"
                        HeaderText="Mother Name" />

                </Columns>

            </asp:GridView>
        </div>
    </div>
</asp:Content>

