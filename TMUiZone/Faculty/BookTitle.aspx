<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="BookTitle.aspx.cs" Inherits="Faculty_BookTitle" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function Confirm() {

            var TOJ = document.getElementById("ContentPlaceHolder1_txtTOJ").value;
            if (TOJ == "") {
                alert('Title of Journal can not be blank');
                return false;

            }
            else {

                return true; 
            }
        }
        function Confirm1() {

            var TOJ = document.getElementById("ContentPlaceHolder1_txtTOJ").value;
            var WhetherSCOPUS = document.getElementById("ContentPlaceHolder1_txtWhetherSCOPUS").value;
            var DOS = document.getElementById("ContentPlaceHolder1_txtDOS").value;
            if (TOJ == "") {
                alert('Title of Journal can not be blank');
                return false;

            }
            else if (WhetherSCOPUS == "") {
                alert('Whether SCOPUS can not be blank');
                return false;

            }
            else if (DOS == "") {
                alert('Date of Submission can not be blank');
                return false;

            }
            else {

                return true;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="Research Incentive" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
        &nbsp&nbsp&nbsp&nbsp&nbsp 
        <label style="line-height: 25px; font-size: large">Title : &nbsp&nbsp</label>
        <asp:DropDownList ID="drpTitle" runat="server" Height="28px" Width="200px" OnSelectedIndexChanged="drpTitle_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
    </fieldset>
    <div id="divGeneralBody">
        <fieldset class="boxBodyInner">

            <table cellpadding="0px" cellspacing="0px">
                <tr>
                    <td colspan="15">
                        <table cellpadding="0px" cellspacing="0px">
                            <tr>

                                <td>
                                    <label style="line-height: 25px">Author Name</label>
                                </td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:TextBox ID="txtAName" runat="server" Enabled="False" Width="220px"></asp:TextBox>
                                </td>




                                <td style="width: 100px"></td>
                                <td style="width: 200px">
                                    <label style="line-height: 25px">Author Code </label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAuthorCode" runat="server" Enabled="False" Width="220px"></asp:TextBox>
                                </td>

                            </tr>
                            <tr>
                                <td colspan="11" style="height: 10px"></td>
                            </tr>
                            <tr>
                                <td style="width: 200px">
                                    <label style="line-height: 25px">Position of Author </label>
                                </td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:TextBox ID="txtAuthors" runat="server" Width="220px" Enabled="false"></asp:TextBox>
                                    <asp:HiddenField ID="hfAuthor" runat="server" />
                                </td>





                                <td style="width: 100px"></td>
                                <td>
                                    <label style="line-height: 25px">Title</label>
                                </td>

                                <td>
                                    <asp:TextBox ID="txtTitleofJournal" runat="server" Enabled="False" Width="220px"></asp:TextBox>
                                </td>


                            </tr>
                            <tr>
                                <td colspan="11" style="height: 10px"></td>
                            </tr>
                            <tr>
                                <td>
                                    <label style="line-height: 25px">Claim Type</label>
                                </td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:TextBox ID="txtclaimType" runat="server" Width="220px" Enabled="false"></asp:TextBox>
                                </td>
                                <td style="width: 100px"></td>
                                <td id="tdTOB" runat="server" visible="false">
                                    <label style="line-height: 25px">Title of the Book</label>
                                </td>

                                <td id="tdTOB1" runat="server" visible="false">
                                    <asp:TextBox ID="txtTOTB" runat="server" Width="220px"></asp:TextBox>
                                </td>
                                <td id="tdP" runat="server" visible="false">
                                    <label style="line-height: 25px">Date of Publication</label>
                                </td>

                                <td id="tdP1" runat="server" visible="false">
                                    <asp:TextBox ID="txtDOP" runat="server" Width="220px"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDOP" Format="dd MMM yyyy">
                                    </cc1:CalendarExtender>
                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" WatermarkText="dd MMM yyyy" TargetControlID="txtDOP">
                                    </cc1:TextBoxWatermarkExtender>
                                </td>

                                <td id="tdTOG" runat="server" visible="false">
                                    <label style="line-height: 25px">Title of Journal</label>
                                </td>

                                <td id="tdTOG1" runat="server" visible="false">
                                    <asp:TextBox ID="txtTitleofGen" runat="server" Width="220px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="11" style="height: 10px"></td>
                            </tr>
                            <tr>
                                <td id="tdNOP" runat="server" visible="false">
                                    <label style="line-height: 25px">Name of Publisher</label>
                                </td>
                                <td id="tdIOJ" runat="server" visible="false" style="width: 200px">
                                    <label style="line-height: 25px">Indexing of Journal</label>
                                </td>
                                <td style="width: 10px"></td>
                                <td id="tdNOP1" runat="server" visible="false">
                                    <asp:TextBox ID="txtNOP" runat="server" Width="220px"></asp:TextBox>
                                </td>
                                <td id="tdIOJ1" runat="server" visible="false">
                                    <asp:TextBox ID="txtInOfJour" runat="server" Width="220px"></asp:TextBox>
                                </td>





                                <td style="width: 100px"></td>
                                <td id="tdTOP" runat="server" visible="false">
                                    <label style="line-height: 25px">Type of Publication</label>
                                </td>

                                <td id="tdTOP1" runat="server" visible="false">
                                    <asp:DropDownList ID="DRPTOP" runat="server" Width="220px" Height="32px">
                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="National" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="International" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>

                            </tr>
                            <tr>
                                <td colspan="11" style="height: 10px"></td>
                            </tr>

                            <tr>
                                <td id="tdRemark" runat="server" visible="false">
                                    <label style="line-height: 25px">Remarks </label>
                                </td>

                                <td style="width: 100px"></td>
                                <td id="tdRemark1" runat="server" visible="false">
                                    <asp:TextBox ID="txtRemark" runat="server" Enabled="false" TextMode="MultiLine" Width="220px"></asp:TextBox>
                                </td>

                                <td style="width: 10px"></td>
                                <td id="tdATBP" runat="server" visible="false">
                                    <label style="line-height: 25px">Amount to be Paid </label>
                                </td>
                                <td></td>
                                <td id="tdATBP1" runat="server" visible="false">
                                    <asp:TextBox ID="txtATBP" runat="server" Enabled="false" Width="220px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="11" style="height: 10px"></td>
                            </tr>

                            <tr>
                                <td></td>

                                <td style="width: 100px"></td>
                                <td></td>

                                <td style="width: 10px"></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return Confirm1()" OnClick="btnSubmit_Click" Width="220px"></asp:Button>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="11" style="height: 10px"></td>
                            </tr>


                            <tr style="border: 1px solid">
                                <td>
                                    <label style="line-height: 25px">Attachment Type</label>
                                </td>

                                <td style="width: 100px"></td>
                                <td>
                                    <asp:DropDownList ID="drpattachtype" Height="29px" runat="server">
                                        <asp:ListItem Text="Full Copy or research Article" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Patent" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Patent Filling Receipt" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Copy of Research" Value="4"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>

                                <td style="width: 10px"></td>
                                <td>Attachment : 
                                  

                                </td>
                                <td>
                                    <asp:FileUpload ID="flupload" runat="server" Height="29px" Width="220px"></asp:FileUpload>
                                </td>
                                <td style="text-align: right">
                                    <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClientClick="return Confirm()" OnClick="btnUpload_Click"></asp:Button>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="11" style="height: 10px"></td>
                            </tr>



                            <tr>


                                <td colspan="14">
                                    <asp:GridView ID="grdDocument" runat="server" AutoGenerateColumns="false" BackColor="White" BorderColor="#E7E7FF" HeaderStyle-BackColor="#ff9900" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="1150px" GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                                        <AlternatingRowStyle BackColor="#F7F7F7" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl. No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                                <ItemStyle Width="7%" />
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="AuthorCode" HeaderText="Author Code" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:BoundField DataField="Attachmentfor1" HeaderText="Attachment Type" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:BoundField DataField="File Name" HeaderText="File Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <asp:LinkButton ID="lnkDownload" Text="Download" CommandArgument='<%# Eval("ID") %>' runat="server" OnClick="DownloadInboxFile"></asp:LinkButton>
                                                        </ContentTemplate>

                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="lnkDownload" />

                                                        </Triggers>
                                                    </asp:UpdatePanel>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">

                                                        <ContentTemplate>
                                                            <asp:LinkButton ID="lnkdelete" Text="Delete" CommandArgument='<%# Eval("ID") %>' runat="server" OnClick="DeleteFile"></asp:LinkButton>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="lnkdelete" />

                                                        </Triggers>
                                                    </asp:UpdatePanel>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>







                                    </asp:GridView>
                                </td>
                            </tr>



                        </table>
                    </td>
                </tr>
            </table>

        </fieldset>
    </div>

</asp:Content>

