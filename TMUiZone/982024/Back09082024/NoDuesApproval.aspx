<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="NoDuesApproval.aspx.cs" Inherits="Faculty_NoDuesApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style2 {
            width: 495px;
            height: 114px;
        }

        .auto-style3 {
            width: 132px;
            height: 114px;
        }

        .auto-style4 {
            height: 114px;
        }

        .auto-style5 {
            height: 171px;
        }

        .auto-style7 {
            width: 132px;
        }

        .auto-style8 {
            width: 495px;
        }

        .auto-style11 {
            height: 53px;
        }

        .auto-style12 {
            width: 136px;
        }

        .auto-style13 {
            width: 629px;
        }

        .auto-style14 {
            width: 99px;
        }

        .auto-style15 {
            height: 16px;
        }

        .auto-style16 {
            height: 137px;
        }

        .auto-style17 {
            height: 26px;
        }

        .auto-style18 {
            height: 55px;
            width: 1123px;
        }

        .auto-style19 {
            height: 119px;
            width: 1481px;
        }

        .auto-style21 {
            height: 60px;
        }

        .auto-style22 {
            height: 37px;
        }

        .auto-style23 {
            height: 83px;
        }

        .auto-style24 {
            width: 100px;
        }

        .auto-style25 {
            width: 613px;
        }

        .auto-style26 {
            width: 643px;
        }

        .auto-style29 {
            width: 83px;
        }

        .auto-style35 {
            width: 154px;
        }

        .auto-style36 {
            width: 106px;
            height: 26px;
        }

        .auto-style37 {
            width: 153px;
        }

        .auto-style39 {
            width: 102px;
            height: 26px;
        }

        .auto-style40 {
            width: 30px;
            height: 26px;
        }

        .auto-style42 {
            width: 106px;
        }

        .auto-style43 {
            width: 30px;
        }

        .auto-style44 {
            width: 102px;
        }

        .auto-style45 {
            width: 194px;
        }

        .auto-style46 {
            width: 121px;
        }

        .auto-style47 {
            width: 146px;
        }

        .auto-style49 {
            width: 44px;
        }

        .auto-style50 {
            width: 126px;
        }

        .auto-style51 {
            width: 137px;
        }

        .auto-style52 {
            width: 145px;
        }

        .auto-style53 {
            width: 149px;
        }
    </style>

    <script type="text/javascript">
        function PrintDiv() {
            var divToPrint = document.getElementById('printarea');
            var popupWin = window.open('', '_blank', 'width=80%,height=950px,location=no,left=200px');
            popupWin.document.open();
            popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
            popupWin.document.close();
        }

    </script>

    </system.net>

   <%-- <script type="text/javascript">
        function PrintGridData() {
            var prtGrid = document.getElementById('<%=pnlGridViewdata.ClientID %>');
        prtGrid.border = 0;
        var prtwin = window.open('', 'PrintGridViewData', 'left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1, status=0,resizable=1');
        prtwin.document.write(prtGrid.outerHTML);
        prtwin.document.close();
        prtwin.focus();
        prtwin.print();
        prtwin.close();
    }
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <fieldset class="boxBodyInner">
        <br />
        <asp:GridView ID="grdnodueslist" runat="server" DataKeyNames="Employee_Code" AlternatingRowStyle-CssClass="danger" PageSize="50" AllowPaging="true" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" Visible="true" OnPageIndexChanging="grdnodueslist_PageIndexChanging1">
            <PagerSettings Mode="NumericFirstLast" />
            <PagerStyle CssClass="csspager" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <%# Container.DataItemIndex +1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Employee Code" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblemployeecode" runat="server" Text='<%# Bind("[Employee_Code]") %>'></asp:Label>
                        <asp:HiddenField ID="Hfemployeecode" Value='<%# Eval("[Employee_Code]") %>' runat="server" />
                        <asp:HiddenField ID="Hfhodname" Value='<%# Eval("[Employee_Code]") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="View" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkbutton" runat="server" OnClick="lnkbutton_Click">View</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Employee Name" ItemStyle-Width="4%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                    <ItemTemplate>
                        <asp:Label ID="lblemployeename" runat="server" Text='<%# Eval("Name_Of_Employee") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="Designation" ItemStyle-Width="3%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                    <ItemTemplate>
                        <asp:Label ID="lbldesignation" runat="server" Text='<%# Eval("Designation") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <%--  <asp:TemplateField HeaderText="Date Of Joining" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lbldateofjoining" runat="server" Text='<%#Eval("Date Of Joining") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Resignation Date" ItemStyle-Width="2%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblresignationdate" runat="server" Text='<%# Eval("Proposed_Date_of_Relieving") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Father's Name" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblRemark" runat="server" Text='<%#Eval("Father_Name") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="Hod_Status" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblhodstatus" runat="server" Text='<%#Eval("Hod_Status") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>

                <%-- <asp:TemplateField HeaderText="HR_Status" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblhrstatus" runat="server" Text='<%#Eval("Hr_Status") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>

                <%-- <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" HeaderText="Select" ItemStyle-CssClass="text-center">
                    
                    <ItemTemplate>
                        <asp:CheckBox ID="Chkemployee" Enabled='<%# Eval("txtMarksEnableDesable").ToString().Equals("true") %>'  runat="server" AutoPostBack="true"  />
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
            <EmptyDataTemplate>No Record To Display</EmptyDataTemplate>
        </asp:GridView>
    </fieldset>

    <asp:Panel ID="pnlGridViewdata" CssClass="modalPopup" Width="65%" runat="server" ScrollBars="Vertical" Height="950px" Style="display: none;">

        <fieldset class="boxBody" style="text-align: center; border-color: black; background-color: black;">
            <asp:Label ID="Label1" runat="server" Text="No Dues" Font-Size="15pt" ForeColor="White" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
            <div class="close">
                <asp:Button ID="btnclose" OnClick="btnclose_Click" runat="server" Text="X" ForeColor="Red" BackColor="White" />
                <asp:Button ID="BtnPrint" OnClientClick="PrintDiv();" runat="server" BackColor="#ff9900" Text="Print" Font-Bold="true" Height="30px" Width="100px" />

            </div>
        </fieldset>
        <br />
        <div id="printarea">
            <fieldset class="boxBody" style="border-color: black">
                <table cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td>
                            <label style="line-height: 25px">Date of Resignation: </label>
                        </td>
                        <td style="width: 20px"></td>
                        <td>
                            <asp:TextBox ID="txtdateofrelieving" runat="server" BorderColor="Black" Width="200px"></asp:TextBox>
                            <%--  <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtdateofrelieving" Format="dd MMM yyyy"></asp:CalendarExtender>

                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdateofrelieving" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>--%>
                <asp:HiddenField ID="hdfDesignation" runat="server" />         
 </td>

                        <td style="width: 10px"></td>
                        <td>
                            <label style="line-height: 25px">Date Of Leaving:</label>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:TextBox ID="txtdateofleaving" runat="server" BorderColor="Black" Width="200px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtdateofleaving" Format="dd MMM yyyy"></asp:CalendarExtender>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtdateofleaving" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                        </td>


                    </tr>
                </table>
            </fieldset>
            <br />
            <fieldset class="boxBody" style="border-color: black">
                <table cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td>
                            <label style="line-height: 25px">Employee Code: </label>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:TextBox ID="txtemployeecode" runat="server" Width="200px" Enabled="false" BorderColor="Black"></asp:TextBox>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <label style="line-height: 25px">Name Of Employee: </label>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:TextBox ID="txtnameofemployee" runat="server" Enabled="false" Width="200px" BorderColor="Black"></asp:TextBox>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <label style="line-height: 25px">Father's Name:</label>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:TextBox ID="txtfathername" runat="server" Enabled="false" Width="200px" BorderColor="Black"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="11" style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td>
                            <label style="line-height: 25px">College/Department/Section: </label>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:TextBox ID="txtcollegedeptsection" runat="server" Enabled="false" Width="200px" BorderColor="Black"></asp:TextBox>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <label style="line-height: 25px">Branch: </label>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:TextBox ID="txtbranch" runat="server" Enabled="false" Width="200px" BorderColor="Black"></asp:TextBox>
                        </td>

                        <td style="width: 10px"></td>
                        <td>
                            <label style="line-height: 25px">Date Of Joining:</label>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:TextBox ID="txtdateofjoining" runat="server" Enabled="false" BorderColor="Black" Width="200px" onkeydown="return false;" autocomplete="off" oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" AutoPostBack="True"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtdateofjoining" Format="dd MMM yyyy"></asp:CalendarExtender>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtdateofjoining" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                        </td>
                    </tr>


                    <tr>
                        <td colspan="11" style="height: 10px"></td>
                    </tr>

                    <tr>


                        <td>
                            <label style="line-height: 25px">Tel./Mobile No.: </label>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:TextBox ID="txtmobileno" runat="server" Enabled="false" Width="200px" BorderColor="Black"></asp:TextBox>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <label style="line-height: 25px">E-mail ID: </label>
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:TextBox ID="txtemailid" runat="server" Enabled="false" Width="200px" BorderColor="Black"></asp:TextBox>
                        </td>

                    </tr>

                </table>


            </fieldset>

            
            <asp:UpdatePanel ID="pnlpic" runat="server">
                <ContentTemplate>
                    <fieldset id="Fldelegible" class="boxBody" style="text-align: center; border-color: black;" runat="server" visible="true">
                        <div class="col-md-12">
                            <div class="form-group">
                                <div class="col-md-5">
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btn_Hrstatus" runat="server" Text="Save" Width="100px" Height="30px" OnClick="btn_Hrstatus_Click" Visible="false" CssClass="form-control" />
                                </div>

                            </div>
                        </div>
                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
            <div id="divNoduesalldept" runat="server" visible="false">


                <fieldset class="boxBody" style="border-color: black">
                    <asp:Label ID="Label2" runat="server" Text="Certified that nothing is outstanding against the employee as mentioned above:" Font-Size="15pt" ForeColor="Black" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                </fieldset>
                <br />
                <asp:Table class="boxBody" Width="1080px" Style="border: 1px solid" runat="server">
                    <asp:TableRow>
                        <asp:TableHeaderCell Style="border: 1px solid">
                            <asp:Label ID="Label7" runat="server" Text="S.NO."></asp:Label>
                        </asp:TableHeaderCell>

                        <asp:TableHeaderCell Style="border: 1px solid">
                            <asp:Label ID="Label8" runat="server" Text="PARTICULARS"></asp:Label>
                        </asp:TableHeaderCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="Label9" runat="server" Text="NO DUES GRANTED"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="Label10" runat="server" Text="NAME"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="Label3" runat="server" Text="DESIGNATION"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="Label4" runat="server" Text="REMARK"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="Label31" runat="server" Text="ACTION"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="border: 1px solid">1.</asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="LabelR1" runat="server" Text="Department Library"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:DropDownList ID="DropDownList1" runat="server" Enabled="false" Height="28px">
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Granted" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Not Granted" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Pending At Department" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Contact To Department" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lbldepartmentlib" runat="server" Enabled="false" Text=""></asp:Label>
                            <asp:TextBox ID="txtdepartmentlibemployeecode" runat="server" Visible="false"></asp:TextBox>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lbldepartmentlibdeg" runat="server" Enabled="false" Text=""></asp:Label>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:TextBox ID="txtremarkdeptlibrary" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Button ID="btn_submit1" runat="server" Enabled="false" Text="Submit" Height="25px" Width="80px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="btn_submit1_Click" />
                            <br />
                            <br />
                            <asp:Button ID="btnrejdeptlibar" runat="server" Text="Reject" Height="25px" Width="80px" Enabled="false" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="btnrejdeptlibar_Click" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <div>
                                <asp:TextBox ID="txtdepartmentlibID" runat="server" Width="50px" Visible="false"></asp:TextBox>
                            </div>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="border: 1px solid">2.</asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="Label5" runat="server" Text="Central Library"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:TableCell Style="border: 1px solid">
                                <asp:DropDownList ID="DropDownList2" runat="server" Enabled="false" Height="28px">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Granted" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Not Granted" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Pending At Department" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Contact To Department" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">

                            <asp:Label ID="lblcentlibname" runat="server" Enabled="false" Text=""></asp:Label>
                            <asp:TextBox ID="lblcentlibnameemployeecode" runat="server" Visible="false"></asp:TextBox>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">

                            <asp:Label ID="lblcentlibdeg" runat="server" Enabled="false" Text=""></asp:Label>

                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:TextBox ID="txtremarkcentrallib" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Button ID="Button2" runat="server" Enabled="false" Text="Submit" Height="25px" Width="80px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="Button2_Click" />
                            <br />
                            <br />
                            <asp:Button ID="btnrejectcentrallib" runat="server" Text="Reject" Height="25px" Width="80px" Enabled="false" OnClick="btnrejectcentrallib_Click" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <div>
                                <asp:TextBox ID="txtCentrallibID" runat="server" Width="50px" Visible="false"></asp:TextBox>
                            </div>
                        </asp:TableCell>
                    </asp:TableRow>
                    <%--<asp:TableRow>
        <asp:TableCell style="border: 1px solid">3.</asp:TableCell>
            <asp:TableCell style="border:1px solid">
                <asp:Label ID="Label6" runat="server" Text="Book Bank"></asp:Label>
