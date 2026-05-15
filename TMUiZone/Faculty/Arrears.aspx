<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="Arrears.aspx.cs" Inherits="Faculty_Arrears" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .modalPopup {
            background-color: #ffffdd;
            /*border-width: 3px;*/
            /*border-style: solid;*/
            /*border-color: Gray;*/
            padding: 3px;
            width: 30%;
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
    </style>
    <script type="text/javascript">

        function checkAll(objRef) {

            var GridView = objRef.parentNode.parentNode.parentNode;

            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {

                //Get the Cell To find out ColumnIndex

                var row = inputList[i].parentNode.parentNode;

                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                    if (objRef.checked) {

                        //If the header checkbox is checked

                        //check all checkboxes

                        //and highlight all rows

                        row.style.backgroundColor = "aqua";

                        inputList[i].checked = true;

                    }

                    else {

                        //If the header checkbox is checked

                        //uncheck all checkboxes

                        //and change rowcolor back to original

                        if (row.rowIndex % 2 == 0) {

                            //Alternating Row Color

                            row.style.backgroundColor = "#C2D69B";

                        }

                        else {

                            row.style.backgroundColor = "white";

                        }

                        inputList[i].checked = false;

                    }

                }

            }

        }

        function Confirm_Apply() {

            if (document.getElementById("ContentPlaceHolder1_txtfrom").value == "") {
                alert("Please Select Arrears Date.");
                return false;
            }
            if (document.getElementById("ContentPlaceHolder1_txtamount").value == "") {
                alert("Please Insert Amount.");
                return false;
            }

            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            var Value = '';

            Value = document.getElementById("ContentPlaceHolder1_txtfrom").value;
            if (confirm("Do you want to Apply Arrears for the date of  " + Value + " ")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
                
            }
            document.forms[0].appendChild(confirm_value);

            if (confirm_value.value == "No") {

                return false;
            }

        }
        function isNumberKey(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="Arrears Request Form" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>


    <fieldset class="boxBody">

        <table cellpadding="0px" cellspacing="0px">
            <tr>
                <td style="width: 10px"></td>
                <td style="width: 200px" valign="top">

                    <table cellpadding="0px" cellspacing="0px" class="leftbg1" style="width: 180px; height: 430px">
                        <tr>
                            <td style="width: 10px"></td>
                            <td>


                                <table cellpadding="0px" cellspacing="0px">
                                    <tr>
                                        <td style="height: 10px"></td>
                                    </tr>
                                    <tr>
                                        <td class="leftmMenu">&nbsp;<asp:LinkButton ID="lnkProfileview" runat="server" OnClick="lnkProfileview_Click">Arrears Application</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px"></td>
                                    </tr>
                                    <tr>
                                        <td class="leftmMenu">&nbsp;<asp:LinkButton ID="lnkRejectProfileDetail" runat="server" OnClick="lnkRejectProfileDetail_Click">Report</asp:LinkButton></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px"></td>
                                    </tr>
                                    <tr>
                                        <td class="leftmMenu">&nbsp;<asp:LinkButton ID="lnkApproval" runat="server" OnClick="lnkApproval_Click">Approval</asp:LinkButton></td>
                                    </tr>



                                    <tr>
                                        <td style="height: 10px">&nbsp;</td>
                                    </tr>


                                    <tr>
                                        <td style="visibility: hidden">

                                            <asp:Label ID="Label4" runat="server"
                                                Text=" Color Represent in Calender" Font-Size="10pt" ForeColor="#093A62" Font-Names="Open Sans"></asp:Label>

                                        </td>
                                    </tr>



                                    <tr>
                                        <td style="height: 5px">&nbsp;</td>
                                    </tr>







                                    <tr>
                                        <td style="height: 2px"></td>
                                    </tr>

                                    <tr>
                                        <td></td>
                                    </tr>




                                </table>
                                <div style="visibility: hidden; height: 1px">


                                    <asp:TextBox ID="txtNoOfLeavePriod" runat="server"></asp:TextBox>
                                </div>
                            </td>
                            <td style="width: 10px"></td>
                        </tr>
                    </table>




                </td>
                <td style="width: 30px"></td>
                <td valign="top">


                    <asp:Panel ID="pnlLeaveApplication" runat="server" CssClass="leftBackground">

                        <table cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td style="width: 10px"></td>
                                <td>

                                    <table cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td>
                                                <br />

                                                <table cellpadding="0px" cellspacing="0px">



                                                    <table cellpadding="0px" cellspacing="0px">

                                                        <tr>
                                                            <td colspan="15">
                                                                <table cellpadding="0px" cellspacing="0px">
                                                                    <caption>
                                                                        <br />
                                                                        <tr>
                                                                            <td>
                                                                                <label style="line-height: 25px">
                                                                                    Arrear Date :</label>
                                                                            </td>
                                                                            <td style="width: 10px"></td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtfrom" runat="server" Width="180px" onkeydown="return false;" AutoPostBack="true"></asp:TextBox>
                                                                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtfrom" Format="dd MMM yyyy">
                                                                                </cc1:CalendarExtender>
                                                                            </td>
                                                                            <td style="width: 50px"></td>
                                                                            <td>
                                                                                <label style="line-height: 25px">
                                                                                    Amount :</label>
                                                                            </td>
                                                                            <td style="width: 10px"></td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtamount" runat="server" onkeypress="return isNumberKey(event)" Width="180px"></asp:TextBox>
                                                                                <%-- <asp:TextBox ID="txtto" runat="server" Width="180px" Enabled="false" AutoPostBack="true" onkeydown="return false;" OnTextChanged="txtto_TextChanged"></asp:TextBox>
                                                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtto" Format="dd MMM yyyy">
                                                                                </cc1:CalendarExtender>--%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="11" style="height: 20px"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <label style="line-height: 25px">
                                                                                    Reason :</label>
                                                                            </td>
                                                                            <td style="width: 10px"></td>
                                                                            <td colspan="5">
                                                                                <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Width="490px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="11" style="height: 20px"></td>
                                                                        </tr>


                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <asp:Button ID="btnSubmit" runat="server" CssClass="btnLogin" OnClientClick="return Confirm_Apply()" OnClick="btnSubmit_Click" Text="Submit" />
                                                                            </td>
                                                                        </tr>
                                                                    </caption>
                                                                </table>
                                                    </table>
                                                </table>
                    </asp:Panel>

                    <asp:Panel ID="pnlApproval" runat="server" Visible="false" CssClass="leftBackground">

                        <fieldset class="boxBody">
                            <asp:Label ID="Label11" runat="server"
                                Text="Employee Arrears Approval" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

                        </fieldset>
                        <table>
                            <tr>
                                <td style="height: 30px"></td>
                                <td style="text-align: right; width: 20px">
                                    <asp:Button ID="btnApprove" CssClass="btnLogin" Text="Approve" OnClick="btnApprove_Click" runat="server" />

                                </td>
                                <td style="text-align: right; width: 20px">
                                    <asp:Button ID="btnReject" CssClass="btnLogin" Text="Reject" OnClick="btnReject_Click" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:GridView ID="grdData" runat="server" DataKeyNames="ID" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="866px" GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                                        <AlternatingRowStyle BackColor="#F7F7F7" />
                                        <Columns>

                                            <asp:TemplateField HeaderText="Sr No" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="UserId" HeaderText="Employee No" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:BoundField DataField="Employee_Name" HeaderText="Employee Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:BoundField DataField="From_Date" HeaderText="From Date" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:BoundField DataField="To_Date" HeaderText="To Date" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:BoundField DataField="Reason" HeaderText="Reason" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />

                                            <asp:BoundField DataField="Status1" HeaderText="Status" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />


                                            <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" runat="server" Text="All" onclick="checkAll(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Leave Create Date" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId" runat="server" Text='<%#Bind("ID") %>'></asp:Label>
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
                </td>
            </tr>
        </table>
    </asp:Panel>
        </table>
</asp:Content>

