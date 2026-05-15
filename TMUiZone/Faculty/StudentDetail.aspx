<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="StudentDetail.aspx.cs" Inherits="Faculty_StudentDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style type="text/css">
body
{
    font-family: Arial;
    font-size: 10pt;
}
.Grid th
{
    color: #fff;
    background-color: #3AC0F2;
}
/* CSS to change the GridLines color */
.Grid, .Grid th, .Grid td
{
    border:1px solid #fff000;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  <br /> <br /> <br /> <br /> <br />

    <asp:GridView ID="grdTimetable"  AutoGenerateColumns="false" runat="server" BackColor="White"
                                BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="4px"  CssClass="Grid"  CellPadding="3" Width="100%" GridLines="Horizontal"
                                EmptyDataText="There are no data records to display." Font-Size="70px" Height="2000px">
                                <AlternatingRowStyle BackColor="#F7F7F7" />
                                <Columns>
                                    <asp:TemplateField HeaderText="" ControlStyle-Width="50px"  ControlStyle-Height="50px" >
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                    </asp:TemplateField>
                                  
                                 
                                   
                                    
                                   <%-- <asp:TemplateField HeaderText="Student Name" ControlStyle-Width="700px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblblStudent" Font-Size="60px" runat="server" Text='<%# Eval("[Student Name]") %>' />

                                        </ItemTemplate>
                                        <ItemStyle Width="4%" />
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="" ControlStyle-Width="400px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Image ID="lblImage" runat="server"  ImageUrl='<%# GetImage (Eval("[Student Image]")) %>' Width="200px"/>
                                            <asp:Label ID="lblblStudent" Font-Size="60px" runat="server" Text='<%# Eval("[Student Name]") %>' />

                                        </ItemTemplate>
                                        <ItemStyle Width="3%" />
                                    </asp:TemplateField>
                                   
                                    
                                </Columns>
                            </asp:GridView>
      
</asp:Content>

