<%@ Page Title="" Language="C#" MasterPageFile="~/Student/IndexMaster.master" AutoEventWireup="true" CodeFile="NAACForm.aspx.cs" Inherits="Student_NAACForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        th {
            text-align: center;
        }
        /*.rowstyle td
        {
            width:300px;
        }*/
    </style>

    <script type="text/javascript">
        function CheckBoxCheck(rb) {
            var gv = document.getElementById("<%=grddata.ClientID%>");
            var row = rb.parentNode.parentNode;
            var rbs = row.getElementsByTagName("input");
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "checkbox") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }
        }
        function CheckBoxValidation() {
            var valid = false;
            //Varibale to hold the checked checkbox count
            var chkselectcount = 0;
            //Get the gridview object
            var gridview = document.getElementById('<%= grddata.ClientID %>');
            //Loop thorugh items
            for (var i = 0; i < gridview.getElementsByTagName("input").length; i++) {
                //Get the object of input type
                var node = gridview.getElementsByTagName("input")[i];
                //check if object is of type checkbox and checked or not
                if (node != null && node.type == "checkbox" && node.checked) {
                    valid = true;
                    //Increase the count of check box
                    chkselectcount = chkselectcount + 1;
                }
            }

            //Checking the count is zero , this means no checkbox is selected
            if (chkselectcount == 0) {
                alert("Please select atleast one checkbox");
                return false;
            }
            else { return false; }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="mnupd">
        <ContentTemplate>--%>
    <table style="width: 100%; text-align: center">
        <tr>
            <td>
                <asp:Label ID="lblMsg" Font-Bold="true" Font-Size="Large" runat="server" Text="Survey is complete. " Visible="false"></asp:Label>
            </td>
        </tr>
    </table>
    <div style="text-align: center; width: 100%" id="logoDiv" runat="server">
        <table style="width: 80%; margin-left: 10%;">
            <tr>
                <td style="width: 200px;" align="left">
                    <img src="~/images/rightlogo.png" id="Image1" runat="server" width="100" height="102" visible="true" />
                </td>
                <td style="width: 80%; vertical-align: middle" align="center">
                    <asp:Label ID="LblTitle" runat="server" Text="Student Satisfaction Survey on Teaching Learning Process " Font-Bold="true" Style="font-size: 15px;"></asp:Label>
                </td>

            </tr>
            <tr style="width: 80%; margin-left: 10%;">
                <td style="width: 200px;" align="left"></td>
                <td style="width: 80%; vertical-align: middle" align="left">
                    <asp:Label ID="Label3" runat="server" Text="Following are questions for online student satisfaction survey regarding teaching learning process." Font-Bold="true" Style="font-size: 15px;"></asp:Label>
                </td>
            </tr>
        </table>
        <br />

        <table style="width: 100%;">
            <tr style="column-span: 2">
                <td style="width: 200px;" align="left">
                    <asp:GridView ID="grddata" runat="server" Style="width: 100%" Visible="true" AutoGenerateColumns="false" ShowFooter="true"
                        CssClass="table table-striped table-bordered table-hover" AlternatingRowStyle-CssClass="danger">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex +1 %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    21
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Question" HeaderStyle-Width="300px">
                                <ItemTemplate>
                                    <asp:Label ID="lblQuestion" Font-Bold="true" runat="server" Text='<%# Bind("[Question]") %>'></asp:Label>
                                    <asp:HiddenField ID="hdfID" runat="server" Value='<%# Bind("[ID]") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="txtQustion" runat="server" Width="300px" Font-Bold="true" Text="Give three observation / suggestions to improve the overall teaching - learning experience in your institution."></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="4">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk4" runat="server" Text='<%# Bind("[Option5]") %>' onclick="CheckBoxCheck(this);"></asp:CheckBox>

                                </ItemTemplate>

                                <FooterTemplate>
                                    <asp:TextBox ID="txtoption1" runat="server" Width="150px" placeholder="Option 1" TextMode="MultiLine"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="3">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk3" runat="server" Text='<%# Bind("[Option4]") %>' onclick="CheckBoxCheck(this);"></asp:CheckBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtoption2" runat="server" Width="150px" placeholder="Option 2" TextMode="MultiLine"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="2">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk2" runat="server" Text='<%# Bind("[Option3]") %>' onclick="CheckBoxCheck(this);"></asp:CheckBox>

                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtoption3" runat="server" Width="150px" placeholder="Option 3" TextMode="MultiLine"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="1">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk1" runat="server" Text='<%# Bind("[Option2]") %>' onclick="CheckBoxCheck(this);"></asp:CheckBox>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="0">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk0" runat="server" Text='<%# Bind("[Option1]") %>' onclick="CheckBoxCheck(this);"></asp:CheckBox>

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle CssClass="rowstyle" />
                    </asp:GridView>

                </td>

            </tr>

            <tr>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" BackColor="Blue" ForeColor="White" Text="Submit" OnClick="btnSubmit_Click" Width="87px"></asp:Button>

                </td>
            </tr>
        </table>
    </div>




    '


       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

