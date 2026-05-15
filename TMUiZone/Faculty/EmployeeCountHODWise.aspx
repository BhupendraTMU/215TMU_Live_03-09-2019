<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="EmployeeCountHODWise.aspx.cs" Inherits="Faculty_EmployeeCountHODWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        .txtstyle {
            border-top-left-radius: 5px;
            border-top-right-radius: 5px;
            border-bottom-left-radius: 5px;
            border-bottom-right-radius: 5px;
            background: #FFFFFF no-repeat 2px 2px;
            padding: 1px 1px 1px 5px;
            border: 2px solid #9900FF;
        }

            .txtstyle:focus {
                transition: all 0.30s ease-in-out;
                border: 1px solid #000000;
            }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset class="boxBody">
        <asp:Label ID="Label3" runat="server"
            Text="Employee Count HOD Wise" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
    </fieldset>

    <fieldset class="boxBody">
        <table>
            <tr style="width: 95%">

                <td align="right">
                    <asp:Button ID="btnExport" runat="server" Text="Export to Excel" CssClass="btnLogin" OnClick="btnExport_Click" />
                </td>
            </tr>
        </table>

    </fieldset>
    <table>
        <tr>
            <td style="width: 50px"></td>

            <td>
                <div style="overflow: auto; height: 450px; width: 1100px">
                    <asp:GridView ID="grdData" runat="server"
                        AutoGenerateColumns="False"
                        ShowFooter="true"
                        Width="1000px"
                        CssClass="gridview"
                        OnRowDataBound="grdData_RowDataBound"
                        EmptyDataText="There is no detail">

                        <Columns>

                         
                            <asp:TemplateField HeaderText="Sr No" HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>

                        
                            <asp:BoundField DataField="HOD Name" HeaderText="HOD Name" />

                          
                            <asp:BoundField DataField="HOD" HeaderText="HOD" />

                           
                            <asp:TemplateField HeaderText="Employee Count"
                                ItemStyle-HorizontalAlign="Right"
                                FooterStyle-HorizontalAlign="Right">

                                <ItemTemplate>
                                    <asp:Label ID="lblqty" runat="server"
                                        Text='<%# Eval("Employee Count") %>' />
                                </ItemTemplate>

                                <FooterTemplate>
                                    <asp:Label ID="lblTotalqty" runat="server"
                                        Font-Bold="true" />
                                </FooterTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>

                </div>
            </td>
        </tr>
        <tr>
            <td style="height: 50px"></td>
        </tr>
    </table>






</asp:Content>

