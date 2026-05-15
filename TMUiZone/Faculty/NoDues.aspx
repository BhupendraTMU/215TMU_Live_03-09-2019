<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="NoDues.aspx.cs" Inherits="Faculty_NoDues" %>

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
            width: 136px
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
            width: 100px
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
            width: 111px;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">
        <div id="printarea">

            <div style="width: 100%; margin-bottom: 10px; margin-left: 1%; margin-right: 1%; margin-top: 5px;">

                <table style="width: 98%;">
                    <tr>
                        <td style="width: 1%"></td>
                        <td style="width: 12%; text-align: left">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/rightlogo.png" Width="55%" />
                        </td>
                        <td style="width: 65%; text-align: center">
                            <strong>
                                <asp:Label ID="lblCName" Font-Size="Large" Text="Teerthanker Mahaveer University, Moradabad" runat="server"></asp:Label></strong>
                            <br />
                            <strong>
                                <asp:Label ID="lblAC" runat="server" Text="(Established under Govt. of U. P. Act No. 30, 2008)"></asp:Label></strong>

                            <br />
                            <strong>
                                <asp:Label ID="LblType" runat="server" Text="Delhi Road, Moradabad (U.P)"></asp:Label>
                            </strong>
                            <br />

                        </td>
                        <td style="width: 10%; text-align: center"></td>
                    </tr>
            </div>
        </div>
        </table>
    </fieldset>
    <fieldset class="boxBody" style="text-align: center; border-color: black; background-color: black;">
        <asp:Label ID="Label1" runat="server" Text="No Dues" Font-Size="15pt" ForeColor="White" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
    <br />
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
                </td>
                <td style="width: 10px"></td>
                <td>
                    <label style="line-height: 25px">Issued On: </label>
                </td>
                <td style="width: 10px"></td>
                <td>
                    <asp:TextBox ID="txtIssuedon" runat="server" BorderColor="Black" Width="200px" onkeydown="return false;" autocomplete="off" oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" AutoPostBack="True"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtIssuedon" Format="dd MMM yyyy"></asp:CalendarExtender>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtIssuedon" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                </td>
                <td style="width: 10px"></td>

                <td style="width: 10px"></td>
                <td>
                    <label style="line-height: 25px">Deputy Registrar (HR):</label>
                </td>
                <td style="width: 10px"></td>
                <td>
                    <asp:TextBox ID="txtdeputyRegistrar" runat="server" Width="200px" BorderColor="Black"></asp:TextBox>
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
                    <label style="line-height: 25px">Date Of Leaving:</label>
                </td>
                <td style="width: 10px"></td>
                <td>
                    <asp:TextBox ID="txtdateofleaving" runat="server" BorderColor="Black" Width="200px" onkeydown="return false;" autocomplete="off" oncopy="return false" Enabled="false" onpaste="return false" oncut="return false" oncontextmenu="return false" AutoPostBack="True"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtdateofleaving" Format="dd MMM yyyy"></asp:CalendarExtender>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtdateofleaving" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                </td>
                <td style="width: 10px"></td>
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
    <br />
    <fieldset class="boxBody" style="border-color: black">
        <asp:Label ID="Label2" runat="server" Text="Certified that nothing is outstanding against the employee as mentioned above:" Font-Size="15pt" ForeColor="Black" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
    </fieldset>
    <br />
    <asp:Table class="boxBody" Width="1200px" Style="border: 1px solid" ID="tblData" runat="server">
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
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="lbldepartmentlibdeg" runat="server" Enabled="false" Text=""></asp:Label>
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:TextBox ID="txtremarkdeptlibrary" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:Button ID="btn_submit1" runat="server" Enabled="false" Text="Submit" Height="25px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="btn_submit1_Click" />
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

            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">

                <asp:Label ID="lblcentlibdeg" runat="server" Enabled="false" Text=""></asp:Label>

            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:TextBox ID="txtremarkcentrallib" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button2" runat="server" Enabled="false" Text="Submit" Height="25px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="Button2_Click" />
            </asp:TableCell>
        </asp:TableRow>
        <%-- <asp:TableRow>
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
            </asp:TableCell>
            
            <asp:TableCell style="border:1px solid">
                 <asp:Label ID="lblbookbankdeg" runat="server" Enabled="false" Text=""></asp:Label>
            </asp:TableCell>
            
            <asp:TableCell style="border:1px solid">
                <asp:TextBox ID="txtremarkbookbank" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
            </asp:TableCell>
              <asp:TableCell style="border:1px solid">
                  <asp:Button ID="Button3" runat="server" Enabled="false" Text="Submit" Height="25px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="Button3_Click" />
                </asp:TableCell>
            </asp:TableRow>--%>
        <asp:TableRow ID="SeedMoneyVal" runat="server">
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
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="lblseedmoneydeg" runat="server" Enabled="false" Text=""></asp:Label>
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:TextBox ID="txtremarkseedmoney" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button4" runat="server" Enabled="false" Text="Submit" Height="25px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="Button4_Click" />
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
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="lbldeptstoredeg" runat="server" Enabled="false" Text=""></asp:Label>
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:TextBox ID="txtremarkdepartmentstore" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button5" runat="server" Enabled="false" Text="Submit" Height="25px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="Button5_Click" />
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
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="lblcentralstoredeg" runat="server" Enabled="false" Text=""></asp:Label>
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:TextBox ID="txtremarkcentralstore" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button6" runat="server" Enabled="false" Text="Submit" Height="25px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="Button6_Click" />
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
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="lblcollegedeptdeg" runat="server" Enabled="false" Text=""></asp:Label>
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:TextBox ID="txtremarkcollegedeptre" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:Button ID="txtremarkdepartmentlaborat" runat="server" Enabled="false" Text="Submit" Height="25px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="txtremarkdepartmentlaborat_Click" />
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
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="lblcollegeworkdeg" runat="server" Enabled="false" Text=""></asp:Label>
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:TextBox ID="txtremarkdepartmentwork" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button8" runat="server" Enabled="false" Text="Submit" Height="25px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="Button8_Click" />
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
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="lblhostelmessdeg" runat="server" Enabled="false" Text=""></asp:Label>
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:TextBox ID="txtremarkhostelmess" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button9" runat="server" Enabled="false" Text="Submit" Height="25px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="Button9_Click" />
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
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="lblhosteloffficedeg" runat="server" Enabled="false" Text=""></asp:Label>
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:TextBox ID="txtremarkhosteloffice" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button10" runat="server" Enabled="false" Text="Submit" Height="25px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="Button10_Click" />
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
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="lbltransportofficedeg" runat="server" Enabled="false" Text=""></asp:Label>
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:TextBox ID="txtremarktransportoffice" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button11" runat="server" Enabled="false" Text="Submit" Height="25px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="Button11_Click" />
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
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="lblguestdeg" runat="server" Enabled="false" Text=""></asp:Label>
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:TextBox ID="txtremarkguesthouseic" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button12" runat="server" Enabled="false" Text="Submit" Height="25px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="Button12_Click" />
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
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="lblfacultydeg" runat="server" Enabled="false" Text=""></asp:Label>
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:TextBox ID="txtremarkfacultyhose" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button13" runat="server" Enabled="false" Text="Submit" Height="25px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="Button13_Click" />
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
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="lblsportdeg" runat="server" Enabled="false" Text=""></asp:Label>
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:TextBox ID="txtremarksportic" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button14" runat="server" Enabled="false" Text="Submit" Height="25px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="Button14_Click" />
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
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="lblmedicaldeg" runat="server" Enabled="false" Text=""></asp:Label>
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:TextBox ID="txtremarkmedicalstore" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button15" runat="server" Enabled="false" Text="Submit" Height="25px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="Button15_Click" />
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
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="lblelectricitydeg" runat="server" Enabled="false" Text=""></asp:Label>
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:TextBox ID="txtremarkelectrictydepart" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button16" runat="server" Enabled="false" Text="Submit" Height="25px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="Button16_Click" />
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
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="lbliccardofficedeg" runat="server" Enabled="false" Text=""></asp:Label>
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:TextBox ID="txtremarkicardoffice" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button17" runat="server" Enabled="false" Text="Submit" Height="25px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="Button17_Click" />
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
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="lblitdeg" runat="server" Enabled="false" Text=""></asp:Label>
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:TextBox ID="txtremarkit" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button18" runat="server" Enabled="false" Text="Submit" Height="25px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="Button18_Click" />
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
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="lbldepartmentdeg" runat="server" Text=""></asp:Label>
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:TextBox ID="txtremarkheaddept" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button19" runat="server" Enabled="false" Text="Submit" Height="25px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="Button19_Click" />
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
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="lblcashdeg" runat="server" Enabled="false" Text=""></asp:Label>
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:TextBox ID="txtremarkcashcounter" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button20" runat="server" Text="Submit" Enabled="false" Height="25px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="Button20_Click" />
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
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="lbljtdirdeg" runat="server" Enabled="false" Text=""></asp:Label>
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:TextBox ID="txtremarkjtdirector" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button21" runat="server" Enabled="false" Text="Submit" Height="25px" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="Button21_Click" />
            </asp:TableCell>
        </asp:TableRow>
       

        <asp:TableRow>
            <asp:TableCell Style="border: 1px solid">21.</asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="Label48" runat="server" Enabled="false" Text="Ph.D Department"></asp:Label>
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
                <asp:TextBox ID="txtphddepartmentnamecode" runat="server" Visible="false"></asp:TextBox>
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="lblphddepartmentdesignation" runat="server" Enabled="false" Text=""></asp:Label>
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:TextBox ID="txtphddepartmentremark" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:Button ID="btnphd" runat="server" Text="Submit" Height="25px" Enabled="false" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
            </asp:TableCell>
            <asp:TableCell>
                <div>
                    <asp:TextBox ID="txtidphd" runat="server" Width="50px" Visible="false"></asp:TextBox>
                </div>

            </asp:TableCell>
        </asp:TableRow>

                 <asp:TableRow>
         <asp:TableCell style="border: 1px solid">22.</asp:TableCell>
            <asp:TableCell style="border:1px solid">
                <asp:Label ID="Label29" runat="server" Enabled="false" Text="Payroll Section"></asp:Label>
