<%@ Page Title="" Language="C#" MasterPageFile="~/Student/IndexMaster.master" AutoEventWireup="true" CodeFile="ReExamTemp.aspx.cs" Inherits="Student_ReExamTemp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   

         


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

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="mnupd">
        <ContentTemplate>
            <%--<asp:textbox id="TextBox1"  runat="server" style="border: 1px solid black; height: 125px; margin-left: auto; width: 550px;" textmode="MultiLine"></asp:textbox>   --%>
            <asp:Panel ID="PnlMsg" runat="server" Visible="false">
                <br />
                <br />
                <strong>
                    <label style="font-size: medium">&nbsp;&nbsp;&nbsp;&nbsp The  RE-Examination Form is yet to be Opened.............</label></strong>
            </asp:Panel>
            <asp:Panel ID="PnlMain" runat="server">
                <div style="text-align: right; width: 100%">
                    <asp:Button ID="BtnPrint" OnClientClick="PrintDiv();" runat="server" Width="10%" Text="Print" Font-Bold="true" Visible="false" BorderColor="WhiteSmoke" />
                </div>

                <div class="col-sm-4 p-0 ml-20 ">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label">Sem/Year</label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlSem" CssClass="form-control" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged" AutoPostBack="true" runat="server">
                               

                               
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

               <div id="div_visible_TF" runat="server">
                <div id="printarea" >
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
                    <div class="panel-heading" style="background-color: #2b5b69">
                        <center>
                            <div class="panel-title" style="fit-position: center;">
                                <b>
                                    <p style="color: white; font-size: 22px">
                                        EXAMINATION FORM FOR ACADEMIC SESSION (<asp:Label ID="LblType" runat="server"></asp:Label> <asp:Label ID="TxtSession" runat="server"></asp:Label>)
                                        <br /> 
                                        RE-APPEAR / SUPPLEMENTARY
                                    </p>
                                </b>
                            </div>
                        </center>
                    </div>
                    <fieldset class="boxBodyInner">
                                       <table id="table3" style="margin-left: 5%; margin-right: 10%; font: 14px; font-family: Times New Roman;" width="100%">
                    <tr>
                        <td style="width: 35%">
                            <strong>
                                <asp:Label ID="lblExaminationName" runat="server" Text="1. Name of Examination"></asp:Label>
                            </strong>
                        </td>
                        <td style="width: 30%">
                            <strong>
                                <asp:TextBox ID="txtExaminationName" runat="server" BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                            </strong>
                        </td>
                        <td style="width: 35%" rowspan="7">
                            <strong>
                               
                                <asp:Image ID="ImgStudent" runat="server" Style="border: 1px solid #d4d7d8;width:150px;height:150px;"  ImageUrl='data:CarImages/png;base64,<%# Eval("Student Image") != System.DBNull.Value ? Convert.ToBase64String((byte[])Eval("Student Image")) : string.Empty %>'></asp:Image>
                            
                            </strong>
                            <table style="margin-top:10px;">
                                <tr>
                                    <td><strong>Contact No.</strong></td>
                                    <td>
                                        <strong>
                                            <asp:TextBox ID="TxtContactNo" runat="server" BorderStyle="None" style="margin-top: 2px;width:90px;height:12px;" ReadOnly="true"></asp:TextBox>
                                        </strong>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Label ID="lblSem" runat="server" Text="2. Program - Year / Sem. "></asp:Label>
                            </strong>
                        </td>
                        <td>
                            <strong>
                                <asp:HiddenField ID="hfsem" runat="server" />
                                <asp:TextBox ID="txtSemester" runat="server" BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
                            </strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Label ID="lblBranch" runat="server" Text="3. Name of Stream/Branch"></asp:Label>
                            </strong>
                        </td>
                        <td>
                            <strong>
                                <asp:TextBox ID="TxtBranch" runat="server" BorderStyle="None" Width="300px" ReadOnly="true" CssClass="auto-style9" Style="text-transform: uppercase;"></asp:TextBox>
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
                                <asp:TextBox ID="TxtEnrollmentNo" runat="server" BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
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
                                <asp:TextBox ID="TxtStudentName" runat="server" BorderStyle="None" Width="300px" Style="text-transform: uppercase;"></asp:TextBox>
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
                                <asp:TextBox ID="TxtHindiName" runat="server" BorderStyle="None" Width="300px"></asp:TextBox>
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
                                <asp:TextBox ID="TxtFathersName" runat="server" BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
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
                                <asp:TextBox ID="TxtHindiFathersName" runat="server" BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
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
                                <asp:TextBox ID="TxtMothersName" runat="server" BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
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
                                <asp:TextBox ID="TxtHindiMothersName" runat="server" BorderStyle="None" Width="300px" ReadOnly="true" Style="text-transform: uppercase;"></asp:TextBox>
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
                                <asp:TextBox ID="TxtAdharNo" runat="server" BorderStyle="None" MaxLength="12" Width="300px" ReadOnly="true"></asp:TextBox>
                            </strong>
                        </td>
                        <td style="width: 25%" rowspan="1">
                            
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Label ID="lblPostalAddress" runat="server" Text="12. Postal Address  "></asp:Label>
                            </strong>
                        </td>
                        <td>
                            <strong>
                                <asp:TextBox ID="TxtPostalAddress" runat="server" BorderStyle="None" Width="300px" Style="text-transform: uppercase; resize:none"  ReadOnly="true"  TextMode="MultiLine"></asp:TextBox>
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
                                <asp:TextBox ID="TxtPermanentAdd" runat="server" BorderStyle="None" Width="300px"  ReadOnly="true" Style="text-transform: uppercase; resize:none" TextMode="MultiLine"></asp:TextBox>
                            </strong>
                        </td>
                        <td style="visibility: hidden"></td>
                    </tr>
                </table>
                        <br />

                        <fieldset class="boxBodyHeader">
                            <asp:Label ID="lblExamination" runat="server"
                                Text="11. Detail of the rest examination applied for " Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                        </fieldset>
                        <br />

                        <asp:GridView ID="GrdAppliedExamination" runat="server" Style="margin-left: 8%; margin-right: 8%; width: 84%" Visible="true" AutoGenerateColumns="false"
                            CssClass="table table-striped table-bordered table-hover" AlternatingRowStyle-CssClass="danger" OnRowDataBound="GrdAppliedExamination_RowDataBound" ShowFooter="true">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex +1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Course /Practical Code ">
                                    <ItemTemplate>
                                        <asp:Label ID="LblPracticalCode" runat="server" Text='<%# Bind("[Subject Code]") %>'></asp:Label>
                                          <asp:HiddenField ID="hdStyp" runat="server" Value='<%# Bind("[Depandent Subject]") %>' ></asp:HiddenField>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Course / Practical Name ">
                                    <ItemTemplate>
                                        <asp:Label ID="LblPracticalName" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Semester">
                                    <ItemTemplate>
                                        <asp:Label ID="LblSemester" runat="server" Text='<%# Bind("[Semester]") %>'></asp:Label>
                                        <asp:HiddenField ID="hddetani" runat="server" Value='<%# Bind("[Detained]") %>' ></asp:HiddenField>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged" />
                                        Select All
                                    </HeaderTemplate>
                                    <ItemTemplate>
                           <asp:HiddenField ID="hfReappear" runat="server" Value='<%# Bind ("[Reappear Exam Form Submitted]") %>'></asp:HiddenField>
                           <asp:CheckBox ID="chkStudent" runat="server" AutoPostBack="true" OnCheckedChanged="chkStudent_CheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>

       
                            </Columns>
                            <EmptyDataTemplate>No Record To Display</EmptyDataTemplate>
                        </asp:GridView>
                        <div style="text-align:right;margin-right:13%;"><asp:Button ID="btnCinvoice" Text="save" Visible="false" OnClientClick="if(!confirm('Do you want to submit'))return false;" OnClick="btnCinvoice_Click" runat="server" />
                            <br />
                            <br />
                        </div>
                   

                     <fieldset class="boxBodyHeader">
                            <asp:Label ID="lblReappeare" runat="server"
                                Text="12. Detail of the re-Appear applied" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                        </fieldset>
                        <br />

                        <asp:GridView ID="GrdAppliedRep" runat="server" Style="margin-left: 8%; margin-right: 8%; width: 84%" Visible="true" AutoGenerateColumns="false"
                            CssClass="table table-striped table-bordered table-hover" AlternatingRowStyle-CssClass="danger" OnRowDataBound="GrdAppliedRep_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex +1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                             
                                <asp:TemplateField HeaderText="Course /Practical Code ">
                                    <ItemTemplate>
                                        <asp:Label ID="LblPracticalCode1" runat="server" Text='<%# Bind("[Subject Code]") %>'></asp:Label>
                                        <asp:HiddenField ID="hdStyAr" runat="server" Value='<%# Bind("[Depandent Subject]") %>' ></asp:HiddenField>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Course / Practical Name ">
                                    <ItemTemplate>
                                        <asp:Label ID="LblPracticalName1" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Semester">
                                    <ItemTemplate>
                                        <asp:Label ID="LblSemester1" runat="server" Text='<%# Bind("[Semester]") %>'></asp:Label>
                                        <asp:HiddenField ID="hdtAp" runat="server" Value='<%# Bind("[Detained]") %>' ></asp:HiddenField>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAllrep" runat="server" AutoPostBack="true" Enabled="false" Visible="false"  OnCheckedChanged="chkAllrep_CheckedChanged" />
                                        Status
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfReappear1" runat="server" Value='<%# Bind ("[Reappear Exam Form Submitted]") %>'></asp:HiddenField>
                                        
                                        <asp:CheckBox ID="chkStudentrep" runat="server" Enabled="false" AutoPostBack="true" OnCheckedChanged="chkStudentrep_CheckedChanged"  />
                                    </ItemTemplate>
                                </asp:TemplateField>

         
                            </Columns>
                            <EmptyDataTemplate>No Record To Display</EmptyDataTemplate>
                        </asp:GridView>

                    <%--</fieldset>

                    <fieldset>--%>


                        <fieldset class="boxBodyHeader">
                            <asp:Label ID="lblPreviousExamination" runat="server"
                                Text="13. Detail of Previous Examination" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                        </fieldset>
                        <br />
                        <asp:GridView ID="GrdPreviousExam" runat="server" Style="margin-left: 10%; margin-right: 20%; width: 80%" Visible="true" AutoGenerateColumns="false"
                            CssClass="table table-striped table-bordered table-hover" AlternatingRowStyle-CssClass="danger">
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
                        <div style="page-break-before: always"></div>
                        <fieldset class="boxBodyHeader">
                            <asp:Label ID="LblFees" runat="server"
                                Text="14. Examination fee detail" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
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
                                        <asp:Label ID="lblSemester" runat="server" Text='<%# Bind("[Year]") %>'></asp:Label>
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
                                <asp:HiddenField ID="hdhindi" runat="server" Value='<%# Bind("[Hindi]") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cash Receipt No">
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
                                Text="15. Declaration" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                        </fieldset>
                        <asp:GridView ID="GrdDeclaration" runat="server" Style="margin-left: 10%; margin-right: 20%; width: 80%" AutoGenerateColumns="false"
                            CssClass="table table-striped table-bordered table-hover" Font-Bold="true" RowStyle-Font-Names="Times New Roman" HeaderStyle-BackColor="White">
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkDeclare" runat="server" />
                                        <strong>I declare / under take that </strong>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDeclare" runat="server" Text='<%# Bind("[I declare / under take that]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </fieldset>
                    <asp:Panel ID="PanelHide" runat="server" Visible="false">
                        <fieldset id="FieldsetHide" class="boxBodyInner">
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
                                <p>Certified that &nbsp;
                                    <asp:Label ID="lblName" Font-Bold="true" Font-Underline="true" runat="server"></asp:Label>
                                    &nbsp; is a bonafide student of our college and the particulars mentioned above are true to the best of our knowledge.</p>
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
                                        <td style="width: 15%"></td>
                                        <td style="text-align: left; width: 20%">&nbsp;</td>
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
                   </div>
                <div style="page-break-before: always"></div>
                <div>
                    <table style="margin-left: 10%; margin-right: 10%">
                        <tr>
                            <td>
                                <asp:Button ID="btnSubmit" runat="server" Visible="false" Text="Submit" OnClick="btnSubmit_Click" />
                            </td>
                        </tr>
                    </table>

                </div>
                    
                <br />
                <br />
            </asp:Panel>
                                       

        </ContentTemplate>

       <Triggers>
         <asp:PostBackTrigger ControlID="BtnSubmit" />
        <asp:PostBackTrigger ControlID="BtnPrint" />
            </Triggers>
    </asp:UpdatePanel>
</asp:Content>