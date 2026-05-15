<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="NoDuesApprovalList.aspx.cs" Inherits="Faculty_NoDuesApprovalList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        tr.myclass a {
            padding-right: 7px;
            padding-left: 7px;
        }
    </style>

    <script type="text/javascript">
        function txtHSpercentage() {

            var Dept1 = 0, Dept2 = 0, Dept3 = 0, Dept4 = 0, Dept5 = 0, Dept6 = 0, Dept7 = 0, Dept8 = 0, Dept9 = 0,
                Dept10 = 0, Dept11 = 0, Dept12 = 0, Dept13 = 0, Dept14 = 0;
            if (document.getElementById("ContentPlaceHolder1_txtpendingamountdeptlibrary").value == "") {
                Dept1 = 0;
            }
            else {
                Dept1 = document.getElementById("ContentPlaceHolder1_txtpendingamountdeptlibrary").value;
            }

            if (document.getElementById("ContentPlaceHolder1_txtpendingamountcentrallibrary").value == "") {
                Dept2 = 0;
            }
            else {
                Dept2 = document.getElementById("ContentPlaceHolder1_txtpendingamountcentrallibrary").value;
            }

            if (document.getElementById("ContentPlaceHolder1_txtpandingamountdeptlaboratory").value == "") {
                Dept3 = 0;
            }
            else {
                Dept3 = document.getElementById("ContentPlaceHolder1_txtpandingamountdeptlaboratory").value;
            }

            if (document.getElementById("ContentPlaceHolder1_TextBox1").value == "") {
                Dept4 = 0;
            }
            else {
                Dept4 = document.getElementById("ContentPlaceHolder1_TextBox1").value;
            }

            if (document.getElementById("ContentPlaceHolder1_TextBox2").value == "") {
                Dept5 = 0;
            }
            else {
                Dept5 = document.getElementById("ContentPlaceHolder1_TextBox2").value;
            }
            if (document.getElementById("ContentPlaceHolder1_TextBox3").value == "") {
                Dept6 = 0;
            }
            else {
                Dept6 = document.getElementById("ContentPlaceHolder1_TextBox3").value;
            }
            if (document.getElementById("ContentPlaceHolder1_TextBox4").value == "") {
                Dept7 = 0;
            }
            else {
                Dept7 = document.getElementById("ContentPlaceHolder1_TextBox4").value;
            }
            if (document.getElementById("ContentPlaceHolder1_TextBox5").value == "") {
                Dept8 = 0;
            }
            else {
                Dept8 = document.getElementById("ContentPlaceHolder1_TextBox5").value;
            }
            if (document.getElementById("ContentPlaceHolder1_TextBox6").value == "") {
                Dept9 = 0;
            }
            else {
                Dept9 = document.getElementById("ContentPlaceHolder1_TextBox6").value;
            }
            if (document.getElementById("ContentPlaceHolder1_TextBox7").value == "") {
                Dept10 = 0;
            }
            else {
                Dept10 = document.getElementById("ContentPlaceHolder1_TextBox7").value;
            }
            if (document.getElementById("ContentPlaceHolder1_TextBox8").value == "") {
                Dept11 = 0;
            }
            else {
                Dept11 = document.getElementById("ContentPlaceHolder1_TextBox8").value;
            }
            if (document.getElementById("ContentPlaceHolder1_TextBox9").value == "") {
                Dept12 = 0;
            }
            else {
                Dept12 = document.getElementById("ContentPlaceHolder1_TextBox9").value;
            }
            if (document.getElementById("ContentPlaceHolder1_TextBox10").value == "") {
                Dept13 = 0;
            }
            else {
                Dept13 = document.getElementById("ContentPlaceHolder1_TextBox10").value;
            }
            if (document.getElementById("ContentPlaceHolder1_TextBox11").value == "") {
                Dept14 = 0;
            }
            else {
                Dept14 = document.getElementById("ContentPlaceHolder1_TextBox11").value;
            }
            document.getElementById("ContentPlaceHolder1_TextBox12").value = parseFloat(Dept1) + parseFloat(Dept2) + parseFloat(Dept3) + parseFloat(Dept4) + parseFloat(Dept5) + parseFloat(Dept6) + parseFloat(Dept7) + parseFloat(Dept8)
                + parseFloat(Dept9) + parseFloat(Dept10) + parseFloat(Dept11) + parseFloat(Dept12) + parseFloat(Dept13) + parseFloat(Dept14);
        }
    </script>

    <script type="text/javascript">
        function txtHSpercentage1() {

            var Dept15 = 0, Dept16 = 0, Dept17 = 0;

            if (document.getElementById("ContentPlaceHolder1$TextBox35").value == "") {
                Dept15 = 0;

            }
            else {
                Dept15 = document.getElementById("ContentPlaceHolder1$TextBox35").value;
            }

            if (document.getElementById("ctl00$ContentPlaceHolder1$TextBox30").value == "") {
                Dept16 = 0;
            }
            else {
                Dept16 = document.getElementById("ctl00$ContentPlaceHolder1$TextBox30").value;
            }

            if (document.getElementById("ContentPlaceHolder1$TextBox38").value == "") {
                Dept17 = 0;
            }
            else {
                Dept17 = document.getElementById("ContentPlaceHolder1$TextBox38").value;
            }


            document.getElementById("ContentPlaceHolder1$TextBox116").value = parseFloat(Dept15) + parseFloat(Dept16) + parseFloat(Dept17);
        }
    </script>


    <script type="text/javascript">
        function numeric(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && ((charCode >= 48 && charCode <= 57) || charCode == 46))
                return true;
            else {
                alert('Please Enter Number Only .');
                return false;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<fieldset id="status" style="text-align: center">
        <div class="form-group">
             <div class="col-md-2">
                 </div>
            <div class="col-md-1">

                <asp:Label ID="Label2" runat="server" Text="Status:" Font-Bold="true" Font-Size="Larger"></asp:Label>
            </div>

            <div class="col-md-3" style="text-align: right">
                <asp:DropDownList ID="drpstatus" runat="server" CssClass="form-control" AutoPostBack="true">
                    <asp:ListItem Text="All" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Pending" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Approved" Value="3"></asp:ListItem>

                </asp:DropDownList>
            </div>
            <div class="col-md-1">
                <asp:Button ID="BtnShowReport" runat="server" Text="Search" ForeColor="White" CssClass="form-control" BackColor="#ff9900" />
            </div>

           
        </div>
    </fieldset>--%>
       <fieldset id="status" style="text-align: center">
      <div class="form-group" style="margin-top:20px;">
          <div class="col-md-2" style="margin-top: 8px;">
              <asp:Label ID="Label222" runat="server" Text="Enrollement No:" Font-Bold="true" Font-Size="Larger"></asp:Label>
          </div>
          <div class="col-md-3" style="text-align: right">
              <asp:TextBox ID="searchvalue" runat="server" CssClass="form-control" AutoPostBack="false">
              </asp:TextBox>
          </div>
          <div class="col-md-1">
              <asp:Button ID="BtnShowReport" runat="server" Text="Search" ForeColor="White" OnClick="lnkbutton_Click1xx" CssClass="form-control" BackColor="#ff9900" />
          </div>
            <div class="col-md-4">
  </div>
 <div class="col-md-2">
     <asp:Button ID="Button11" runat="server" Text="Export to Excel" ForeColor="White" OnClick="Button11_Click" CssClass="form-control" BackColor="#ff9900" />
 </div>
         
         
      </div>
  </fieldset>                
    <br />
    <asp:GridView ID="GriedviewStudent" runat="server" AlternatingRowStyle-CssClass="danger" DataKeyNames="ST NO_" PageSize="10" OnPageIndexChanging="GriedviewStudent_PageIndexChanging" AllowPaging="true" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" Visible="true">
        <PagerSettings Mode="NumericFirstLast" />
        <PagerStyle CssClass="myclass" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <%# Container.DataItemIndex +1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ST.No" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <asp:Label ID="lblemployeecode" runat="server" Text='<%# Bind("[ST NO_]") %>'></asp:Label>
                    <asp:HiddenField ID="Hfemployeecode" Value='<%# Eval("[ST NO_]") %>' runat="server" />
                    <asp:HiddenField ID="Hfhodname" Value='<%# Eval("[ST NO_]") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Enrollement No" ItemStyle-Width="4%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
             <ItemTemplate>
                 <asp:Label ID="lblEnrollementNo" runat="server" Text='<%# Eval("[Enrollement No]") %>' Style="text-transform: uppercase;"></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>
            <asp:TemplateField HeaderText="View" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkbutton" runat="server" OnClick="lnkbutton_Click">View</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="4%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                <ItemTemplate>
                    <asp:Label ID="lblStudentname" runat="server" Text='<%# Eval("[Student Name]") %>' Style="text-transform: uppercase;"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Father's Name" ItemStyle-Width="4%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                <ItemTemplate>
                    <asp:Label ID="lblfathername" runat="server" Text='<%# Eval("FatherName") %>' Style="text-transform: uppercase;"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <%-- <asp:TemplateField HeaderText="Father's Name" ItemStyle-Width="2%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblfathername" runat="server" Text='<%# Eval("Father's Name]") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>--%>

            <asp:TemplateField HeaderText="Course" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <asp:Label ID="lblCourse" runat="server" Text='<%#Eval("Programme") %>' Style="text-transform: uppercase;"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="College" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <asp:Label ID="lblCollege" runat="server" Text='<%#Eval("College/Dept") %>' Style="text-transform: uppercase;"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("No Status") %>' Style="text-transform: uppercase;"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>


        </Columns>
        <EmptyDataTemplate>No Record To Display</EmptyDataTemplate>
    </asp:GridView>

    <div>
        <asp:Panel ID="pnlGridViewdata" CssClass="modalPopup" Width="82%" runat="server" ScrollBars="Auto" Height="100%" Style="display: none;">

            <div class="close">
                <asp:Button ID="btnclose" runat="server" OnClick="btnclose_Click" Text="X" ForeColor="Red" BackColor="White" />
            </div>
            <fieldset class="boxBody" style="text-align: center; border-color: black; background-color: black;">
                <asp:Label ID="Label14" runat="server" Text=" Student No Dues" Font-Size="15pt" ForeColor="White" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

            </fieldset>


            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

            <fieldset class="boxBody">


                <div style="width: 100%; margin-bottom: 10px; margin-left: 1%; margin-right: 1%; margin-top: 5px;">

                    <table style="width: 98%;">
                        <tr>
                            <td style="width: 1%"></td>
                            <td style="width: 12%; text-align: left">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/UPDATEDLOGO.jpg" Width="55%" />
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

                    </table>
                </div>
            </fieldset>
            <fieldset class="boxBody" style="text-align: center; border-color: black; background-color: black;">
                <asp:Label ID="Label1" runat="server" Text="No Dues Certificate" Font-Size="15pt" ForeColor="White" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
            </fieldset>
            <div id="divGeneralBodyenrollmentform">
                <fieldset class="boxBodyInner">
                    <div class="form-horizontal">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large">Enrollement No:</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="UserId" runat="server" Enabled="false" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large">ST.No:</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtSection" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large">Student Name:</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtstudentName" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large">Father's Name:</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtfathername" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large">College/Dept:</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtcollegedept" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large">Programme:</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtPrograme" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large">Tel/Mobile:</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtmobile" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large">Email-ID:</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtemailid" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large">Gender:</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtgender" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-2">
                                            <label style="width: 200px; font: bold; color: black; font-size: large">No Dues Id:</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtnoduesid" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <asp:HiddenField ID="hdfSTNO" runat="server" />
                                         <%-- Sanjay Jain--%>
                                           <div class="col-md-2">
                                           <label style="width: 200px; font: bold; color: black; font-size: large">Apply Date:</label>
                                       </div>
                                       <div class="col-md-2">
                                           <asp:TextBox ID="txtnoduesApplyDate" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                       </div>
                                         <%-- Sanjay Jain--%>
                                    </div>
                              </div>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </div>
            <br />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div id="divverifydat" runat="server">

                        <asp:Table class="boxBody" Width="100%" Style="border: 1px solid; text-align: center" ID="tblData" runat="server">
                            <asp:TableRow>
                                <asp:TableHeaderCell Style="border: 1px solid; text-align: center">
                                    <asp:Label ID="Label7" runat="server" Font-Bold="true" Text="S.NO."></asp:Label>
                                </asp:TableHeaderCell>

                                <asp:TableHeaderCell Style="border: 1px solid; text-align: center">
                                    <asp:Label ID="Label8" runat="server" Font-Bold="true" Text="PARTICULARS"></asp:Label>
                                </asp:TableHeaderCell>
                                <%-- <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="Label9" runat="server" Text="Status of Dues"></asp:Label>
                    </asp:TableCell>--%>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label10" runat="server" Font-Bold="true" Text="NAME"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label3" runat="server" Font-Bold="true" Text="DESIGNATION"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label4" runat="server" Font-Bold="true" Text="REMARK"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label23" runat="server" Font-Bold="true" Text="PENDING AMOUNT"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label31" runat="server" Font-Bold="true" Text="ACTION"></asp:Label>
                                </asp:TableCell>
                              <%--  Sanjay Jain--%>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label64" runat="server" Font-Bold="true" Text="Approved Date"></asp:Label>
                                </asp:TableCell>
                                <%--  Sanjay Jain--%>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">1.</asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="LabelR1" runat="server" Font-Bold="true" Text="Department Library"></asp:Label>
                                </asp:TableCell>
                                 <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lbldepartmentlib" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                    <asp:TextBox ID="lbldepartmenthnamecode" runat="server" Visible="false"></asp:TextBox>
                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lbldepartmentlibdeg" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                    <asp:TextBox ID="TextBox6" runat="server" Visible="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:CheckBox ID="CheckBox1" OnCheckedChanged="CheckBox1_CheckedChanged" Font-Bold="true" AutoPostBack="true" Enabled="false" runat="server" />
                                    <asp:TextBox ID="txtremarkdeptlibrary" runat="server" Enabled="false" Font-Bold="true" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:TextBox ID="txtpendingamountdeptlibrary" Font-Bold="true" runat="server" onchange="txtHSpercentage()" onkeypress="return numeric(event)" Enabled="false" BorderColor="Black"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="regexValidator" runat="server" ControlToValidate="txtpendingamountdeptlibrary" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Button ID="btn_DepartmentLib" runat="server" Enabled="false" OnClick="btn_DepartmentLib_Click" Text="Submit" Height="25px" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />
                                </asp:TableCell>
                                    <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="txtdatedeptlibrary" runat="server" Font-Bold="true" Enabled="true" Text=""></asp:Label>
                                   </asp:TableCell>
                                <asp:TableCell>
                                    <div>
                                        <asp:TextBox ID="txtdepartmentlibID" runat="server" Width="50px" Visible="false"></asp:TextBox>
                                    </div>
                                </asp:TableCell>                             
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">2.</asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label5" runat="server" Font-Bold="true" Text="Central Library"></asp:Label>
                                    <asp:TextBox ID="txtcentrallibcode" Font-Bold="true" runat="server" Visible="false"></asp:TextBox>
                                </asp:TableCell>
                                <%--<asp:TableCell Style="border: 1px solid">

                        <%--<asp:DropDownList ID="DropDownList2" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" AutoPostBack="true"  runat="server" Enabled="false" Height="28px">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="No Dues" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Pending" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>--%>


                                <asp:TableCell Style="border: 1px solid">

                                    <asp:Label ID="lblcentlibname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>


                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">

                                    <asp:Label ID="lblcentlibdeg" runat="server" Enabled="false" Font-Bold="true" Text=""></asp:Label>
                                    <asp:TextBox ID="TextBox8" runat="server" Visible="false" BorderColor="Black"></asp:TextBox>

                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:CheckBox ID="CheckBox3" OnCheckedChanged="CheckBox3_CheckedChanged" Font-Bold="true" AutoPostBack="true" Enabled="false" runat="server" />
                                    <asp:TextBox ID="txtremarkcentrallib" runat="server" Enabled="false" Font-Bold="true" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:TextBox ID="txtpendingamountcentrallibrary" Font-Bold="true" runat="server" onchange="txtHSpercentage()" onkeypress="return numeric(event)" Enabled="false" BorderColor="Black"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtpendingamountcentrallibrary" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>

                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Button ID="btn_Centrallib" runat="server" Enabled="false" Text="Submit" OnClick="btn_Centrallib_Click" Height="25px" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />
                                </asp:TableCell>
                                 <%--  Sanjay Jain--%>
                                   <asp:TableCell Style="border: 1px solid">
                                   <asp:Label ID="txtdateCentralLibrary" runat="server" Font-Bold="true" Enabled="true" Text=""></asp:Label>
                                  </asp:TableCell>
                                 <%--  Sanjay Jain--%>
                                <asp:TableCell>
                                    <div>
                                        <asp:TextBox ID="txtCentrallibID" runat="server" Width="50px" Visible="false"></asp:TextBox>
                                    </div>
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">3.</asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label12" runat="server" Font-Bold="true" Text="College/Department Laboratory"></asp:Label>
                                </asp:TableCell>

                                <%--<asp:TableCell Style="border: 1px solid">
                        <asp:DropDownList ID="DropDownList5" runat="server" OnSelectedIndexChanged="DropDownList5_SelectedIndexChanged" AutoPostBack="true" Enabled="false" Height="28px">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="No Dues" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Pending" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>--%>


                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lbldeptLaboratory" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                    <asp:TextBox ID="txtdeptlaboratorydegcode" runat="server" Visible="false"></asp:TextBox>
                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lbldeptLaboratorydeg" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                    <asp:TextBox ID="TextBox9" runat="server" Visible="false" BorderColor="Black"></asp:TextBox>

                                </asp:TableCell>


                                <asp:TableCell Style="border: 1px solid">
                                    <asp:CheckBox ID="CheckBox2" OnCheckedChanged="CheckBox2_CheckedChanged" Font-Bold="true" AutoPostBack="true" Enabled="false" runat="server" />
                                    <asp:TextBox ID="txtremarkdepartmentLaboratory" Font-Bold="true" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:TextBox ID="txtpandingamountdeptlaboratory" Font-Bold="true" runat="server" onchange="txtHSpercentage()" onkeypress="return numeric(event)" Enabled="false" BorderColor="Black"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtpandingamountdeptlaboratory" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>

                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Button ID="btn_DeptLaboratory" runat="server" Enabled="false" Text="Submit" OnClick="btn_DeptLaboratory_Click" Height="25px" Font-Bold="true" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
                                </asp:TableCell>
                                
                                 <%--  Sanjay Jain--%>
                                   <asp:TableCell Style="border: 1px solid">
                                   <asp:Label ID="txtdateCollegeDepartmentLaboratory" runat="server" Font-Bold="true" Enabled="true" Text=""></asp:Label>
                                  </asp:TableCell>
                                 <%--  Sanjay Jain--%>
                                <asp:TableCell>
                                    <div>
                                        <asp:TextBox ID="txtidDeptLaboratory" runat="server" Width="50px" Visible="false"></asp:TextBox>
                                    </div>
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                            </asp:TableRow>
                            <asp:TableRow>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">4.</asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label15" runat="server" Font-Bold="true" Text="College/Department Workshop"></asp:Label>
                                    <asp:TextBox ID="txtCollegeDepartmentWorkshopcode" Font-Bold="true" runat="server" Visible="false"></asp:TextBox>
                                </asp:TableCell>
                                <%--<asp:TableCell Style="border: 1px solid">

                        <%--<asp:DropDownList ID="DropDownList8" runat="server" OnSelectedIndexChanged="DropDownList8_SelectedIndexChanged" AutoPostBack="true" Enabled="false" Height="28px">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                             <asp:ListItem Text="No Dues" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Pending" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>--%>


                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblcollegeworkname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblcollegeworkdeg" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                    <asp:TextBox ID="TextBox10" runat="server" Visible="false" Font-Bold="true" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:CheckBox ID="CheckBox4" OnCheckedChanged="CheckBox4_CheckedChanged" Font-Bold="true" AutoPostBack="true" Enabled="false" runat="server" />
                                    <asp:TextBox ID="txtremarkdepartmentwork" runat="server" Font-Bold="true" Enabled="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:TextBox ID="TextBox1" runat="server" Enabled="false" Font-Bold="true" onchange="txtHSpercentage()" onkeypress="return numeric(event)" BorderColor="Black"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TextBox1" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>

                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Button ID="Button8" runat="server" Enabled="false" Text="Submit" OnClick="Button8_Click" Height="25px" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />
                                </asp:TableCell>
                                 <%--  Sanjay Jain--%>
                                   <asp:TableCell Style="border: 1px solid">
                                   <asp:Label ID="txtdateCollegeDepartmentWorkshop" runat="server" Font-Bold="true" Enabled="true" Text=""></asp:Label>
                                  </asp:TableCell>
                                 <%--  Sanjay Jain--%>
                                <asp:TableCell>
                                    <div>
                                        <asp:TextBox ID="txtidcollegework" runat="server" Width="50px" Visible="false"></asp:TextBox>
                                    </div>
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">5.</asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label16" runat="server" Font-Bold="true" Text="Hostel"></asp:Label>
                                </asp:TableCell>
                                <%--<asp:TableCell Style="border: 1px solid">

                        <%--<asp:DropDownList ID="DropDownList9" runat="server" OnSelectedIndexChanged="DropDownList9_SelectedIndexChanged" AutoPostBack="true" Enabled="false" Height="28px">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                             <asp:ListItem Text="No Dues" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Pending" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>--%>


                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblhostelsname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblhosteldeg" runat="server" Enabled="false" Font-Bold="true" Text=""></asp:Label>
                                    <asp:TextBox ID="TextBox11" runat="server" Visible="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:CheckBox ID="CheckBox5" OnCheckedChanged="CheckBox5_CheckedChanged" Font-Bold="true" AutoPostBack="true" Enabled="false" runat="server" />
                                    <asp:TextBox ID="txtremarkhostel" runat="server" Font-Bold="true" Enabled="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:TextBox ID="TextBox2" runat="server" Enabled="false" Font-Bold="true" onchange="txtHSpercentage()" onkeypress="return numeric(event)" BorderColor="Black"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TextBox2" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>

                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Button ID="Btn_Hostel" runat="server" Enabled="false" Text="Submit" OnClick="Btn_Hostel_Click" Height="25px" Font-Bold="true" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
                                </asp:TableCell>
                                 <%--  Sanjay Jain--%>
                                   <asp:TableCell Style="border: 1px solid">
                                   <asp:Label ID="txtdateHostel" runat="server" Font-Bold="true" Enabled="true" Text=""></asp:Label>
                                  </asp:TableCell>
                                 <%--  Sanjay Jain--%>
                                <asp:TableCell>
                                    <div>
                                        <asp:TextBox ID="txtidhostel" runat="server" Width="50px" Visible="false"></asp:TextBox>
                                    </div>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">6.</asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label22" runat="server" Font-Bold="true" Text="Electricty Department"></asp:Label>
                                </asp:TableCell>
                                <%--<asp:TableCell Style="border: 1px solid">

                        <%--<asp:DropDownList ID="drpelectrictydepartment" runat="server" OnSelectedIndexChanged="drpelectrictydepartment_SelectedIndexChanged" AutoPostBack="true" Enabled="false" Height="28px">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="No Dues" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Pending" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>--%>


                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblelectrictydeptname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblelectrictydeptdeg" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                    <asp:TextBox ID="TextBox14" runat="server" Visible="false" Font-Bold="true" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:CheckBox ID="CheckBox7" OnCheckedChanged="CheckBox7_CheckedChanged" Font-Bold="true" AutoPostBack="true" Enabled="false" runat="server" />
                                    <asp:TextBox ID="txtelectrictydeptremark" runat="server" Font-Bold="true" Enabled="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:TextBox ID="TextBox4" runat="server" Enabled="false" Font-Bold="true" onchange="txtHSpercentage()" onkeypress="return numeric(event)" BorderColor="Black"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="TextBox4" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>

                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Button ID="Button1" runat="server" Enabled="false" Text="Submit" OnClick="Button1_Click" Height="25px" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />
                                </asp:TableCell>
                                 <%--  Sanjay Jain--%>
                                   <asp:TableCell Style="border: 1px solid">
                                   <asp:Label ID="txtdateElectrictyDepartment" runat="server" Font-Bold="true" Enabled="true" Text=""></asp:Label>
                                  </asp:TableCell>
                                 <%--  Sanjay Jain--%>
                                <asp:TableCell>
                                    <div>
                                        <asp:TextBox ID="txtelectrictid" runat="server" Width="50px" Visible="false"></asp:TextBox>
                                    </div>
                                </asp:TableCell>

                            </asp:TableRow>


                            <asp:TableRow>

                                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">7.</asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label21" runat="server" Font-Bold="true" Text="Sports"></asp:Label>
                                </asp:TableCell>
                                <%--<asp:TableCell Style="border: 1px solid">
                        <asp:DropDownList ID="DrpSport" runat="server" OnSelectedIndexChanged="DrpSport_SelectedIndexChanged" AutoPostBack="true" Enabled="false" Height="28px">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="No Dues" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Pending" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>--%>


                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblsportname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblsportdeg" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                    <asp:TextBox ID="TextBox15" runat="server" Visible="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:CheckBox ID="CheckBox8" OnCheckedChanged="CheckBox8_CheckedChanged" Font-Bold="true" AutoPostBack="true" Enabled="false" runat="server" />
                                    <asp:TextBox ID="txtremarksportic" runat="server" Font-Bold="true" Enabled="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:TextBox ID="TextBox5" runat="server" Enabled="false" Font-Bold="true" onchange="txtHSpercentage()" onkeypress="return numeric(event)" BorderColor="Black"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="TextBox5" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>

                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Button ID="Button14" runat="server" Enabled="false" Text="Submit" OnClick="Button14_Click" Height="25px" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />
                                </asp:TableCell>
                                <%--  Sanjay Jain--%>
                                   <asp:TableCell Style="border: 1px solid">
                                   <asp:Label ID="txtdateSports" runat="server" Font-Bold="true" Enabled="true" Text=""></asp:Label>
                                  </asp:TableCell>
                                 <%--  Sanjay Jain--%>
                                <asp:TableCell>
                                    <div>
                                        <asp:TextBox ID="txtSportID" runat="server" Width="50px" Visible="false"></asp:TextBox>
                                    </div>
                                </asp:TableCell>
                            </asp:TableRow>

                            <%--Sanjay--%>
                                 <asp:TableRow>

            <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">8.</asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="Label821" runat="server" Font-Bold="true" Text="IT Department"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="lblITname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:Label ID="lblITdeg" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                <asp:TextBox ID="TextBox815" runat="server" Visible="false" BorderColor="Black"></asp:TextBox>
            </asp:TableCell>

            <asp:TableCell Style="border: 1px solid">
                <asp:CheckBox ID="CheckBox88" OnCheckedChanged="CheckBox88_CheckedChanged" Font-Bold="true" AutoPostBack="true" Enabled="false" runat="server" />
                <asp:TextBox ID="txtremarkIT" runat="server" Font-Bold="true" Enabled="false" BorderColor="Black"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:TextBox ID="TextBox858" runat="server" Enabled="false" Font-Bold="true" onchange="txtHSpercentage()" onkeypress="return numeric(event)" BorderColor="Black"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator31" runat="server" ControlToValidate="TextBox858" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>

            </asp:TableCell>
            <asp:TableCell Style="border: 1px solid">
                <asp:Button ID="Button814" runat="server" Enabled="false" Text="Submit" OnClick="Button814_Click" Height="25px" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />
            </asp:TableCell>
            <%--  Sanjay Jain--%>
               <asp:TableCell Style="border: 1px solid">
               <asp:Label ID="txtdateITs" runat="server" Font-Bold="true" Enabled="true" Text=""></asp:Label>
              </asp:TableCell>
             <%--  Sanjay Jain--%>
            <asp:TableCell>
                <div>
                    <asp:TextBox ID="txtITID" runat="server" Width="50px" Visible="false"></asp:TextBox>
                </div>
            </asp:TableCell>
        </asp:TableRow>
                                 <asp:TableRow>

    <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">9.</asp:TableCell>
    <asp:TableCell Style="border: 1px solid">
        <asp:Label ID="Label821Security" runat="server" Font-Bold="true" Text="Security Department"></asp:Label>
    </asp:TableCell>
    <asp:TableCell Style="border: 1px solid">
        <asp:Label ID="lblSecurityname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
    </asp:TableCell>

    <asp:TableCell Style="border: 1px solid">
        <asp:Label ID="lblSecuritydeg" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
        <asp:TextBox ID="TextBox815Security" runat="server" Visible="false" BorderColor="Black"></asp:TextBox>
    </asp:TableCell>

    <asp:TableCell Style="border: 1px solid">
        <asp:CheckBox ID="CheckBox88Security" OnCheckedChanged="CheckBox88Security_CheckedChanged" Font-Bold="true" AutoPostBack="true" Enabled="false" runat="server" />
        <asp:TextBox ID="txtremarkSecurity" runat="server" Font-Bold="true" Enabled="false" BorderColor="Black"></asp:TextBox>
    </asp:TableCell>
    <asp:TableCell Style="border: 1px solid">
        <asp:TextBox ID="TextBox858Security" runat="server" Enabled="false" Font-Bold="true" onchange="txtHSpercentage()" onkeypress="return numeric(event)" BorderColor="Black"></asp:TextBox>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator31Security" runat="server" ControlToValidate="TextBox858Security" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>

    </asp:TableCell>
    <asp:TableCell Style="border: 1px solid">
        <asp:Button ID="Button814Security" runat="server" Enabled="false" Text="Submit" OnClick="Button814Security_Click" Height="25px" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />
    </asp:TableCell>
    <%--  Sanjay Jain--%>
       <asp:TableCell Style="border: 1px solid">
       <asp:Label ID="txtdateSecurity" runat="server" Font-Bold="true" Enabled="true" Text=""></asp:Label>
      </asp:TableCell>
     <%--  Sanjay Jain--%>
    <asp:TableCell>
        <div>
            <asp:TextBox ID="txtSecurityID" runat="server" Width="50px" Visible="false"></asp:TextBox>
        </div>
    </asp:TableCell>
