<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="Resignationformlist.aspx.cs" Inherits="Faculty_Resignationformlist" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<script runat="server">

    protected void ddlemployeepost_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnview_Click(object sender, EventArgs e)
    {

    }
</script>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        #confirmModalB .modal-dialog.modalPopup {
            width: 95%;
        }

        table thead tr th:first-child, .table > tbody > tr > th:first-child {
            border-left: 1px solid #60594f;
            padding: 5px 8px;
        }
    </style>
    <script type="text/javascript">
        function PrintDiv() {
            var divToPrint = document.getElementById('printarea');
            var popupWin = window.open('', '_blank', 'width=300,height=400,location=no,left=200px');
            popupWin.document.open();
            popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
            popupWin.document.close();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ty" runat="server"></asp:ScriptManager>


    <br />
    <fieldset>
        <div class="text-left" style="padding-left: 150px">

            <asp:TextBox ID="txtEmployee" runat="server" placeholder="Employee Name" />&nbsp&nbsp&nbsp&nbsp
            <asp:Button ID="btnSearch" runat="server" Text="Search" ForeColor="White" CssClass="btn" BackColor="#ff9900" OnClick="btnSearch_Click" />

        </div>

        <div class="text-right" style="padding-left: 150px">

            <asp:Button ID="BtnSubmit" runat="server" Text="Approved" ForeColor="White" CssClass="btn" OnClick="BtnSubmit_Click" BackColor="#ff9900" />
            <asp:Button ID="BtnRejected" runat="server" Text="Rejected" ForeColor="White" CssClass="btn" BackColor="#ff9900" OnClick="BtnRejected_Click" />

        </div>
    </fieldset>
    <fieldset class="boxBodyInner">
        <br />
        <asp:GridView ID="grdresignationlist" runat="server" DataKeyNames="Employee Code" OnPageIndexChanging="grdresignationlist_PageIndexChanging" AlternatingRowStyle-CssClass="danger" PageSize="50"
            AllowPaging="true" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" Visible="true">
            <PagerSettings Mode="NumericFirstLast" />
            <PagerStyle CssClass="csspager" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <%# Container.DataItemIndex +1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Employee Code" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblemployeecode" runat="server" Text='<%# Bind("[Employee Code]") %>'></asp:Label>
                        <asp:HiddenField ID="Hfemployeecode" Value='<%# Eval("[Employee Code]") %>' runat="server" />
                        <asp:HiddenField ID="Hfhodname" Value='<%# Eval("[Employee Code]") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="View" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkbutton" runat="server" OnClick="lnkbutton_Click">View</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Employee Name" ItemStyle-Width="4%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                    <ItemTemplate>
                        <asp:Label ID="lblemployeename" runat="server" Text='<%# Eval("Employee Name") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Designation" ItemStyle-Width="3%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                    <ItemTemplate>
                        <asp:Label ID="lbldesignation" runat="server" Text='<%# Eval("Designation") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date Of Joining" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lbldateofjoining" runat="server" Text='<%#Eval("Date Of Joining") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Resignation Date" ItemStyle-Width="2%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblresignationdate" runat="server" Text='<%# Eval("FormattedDate") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Notice Period" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblRemark" runat="server" Text='<%#Eval("Total Duration") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Hod_Status" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblhodstatus" runat="server" Text='<%#Eval("Hod_Status") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="HR_Status" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblhrstatus" runat="server" Text='<%#Eval("Hr_Status") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Registrar_Status" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblregistrarstatus" runat="server" Text='<%#Eval("Registrar_Approval") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="VC_Status" ItemStyle-Width="5%"  HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblvcstatus" runat="server" Text='<%#Eval("VC_Approval") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" HeaderText="Select" ItemStyle-CssClass="text-center">

                    <ItemTemplate>
                        <asp:CheckBox ID="Chkemployee" Enabled='<%# Eval("txtMarksEnableDesable").ToString().Equals("true") %>' runat="server" AutoPostBack="true" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>No Record To Display</EmptyDataTemplate>
        </asp:GridView>
        <asp:Panel ID="pnlGridViewDetails" CssClass="modalPopup" Width="65%" runat="server" Style="display: none;">

            <div class="header">
                <b>

                    <asp:Label ID="lblNotification" runat="server" Text="Resignation Form"></asp:Label></b>
                <div class="close">
                    <asp:Button ID="btnclose" OnClick="btnclose_Click" runat="server" Text="X" />
                </div>
            </div>

            <div id="Div1" runat="server" style="max-height: 900px; overflow: auto;">

                <div class="body">
                    <div style="width: 100%">
                        <center>
                            <table cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td style="height: 10px"></td>
                                </tr>


                                <tr>


                                    <td>&nbsp;&nbsp;&nbsp; 
                 <div style="width: 99%">
                     <div style="text-align: right; width: 100%; margin-bottom: -30px;">
                         <asp:Button ID="BtnPrint" OnClientClick="PrintDiv();" runat="server" Width="10%" Style="margin-top: 5px;" Text="Print" Font-Bold="true" BorderColor="WhiteSmoke" />
                     </div>

                     <div id="printarea">
                         <div style="text-align: center; width: 100%" id="logoDiv">
                             <table style="width: 80%; margin-left: 10%;">
                                 <tr>
                                     <td style="width: 200px;" align="left">
                                         <img src="~/images/rightlogo.png" id="Image1" runat="server" width="100" height="102" visible="true" />
                                     </td>
                                     <td style="width: 80%; vertical-align: middle" align="left">
                                         <asp:Label ID="LblTitle" runat="server" Text="Teerthanker Mahaveer University, Moradabad" Style="font-size: 25px;"></asp:Label>
                                     </td>
                                 </tr>
                             </table>
                         </div>





                         <table>
                             <tr>
                                 <td style="height: 30px"></td>
                             </tr>
                             <tr>
                                 <td>
                                     <table cellpadding="0px" cellspacing="0px" style="padding-top: 100px">
                                         <tr>
                                             <td style="width: 120px"></td>
                                             <td>
                                                 <table cellpadding="0px" cellspacing="0px">
                                                     <tr>
                                                         <td><strong>Personal Details </strong></td>
                                                     </tr>
                                                     <tr>
                                                         <td style="height: 10px"></td>
                                                     </tr>
                                                     <tr>
                                                         <td style="background-color: #808080; height: 1px"></td>
                                                     </tr>
                                                     <tr>
                                                         <td style="height: 10px"></td>
                                                     </tr>
                                                     <tr>
                                                         <td>
                                                             <table cellpadding="0px" cellspacing="0px">
                                                                 <tr>
                                                                     <td>Employee Name : </td>
                                                                     <td style="width: 107px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="txtEmployeeName" runat="server" Enabled="False" Width="400px"></asp:TextBox>
                                                                     </td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>Employee Code : </td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="txtEmployeeCode" runat="server" Enabled="False" Width="195px"></asp:TextBox>
                                                                     </td>
                                                                 </tr>
                                                             </table>
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td style="background-color: #808080; height: 1px"></td>
                                                     </tr>
                                                     <tr>
                                                         <td>
                                                             <table cellpadding="0px" cellspacing="0px">
                                                                 <tr>
                                                                     <td>Designation :</td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="lbldesignation" runat="server" Enabled="False" Width="720px"></asp:TextBox>
                                                                     </td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td colspan="3" style="background-color: #808080; height: 1px"></td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td>Institution :</td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="lblInstitution" runat="server" Enabled="false" Width="720px"></asp:TextBox>
                                                                     </td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td colspan="3" style="background-color: #808080; height: 1px"></td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td>Name of Current Director / HOD :</td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="lblnameofHOD" runat="server" Enabled="false" Width="720px"></asp:TextBox>
                                                                     </td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td colspan="3" style="background-color: #808080; height: 1px"></td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td>Date of Joining :</td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="lblDateofJoining" runat="server" Enabled="false" Width="720px"></asp:TextBox>
                                                                     </td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td colspan="3" style="background-color: #808080; height: 1px"></td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td>Date of Applying for Resignation :</td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="txtapplyingresignation" runat="server" autocomplete="off" AutoPostBack="True" BorderColor="Black" Enabled="False" oncontextmenu="return false" oncopy="return false" oncut="return false" onkeydown="return false;" onpaste="return false" Width="200px"></asp:TextBox>
                                                                         <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtapplyingresignation">
                                                                         </asp:CalendarExtender>
                                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtapplyingresignation" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                                                     </td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td colspan="3" style="background-color: #808080; height: 1px"></td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td>Notice Period :</td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="txtnoticeperiod" runat="server" Enabled="false" Width="120px"></asp:TextBox>
                                                                     </td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td colspan="3" style="background-color: #808080; height: 1px"></td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td>Employee Type :</td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="txtemployeetype" runat="server" Width="120px" Enabled="false"></asp:TextBox></td>

                                                                 </tr>
                                                                 <tr>
                                                                     <td colspan="3" style="background-color: #808080; height: 1px"></td>
                                                                 </tr>
                                                             </table>
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td><strong>Reasons For Job Switch (All applicable reasons with remarks can be mentioned) </strong></td>
                                                     </tr>
                                                     <tr>
                                                         <td style="background-color: #808080; height: 1px"></td>
                                                     </tr>
                                                     <tr>
                                                         <td>
                                                             <table cellpadding="0px" cellspacing="0px">
                                                                 <tr>
                                                                     <td>Better Profile :</td>
                                                                     <td style="width: 1px"></td>
                                                                     <td style="width: 1px; background-color: #808080"></td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="txtbetterprofile" runat="server" Enabled="False" MaxLength="200" Width="800px"></asp:TextBox>
                                                                     </td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td colspan="5" style="background-color: #808080; height: 1px"></td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td>Better Emoluments :</td>
                                                                     <td style="width: 10px"></td>
                                                                     <td style="width: 1px; background-color: #808080"></td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="txtBetterEmoluments" runat="server" Enabled="False" MaxLength="200" Width="800px"></asp:TextBox>
                                                                     </td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td colspan="5" style="background-color: #808080; height: 1px"></td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td>Personal Reason :</td>
                                                                     <td style="width: 10px"></td>
                                                                     <td style="width: 1px; background-color: #808080"></td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="txtPersonalReason" runat="server" Enabled="False" MaxLength="200" Width="800px"></asp:TextBox>
                                                                     </td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td colspan="5" style="background-color: #808080; height: 1px"></td>
                                                                 </tr>
                                                             </table>
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td>
                                                             <table cellpadding="0px" cellspacing="0px">
                                                                 <tr>
                                                                     <td><strong>Any other reason :</strong> </td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="txtanyotherreason" runat="server" Enabled="False" MaxLength="250" Width="823px"></asp:TextBox>
                                                                     </td>
                                                                 </tr>
                                                             </table>
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td style="height: 1px; background-color: #808080"></td>
                                                     </tr>
                                                     <tr>
                                                         <td>
                                                             <table cellpadding="0px" cellspacing="0px">
                                                                 <tr>
                                                                     <td>Name of Organization Joining : </td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="txtNameofOrgJoining" runat="server" Enabled="False" MaxLength="150" Width="748px"></asp:TextBox>
                                                                     </td>
                                                                 </tr>
                                                             </table>
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td style="height: 1px; background-color: #808080"></td>
                                                     </tr>
                                                     <tr>
                                                         <td>What triggered you to look for a change : </td>
                                                     </tr>
                                                     <tr>
                                                         <td style="height: 1px; background-color: #808080"></td>
                                                     </tr>
                                                     <tr>
                                                         <td>
                                                             <asp:TextBox ID="txttriggerdlookforchange" runat="server" Enabled="False" MaxLength="250" Width="948px"></asp:TextBox>
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td style="height: 1px; background-color: #808080"></td>
                                                     </tr>
                                                     <tr>
                                                         <td>Good/Enjoyable experiences with TMU: </td>
                                                     </tr>
                                                     <tr>
                                                         <td style="height: 1px; background-color: #808080"></td>
                                                     </tr>
                                                     <tr>
                                                         <td>
                                                             <asp:TextBox ID="txtGoodwithTMU" runat="server" Enabled="False" MaxLength="250" Width="948px"></asp:TextBox>
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td style="height: 1px; background-color: #808080"></td>
                                                     </tr>
                                                     <tr>
                                                         <td>Difficult/upsetting experiences with TMU: </td>
                                                     </tr>
                                                     <tr>
                                                         <td style="height: 1px; background-color: #808080"></td>
                                                     </tr>
                                                     <tr>
                                                         <td>
                                                             <asp:TextBox ID="txtDifficultwithtmu" runat="server" Enabled="False" MaxLength="250" Width="948px"></asp:TextBox>
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td style="height: 1px; background-color: #808080"></td>
                                                     </tr>
                                                     <tr>
                                                         <td><strong>Please complete Responses (Unsatisfactory; Satisfactory; Good; Excellent) </strong></td>
                                                     </tr>
                                                     <tr>
                                                         <td style="height: 1px; background-color: #808080"></td>
                                                     </tr>
                                                     <tr>
                                                         <td>
                                                             <table cellpadding="0px" cellspacing="0px">
                                                                 <tr>
                                                                     <td><strong>Questions </strong></td>
                                                                     <td style="width: 10px"></td>
                                                                     <td style="background-color: #808080; width: 1px"></td>
                                                                     <td style="width: 10px"></td>
                                                                     <td><strong>Response </strong></td>
                                                                     <td style="width: 10px"></td>
                                                                     <td style="background-color: #808080; width: 1px"></td>
                                                                     <td style="width: 10px"></td>
                                                                     <td><strong>Remarks</strong></td>
                                                                     <td style="width: 10px"></td>
                                                                     <td style="background-color: #808080; width: 1px"></td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td colspan="11" style="background-color: #808080; height: 1px"></td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td>Overall rating of Teerthankar Mahaveer University as an organization </td>
                                                                     <td style="width: 10px"></td>
                                                                     <td style="background-color: #808080; width: 1px"></td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="txtovalallratingResponse" runat="server" Enabled="False" MaxLength="50"></asp:TextBox>
                                                                     </td>
                                                                     <td style="width: 10px"></td>
                                                                     <td style="background-color: #808080; width: 1px"></td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="txtovalallratingRemarks" runat="server" Enabled="False" MaxLength="250" Width="300px"></asp:TextBox>
                                                                     </td>
                                                                     <td style="width: 10px"></td>
                                                                     <td style="background-color: #808080; width: 1px"></td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td colspan="11" style="background-color: #808080; height: 1px"></td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td>The performance measurement and the feedback system </td>
                                                                     <td style="width: 10px"></td>
                                                                     <td style="background-color: #808080; width: 1px"></td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="txtperformancemeasurementResponse" runat="server" Enabled="False" MaxLength="50"></asp:TextBox>
                                                                     </td>
                                                                     <td style="width: 10px"></td>
                                                                     <td style="background-color: #808080; width: 1px"></td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="txtperformancemeasurementRemarks" runat="server" Enabled="False" MaxLength="250" Width="300px"></asp:TextBox>
                                                                     </td>
                                                                     <td style="width: 10px"></td>
                                                                     <td style="background-color: #808080; width: 1px"></td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td colspan="11" style="background-color: #808080; height: 1px"></td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td>The communication within the organization </td>
                                                                     <td style="width: 10px"></td>
                                                                     <td style="background-color: #808080; width: 1px"></td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="txtCommunicationResponse" runat="server" Enabled="False" MaxLength="50"></asp:TextBox>
                                                                     </td>
                                                                     <td style="width: 10px"></td>
                                                                     <td style="background-color: #808080; width: 1px"></td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="txtCommunicationRemarks" runat="server" Enabled="False" MaxLength="250" Width="300px"></asp:TextBox>
                                                                     </td>
                                                                     <td style="width: 10px"></td>
                                                                     <td style="background-color: #808080; width: 1px"></td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td colspan="11" style="background-color: #808080; height: 1px"></td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td>Recruitment and Induction procedures in TMU </td>
                                                                     <td style="width: 10px"></td>
                                                                     <td style="background-color: #808080; width: 1px"></td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="txtRecruitmentResponse" runat="server" Enabled="False" MaxLength="50"></asp:TextBox>
                                                                     </td>
                                                                     <td style="width: 10px"></td>
                                                                     <td style="background-color: #808080; width: 1px"></td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="txtRecruitmentRemarks" runat="server" Enabled="False" MaxLength="250" Width="300px"></asp:TextBox>
                                                                     </td>
                                                                     <td style="width: 10px"></td>
                                                                     <td style="background-color: #808080; width: 1px"></td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td colspan="11" style="background-color: #808080; height: 1px"></td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td>Willingness of superiors to listen and help in solving problems </td>
                                                                     <td style="width: 10px"></td>
                                                                     <td style="background-color: #808080; width: 1px"></td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="txtWillingnessResponse" runat="server" Enabled="False" MaxLength="50"></asp:TextBox>
                                                                     </td>
                                                                     <td style="width: 10px"></td>
                                                                     <td style="background-color: #808080; width: 1px"></td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="txtWillingnessRemarks" runat="server" Enabled="False" MaxLength="250" Width="300px"></asp:TextBox>
                                                                     </td>
                                                                     <td style="width: 10px"></td>
                                                                     <td style="background-color: #808080; width: 1px"></td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td colspan="11" style="background-color: #808080; height: 1px"></td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td>Recruitment and Induction procedures in TMU </td>
                                                                     <td style="width: 10px"></td>
                                                                     <td style="background-color: #808080; width: 1px"></td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="txtRecruitment_Proc_Response" runat="server" Enabled="False" MaxLength="50"></asp:TextBox>
                                                                     </td>
                                                                     <td style="width: 10px"></td>
                                                                     <td style="background-color: #808080; width: 1px"></td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="txtRecruitment_Proc_Remarks" runat="server" Enabled="False" MaxLength="250" Width="300px"></asp:TextBox>
                                                                     </td>
                                                                     <td style="width: 10px"></td>
                                                                     <td style="background-color: #808080; width: 1px"></td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td colspan="11" style="background-color: #808080; height: 1px"></td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td>The working environment </td>
                                                                     <td style="width: 10px"></td>
                                                                     <td style="background-color: #808080; width: 1px"></td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="txtWorkingEnviron_Response" runat="server" Enabled="False" MaxLength="50"></asp:TextBox>
                                                                     </td>
                                                                     <td style="width: 10px"></td>
                                                                     <td style="background-color: #808080; width: 1px"></td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="txtWorkingEnviron_Remarks" runat="server" Enabled="False" MaxLength="250" Width="300px"></asp:TextBox>
                                                                     </td>
                                                                     <td style="width: 10px"></td>
                                                                     <td style="background-color: #808080; width: 1px"></td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td colspan="11" style="background-color: #808080; height: 1px"></td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td>Growth opportunities </td>
                                                                     <td style="width: 10px"></td>
                                                                     <td style="background-color: #808080; width: 1px"></td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="txtgrowthOpportuniti_Response" runat="server" Enabled="False" MaxLength="50"></asp:TextBox>
                                                                     </td>
                                                                     <td style="width: 10px"></td>
                                                                     <td style="background-color: #808080; width: 1px"></td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="txtgrowthOpportuniti_Remarks" runat="server" Enabled="False" MaxLength="250" Width="300px"></asp:TextBox>
                                                                     </td>
                                                                     <td style="width: 10px"></td>
                                                                     <td style="background-color: #808080; width: 1px"></td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td colspan="11" style="background-color: #808080; height: 1px"></td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td>Effectiveness of Appraisal process </td>
                                                                     <td style="width: 10px"></td>
                                                                     <td style="background-color: #808080; width: 1px"></td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="txteffectiveness_Response" runat="server" Enabled="False" MaxLength="50"></asp:TextBox>
                                                                     </td>
                                                                     <td style="width: 10px"></td>
                                                                     <td style="background-color: #808080; width: 1px"></td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="txteffectiveness_Remarks" runat="server" Enabled="False" MaxLength="250" Width="300px"></asp:TextBox>
                                                                     </td>
                                                                     <td style="width: 10px"></td>
                                                                     <td style="background-color: #808080; width: 1px"></td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td colspan="11" style="background-color: #808080; height: 1px"></td>
                                                                 </tr>
                                                             </table>
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td>
                                                             <table cellpadding="0px" cellspacing="0px">
                                                                 <tr>
                                                                     <td><strong>Any other Comment :</strong> </td>
                                                                     <td style="width: 10px"></td>
                                                                     <td>
                                                                         <asp:TextBox ID="txtAnyOtherComment" runat="server" Enabled="False" MaxLength="250" Width="800px"></asp:TextBox>
                                                                     </td>
                                                                 </tr>
                                                             </table>
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td style="background-color: #808080; height: 1px"></td>
                                                     </tr>
                                                     <tr>
                                                         <td><strong>Contact Details </strong>
                                                             <br />
                                                             <br />
                                                             <br />
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td>
                                                             <table cellpadding="0px" cellspacing="0px">
                                                                 <tr>
                                                                     <td>
                                                                         <table cellpadding="0px" cellspacing="0px">
                                                                             <tr>
                                                                                 <td>Mobile No : </td>
                                                                                 <td style="width: 10px"></td>
                                                                                 <td>
                                                                                     <asp:TextBox ID="txtMobileNo" runat="server" Enabled="False" Width="300px"></asp:TextBox>
                                                                                 </td>
                                                                             </tr>
                                                                             <tr>
                                                                                 <td>Official Email-id : </td>
                                                                                 <td style="width: 10px"></td>
                                                                                 <td>
                                                                                     <asp:TextBox ID="txtofficial" runat="server" Enabled="False" Width="300px"></asp:TextBox>
                                                                                 </td>
                                                                             </tr>
                                                                             <tr>
                                                                                 <td>Personal Email-id : </td>
                                                                                 <td style="width: 10px"></td>
                                                                                 <td>
                                                                                     <asp:TextBox ID="txtEmail" runat="server" Enabled="False" Width="300px"></asp:TextBox>
                                                                                 </td>
                                                                             </tr>
                                                                         </table>
                                                                     </td>
                                                                     <td style="width: 400px"></td>
                                                                     <td style="vertical-align: bottom"><strong>Signature</strong> </td>
                                                                 </tr>
                                                             </table>
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td style="height: 10px"></td>
                                                     </tr>
                                                     <tr>
                                                         <td style="background-color: #808080; height: 1px"></td>
                                                     </tr>
                                                     <tr>
                                                         <td style="height: 10px"></td>
                                                     </tr>
                                                     <tr>
                                                         <td style="height: 10px"></td>
                                                     </tr>
                                                     <tr>
                                                         <td style="background-color: #808080; height: 1px"></td>
                                                     </tr>
                                                     <tr>
                                                         <td style="height: 90px">Please fill Interview form and after completion, cilck here for submit&nbsp;&nbsp;&nbsp;
                                                                <br />
                                                             <asp:Label ID="lbltxt" runat="server" ForeColor="Red" Text="Note: No changes will  be accepted after Submission."></asp:Label>
                                                             &nbsp;<br />
                                                             <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="Note: No Leaves Shall be applicable After Resignation."></asp:Label>
                                                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                                     </tr>

                                                 </table>
                     </div>
                                    </td>
                                    <td style="width: 10px"></td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            </td>
                                </tr>
                            </table>
                        </center>

                    </div>

                    <triggers>
                        <asp:PostBackTrigger ControlID="BtnPrint" />
                    </triggers>

                </div>
                </center>
            </div>

            </div>
            </div>
            
        </asp:Panel>
        <asp:Button ID="btnDummy" runat="server" Style="display: none;" />
        <asp:ModalPopupExtender ID="GridViewDetails" runat="server" TargetControlID="btnDummy"
            PopupControlID="pnlGridViewDetails" BackgroundCssClass="modalBackground" />
        <br />
        <br />

    </fieldset>
    <br />
    <br />


</asp:Content>

