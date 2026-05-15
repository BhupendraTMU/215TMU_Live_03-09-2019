<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="UploadRoaster.aspx.cs" Inherits="Faculty_UploadRoaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
    <table id="tblRoaster" runat="server"   style="width:100%" >
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
                    </asp:DropDownList></td>  <td style="width:10px"> </td> <td>  Year </td> <td style="width:10px"> </td> <td> <asp:DropDownList ID="ddlYear" Enabled="false" runat="server" Height="29px"></asp:DropDownList> </td> <td style="width:10px"> </td>  <td> Employee </td> <td style="width:10px">  </td><td> <asp:DropDownList ID="txtUserid" runat="server" Height="28px"></asp:DropDownList> </td> <td style="width:10px"> </td> <td> <asp:Button ID="btnGet" runat="server" Text="Get" OnClick="btnGet_Click" /> </td><td style="width:10px"> </td><td> </td> </tr> </table> </td></tr>

                <tr> <td style="height:13px"> 
                    <asp:Label ID="lblMonth" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lbleyear" runat="server" Visible="False"></asp:Label>
                    </td></tr>

                <tr> <td>

                      <asp:UpdatePanel ID="mrak" runat="server">
        <ContentTemplate>
    <table cellpadding="0px" cellspacing="0px">
        <tr>
            <td>
                <div style='overflow: auto; width: 1200px; height: 100px;'>
                    <asp:GridView ID="grddata" runat="server" CssClass="table table-striped table-bordered table-hover" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" AutoGenerateColumns="false" OnRowDataBound="grddata_RowDataBound" >
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
                                    <asp:DropDownList ID="drpShift1" runat="server"  ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Shift Code-2">
                                <ItemTemplate>
                                     <asp:DropDownList ID="drpShift2" runat="server"   ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-3">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpShift3" runat="server"   ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-4">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpShift4" runat="server"   ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-5">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpShift5" runat="server"   ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-6">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpShift6" runat="server"   ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-7">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpShift7" runat="server"  ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-8">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpShift8" runat="server"   ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-9">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpShift9" runat="server"   ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-10">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpShift10" runat="server"   ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-11">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpShift11" runat="server"   ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-12">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpShift12" runat="server"   ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-13">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpShift13" runat="server"   ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-14">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpShift14" runat="server"   ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-15">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpShift15" runat="server"   ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-16">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpShift16" runat="server"   ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-17">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpShift17" runat="server"   ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-18">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpShift18" runat="server"   ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Shift Code-19">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpShift19" runat="server"   ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-20">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpShift20" runat="server"   ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-21">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpShift21" runat="server"   ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-22">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpShift22" runat="server"   ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-23">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpShift23" runat="server"   ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-24">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpShift24" runat="server"   ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-25">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpShift25" runat="server"   ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-26">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpShift26" runat="server"  ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-27">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpShift27" runat="server"  ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-28">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpShift28" runat="server"  ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-29">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpShift29" runat="server"  ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Shift Code-30">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpShift30" runat="server"  ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Shift Code-31">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpShift31" runat="server"   ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>

                    </asp:GridView>
                </div>
            </td>
           
        </tr>
        <tr>
            <td style="height:20px">

            </td>
        </tr>
        <tr>
             <td align="right">
                <asp:Button ID="btnSubmit" Text="Update Roaster" runat="server" Visible="false" OnClick="btnSubmit_Click" />
            </td>
        </tr>
    </table>
            </ContentTemplate>
        </asp:UpdatePanel>
                   

                     </td></tr>

                <tr> <td style="height:90px"> </td></tr>
    </table>
</asp:Content>