</asp:TableCell>
            <asp:TableCell style="border:1px solid">
                 <asp:TableCell style="border:1px solid">
                <asp:DropDownList ID="DropDownList22" runat="server" Enabled="false" Height="28px">
                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Granted" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Not Granted" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Pending At Department" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Contact To Department" Value="4"></asp:ListItem>
                </asp:DropDownList>
            </asp:TableCell>
            </asp:TableCell>
        
            <asp:TableCell style="border:1px solid">
                 <asp:Label ID="lblpayrollname" runat="server" Enabled="false"  Text=""></asp:Label>
            </asp:TableCell>
            
            <asp:TableCell style="border:1px solid">
                 <asp:Label ID="lblpayrolldeg" runat="server" Enabled="false"  Text=""></asp:Label>
            </asp:TableCell>
            
            <asp:TableCell style="border:1px solid">
                <asp:TextBox ID="txtremarkpayrollsection" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
            </asp:TableCell>
             <asp:TableCell style="border:1px solid">
                  <asp:Button ID="Button22" runat="server" Text="Submit" Height="25px" Enabled="false" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" OnClick="Button22_Click" />
                </asp:TableCell>
            </asp:TableRow>
       <%-- <asp:TableRow>
            <asp:TableCell Style="border: 1px solid">22.</asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="Label6" runat="server" Enabled="false" Text="Linen and Laundry"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:TableCell Style="border: 1px solid">
                    <asp:DropDownList ID="drplineLaundry" runat="server" Enabled="false" Height="28px">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Granted" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Not Granted" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Pending At Department" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Contact To Department" Value="4"></asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="lblLineLoundaryCode" runat="server" Enabled="false" Text=""></asp:Label>
                <asp:TextBox ID="lblLineLoundaryName" runat="server" Visible="false"></asp:TextBox>
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="lblLineLoundarydesignation" runat="server" Enabled="false" Text=""></asp:Label>
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:TextBox ID="txtLineLoundaryremark" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:Button ID="btnLineLoundary" runat="server" Text="Submit" Height="25px" Enabled="false" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
            </asp:TableCell>
            <asp:TableCell>
                <div>
                    <asp:TextBox ID="txtLineLoundary" runat="server" Width="50px" Visible="false"></asp:TextBox>
                </div>

            </asp:TableCell>
        </asp:TableRow>


        <asp:TableRow>
            <asp:TableCell Style="border: 1px solid">23.</asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="Label29" runat="server" Enabled="false" Text="Hospital IT Dept."></asp:Label>
            </asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:TableCell Style="border: 1px solid">
                    <asp:DropDownList ID="drpHospitalITDept" runat="server" Enabled="false" Height="28px">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Granted" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Not Granted" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Pending At Department" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Contact To Department" Value="4"></asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="lblHospitalITDept" runat="server" Enabled="false" Text=""></asp:Label>
                <asp:TextBox ID="txtHospitalITDept" runat="server" Visible="false"></asp:TextBox>
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="lblHospitalITDeptDesignation" runat="server" Enabled="false" Text=""></asp:Label>
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:TextBox ID="txtHospitalITDeptRemark" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:Button ID="btnHospitalITDept" runat="server" Text="Submit" Height="25px" Enabled="false" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
            </asp:TableCell>
            <asp:TableCell>
                <div>
                    <asp:TextBox ID="txtHospitalITDept1" runat="server" Width="50px" Visible="false"></asp:TextBox>
                </div>

            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell Style="border: 1px solid">24.</asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="Label63" runat="server" Enabled="false" Text="Biomedical Engineer"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:TableCell Style="border: 1px solid">
                    <asp:DropDownList ID="drpBiomedicalEngineer" runat="server" Enabled="false" Height="28px">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Granted" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Not Granted" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Pending At Department" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Contact To Department" Value="4"></asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="lblBiomedicalEngineer" runat="server" Enabled="false" Text=""></asp:Label>
                <asp:TextBox ID="txtBiomedicalEngineer" runat="server" Visible="false"></asp:TextBox>
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="lblBiomedicalEngineerdesignation" runat="server" Enabled="false" Text=""></asp:Label>
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:TextBox ID="txtBiomedicalEngineerRemark" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:Button ID="btnBiomedicalEngineer" runat="server" Text="Submit" Height="25px" Enabled="false" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
            </asp:TableCell>
            <asp:TableCell>
                <div>
                    <asp:TextBox ID="txtBiomedicalEngineer1" runat="server" Width="50px" Visible="false"></asp:TextBox>
                </div>
            </asp:TableCell>
        </asp:TableRow>


         <asp:TableRow>
     <asp:TableCell Style="border: 1px solid">25.</asp:TableCell>
     <asp:TableCell Style="border: 1px solid">
         <asp:Label ID="Label64" runat="server" Enabled="false" Text="Cash Counter"></asp:Label>
     </asp:TableCell>
     <asp:TableCell Style="border: 1px solid">
         <asp:TableCell Style="border: 1px solid">
             <asp:DropDownList ID="drpCashCounter" runat="server" Enabled="false" Height="28px">
                 <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                 <asp:ListItem Text="Granted" Value="1"></asp:ListItem>
                 <asp:ListItem Text="Not Granted" Value="2"></asp:ListItem>
                 <asp:ListItem Text="Pending At Department" Value="3"></asp:ListItem>
                 <asp:ListItem Text="Contact To Department" Value="4"></asp:ListItem>
             </asp:DropDownList>
         </asp:TableCell>
     </asp:TableCell>

     <asp:TableCell Style="border: 1px solid">
         <asp:Label ID="lblCashCounter" runat="server" Enabled="false" Text=""></asp:Label>
         <asp:TextBox ID="txtCashCounter" runat="server" Visible="false"></asp:TextBox>
     </asp:TableCell>

     <asp:TableCell Style="border: 1px solid">
         <asp:Label ID="lblCashCounterDesignation" runat="server" Enabled="false" Text=""></asp:Label>
     </asp:TableCell>

     <asp:TableCell Style="border: 1px solid">
         <asp:TextBox ID="txtCashCounterRemark" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
     </asp:TableCell>
     <asp:TableCell Style="border: 1px solid">
         <asp:Button ID="btnCashCounter" runat="server" Text="Submit" Height="25px" Enabled="false" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
     </asp:TableCell>
     <asp:TableCell>
         <div>
             <asp:TextBox ID="txtCashCounter1" runat="server" Width="50px" Visible="false"></asp:TextBox>
         </div>
     </asp:TableCell>
 </asp:TableRow>

                <asp:TableRow>
    <asp:TableCell Style="border: 1px solid">26.</asp:TableCell>
    <asp:TableCell Style="border: 1px solid">
        <asp:Label ID="Label65" runat="server" Enabled="false" Text="Payroll"></asp:Label>
    </asp:TableCell>
    <asp:TableCell Style="border: 1px solid">
        <asp:TableCell Style="border: 1px solid">
            <asp:DropDownList ID="drpPayroll" runat="server" Enabled="false" Height="28px">
                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                <asp:ListItem Text="Granted" Value="1"></asp:ListItem>
                <asp:ListItem Text="Not Granted" Value="2"></asp:ListItem>
                <asp:ListItem Text="Pending At Department" Value="3"></asp:ListItem>
                <asp:ListItem Text="Contact To Department" Value="4"></asp:ListItem>
            </asp:DropDownList>
        </asp:TableCell>
    </asp:TableCell>

    <asp:TableCell Style="border: 1px solid">
        <asp:Label ID="lblPayroll" runat="server" Enabled="false" Text=""></asp:Label>
        <asp:TextBox ID="txtPayroll" runat="server" Visible="false"></asp:TextBox>
    </asp:TableCell>

    <asp:TableCell Style="border: 1px solid">
        <asp:Label ID="lblPayrollDesignation" runat="server" Enabled="false" Text=""></asp:Label>
    </asp:TableCell>

    <asp:TableCell Style="border: 1px solid">
        <asp:TextBox ID="txtPayrollRemark" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
    </asp:TableCell>
    <asp:TableCell Style="border: 1px solid">
        <asp:Button ID="btnPayroll" runat="server" Text="Submit" Height="25px" Enabled="false" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
    </asp:TableCell>
    <asp:TableCell>
        <div>
            <asp:TextBox ID="txtPayroll1" runat="server" Width="50px" Visible="false"></asp:TextBox>
        </div>
    </asp:TableCell>
