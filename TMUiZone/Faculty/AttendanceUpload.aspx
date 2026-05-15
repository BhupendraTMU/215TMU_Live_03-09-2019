<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="AttendanceUpload.aspx.cs" Inherits="Faculty_AttendanceUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .loader {
            position: fixed;
            left: 45%;
            top: 45%;
            width: 100px;
            height: 100px;
            z-index: 9999;
            background: url('../images/loader.gif') 50% 50% no-repeat rgb(249,249,249);
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

        function Save() {
            var elem = document.getElementById("Loader1");
            elem.style.display = "block";
            $(".loader").fadeIn("slow");
            $('[id$=btnSave]').click();
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">

        <table width="95%">
            <tr>
                <td width="20%">
                    <asp:Label ID="Label1" runat="server"
                        Text="Attendance Upload" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                </td>
                <td width="35%">
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:FileUpload ID="FileUpload1" runat="server" Height="25px" />
                            </td>
                            <td>&nbsp&nbsp
                                <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" Height="25px" Width="70px" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td align="left" width="20%"></td>
                <td align="right" width="25%">
                    <table>
                        <tr>
                           
                            <td align="right">
                                <a href="../Files/AttendanceUploadFormat.xlsx" class="btn btn-info btn-sm">
                                    <span class="glyphicon glyphicon-download"></span>Download Format
                                </a>

                            </td>
                        </tr>
                    </table>


                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset class="boxBodyHeader">
    </fieldset>
    <div>
        <table width="100%">
            <tr>
                <td width="5%">
                    <asp:HiddenField ID="hfExtension" runat="server" />
                    <asp:HiddenField ID="hfFilePath" runat="server" />
                    <asp:HiddenField ID="hfUploadSave" runat="server" />

                </td>
                <td align="left"></td>
                <td align="left"></td>
                <td width="25%">
                    <asp:RadioButtonList ID="rbHDR" runat="server" Visible="false">
                        <asp:ListItem Text="Yes" Value="Yes" Selected="True">   </asp:ListItem>
                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                    </asp:RadioButtonList>

                </td>
            </tr>
            <tr>
                <td></td>
            </tr>

            <tr>
                <td colspan="4" align="center">
                    <asp:GridView ID="grdUpload" runat="server" BorderWidth="1px" BackColor="#DEBA84" CellPadding="3" CellSpacing="2" BorderStyle="None" BorderColor="#DEBA84" Width="98%">
                        <HeaderStyle ForeColor="White" Font-Bold="True" BackColor="#A55129"></HeaderStyle>
                        <FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
                    </asp:GridView>
                </td>

            </tr>
            <tr>
                <td colspan="4" style="height: 10px; text-align: right">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="4" style="height: 20px" align="right">

                    <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" Visible="false" CssClass="btn btn-info btn-sm" Width="130px" />
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                
                </td>
            </tr>
        </table>
    </div>



</asp:Content>