</asp:TableCell>
            <asp:TableCell style="border:1px solid">
                <asp:TableCell style="border:1px solid">
                <asp:DropDownList ID="DropDownList3" runat="server" Enabled="false" Height="28px">
                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Granted" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Not Granted" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Pending At Department" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Contact To Department" Value="4"></asp:ListItem>
                </asp:DropDownList>
            </asp:TableCell>
            </asp:TableCell>
        
            <asp:TableCell style="border:1px solid">
                <asp:Label ID="lblbookbankname" runat="server" Enabled="false" Text=""></asp:Label>
                <asp:TextBox ID="lblbookbanknamecode" runat="server" Visible="false"></asp:TextBox>
            </asp:TableCell>
            
            <asp:TableCell style="border:1px solid">
                 <asp:Label ID="lblbookbankdeg" runat="server" Enabled="false" Text=""></asp:Label>
            </asp:TableCell>
            
            <asp:TableCell style="border:1px solid">
                <asp:TextBox ID="txtremarkbookbank" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
            </asp:TableCell>
              <asp:TableCell style="border:1px solid">
                  <asp:Button ID="Button3" runat="server" Enabled="false" Text="Submit" Width="80px"  Height="25px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="Button3_Click" />
              <br />
                 <br />
                  <asp:Button ID="btnrejbook" runat="server" Text="Reject" Height="25px" Width="80px"  Enabled="false" OnClick="btnrejbook_Click" BackColor="#ff9900" ForeColor="Black" BorderColor="Black"/>  
              </asp:TableCell>
                  <asp:TableCell>
                 <div>
                        <asp:TextBox ID="txtBookID" runat="server" Width="50px" Visible="false"></asp:TextBox>
               </div>
                </asp:TableCell>
            </asp:TableRow>--%>
                    <asp:TableRow>
                        <asp:TableCell Style="border: 1px solid">3.</asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="Label11" runat="server" Text="Seed Money"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:TableCell Style="border: 1px solid">
                                <asp:DropDownList ID="DropDownList4" runat="server" Enabled="false" Height="28px">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Granted" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Not Granted" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Pending At Department" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Contact To Department" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lblseedmoneyname" runat="server" Enabled="false" Text=""></asp:Label>
                            <asp:TextBox ID="lblseedmoneynamecode" runat="server" Visible="false"></asp:TextBox>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lblseedmoneydeg" runat="server" Enabled="false" Text=""></asp:Label>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:TextBox ID="txtremarkseedmoney" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Button ID="Button4" runat="server" Enabled="false" Text="Submit" Height="25px" Width="80px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="Button4_Click" />
                            <br />
                            <br />
                            <asp:Button ID="txtrejseedmoney" runat="server" Text="Reject" Height="25px" Width="80px" Enabled="false" OnClick="txtrejseedmoney_Click" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <div>
                                <asp:TextBox ID="txtseedmoneyID" runat="server" Width="50px" Visible="false"></asp:TextBox>
                            </div>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="border: 1px solid">4.</asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="Label12" runat="server" Text="College/Department Store"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:TableCell Style="border: 1px solid">
                                <asp:DropDownList ID="DropDownList5" runat="server" Enabled="false" Height="28px">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Granted" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Not Granted" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Pending At Department" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Contact To Department" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lbldeptstorename" runat="server" Enabled="false" Text=""></asp:Label>
                            <asp:TextBox ID="lbldeptstorenamecode" runat="server" Visible="false"></asp:TextBox>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lbldeptstoredeg" runat="server" Enabled="false" Text=""></asp:Label>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:TextBox ID="txtremarkdepartmentstore" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Button ID="Button5" runat="server" Enabled="false" Text="Submit" Height="25px" Width="80px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="Button5_Click" />
                            <br />
                            <br />
                            <asp:Button ID="btnrejdepartmentstore" runat="server" Text="Reject" Height="25px" Width="80px" Enabled="false" OnClick="btnrejdepartmentstore_Click" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <div>
                                <asp:TextBox ID="txtdepartmentStoreID" runat="server" Width="50px" Visible="false"></asp:TextBox>
                            </div>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="border: 1px solid">5.</asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="Label13" runat="server" Text="Central Store (Furniture or any other equipment etc.)"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:TableCell Style="border: 1px solid">
                                <asp:DropDownList ID="DropDownList6" runat="server" Enabled="false" Height="28px">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Granted" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Not Granted" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Pending At Department" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Contact To Department" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lblcentralstorename" runat="server" Enabled="false" Text=""></asp:Label>
                            <asp:TextBox ID="lblcentralstorenamecode" runat="server" Visible="false"></asp:TextBox>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lblcentralstoredeg" runat="server" Enabled="false" Text=""></asp:Label>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:TextBox ID="txtremarkcentralstore" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Button ID="Button6" runat="server" Enabled="false" Text="Submit" Height="25px" Width="80px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="Button6_Click" />
                            <br />
                            <br />
                            <asp:Button ID="btnrejcentralstore" runat="server" Text="Reject" Height="25px" Width="80px" Enabled="false" OnClick="btnrejcentralstore_Click" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <div>
                                <asp:TextBox ID="txtCentralstoreID" runat="server" Width="50px" Visible="false"></asp:TextBox>
                            </div>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="border: 1px solid">6.</asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="Label14" runat="server" Text="College/Department Laboratory"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:TableCell Style="border: 1px solid">
                                <asp:DropDownList ID="DropDownList7" runat="server" Enabled="false" Height="28px">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Granted" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Not Granted" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Pending At Department" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Contact To Department" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lblcollegedeptname" runat="server" Enabled="false" Text=""></asp:Label>
                            <asp:TextBox ID="lblcollegedeptnamecode" runat="server" Visible="false"></asp:TextBox>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lblcollegedeptdeg" runat="server" Enabled="false" Text=""></asp:Label>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:TextBox ID="txtremarkcollegedeptre" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Button ID="txtremarkdepartmentlaborat" runat="server" Enabled="false" Text="Submit" Width="80px" Height="25px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="txtremarkdepartmentlaborat_Click" />
                            <br />
                            <br />
                            <asp:Button ID="btnrejlaborate" runat="server" Text="Reject" Height="25px" Enabled="false" Width="80px" OnClick="btnrejlaborate_Click" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <div>
                                <asp:TextBox ID="txtlaborateID" runat="server" Width="50px" Visible="false"></asp:TextBox>
                            </div>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="border: 1px solid">7.</asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="Label15" runat="server" Text="College/Department Workshop"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:TableCell Style="border: 1px solid">
                                <asp:DropDownList ID="DropDownList8" runat="server" Enabled="false" Height="28px">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Granted" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Not Granted" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Pending At Department" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Contact To Department" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lblcollegeworkname" runat="server" Enabled="false" Text=""></asp:Label>
                            <asp:TextBox ID="lblcollegeworknamecode" runat="server" Visible="false"></asp:TextBox>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lblcollegeworkdeg" runat="server" Enabled="false" Text=""></asp:Label>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:TextBox ID="txtremarkdepartmentwork" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Button ID="Button8" runat="server" Enabled="false" Text="Submit" Height="25px" Width="80px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="Button8_Click" />
                            <br />
                            <br />
                            <asp:Button ID="btnrejectdepartment" runat="server" Text="Reject" Height="25px" Width="80px" Enabled="false" OnClick="btnrejectdepartment_Click" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <div>
                                <asp:TextBox ID="txtcollegeID" runat="server" Width="50px" Visible="false"></asp:TextBox>
                            </div>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="border: 1px solid">8.</asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="Label16" runat="server" Text="Hostel Mess"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:TableCell Style="border: 1px solid">
                                <asp:DropDownList ID="DropDownList9" runat="server" Enabled="false" Height="28px">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Granted" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Not Granted" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Pending At Department" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Contact To Department" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lblhostelmessname" runat="server" Enabled="false" Text=""></asp:Label>
                            <asp:TextBox ID="lblhostelmessnamecode" runat="server" Visible="false"></asp:TextBox>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lblhostelmessdeg" runat="server" Enabled="false" Text=""></asp:Label>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:TextBox ID="txtremarkhostelmess" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Button ID="Button9" runat="server" Enabled="false" Text="Submit" Height="25px" Width="80px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="Button9_Click" />
                            <br />
                            <br />
                            <asp:Button ID="btnrejectmess" runat="server" Text="Reject" OnClick="btnrejectmess_Click" Width="80px" Height="25px" Enabled="false" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <div>
                                <asp:TextBox ID="txthostelmessID" runat="server" Width="50px" Visible="false"></asp:TextBox>
                            </div>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="border: 1px solid">9.</asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="Label17" runat="server" Text="Hostel Office"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:TableCell Style="border: 1px solid">
                                <asp:DropDownList ID="DropDownList10" runat="server" Enabled="false" Height="28px">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Granted" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Not Granted" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Pending At Department" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Contact To Department" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lblhosteloffficename" runat="server" Enabled="false" Text=""></asp:Label>
                            <asp:TextBox ID="lblhosteloffficenamecode" runat="server" Visible="false"></asp:TextBox>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lblhosteloffficedeg" runat="server" Enabled="false" Text=""></asp:Label>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:TextBox ID="txtremarkhosteloffice" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Button ID="Button10" runat="server" Enabled="false" Text="Submit" Height="25px" Width="80px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="Button10_Click" />
                            <br />
                            <br />
                            <asp:Button ID="btnrejecthosteloffice" runat="server" Text="Reject" Height="25px" Width="80px" Enabled="false" OnClick="btnrejecthosteloffice_Click" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <div>
                                <asp:TextBox ID="txthostelofficeID" runat="server" Width="50px" Visible="false"></asp:TextBox>
                            </div>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="border: 1px solid">10.</asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="Label18" runat="server" Text="Transport Office"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:TableCell Style="border: 1px solid">
                                <asp:DropDownList ID="DropDownList11" runat="server" Enabled="false" Height="28px">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Granted" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Not Granted" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Pending At Department" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Contact To Department" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lbltransportofficename" runat="server" Enabled="false" Text=""></asp:Label>
                            <asp:TextBox ID="lbltransportofficenamecode" runat="server" Visible="false"></asp:TextBox>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lbltransportofficedeg" runat="server" Enabled="false" Text=""></asp:Label>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:TextBox ID="txtremarktransportoffice" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Button ID="Button11" runat="server" Enabled="false" Text="Submit" Height="25px" Width="80px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="Button11_Click" />
                            <br />
                            <br />
                            <asp:Button ID="btnrejecttransport" runat="server" Text="Reject" Height="25px" Width="80px" Enabled="false" OnClick="btnrejecttransport_Click" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <div>
                                <asp:TextBox ID="txttransportID" runat="server" Width="50px" Visible="false"></asp:TextBox>
                            </div>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="border: 1px solid">11.</asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="Label19" runat="server" Text="Guest House I/c"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:TableCell Style="border: 1px solid">
                                <asp:DropDownList ID="DropDownList12" runat="server" Enabled="false" Height="28px">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Granted" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Not Granted" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Pending At Department" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Contact To Department" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lblguestname" runat="server" Enabled="false" Text=""></asp:Label>
                            <asp:TextBox ID="lblguestnamecode" runat="server" Visible="false"></asp:TextBox>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lblguestdeg" runat="server" Enabled="false" Text=""></asp:Label>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:TextBox ID="txtremarkguesthouseic" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Button ID="Button12" runat="server" Enabled="false" Text="Submit" Height="25px" Width="80px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="Button12_Click" />
                            <br />
                            <br />
                            <asp:Button ID="btnrejcetgesthouse" runat="server" Text="Reject" Height="25px" Width="80px" Enabled="false" OnClick="btnrejcetgesthouse_Click" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <div>
                                <asp:TextBox ID="txtguesthouseID" runat="server" Width="50px" Visible="false"></asp:TextBox>
                            </div>
                        </asp:TableCell>

                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="border: 1px solid">12.</asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="Label20" runat="server" Text="Faculty House I/c"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:TableCell Style="border: 1px solid">
                                <asp:DropDownList ID="DropDownList13" runat="server" Enabled="false" Height="28px">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Granted" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Not Granted" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Pending At Department" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Contact To Department" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lblfacultyname" runat="server" Enabled="false" Text=""></asp:Label>
                            <asp:TextBox ID="lblfacultynamecode" runat="server" Visible="false"></asp:TextBox>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lblfacultydeg" runat="server" Enabled="false" Text=""></asp:Label>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:TextBox ID="txtremarkfacultyhose" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Button ID="Button13" runat="server" Enabled="false" Text="Submit" Height="25px" BackColor="#ff9900" Width="80px" ForeColor="Black" BorderColor="Black" OnClick="Button13_Click" />
                            <br />
                            <br />
                            <asp:Button ID="btnrejectfaculthou" runat="server" Text="Reject" Height="25px" Enabled="false" Width="80px" OnClick="btnrejectfaculthou_Click" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <div>
                                <asp:TextBox ID="txtfacultyhouseID" runat="server" Width="50px" Visible="false"></asp:TextBox>
                            </div>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="border: 1px solid">13.</asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="Label21" runat="server" Text="Sport I/c"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:TableCell Style="border: 1px solid">
                                <asp:DropDownList ID="DropDownList14" runat="server" Enabled="false" Height="28px">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Granted" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Not Granted" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Pending At Department" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Contact To Department" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lblsportname" runat="server" Enabled="false" Text=""></asp:Label>
                            <asp:TextBox ID="lblsportnamecode" runat="server" Visible="false"></asp:TextBox>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lblsportdeg" runat="server" Enabled="false" Text=""></asp:Label>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:TextBox ID="txtremarksportic" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Button ID="Button14" runat="server" Enabled="false" Text="Submit" Height="25px" BackColor="#ff9900" Width="80px" ForeColor="Black" BorderColor="Black" OnClick="Button14_Click" />
                            <br />
                            <br />
                            <asp:Button ID="btnrejsport" runat="server" Text="Reject" Height="25px" Enabled="false" Width="80px" OnClick="btnrejsport_Click" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <div>
                                <asp:TextBox ID="txtsportID" runat="server" Width="50px" Visible="false"></asp:TextBox>
                            </div>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="border: 1px solid">14.</asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="Label22" runat="server" Text="Medical Store"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:TableCell Style="border: 1px solid">
                                <asp:DropDownList ID="DropDownList15" runat="server" Enabled="false" Height="28px">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Granted" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Not Granted" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Pending At Department" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Contact To Department" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lblmedicalname" runat="server" Enabled="false" Text=""></asp:Label>
                            <asp:TextBox ID="lblmedicalnamecode" runat="server" Visible="false"></asp:TextBox>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lblmedicaldeg" runat="server" Enabled="false" Text=""></asp:Label>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:TextBox ID="txtremarkmedicalstore" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Button ID="Button15" runat="server" Enabled="false" Text="Submit" Height="25px" BackColor="#ff9900" Width="80px" ForeColor="Black" BorderColor="Black" OnClick="Button15_Click" />
                            <br />
                            <br />
                            <asp:Button ID="btnrejmedicalstore" runat="server" Text="Reject" Height="25px" Enabled="false" Width="80px" OnClick="btnrejmedicalstore_Click" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <div>
                                <asp:TextBox ID="txtmedicalstoreID" runat="server" Width="50px" Visible="false"></asp:TextBox>
                            </div>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="border: 1px solid">15.</asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="Label23" runat="server" Text="Electricty Department"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:TableCell Style="border: 1px solid">
                                <asp:DropDownList ID="DropDownList16" runat="server" Enabled="false" Height="28px">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Granted" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Not Granted" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Pending At Department" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Contact To Department" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lblelectricityname" runat="server" Enabled="false" Text=""></asp:Label>
                            <asp:TextBox ID="lblelectricitynamecode" runat="server" Visible="false"></asp:TextBox>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lblelectricitydeg" runat="server" Enabled="false" Text=""></asp:Label>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:TextBox ID="txtremarkelectrictydepart" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Button ID="Button16" runat="server" Enabled="false" Text="Submit" Height="25px" BackColor="#ff9900" Width="80px" ForeColor="Black" BorderColor="Black" OnClick="Button16_Click" />
                            <br />
                            <br />
                            <asp:Button ID="btnrejelectric" runat="server" Text="Reject" Height="25px" Enabled="false" Width="80px" OnClick="btnrejelectric_Click" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <div>
                                <asp:TextBox ID="txtelectricsID" runat="server" Width="50px" Visible="false"></asp:TextBox>
                            </div>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="border: 1px solid">16.</asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="Label24" runat="server" Text="I/Card Office (for surrendering I/Card)"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:TableCell Style="border: 1px solid">
                                <asp:DropDownList ID="DropDownList17" runat="server" Enabled="false" Height="28px">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Granted" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Not Granted" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Pending At Department" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Contact To Department" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lblicardofficename" runat="server" Enabled="false" Text=""></asp:Label>
                            <asp:TextBox ID="lblicardofficenamecode" runat="server" Visible="false"></asp:TextBox>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lbliccardofficedeg" runat="server" Enabled="false" Text=""></asp:Label>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:TextBox ID="txtremarkicardoffice" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Button ID="Button17" runat="server" Enabled="false" Text="Submit" Height="25px" BackColor="#ff9900" Width="80px" ForeColor="Black" BorderColor="Black" OnClick="Button17_Click" />
                            <br />
                            <br />
                            <asp:Button ID="btniccardreject" runat="server" Text="Reject" Height="25px" Enabled="false" Width="80px" OnClick="btniccardreject_Click" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <div>
                                <asp:TextBox ID="txtIcardID" runat="server" Width="50px" Visible="false"></asp:TextBox>
                            </div>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="border: 1px solid">17.</asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="Label25" runat="server" Text="Computer Center/IT Dept.(for surrendering Mobile Hand Set/SIM Card/Laptop/Computer/Pen/Drive/Biometrice Attendance Card/Any other equipment)"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:TableCell Style="border: 1px solid">
                                <asp:DropDownList ID="DropDownList18" runat="server" Enabled="false" Height="28px">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Granted" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Not Granted" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Pending At Department" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Contact To Department" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lblitname" runat="server" Enabled="false" Text=""></asp:Label>
                            <asp:TextBox ID="lblitnamecode" runat="server" Visible="false"></asp:TextBox>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lblitdeg" runat="server" Enabled="false" Text=""></asp:Label>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:TextBox ID="txtremarkit" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Button ID="Button18" runat="server" Enabled="false" Text="Submit" Height="25px" Width="80px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="Button18_Click" />
                            <br />
                            <br />
                            <asp:Button ID="btnrejectit" runat="server" Text="Reject" Height="25px" Enabled="false" Width="80px" OnClick="btnrejectit_Click" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <div>
                                <asp:TextBox ID="txtitID" runat="server" Width="50px" Visible="false"></asp:TextBox>
                            </div>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="border: 1px solid">18.</asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="Label26" runat="server" Text="Department Head"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:TableCell Style="border: 1px solid">
                                <asp:DropDownList ID="DropDownList19" runat="server" Enabled="false" Height="28px">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Granted" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Not Granted" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Pending At Department" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Contact To Department" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lbldepartmenthname" runat="server" Enabled="false" Text=""></asp:Label>
                            <asp:TextBox ID="lbldepartmenthnamecode" runat="server" Visible="false"></asp:TextBox>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lbldepartmentdeg" runat="server" Text=""></asp:Label>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:TextBox ID="txtremarkheaddept" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Button ID="Button19" runat="server" Enabled="false" Text="Submit" Height="25px" Width="80px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="Button19_Click" />
                            <br />
                            <br />
                            <asp:Button ID="btnrejdept" runat="server" Text="Reject" Height="25px" Enabled="false" Width="80px" OnClick="btnrejdept_Click" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <div>
                                <asp:TextBox ID="txtheaddeptID" runat="server" Width="50px" Visible="false"></asp:TextBox>
                            </div>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="border: 1px solid">19.</asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="Label27" runat="server" Text="Cash Counter"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:TableCell Style="border: 1px solid">
                                <asp:DropDownList ID="DropDownList20" runat="server" Enabled="false" Height="28px">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Granted" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Not Granted" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Pending At Department" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Contact To Department" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lblcashname" runat="server" Enabled="false" Text=""></asp:Label>
                            <asp:TextBox ID="lblcashnamecode" runat="server" Visible="false"></asp:TextBox>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lblcashdeg" runat="server" Enabled="false" Text=""></asp:Label>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:TextBox ID="txtremarkcashcounter" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Button ID="Button20" runat="server" Text="Submit" Enabled="false" Height="25px" BackColor="#ff9900" Width="80px" ForeColor="Black" BorderColor="Black" OnClick="Button20_Click" />
                            <br />
                            <br />
                            <asp:Button ID="btnrejectcounter" runat="server" Text="Reject" Height="25px" Enabled="false" Width="80px" OnClick="btnrejectcounter_Click" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <div>
                                <asp:TextBox ID="txtCounterID" runat="server" Width="50px" Visible="false"></asp:TextBox>
                            </div>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Style="border: 1px solid">20.</asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="Label28" runat="server" Text="Jt. Director(Security)"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:TableCell Style="border: 1px solid">
                                <asp:DropDownList ID="DropDownList21" runat="server" Enabled="false" Height="28px">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Granted" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Not Granted" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Pending At Department" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Contact To Department" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lbljtdirename" runat="server" Enabled="false" Text=""></asp:Label>
                            <asp:TextBox ID="lbljtdirenamecode" runat="server" Visible="false"></asp:TextBox>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lbljtdirdeg" runat="server" Enabled="false" Text=""></asp:Label>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:TextBox ID="txtremarkjtdirector" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Button ID="Button21" runat="server" Enabled="false" Text="Submit" Height="25px" BackColor="#ff9900" Width="80px" ForeColor="Black" BorderColor="Black" OnClick="Button21_Click" />
                            <br />
                            <br />
                            <asp:Button ID="btnrejectdirector" runat="server" Text="Reject" Height="25px" Enabled="false" Width="80px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="btnrejectdirector_Click" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <div>
                                <asp:TextBox ID="txtID" runat="server" Width="50px" Visible="false"></asp:TextBox>
                            </div>

                        </asp:TableCell>
                    </asp:TableRow>


                    <asp:TableRow>
                        <asp:TableCell Style="border: 1px solid">21.</asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="Label48" runat="server" Enabled="false" Text="Ph.D DEPARTMENT"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:TableCell Style="border: 1px solid">
                                <asp:DropDownList ID="drpphddepartment" runat="server" Enabled="false" Height="28px">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Granted" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Not Granted" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Pending At Department" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Contact To Department" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lblphddepartmentname" runat="server" Enabled="false" Text=""></asp:Label>
                            <asp:TextBox ID="lblphddepartmentnamecode" runat="server" Visible="false"></asp:TextBox>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:Label ID="lblphddepartmentdesignation" runat="server" Enabled="false" Text=""></asp:Label>
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid">
                            <asp:TextBox ID="txtphddepartmentremark" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid">
                            <asp:Button ID="btnphd" runat="server" Text="Submit" Height="25px" Enabled="false" BackColor="#ff9900" Width="80px" ForeColor="Black" BorderColor="Black" OnClick="btnphd_Click" />
                            <br />
                            <br />
                            <asp:Button ID="btnphdreject" runat="server" Text="Reject" Height="25px" Enabled="false" Width="80px" OnClick="btnphdreject_Click" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <div>
                                <asp:TextBox ID="txtidphd" runat="server" Width="50px" Visible="false"></asp:TextBox>
                            </div>

                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <br />

                <fieldset class="boxBody" style="border-color: black">
                    <asp:Label ID="Label30" runat="server" Text="FOR COLLEGE/DEPARTMENT/SECTION USE ONLY:" Font-Size="15pt" ForeColor="Black" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                </fieldset>
                <fieldset class="boxBody" style="border-color: black">
                    <table cellpadding="0px" cellspacing="0px">
                        <tr>
                            <td>
                                <td>
                                    <asp:Label ID="Label32" runat="server" Text="Certified that Mr./Ms."></asp:Label><asp:Label ID="Label33" runat="server" Text="" Font-Bold="true" Font-Underline="true"></asp:Label><asp:Label ID="Label34" runat="server" Text="has been working as"></asp:Label><asp:Label ID="Label35" runat="server" Text="" Font-Bold="true" Font-Underline="true"></asp:Label>
                                    <asp:Label ID="Label36" runat="server" Text="  since">
                                    </asp:Label><asp:Label ID="Label37" runat="server" Text=" " Font-Bold="true" Font-Underline="true"></asp:Label><asp:Label ID="Label38" runat="server" Text=" in the"></asp:Label><asp:Label ID="Label39" runat="server" Text=" " Font-Bold="true" Font-Underline="true"></asp:Label>&nbsp;<asp:Label ID="Label40" runat="server" Text="(College/dept./section). He/She has resigned from the services of the university.His/her case may be processed for relieving from the duty and issue of Experience Certificate/Relieving Certificate. "></asp:Label></td>
                            </td>
                        </tr>

                    </table>
                </fieldset>

                <fieldset class="boxBody" style="border-color: black">
                    <table cellpadding="0px" cellspacing="0px">
                        <tr>
                            <td>
                                <asp:Label ID="Label41" runat="server" Text="DIRECTOR/PRINCIPAL/HEAD"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="11" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td>
                                <label style="line-height: 25px">Name: </label>
                            </td>
                            <td style="width: 5px"></td>
                            <td>
                                <asp:TextBox ID="txtdirectorprinciname" runat="server" Enabled="false" Width="200px" BorderColor="Black"></asp:TextBox>
                            </td>
                            <td></td>
                            <td>
                                <asp:Button ID="Button3" runat="server" Text="Save" BackColor="#ff9900" Height="30px" Width="100px" Visible="false" />
                                <asp:Button ID="Button7" runat="server" Text="Reject" BackColor="#ff9900" Height="30px" Width="100px" Visible="false" />
                            </td>

                        </tr>
                        <tr>
                            <td colspan="11" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td>
                                <label style="line-height: 25px">Designation: </label>
                            </td>
                            <td style="width: 5px"></td>
                            <td>
                                <asp:TextBox ID="txtdirectorprincipalheaddeg" runat="server" Enabled="false" Width="200px" BorderColor="Black"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="11" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td>
                                <label style="line-height: 25px">College/Dept./Sec.: </label>
                            </td>
                            <td style="width: 5px"></td>
                            <td>
                                <asp:TextBox ID="txtdirectorprincipalheadcollegedept" runat="server" Enabled="false" Width="200px" BorderColor="Black"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td colspan="11" style="height: 5px"></td>
                        </tr>
                        <tr>
                            <td>
                                <label style="line-height: 25px">Date: </label>
                            </td>
                            <td style="width: 5px"></td>
                            <td>
                                <asp:TextBox ID="txtdatedirectorprincipaldate" runat="server" Enabled="false" BorderColor="Black" Width="200px" onkeydown="return false;" autocomplete="off" oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" AutoPostBack="True"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtdatedirectorprincipaldate" Format="dd MMM yyyy"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtdatedirectorprincipaldate" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </fieldset>

            </div>
                    </ContentTemplate>
                 </asp:UpdatePanel>
        </div>



        <div id="divfinance" runat="server" visible="false">

            <fieldset class="boxBody" style="border-color: black">
                <asp:Label ID="lblforhr" runat="server" Text="FOR HR/ACCOUNTS DEPARTMENT USE ONLY(For Leave Adjustment)" Font-Size="15pt" ForeColor="Black" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
            </fieldset>
            <asp:Table class="boxBody" Width="1080px" Style="border: 1px solid" HorizontalAlign="Center" ID="Table1" runat="server">
                <asp:TableRow>
                    <asp:TableHeaderCell Style="border: 1px solid">
                        <asp:Label ID="Label42" runat="server" Text="LEAVE TYPE" Enabled="false"></asp:Label>
                    </asp:TableHeaderCell>

                    <asp:TableHeaderCell Style="border: 1px solid">
                        <asp:Label ID="Label43" runat="server" Text="DUE" Enabled="false"></asp:Label>
                    </asp:TableHeaderCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="Label44" runat="server" Text="AVAILED" Enabled="false"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="Label45" runat="server" Text="BALANCE" Enabled="false"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="Label46" runat="server" Text="ADJUSTED" Enabled="false"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid" RowSpan="10">
                        <asp:Label ID="Label47" runat="server" Text="SIGNATURE" Enabled="false"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid" RowSpan="10">
                        <asp:Label ID="Label49" runat="server" Text="DESIGNATION" Enabled="false"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="Label51" runat="server" Text="Academic" Enabled="false"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:TextBox ID="txtdue1" runat="server" BorderColor="Black" Enabled="false"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:TextBox ID="txtavailed1" runat="server" BorderColor="Black" Enabled="false"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:DropDownList ID="drpbalance1" runat="server" Height="28px" Enabled="false">
                            <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                            <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                            <asp:ListItem Text="NO" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:DropDownList ID="drpadjust1" runat="server" Height="28px" Enabled="false">
                            <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                            <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                            <asp:ListItem Text="NO" Value="2"></asp:ListItem>
                            <asp:ListItem Text="NA" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="Label52" runat="server" Text="Medical" Enabled="false"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:TextBox ID="txtdues2" runat="server" BorderColor="Black" Enabled="false"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:TextBox ID="txtavailed2" runat="server" BorderColor="Black" Enabled="false"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:DropDownList ID="drbalance2" runat="server" Height="28px" Enabled="false">
                            <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                            <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                            <asp:ListItem Text="NO" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:DropDownList ID="drpadjust2" runat="server" Height="28px" Enabled="false">
                            <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                            <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                            <asp:ListItem Text="NO" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="Label53" runat="server" Text="Casual" Enabled="false"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:TextBox ID="txtdues3" runat="server" BorderColor="Black" Enabled="false"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:TextBox ID="txtavailed3" runat="server" BorderColor="Black" Enabled="false"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:DropDownList ID="drpbalance3" runat="server" Height="28px" Enabled="false">
                            <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                            <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                            <asp:ListItem Text="NO" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:DropDownList ID="drpadjust3" runat="server" Height="28px" Enabled="false">
                            <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                            <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                            <asp:ListItem Text="NO" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="Label54" runat="server" Text="Earned" Enabled="false"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:TextBox ID="txtdues4" runat="server" BorderColor="Black" Enabled="false"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:TextBox ID="txtavailed4" runat="server" BorderColor="Black" Enabled="false"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:DropDownList ID="drpbalance4" runat="server" Height="28px" Enabled="false">
                            <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                            <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                            <asp:ListItem Text="NO" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:DropDownList ID="drpadjust4" runat="server" Height="28px" Enabled="false">
                            <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                            <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                            <asp:ListItem Text="NO" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="Label55" runat="server" Text="Extraordinary" Enabled="false"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:TextBox ID="txtdue5" runat="server" BorderColor="Black" Enabled="false"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:TextBox ID="txtavailed5" runat="server" BorderColor="Black" Enabled="false"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:DropDownList ID="drpbalance5" runat="server" Height="28px" Enabled="false">
                            <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                            <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                            <asp:ListItem Text="NO" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:DropDownList ID="drpadjust5" runat="server" Height="28px" Enabled="false">
                            <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                            <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                            <asp:ListItem Text="NO" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="Label56" runat="server" Text="Special" Enabled="false"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:TextBox ID="txtdue6" runat="server" BorderColor="Black" Enabled="false"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:TextBox ID="txtavailed6" runat="server" BorderColor="Black" Enabled="false"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:DropDownList ID="drpbalance6" runat="server" Height="28px" Enabled="false">
                            <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                            <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                            <asp:ListItem Text="NO" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:DropDownList ID="drpadjust6" runat="server" Height="28px" Enabled="false">
                            <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                            <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                            <asp:ListItem Text="NO" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="Label57" runat="server" Text="Sabbatical" Enabled="false"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:TextBox ID="txtdue7" runat="server" BorderColor="Black" Enabled="false"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:TextBox ID="txtavailed7" runat="server" BorderColor="Black" Enabled="false"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:DropDownList ID="drpbalance7" runat="server" Height="28px" Enabled="false">
                            <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                            <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                            <asp:ListItem Text="NO" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:DropDownList ID="drpadjust7" runat="server" Height="28px" Enabled="false">
                            <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                            <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                            <asp:ListItem Text="NO" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="Label58" runat="server" Text="Maternity" Enabled="false"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:TextBox ID="txtdue8" runat="server" BorderColor="Black" Enabled="false"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:TextBox ID="txtavailed8" runat="server" BorderColor="Black" Enabled="false"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:DropDownList ID="drpbalance8" runat="server" Height="28px" Enabled="false">
                            <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                            <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                            <asp:ListItem Text="NO" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:DropDownList ID="drpadjust8" runat="server" Height="28px" Enabled="false">
                            <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                            <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                            <asp:ListItem Text="NO" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="Label59" runat="server" Text="Any Other (please specify)"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:TextBox ID="txtdue9" runat="server" BorderColor="Black" Enabled="false"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:TextBox ID="txtavailed9" runat="server" BorderColor="Black" Enabled="false"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:DropDownList ID="drpbalance9" runat="server" Height="28px" Enabled="false">
                            <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                            <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                            <asp:ListItem Text="NO" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:DropDownList ID="drpadjust9" runat="server" Height="28px" Enabled="false">
                            <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                            <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                            <asp:ListItem Text="NO" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <br />
            <table class="auto-style22" id="hr" runat="server" visible="false">
                <tr>
                    <td>&nbsp;Certified that the salary account of Mr./Ms.&nbsp;
                        <asp:Label ID="Label50" runat="server" Text="" Font-Bold="true" Font-Underline="true"></asp:Label>
                        &nbsp;has&nbsp; been&nbsp; adjusted&nbsp;for&nbsp;all&nbsp;outstanding/leaves/lones/advances etc. and nothing is due from him/her.Further,the details
                <br />
                        <br />
                        &nbsp;of payments&nbsp; to be made for Full &amp; Final&nbsp; Settlement are as under  
           
                :
                    </td>
                </tr>
            </table>
            <br />
            <table class="auto-style5" runat="server">
                <tr>
                    <td class="auto-style3"></td>
                    <td class="auto-style2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label60" runat="server" Text="DESCRIPTION"></asp:Label>
                        <br />
                        <br />

                        <asp:TextBox ID="TextBox81" runat="server" Height="58px" TextMode="MultiLine" Width="267px" Enabled="false" BorderColor="Black"></asp:TextBox>
                    </td>
                    <td class="auto-style4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label61" runat="server" Text="AMOUNT (Rs.)"></asp:Label>
                        <br />
                        <br />
                        <asp:TextBox ID="TextBox82" runat="server" Height="58px" Width="267px" Enabled="false" BorderColor="Black"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style7"></td>
                    <td style="font-size: large" class="auto-style8">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Total:</td>
                    <td>
                        <asp:TextBox ID="txttotalamount" runat="server" Height="43px" Width="262px" Enabled="false" BorderColor="Black"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table class="auto-style11" width="1200px">
                <tr>
                    <td></td>
                    <td class="auto-style51">Hence, an amount of</td>
                    <td class="auto-style52">
                        <asp:TextBox ID="TextBox16" runat="server"></asp:TextBox></td>
                    <td>(Rs</td>
                    <td class="auto-style53">
                        <asp:TextBox ID="TextBox17" runat="server"></asp:TextBox></td>
                    <td>for Full &amp; Final settlement of his/her account with the university.</td>
                </tr>
            </table>
            <table>
                <tr>
                    <td class="auto-style12">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                <asp:Label ID="Label66" runat="server" Text="Date:"></asp:Label>
                        &nbsp;<br />
                        <br />
                        <br />

                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label67" runat="server" Text="APPROVED BY:"></asp:Label>
                        <br />
                        <br />
                        <br />

                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                        <br />
                        <br />
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                <asp:Label ID="Label68" runat="server" Text="Date:"></asp:Label>
                    </td>
                    <td class="auto-style13">
                        <asp:TextBox ID="txtdatehr" runat="server" BorderColor="Black" Width="200px" onkeydown="return false;" Enabled="false" autocomplete="off" oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" AutoPostBack="True"></asp:TextBox>
                       
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                        <br />
                        <br />

                        <asp:Label ID="lblapprovedby" runat="server" Text=""></asp:Label>
                        <br />
                        <br />
                        <br />

                        <br />
                        <br />
                        <br />
                        <br />


                        <asp:TextBox ID="txtdatehr0" runat="server" autocomplete="off" AutoPostBack="True" Enabled="false" BorderColor="Black" oncontextmenu="return false" oncopy="return false" oncut="return false" onkeydown="return false;" onpaste="return false" Width="200px"></asp:TextBox>
                       
                        <br />
                    </td>
                    <td class="auto-style14">&nbsp;&nbsp;
                <asp:Label ID="Label69" runat="server" Text="Signature:"></asp:Label>&nbsp;<br />
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Name:<br />
                        <br />
                        &nbsp; Designation:<br />
                        <br />
                        <br />
                        &nbsp;&nbsp;
                <asp:Label ID="Label70" runat="server" Text="Signature:"></asp:Label>&nbsp;<br />
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Name:<br />
                        <br />
                        &nbsp; Designation:<td>
                            <asp:TextBox ID="TextBox83" runat="server" Width="200px" BorderColor="Black" Enabled="false"></asp:TextBox>

                            <br />
                            <br />
                            <asp:TextBox ID="TextBox84" runat="server" Width="200px" BorderColor="Black" Enabled="false"></asp:TextBox>

                            <br />
                            <br />
                            <asp:TextBox ID="TextBox85" runat="server" Width="200px" BorderColor="Black" Enabled="false"></asp:TextBox>

                            <br />
                            <br />
                            <br />
                            <asp:TextBox ID="TextBox86" runat="server" Width="200px" BorderColor="Black" Enabled="false"></asp:TextBox>

                            <br />
                            <br />
                            <asp:TextBox ID="TextBox87" runat="server" Width="200px" BorderColor="Black" Enabled="false"></asp:TextBox>
                            <br />
                            <br />

                            <asp:TextBox ID="TextBox88" runat="server" Width="200px" BorderColor="Black" Enabled="false"></asp:TextBox>
                        </td>
                    </td>
                </tr>

            </table>
            <br />

            <fieldset class="boxBody" style="border-color: black;">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label82" runat="server" Text="FOR FINANCE/ACCOUNTS/DEPARTMENT USE ONLY" Font-Bold="true" Font-Underline="true"></asp:Label>
                <table class="auto-style18">
                    <tr>
                        <td></td>
                        <td class="auto-style42">An amount of Rs</td>
                        <td>
                            <asp:TextBox ID="TextBox89" runat="server" Width="85px"></asp:TextBox></td>
                        <td class="auto-style43">(Rs</td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server" Width="91px"></asp:TextBox></td>
                        <td class="auto-style35">) in cash/Vide cheque no.</td>
                        <td class="auto-style37">
                            <asp:TextBox ID="TextBox2" runat="server" Width="75px" CssClass="col-xs-offset-0"></asp:TextBox></td>
                        <td class="auto-style44">dated</td>
                        <td>
                            <asp:TextBox ID="TextBox3" runat="server" Width="91px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender10" runat="server" TargetControlID="TextBox3" Format="dd MMM yyyy"></asp:CalendarExtender>

                            <%-- //<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="TextBox3" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps">--%>
                        </td>
                        <td class="auto-style29">drawn on</td>
                        <td>
                            <asp:TextBox ID="TextBox4" runat="server" Enabled="false" Width="91px"></asp:TextBox></td>
                        <td>is paid to </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr style="height: 20px">
                        <td class="auto-style17"></td>
                        <td class="auto-style36">Mr./Ms.</td>
                        <td class="auto-style17">
                            <asp:Label ID="Label88" runat="server" Text="" Font-Bold="true" Font-Underline="true"></asp:Label></td>
                        <td colspan="5" class="auto-style17">for Full Final Settlement of his/her account with the university. OR An amount of Rs</td>
                        <td class="auto-style39">
                            <asp:TextBox ID="TextBox6" runat="server" Width="91px"></asp:TextBox></td>
                        <td class="auto-style40">(Rs</td>
                        <td colspan="2">
                            <asp:TextBox ID="TextBox8" runat="server" Width="91px"></asp:TextBox></td>

                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr style="height: 20px">
                        <td colspan="5">shall be transferred to the salary account of Mr./Ms.</td>
                        <td>
                            <asp:Label ID="Label62" runat="server" Text="" Font-Bold="true" Font-Underline="true"></asp:Label></td>
                        <td colspan="4">for Full & Final settlement of his/her account with the university.</td>

                    </tr>
                </table>
                <br />

                <table class="auto-style19" runat="server">
                    <tr>
                        <td class="auto-style12">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                <asp:Label ID="Label92" runat="server" Text="Date:"></asp:Label>
                        </td>
                        <td class="auto-style13">
                            <asp:TextBox ID="txtfinance" runat="server" BorderColor="Black" Enabled="false" Width="200px" onkeydown="return false;" autocomplete="off" oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                            <%--<asp:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txtfinance" Format="dd MMM yyyy"></asp:CalendarExtender>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtfinance" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>--%>
                        </td>
                        <td class="auto-style14">&nbsp;&nbsp;
                <asp:Label ID="Label93" runat="server" Text="Signature:"></asp:Label>&nbsp;<br />
                            <br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Name:<br />
                            <br />
                            &nbsp; Designation:<br />
                            <br />
                            <td>
                                <asp:TextBox ID="TextBox7" runat="server" Width="200px" BorderColor="Black" Enabled="false"></asp:TextBox>

                                <br />
                                <br />
                                <asp:TextBox ID="TextBox9" runat="server" Width="200px" BorderColor="Black" Enabled="false"></asp:TextBox>

                                <br />
                                <br />
                                <asp:TextBox ID="TextBox10" runat="server" Width="200px" BorderColor="Black" Enabled="false"></asp:TextBox>
                            </td>
                    </tr>
                    <td class="auto-style24"></td>
                    <td style="text-align: center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                
                <asp:Button ID="Button1" runat="server" Text="Save" BackColor="#ff9900" Height="30px" Width="100px" Visible="false" OnClick="Button1_Click" />
                    </td>
                    </td>
        </tr>
                </table>
                <br />

            </fieldset>


        </div>

    </asp:Panel>


    <asp:Button ID="btnDummy" runat="server" Style="display: none;" />
    <asp:ModalPopupExtender ID="GridViewdata" runat="server" TargetControlID="btnDummy"
        PopupControlID="pnlGridViewdata" BackgroundCssClass="modalBackground" />


</asp:Content>

