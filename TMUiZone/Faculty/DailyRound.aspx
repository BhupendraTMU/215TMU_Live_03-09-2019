<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="DailyRound.aspx.cs" EnableEventValidation="false" Inherits="Faculty_DailyRound" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"> 
     
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/select2@4.0.13/dist/css/select2.min.css" />
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript" src="https://cdn.jsdelivr.net/npm/select2@4.0.13/dist/js/select2.min.js"></script>
<script type="text/javascript">
    $(function () {
        $("[id*=drpwardassistantname]").select2();
        //$("[id*=drpfloorname]").select2();
        //$("[id*=drpshift]").select2();
    });
</script>
 


     <%--    <script type="text/javascript" src="dropdowneditable/jquery.min.js"></script>
    <script type="text/javascript" src="dropdowneditable/jquery.searchabledropdown-1.0.8.min.js"></script>--%>

   
    <%--<script type="text/javascript" >
        $(document).ready(function () {
            $("select").searchable();
        });
    </script> --%>
  
}
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server" Text="WARD ASSISTANT'S SUPERVISOR DAILY ROUND" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
    </fieldset>
    <br />
    <div id="divGeneralBody">
        <fieldset class="boxBodyInner">
            <div class="form-horizontal">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">

                            <div class="form-group">
                                <div class="col-md-2" >
                                    <label style="width: 200px">Supervisor Name.</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtsupervisor" runat="server" Enabled="false"  CssClass="form-control"></asp:TextBox>
                                </div>

                                <div class="col-md-2">
                                    <label>Ward Assistant Name.</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="drpwardassistantname" runat="server" AutoPostBack="true" BorderColor="Black" OnSelectedIndexChanged="drpwardassistantname_SelectedIndexChanged2"  CssClass="form-control"></asp:DropDownList>
                                    <asp:Label ID="lblwardassistant" runat="server" Text="Ward Assistant." Font-Bold="true" Font-Underline="true" Visible="false"></asp:Label>
                                    <asp:TextBox ID="txtwardassistant" runat="server" BorderColor="Black" CssClass="form-control" Visible="false"></asp:TextBox>

                                </div>
                            </div>


                            <div class="form-group">
                                <div class="col-md-2" >
                                    <label style="width: 200px">Floor Name.</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="drpfloorname" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpfloorname_SelectedIndexChanged"></asp:DropDownList>
                                </div>

                                <div class="col-md-2">
                                    <label>Ward Name.</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="drpwardname" runat="server"  CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>


                            <div class="form-group">
                                <div class="col-md-2">
                                    <label style="width: 200px">Shift.</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="drpshift" runat="server"  CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="drpshift_SelectedIndexChanged">
                                        <asp:ListItem Text="---SELECT---" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Day" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Night" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="P2" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="M3" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="G6" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="G2" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="M6" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="N5" Value="8"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-2" >
                                    <label>Shift Timing.</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="drpshifttiming" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-2">
                                    <label>Status.</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="DropDownList1" runat="server"  CssClass="form-control">
                                        <asp:ListItem Text="---SELECT---" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="InActive" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-2" >
                                    <label>Complain.</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="true"  CssClass="form-control" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                        <asp:ListItem Text="--- SELECT---" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="WITHOUT UNIFORM" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="SLEEPING" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="ABSENT" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="USE OF MOBILE PHONE" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="MISCONDUCT" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="NO COMPLAIN" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="OTHER" Value="7"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-md-2">
                                    <label>Round Time.</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="drproundtime" runat="server"  AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="drproundtime_SelectedIndexChanged">
                                        <asp:ListItem Text="---SELECT---" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="08:00AM" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="10:00AM" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="12:00PM" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="02:00PM" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="04:00PM" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="06:00PM" Value="6"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-2" >
                                    <label style="line-height: 25px" runat="server" id="other" visible="false">Others </label>
                                </div>
                                <div class="col-md-4" runat="server" id="OtherTD">
                                    <asp:TextBox ID="txtother" runat="server" Visible="false" CssClass="form-control" BorderColor="Black">
                                         
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtother" ValidationGroup="g1" ErrorMessage="Plaese Fill Other Complain" runat="Server"></asp:RequiredFieldValidator>

                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-md-2">
                                    <label>Moment.</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="drpmoment" runat="server"  AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="drpmoment_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-2">
                                  
                                    <asp:Label ID="lblTime" runat="server" Text="In Time." Visible="false"></asp:Label>
                                </div>



                                <div class="col-md-4" >
                                    <asp:TextBox ID="txtintime" runat="server" TextMode="DateTimeLocal"  Visible="false" CssClass="form-control">
                                         
                                    </asp:TextBox>
                                    <asp:TextBox ID="txtouttime" runat="server" TextMode="DateTimeLocal" Visible="false" CssClass="form-control">
                                    </asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">

                                <div class="col-md-2">
                                    <asp:Label ID="lblPlace" runat="server" Text="Place." Visible="false"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtplace" runat="server" Visible="false" CssClass="form-control">
                                         
                                    </asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-4">
                                </div>
                            </div>

                        <div class="form-group">
                            <div class="col-md-2">
                                <label>Upload Photo.</label>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group" >
                                    <div class="input-group" >
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark" ></span></span>
                                        <asp:FileUpload ID="txtPhotoUploader"  placeholder="Photo" runat="server" CssClass="form-control" style="width:346px" Height="34px"></asp:FileUpload>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="form-group">
                                <div class="col-md-2" style="width: 200px">
                                    <label style="width: 200px"></label>
                                </div>
                                <div class="col-md-4" style="visibility: hidden">
                                    <asp:TextBox ID="txtID" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
                </div>
        </fieldset>
        
    </div>
    <div>
         <table>
        <tr>
            <td width="1140px" height="15px"></td>
        </tr>
        <tr class="pull-right">
            <td>
                <asp:Button ID="btnSave" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" Height="30px" Width="90px" Text="Save" OnClick="btnSave_Click" />
            </td>
            <td>
                <asp:Button runat="server" CssClass="btn-sm btn-primary btn-block" Text="Export to Excel" Height="30px" Width="120px" ID="btnexporttoexel" OnClick="btnexporttoexel_Click" />
            </td>

        </tr>
    </table>

    </div>
   
    <br />
    <div style="height: 300px; width: 1190px; overflow: scroll;">
        <asp:GridView ID="grdroundreport" runat="server" DataKeyNames="ID" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
            BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
            GridLines="Horizontal" EmptyDataText="There are no data records to display."
            AllowSorting="true">

            <AlternatingRowStyle BackColor="#F7F7F7" />
            <Columns>

                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>

                        <asp:LinkButton ID="btnselect" runat="server" Text="Select" Width="80px" OnClick="btnselect_Click" />
                    </ItemTemplate>

                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sl. No.">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Supervisor Name">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="130px" Text='<%# Eval("Supervisor_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Supervisor Code">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="130px" Text='<%# Eval("Supervisor_Code") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />

                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ward Assistant Name">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="170px" Text='<%# Eval("Ward_Assistant_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Temp. Ward Assistant Name">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="170px" Text='<%# Eval("Temp_Ward_Assistant_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Floor Name">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="200px" Text='<%# Eval("Floor_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Ward Name">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="130px" Text='<%# Eval("Ward_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Shift">
                    <ItemTemplate>

                        <asp:Label runat="server" Width="130px" Text='<%# Eval("Shift") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Shift Timing">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="200px" Text='<%# Eval("Shift_Timing") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Round Time-1">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="130px" Text='<%# Eval("Round_Time1") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Out Time Round1">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Out_Time1") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="In Time Round1">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("In_Time1") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Place Round1">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Place1") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Transaction Date Round1">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Trans_Date1") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Round Time-2">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="130px" Text='<%# Eval("Round_Time2") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Out Time Round2">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Out_Time2") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="In Time Round2">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("In_Time2") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Place Round2">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Place2") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Transaction Date Round2">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Trans_Date2") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Round Time-3">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="130px" Text='<%# Eval("Round_Time3") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Out Time Round3">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Out_Time3") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="In Time Round3">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("In_Time3") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Place Round 3">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Place3") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Transaction Date Round3">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Trans_Date3") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Round Time-4">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="130px" Text='<%# Eval("Round_Time4") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Out Time Round4">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Out_Time4") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="In Time Round4">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("In_Time4") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Place Round4">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Place4") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Transaction Date Round4">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Trans_Date4") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Round Time-5">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="130px" Text='<%# Eval("Round_Time5") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Out Time Round5">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Out_Time5") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="In Time Round5">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("In_Time5") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Place Round5">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Place5") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Transaction Date Round5">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Trans_Date5") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Round Time-6">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="130px" Text='<%# Eval("Round_Time6") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Out Time Round6">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Out_Time6") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="In Time Round6">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("In_Time6") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Place Round6">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Place6") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Transaction Date Round6">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Trans_Date6") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="130px" Text='<%# Eval("Status") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Complain">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="130px" Text='<%# Eval("Complain") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Other Complain">
                    <ItemTemplate>
                        <asp:Label runat="server" Width="150px" Text='<%# Eval("Other_Complain") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Option to Download Photo">
                    <ItemTemplate>
                        <%--   <asp:Label runat="server" Width="100px" Text='<%# Eval("Upload_Photo") %>'></asp:Label>--%>
                        <asp:LinkButton ID="lnkPhoto" runat="server" ForeColor="Red" Font-Underline="true" Enabled='<%# Eval("Upload_Photo").ToString() == "0" ? false : true %>' OnClick="lnkPhoto_Click"> View Photo</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
            <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
            <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />
            <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
            <SortedAscendingCellStyle BackColor="#F4F4FD" />
            <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
            <SortedDescendingCellStyle BackColor="#D8D8F0" />
            <SortedDescendingHeaderStyle BackColor="#3E3277" />

        </asp:GridView>
    </div>
</asp:Content>

