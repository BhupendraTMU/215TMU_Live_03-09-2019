<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="StudentDetaineeReport.aspx.cs" Inherits="StudentDetaineeReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../Scripts/jquery-1.9.1.min.js"></script>

    <script type="text/javascript">
        function callFeedbackMessage(inputType, inputText) {

            if (inputType == 'Error') {
                alertify.error(inputText);
                return false;
            }
            else if (inputType == 'Success') {
                //alertify.confirm().set('overflow', false);
                alertify.success("Save Successfully");
                return false;
            }
            else {
                alertify.log(inputText, "", 10000);
                return false;
            }
        }
    </script>

    <style type="text/css">
        .parent {
            text-align: center;
            display: block;
            border: 1px solid outset;
        }

        .child {
            display: inline-block;
            width: 200px;
        }

        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
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
    </script>
    <script type="text/javascript">

        function onUpdating() {
            // get the divImage

            var panelProg = $get('divImage');
            // set it to visible
            panelProg.style.display = '';

            // hide label if visible     
            var lbl = $get('<%= this.lblText.ClientID %>');
            lbl.innerHTML = '';

        }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1"
        TargetControlID="updmain" runat="server">
        <Animations>
            <OnUpdating>
               <Parallel duration="0">
                    <ScriptAction Script="onUpdating();" />
                    <EnableAction AnimationTarget="ddlReaapear" Enabled="false" /> 
                </Parallel>
            </OnUpdating>
        </Animations>
    </asp:UpdatePanelAnimationExtender>




    <asp:UpdatePanel runat="server" ID="updmain" UpdateMode="Conditional">
        <ContentTemplate>


            <fieldset class="boxBody">
                <table>
                    <tr>
                        <td style="padding-left: 15px;">
                            <asp:CheckBox runat="server" Checked="false" ID="Checkdetainee" Visible="false" OnCheckedChanged="CheckBox1_CheckedChanged" /></td>

                        <td>
                            <asp:Label ID="Label3" runat="server"
                                Text="Detainee List" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                            <asp:Label ID="lblText" runat="server" Text="" Height="10px"></asp:Label>
                        </td>
                        <td>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    Academic Year:&nbsp&nbsp
                    <asp:DropDownList ID="ddlAcademicYear" Width="100px" Height="20px" runat="server" AutoPostBack="true"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Font-Size="13px" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="ddlAcademicYear" ValidationGroup="g1" ErrorMessage="Please select the Academic Year!"></asp:RequiredFieldValidator>
                        </td>
                        <td style="width: 40px"></td>
                        <td>&nbsp&nbsp&nbsp&nbsp 
                            <asp:CheckBox ID="chkOpen" runat="server" Text="OpenElective" AutoPostBack="true" OnCheckedChanged="chkOpen_CheckedChanged" />
                        </td>

                        <td>
                            <id id="divImage" style="display: none;" class="fa fa-spinner fa-pulse fa-2x fa-fw"></id>
                            <span class="sr-only">Loading...</span>
                        </td>
                    </tr>
                </table>


            </fieldset>

            <fieldset class="boxBodyInner">



                <table>
                    <tr>
                        <td style="padding-left: 15px;">
                            <asp:CheckBox runat="server" ID="chkPrincipal" Text="As Principal" AutoPostBack="true" OnCheckedChanged="chkPrincipal_CheckedChanged" /></td>
                    </tr>
                </table>
                <br />
                <table width="100%">
                    <tr>
                         
                        <td>
                            <asp:DropDownList ID="DrpCourse" runat="server" Width="120px" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="DrpCourse_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DrpCourse" SetFocusOnError="true" InitialValue="--Course--" ErrorMessage="*" ValidationGroup="show" Font-Bold="true" ForeColor="RED"></asp:RequiredFieldValidator>

                        </td>
                        <td>
                            <asp:DropDownList ID="drpSemester" runat="server" Width="120px" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="drpSemester_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="drpSemester" SetFocusOnError="true" InitialValue="" ErrorMessage="*" ValidationGroup="show" Font-Bold="true" ForeColor="RED"></asp:RequiredFieldValidator>


                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlSubject" Width="120px" Height="30px" AutoPostBack="true" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSubject" SetFocusOnError="true" InitialValue="" ErrorMessage="*" ValidationGroup="show" Font-Bold="true" ForeColor="RED"></asp:RequiredFieldValidator> 
              
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td><b></b></td>
                                    <td>&nbsp;&nbsp;</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtPercentFrom" Width="100px" Height="30px" ToolTip="% From" placeholder="% From" Text="0" Enabled="false" MaxLength="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtPercentFrom" SetFocusOnError="true" InitialValue="" ErrorMessage="*" ValidationGroup="show" Font-Bold="true" ForeColor="RED"></asp:RequiredFieldValidator>


                                    </td>
                                    <td>&nbsp;&nbsp;</td>

                                    <td>To</td>
                                    <td>&nbsp;&nbsp;</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtPercentTo" Width="100px" Height="30px" ToolTip="% To" placeholder="% To"  MaxLength="2" Enabled="false"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPercentTo" SetFocusOnError="true" InitialValue="" ErrorMessage="*" ValidationGroup="show" Font-Bold="true" ForeColor="RED"></asp:RequiredFieldValidator>

                                    </td>

                                </tr>
                            </table>
                        </td>
                        <td style="padding-left: 15px;">
                            <b>
                                <asp:TextBox runat="server" ID="txtFromtDate" CssClass="form-control input-sm" Width="110px" ToolTip="From Date" Enabled="false" onkeypress="return false"
                                    onKeyDown="preventBackspace();" placeholder="From Date"></asp:TextBox>&nbsp&nbsp
                                <asp:Image src="../Images/Calendar.png" runat="server" Height="30px" Width="30px" alt="" ID="fdate" Visible="false" />
                                <asp:CalendarExtender ID="cleFromDate" Format="dd MMM yyyy" runat="server" OnClientDateSelectionChanged="checkDate" CssClass="cal_Theme1"
                                    PopupButtonID="fdate" Enabled="true" TargetControlID="txtFromtDate" />
                                <%--<asp:RequiredFieldValidator ID="reqFromDate" runat="server" ControlToValidate="txtFromtDate" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="show" Font-Bold="true" ForeColor="RED"></asp:RequiredFieldValidator>--%>
                                &nbsp&nbsp
                            </b>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtToDate" CssClass="form-control input-sm" Width="110px" Enabled="false" ToolTip="To Date" onkeypress="return false"
                                onKeyDown="preventBackspace();" placeholder="To Date"></asp:TextBox>&nbsp&nbsp
                                <asp:Image src="../Images/Calendar.png" runat="server" Height="30px" Width="30px" alt="" ID="tdate" Visible="false" />
                            <asp:CalendarExtender ID="cleToDate" Format="dd MMM yyyy" runat="server" OnClientDateSelectionChanged="checkDate" CssClass="cal_Theme1"
                                PopupButtonID="tdate" Enabled="true" TargetControlID="txtToDate" />
                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtToDate" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="show" Font-Bold="true" ForeColor="RED"></asp:RequiredFieldValidator>--%>
                            &nbsp
                        </td>
                       
                        <td>
                            <asp:Button ID="BtnShow" type="button" Text="Show" Width="90px" class="btn btn-info btn" runat="server"  OnClick="BtnShow_Click" ValidationGroup="show"></asp:Button>
                            <asp:Button ID="Button1" type="button" Text="Show" Width="90px" class="btn btn-info btn" runat="server" Visible="false" OnClick="Button1_Click"></asp:Button>
                           <asp:Button ID="btnExport" type="button"  Width="140px"  runat="server" class="btn btn-info btn" Text="EXPORT TO EXCEL"  OnClick="btnExport_Click"/>



                        </td>
                        <td align="left"></td>
                    </tr>

                </table>







                <asp:GridView ID="GrdDetenee" runat="server" Style="margin-left: 0%; margin-right: 0%; width: 99%" Visible="true" AutoGenerateColumns="false"
                    CssClass="table table-striped table-bordered table-hover" AlternatingRowStyle-CssClass="danger" ShowFooter="true">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate>
                                <%# Container.DataItemIndex +1 %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Enrollment No">
                            <ItemTemplate>
                                <asp:Label ID="lblEnrollment" runat="server" Text='<%# Bind("EnrollmentNo") %>'></asp:Label>
                                <asp:HiddenField ID="SNumber" runat="server" Value='<%# Eval("[Student No_]") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student Name">
                            <ItemTemplate>
                                <asp:Label ID="lblStudentName" runat="server" Text='<%# Bind("StudentName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sem Year">
                            <ItemTemplate>
                                <asp:Label ID="lblSemester" runat="server" Text='<%# Bind("SemestYear") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Course">
                            <ItemTemplate>
                                <asp:Label ID="lblCourse" runat="server" Text='<%# Bind("Course") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subject">
                            <ItemTemplate>
                                <asp:Label ID="lblSubject" runat="server" Text='<%# Bind("Subject") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="%">
                            <ItemTemplate>
                                <asp:Label ID="lblObtainPer" runat="server" Text='<%# Bind("Percentage") %>'></asp:Label>
                                <asp:HiddenField ID="hdnperfrom" runat="server" Value='<%# Bind("PercentageFrom") %>' />
                                <asp:HiddenField ID="hdnperTo" runat="server" Value='<%# Bind("PercentageTo") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField ControlStyle-Width="15px">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" Checked="true" Width="30px" Enabled="false"  OnCheckedChanged="chkAll_CheckedChanged" />
                                Select All
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkStudent" runat="server" AutoPostBack="true" Checked="true" Width="20px"  Enabled="false" OnCheckedChanged="chkStudent_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                           <asp:TemplateField ControlStyle-Width="15px">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkAllUn" runat="server" AutoPostBack="true"  Width="15px" Enabled="false"  OnCheckedChanged="chkAllUn_CheckedChanged" />
                              Allow Detained Student
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkStudentUn" runat="server"  Width="15px" Enabled="false"  OnCheckedChanged="chkStudentUn_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reason">
                            <ItemTemplate>
                                <asp:TextBox ID="txtReason" runat="server" Enabled="false" ></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <EmptyDataTemplate>No Record To Display</EmptyDataTemplate>
                </asp:GridView>
                <div id="confirmModalB" visible="false" runat="server" style="text-align: center; margin-left: 25%;">

                    <table>
                        <tr>
                            <td colspan="2">
                                <p><b>No record found for this range. Do you still want to submit the Detainee List?</b></p>
                                <br />
                            </td>


                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                <asp:TextBox ID="txtremarsk" Width="350px" MaxLength="100" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtremarsk" SetFocusOnError="true" InitialValue="" ErrorMessage="*" ValidationGroup="RR" Font-Bold="true" ForeColor="RED"></asp:RequiredFieldValidator>

                            </td>
                            <td style="text-align: left;">
                                <asp:Button ID="Btnblank" runat="server" OnClick="Btnblank_Click" ValidationGroup="RR" class="btn btn-info btn" Text="Submit" />


                            </td>


                        </tr>

                    </table>

                </div>
                <div style="text-align: right; margin-right: 5%;">
                    <asp:Button ID="btnDetanie" Text="Submit" Visible="false" OnClick="btnDetanie_Click" OnClientClick="if(!confirm('Are you want to submit'))return false;" class="btn btn-info btn" runat="server" />
                    <br />
                    <br />
                </div>
                </td>
             </tr>
           
             </table>
         
                                    


               
                     </div>


            </fieldset>

        </ContentTemplate>

 <Triggers>
          <asp:PostBackTrigger ControlID="btnExport" />
          </Triggers>

    </asp:UpdatePanel>
    <div>
    </div>
</asp:Content>

