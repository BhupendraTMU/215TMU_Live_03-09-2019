<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/IndexMaster.master" AutoEventWireup="true" CodeFile="EventList.aspx.cs" Inherits="Alumni_EventList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        @page :footer {
            display: none;
        }

        @page :header {
            display: none;
        }
    </style>
    <script type="text/javascript">

        function preventBackspace(e) {
            var evt = e || window.event;
            if (evt) {
                var keyCode = evt.charCode || evt.keyCode;
                if (keyCode === 8) {
                    if (evt.preventDefault) {
                        evt.preventDefault();
                    } else {
                        evt.returnValue = false;
                    }
                }
            }
        }
        function checkDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("You cannot select greater than current date!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
        function checkDate1(sender, args) {
            if (sender._selectedDate < new Date()) {
                alert("You cannot select Less than current date!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <%-- <asp:UpdatePanel ID="elist" runat="server">
        <ContentTemplate>--%>




    <asp:Panel ID="pnlList" runat="server" BorderWidth="2px" BorderColor="#ACE9FB" ScrollBars="Vertical">
        <div class="navbar-inverse" style="height: 40px; background-color: #1ECCF3">
            <marquee><asp:Label id="lblEvent" style="font-size:20px; line-height:45px"  ForeColor="White"  runat="server" ></asp:Label>
                <asp:Label id="Label1" style="font-size:20px; line-height:45px;margin-left:60px;"  ForeColor="White"  runat="server" ></asp:Label>
            </marquee>
        </div>
        <div class="parent" style="background-color: #ff6a00">
        </div>

        <div class="table-responsive">
            <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered table-hover" DataKeyNames="Code" AutoGenerateColumns="false" BackColor="White" EmptyDataText="Will Update Soon." EmptyDataRowStyle-BackColor="Yellow" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" GridLines="Horizontal" AllowPaging="true"
                PageSize="55">
                <AlternatingRowStyle BackColor="#F7F7F7" />
                <Columns>
                    <asp:TemplateField HeaderText="Sr. No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <ItemStyle Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Event Name">
                        <ItemStyle Width="7%" />
                        <ItemTemplate>
                            <asp:Label ID="lblEvent" runat="server" Text='<%#Eval("Event") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Event Type">
                        <ItemStyle Width="7%" />
                        <ItemTemplate>
                            <asp:Label ID="lblEventType" runat="server" Text='<%#Eval("[EventType]") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Objective">
                        <ItemStyle Width="25%" />
                        <ItemTemplate>
                            <asp:TextBox ID="lblobjEvent" runat="server" Text='<%#Eval("[Objective of Event]") %>' TextMode="MultiLine" Width="100%" ReadOnly="true"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>




                    <asp:TemplateField HeaderText="Date" Visible="false">
                        <ItemStyle Width="10%" />
                        <ItemTemplate>
                            <asp:Label ID="lbldate" runat="server" Text='<%#Eval("Date","{0:dd MMM yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Organized By">
                        <ItemStyle Width="7%" />
                        <ItemTemplate>
                            <asp:Label ID="lblorganizedby" runat="server" Text='<%#Eval("[OrganizedBy]") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>




                    <%--<asp:TemplateField HeaderText="Program"> <ItemStyle Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblprogram" runat="server"  Text='<%#Eval("[Program]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="Batch">
                        <ItemStyle Width="7%" />
                        <ItemTemplate>
                            <asp:Label ID="lblBatch" runat="server" Text='<%#Eval("[Batch]") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Venue">
                        <ItemStyle Width="7%" />
                        <ItemTemplate>
                            <asp:Label ID="lblvenue" runat="server" Text='<%#Eval("[Organization]") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Date From">
                        <ItemStyle Width="10%" />
                        <ItemTemplate>
                            <asp:Label ID="lblFromdate" runat="server" Text='<%#Eval("[From Date]","{0:dd MMM yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Date To">
                        <ItemStyle Width="10%" />
                        <ItemTemplate>
                            <asp:Label ID="lbltodate" runat="server" Text='<%#Eval("[To Date]","{0:dd MMM yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Connecting Link">
                        <ItemStyle Width="10%" />
                        <ItemTemplate>
                            <a href="#" onclick='openWindow("4");'>View Details</a>
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
            <br />




        </div>
    </asp:Panel>



    <%--</ContentTemplate>
        
    </asp:UpdatePanel>--%>

    <script type="text/javascript">
        function openWindow(code) {
            window.open('AluminiTalk.aspx', 'open_window', ' width=820, height=500, left=300, top=100');
        }
    </script>

</asp:Content>

