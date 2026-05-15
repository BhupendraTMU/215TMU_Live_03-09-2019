<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="SeedMoney.aspx.cs" Inherits="Faculty_SeedMoney" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        .column {
            border: 1px solid #000;
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 500px;
            height: 200px;
        }
    </style>
    <style type="text/css">
        .GridPager a, .GridPager span {
            display: block;
            height: 15px;
            width: 15px;
            font-weight: bold;
            text-align: center;
            text-decoration: none;
        }

        .GridPager a {
            background-color: #f5f5f5;
            border: 1px solid #969696;
        }

        .GridPager span {
            background-color: #A1DCF2;
            border: 1px solid #3AC0F2;
        }
    </style>
    <script type="text/javascript">
        function UncheckOthers(objchkbox) {

            var objchkList = objchkbox.parentNode.parentNode.parentNode;

            var chkboxControls = objchkList.getElementsByTagName("input");

            for (var i = 0; i < chkboxControls.length; i++) {

                if (chkboxControls[i] != objchkbox && objchkbox.checked) {

                    chkboxControls[i].checked = false;
                }
            }
        }
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                alert("Please Enter Only Numeric Value:");
                return false;
            }

            return true;
        }


        function HidePopup6() {

            $('#confirmModal6').modal('hide');
        }
        function HidePopup7() {

            $('#confirmModal7').modal('hide');
        }
        function confirmModal8() {

            $('#confirmModal8').modal('hide');
        }
        function confirmModal9() {

            $('#confirmModal9').modal('hide');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset class="boxBody">
        <asp:label id="Label1" runat="server"
            text="Seed Money" font-size="15pt" forecolor="#093A62" font-names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:label>

    </fieldset>
    <table cellpadding="0px" cellspacing="0px">
        <tr>
            <td style="width: 30px"></td>
            <td valign="top" style="width: 140px">
                <table cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td class="leftmMenu">&nbsp;<asp:linkbutton id="lnkSeedMoneyApplication" width="140px" runat="server" onclick="lnkSeedMoneyApplication_Click">Application</asp:linkbutton></td>
                    </tr>
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td class="leftmMenu">&nbsp;<asp:linkbutton id="lnkSeedMoneyApproval" width="140px" runat="server" onclick="lnkSeedMoneyApproval_Click">Approval</asp:linkbutton></td>
                    </tr>

                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td class="leftmMenu">&nbsp;<asp:linkbutton id="lnkClaimApplication" width="140px" runat="server" onclick="lnkClaimApplication_Click">Claim Application</asp:linkbutton></td>
                    </tr>
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td class="leftmMenu">&nbsp;<asp:linkbutton id="lnkClaimApproval" width="140px" runat="server" onclick="lnkClaimApproval_Click">Claim Approval</asp:linkbutton></td>
                    </tr>
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td class="leftmMenu">&nbsp;<asp:linkbutton id="lnkReport" width="140px" runat="server" onclick="lnkReport_Click">Report</asp:linkbutton></td>
                    </tr>
                </table>

            </td>
            <td style="width: 30px"></td>
            <td style="width: 1px; background-color: #f1f1f1"></td>
            <td style="width: 30px"></td>
            <td valign="top">









                <table cellpadding="0px" cellspacing="0px">


                    <tr>
                        <td style="height: 10px">

                            <asp:scriptmanager id="ScriptManager1" runat="server">
                            </asp:scriptmanager>

                        </td>
                    </tr>
            </td>
        </tr>
        <tr>
            <td>
                <asp:panel id="pnlTitleList" runat="server" visible="true">
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnAddNew" OnClick="btnAddNew_Click" runat="server" Width="120px" Text="Add New" />
                                <div style="overflow:scroll">
                                <asp:GridView ID="grdTitleList" DataKeyNames="ID" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="2px" CellPadding="3" Width="1000px" GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl. No." ItemStyle-CssClass="column" HeaderStyle-CssClass="column">

                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle Width="2%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ApplicationNo" ItemStyle-Width="20px" HeaderText="Application No" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                        <asp:BoundField DataField="S_Title" HeaderText="Title" ItemStyle-Width="180px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                        <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-Width="10px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                        <asp:BoundField DataField="AcademicYear" HeaderText="Academic Year" ItemStyle-Width="80px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                        <asp:BoundField DataField="Principal_Status" HeaderText="Principal Status" ItemStyle-Width="150px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                        <asp:BoundField DataField="S_Status" HeaderText="Second Status" ItemStyle-Width="120px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                        <asp:BoundField DataField="T_Status" HeaderText="Third Status" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                        <asp:BoundField DataField="VC_Status" HeaderText="VC Status" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />

                                        <asp:TemplateField HeaderText="Annexure" ItemStyle-CssClass="column" HeaderStyle-CssClass="column">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkAnnexure" Text="Annexure" CommandArgument='<%# Eval("ID") %>' Visible='<%# Eval("Ann_Attachment").ToString() != "0" %>' OnClick="lnkAnnexure_Click1" runat="server"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Minutes Doc" ItemStyle-CssClass="column" HeaderStyle-CssClass="column">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkMinutes" Text="Minutes Doc" CommandArgument='<%# Eval("ID") %>' Visible='<%# Eval("Minute_Attachment").ToString() != "0" %>' OnClick="lnkMinutes_Click" runat="server"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Grant Letter" ItemStyle-CssClass="column" HeaderStyle-CssClass="column">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkGrandLetter" Text="Grant Letter" CommandArgument='<%# Eval("ID") %>' Visible='<%# Eval("SM_G_Letter_Attachment").ToString() != "0" %>' OnClick="lnkGrandLetter_Click" runat="server"></asp:LinkButton>
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
                                </div>

                            </td>
                        </tr>


                    </table>


                </asp:panel>

                <asp:panel id="pnlTitleApplication" visible="false" runat="server">
                    <%--  <ContentTemplate>--%>


                    <table>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server"
                                    Text="Application Form" Font-Size="15pt" ForeColor="#093A62"
                                    Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                                <asp:HiddenField ID="hdfApplicationNo" runat="server" />
                            </td>
                            <td style="text-align: right; width: 600px">
                                <asp:LinkButton ID="btnbackTitle" runat="server" Width="140px" Text="Back to List" OnClick="btnbackTitle_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="height: 1px; background-color: #f1f1f1"></td>
                        </tr>
                    </table>

                    <div class="col-sm-3 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp Applicant Name</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ValidationGroup="R1" Display="Dynamic" ControlToValidate="txtApplicant" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtApplicant" runat="server" CssClass="form-control" Width="200px" Enabled="false"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp Designation</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ValidationGroup="R1" Display="Dynamic" ControlToValidate="txtDesignation" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" Width="200px" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp Name of College</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ValidationGroup="R1" Display="Dynamic" ControlToValidate="txtCollege" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtCollege" runat="server" CssClass="form-control" Width="200px" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp  Department </label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ValidationGroup="R1" Display="Dynamic" ControlToValidate="txtDep" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtDep" runat="server" CssClass="form-control" Width="200px" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>


                     <div class="col-sm-3 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label" style="font-size: small;width: 200px">&nbsp&nbsp&nbsp  Title </label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="R1" Display="Dynamic" ControlToValidate="txtTitle" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtTitle" runat="server" TextMode="MultiLine" CssClass="form-control" Width="200px" ></asp:TextBox>
                            </div>
                        </div>
                    </div>




                    <div class="col-sm-3 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp  Amount   </label>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="R1" Display="Dynamic" ControlToValidate="txtAmount" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            <div class="col-sm-8">

                                <asp:TextBox ID="txtAmount" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control" Width="200px">
                                </asp:TextBox>

                            </div>
                        </div>
                    </div>





                    <div class="col-sm-16 p-0" style="text-align: right">

                        <div class="form-group clearfix">
                            <asp:LinkButton ID="lnkAnnexure" Text="Download Seed Money Proposal Form - Annexure I Format" OnClick="lnkAnnexure_Click" Font-Italic="true" ForeColor="Green" runat="server" class="col-form-label" Style="font-size: small; width: 450px"> </asp:LinkButton>

                            <div class="col-sm-3">
                            </div>



                        </div>




                    </div>

                    <%-- </ContentTemplate>--%>

                    <div id="divUploadDoc" runat="server">
                        <div class="col-sm-12 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font: bold; color: green; font-size: medium">&nbsp&nbsp&nbsp   DOCUMENT SUBMITTED FOR CONSIDERATION :-</label>


                            </div>
                        </div>
                        <div class="col-sm-3 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font: bold; font-size: small">&nbsp&nbsp&nbsp  Attachments Type:</label>

                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlAttachtype" runat="server" Font-Bold="true" Height="32px" Font-Size="small" Width="220px" AutoPostBack="true">
                                        <asp:ListItem Text="Seed Money Proposal Form - Annexure I Format" Value="1"></asp:ListItem>


                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="fe" runat="server">
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnSubmitTitleApplication" />
                            </Triggers>
                            <ContentTemplate>
                                <div class="col-sm-3 p-0">
                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font: bold; font-size: small">&nbsp&nbsp&nbsp  Attachments :</label>

                                        <div class="col-sm-8">

                                            <asp:FileUpload ID="FileUpload1" runat="server" Font-Bold="true" Height="32px" Font-Size="small" Width="250px"></asp:FileUpload>


                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-6 p-0">
                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font: bold; visibility: hidden; font-size: small">&nbsp&nbsp&nbsp  Registration fee details of the Conference/Seminar/Workshop </label>

                                        <div class="col-sm-8">
                                            <asp:Button ID="btnSubmitTitleApplication" runat="server" autocomplete="off" Text="Submit" BackColor="Green" ValidationGroup="R1" ForeColor="White" Font-Bold="true" Height="32px" Font-Size="Large" class="btn btn-info" Width="120px" AutoPostBack="true" OnClick="btnSubmitTitleApplication_Click"></asp:Button>


                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>

                    <div runat="server" id="divAttachmentGrid">
                        <asp:GridView ID="grdDocument" runat="server" AutoGenerateColumns="false" BackColor="White" BorderColor="#E7E7FF" HeaderStyle-BackColor="#ff9900" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="866px" GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                            <AlternatingRowStyle BackColor="#F7F7F7" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Document name" HeaderText="Document name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Academic Year" HeaderText="Academic Year" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Content Type" HeaderText="Content Type" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="File Name" HeaderText="File Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="lnkDownload" Text="Download" CommandArgument='<%# Eval("ID") %>' runat="server" OnClick="DownloadInboxFile"></asp:LinkButton>
                                            </ContentTemplate>

                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="lnkDownload" />
                                                <%--  <asp:PostBackTrigger ControlID="drpAcademic" />--%>
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
                    </div>
                </asp:panel>
                <asp:panel id="pnlApplicationApproval" visible="false" runat="server">
                    <fieldset class="boxBody">
                        <asp:Label ID="Label2" runat="server"
                            Text="Seed Money Approval" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

                    </fieldset>
                    <div style="overflow:scroll">
                    <asp:GridView ID="grdApplicationApproval" DataKeyNames="ID" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="2px" CellPadding="3" Width="1000px" GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                        <Columns>
                            <asp:TemplateField HeaderText="Sl. No." ItemStyle-CssClass="column" HeaderStyle-CssClass="column">

                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle Width="2%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ApplicationNo" ItemStyle-Width="20px" HeaderText="Application No" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                            <asp:BoundField DataField="userid" HeaderText="Employee Code" ItemStyle-Width="10px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                            <asp:BoundField DataField="UserName" HeaderText="User Name" ItemStyle-Width="10px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                            <asp:BoundField DataField="Designation" HeaderText="Designation" ItemStyle-Width="80px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                            <asp:BoundField DataField="S_Title" HeaderText="Title" ItemStyle-Width="180px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                            <asp:BoundField DataField="NOC" HeaderText="College" ItemStyle-Width="150px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                            <asp:BoundField DataField="Dept" HeaderText="Department" ItemStyle-Width="120px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                            <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                            <asp:BoundField DataField="AcademicYear" HeaderText="Academic Year" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />

                            <asp:TemplateField HeaderText="Annexure" ItemStyle-CssClass="column" HeaderStyle-CssClass="column">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAnnexure" Text="Annexure" CommandArgument='<%# Eval("ID") %>' Visible='<%# Eval("Ann_Attachment").ToString() != "0" %>' OnClick="lnkAnnexure_Click1" runat="server"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Minutes Doc" ItemStyle-CssClass="column" HeaderStyle-CssClass="column">
                                <ItemTemplate>

                                    <asp:LinkButton ID="lnkMinutes" Text="Minutes Doc" CommandArgument='<%# Eval("ID") %>' Visible='<%# Eval("Minute_Attachment").ToString() != "0" %>' OnClick="lnkMinutes_Click" runat="server"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Grant Letter" ItemStyle-CssClass="column" HeaderStyle-CssClass="column">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkGrandLetter" Text="Grant Letter" CommandArgument='<%# Eval("ID") %>' Visible='<%# Eval("SM_G_Letter_Attachment").ToString() != "0" %>' OnClick="lnkGrandLetter_Click" runat="server"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                                                       
                              <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="column" HeaderStyle-CssClass="column">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" Text='<%# Eval("Status") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Action" ItemStyle-CssClass="column" HeaderStyle-CssClass="column">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnApproveP" Height="30px" Width="70px" Text="Action" BackColor="Green" Visible='<%# Eval("Status").ToString() == "Pending" %>' ForeColor="White" CommandArgument='<%# Eval("ID") %>' OnClick="btnApproveP_Click" runat="server"></asp:LinkButton>
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
                    </div>
                </asp:panel>

                <asp:panel id="pnlClaimList" runat="server" visible="false">
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnClaimNew" OnClick="btnClaimNew_Click" runat="server" Width="120px" Text="Add New" />

                                <asp:GridView ID="grdClaim" DataKeyNames="ID" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="2px" CellPadding="3" Width="1000px" GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl. No." ItemStyle-CssClass="column" HeaderStyle-CssClass="column">

                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle Width="2%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ApplicationNo" ItemStyle-Width="100px" HeaderText="Application No" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                        <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-Width="10px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                        <asp:BoundField DataField="Remark" HeaderText="Remark" ItemStyle-Width="10px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                        <asp:BoundField DataField="AcademicYear" HeaderText="Academic Year" ItemStyle-Width="80px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                        <asp:BoundField DataField="Principal_Status" HeaderText="Principal Status" ItemStyle-Width="150px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                        <asp:BoundField DataField="S_Status" HeaderText="Second Status" ItemStyle-Width="120px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                        <asp:BoundField DataField="T_Status" HeaderText="Third Status" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                        <asp:BoundField DataField="VC_Status" HeaderText="VC Status" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />

                                        <asp:TemplateField HeaderText="Application Letter" ItemStyle-CssClass="column" HeaderStyle-CssClass="column">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkLetter" Text="Application Letter" CommandArgument='<%# Eval("ID") %>' Visible='<%# Eval("Letter_Attachment").ToString() != "0" %>' OnClick="lnkLetter_Click" runat="server"></asp:LinkButton>
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


                            </td>
                        </tr>


                    </table>


                </asp:panel>

                <asp:panel id="pnlClaimApplication" visible="false" runat="server">

                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnSubmitApplication" />
                        </Triggers>
                        <ContentTemplate>

                            <table>
                                <tr>
                                    <td style="height: 10px"></td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server"
                                            Text="Claim Application Form" Font-Size="15pt" ForeColor="#093A62"
                                            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                    </td>
                                    <td style="text-align: right; width: 600px">
                                        <asp:LinkButton ID="lnkClaimBack" runat="server" Width="140px" Text="Back to List" OnClick="lnkClaimBack_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td style="height: 1px; background-color: #f1f1f1"></td>
                                </tr>
                            </table>

                            <div class="col-sm-3 p-0">
                                <div class="form-group clearfix">
                                    <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp Applicant No.</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="drpApplicationNo" runat="server" CssClass="form-control" Width="200px"></asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3 p-0">
                                <div class="form-group clearfix">
                                    <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Amount</label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="R1" Display="Dynamic" ControlToValidate="txtAmountC" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtAmountC" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" Width="200px"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6 p-0">
                                <div class="form-group clearfix">
                                    <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Remark</label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="R1" Display="Dynamic" ControlToValidate="txtRemarkC" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtRemarkC" runat="server" CssClass="form-control" TextMode="MultiLine" Width="400px"></asp:TextBox>
                                    </div>
                                </div>
                            </div>



                            <div class="col-sm-3 p-0">
                                <div class="form-group clearfix">
                                    <label for="inputEmail3" class="col-form-label" style="font: bold; font-size: small; width: 200px">&nbsp&nbsp&nbsp  Attachments Type:</label>

                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="drpApplicatioLetter" runat="server" Font-Bold="true" Height="32px" Font-Size="small" Width="220px" AutoPostBack="true">
                                            <asp:ListItem Text="Application Letter" Value="1"></asp:ListItem>


                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-3 p-0">
                                <div class="form-group clearfix">
                                    <label for="inputEmail3" class="col-form-label" style="font: bold; font-size: small; width: 200px">&nbsp&nbsp&nbsp  Attachments :</label>

                                    <div class="col-sm-8">

                                        <asp:FileUpload ID="flApplicationLetter" runat="server" Font-Bold="true" Height="32px" Font-Size="small" Width="250px"></asp:FileUpload>


                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-6 p-0">
                                <div class="form-group clearfix">
                                    <label for="inputEmail3" class="col-form-label" style="font: bold; visibility: hidden; font-size: small">&nbsp&nbsp&nbsp  Registration fee details of the Conference/Seminar/Workshop </label>

                                    <div class="col-sm-8">
                                        <asp:Button ID="btnSubmitApplication" runat="server" autocomplete="off" Text="Submit" BackColor="Green" ValidationGroup="R1" ForeColor="White" Font-Bold="true" Height="32px" Font-Size="Large" class="btn btn-info" Width="120px" AutoPostBack="true" OnClick="btnSubmitApplication_Click"></asp:Button>


                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:panel>


                <asp:panel id="pnlClaimApproval" visible="false" runat="server">
                    <fieldset class="boxBody">
                        <asp:Label ID="Label4" runat="server"
                            Text="Seed Money Claim Approval" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

                    </fieldset>
                    <asp:GridView ID="grdClaimApproval" DataKeyNames="ID" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="2px" CellPadding="3" Width="1000px" GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                        <Columns>
                            <asp:TemplateField HeaderText="Sl. No." ItemStyle-CssClass="column" HeaderStyle-CssClass="column">

                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle Width="2%" />
                            </asp:TemplateField>
                             <asp:BoundField DataField="ClaimApplicationNo" HeaderText="Grant Application No" ItemStyle-Width="200px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                            <asp:BoundField DataField="ApplicationNo" ItemStyle-Width="150px" HeaderText="Application No" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                            <asp:BoundField DataField="userid" HeaderText="Employee Code" ItemStyle-Width="150px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                            <asp:BoundField DataField="UserName" HeaderText="User Name" ItemStyle-Width="200px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                            
                            <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                            <asp:BoundField DataField="AcademicYear" HeaderText="Academic Year" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />

                            <asp:TemplateField HeaderText="Application Letter" ItemStyle-CssClass="column" HeaderStyle-CssClass="column">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAppLetter" Text="Application Letter" CommandArgument='<%# Eval("ID") %>' Visible='<%# Eval("Letter_Attachment").ToString() != "0" %>' OnClick="lnkAppLetter_Click" runat="server"></asp:LinkButton>

                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                            <asp:TemplateField HeaderText="Action" ItemStyle-CssClass="column" HeaderStyle-CssClass="column">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnApproveClaim" Height="30px" Width="70px" Text="Action" Visible='<%# Eval("Status").ToString() == "Pending" %>' BackColor="Green" ForeColor="White" CommandArgument='<%# Eval("ID") %>' OnClick="btnApproveClaim_Click" runat="server"></asp:LinkButton>
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

                </asp:panel>

                <asp:Panel ID="pnlRepport" runat="server" Visible="false">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="height: 10px"></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="Label5" runat="server"
                                                Text="Research Incentive Report" Font-Size="15pt" ForeColor="#093A62"
                                                Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px"></td>
                                    </tr>
                                    <tr>
                                        <td>


                                            <rsweb:reportviewer id="ReportViewer1" runat="server" width="100%" hyperlinktarget="_blank"></rsweb:reportviewer>

                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>





            </td>
        </tr>
    </table>
    <div id="confirmModal6" class="modal fade confirm-modal" role="dialog">

        <div class="modal-dialog modalPopup" style="width: 750px; height: 300px">
            <div style="text-align: right; padding-bottom: -40px">
                <asp:button id="Button1" runat="server" text="X" onclientclick="HidePopup6();" font-size="Larger" />
            </div>
            <div class="clearfix" style="margin-bottom: 10px; margin-top: 10px; margin-left: 20px">

                <asp:updatepanel id="UpdatePanel3" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnApprove" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="col-sm-8 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp Upload Mitues</label>

                                <div class="col-sm-8">
                                    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ValidationGroup="g8" ErrorMessage="**" ControlToValidate="flUploadMinute">
                                    </asp:RequiredFieldValidator>
                                    <asp:FileUpload ID="flUploadMinute" runat="server" CssClass="form-control" ValidationGroup="g8" Width="452px"></asp:FileUpload>

                                </div>
                            </div>
                        </div>

                        <div class="col-sm-8 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp Remarks</label>

                                <div class="col-sm-8">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ValidationGroup="g8" Display="Dynamic" ControlToValidate="txtRemark" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" ValidationGroup="g8" TextMode="MultiLine" Width="452px"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-2 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font: bold; visibility: hidden; font-size: small">&nbsp&nbsp&nbsp  Registration fee details  </label>

                                <div class="col-sm-8">

                                    <asp:Button ID="btnApprove" runat="server" Text="Recommend" Font-Bold="true" ValidationGroup="g8" AutoPostBack="true" OnClick="btnApprove_Click" Height="32px" Font-Size="small" class="btn btn-info" Width="120px"></asp:Button>
                                    <asp:Button ID="btnRejectPop" runat="server" Text="Reject" Font-Bold="true" ValidationGroup="g8" AutoPostBack="true" OnClick="btnRejectPop_Click" Height="32px" Font-Size="small" class="btn btn-info" Width="120px"></asp:Button>
                                </div>
                            </div>
                        </div>

                    </ContentTemplate>

                </asp:updatepanel>
            </div>
        </div>
    </div>


    <div id="confirmModal7" class="modal fade confirm-modal" role="dialog">

        <div class="modal-dialog modalPopup" style="width: 750px; height: 300px">
            <div style="text-align: right; padding-bottom: -40px">
                <asp:button id="Button2" runat="server" text="X" onclientclick="HidePopup7();" font-size="Larger" />
            </div>
            <div class="clearfix" style="margin-bottom: 10px; margin-top: 10px; margin-left: 20px">

                <asp:updatepanel id="UpdatePanel4" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSApproval" />
                        <asp:PostBackTrigger ControlID="btnSReject" />
                    </Triggers>
                    <ContentTemplate>


                        <div class="col-sm-8 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp Remarks</label>

                                <div class="col-sm-8">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="V1" Display="Dynamic" ControlToValidate="TextBox1" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" ValidationGroup="V1" TextMode="MultiLine" Width="452px"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-2 p-0">
                            <div class="form-group clearfix">

                                <div class="col-sm-8">

                                    <asp:Button ID="btnSApproval" runat="server" Text="Approve" Font-Bold="true" ValidationGroup="V1" AutoPostBack="true" OnClick="btnSApproval_Click" Height="32px" Font-Size="small" class="btn btn-info" Width="120px"></asp:Button>
                                    <asp:Button ID="btnSReject" runat="server" Text="Reject" Font-Bold="true" ValidationGroup="V1" AutoPostBack="true" OnClick="btnSReject_Click" Height="32px" Font-Size="small" class="btn btn-info" Width="120px"></asp:Button>
                                </div>
                            </div>
                        </div>

                    </ContentTemplate>

                </asp:updatepanel>
            </div>
        </div>
    </div>


    <div id="confirmModal8" class="modal fade confirm-modal" role="dialog">

        <div class="modal-dialog modalPopup" style="width: 750px; height: 300px">
            <div style="text-align: right; padding-bottom: -40px">
                <asp:button id="Button3" runat="server" text="X" onclientclick="HidePopup8();" font-size="Larger" />
            </div>
            <div class="clearfix" style="margin-bottom: 10px; margin-top: 10px; margin-left: 20px">

                <asp:updatepanel id="UpdatePanel5" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="Button4" />
                        <asp:PostBackTrigger ControlID="Button5" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="col-sm-8 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp Upload Grant Letter</label>

                                <div class="col-sm-8">
                                    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ValidationGroup="V2" ErrorMessage="**" ControlToValidate="FileUpload2">
                                    </asp:RequiredFieldValidator>
                                    <asp:FileUpload ID="FileUpload2" runat="server" CssClass="form-control" ValidationGroup="V2" Width="452px"></asp:FileUpload>

                                </div>
                            </div>
                        </div>

                        <div class="col-sm-8 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp Remarks</label>

                                <div class="col-sm-8">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="V3" Display="Dynamic" ControlToValidate="TextBox2" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" ValidationGroup="V3" TextMode="MultiLine" Width="452px"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-2 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font: bold; visibility: hidden; font-size: small">&nbsp&nbsp&nbsp  Registration fee details  </label>

                                <div class="col-sm-8">

                                    <asp:Button ID="Button4" runat="server" Text="Recommend" Font-Bold="true" ValidationGroup="V2" AutoPostBack="true" OnClick="Button4_Click" Height="32px" Font-Size="small" class="btn btn-info" Width="120px"></asp:Button>
                                    <asp:Button ID="Button5" runat="server" Text="Reject" Font-Bold="true" ValidationGroup="V3" AutoPostBack="true" OnClick="Button5_Click" Height="32px" Font-Size="small" class="btn btn-info" Width="120px"></asp:Button>
                                </div>
                            </div>
                        </div>

                    </ContentTemplate>

                </asp:updatepanel>
            </div>
        </div>
    </div>





    <div id="confirmModal9" class="modal fade confirm-modal" role="dialog">

        <div class="modal-dialog modalPopup" style="width: 750px; height: 300px">
            <div style="text-align: right; padding-bottom: -40px">
                <asp:button id="Button6" runat="server" text="X" onclientclick="HidePopup9();" font-size="Larger" />
            </div>
            <div class="clearfix" style="margin-bottom: 10px; margin-top: 10px; margin-left: 20px">

                <asp:updatepanel id="UpdatePanel7" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnClaimApprove" />
                        <asp:PostBackTrigger ControlID="btnClaimReject" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="col-sm-8 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp Remarks</label>
                                <div class="col-sm-8">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="L1" Display="Dynamic" ControlToValidate="txtClaimRemark" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtClaimRemark" runat="server" CssClass="form-control" ValidationGroup="L1" TextMode="MultiLine" Width="452px"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-2 p-0">
                            <div class="form-group clearfix">
                                <div class="col-sm-8">
                                    <asp:Button ID="btnClaimApprove" runat="server" Text="Approve" Font-Bold="true" ValidationGroup="L1" AutoPostBack="true" OnClick="btnClaimApprove_Click" Height="32px" Font-Size="small" class="btn btn-info" Width="120px"></asp:Button>
                                    <asp:Button ID="btnClaimReject" runat="server" Text="Reject" Font-Bold="true" ValidationGroup="L1" AutoPostBack="true" OnClick="btnClaimReject_Click" Height="32px" Font-Size="small" class="btn btn-info" Width="120px"></asp:Button>
                                </div>
                            </div>
                        </div>

                    </ContentTemplate>

                </asp:updatepanel>
            </div>
        </div>
    </div>


</asp:Content>

