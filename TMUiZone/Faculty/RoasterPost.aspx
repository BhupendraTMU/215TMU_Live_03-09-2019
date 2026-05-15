<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="RoasterPost.aspx.cs" Inherits="Faculty_RoasterPost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
    <table cellpadding="0px" cellspacing="0px" style="width:100%">
            <tr> <td style="height:13px"> </td></tr>
         <tr> <td>  


      &nbsp;&nbsp;&nbsp; &nbsp;  <asp:Label ID="Label3" runat="server" 
            Text="Upload Roaster Data" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
                                                     </td></tr> 

            <tr> <td style="height:13px"> </td></tr>


        <tr> <td class="leftm">  </td></tr>

        <tr> <td style="height:13px"> </td></tr>

        <tr> <td align="center">  <table cellpadding="0px" cellspacing="0px"> <tr> <td>  Month</td> <td style="width:10px"> </td> <td> <asp:DropDownList ID="ddlMonth" Enabled="false" runat="server" Height="29px">
                        <asp:ListItem Value="01">January</asp:ListItem>
                        <asp:ListItem Value="02">February</asp:ListItem>
                        <asp:ListItem Value="03">March</asp:ListItem>
                        <asp:ListItem Value="04">April</asp:ListItem>
                        <asp:ListItem Value="05">May</asp:ListItem>
                        <asp:ListItem Value="06">June</asp:ListItem>
                        <asp:ListItem Value="07">July</asp:ListItem>
                        <asp:ListItem Value="08">August</asp:ListItem>
                        <asp:ListItem Value="09">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList></td>  <td style="width:10px"> </td> <td>  Year </td> <td style="width:10px"> </td> <td> <asp:DropDownList ID="ddlYear" Enabled="false" runat="server" Height="29px"></asp:DropDownList> </td> <td style="width:10px"> </td> 
             <td>  </td> <td style="width:50px">  </td><td> <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" ></asp:Button> </td> <td> <asp:Button ID="btnPost" runat="server" Text="Final Post" OnClick="btnPost_Click" ></asp:Button> </td> 
            
            <td style="width:10px"> </td> <td style="width:10px"> </td><td> </td> </tr> </table> </td></tr>

                <tr> <td style="height:13px"> 
                    <asp:Label ID="lblMonth" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lbleyear" runat="server" Visible="False"></asp:Label>
                    </td></tr>

             </table>
     <div style='overflow: auto; width: 1200px'>
                    <asp:GridView ID="grddata" runat="server" CssClass="table table-striped table-bordered table-hover" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" AutoGenerateColumns="false"  >
                        <%--OnRowDataBound="grddata_RowDataBound"--%>
                        <Columns>
                            <asp:TemplateField HeaderText="SNo">
                                <ItemTemplate>
                                    <span>
                                        <%#Container.DataItemIndex + 1%>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Employee Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmplo_Code" runat="server" Text='<%#Bind("[First Name]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                           
                          <asp:TemplateField HeaderText="Shift Code-1">
                                <ItemTemplate>
                                    <asp:Label ID="drpShift1" runat="server" Text='<%#Bind("[Shift Code-1]") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Shift Code-2">
                                <ItemTemplate>
                                     <asp:Label ID="drpShift2" runat="server"  Text='<%#Bind("[Shift Code-2]") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-3">
                                <ItemTemplate>
                                    <asp:Label ID="drpShift3" runat="server"  Text='<%#Bind("[Shift Code-3]") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-4">
                                <ItemTemplate>
                                    <asp:Label ID="drpShift4" runat="server" Text='<%#Bind("[Shift Code-4]") %>'  ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-5">
                                <ItemTemplate>
                                    <asp:Label ID="drpShift5" runat="server" Text='<%#Bind("[Shift Code-5]") %>'  ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-6">
                                <ItemTemplate>
                                    <asp:Label ID="drpShift6" runat="server" Text='<%#Bind("[Shift Code-6]") %>'  ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-7">
                                <ItemTemplate>
                                    <asp:Label ID="drpShift7" runat="server" Text='<%#Bind("[Shift Code-7]") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-8">
                                <ItemTemplate>
                                    <asp:Label ID="drpShift8" runat="server" Text='<%#Bind("[Shift Code-8]") %>'  ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-9">
                                <ItemTemplate>
                                    <asp:Label ID="drpShift9" runat="server" Text='<%#Bind("[Shift Code-9]") %>'  ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-10">
                                <ItemTemplate>
                                    <asp:Label ID="drpShift10" runat="server"  Text='<%#Bind("[Shift Code-10]") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-11">
                                <ItemTemplate>
                                    <asp:Label ID="drpShift11" runat="server"  Text='<%#Bind("[Shift Code-11]") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-12">
                                <ItemTemplate>
                                    <asp:Label ID="drpShift12" runat="server"  Text='<%#Bind("[Shift Code-12]") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-13">
                                <ItemTemplate>
                                    <asp:Label ID="drpShift13" runat="server" Text='<%#Bind("[Shift Code-13]") %>'  ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-14">
                                <ItemTemplate>
                                    <asp:Label ID="drpShift14" runat="server" Text='<%#Bind("[Shift Code-14]") %>'  ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-15">
                                <ItemTemplate>
                                    <asp:Label ID="drpShift15" runat="server" Text='<%#Bind("[Shift Code-15]") %>'  ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-16">
                                <ItemTemplate>
                                    <asp:Label ID="drpShift16" runat="server" Text='<%#Bind("[Shift Code-16]") %>'  ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-17">
                                <ItemTemplate>
                                    <asp:Label ID="drpShift17" runat="server" Text='<%#Bind("[Shift Code-17]") %>'  ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-18">
                                <ItemTemplate>
                                    <asp:Label ID="drpShift18" runat="server"  Text='<%#Bind("[Shift Code-18]") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Shift Code-19">
                                <ItemTemplate>
                                    <asp:Label ID="drpShift19" runat="server"  Text='<%#Bind("[Shift Code-19]") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-20">
                                <ItemTemplate>
                                    <asp:Label ID="drpShift20" runat="server"  Text='<%#Bind("[Shift Code-20]") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-21">
                                <ItemTemplate>
                                    <asp:Label ID="drpShift21" runat="server"  Text='<%#Bind("[Shift Code-21]") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-22">
                                <ItemTemplate>
                                    <asp:Label ID="drpShift22" runat="server"  Text='<%#Bind("[Shift Code-22]") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-23">
                                <ItemTemplate>
                                    <asp:Label ID="drpShift23" runat="server" Text='<%#Bind("[Shift Code-23]") %>'  ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-24">
                                <ItemTemplate>
                                    <asp:Label ID="drpShift24" runat="server" Text='<%#Bind("[Shift Code-24]") %>'  ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-25">
                                <ItemTemplate>
                                    <asp:Label ID="drpShift25" runat="server" Text='<%#Bind("[Shift Code-25]") %>'  ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-26">
                                <ItemTemplate>
                                    <asp:Label ID="drpShift26" runat="server" Text='<%#Bind("[Shift Code-26]") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-27">
                                <ItemTemplate>
                                    <asp:Label ID="drpShift27" runat="server" Text='<%#Bind("[Shift Code-27]") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-28">
                                <ItemTemplate>
                                    <asp:Label ID="drpShift28" runat="server" Text='<%#Bind("[Shift Code-28]") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-29">
                                <ItemTemplate>
                                    <asp:Label ID="drpShift29" runat="server" Text='<%#Bind("[Shift Code-29]") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-30">
                                <ItemTemplate>
                                    <asp:Label ID="drpShift30" runat="server" Text='<%#Bind("[Shift Code-30]") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Shift Code-31">
                                <ItemTemplate>
                                    <asp:Label ID="drpShift31" runat="server"  Text='<%#Bind("[Shift Code-31]") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>

                    </asp:GridView>
                </div>

</asp:Content>

