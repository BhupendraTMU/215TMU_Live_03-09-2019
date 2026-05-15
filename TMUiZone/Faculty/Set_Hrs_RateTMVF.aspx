<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="Set_Hrs_RateTMVF.aspx.cs" Inherits="Faculty_Set_Hrs_RateTMVF" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
    <div style="width: 100%; height: 600px; overflow: scroll">




        <table cellpadding="0px" cellspacing="0px" width="99%">

            <fieldset class="boxBody" style="text-align: center; border-color: black; background-color: black;">
                <asp:Label ID="Label1" runat="server" Text="VISITING FACULTY RATE SETUP" Font-Size="15pt" ForeColor="White" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

            </fieldset>
            <div id="divGeneralBodyenrollmentform">
                <fieldset class="boxBodyInner">
                    <div class="form-horizontal">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <div class="col-md-1">
                                            <label style="width: 200px">Employee</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="txtDesig" runat="server" BorderColor="Black" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                      <%--  <div class="col-md-1">
                                            <label style="width: 200px">Month</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="drpMonth" runat="server" BorderColor="Black" CssClass="form-control">

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

                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-1">
                                            <label style="width: 200px">Year</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlYear" runat="server" Height="29px" CssClass="form-control"></asp:DropDownList>
                                        </div>--%>
                                        <div class="col-md-1">
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" BackColor="#ff9933" ForeColor="White" OnClick="btnFilter_Click" CssClass="form-control" Height="35px" Width="100px"></asp:Button>
                                        </div>
                                    </div>
                                    <asp:GridView ID="grdEmployee" DataKeyNames="No_" runat="server" CellPadding="3" BorderWidth="1px" AlternatingRowStyle-CssClass="danger" PageSize="50" AllowPaging="true" OnPageIndexChanging="grdEmployee_PageIndexChanging"
                                        AutoGenerateColumns="false" OnRowCommand="grdEmployee_RowCommand" CssClass="table table-striped table-bordered table-hover" Visible="true" Width="1000px">

                                        <Columns>

                                            <asp:TemplateField HeaderText="Sr. No.">
                                                <ItemTemplate>
                                                    <span>
                                                        <%#Container.DataItemIndex + 1%>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Emp Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="EmpName" runat="server" Text='<%#Bind("[First Name]") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Emp Code" ControlStyle-Width="100px">
                                                <ItemTemplate>
                                                    <asp:Label ID="EmpCode" runat="server" Text='<%#Bind("[No_]") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Joing Date" ControlStyle-Width="100px">
                                                <ItemTemplate>
                                                    <asp:Label ID="JoingDate" runat="server" Text='<%#Bind("[Employment Date]") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="College Code" ControlStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCollege" runat="server" Text='<%#Bind("[Global Dimension 1 Code]") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Deptartment" ControlStyle-Width="120px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDepart" runat="server" Text='<%#Bind("[Dept]") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reporting" ControlStyle-Width="120px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReporting" runat="server" Text='<%#Bind("[Reporting]") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="Designation" runat="server" Text='<%#Bind("[Designation]") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Modify Date" ControlStyle-Width="70px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblModify" runat="server" Text='<%#Bind("[date]") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>                                        

                                            <asp:TemplateField HeaderText="Amount / hrs">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtAmount" runat="server" onkeypress="return isNumberKey(event)" Width="60px" Text='<%#Bind("[Amount]") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRemark" runat="server" Width="200px" Height="36px" Text='<%#Bind("[Remark]") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Update">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnUpdate" runat="server" CommandArgument='<%# Container.DataItemIndex%>'  CommandName="EditEE" Width="20px" Height="36px" Text="Update" />
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                        </Columns>
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <EmptyDataTemplate>
                                            There is no Record Found.
                                        </EmptyDataTemplate>
                                        <FooterStyle BackColor="#ed7600" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="White"
                                            HorizontalAlign="Left" CssClass="cssGridheaderfont" Font-Names="Open Sans" />
                                        <PagerStyle BackColor="#ed7600" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#E3EAEB" CssClass="cssGridheaderfont" Font-Names="Open Sans" Font-Size="10px" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                        <SortedDescendingHeaderStyle BackColor="#15524A" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </div>

        </table>
    </div>


</asp:Content>