</asp:TableRow>
                            <%-- <asp:TableCell Style="border: 1px solid">8.</asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="Label18" runat="server" Text="Alumni Fee"></asp:Label>
                        <asp:TextBox ID="txtalumnifee" runat="server" Visible="false"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">

                        <asp:DropDownList ID="DrpAlumnifee" runat="server" Enabled="false" Height="28px">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Granted" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Not Granted" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Pending At Department" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Pending" Value="4"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>


                    <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="lblAlumnifeename" runat="server" Enabled="false" Text=""></asp:Label>
                    </asp:TableCell>

                    <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="lblAlumnifeedeg" runat="server" Enabled="false" Text=""></asp:Label>
                    </asp:TableCell>

                    <asp:TableCell Style="border: 1px solid">
                        <asp:TextBox ID="txtremarkAlumnifee" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:Button ID="Btn_Alumnifee" runat="server" Enabled="false" Text="Submit" Height="25px" OnClick="Btn_Alumnifee_Click" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <div>
                            <asp:TextBox ID="txtidAlumnifee" runat="server" Width="50px" Visible="false"></asp:TextBox>
                        </div>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>--%>
                               <asp:TableRow>

    <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">10.</asp:TableCell>
    <asp:TableCell Style="border: 1px solid">
        <asp:Label ID="Label821Examination" runat="server" Font-Bold="true" Text="Examination Department"></asp:Label>
    </asp:TableCell>
    <asp:TableCell Style="border: 1px solid">
        <asp:Label ID="lblExaminationname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
    </asp:TableCell>

    <asp:TableCell Style="border: 1px solid">
        <asp:Label ID="lblExaminationdeg" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
        <asp:TextBox ID="TextBox815Examination" runat="server" Visible="false" BorderColor="Black"></asp:TextBox>
    </asp:TableCell>

    <asp:TableCell Style="border: 1px solid">
        <asp:CheckBox ID="CheckBox88Examination" OnCheckedChanged="CheckBox88Examination_CheckedChanged" Font-Bold="true" AutoPostBack="true" Enabled="false" runat="server" />
        <asp:TextBox ID="txtremarkExamination" runat="server" Font-Bold="true" Enabled="false" BorderColor="Black"></asp:TextBox>
    </asp:TableCell>
    <asp:TableCell Style="border: 1px solid">
        <asp:TextBox ID="TextBox858Examination" runat="server" Enabled="false" Font-Bold="true" onchange="txtHSpercentage()" onkeypress="return numeric(event)" BorderColor="Black"></asp:TextBox>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator31Examination" runat="server" ControlToValidate="TextBox858Examination" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>

    </asp:TableCell>
    <asp:TableCell Style="border: 1px solid">
        <asp:Button ID="Button814Examination" runat="server" Enabled="false" Text="Submit" OnClick="Button814Examination_Click" Height="25px" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />
    </asp:TableCell>
       <asp:TableCell Style="border: 1px solid">
       <asp:Label ID="txtdateExamination" runat="server" Font-Bold="true" Enabled="true" Text=""></asp:Label>
      </asp:TableCell>
     <asp:TableCell>
        <div>
            <asp:TextBox ID="txtExaminationID" runat="server" Width="50px" Visible="false"></asp:TextBox>
        </div>
    </asp:TableCell>
