<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/IndexMaster.master" AutoEventWireup="true" CodeFile="AlumniFeedback.aspx.cs" Inherits="Alumni_AlumniFeedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .chkChoiceRadio input {
            margin-left: 15px;
            height: 15px;
            width: 18px;
        }

        .chkChoiceRadio td {
            padding-left: 20px;
            /*background-color: #f4ecce;*/
        }

        .chkChoiceRadio1 input {
            margin-left: 15px;
            height: 15px;
            width: 18px;
        }

        .chkChoiceRadio1 td {
            padding-left: 20px;
            /*background-color: #f4ecce;*/
        }
    </style>
    <script type="text/javascript">
        function CheckBoxCheck(rb) {
            var gv = document.getElementById("<%=grdCourse.ClientID%>");
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
            var gridview = document.getElementById('<%= grdCourse.ClientID %>');
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
    <asp:Panel ID="pnlError" runat="server" Visible="false">
        <table style="width: 100%; text-align: center">
            <tr>
                <td>
                    <asp:Label ID="lblMsg" Font-Bold="true" Font-Size="Large" runat="server" Text="Alumni Feedback  is complete. "></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlMain" runat="server" Visible="false">
        <div style="width: 100%; margin-bottom: 10px; border: 2px solid">
            <div style="width: 100%; margin-bottom: 10px; margin-left: 1%; margin-right: 1%; height: 100%; margin-top: 5px; border: 1px; border-color: black;">
                <table style="width: 98%;">
                    <tr>
                        <td style="height: 60px; text-align: center">
                            <asp:Image ID="Image1" runat="server" Height="100" Width="250" ImageUrl="../logo/logo123.png" /></td>
                    </tr>
                    <tr>
                        <td style="height: 20px"></td>
                    </tr>
                    <tr>
                        <td style="height: 40px; font-size: larger; color: black; font-weight: bold; text-align: center">ALUMNI FEEDBACK ON SYLLABUS
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: larger; color: black; font-weight: bold; text-align: center; margin-bottom: 200px">SESSION :
                        <asp:Label ID="lblSession" runat="server"></asp:Label>
                        </td>

                    </tr>
                </table>
                <br />
                <table style="width: 98%;">
                    <tr>
                        <td style="border: 1px solid; height: 30px; width: 50%; margin-left: 5px; Padding-left: 10px">
                            <p style="font-size: large; color: black">Name:&nbsp&nbsp<asp:Label ID="lblAlumniName" runat="server" Font-Bold="true" Text=""></asp:Label></p>
                        </td>

                        <td style="border: 1px solid; height: 30px; width: 50%; margin-left: 5px; Padding-left: 10px">
                            <p style="font-size: large; color: black">Current Org. with designation:&nbsp&nbsp<asp:Label ID="lblCurrDesg"  Font-Bold="true" runat="server" Text=""></asp:Label></p>
                        </td>
                    </tr>
                    <tr>
                        <td style="border: 1px solid; height: 30px; width: 50%; margin-left: 5px; Padding-left: 10px">
                            <p style="font-size: large; color: black">Programme with specialization:&nbsp&nbsp<asp:Label ID="Label1" Font-Bold="true"  runat="server" Text=""></asp:Label></p>
                        </td>

                        <td style="border: 1px solid; height: 30px; width: 50%; margin-left: 5px; Padding-left: 10px">
                            <p style="font-size: large; color: black">Email:&nbsp&nbsp<asp:Label ID="Label2" Font-Bold="true" runat="server" Text=""></asp:Label></p>
                        </td>
                    </tr>
                    <tr>
                        <td style="border: 1px solid; height: 30px; width: 50%; margin-left: 5px; Padding-left: 10px">
                            <p style="font-size: large; color: black">Year of passing out:&nbsp&nbsp<asp:Label ID="Label3" Font-Bold="true" runat="server" Text=""></asp:Label></p>
                        </td>

                        <td style="border: 1px solid; height: 30px; width: 50%; margin-left: 5px; Padding-left: 10px">
                            <p style="font-size: large; color: black">Mobile:&nbsp&nbsp<asp:Label ID="Label4" runat="server" Font-Bold="true" Text=""></asp:Label></p>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 30px; width: 50%; margin-left: 5px; Padding-left: 10px" colspan="2">Directions: A score for each item Between 1 and 5 as:<br />
                            (A) Excellent-5, (B) Very Good – 4 (C) Good - 3 (D) Average - 2 (E) Poor - 1<br />
                            <br />
                            Please indicate your level of satisfaction with the following statement by choosing between 1 and 5
                        </td>
                    </tr>
                </table>
                <table style="width: 98%;">
                    <tr>
                        <td>
                            <asp:GridView ID="grdCourse" runat="server" Style="width: 98%" Visible="true" AutoGenerateColumns="false"
                                CssClass="table table-striped table-bordered table-hover" HeaderStyle-BackColor="Transparent">
                                <Columns>
                                    <asp:TemplateField HeaderText="S. N.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle Width="3%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Question" HeaderStyle-Width="500px" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuestion" Font-Bold="true" runat="server" Text='<%# Bind("[Question]") %>'></asp:Label>
                                              <asp:HiddenField ID="hdfID" runat="server" Value='<%# Bind("[ID]") %>' />
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Excellent" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="rbtExcell" runat="server" onclick="CheckBoxCheck(this);" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Very Good" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="rbtVGood" runat="server" onclick="CheckBoxCheck(this);" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Good" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="rbtGood" runat="server" onclick="CheckBoxCheck(this);" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Average" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="rbtAverage" runat="server" onclick="CheckBoxCheck(this);" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Poor" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="rbtPoor" runat="server" onclick="CheckBoxCheck(this);" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle ForeColor="Black" BackColor="Transparent"></RowStyle>
                            </asp:GridView>
                        </td>

                    </tr>

                </table>
                <table style="width: 98%; border: 1px solid">
                    <tr>
                        <td colspan="2" style="height: 25px; font-size: larger; color: black; font-weight: bold; text-align: center">Comment & Suggestions
                        </td>

                    </tr>
                    <tr>
                        <td style="border: 1px solid; height: 30px; width: 40%; margin-left: 5px; Padding-left: 10px">Suggestions for modifications in the syllabi (Mention the course name/code)
                        </td>
                        <td style="border: 1px solid; height: 30px; width: 60%; margin-left: 0px; Padding-left: 0px">
                            <asp:TextBox ID="txtSugg" Width="800px" Height="35px" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td style="border: 1px solid; height: 30px; width: 40%; margin-left: 5px; Padding-left: 10px">Any other comments/suggestions
                        </td>
                        <td style="border: 1px solid; height: 30px; width: 60%; margin-left: 0px; Padding-left: 0px">
                            <asp:TextBox ID="TextBox1" Width="800px" Height="35px" runat="server"></asp:TextBox>
                        </td>
                    </tr>


                </table>
                <table style="width: 98%">
                    <tr>
                        <td style="border: none; height: 30px; font-size: large; width: 40%; margin-left: 5px; padding-top: 30px; Padding-left: 10px; font-weight: bold">Signature
                        </td>
                        <td style="border: none; height: 30px; font-size: large; width: 60%; margin-left: 55px; padding-top: 30px; Padding-left: 0px; font-weight: bold; text-align: center">Date :
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="padding-top: 30px;text-align:right">
                            <asp:Button ID="btnSubmit" runat="server" BackColor="Blue" ForeColor="White" Text="Submit" OnClick="btnSubmit_Click" Width="87px"></asp:Button>

                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <%-- <script type="text/javascript">
       
        function rdoBtnCount() {
            var count = document.getElementById('<%= hfcount.ClientID %>').value
            var RadioCount = 0;
            var count1 = count;
            for (var i = 0; i < count; i++) {

                if (document.getElementById("ContentPlaceHolder1_grdaddedEmployee_HfRequired_" + i).value == 1) {
                   
                    for (var j = 0; j < 5; j++) {
                        if (document.getElementById("ContentPlaceHolder1_grdaddedEmployee_ddlRating1_" + i + "_" + j + "_" + i).checked) {
                            RadioCount = RadioCount + 1;
                        }
                    }
                }
                else {
                    count1 = count1 - 1;
                }
            }

            var countYesNo = document.getElementById('<%= hfcountYesNo.ClientID %>').value
            var RadioCountYesNo = 0;
            for (var i = 0; i < countYesNo; i++) {

                for (var j = 0; j < 2; j++) {
                    if (document.getElementById("ContentPlaceHolder1_rptrYesNo_ddrptrYesNo_" + i + "_" + j + "_" + i).checked) {
                        RadioCountYesNo = RadioCountYesNo + 1;
                    }
                }
            }
            debugger
            if (parseInt(count1) != parseInt(RadioCount)) {
                alert("Please mark all rating question.")
                return false;
            }
            else {
                debugger
                if (parseInt(countYesNo) != parseInt(RadioCountYesNo)) {
                    alert("Please mark all yes no question.")
                    return false;
                }
                else {
                    return true;
                }
            }

        }
    </script>--%>
    </asp:Panel>
</asp:Content>

