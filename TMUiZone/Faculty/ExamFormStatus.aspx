<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true"  CodeFile="ExamFormStatus.aspx.cs" Inherits="Faculty_ExamFormStatus"  EnableEventValidation="false" %>





<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <style type="text/css">
        .csspager a, .csspager span {
            display: inline-flexbox;
            font-weight: bold;
            text-align: center;
            text-decoration: none;
            border: 1px solid white;
            padding: 2px;
            margin: 5px;
        }

        .csspager a {
            border: 1px solid #969696;
            height: 5px;
        }

        .csspager span {
            background-color: #A1DCF2;
            color: #000;
            border: 1px solid #3AC0F2;
        }
    </style>

    <style>
        .rbl input[type="radio"] {
            margin-left: 10px;
            margin-right: 1px;
        }

        #confirmModalB .modal-dialog.modalPopup {
            width: 95%;
        }

        table thead tr th:first-child, .table > tbody > tr > th:first-child {
            border-left: 1px solid #60594f;
            padding: 5px 8px;
        }
    </style>

    <script type="text/javascript">
        function PrintDiv() {
            var divToPrint = document.getElementById('printarea');
            var popupWin = window.open('', '_blank', 'width=300,height=400,location=no,left=200px');
            popupWin.document.open();
            popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
            popupWin.document.close();
        }

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <asp:ScriptManager ID="ty" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="fe" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <fieldset class="boxBodyInner">
                <div class="row tmu-form ml-5 mr-5">
                    <div class="col-sm-3 p-0">
                        <p style="color: black; font-size: 20px">Exam Form Status :</p>
                    </div>
                    <div class="col-sm-3 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label">ExamType</label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlReaapear" runat="server" AutoPostBack="true" CssClass="form-control" >
                                    <asp:ListItem Value="0" Text="Main" Selected="True"> </asp:ListItem>
                                    <asp:ListItem Value="1" Text="Re-Appear"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                   <%-- <div class="col-sm-8 p-0">
                         
                        <asp:RadioButtonList ID="Rblist" runat="server" OnSelectedIndexChanged="Rblist_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal" CssClass="rbl">
                            <asp:ListItem Value="0" Text="Pending" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Approved"></asp:ListItem>
                            <asp:ListItem Value="5" Text="Rejected"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Approved by Principal"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Rejected by Principal"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Rejected by COE"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>--%>


                </div>
            </fieldset>



            <br />
            <fieldset class="boxBodyInner">
                <div class="row tmu-form ml-5 mr-5">

                    <div class="col-sm-3 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label">Academic Year</label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlAcademicYear" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-3 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label">Course</label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4 p-0">
                        <div class="form-group clearfix">
                            <div id="divSp" runat="server" visible="false">
                                <label for="inputEmail3" style="margin-right: 5px" class="col-form-label">Special</label>
                                <asp:CheckBox ID="Chekap" runat="server" OnCheckedChanged="Chekap_CheckedChanged" AutoPostBack="true" />

                            </div>
                            <label for="inputEmail3" class="col-form-label">Sem/Year</label>
                            <div class="col-sm-7">
                                <asp:DropDownList ID="ddlSem" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>

                            </div>



                        </div>
                    </div>

                    <div class="col-sm-4 p-0 text-right">
                           <asp:Button ID="BtnShow" runat="server" Text="Show" CssClass="btn" OnClick="BtnShow_Click" />
                          <asp:Button ID="btnReport" runat="server" CssClass="btn" Text="Export to Excel" OnClick="btnReport_Click" />
                     
                        

                    </div>
                </div>
            </fieldset>
            <fieldset class="boxBodyInner">
                <div class="text-center">
                    <asp:GridView ID="GrdExamList" runat="server" DataKeyNames="Enrollment No" AlternatingRowStyle-CssClass="danger" PageSize="25" OnPageIndexChanging="GrdExamList_PageIndexChanging"
                        AllowPaging="true" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" Visible="true">
                        <PagerStyle CssClass="csspager" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>

                            <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex +1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Enrolment No" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblEnrollNo" runat="server" Text='<%# Bind("[Enrollment No]") %>'></asp:Label>
                                    <asp:HiddenField ID="HfEnrollmentNo" Value='<%# Eval("[Enrollment No]") %>' runat="server" />
                                    <asp:HiddenField ID="HfStudentNo" Value='<%# Eval("[No_]") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                          <%--  <asp:TemplateField HeaderText="View" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnview" runat="server" OnClick="btnview_Click" Text="View"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="Name" ItemStyle-Width="4%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Student Name") %>' Style="text-transform: uppercase;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Course" ItemStyle-Width="3%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                <ItemTemplate>
                                    <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("Course") %>' Style="text-transform: uppercase;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Course Name" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblCourseName" runat="server" Text='<%#Eval("Course Name") %>' Style="text-transform: uppercase;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Semester/Year" ItemStyle-Width="2%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("Semester") %>'></asp:Label>
                                    <asp:HiddenField ID="HfSemester_S" Value='<%# Eval("[Semester]") %>' runat="server" />
                                    <asp:HiddenField ID="HFSemY" Value='<%# Eval("[Semester]") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Exam Form Status" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemark" runat="server" Text='<%#Eval("ReappearStatus") %>' Style="text-transform: uppercase;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Admit Card Status" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblAdmit" runat="server" Text='<%#Eval("AdmitCardStatus") %>' Style="text-transform: uppercase;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                        </Columns>
                        <EmptyDataTemplate>No Record To Display</EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </fieldset>

            <div id="confirmModalB" class="modal fade confirm-modal" role="dialog">

                <div class="modal-dialog modalPopup border-box">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" name="btn_close" id="Button1" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <div class="clearfix">
                            <div class="col-sm-12">
                                <asp:Panel runat="server" ID="Panel2">
                                    <asp:Panel ID="PnlMain" runat="server">
                                        <%-- <asp:textbox id="TextBox1"  runat="server" style="border: 1px solid black; height: 125px; margin-left: auto; width: 550px;" textmode="MultiLine"></asp:textbox>   --%>
                                        <div style="text-align: right; width: 100%; margin-bottom: -30px;">
                                            <asp:Button ID="BtnPrint" OnClientClick="PrintDiv();" runat="server" Width="10%" Style="margin-top: 5px;" Text="Print" Font-Bold="true" BorderColor="WhiteSmoke" />
                                        </div>

                                        <div id="printarea">
                                            <div style="text-align: center; width: 100%" id="logoDiv">
                                                <table style="width: 80%; margin-left: 10%;">
                                                    <tr>
                                                        <td style="width: 200px;" align="left">
                                                            <img src="~/images/rightlogo.png" id="Image1" runat="server" width="100" height="102" visible="true" />
                                                        </td>
                                                        <td style="width: 80%; vertical-align: middle" align="left">
                                                            <asp:Label ID="LblTitle" runat="server" Text="Teerthanker Mahaveer University,Moradabad" Style="font-size: 25px;"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>


                                            <asp:Repeater ID="RepSinformation" runat="server">

                                                <ItemTemplate>

                                                    <div class="panel-heading" style="background-color: #2b5b69">
                                                        <center>
                                                            <div class="panel-title" style="fit-position: center;">
                                                                <b>
                                                                    <p style="color: white; font-size: 22px">
                                                                        EXAMINATION FORM FOR ACADEMIC SESSION (<asp:Label ID="LblType" runat="server" Text='<%#Eval("ExaminationType") %>'></asp:Label>
                                                                        <asp:Label ID="TxtSession" runat="server" Text='<%#Eval("Academic Year") %>'></asp:Label>

                                                                    </p>
                                                                </b>
                                                            </div>
                                                        </center>
                                                    </div>



                                                    <table id="table3" style="margin-left: 5%; margin-right: 10%; font: 14px; font-family: Times New Roman;" width="100%">
                                                        <tr>
                                                            <td style="width: 35%">
                                                                <strong>
                                                                    <asp:Label ID="lblExaminationName" runat="server" Text="1. Name of Examination"></asp:Label>

                                                                </strong>
                                                            </td>
                                                            <td style="width: 30%">
                                                                <strong>
                                                                    <asp:TextBox ID="txtExaminationName" runat="server" Text='<%#Eval("Course Name") %>' BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase; resize: none" TextMode="MultiLine"></asp:TextBox>
                                                                </strong>
                                                            </td>
                                                            <td style="width: 35%" rowspan="7">
                                                                <strong>
                                                                    <div style="text-align: left; margin-right: 14%">
                                                                        <img src='data:CarImages/png;base64,<%# Eval("Student Image") != System.DBNull.Value ? Convert.ToBase64String((byte[])Eval("Student Image")) : string.Empty %>' alt="image" height="150" width="120" style="border: 1px solid black" />
                                                                    </div>


                                                                </strong>
                                                                <table style="margin-top: 10px;">
                                                                    <tr>
                                                                        <td><strong>Contact No.</strong></td>
                                                                        <td>
                                                                            <strong>
                                                                                <asp:TextBox ID="TxtContactNo" runat="server" Text='<%#Eval("Mobile Number") %>' BorderStyle="None" Style="margin-top: 2px; width: 90px; height: 12px;" ReadOnly="true"></asp:TextBox>
                                                                            </strong>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <strong>
                                                                    <asp:Label ID="lblSem" runat="server" Text='<%#Eval("SemHead") %>'></asp:Label>
                                                                </strong>
                                                            </td>
                                                            <td>
                                                                <strong>
                                                                    <asp:TextBox ID="txtSemester" runat="server" Text='<%#Eval("Sem") %>' BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                                                </strong>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <strong>
                                                                    <asp:Label ID="lblBranch" runat="server" Text="3. Program Code"></asp:Label>
                                                                </strong>
                                                            </td>
                                                            <td>
                                                                <strong>
                                                                    <asp:TextBox ID="TxtBranch" runat="server" Text='<%#Eval("Course Code") %>' BorderStyle="None" Width="300px" ReadOnly="true" CssClass="auto-style9" Style="text-transform: uppercase;"></asp:TextBox>
                                                                </strong>
                                                            </td>


                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <strong>
                                                                    <asp:Label ID="LblEnrollmentNo" runat="server" Text="4. Enrolment No."></asp:Label>
                                                                </strong>
                                                            </td>
                                                            <td>
                                                                <strong>
                                                                    <asp:TextBox ID="TxtEnrollmentNo" runat="server" Text='<%#Eval("Enrollment No_") %>' BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                                                </strong>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <strong>
                                                                    <asp:Label ID="lblStudentName" runat="server" Text="5. Name of Student"></asp:Label>
                                                                </strong>
                                                            </td>
                                                            <td>
                                                                <strong>
                                                                    <asp:TextBox ID="TxtStudentName" runat="server" Text='<%#Eval("Student Name") %>' BorderStyle="None" Width="300px" Style="text-transform: uppercase;"></asp:TextBox>
                                                                </strong>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <strong>
                                                                    <asp:Label ID="LblHindiName" runat="server" Text="6. छात्र/छात्रा का नाम"></asp:Label>
                                                                </strong>
                                                            </td>
                                                            <td>
                                                                <strong>
                                                                    <asp:TextBox ID="TxtHindiName" runat="server" Text='<%#Eval("StudentHindiName") %>' BorderStyle="None" Width="300px"></asp:TextBox>
                                                                </strong>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td>

                                                                <strong>

                                                                    <asp:Label ID="lblFathersName" runat="server" Text="7. Father's Name "></asp:Label>
                                                                </strong>
                                                            </td>
                                                            <td>
                                                                <strong>
                                                                    <asp:TextBox ID="TxtFathersName" runat="server" Text='<%#Eval("Fathers Name") %>' BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                                                </strong>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td>

                                                                <strong>

                                                                    <asp:Label ID="LblHindiFatherName" runat="server" Text="8. पिता का नाम "></asp:Label>
                                                                </strong>
                                                            </td>
                                                            <td>
                                                                <strong>
                                                                    <asp:TextBox ID="TxtHindiFathersName" runat="server" Text='<%#Eval("FatherHindiName") %>' BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                                                </strong>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <strong>
                                                                    <asp:Label ID="lblMothersName" runat="server" Text="9.  Mother’s Name "></asp:Label>
                                                                </strong>
                                                            </td>
                                                            <td>
                                                                <strong>
                                                                    <asp:TextBox ID="TxtMothersName" runat="server" Text='<%#Eval("Mothers Name") %>' BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                                                </strong>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td>

                                                                <strong>

                                                                    <asp:Label ID="LblHindiMothersName" runat="server" Text="10. माता का नाम "></asp:Label>
                                                                </strong>
                                                            </td>
                                                            <td>
                                                                <strong>
                                                                    <asp:TextBox ID="TxtHindiMothersName" runat="server" Text='<%#Eval("MotherHindiName") %>' BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                                                                </strong>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <strong>
                                                                    <asp:Label ID="lblAdharNo" runat="server" Text="11. Aadhar No."></asp:Label>
                                                                </strong>
                                                            </td>
                                                            <td>
                                                                <strong>
                                                                    <asp:TextBox ID="TxtAdharNo" runat="server" Visible="false" BorderStyle="None" MaxLength="12" Width="300px" ReadOnly="true"></asp:TextBox>
                                                                </strong>
                                                            </td>
                                                            <td style="width: 25%" rowspan="1"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <strong>
                                                                    <asp:Label ID="lblPostalAddress" runat="server" Text="12. Postal Address  "></asp:Label>
                                                                </strong>
                                                            </td>
                                                            <td>
                                                                <strong>
                                                                    <asp:TextBox ID="TxtPostalAddress" runat="server" Text='<%#Eval("PAdress") %>' BorderStyle="None" Width="300px" Style="text-transform: uppercase; resize: none" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
                                                                </strong>
                                                            </td>
                                                            <td style="visibility: hidden;"></td>

                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <strong>
                                                                    <asp:Label ID="lblPermanentAdd" runat="server" Text="13.  Permanent Address "></asp:Label>
                                                                </strong>
                                                            </td>
                                                            <td>
                                                                <strong>
                                                                    <asp:TextBox ID="TxtPermanentAdd" runat="server" Text='<%#Eval("Adress") %>' BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase; resize: none" TextMode="MultiLine"></asp:TextBox>
                                                                </strong>
                                                            </td>
                                                            <td style="visibility: hidden"></td>
                                                        </tr>
                                                    </table>

                                                </ItemTemplate>
                                            </asp:Repeater>


                                            <fieldset class="boxBodyInner">

                                                <br />
                                                <fieldset class="boxBodyHeader">
                                                    <asp:Label ID="lblExamination" runat="server"
                                                        Text="14. Detail of the Examination applied for " Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                                                </fieldset>
                                                <br />
                                                <asp:GridView ID="GrdAppliedExamination" runat="server" Style="margin-left: 10%; margin-right: 10%; width: 80%" Visible="true" AutoGenerateColumns="false"
                                                    CssClass="table table-striped table-bordered table-hover" AlternatingRowStyle-CssClass="danger">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex +1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Course /Practical Code ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblPracticalCode" runat="server" Text='<%# Bind("[Subject Code]") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Course / Practical Name ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblPracticalName" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Category ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblCategory" runat="server" Text='<%# Bind("[Subject Classification]") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <br />
                                                <br />
                                            </fieldset>




                                            <fieldset class="boxBodyInner">
                                                <div style="page-break-before: always"></div>
                                                <fieldset class="boxBodyHeader">
                                                    <asp:Label ID="lblPreviousExamination" runat="server"
                                                        Text="15. Detail of Previous Examination" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                                                </fieldset>
                                                <asp:GridView ID="GrdPreviousExam" runat="server" Style="margin-left: 10%; margin-right: 20%; width: 80%" Visible="true"
                                                    AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" AlternatingRowStyle-CssClass="danger">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Name of examination">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblExamName" runat="server" Text='<%# Bind("[Name of examination]") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Semester/Year">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblYear" runat="server" Text='<%# Bind("[Sem Year]") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="University/ Board">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBoard" runat="server" Text='<%# Bind("[University Board]") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText=" % of Marks">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMarks" runat="server" Text='<%# Bind("[% of marks]") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <br />
                                                <fieldset class="boxBodyHeader">
                                                    <asp:Label ID="LblFees" runat="server"
                                                        Text="16. Examination fee detail" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

                                                </fieldset>
                                                <asp:GridView ID="GridViewFees" runat="server" Style="margin-left: 10%; margin-right: 20%; width: 80%" Visible="true" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" AlternatingRowStyle-CssClass="danger">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Programme ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProgramme" runat="server" Text='<%# Bind("Course") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Year/Semester">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSemesterM" runat="server" Text='<%# Bind("[Year]") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Session">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSession" runat="server" Text='<%# Bind("[Academic Year]") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText=" Due Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFee" runat="server" Text='<%# Bind("[Due Amount]") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText=" Paid Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPaidFee" runat="server" Text='<%# Bind("[Fee Paid]") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CR No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCashReceipt" runat="server" Text='<%# Bind("[Document No_]") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText=" Date ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDate" runat="server" Text='<%# Bind("[Posting Date]") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <br />
                                                <fieldset class="boxBodyHeader">
                                                    <asp:Label ID="Label1" runat="server"
                                                        Text="17. Declaration" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

                                                </fieldset>
                                                <asp:GridView ID="GrdDeclaration" runat="server" Style="margin-left: 10%; margin-right: 20%; width: 80%" AutoGenerateColumns="false"
                                                    CssClass="table table-striped table-bordered table-hover mt-10" Font-Bold="true" RowStyle-Font-Names="Times New Roman" HeaderStyle-BackColor="White">
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkDeclare" Checked="true" Enabled="false" runat="server" />
                                                                <strong>I declare / under take that </strong>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDeclare" runat="server" Text='<%# Bind("[I declare / under take that]") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:HiddenField ID="HfEnrollment_No" runat="server" />
                                                <asp:HiddenField ID="HfStudent_No" runat="server" />
                                                <asp:HiddenField ID="HFsemester" runat="server" />
                                            </fieldset>
                                            <asp:Panel ID="PanelHide" runat="server" Visible="true">
                                                <fieldset id="FieldsetHide">
                                                    <div>
                                                        <br />
                                                        <table style="margin-left: 10%; margin-right: 10%; width: 100%">
                                                            <tr>
                                                                <td style="text-align: left; width: 35%">
                                                                    <asp:Label ID="lbldate" runat="server" Text="Date:"></asp:Label>
                                                                </td>
                                                                <td style="width: 30%"></td>
                                                                <td style="text-align: left; width: 35%">
                                                                    <asp:Label ID="lblSignature" runat="server" Text="Student Signature"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div style="margin-left: 10%; margin-right: 10%">
                                                        <p>
                                                            Forwarded by: 
                                                        </p>
                                                        <p>
                                                            Certified that &nbsp;
                            <asp:Label ID="lblName" Font-Bold="true" Font-Underline="true" runat="server"></asp:Label>
                                                            &nbsp; is a bonafide student of our college and the particulars mentioned above are true to the best of our knowledge.
                                                        </p>
                                                    </div>
                                                    <br />
                                                    <div>
                                                        <table style="margin-left: 10%; margin-right: 10%; width: 100%">
                                                            <tr>
                                                                <td style="width: 25%">
                                                                    <asp:Label ID="LabelCordinator" runat="server" Text="  Programme / Class Coordinator "></asp:Label>
                                                                </td>
                                                                <td style="text-align: left; width: 30%">&nbsp;</td>
                                                                <td style="text-align: left; width: 45%">
                                                                    <asp:Label ID="Labsign" runat="server" Text=" Principal / Director / Dean 
                                Seal of College/Department "></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <hr />
                                                    <div style="text-align: center;">
                                                        <asp:Label ID="head" runat="server" Text="For Office Use Only " Font-Bold="true"></asp:Label>
                                                        <table style="width: 100%; margin-left: 10%; margin-right: 10%">
                                                            <tr>
                                                                <td style="text-align: left; width: 35%">
                                                                    <asp:Label ID="Label2" runat="server" Text="  Examination Fee details: "></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="Label3" runat="server" Text="  Candidature – Accepted / Rejected / Detained  "></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left; width: 35%">
                                                                    <asp:Label ID="Label4" runat="server" Text="  Admit Card :   "></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left; width: 35%">
                                                                    <asp:Label ID="Label5" runat="server" Text=" Issued / On hold :   "></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 50%" align="left">
                                                                    <strong>
                                                                        <asp:Label ID="Label9" runat="server" Text="Checked By"></asp:Label>
                                                                    </strong>
                                                                </td>

                                                                <td style="text-align: left; width: 50%">
                                                                    <strong>
                                                                        <asp:Label ID="Label8" runat="server" Text=" Jt. Controller of Examinations"></asp:Label></strong>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <hr />
                                                        <table style="width: 100%; margin-left: 10%; margin-right: 10%">
                                                            <tr>

                                                                <td style="width: 25%"></td>
                                                                <td style="text-align: left; width: 25%">&nbsp;</td>
                                                                <td style="text-align: left; width: 35%">
                                                                    <strong>
                                                                        <asp:Label ID="Label6" runat="server" Text=" Approved by"></asp:Label></strong>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <br />
                                                        <table style="width: 100%; margin-left: 10%; margin-right: 10%">
                                                            <tr>
                                                                <td style="width: 30%"></td>
                                                                <td style="text-align: left; width: 25%">&nbsp;</td>
                                                                <td style="text-align: left; width: 45%">
                                                                    <strong>
                                                                        <asp:Label ID="Label7" runat="server" Text=" Controller of  Examinations "></asp:Label>
                                                                    </strong>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </fieldset>
                                            </asp:Panel>
                                        </div>
                                    </asp:Panel>
                                </asp:Panel>
                            </div>


                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnShow" />
           <%-- <asp:PostBackTrigger ControlID="BtnSubmit" />--%>
           <%-- <asp:PostBackTrigger ControlID="BtnRejected" />
            <asp:PostBackTrigger ControlID="BtnPrint" />--%>
        </Triggers>
    </asp:UpdatePanel>


</asp:Content>

