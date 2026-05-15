<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="FellowshipForm.aspx.cs" Inherits="Faculty_FellowshipForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


        <script type="text/javascript">



     function Confirm() {

            var confirm_value = document.createElement("INPUT");


            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to save data?")) {
                confirm_value.value = "Yes";



            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }

    </script>

    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 500px;
            height: 200px;
        }
    </style>
    <style type="text/css">
        .GridPager a, .GridPager span {
            display: block;
            height: 15px;
            width: 15px;
            font-weight: bold;
            text-align: center;
            text-decoration: none;
        }

        .GridPager a {
            background-color: #f5f5f5;
            border: 1px solid #969696;
        }

        .GridPager span {
            background-color: #A1DCF2;
            border: 1px solid #3AC0F2;
        }

        .auto-style3 {
            width: 185px;
        }

        .auto-style4 {
            width: 194px;
        }

        .auto-style5 {
            width: 46px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <table cellpadding="0px" cellspacing="0px" style="width: 100%">
        <tr>
            <td style="height: 13px"></td>
        </tr>
         <tr style="width:400px;border: 1px solid" >
               
            <td style="height: 13px; width:150px"  >&nbsp&nbsp&nbsp&nbsp Account No.</td>
             <td style="height: 13px"><asp:TextBox ID="txtACNo" runat="server" Placeholder="Account Number" ></asp:TextBox>  </td>
            <td style="height: 13px">IFSC Code.</td>
             <td style="height: 13px"><asp:TextBox ID="txtIfscCode" runat="server" Placeholder="IFSC Code" ></asp:TextBox>  </td>
            <td style="height: 13px">Bank Name & Branch</td>
             <td style="height: 13px"><asp:TextBox ID="txtBankName" runat="server" Placeholder="Bank Name" ></asp:TextBox>  </td>
             <td style="height: 13px"><asp:Button ID="btnSubmit" runat="server" Text="Submit"  OnClick="btnSubmit_Click" ></asp:Button>  </td>
        </tr>
         <tr >
            <td colspan="8" style="height:20px">


            </td>
        </tr>
        <tr>
            <td colspan="8">&nbsp;&nbsp;&nbsp; &nbsp; 
                <asp:Label ID="Label3" runat="server"
                    Text="Details of the Daily Activities Done by the Research Fellow" Font-Size="15pt" ForeColor="#093A62"
                    Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>




            </td>
        </tr>
        <tr >
            <td colspan="8" style="height:20px">


            </td>
        </tr>
        <asp:UpdatePanel ID="PanelMain" runat="server">
            <ContentTemplate>


           
        <tr>
            
            <td align="center" colspan="8">
                <table cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td>Month</td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:DropDownList ID="ddlMonth" runat="server" Height="29px">
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
                            </asp:DropDownList></td>
                        <td style="width: 10px"></td>
                        <td>Year </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:DropDownList ID="ddlYear" runat="server" Height="29px"></asp:DropDownList>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:Button ID="btnGet" runat="server" Text="Get" OnClick="btnGet_Click" />
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:Button ID="btnexporttoexcel" runat="server" Text="Export To Excel" /></td>
                    </tr>
                </table>
            </td>
        </tr>

        </ContentTemplate>


        </asp:UpdatePanel>

        <tr>
            <td colspan="8">

                <table cellpadding="0px" cellspacing="0px" style="width: 100%">
                    <tr>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:GridView ID="grd_ViewAttendance" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" OnRowDataBound="grd_ViewAttendance_RowDataBound" BorderWidth="1px" CellPadding="4" CssClass="table table-striped table-bordered table-hover">
                                <Columns>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDate" runat="server" Text='<%#Bind("[Attendance Date]") %>' Enabled="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Week Day">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWeekDay" runat="server" Text='<%#Bind("[Week Day]") %>' Enabled="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Shift Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lblShiftTime" runat="server" Text='<%#Bind("[ShiftTime]") %>' Enabled="false"></asp:Label>
                                            <asp:HiddenField ID="hdfWHour" runat="server"  value='<%#Bind("[WorkingHour]") %>' />
                                             <asp:HiddenField ID="HiddenField1" runat="server"  value='<%#Bind("[Guide_Approval_Status]") %>' />
                                             <asp:HiddenField ID="HiddenField2" runat="server"  value='<%#Bind("[Principal_Approval_Status]") %>' />
                                             <asp:HiddenField ID="HiddenField3" runat="server"  value='<%#Bind("[Phd_Office_Approval_Status]") %>' />
                                             <asp:HiddenField ID="HiddenField4" runat="server"  value='<%#Bind("[Guide_Approval]") %>' />
                                             <asp:HiddenField ID="HiddenField5" runat="server"  value='<%#Bind("[Principal_Approval]") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                    <%-- <asp:BoundField DataField="Time From" HeaderText="In Time" />
                            <asp:BoundField DataField="Time To" HeaderText="Out Time" />
                             <asp:BoundField DataField="WorkingHour" HeaderText="Working Hour" />
                            <asp:BoundField DataField="LateBy" HeaderText="Late BY" />
                            <asp:BoundField DataField="EarlyBy" HeaderText="Early BY" />
                              <asp:BoundField DataField="TBU" HeaderText="Buffer Utilization" />
                              <asp:BoundField DataField="RB" HeaderText="Remaining Buffer" />--%>
                                    <asp:TemplateField HeaderText="Research Activity" >
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtResearch" runat="server" oncopy="return false" onpaste="return false" Text='<%#Bind("[Research_Actuvity]") %>' TextMode="MultiLine"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fellowship Activity">
                                        <ItemTemplate>
                                             <asp:TextBox ID="txtFellow" runat="server" oncopy="return false" onpaste="return false" Text='<%#Bind("[FellowShip_Activity]") %>'  TextMode="MultiLine"></asp:TextBox>
                                           
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("Status") %>'></asp:Label>
                                            <asp:HiddenField ID="hdfFormStatus" runat="server" Value='<%#Bind("FormStatus") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:Button ID="BtnSave" runat="server" Text="Save" ForeColor="White" OnClick="BtnSave_Click"  CssClass="btn" BackColor="#ff9900" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Approval Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblApprovalStatus" runat="server"  ForeColor="Black" Text='<%#Bind("[GFormStatus]") %>'  /><br />
                                            <asp:Label ID="Label1" runat="server"  ForeColor="Black" Text='<%#Bind("[PFormStatus]") %>'    /><br />
                                            <asp:Label ID="Label2" runat="server"  ForeColor="Black" Text='<%#Bind("[PhdFormStatus]") %>'   /><br />
                                            <asp:Label ID="Label4" runat="server"  ForeColor="Black" Text='<%#Bind("[RFormStatus]") %>'    />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    There is no record found
                                </EmptyDataTemplate>
                                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                <HeaderStyle HorizontalAlign="Center" Height="20px" BackColor="#ff9900" ForeColor="White" />
                                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                <RowStyle BackColor="White" ForeColor="#330099" HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                <SortedDescendingHeaderStyle BackColor="#7E0000" />
                            </asp:GridView>
                        </td>
                        <td style="width: 10px"></td>
                    </tr>
                </table>


            </td>
        </tr>

      
    </table>
</asp:Content>