</asp:TableRow>--%>





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
                <td rowspan="10">
                    <asp:TextBox ID="TextBox5" runat="server" Width="700px" Enabled="false" Height="150px" BorderColor="Black"></asp:TextBox></td>
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

    <fieldset class="boxBody" style="border-color: black">
        <asp:Label ID="lblforhr" runat="server" Text="FOR HR/ACCOUNTS DEPARTMENT USE ONLY(For Leave Adjustment)" Font-Size="15pt" ForeColor="Black" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
    </fieldset>
    <asp:Table class="boxBody" Width="1200px" Style="border: 1px solid" ID="Table1" runat="server">
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
                <asp:Label ID="Label59" runat="server" Text="Any Other (please specify)" Enabled="false"></asp:Label>
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
    <table class="auto-style22">
        <tr>
            <td>&nbsp;Certified that the salary account of Mr./Ms.&nbsp;
                <asp:Label ID="Label50" runat="server" Text="" Font-Bold="true" Font-Underline="true"></asp:Label>
                &nbsp;has&nbsp; been&nbsp; adjusted&nbsp;for&nbsp;all&nbsp;outstanding/leaves/lones/advances etc. and nothing is due from him/her.Further,the details
                <br />
                <br />
            &nbsp;of payments&nbsp; to be madefor Full &amp; Final&nbsp; Settlement are as under  
           
                :
        </tr>
    </table>
    <br />
    <table class="auto-style5">
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
                <asp:TextBox ID="TextBox82" runat="server" Height="58px" Enabled="false" Width="267px" BorderColor="Black"></asp:TextBox>
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
                <asp:TextBox ID="txttotalamount" runat="server" Height="43px" Enabled="false" Width="262px" BorderColor="Black"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
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
    <br />
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
                <asp:TextBox ID="txtdatehr" runat="server" BorderColor="Black" Width="200px" Enabled="false" onkeydown="return false;" autocomplete="off" oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" AutoPostBack="True"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtdatehr" Format="dd MMM yyyy"></asp:CalendarExtender>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtdatehr" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
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


                <asp:TextBox ID="txtdatehr0" runat="server" autocomplete="off" Enabled="false" AutoPostBack="True" BorderColor="Black" oncontextmenu="return false" oncopy="return false" oncut="return false" onkeydown="return false;" onpaste="return false" Width="200px"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd MMM yyyy" TargetControlID="txtdatehr0">
                </asp:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtdatehr0" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
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
                    <asp:TextBox ID="TextBox83" runat="server" Width="200px" Enabled="false" BorderColor="Black"></asp:TextBox>

                    <br />
                    <br />
                    <asp:TextBox ID="TextBox84" runat="server" Width="200px" Enabled="false" BorderColor="Black"></asp:TextBox>

                    <br />
                    <br />
                    <asp:TextBox ID="TextBox85" runat="server" Width="200px" Enabled="false" BorderColor="Black"></asp:TextBox>

                    <br />
                    <br />
                    <br />
                    <asp:TextBox ID="TextBox86" runat="server" Width="200px" Enabled="false" BorderColor="Black"></asp:TextBox>

                    <br />
                    <br />
                    <asp:TextBox ID="TextBox87" runat="server" Width="200px" Enabled="false" BorderColor="Black"></asp:TextBox>
                    <br />
                    <br />

                    <asp:TextBox ID="TextBox88" runat="server" Width="200px" Enabled="false" BorderColor="Black"></asp:TextBox>
                </td>
        </tr>

    </table>
    <br />
    <%--<fieldset class="boxBody" style="border-color:black;">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        
        
                <asp:Label ID="Label71" runat="server" Text="CONFIRMATION FROM HR" Font-Bold="true" Font-Underline="true"></asp:Label>
        <table class="auto-style16">
            <tr>
            <td class="auto-style15">
                Mr./Ms&nbsp; <asp:Label ID="Label72" runat="server" Text="" Font-Underline="true" Font-Bold="true"></asp:Label>
                &nbsp; Designation&nbsp;&nbsp; <asp:Label ID="Label73" runat="server" Text="" Font-Bold="true" Font-Underline="true"></asp:Label>
                &nbsp;
                Deptt./College&nbsp; <asp:Label ID="Label74" runat="server" Text="" Font-Underline="true" Font-Bold="true"></asp:Label>
                &nbsp; has submitted application from&nbsp; resignation from services, w.e.f&nbsp;<asp:Label ID="Label75" runat="server" Text=""></asp:Label>
               .&nbsp;&nbsp; The same&nbsp;&nbsp;&nbsp;&nbsp; has&nbsp;&nbsp;
            </td>
                </tr>
            <tr>
                <td>
                    been approved by the
                    competent authority. Employee may be deactivated on ERP by Payroll section w.e.f.<asp:Label ID="Label76" runat="server" Text=""></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; .<br />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label79" runat="server" Text=""></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <br />
                    <asp:Label ID="Label77" runat="server" Text=""></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label80" runat="server" Text="Registrar"></asp:Label>
                    <br />
                    <asp:Label ID="Label78" runat="server" Text="Deputy Registrar (HR)"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label81" runat="server" Text="(Approving Authority)"></asp:Label>
                    
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td style="text-align: center;" />
                <asp:Button ID="btn_Savehr" runat="server" Text="Save" BackColor="#ff9900" Height="30px" Width="100px" OnClick="btn_Savehr_Click" visible="false" />
                </td>
            </tr>
            </table>
        </fieldset>--%>
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
                    <asp:TextBox ID="TextBox3" runat="server" Width="91px"></asp:TextBox></td>
                <td class="auto-style29">drawn on</td>
                <td>
                    <asp:TextBox ID="TextBox4" runat="server" Width="91px"></asp:TextBox></td>
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

        <table class="auto-style19">
            <tr>
                <td class="auto-style12">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                <asp:Label ID="Label92" runat="server" Text="Date:"></asp:Label>
                </td>
                <td class="auto-style25">
                    <asp:TextBox ID="txtfinance" runat="server" BorderColor="Black" Width="200px" Enabled="false" onkeydown="return false;" autocomplete="off" oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" AutoPostBack="True"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txtfinance" Format="dd MMM yyyy"></asp:CalendarExtender>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtfinance" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                </td>
                <td class="auto-style14">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label93" runat="server" Text="Signature:"></asp:Label>&nbsp;<br />
                    &nbsp;<br />
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
            </tr>
        
        </table>
    </fieldset>
    <table class="auto-style23">
        <tr>
            <td style="width: 560px"></td>
            <td>
                <asp:Button ID="btnsave" runat="server" Text="Save" Height="30px" Width="100px" OnClick="btnsave_Click" BackColor="#ff9900" />
            </td>
        </tr>
    </table>

    <br />
    <br />
</asp:Content>

