<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Co_PO_Mapping.aspx.cs" Inherits="Faculty_Co_PO_Mapping" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                alert("Please Enter Only Numeric Value:");
                return false;
            }

            return true;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="CO-PSO-MAPPING" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
    <fieldset class="boxBodyInner" runat="server">


        <asp:Panel ID="PnlDeatinedAttendence" runat="server">
            <fieldset class="boxBodyInner">
                <div class="loader" id="Loader1" style="display: none"></div>
            </fieldset>
            <fieldset class="boxBodyInner">

                <div class="col-sm-3 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 400px">&nbsp&nbsp&nbsp Course Code</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpCourse" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="drpCourse" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged" Width="200px"></asp:DropDownList>

                        </div>
                    </div>
                </div>
                <div class="col-sm-3 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 400px">&nbsp&nbsp&nbsp Semester Code</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpSemester" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="drpSemester" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpSemester_SelectedIndexChanged" CssClass="form-control" Width="200px"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-sm-3 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 400px">&nbsp&nbsp&nbsp Subject Code</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpSubject" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="drpSubject" runat="server" AutoPostBack="true" CssClass="form-control" Width="200px"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-sm-2 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 100px; visibility: hidden">&nbsp&nbsp&nbsp Subject Code</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpSubject" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                        <div class="col-sm-8">
                            <asp:Button ID="btnShow" runat="server" Text="SHOW" CssClass="btn-sm btn-primary btn-block" OnClick="btnShow_Click" ValidationGroup="g1" Height="30px" Width="90px" />
                        </div>
                    </div>

                </div>
                <%-- <div class="col-sm-1 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 100px">&nbsp&nbsp&nbsp  CO </label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpCo" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="drpCo" runat="server" CssClass="form-control" Width="85px">
                                <asp:ListItem Text="CO-1" Value="5"></asp:ListItem>
                                <asp:ListItem Text="CO-2" Value="6"></asp:ListItem>
                                <asp:ListItem Text="CO-3" Value="7"></asp:ListItem>
                                <asp:ListItem Text="CO-4" Value="8"></asp:ListItem>
                                <asp:ListItem Text="CO-5" Value="9"></asp:ListItem>
                                <asp:ListItem Text="PEO-1" Value="10"></asp:ListItem>
                                <asp:ListItem Text="PEO-2" Value="11"></asp:ListItem>
                                <asp:ListItem Text="PEO-3" Value="12"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>--%>
                <%--<div class="col-sm-2 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 100px">&nbsp&nbsp&nbsp  PO </label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpPO" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="drpPO" runat="server" CssClass="form-control" Width="85px">
                                <asp:ListItem Text="PO-1" Value="1"></asp:ListItem>
                                <asp:ListItem Text="PO-2" Value="2"></asp:ListItem>
                                <asp:ListItem Text="PO-3" Value="3"></asp:ListItem>
                                <asp:ListItem Text="PO-4" Value="4"></asp:ListItem>
                                <asp:ListItem Text="PO-5" Value="5"></asp:ListItem>
                                <asp:ListItem Text="PO-6" Value="6"></asp:ListItem>
                                <asp:ListItem Text="PO-7" Value="7"></asp:ListItem>
                                <asp:ListItem Text="PO-8" Value="8"></asp:ListItem>
                                <asp:ListItem Text="PO-9" Value="9"></asp:ListItem>
                                <asp:ListItem Text="PO-10" Value="10"></asp:ListItem>
                                <asp:ListItem Text="PO-11" Value="11"></asp:ListItem>
                                <asp:ListItem Text="PO-12" Value="12"></asp:ListItem>
                                <asp:ListItem Text="PSO-1" Value="13"></asp:ListItem>
                                <asp:ListItem Text="PSO-2" Value="14"></asp:ListItem>
                                <asp:ListItem Text="PSO-3" Value="15"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>--%>
                <%--<div class="col-sm-3 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp  Value </label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpValue" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="drpValue" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpValue_SelectedIndexChanged" Width="200px">
                                <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                <asp:ListItem Value="3" Text="3"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>--%>
                <%--<div class="col-sm-6 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp  Description </label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtDesc" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtDesc" runat="server" Enabled="false" Text="Not Correlated" CssClass="form-control" TextMode="MultiLine" Width="600px"></asp:TextBox>
                        </div>
                    </div>
                </div>--%>
            </fieldset>




            <h2 style="color: green">0-Not Correlated, 1-Low Correlated, 2-Moderate Correlated, 3-Highly Correlated </h2>
            <br />
            <div style="overflow: scroll; height: 200px; width: 100%" runat="server">

                <asp:GridView ID="GridCOPODetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
                    EmptyDataText="There are no data records to display." BorderStyle="None" BorderWidth="2px" CellPadding="30" Width="1130px"
                    GridLines="Horizontal" ShowFooter="true">
                    <AlternatingRowStyle BackColor="#F7F7F7" />
                    <Columns>
                        <asp:TemplateField HeaderText="S.No" ItemStyle-Width="4%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <%# Container.DataItemIndex +1 %>
                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="OUTCOME" ItemStyle-Width="15%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:Label ID="lblOutCome" runat="server" Text='<%# Bind("[OutCome]") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO-1" ItemStyle-Width="7%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:DropDownList ID="lblPO1" Width="50px" runat="server" Text='<%# Bind("[PO-1]") %>'>
                                    <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO-2" ItemStyle-Width="7%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:DropDownList ID="lblPO2" Width="50px" runat="server" Text='<%# Bind("[PO-2]") %>'>
                                    <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO-3" ItemStyle-Width="7%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:DropDownList ID="lblPO3" Width="50px" runat="server" Text='<%# Bind("[PO-3]") %>'>
                                    <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO-4" ItemStyle-Width="7%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:DropDownList ID="lblPO4" Width="50px" runat="server" Text='<%# Bind("[PO-4]") %>'>
                                    <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO-5" ItemStyle-Width="7%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:DropDownList ID="lblPO5" Width="50px" runat="server" Text='<%# Bind("[PO-5]") %>'>
                                    <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO-6" ItemStyle-Width="7%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:DropDownList ID="lblPO6" Width="50px" runat="server" Text='<%# Bind("[PO-6]") %>'>
                                    <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO-7" ItemStyle-Width="7%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:DropDownList ID="lblPO7" Width="50px" runat="server" Text='<%# Bind("[PO-7]") %>'>
                                    <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO-8" ItemStyle-Width="7%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:DropDownList ID="lblPO8" Width="50px" runat="server" Text='<%# Bind("[PO-8]") %>'>
                                    <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO-9" ItemStyle-Width="7%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:DropDownList ID="lblPO9" Width="50px" runat="server" Text='<%# Bind("[PO-9]") %>'>
                                    <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO-10" ItemStyle-Width="7%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:DropDownList ID="lblPO10" Width="50px" runat="server" Text='<%# Bind("[PO-10]") %>'>
                                    <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO-11" ItemStyle-Width="7%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:DropDownList ID="lblPO11" Width="50px" runat="server" Text='<%# Bind("[PO-11]") %>'>
                                    <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO-12" ItemStyle-Width="7%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:DropDownList ID="lblPO12" Width="50px" runat="server" Text='<%# Bind("[PO-12]") %>'>
                                    <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PSO-1" ItemStyle-Width="7%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:DropDownList ID="lblPSO1" Width="50px" runat="server" Text='<%# Bind("[PSO-1]") %>'>
                                    <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PSO-2" ItemStyle-Width="7%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:DropDownList ID="lblPSO2" Width="50px" runat="server" Text='<%# Bind("[PSO-2]") %>'>
                                    <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PSO-3" ItemStyle-Width="7%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:DropDownList ID="lblPSO3" Width="50px" runat="server" Text='<%# Bind("[PSO-3]") %>'>
                                    <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle ForeColor="Green" Font-Bold="true" Font-Size="Medium" BorderStyle="Solid" BorderColor="Black" BackColor="LightGray" />
                    <HeaderStyle BackColor="LightGray" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" Font-Size="Large" Height="40px" VerticalAlign="Bottom" />
                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                    <RowStyle ForeColor="#4A3C8C" Font-Bold="true" Font-Size="Medium" Height="30px" BorderStyle="Solid" BorderColor="Black" />
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                    <SortedAscendingCellStyle BackColor="#F4F4FD" />
                    <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                    <SortedDescendingCellStyle BackColor="#D8D8F0" />
                    <SortedDescendingHeaderStyle BackColor="#3E3277" />
                </asp:GridView>

                <div id="divreport" runat="server" visible="false">
                 <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
                    EmptyDataText="There are no data records to display." BorderStyle="None" BorderWidth="2px" CellPadding="30" Width="1130px"
                    GridLines="Horizontal" ShowFooter="true">
                    <AlternatingRowStyle BackColor="#F7F7F7" />
                    <Columns>
                        <asp:TemplateField HeaderText="S.No" ItemStyle-Width="4%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <%# Container.DataItemIndex +1 %>
                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="OUTCOME" ItemStyle-Width="15%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:Label ID="lblOutCome" runat="server" Text='<%# Bind("[OutCome]") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO-1" ItemStyle-Width="7%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:Label ID="lblPO1" Width="50px" runat="server" Text='<%# Bind("[PO-1]") %>'>
                                   
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO-2" ItemStyle-Width="7%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:Label ID="lblPO2" Width="50px" runat="server" Text='<%# Bind("[PO-2]") %>'>
                                   
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO-3" ItemStyle-Width="7%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:Label ID="lblPO3" Width="50px" runat="server" Text='<%# Bind("[PO-3]") %>'>
                                    
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO-4" ItemStyle-Width="7%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:Label ID="lblPO4" Width="50px" runat="server" Text='<%# Bind("[PO-4]") %>'>
                                   
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO-5" ItemStyle-Width="7%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:Label ID="lblPO5" Width="50px" runat="server" Text='<%# Bind("[PO-5]") %>'>
                                   
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO-6" ItemStyle-Width="7%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:Label ID="lblPO6" Width="50px" runat="server" Text='<%# Bind("[PO-6]") %>'>
                                   
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO-7" ItemStyle-Width="7%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:Label ID="lblPO7" Width="50px" runat="server" Text='<%# Bind("[PO-7]") %>'>
                                    
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO-8" ItemStyle-Width="7%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:Label ID="lblPO8" Width="50px" runat="server" Text='<%# Bind("[PO-8]") %>'>
                                   
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO-9" ItemStyle-Width="7%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:Label ID="lblPO9" Width="50px" runat="server" Text='<%# Bind("[PO-9]") %>'>
                                   
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO-10" ItemStyle-Width="7%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:Label ID="lblPO10" Width="50px" runat="server" Text='<%# Bind("[PO-10]") %>'>
                                  
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO-11" ItemStyle-Width="7%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:Label ID="lblPO11" Width="50px" runat="server" Text='<%# Bind("[PO-11]") %>'>
                                  
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO-12" ItemStyle-Width="7%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:Label ID="lblPO12" Width="50px" runat="server" Text='<%# Bind("[PO-12]") %>'>
                                  
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PSO-1" ItemStyle-Width="7%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:Label ID="lblPSO1" Width="50px" runat="server" Text='<%# Bind("[PSO-1]") %>'>
                                   
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PSO-2" ItemStyle-Width="7%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:Label ID="lblPSO2" Width="50px" runat="server" Text='<%# Bind("[PSO-2]") %>'>
                                   
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PSO-3" ItemStyle-Width="7%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:Label ID="lblPSO3" Width="50px" runat="server" Text='<%# Bind("[PSO-3]") %>'>
                                   
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle ForeColor="Green" Font-Bold="true" Font-Size="Medium" BorderStyle="Solid" BorderColor="Black" BackColor="LightGray" />
                    <HeaderStyle BackColor="LightGray" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" Font-Size="Large" Height="40px" VerticalAlign="Bottom" />
                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                    <RowStyle ForeColor="#4A3C8C" Font-Bold="true" Font-Size="Medium" Height="30px" BorderStyle="Solid" BorderColor="Black" />
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                    <SortedAscendingCellStyle BackColor="#F4F4FD" />
                    <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                    <SortedDescendingCellStyle BackColor="#D8D8F0" />
                    <SortedDescendingHeaderStyle BackColor="#3E3277" />
                </asp:GridView>
                </div>
            </div>
             <div class="col-sm-1 p-0">
                    <div class="form-group clearfix">
                         
                        <div class="col-sm-8">
                          <asp:Button ID="btnSave" runat="server" CssClass="btn-sm btn-primary btn-block" Height="30px" Width="90px" OnClick="btnSave_Click" Text="Save" />
                        
                        </div>
                    </div>
                </div>

            <div class="col-sm-1 p-0">
                    <div class="form-group clearfix">
                      
                        <div class="col-sm-8">
                           <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" Height="30px" Width="90px" OnClick="btnSubmit_Click" Text="SUBMIT" />

                        </div>
                    </div>
                </div>
                <div class="col-sm-1 p-0">
                    <div class="form-group clearfix">
                         
                        <div class="col-sm-8">
                          <asp:Button ID="btnExport" runat="server" CssClass="btn-sm btn-primary btn-block" Height="30px" Width="90px" OnClick="btnExport_Click" Text="Export" />
                        
                        </div>
                    </div>
                </div>

        

        </asp:Panel>



    </fieldset>
</asp:Content>