</asp:TableRow>


                                                        


                            <asp:TableRow>
                                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">11.</asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label19" runat="server" Font-Bold="true" Text="Other Fee"></asp:Label>
                                    <asp:TextBox ID="txtaccountemployeecode" runat="server" Visible="false"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblaccountname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblaccountdeg" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                    <asp:TextBox ID="TextBox16" runat="server" Visible="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:CheckBox ID="CheckBox9" OnCheckedChanged="CheckBox9_CheckedChanged" Font-Bold="true" AutoPostBack="true" Enabled="false" runat="server" />
                                    <asp:TextBox ID="txtotherfee" runat="server" Font-Bold="true" Enabled="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:TextBox ID="TextBox7" runat="server" Font-Bold="true" onchange="txtHSpercentage()" onkeypress="return numeric(event)" Enabled="false" BorderColor="Black"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="TextBox7" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>

                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Button ID="Button3" runat="server" Enabled="false" Text="Submit" OnClick="Btn_otherfee_Click" Height="25px" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />

                                </asp:TableCell>
                                <%--  Sanjay Jain--%>
                                   <asp:TableCell Style="border: 1px solid">
                                   <asp:Label ID="txtdateOtherFee" runat="server" Font-Bold="true" Enabled="true" Text=""></asp:Label>
                                  </asp:TableCell>
                                 <%--  Sanjay Jain--%>
                                <asp:TableCell>
                                    <div>
                                        <asp:TextBox ID="txtifotherfeeid" runat="server" Width="50px" Visible="false"></asp:TextBox>
                                    </div>

                                </asp:TableCell>

                            </asp:TableRow>

 
                            <%--<asp:TableRow>

                    <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="Label20" runat="server" Text="Institution Fee"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">

                        <asp:DropDownList ID="DropDownList13" runat="server" OnSelectedIndexChanged="DropDownList13_SelectedIndexChanged" AutoPostBack="true" Enabled="false" Height="28px">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="No Dues" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Pending" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>


                    <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="lblInstitutionFeename" runat="server" Enabled="false" Text=""></asp:Label>
                    </asp:TableCell>

                    <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="lblInstitutionFeedeg" runat="server" Enabled="false" Text=""></asp:Label>
                    </asp:TableCell>

                    <asp:TableCell Style="border: 1px solid">
                        <asp:CheckBox ID="CheckBox9" Enabled="false" runat="server" />
                        <asp:TextBox ID="txtremarkInstitutionFee" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:TextBox ID="TextBox6" runat="server" Enabled="false" onchange="txtHSpercentage()" onkeypress="return numeric(event)" BorderColor="Black"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:Button ID="Btn_InstitutionFee" runat="server" Enabled="false" Text="Submit" OnClick="Btn_InstitutionFee_Click" Height="25px" Font-Bold="true" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <div>
                            <asp:TextBox ID="txtIDInstitute" runat="server" Width="50px" Visible="false"></asp:TextBox>
                        </div>
                    </asp:TableCell>
                </asp:TableRow>--%>
                            <%--<asp:TableRow>

                    <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="Label2" runat="server" Text="Exam Fee"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">

                        <asp:DropDownList ID="DrpExamFee" runat="server" OnSelectedIndexChanged="DrpExamFee_SelectedIndexChanged" AutoPostBack="true" Enabled="false" Height="28px">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="No Dues" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Pending" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>


                    <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="lblExamFeename" runat="server" Enabled="false" Text=""></asp:Label>
                    </asp:TableCell>

                    <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="lblExamFeeDeg" runat="server" Enabled="false" Text=""></asp:Label>
                    </asp:TableCell>

                    <asp:TableCell Style="border: 1px solid">
                        <asp:CheckBox ID="CheckBox10" Enabled="false" runat="server" />
                        <asp:TextBox ID="txtremarkExamFeename" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:TextBox ID="TextBox7" runat="server" Enabled="false" onchange="txtHSpercentage()" onkeypress="return numeric(event)" BorderColor="Black"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:Button ID="Btn_ExamFee" runat="server" Enabled="false" OnClick="Btn_ExamFee_Click" Text="Submit" Height="25px" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <div>
                            <asp:TextBox ID="txtidexamfee" runat="server" Width="50px" Visible="false"></asp:TextBox>
                        </div>
                    </asp:TableCell>
                </asp:TableRow>--%>
                            <%--<asp:TableRow>

                    <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="Label6" runat="server" Text="Hostel Fee"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">

                        <asp:DropDownList ID="DrpHostelFee" runat="server" OnSelectedIndexChanged="DrpHostelFee_SelectedIndexChanged" AutoPostBack="true" Enabled="false" Height="28px">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                             <asp:ListItem Text="No Dues" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Pending" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>


                    <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="lblHostelFeename" runat="server" Enabled="false" Text=""></asp:Label>
                    </asp:TableCell>

                    <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="lblHostelFeedeg" runat="server" Enabled="false" Text=""></asp:Label>
                    </asp:TableCell>

                    <asp:TableCell Style="border: 1px solid">
                        <asp:CheckBox ID="CheckBox11" Enabled="false" runat="server" />
                        <asp:TextBox ID="txtremarkHostelFee" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:TextBox ID="TextBox8" runat="server" Enabled="false" onchange="txtHSpercentage()" onkeypress="return numeric(event)" BorderColor="Black"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:Button ID="Btn_HostelFee" runat="server" Enabled="false" Text="Submit" OnClick="Btn_HostelFee_Click" Height="25px" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <div>
                            <asp:TextBox ID="txtidhostelfee" runat="server" Width="50px" Visible="false"></asp:TextBox>
                        </div>
                    </asp:TableCell>

                </asp:TableRow>--%>
                            <%--<asp:TableRow>

                    <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="Label11" runat="server" Text="Transport Fee"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">

                        <asp:DropDownList ID="DrpTransportFee" OnSelectedIndexChanged="DrpTransportFee_SelectedIndexChanged" AutoPostBack="true" runat="server" Enabled="false" Height="28px">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                             <asp:ListItem Text="No Dues" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Pending" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>


                    <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="lblTransportFee" runat="server" Enabled="false" Text=""></asp:Label>
                    </asp:TableCell>

                    <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="lblTransportFeedeg" runat="server" Enabled="false" Text=""></asp:Label>
                    </asp:TableCell>

                    <asp:TableCell Style="border: 1px solid">
                        <asp:CheckBox ID="CheckBox12" Enabled="false" runat="server" />
                        <asp:TextBox ID="txtremarkTransportFee" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:TextBox ID="TextBox9" runat="server" Enabled="false" onchange="txtHSpercentage()" onkeypress="return numeric(event)" BorderColor="Black"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:Button ID="Btn_TransportFee" runat="server" Enabled="false" OnClick="Btn_TransportFee_Click" Text="Submit" Height="25px" Font-Bold="true" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <div>
                            <asp:TextBox ID="txtidTransportFee" runat="server" Width="50px" Visible="false"></asp:TextBox>
                        </div>
                    </asp:TableCell>
                </asp:TableRow>--%>
                            <%-- <asp:TableRow>--%>

                            <%-- <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="Label13" runat="server" Text="DisciplinaryFine"></asp:Label>
                    </asp:TableCell>--%>
                            <%--<asp:TableCell Style="border: 1px solid">

                        <asp:DropDownList ID="DrpDisciplinaryFine" runat="server" OnSelectedIndexChanged="DrpDisciplinaryFine_SelectedIndexChanged" AutoPostBack="true" Enabled="false" Height="28px">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                             <asp:ListItem Text="No Dues" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Pending" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>--%>


                            <%--<asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="lblDisciplinaryFinename" runat="server" Enabled="false" Text=""></asp:Label>
                    </asp:TableCell>

                    <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="lblDisciplinaryFinedeg" runat="server" Enabled="false" Text=""></asp:Label>
                    </asp:TableCell>

                    <asp:TableCell Style="border: 1px solid">
                        <asp:CheckBox ID="CheckBox13" Enabled="false" runat="server" />
                        <asp:TextBox ID="txtremarkDisciplinaryFine" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:TextBox ID="TextBox10" runat="server" Enabled="false" onchange="txtHSpercentage()" onkeypress="return numeric(event)" BorderColor="Black"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:Button ID="Btn_DisciplinaryFine" runat="server" Enabled="false" OnClick="Btn_DisciplinaryFine_Click" Text="Submit" Height="25px" Font-Bold="true" BackColor="#ff9900" ForeColor="Black" BorderColor="Black" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <div>
                            <asp:TextBox ID="txtID" runat="server" Width="50px" Visible="false"></asp:TextBox>
                        </div>

                    </asp:TableCell>


                </asp:TableRow>--%>

                            <%--<asp:TableRow>

                    <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="Label18" runat="server" Text="Other Fee"></asp:Label>
                    </asp:TableCell>
                    <%--<asp:TableCell Style="border: 1px solid">

                        <asp:DropDownList ID="drpotherfeee" runat="server" OnSelectedIndexChanged="drpotherfeee_SelectedIndexChanged" AutoPostBack="true" Enabled="false" Height="28px">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                             <asp:ListItem Text="No Dues" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Pending" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>


                    <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="lblotherfinename" runat="server" Enabled="false" Text=""></asp:Label>
                    </asp:TableCell>

                    <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="lblotherfeedeg" runat="server" Enabled="false" Text=""></asp:Label>
                    </asp:TableCell>

                    <asp:TableCell Style="border: 1px solid">
                        <asp:CheckBox ID="CheckBox14" Enabled="false" runat="server" />
                        <asp:TextBox ID="txtotherfee" runat="server" Enabled="false" BorderColor="Black"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:TextBox ID="TextBox11" runat="server" onchange="txtHSpercentage()" onkeypress="return numeric(event)" Enabled="false" BorderColor="Black"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Style="border: 1px solid">
                        <asp:Button ID="Btn_otherfee" runat="server" Enabled="false" Text="Submit" OnClick="Btn_otherfee_Click" Height="25px" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />
                           
                    </asp:TableCell>

                    <asp:TableCell>
                        <div>
                            <asp:TextBox ID="txtifotherfeeid" runat="server" Width="50px" Visible="false"></asp:TextBox>
                        </div>

                    </asp:TableCell>

                </asp:TableRow>--%>

                            <asp:TableRow>
                                <asp:TableCell Style="border: 1px solid; text-align: right; vertical-align: middle" ColumnSpan="5">
                                    <asp:Label ID="Label24" runat="server" Font-Size="Large" Font-Bold="true" ForeColor="Red" Text="Total Pending Amount : "></asp:Label>
                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:TextBox ID="TextBox12" runat="server" Font-Bold="true" Enabled="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid; vertical-align: bottom">
                                    <asp:Button ID="Button2" runat="server" Text="Verify" OnClick="Button2_Click" Height="25px" BackColor="#ff9900" Enabled="false" Font-Bold="true" ForeColor="Black" BorderColor="Black" />
                                </asp:TableCell>
                            </asp:TableRow>
                                <asp:TableRow>
    <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">12.</asp:TableCell>
    <asp:TableCell Style="border: 1px solid">
        <asp:Label ID="LabelInternshipStatus" runat="server" Font-Bold="true" Text="Internship"></asp:Label>
    </asp:TableCell>
    <asp:TableCell Style="border: 1px solid">
        <asp:Label ID="Label17InternshipStatus" runat="server" Font-Bold="true"></asp:Label>
    </asp:TableCell>

    <asp:TableCell Style="border: 1px solid">
        <asp:Label ID="Label42InternshipStatus" runat="server" Font-Bold="true" Text="Internship Status"></asp:Label>
    </asp:TableCell>       
         <asp:TableCell ColumnSpan="2" Style="border: 1px solid"> 
             <asp:DropDownList ID="txtInternshipStatus" runat="server" BorderColor="Black" CssClass="form-control" style="width: 375px;margin-left: 60px;">
                  <asp:ListItem Text="Select" Value=""></asp:ListItem>
                  <asp:ListItem Text="Not Applicable" Value="Not Applicable"></asp:ListItem>
                  <asp:ListItem Text="Internship Done" Value="Internship Done"></asp:ListItem>
                  <asp:ListItem Text="Internship Not Done" Value="Internship Not Done"></asp:ListItem>
              </asp:DropDownList>
       </asp:TableCell>
    <asp:TableCell Style="border: 1px solid">
        <asp:Button ID="InternshipStatus" runat="server" Enabled="false" Text="Submit" OnClick="InternshipStatus_Click" Height="25px" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />
    </asp:TableCell>
     <asp:TableCell Style="border: 1px solid"> 
          <asp:Label ID="txtInternshipStatusDate" runat="server" Font-Bold="true"></asp:Label>
     </asp:TableCell>
