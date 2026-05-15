<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="TMVF_Activity.aspx.cs" Inherits="Faculty_TMVF_Activity" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        .column {
            border: 1px solid #000;
        }

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

    <script type="text/javascript">
        function HidePopup10() {

            $('#confirmModal10').modal('hide');
        }
        function VisiblePopup10() {
            $('#confirmModal10').modal('show');


        }
        function HidePopup101() {

            $('#confirmModal101').modal('hide');
        }
        function VisiblePopup101() {
            $('#confirmModal101').modal('show');


        }
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                alert("Please Enter Only Numeric Value:");
                return false;
            }

            return true;
        }

        function validateHhMm(inputField) {
            var isValid = /^(([01][0-9])|(2[0-3])):([0-5][0-9])$/.test(inputField.value);

            if (isValid) {
                inputField.style.backgroundColor = '#bfa';
            } else {
                inputField.style.backgroundColor = '#fba';

                alert("Accept only Time format .. (HH:mm)!  ");
                inputField.value = "";
            }

            return isValid;


        }



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
        <tr style="width: 400px; border: 1px solid">

            <td style="height: 13px; width: 150px">&nbsp&nbsp&nbsp&nbsp Account No.</td>
            <td style="height: 13px">
                <asp:TextBox ID="txtACNo" runat="server" Placeholder="Account Number"></asp:TextBox>
            </td>
            <td style="height: 13px">IFSC Code.</td>
            <td style="height: 13px">
                <asp:TextBox ID="txtIfscCode" runat="server" Placeholder="IFSC Code"></asp:TextBox>
            </td>
            <td style="height: 13px">Bank Name & Branch</td>
            <td style="height: 13px">
                <asp:TextBox ID="txtBankName" runat="server" Placeholder="Bank Name"></asp:TextBox>
            </td>
            <td style="height: 13px">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"></asp:Button>
            </td>
        </tr>
        <tr>
            <td colspan="8" style="height: 20px"></td>
        </tr>
        <tr>
            <td colspan="8">&nbsp;&nbsp;&nbsp; &nbsp; 
                <asp:Label ID="Label3" runat="server"
                    Text="Details of the Daily Activities Done by the Research Fellow" Font-Size="15pt" ForeColor="#093A62"
                    Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>




            </td>
        </tr>
        <tr>
            <td colspan="8" style="height: 20px"></td>
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
                                    <asp:Button ID="btndetail" runat="server" Text="Monthly Details" OnClick="btndetail_Click" /></td>
                                </td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:Button ID="btnGet" runat="server" Text="Get" OnClick="btnGet_Click" />
                                </td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:Button ID="btnexporttoexcel" runat="server" Text="Export To Excel" /></td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:Button ID="btnSubmitApplication" runat="server" Text="Submit" OnClick="btnSubmitApplication_Click" /></td>
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
                            <asp:GridView ID="grd_ViewAttendance" runat="server" DataKeyNames="Attendance_Date,No_,ShiftTime" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" OnRowDataBound="grd_ViewAttendance_RowDataBound" BorderWidth="1px" CellPadding="4" CssClass="table table-striped table-bordered table-hover">
                                <Columns>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDate" runat="server" Text='<%#Bind("Attendance_Date") %>' Enabled="false"></asp:Label>
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
                                            <asp:HiddenField ID="hdfWHour" runat="server" Value='<%#Bind("[WorkingHour]") %>' />
                                            <asp:HiddenField ID="HDStatus" runat="server" Value='<%#Bind("[F_Status]") %>' />

                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("Status") %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:Button ID="BtnSave" runat="server" Text="Fill Details" ForeColor="White" OnClick="BtnSave_Click" CssClass="btn" BackColor="#ff9900" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Approval Status">
                                        <ItemTemplate>

                                            <asp:Label ID="Label1" runat="server" ForeColor="Black" Text='<%#Bind("[PFormStatus]") %>' /><br />

                                            <asp:Label ID="Label4" runat="server" ForeColor="Black" Text='<%#Bind("[RFormStatus]") %>' />
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
     <div id="confirmModal101" class="modal fade confirm-modal" role="dialog" >

        <div class="modal-dialog modalPopup" style="width: 1050px; height: 350px;overflow:scroll">
            <div style="text-align: right; padding-bottom: -40px">
                <asp:Button ID="Button1" runat="server" Text="X" OnClientClick="HidePopup101(); return false;" Font-Size="Larger" />
            </div>
            <div class="clearfix" style="margin-bottom: 10px; margin-top: 10px; margin-left: 20px;overflow-y: scroll" 
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grdDetail" runat="server" 
                             AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" Visible="true">
                            <PagerSettings Mode="NumericFirstLast" />
                            <PagerStyle CssClass="csspager" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex +1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Program" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblemployeecode" runat="server" Text='<%# Bind("Program") %>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Semester" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSemester" runat="server" Text='<%# Bind("Semester") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Course" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" Visible="false" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("Course") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Name of Toppic" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center"  ItemStyle-CssClass="text-center">
                                     <ItemTemplate>
                                         <asp:Label ID="lbltopioc" runat="server" Text='<%# Eval("Name of topic") %>' ></asp:Label>
                                    </ItemTemplate>
                                 </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit" ItemStyle-Width="4%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("Unit") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Date" ItemStyle-Width="4%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                    <ItemTemplate>
                                        <asp:Label ID="Date" runat="server" Text='<%# Eval("Lecture_Date") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Time" ItemStyle-Width="4%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTime" runat="server" Text='<%# Eval("Time") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Strength" ItemStyle-Width="4%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("TotalStrength") %>' Style="text-transform: uppercase;"></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Present Strength" ItemStyle-Width="4%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPStrength" runat="server" Text='<%# Eval("StudentStrength") %>' Style="text-transform: uppercase;"></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>
                        </asp:GridView>



                        
                    </ContentTemplate>


                </asp:UpdatePanel>
            </div>
        </div>
              
    </div>
    <div id="confirmModal10" class="modal fade confirm-modal" role="dialog">

        <div class="modal-dialog modalPopup" style="width: 1050px; height: 350px">
            <div style="text-align: right; padding-bottom: -40px">
                <asp:Button ID="Button2" runat="server" Text="X" OnClientClick="HidePopup10(); return false;" Font-Size="Larger" />
            </div>
            <div class="clearfix" style="margin-bottom: 10px; margin-top: 10px; margin-left: 20px">
                <asp:UpdatePanel ID="pnlUpdate" runat="server">
                    <ContentTemplate>




                        <div class="col-sm-3 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp Program Name</label>
                                <asp:HiddenField ID="hdfEmployeeCode" runat="server" />
                                <asp:HiddenField ID="hdfShifttime" runat="server" />
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="drpProgram" runat="server" CausesValidation="false" AutoPostBack="true" CssClass="form-control" Width="200px" OnSelectedIndexChanged="drpProgram_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp  Semester </label>

                                <div class="col-sm-8">
                                    <asp:DropDownList ID="drpSemester" runat="server" CssClass="form-control" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="drpSemester_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp Course</label>

                                <div class="col-sm-8">
                                    <asp:DropDownList ID="drpCourse" runat="server" CssClass="form-control" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-3 p-0">
                            <div class="form-group clearfix">

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp  Section </label>
                                <div class="col-sm-8">

                                    <asp:DropDownList ID="drpSection" runat="server"  CssClass="form-control"  Width="200px">
                                       
                                    </asp:DropDownList>

                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">
                            <div class="form-group clearfix">

                                <label for="inputEmail3" class="col-form-label" style="font-size: small;width:200px">&nbsp&nbsp&nbsp  Hour </label>
                                <div class="col-sm-8">

                                    <asp:DropDownList ID="txtHour" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control" OnSelectedIndexChanged="txtHour_SelectedIndexChanged" AutoPostBack="true" Width="200px">
                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                    </asp:DropDownList>

                                </div>

                            </div>
                        </div>



                        <div class="col-sm-3 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp  Unit </label>
                                <div class="col-sm-8">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="V1" Display="Dynamic" ControlToValidate="txtUnit" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtUnit" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control" Width="200px"></asp:TextBox>

                                </div>

                            </div>
                        </div>
                        <div class="col-sm-3 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp Name of the Topic  </label>

                                <div class="col-sm-8">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="V1" Display="Dynamic" ControlToValidate="txtNameOfTopic" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtNameOfTopic" runat="server" CssClass="form-control" Width="200px">
                                    </asp:TextBox>

                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp  Date   </label>

                                <div class="col-sm-8">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="V1" Display="Dynamic" ControlToValidate="txtDate" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtDate" runat="server" Enabled="false" CssClass="form-control" Width="200px"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp Start Time</label>

                                <div class="col-sm-8">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="V1" Display="Dynamic" ControlToValidate="txtStartTime" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtStartTime" runat="server" onchange="validateHhMm(this);" CssClass="form-control" Width="200px"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp End Time</label>

                                <div class="col-sm-8">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="V1" Display="Dynamic" ControlToValidate="txtEndTime" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtEndTime" runat="server" onchange="validateHhMm(this);" CssClass="form-control" ValidationGroup="V1" Width="200px"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp Total Strength of students  </label>
                                <div class="col-sm-8">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="V1" Display="Dynamic" ControlToValidate="txtTotalStrength" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtTotalStrength" runat="server" onkeypress="return isNumberKey(event)" Enabled="false" CssClass="form-control" Width="200px">
                                    </asp:TextBox>

                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp  Number of Students Present   </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="V1" Display="Dynamic" ControlToValidate="txtPresentStudent" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtPresentStudent" runat="server" CssClass="form-control" Enabled="false" onkeypress="return isNumberKey(event)" Width="200px"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font-size: small; visibility: hidden">&nbsp&nbsp&nbsp xcscscvSC xcZX</label>

                                <div class="col-sm-8">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="V1" CssClass="form-control" Width="200px" OnClick="btnSave_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>


                </asp:UpdatePanel>
            </div>
        </div>
    </div>



</asp:Content>


