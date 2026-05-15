<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="COAllow.aspx.cs" Inherits="Faculty_COAllow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="Compensatory Off Allow" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>

    <table style="padding-left: 250px">
        <tr>
            <td style="height: 20px"></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td style="width: 50px"></td>
            <td>Employee Code</td>
            <td style="width: 10px"></td>
            <td>
                <asp:TextBox ID="txtEmployee" runat="server" Height="23px" Width="100px"></asp:TextBox>

            </td>
            <td style="width: 10px"></td>
            <td>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td style ="width:50px"></td>
            <td>
                <asp:GridView ID="grdApproval" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                    <Columns>
                        <asp:TemplateField HeaderText="SNo">
                            <ItemTemplate>
                                <span>
                                    <%#Container.DataItemIndex + 1%>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Employee Code" HeaderStyle-Width="150px">
                            <ItemTemplate>
                                <asp:Label ID="lblEmployee" runat="server" Text='<%#Bind("EmployeeCode") %>'></asp:Label>


                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Employee Name" HeaderStyle-Width="250px"> 
                            <ItemTemplate>
                                <asp:Label ID="lblEmployeeName" runat="server" Text='<%#Bind("EmployeeName") %>'></asp:Label>


                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Statue" HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("Status") %>'></asp:Label>


                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>


</asp:Content>