</asp:TableRow>

                        </asp:Table>
                        <br />
                        <fieldset style="display:none;">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label>
                                        Certified that Mr./Ms&nbsp;&nbsp;<asp:Label ID="lblcertifiedname" Font-Bold="true" Font-Underline="true" runat="server"></asp:Label>
                                        has been  bona fide student of the college and has completed the studies in between in the Year<asp:Label ID="lblYear" runat="server" Font-Bold="true" Font-Underline="true"></asp:Label>
                                        .
                                    </label>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label>The "NO DUES CERTIFICATE" is issued to him/her and forwarded to Exam. Branch for final declaration of exam results.</label>
                                </div>
                            </div>
                            <br />
                            <br />

                            <div class="form-group">
                                <div class="col-md-1">
                                    <label>Date:</label>
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtdatedirectorprincipaldate" runat="server" BorderColor="Black" Width="200px" Enabled="false" onkeydown="return false;" autocomplete="off" oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtdatedirectorprincipaldate" Format="dd MMM yyyy"></asp:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtdatedirectorprincipaldate" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="aspx"></asp:RequiredFieldValidator>

                                </div>
                            </div>

                        </fieldset>
                        <div class="form-group">
                            <div class="col-md-5">
                                <label></label>
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btn_Save" runat="server" Width="200px" Text="Final Approval by COE" Height="40px" Font-Bold="true" ForeColor="white" BackColor="Green" OnClick="btn_Save_Click" Visible="false" CssClass="form-control" />
                            </div>
                            <br />
                            <br />
                            <%--<asp:TextBox ID="txtBarcode" runat="server"></asp:TextBox>--%>
                            <%-- <asp:Button ID="btnGenerate" runat="server" Text="Generate" OnClick="btnGenerate_Click"/>--%>
                        </div>


                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <asp:Button ID="btnDummy" runat="server" Style="display: none;" />
        <asp:ModalPopupExtender ID="GridViewdata" runat="server" TargetControlID="btnDummy"
            PopupControlID="pnlGridViewdata" BackgroundCssClass="modalBackground" />
    </div>
   

    <asp:Panel ID="Panel1" CssClass="modalPopup" Width="82%" runat="server" ScrollBars="Both" Height="100%" Style="display: none;">

        <div class="close">
            <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="X" ForeColor="Red" BackColor="White" />
        </div>
        <fieldset class="boxBody" style="text-align: center; border-color: black; background-color: black;">
            <asp:Label ID="Label2" runat="server" Text=" Student No Dues" Font-Size="15pt" ForeColor="White" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

        </fieldset>
        <fieldset class="boxBody">
            <div style="width: 100%; margin-bottom: 10px; margin-left: 1%; margin-right: 1%; margin-top: 5px;">
                <table style="width: 98%;">
                    <tr>
                        <td style="width: 1%"></td>
                        <td style="width: 12%; text-align: left">
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/images/tmulogo.png" Width="50%" />
                        </td>
                        <td style="width: 70%; text-align: center">
                            <strong>
                                <asp:Label ID="Label28" Font-Size="X-Large" Text="TEERTHANKER MAHAVEER MEDICAL COLLEGE & RESEARCH CENTER" Font-Names="Times New Roman" runat="server"></asp:Label></strong>
                            <br />
                            <br />
                            <strong>
                                <asp:Label ID="Label29" Font-Size="Large" Text="(Established under Govt. of U. P. Act No. 30, 2008)" Font-Names="Times New Roman" runat="server"></asp:Label></strong>

                            <br />
                            <br />
                            <strong>
                                <asp:Label ID="Label30" runat="server" Font-Size="Large" Font-Names="Times New Roman" Text="Delhi Road, Moradabad - 244001, (U.P.)"></asp:Label></strong>

                        </td>

                        <td style="width: 10%; text-align: center"></td>
                    </tr>
                </table>
            </div>
        </fieldset>
        <fieldset class="boxBody" style="text-align: center; border-color: black; background-color: black;">
            <asp:Label ID="Label6" runat="server" Text="No Dues Certificate (Interns)" Font-Size="15pt" ForeColor="White" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
        </fieldset>
        <div id="divGeneralBodyenrollmentform1">
            <fieldset class="boxBodyInner">
                <div class="form-horizontal">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class="col-md-2">
                                        <label style="width: 200px; font: bold; color: black; font-size: large">Enrollement No:</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="TextBox17" runat="server" Enabled="false" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="col-md-2">
                                        <label style="width: 200px; font: bold; color: black; font-size: large">ST.No:</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="TextBox18" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label style="width: 200px; font: bold; color: black; font-size: large">Student Name:</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="TextBox19" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-2">
                                        <label style="width: 200px; font: bold; color: black; font-size: large">Father's Name:</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="TextBox20" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label style="width: 200px; font: bold; color: black; font-size: large">College/Dept:</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="TextBox21" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label style="width: 200px; font: bold; color: black; font-size: large">Programme:</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="TextBox22" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-2">
                                        <label style="width: 200px; font: bold; color: black; font-size: large">Tel/Mobile:</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="TextBox23" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label style="width: 200px; font: bold; color: black; font-size: large">Email-ID:</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="TextBox24" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label style="width: 200px; font: bold; color: black; font-size: large">Gender:</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="TextBox25" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-2">
                                        <label style="width: 200px; font: bold; color: black; font-size: large">No Dues Id:</label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="TextBox26" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label style="width: 200px; font: bold; color: black; font-size: large">Apply Date:</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="TextBox117" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </fieldset>
        </div>

         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Table class="boxBody" Width="100%" Style="border: 1px solid; text-align: center" ID="Table1" runat="server">
                     <asp:TableRow>
                                <asp:TableHeaderCell Style="border: 1px solid; text-align: center">
                                    <asp:Label ID="Label9" runat="server" Font-Bold="true" Text="S.NO."></asp:Label>
                                </asp:TableHeaderCell>

                                <asp:TableHeaderCell Style="border: 1px solid; text-align: center">
                                    <asp:Label ID="Label11" runat="server" Font-Bold="true" Text="PARTICULARS"></asp:Label>
                                </asp:TableHeaderCell>
                                <%-- <asp:TableCell Style="border: 1px solid">
                        <asp:Label ID="Label9" runat="server" Text="Status of Dues"></asp:Label>
                    </asp:TableCell>--%>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label13" runat="server" Font-Bold="true" Text="NAME"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label18" runat="server" Font-Bold="true" Text="DESIGNATION"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label20" runat="server" Font-Bold="true" Text="REMARK"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label25" runat="server" Font-Bold="true" Text="PENDING AMOUNT"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label26" runat="server" Font-Bold="true" Text="ACTION"></asp:Label>
                                </asp:TableCell>
                          <%--  Sanjay Jain--%>
                           <asp:TableCell Style="border: 1px solid">
                              <asp:Label ID="Label65" runat="server" Font-Bold="true" Text="Approved Date"></asp:Label>
                          </asp:TableCell>
                          <%--  Sanjay Jain--%>
                            </asp:TableRow>

                        <asp:TableRow>
                                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">1.</asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label34" runat="server" Font-Bold="true" Text="Other Fee"></asp:Label>
                                       <asp:Label ID="lblaccountcode" runat="server" Font-Bold="true"  Visible="false"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label35" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label36" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                    <asp:TextBox ID="TextBox33" runat="server" Visible="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:CheckBox ID="CheckBox11"  Font-Bold="true" OnCheckedChanged="CheckBox11_CheckedChanged" AutoPostBack="true" Enabled="false" runat="server" />
                                    <asp:TextBox ID="TextBox34" runat="server" Font-Bold="true" Enabled="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:TextBox ID="TextBox35" runat="server" Font-Bold="true" onchange="txtHSpercentage1()" onkeypress="return numeric(event)" Enabled="false" BorderColor="Black"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="TextBox7" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>

                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Button ID="Button7" runat="server" Enabled="false" Text="Submit" OnClick="Button7_Click"  Height="25px" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />
                                       
                                </asp:TableCell>
                             <%--  Sanjay Jain--%>
                                <asp:TableCell Style="border: 1px solid">
                                     <asp:Label ID="Label66" runat="server" Font-Bold="true" Text=""></asp:Label>
                                </asp:TableCell>
                             <%--  Sanjay Jain--%>
                                <asp:TableCell>
                                    <div>
                                        <asp:TextBox ID="TextBox36" runat="server" Width="50px" Visible="false"></asp:TextBox>
                                    </div>

                                </asp:TableCell>

                            </asp:TableRow>

                        <asp:TableRow>
                                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">2.</asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label27" runat="server" Font-Bold="true" Text="Central Library"></asp:Label>
                                    <asp:TextBox ID="TextBox27" Font-Bold="true" runat="server" Visible="false"></asp:TextBox>
                                </asp:TableCell>
                            

                                <asp:TableCell Style="border: 1px solid">

                                    <asp:Label ID="Label32" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>


                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">

                                    <asp:Label ID="Label33" runat="server" Enabled="false" Font-Bold="true" Text=""></asp:Label>
                                    <asp:TextBox ID="TextBox28" runat="server" Visible="false" BorderColor="Black"></asp:TextBox>

                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:CheckBox ID="CheckBox10"  Font-Bold="true" OnCheckedChanged="CheckBox10_CheckedChanged" AutoPostBack="true" Enabled="false" runat="server" />
                                    <asp:TextBox ID="TextBox29" runat="server" Enabled="false" Font-Bold="true" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:TextBox ID="TextBox30" Font-Bold="true" runat="server" onchange="txtHSpercentage1()" onkeypress="return numeric(event)" Enabled="false" BorderColor="Black"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtpendingamountcentrallibrary" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>

                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Button ID="Button6" runat="server" Enabled="false" Text="Submit" Height="25px" OnClick="Button6_Click" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />
                                </asp:TableCell>
                             <%--  Sanjay Jain--%>
                            <asp:TableCell Style="border: 1px solid">
                                 <asp:Label ID="Label67" runat="server" Font-Bold="true" Text=""></asp:Label>
                            </asp:TableCell>
                             <%--  Sanjay Jain--%>
                                <asp:TableCell>
                                    <div>
                                        <asp:TextBox ID="TextBox31" runat="server" Width="50px" Visible="false"></asp:TextBox>
                                    </div>
                                </asp:TableCell>
                            </asp:TableRow>

                        <asp:TableRow>
                                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">3.</asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label37" runat="server" Font-Bold="true" Text="Hostel"></asp:Label>
                                       <asp:Label ID="lblhostelemployeecode" runat="server" Font-Bold="true"  Visible="false"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblHostalname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblHostalDesignation" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                    <asp:TextBox ID="TextBox32" runat="server" Visible="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:CheckBox ID="CheckBox12"  Font-Bold="true" AutoPostBack="true" OnCheckedChanged="CheckBox12_CheckedChanged" Enabled="false" runat="server" />
                                    <asp:TextBox ID="TextBox37" runat="server" Font-Bold="true" Enabled="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:TextBox ID="TextBox38" runat="server" Font-Bold="true" onchange="txtHSpercentage1()" onkeypress="return numeric(event)" Enabled="false" BorderColor="Black"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ControlToValidate="TextBox7" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>

                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Button ID="Button9" runat="server" Enabled="false" Text="Submit"  Height="25px" OnClick="Button9_Click" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />

                                </asp:TableCell>
                             <%--  Sanjay Jain--%>
                            <asp:TableCell Style="border: 1px solid">
                                 <asp:Label ID="Label68" runat="server" Font-Bold="true" Text=""></asp:Label>
                            </asp:TableCell>
                             <%--  Sanjay Jain--%>
                                <asp:TableCell>
                                    <div>
                                        <asp:TextBox ID="TextBox39" runat="server" Width="50px" Visible="false"></asp:TextBox>
                                    </div>

                                </asp:TableCell>

                            </asp:TableRow>

                        <asp:TableRow>
                                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">4.</asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label38" runat="server" Font-Bold="true" Text="Hostel Mess"></asp:Label>
                                       <asp:Label ID="Label39" runat="server" Font-Bold="true"  Visible="false"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label40" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label41" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                    <asp:TextBox ID="TextBox40" runat="server" Visible="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:CheckBox ID="CheckBox13"  Font-Bold="true" OnCheckedChanged="CheckBox13_CheckedChanged" AutoPostBack="true" Enabled="false" runat="server" />
                                    <asp:TextBox ID="TextBox41" runat="server" Font-Bold="true" Enabled="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:TextBox ID="TextBox42" runat="server" Font-Bold="true" onchange="txtHSpercentage1()" onkeypress="return numeric(event)" Enabled="false" BorderColor="Black"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="TextBox7" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>

                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Button ID="Button10" runat="server" Enabled="false" Text="Submit" OnClick="Button10_Click"  Height="25px" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />

                                </asp:TableCell>
                             <%--  Sanjay Jain--%>
                            <asp:TableCell Style="border: 1px solid">
                                 <asp:Label ID="Label69" runat="server" Font-Bold="true" Text=""></asp:Label>
                            </asp:TableCell>
                             <%--  Sanjay Jain--%>
                                <asp:TableCell>
                                    <div>
                                        <asp:TextBox ID="TextBox43" runat="server" Width="50px" Visible="false"></asp:TextBox>
                                    </div>

                                </asp:TableCell>

                            </asp:TableRow>

                   
                        <asp:TableRow>
                                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">5.</asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label46" runat="server" Font-Bold="true" Text="Sports"></asp:Label>
                                       <asp:Label ID="lblsportcode" runat="server" Font-Bold="true"  Visible="false"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblsportsname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblsportsdesi" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                    <asp:TextBox ID="TextBox48" runat="server" Visible="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:CheckBox ID="CheckBox15"  Font-Bold="true" OnCheckedChanged="CheckBox15_CheckedChanged" AutoPostBack="true" Enabled="false" runat="server" />
                                    <asp:TextBox ID="TextBox49" runat="server" Font-Bold="true" Enabled="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:TextBox ID="TextBox50" runat="server" Font-Bold="true" onchange="txtHSpercentage1()" onkeypress="return numeric(event)" Enabled="false" BorderColor="Black"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ControlToValidate="TextBox7" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>

                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Button ID="Button12" runat="server" Enabled="false" Text="Submit" OnClick="Button12_Click"  Height="25px" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />

                                </asp:TableCell>
                                <%--  Sanjay Jain--%>
                                <asp:TableCell Style="border: 1px solid">
                                     <asp:Label ID="Label71" runat="server" Font-Bold="true" Text=""></asp:Label>
                                </asp:TableCell>
                                 <%--  Sanjay Jain--%>
                                <asp:TableCell>
                                    <div>
                                        <asp:TextBox ID="TextBox51" runat="server" Width="50px" Visible="false"></asp:TextBox>
                                    </div>

                                </asp:TableCell>

                            </asp:TableRow>

                         <asp:TableRow>
         <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">6.</asp:TableCell>
         <asp:TableCell Style="border: 1px solid">
             <asp:Label ID="Label946" runat="server" Font-Bold="true" Text="IT Department"></asp:Label>
                <asp:Label ID="lblITcode" runat="server" Font-Bold="true"  Visible="false"></asp:Label>
         </asp:TableCell>
         <asp:TableCell Style="border: 1px solid">
             <asp:Label ID="lblITsname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
         </asp:TableCell>
         <asp:TableCell Style="border: 1px solid">
             <asp:Label ID="lblITsdesi" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
             <asp:TextBox ID="TextBox948" runat="server" Visible="false" BorderColor="Black"></asp:TextBox>
         </asp:TableCell>
         <asp:TableCell Style="border: 1px solid">
             <asp:CheckBox ID="CheckBox915"  Font-Bold="true" OnCheckedChanged="CheckBox915_CheckedChanged" AutoPostBack="true" Enabled="false" runat="server" />
             <asp:TextBox ID="TextBox949" runat="server" Font-Bold="true" Enabled="false" BorderColor="Black"></asp:TextBox>
         </asp:TableCell>
         <asp:TableCell Style="border: 1px solid">
             <asp:TextBox ID="TextBox950" runat="server" Font-Bold="true" onchange="txtHSpercentage1()" onkeypress="return numeric(event)" Enabled="false" BorderColor="Black"></asp:TextBox>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator914" runat="server" ControlToValidate="TextBox950" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>

         </asp:TableCell>

         <asp:TableCell Style="border: 1px solid">
             <asp:Button ID="Button912" runat="server" Enabled="false" Text="Submit" OnClick="Button912_Click"  Height="25px" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />

         </asp:TableCell>
         <%--  Sanjay Jain--%>
         <asp:TableCell Style="border: 1px solid">
              <asp:Label ID="Label971" runat="server" Font-Bold="true" Text=""></asp:Label>
         </asp:TableCell>
          <%--  Sanjay Jain--%>
         <asp:TableCell>
             <div>
                 <asp:TextBox ID="TextBox951" runat="server" Width="50px" Visible="false"></asp:TextBox>
             </div>

         </asp:TableCell>

     </asp:TableRow>

                        <asp:TableRow>
                                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">7.</asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label47" runat="server" Font-Bold="true" Text="Community Medicine"></asp:Label>
                                       <asp:Label ID="lblCommunityMedicinecode" runat="server" Font-Bold="true"  Visible="false"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblCommunityMedicinename" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblCommunityMedicinedesig" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                    <asp:TextBox ID="TextBox52" runat="server" Visible="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:CheckBox ID="CheckBox16"  Font-Bold="true" OnCheckedChanged="CheckBox16_CheckedChanged" AutoPostBack="true" Enabled="false" runat="server" />
                                    <asp:TextBox ID="TextBox53" runat="server" Font-Bold="true" Enabled="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:TextBox ID="TextBox54" runat="server" Font-Bold="true" onchange="txtHSpercentage()" onkeypress="return numeric(event)" Enabled="false" BorderColor="Black"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" ControlToValidate="TextBox7" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>

                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Button ID="Button13" runat="server" Enabled="false" Text="Submit" OnClick="Button13_Click" Height="25px" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />

                                </asp:TableCell>
                             <%--  Sanjay Jain--%>
                            <asp:TableCell Style="border: 1px solid">
                                 <asp:Label ID="Label72" runat="server" Font-Bold="true" Text=""></asp:Label>
                            </asp:TableCell>
                             <%--  Sanjay Jain--%>
                                <asp:TableCell>
                                    <div>
                                        <asp:TextBox ID="TextBox55" runat="server" Width="50px" Visible="false"></asp:TextBox>
                                    </div>

                                </asp:TableCell>

                            </asp:TableRow>

                        <asp:TableRow>
                                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">8.</asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label48" runat="server" Font-Bold="true" Text="General Medicine"></asp:Label>
                                       <asp:Label ID="lblGeneralMedicineCode" runat="server" Font-Bold="true"  Visible="false"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblGeneralMedicineName" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblGeneralMedicinedegis" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                    <asp:TextBox ID="TextBox56" runat="server" Visible="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:CheckBox ID="CheckBox17"  Font-Bold="true" OnCheckedChanged="CheckBox17_CheckedChanged" AutoPostBack="true" Enabled="false" runat="server" />
                                    <asp:TextBox ID="TextBox57" runat="server" Font-Bold="true" Enabled="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:TextBox ID="TextBox58" runat="server" Font-Bold="true" onchange="txtHSpercentage()" onkeypress="return numeric(event)" Enabled="false" BorderColor="Black"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server" ControlToValidate="TextBox7" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>

                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Button ID="Button15" runat="server" Enabled="false" Text="Submit"  Height="25px" OnClick="Button15_Click" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />

                                </asp:TableCell>
                             <%--  Sanjay Jain--%>
                                <asp:TableCell Style="border: 1px solid">
                                     <asp:Label ID="Label73" runat="server" Font-Bold="true" Text=""></asp:Label>
                                </asp:TableCell>
                                 <%--  Sanjay Jain--%>
                                <asp:TableCell>
                                    <div>
                                        <asp:TextBox ID="TextBox59" runat="server" Width="50px" Visible="false"></asp:TextBox>
                                    </div>

                                </asp:TableCell>

                            </asp:TableRow>

                        <asp:TableRow>
                                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">9.</asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label49" runat="server" Font-Bold="true" Text="Psychiatry"></asp:Label>
                                       <asp:Label ID="lblPsychiatrycode" runat="server" Font-Bold="true"  Visible="false"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblPsychiatryname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblPsychiatrydesig" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                    <asp:TextBox ID="TextBox60" runat="server" Visible="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:CheckBox ID="CheckBox18"  Font-Bold="true" OnCheckedChanged="CheckBox18_CheckedChanged" AutoPostBack="true" Enabled="false" runat="server" />
                                    <asp:TextBox ID="TextBox61" runat="server" Font-Bold="true" Enabled="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:TextBox ID="TextBox62" runat="server" Font-Bold="true" onchange="txtHSpercentage()" onkeypress="return numeric(event)" Enabled="false" BorderColor="Black"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server" ControlToValidate="TextBox7" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>

                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Button ID="Button16" runat="server" Enabled="false" Text="Submit" OnClick="Button16_Click"  Height="25px" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />

                                </asp:TableCell>
                             <%--  Sanjay Jain--%>
                                <asp:TableCell Style="border: 1px solid">
                                     <asp:Label ID="Label74" runat="server" Font-Bold="true" Text=""></asp:Label>
                                </asp:TableCell>
                                 <%--  Sanjay Jain--%>
                                <asp:TableCell>
                                    <div>
                                        <asp:TextBox ID="TextBox63" runat="server" Width="50px" Visible="false"></asp:TextBox>
                                    </div>

                                </asp:TableCell>

                            </asp:TableRow>

                        <asp:TableRow>
                                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">10.</asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label50" runat="server" Font-Bold="true" Text="General Surgery"></asp:Label>
                                       <asp:Label ID="lblGeneralSurgerycode" runat="server" Font-Bold="true"  Visible="false"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblGeneralSurgeryname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblGeneralSurgerydesi" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                    <asp:TextBox ID="TextBox64" runat="server" Visible="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:CheckBox ID="CheckBox19"  Font-Bold="true" OnCheckedChanged="CheckBox19_CheckedChanged" AutoPostBack="true" Enabled="false" runat="server" />
                                    <asp:TextBox ID="TextBox65" runat="server" Font-Bold="true" Enabled="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:TextBox ID="TextBox66" runat="server" Font-Bold="true" onchange="txtHSpercentage()" onkeypress="return numeric(event)" Enabled="false" BorderColor="Black"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server" ControlToValidate="TextBox7" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>

                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Button ID="Button17" runat="server" Enabled="false" Text="Submit" OnClick="Button17_Click"  Height="25px" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />

                                </asp:TableCell>
                             <%--  Sanjay Jain--%>
                                <asp:TableCell Style="border: 1px solid">
                                     <asp:Label ID="Label75" runat="server" Font-Bold="true" Text=""></asp:Label>
                                </asp:TableCell>
                                 <%--  Sanjay Jain--%>
                                <asp:TableCell>
                                    <div>
                                        <asp:TextBox ID="TextBox67" runat="server" Width="50px" Visible="false"></asp:TextBox>
                                    </div>

                                </asp:TableCell>

                            </asp:TableRow>

                        <asp:TableRow>
                                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">11.</asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label51" runat="server" Font-Bold="true" Text="Anesthisia"></asp:Label>
                                       <asp:Label ID="lblAnesthisiaCode" runat="server" Font-Bold="true"  Visible="false"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblAnesthisiaName" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblAnesthisiadesi" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                    <asp:TextBox ID="TextBox68" runat="server" Visible="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:CheckBox ID="CheckBox20"  Font-Bold="true" OnCheckedChanged="CheckBox20_CheckedChanged" AutoPostBack="true" Enabled="false" runat="server" />
                                    <asp:TextBox ID="TextBox69" runat="server" Font-Bold="true" Enabled="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:TextBox ID="TextBox70" runat="server" Font-Bold="true" onchange="txtHSpercentage()" onkeypress="return numeric(event)" Enabled="false" BorderColor="Black"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator19" runat="server" ControlToValidate="TextBox7" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>

                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Button ID="Button18" runat="server" Enabled="false" Text="Submit" OnClick="Button18_Click"  Height="25px" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />

                                </asp:TableCell>
                             <%--  Sanjay Jain--%>
                                <asp:TableCell Style="border: 1px solid">
                                     <asp:Label ID="Label76" runat="server" Font-Bold="true" Text=""></asp:Label>
                                </asp:TableCell>
                                 <%--  Sanjay Jain--%>
                                <asp:TableCell>
                                    <div>
                                        <asp:TextBox ID="TextBox71" runat="server" Width="50px" Visible="false"></asp:TextBox>
                                    </div>

                                </asp:TableCell>

                            </asp:TableRow>

                        <asp:TableRow>
                                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">12.</asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label52" runat="server" Font-Bold="true" Text="Obs & Gyane"></asp:Label>
                                       <asp:Label ID="lblObsGyaneCode" runat="server" Font-Bold="true"  Visible="false"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblObsGyaneName" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblObsGyanedesi" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                    <asp:TextBox ID="TextBox72" runat="server" Visible="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:CheckBox ID="CheckBox21"  Font-Bold="true" OnCheckedChanged="CheckBox21_CheckedChanged" AutoPostBack="true" Enabled="false" runat="server" />
                                    <asp:TextBox ID="TextBox73" runat="server" Font-Bold="true" Enabled="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:TextBox ID="TextBox74" runat="server" Font-Bold="true" onchange="txtHSpercentage()" onkeypress="return numeric(event)" Enabled="false" BorderColor="Black"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server" ControlToValidate="TextBox7" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>

                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Button ID="Button19" runat="server" Enabled="false" Text="Submit" OnClick="Button19_Click"  Height="25px" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />

                                </asp:TableCell>
                             <%--  Sanjay Jain--%>
                                <asp:TableCell Style="border: 1px solid">
                                     <asp:Label ID="Label77" runat="server" Font-Bold="true" Text=""></asp:Label>
                                </asp:TableCell>
                                 <%--  Sanjay Jain--%>
                                <asp:TableCell>
                                    <div>
                                        <asp:TextBox ID="TextBox75" runat="server" Width="50px" Visible="false"></asp:TextBox>
                                    </div>

                                </asp:TableCell>

                            </asp:TableRow>

                        <asp:TableRow>
                                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">13.</asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label53" runat="server" Font-Bold="true" Text="Pediatrics"></asp:Label>
                                       <asp:Label ID="lblPediatricscode" runat="server" Font-Bold="true"  Visible="false"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblPediatricsname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblPediatricsdesi" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                    <asp:TextBox ID="TextBox76" runat="server" Visible="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:CheckBox ID="CheckBox22"  Font-Bold="true" OnCheckedChanged="CheckBox22_CheckedChanged" AutoPostBack="true" Enabled="false" runat="server" />
                                    <asp:TextBox ID="TextBox77" runat="server" Font-Bold="true" Enabled="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:TextBox ID="TextBox78" runat="server" Font-Bold="true" onchange="txtHSpercentage()" onkeypress="return numeric(event)" Enabled="false" BorderColor="Black"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator21" runat="server" ControlToValidate="TextBox7" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>

                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Button ID="Button20" runat="server" Enabled="false" Text="Submit" OnClick="Button20_Click"  Height="25px" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />

                                </asp:TableCell>
                             <%--  Sanjay Jain--%>
                                <asp:TableCell Style="border: 1px solid">
                                     <asp:Label ID="Label78" runat="server" Font-Bold="true" Text=""></asp:Label>
                                </asp:TableCell>
                                 <%--  Sanjay Jain--%>
                                <asp:TableCell>
                                    <div>
                                        <asp:TextBox ID="TextBox79" runat="server" Width="50px" Visible="false"></asp:TextBox>
                                    </div>

                                </asp:TableCell>

                            </asp:TableRow>

                        <asp:TableRow>
                                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">14.</asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label54" runat="server" Font-Bold="true" Text="Orthopedics"></asp:Label>
                                       <asp:Label ID="lblOrthopedicscode" runat="server" Font-Bold="true"  Visible="false"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblOrthopedicsname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblOrthopedicsdesi" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                    <asp:TextBox ID="TextBox80" runat="server" Visible="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:CheckBox ID="CheckBox23"  Font-Bold="true" OnCheckedChanged="CheckBox23_CheckedChanged" AutoPostBack="true" Enabled="false" runat="server" />
                                    <asp:TextBox ID="TextBox81" runat="server" Font-Bold="true" Enabled="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:TextBox ID="TextBox82" runat="server" Font-Bold="true" onchange="txtHSpercentage()" onkeypress="return numeric(event)" Enabled="false" BorderColor="Black"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator22" runat="server" ControlToValidate="TextBox7" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>

                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Button ID="Button21" runat="server" Enabled="false" Text="Submit" OnClick="Button21_Click"  Height="25px" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />

                                </asp:TableCell>
                             <%--  Sanjay Jain--%>
                                <asp:TableCell Style="border: 1px solid">
                                     <asp:Label ID="Label79" runat="server" Font-Bold="true" Text=""></asp:Label>
                                </asp:TableCell>
                                 <%--  Sanjay Jain--%>
                                <asp:TableCell>
                                    <div>
                                        <asp:TextBox ID="TextBox83" runat="server" Width="50px" Visible="false"></asp:TextBox>
                                    </div>

                                </asp:TableCell>

                            </asp:TableRow>

                        <asp:TableRow>
                                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">15.</asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label55" runat="server" Font-Bold="true" Text="Ent"></asp:Label>
                                       <asp:Label ID="lblEntcode" runat="server" Font-Bold="true"  Visible="false"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblentname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblentdesi" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                    <asp:TextBox ID="TextBox84" runat="server" Visible="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:CheckBox ID="CheckBox24"  Font-Bold="true" OnCheckedChanged="CheckBox24_CheckedChanged" AutoPostBack="true" Enabled="false" runat="server" />
                                    <asp:TextBox ID="TextBox85" runat="server" Font-Bold="true" Enabled="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:TextBox ID="TextBox86" runat="server" Font-Bold="true" onchange="txtHSpercentage()" onkeypress="return numeric(event)" Enabled="false" BorderColor="Black"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator23" runat="server" ControlToValidate="TextBox7" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>

                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Button ID="Button22" runat="server" Enabled="false" Text="Submit" OnClick="Button22_Click" Height="25px" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />

                                </asp:TableCell>
                             <%--  Sanjay Jain--%>
                                <asp:TableCell Style="border: 1px solid">
                                     <asp:Label ID="Label80" runat="server" Font-Bold="true" Text=""></asp:Label>
                                </asp:TableCell>
                                 <%--  Sanjay Jain--%>
                                <asp:TableCell>
                                    <div>
                                        <asp:TextBox ID="TextBox87" runat="server" Width="50px" Visible="false"></asp:TextBox>
                                    </div>

                                </asp:TableCell>

                            </asp:TableRow>

                        <asp:TableRow>
                                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">16.</asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label56" runat="server" Font-Bold="true" Text="Ophthalmology"></asp:Label>
                                       <asp:Label ID="lblOphthalmologycode" runat="server" Font-Bold="true"  Visible="false"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblOphthalmologyname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblOphthalmologydesi" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                    <asp:TextBox ID="TextBox88" runat="server" Visible="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:CheckBox ID="CheckBox25"  Font-Bold="true" OnCheckedChanged="CheckBox25_CheckedChanged" AutoPostBack="true" Enabled="false" runat="server" />
                                    <asp:TextBox ID="TextBox89" runat="server" Font-Bold="true" Enabled="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:TextBox ID="TextBox90" runat="server" Font-Bold="true" onchange="txtHSpercentage()" onkeypress="return numeric(event)" Enabled="false" BorderColor="Black"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator24" runat="server" ControlToValidate="TextBox90" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>

                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Button ID="Button23" runat="server" Enabled="false" Text="Submit" OnClick="Button23_Click"  Height="25px" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />

                                </asp:TableCell>
                             <%--  Sanjay Jain--%>
                                <asp:TableCell Style="border: 1px solid">
                                     <asp:Label ID="Label81" runat="server" Font-Bold="true" Text=""></asp:Label>
                                </asp:TableCell>
                                 <%--  Sanjay Jain--%>
                                <asp:TableCell>
                                    <div>
                                        <asp:TextBox ID="TextBox91" runat="server" Width="50px" Visible="false"></asp:TextBox>
                                    </div>

                                </asp:TableCell>

                            </asp:TableRow>

                        <asp:TableRow>
                                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">17.</asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label57" runat="server" Font-Bold="true" Text="Casualty"></asp:Label>
                                       <asp:Label ID="lblCasualtycode" runat="server" Font-Bold="true"  Visible="false"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblCasualtyname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblCasualtydesi" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                    <asp:TextBox ID="TextBox92" runat="server" Visible="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:CheckBox ID="CheckBox26"  Font-Bold="true" OnCheckedChanged="CheckBox26_CheckedChanged" AutoPostBack="true" Enabled="false" runat="server" />
                                    <asp:TextBox ID="TextBox93" runat="server" Font-Bold="true" Enabled="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:TextBox ID="TextBox94" runat="server" Font-Bold="true" onchange="txtHSpercentage()" onkeypress="return numeric(event)" Enabled="false" BorderColor="Black"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator25" runat="server" ControlToValidate="TextBox7" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>

                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Button ID="Button24" runat="server" Enabled="false" Text="Submit" OnClick="Button24_Click"  Height="25px" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />

                                </asp:TableCell>
                             <%--  Sanjay Jain--%>
                                <asp:TableCell Style="border: 1px solid">
                                     <asp:Label ID="Label82" runat="server" Font-Bold="true" Text=""></asp:Label>
                                </asp:TableCell>
                                 <%--  Sanjay Jain--%>
                                <asp:TableCell>
                                    <div>
                                        <asp:TextBox ID="TextBox95" runat="server" Width="50px" Visible="false"></asp:TextBox>
                                    </div>

                                </asp:TableCell>

                            </asp:TableRow>

                        <asp:TableRow>
                                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">18.</asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label58" runat="server" Font-Bold="true" Text="Dermatology"></asp:Label>
                                       <asp:Label ID="lblDermatologycode" runat="server" Font-Bold="true"  Visible="false"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblDermatologyname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblDermatologydesi" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                    <asp:TextBox ID="TextBox96" runat="server" Visible="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:CheckBox ID="CheckBox27"  Font-Bold="true" OnCheckedChanged="CheckBox27_CheckedChanged" AutoPostBack="true" Enabled="false" runat="server" />
                                    <asp:TextBox ID="TextBox97" runat="server" Font-Bold="true" Enabled="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:TextBox ID="TextBox98" runat="server" Font-Bold="true" onchange="txtHSpercentage()" onkeypress="return numeric(event)" Enabled="false" BorderColor="Black"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator26" runat="server" ControlToValidate="TextBox7" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>

                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Button ID="Button25" runat="server" Enabled="false" Text="Submit" OnClick="Button25_Click"  Height="25px" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />

                                </asp:TableCell>
                             <%--  Sanjay Jain--%>
                                    <asp:TableCell Style="border: 1px solid">
                                         <asp:Label ID="Label83" runat="server" Font-Bold="true" Text=""></asp:Label>
                                    </asp:TableCell>
                                     <%--  Sanjay Jain--%>
                                <asp:TableCell>
                                    <div>
                                        <asp:TextBox ID="TextBox99" runat="server" Width="50px" Visible="false"></asp:TextBox>
                                    </div>

                                </asp:TableCell>

                            </asp:TableRow>

                        <asp:TableRow>
                                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">19.</asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label59" runat="server" Font-Bold="true" Text="Tb & Chest (Pulmonary Medicine)"></asp:Label>
                                       <asp:Label ID="lbltbchestcode" runat="server" Font-Bold="true"  Visible="false"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lbltbchestname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lbltbchestdesi" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                    <asp:TextBox ID="TextBox100" runat="server" Visible="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:CheckBox ID="CheckBox28"  Font-Bold="true" OnCheckedChanged="CheckBox28_CheckedChanged" AutoPostBack="true" Enabled="false" runat="server" />
                                    <asp:TextBox ID="TextBox101" runat="server" Font-Bold="true" Enabled="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:TextBox ID="TextBox102" runat="server" Font-Bold="true" onchange="txtHSpercentage()" onkeypress="return numeric(event)" Enabled="false" BorderColor="Black"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator27" runat="server" ControlToValidate="TextBox7" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>

                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Button ID="Button26" runat="server" Enabled="false" Text="Submit" OnClick="Button26_Click"  Height="25px" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />

                                </asp:TableCell>
                             <%--  Sanjay Jain--%>
                                <asp:TableCell Style="border: 1px solid">
                                     <asp:Label ID="Label84" runat="server" Font-Bold="true" Text=""></asp:Label>
                                </asp:TableCell>
                                 <%--  Sanjay Jain--%>
                                <asp:TableCell>
                                    <div>
                                        <asp:TextBox ID="TextBox103" runat="server" Width="50px" Visible="false"></asp:TextBox>
                                    </div>

                                </asp:TableCell>

                            </asp:TableRow>

                        <asp:TableRow>
                                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">20.</asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label60" runat="server" Font-Bold="true" Text="Radiology"></asp:Label>
                                       <asp:Label ID="lblRadiologycode" runat="server" Font-Bold="true"  Visible="false"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblRadiologyname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblRadiologydesi" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                    <asp:TextBox ID="TextBox104" runat="server" Visible="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:CheckBox ID="CheckBox29"  Font-Bold="true" OnCheckedChanged="CheckBox29_CheckedChanged" AutoPostBack="true" Enabled="false" runat="server" />
                                    <asp:TextBox ID="TextBox105" runat="server" Font-Bold="true" Enabled="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:TextBox ID="TextBox106" runat="server" Font-Bold="true" onchange="txtHSpercentage()" onkeypress="return numeric(event)" Enabled="false" BorderColor="Black"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator28" runat="server" ControlToValidate="TextBox7" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>

                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Button ID="Button27" runat="server" Enabled="false" Text="Submit" OnClick="Button27_Click"  Height="25px" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />

                                </asp:TableCell>
                             <%--  Sanjay Jain--%>
                            <asp:TableCell Style="border: 1px solid">
                                 <asp:Label ID="Label85" runat="server" Font-Bold="true" Text=""></asp:Label>
                            </asp:TableCell>
                             <%--  Sanjay Jain--%>
                                <asp:TableCell>
                                    <div>
                                        <asp:TextBox ID="TextBox107" runat="server" Width="50px" Visible="false"></asp:TextBox>
                                    </div>

                                </asp:TableCell>

                            </asp:TableRow>

                        <asp:TableRow>
                                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">21.</asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label61" runat="server" Font-Bold="true" Text="Pathlogy(Blood Bank)"></asp:Label>
                                       <asp:Label ID="lblPathlogycode" runat="server" Font-Bold="true"  Visible="false"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblPathlogyname" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblPathlogydesi" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                    <asp:TextBox ID="TextBox108" runat="server" Visible="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:CheckBox ID="CheckBox30"  Font-Bold="true" OnCheckedChanged="CheckBox30_CheckedChanged" AutoPostBack="true" Enabled="false" runat="server" />
                                    <asp:TextBox ID="TextBox109" runat="server" Font-Bold="true" Enabled="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:TextBox ID="TextBox110" runat="server" Font-Bold="true" onchange="txtHSpercentage()" onkeypress="return numeric(event)" Enabled="false" BorderColor="Black"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator29" runat="server" ControlToValidate="TextBox7" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>

                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Button ID="Button28" runat="server" Enabled="false" Text="Submit" OnClick="Button28_Click"  Height="25px" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />

                                </asp:TableCell>
                             <%--  Sanjay Jain--%>
                                <asp:TableCell Style="border: 1px solid">
                                     <asp:Label ID="Label86" runat="server" Font-Bold="true" Text=""></asp:Label>
                                </asp:TableCell>
                                 <%--  Sanjay Jain--%>
                                <asp:TableCell>
                                    <div>
                                        <asp:TextBox ID="TextBox111" runat="server" Width="50px" Visible="false"></asp:TextBox>
                                    </div>

                                </asp:TableCell>

                            </asp:TableRow>

                        <asp:TableRow>
                                <asp:TableCell Style="border: 1px solid; text-align: center" Font-Bold="true">22.</asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="Label62" runat="server" Font-Bold="true" Text="Forensic Medicine"></asp:Label>
                                       <asp:Label ID="lblForensicMedicinecode" runat="server" Font-Bold="true"  Visible="false"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblForensicMedicinename" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Label ID="lblForensicMedicinedesi" runat="server" Font-Bold="true" Enabled="false" Text=""></asp:Label>
                                    <asp:TextBox ID="TextBox112" runat="server" Visible="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:CheckBox ID="CheckBox31"  Font-Bold="true" OnCheckedChanged="CheckBox31_CheckedChanged" AutoPostBack="true" Enabled="false" runat="server" />
                                    <asp:TextBox ID="TextBox113" runat="server" Font-Bold="true" Enabled="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid">
                                    <asp:TextBox ID="TextBox114" runat="server" Font-Bold="true" onchange="txtHSpercentage()" onkeypress="return numeric(event)" Enabled="false" BorderColor="Black"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator30" runat="server" ControlToValidate="TextBox7" ValidationExpression="^[^.]*$" ErrorMessage=". Not Allowed" Display="Dynamic"></asp:RegularExpressionValidator>

                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:Button ID="Button29" runat="server" Enabled="false" Text="Submit" OnClick="Button29_Click" Height="25px" BackColor="#ff9900" Font-Bold="true" ForeColor="Black" BorderColor="Black" />

                                </asp:TableCell>
                             <%--  Sanjay Jain--%>
                                <asp:TableCell Style="border: 1px solid">
                                     <asp:Label ID="Label87" runat="server" Font-Bold="true" Text=""></asp:Label>
                                </asp:TableCell>
                                 <%--  Sanjay Jain--%>
                                <asp:TableCell>
                                    <div>
                                        <asp:TextBox ID="TextBox115" runat="server" Width="50px" Visible="false"></asp:TextBox>
                                    </div>

                                </asp:TableCell>

                            </asp:TableRow>


                        <asp:TableRow>
                                <asp:TableCell Style="border: 1px solid; text-align: right; vertical-align: middle" ColumnSpan="5">
                                    <asp:Label ID="Label63" runat="server" Font-Size="Large" Font-Bold="true" ForeColor="Red" Text="Total Pending Amount : "></asp:Label>
                                </asp:TableCell>

                                <asp:TableCell Style="border: 1px solid">
                                    <asp:TextBox ID="TextBox116" runat="server" Font-Bold="true" Enabled="false" BorderColor="Black"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Style="border: 1px solid; vertical-align: bottom">
                                    <asp:Button ID="Button30" runat="server" Text="Verify"  Height="25px" BackColor="#ff9900" OnClick="Button30_Click" Enabled="false" Font-Bold="true" ForeColor="Black" BorderColor="Black" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>

                    <br />
                     <fieldset>
             <div class="form-group">
                <div class="col-md-4">
                    <label></label>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="Button31" runat="server"  Width="200px" Visible="false" Height="40px" OnClick="Button31_Click" Font-Bold="true" ForeColor="white" BackColor="Green" Text="Submit by Principal" CssClass="form-control" />
                </div>

                 <div class="col-md-2">
                    <asp:Button ID="Button32" runat="server"  Width="200px" Visible="false" Height="40px" OnClick="Button32_Click" Font-Bold="true" ForeColor="white" BackColor="Red" Text="Rejected by Principal" CssClass="form-control" />
                </div>

                <%--<asp:TextBox ID="txtBarcode" runat="server"></asp:TextBox>--%>
                <%-- <asp:Button ID="btnGenerate" runat="server" Text="Generate" OnClick="btnGenerate_Click"/>--%>
            </div>

        </fieldset>

                 <div class="form-group">
                            <div class="col-md-5">
                                <label></label>
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="Button33" runat="server" Width="200px" Text="Final Approval by COE" OnClick="Button33_Click" Height="40px" Font-Bold="true" ForeColor="white" BackColor="Green"  Visible="false" CssClass="form-control" />
                            </div>
                            <br />
                            <br />
                            <%--<asp:TextBox ID="txtBarcode" runat="server"></asp:TextBox>--%>
                            <%-- <asp:Button ID="btnGenerate" runat="server" Text="Generate" OnClick="btnGenerate_Click"/>--%>
                        </div>
                    <br />
                    </ContentTemplate>
             </asp:UpdatePanel>

                


          
    </asp:Panel>
    <asp:Button ID="Button5" runat="server" Style="display: none;" />
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button5"
        PopupControlID="Panel1" BackgroundCssClass="modalBackground" />
                 
    ,


    <br />
    <br />
</asp:Content>
