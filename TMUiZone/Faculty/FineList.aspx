<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="FineList.aspx.cs" Inherits="Faculty_FineList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    </script>
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:Panel ID="pnlApproval" runat="server" Visible="false" CssClass="leftBackground">

        <fieldset class="boxBody">
            <asp:Label ID="Label12" runat="server"
                Text="Employee Fine List:-" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

        </fieldset>
        <table>
            

       


            <tr>
                <td style="height: 30px"></td>
               <td style="width:150px;text-align:right;font:200">&nbsp &nbsp &nbsp &nbsp Final Posting : &nbsp &nbsp &nbsp &nbsp  </td> <td style="text-align:left;padding-right:20px"> <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" /></td>
                <td style="text-align: right; width: 20px">
                    <asp:Button ID="btnApprove" CssClass="btnLogin" Text="Paid" OnClick="btnApprove_Click" runat="server" />

                </td>
                <td style="text-align: right; width: 20px">
                    <asp:Button ID="btnReject" CssClass="btnLogin" Text="Unpaid" OnClick="btnReject_Click" runat="server" />
                </td>
                <td style="text-align: right; width: 20px">
                    <asp:Button ID="btnPost" CssClass="btnLogin" Text="Final Post" OnClick="btnPost_Click" Visible="false" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="20">
                    <asp:GridView ID="grdData" runat="server" DataKeyNames="ID" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="1200px" GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                        <AlternatingRowStyle BackColor="#F7F7F7" />
                        <Columns>

                            <asp:TemplateField HeaderText="Sr No" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Employee_ID" HeaderText="Employee No" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Employee_name" HeaderText="Employee Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:TemplateField HeaderText="Corrected Amount" ControlStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCAmount" runat="server" Text='<%#Bind("CAmount") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCAmountRemarks" runat="server" Text='<%#Bind("CRemarksAmount") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:BoundField DataField="FineDate" HeaderText="Fine Date" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                              <asp:BoundField DataField="createDate" HeaderText="Create Date" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
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


                    <asp:GridView ID="GridView1" runat="server" DataKeyNames="ID" Width="1200px" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" EmptyDataText="There are no data records to display." Visible="false">
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
                            <asp:BoundField DataField="Fine_Date" HeaderText="From Date" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                           
                            <asp:BoundField DataField="Reason" HeaderText="Reason" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:TemplateField HeaderText="Corrected Amount" ControlStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCAmount" runat="server" Text='<%#Bind("CAmount") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCAmountRemarks" runat="server" Text='<%#Bind("CRemarksAmount") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
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
    <asp:Panel ID="pnlmsg" runat="server" Visible="false" CssClass="leftBackground">

        <fieldset class="boxBody">
            <asp:Label ID="Label11" runat="server"
                Text="You are not Authorized for this page." Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

        </fieldset>
    </asp:Panel>






</asp:Content>

