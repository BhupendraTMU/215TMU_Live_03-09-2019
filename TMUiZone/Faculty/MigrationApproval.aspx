<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="MigrationApproval.aspx.cs" Inherits="Faculty_MigrationApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function HidePopup1() {

            $('#ApplicationModel').modal('hide');
        }
        function VisiblePopup10() {
            $('#ApplicationModel').modal('show');


        }
        function HideCertificateModel() {
            $('#CertificateModel').modal('show');
        }
    </script>
    <script type="text/javascript">
        function PrintDiv() {
            var divToPrint = document.getElementById('printarea');
            var popupWin = window.open('', '_blank', 'width=80%,height=950px,location=no,left=200px');
            popupWin.document.open();
            popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
            popupWin.document.close();
        }
        function PrintDiv1() {
            var divToPrint = document.getElementById('printareacertificate');
            var popupWin = window.open('', '_blank', 'width=80%,height=950px,location=no,left=200px');
            popupWin.document.open();
            popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
            popupWin.document.close();
        }
    </script>
    <%-- <style>
        p {
            margin-bottom: 2px;
        }

            p > strong {
                margin-bottom: 2px;
                display: block;
            }
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="Migration Approval" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
    </fieldset>

    <fieldset class="boxBodyInner">
        <div>
            <asp:Button ID="btnExport" runat="server" Text="Export to Excel" 
    CssClass="btn btn-success" OnClick="btnExport_Click" />
        </div>
        <div class="row">
            <asp:Panel ID="pnlMigration" runat="server">
                <asp:GridView ID="GrdMigrationStudent" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover" CellPadding="20" Width="100%" EmptyDataText="No Data to display">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex +1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Application No." HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="AppNo_" runat="server" Text='<%# Bind("[App_No]") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Enrollment No" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblEnrollment_No" runat="server" Text='<%# Bind("[Enrollment_No]") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student Name" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblStudent_Name" runat="server" Text='<%# Bind("[Student_Name]") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Father Name" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblFather_Name" runat="server" Text='<%# Bind("[Father_Name]") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Certificate Type" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblCertificate_Type" runat="server" Text='<%# Bind("[Certificate_Type]") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Certificate" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblCertificate" runat="server" Text='<%# Bind("Certificate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Create date" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblCreatedate" Width="100%" runat="server" Text='<%# Bind("Create_date") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Paymnet Amount" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblPay_amount" runat="server" Text='<%# Bind("[pay_amount]") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Paymnet Status" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblPayment_Status" runat="server" Text='<%# Bind("[Payment_Status]") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblApp_Status" runat="server" Text='<%# Bind("[App_Status]") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="View Details" HeaderStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:LinkButton ID="lblView" runat="server" CommandArgument='<%# Eval("App_No") %>'  Text="View" OnClick="lblView_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Application Form" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblApplication" Visible='<%# Eval("FacultyCode").ToString() == "TMU00865" ? false : true %>' runat="server" CommandArgument='<%# Eval("App_No") %>' Text="View" OnClick="lblApplication_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Certificate" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkCertificate" runat="server"  Visible='<%# Eval("FacultyCode").ToString() == "TMU00865" ? false : true %>' CommandArgument='<%# Eval("App_No") %>' Text="View" OnClick="lblCertificate_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </asp:Panel>
        </div>
    </fieldset>

    <div id="confirmModal1" class="modal fade confirm-modal" role="dialog">

        <div class="modal-dialog modalPopup" style="width: 1200px; height: 400px; overflow: scroll">
            <div style="text-align: right; padding-bottom: -40px">
                <asp:Button ID="Button1" runat="server" Text="X" OnClientClick="HidePopup1();" Font-Size="Larger" />
            </div>
            <div class="clearfix" style="margin-bottom: 10px; margin-top: 10px; margin-left: 20px">

                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnApprove" />
                        <asp:PostBackTrigger ControlID="btnRejectPop" />
                        <asp:PostBackTrigger ControlID="lnkSupport" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="col-sm-3 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp Application No</label>
                                <asp:HiddenField ID="hdfApplicationNo" runat="server" />
                                <div class="col-sm-8">
                                    <asp:Label ID="lblAppNo" runat="server"></asp:Label>

                                </div>
                            </div>
                        </div>

                        <div class="col-sm-3 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Enrollment No</label>
                                <div class="col-sm-8">
                                    <asp:Label ID="lblEnrollNo" runat="server"></asp:Label>

                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Student Name</label>

                                <div class="col-sm-8">
                                    <asp:Label ID="lblStudentName" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-1 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp College</label>
                                <div class="col-sm-8">
                                    <asp:Label ID="lblCollege" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp  Program </label>

                                <div class="col-sm-8">
                                    <asp:Label ID="lblProgram" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-3 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Description </label>

                                <div class="col-sm-8">
                                    <asp:Label ID="lblDesc" runat="server"></asp:Label>

                                </div>
                            </div>
                        </div>





                        <div class="col-sm-3 p-0">

                            <div class="form-group clearfix">

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Certificate Type </label>

                                <div class="col-sm-8">
                                    <asp:Label ID="lblCertificatetype" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">

                            <div class="form-group clearfix">

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Account Remark </label>

                                <div class="col-sm-8">
                                    <asp:Label ID="lblExamRemark" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">

                            <div class="form-group clearfix">

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Dept Remark </label>

                                <div class="col-sm-8">
                                    <asp:Label ID="lbldeptRemark" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">

                            <div class="form-group clearfix">

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Country </label>

                                <div class="col-sm-8">
                                    <asp:Label ID="lblCountry" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">

                            <div class="form-group clearfix">

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp State </label>

                                <div class="col-sm-8">
                                    <asp:Label ID="lblState" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">

                            <div class="form-group clearfix">

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp City </label>

                                <div class="col-sm-8">
                                    <asp:Label ID="lblcity" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">

                            <div class="form-group clearfix">

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Post Code </label>

                                <div class="col-sm-8">
                                    <asp:Label ID="lblpostcode" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">

                            <div class="form-group clearfix">

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Address </label>

                                <div class="col-sm-8">
                                    <asp:Label ID="lbladdress" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-3 p-0">

                            <div class="form-group clearfix">

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp  Affidavit </label>

                                <div class="col-sm-8">
                                    <asp:LinkButton ID="lnkSupport" runat="server" Text="Download" OnClick="lnkSupport_Click"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                         <div class="col-sm-3 p-0">

                            <div class="form-group clearfix">

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Payment Order No </label>
                               
                                <div class="col-sm-8">
                                    <asp:Label ID="txtPayOrderNo" runat="server" TextMode="MultiLine" ValidationGroup="R1" OnClick="txtRemark_Click"></asp:Label>
                                </div>
                            </div>
                        </div>
                         <div class="col-sm-3 p-0">

                            <div class="form-group clearfix">

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp  Payment Date </label>
                               
                                <div class="col-sm-8">
                                    <asp:Label ID="txtPaymentDate" runat="server" TextMode="MultiLine" ValidationGroup="R1" OnClick="txtRemark_Click"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">

                            <div class="form-group clearfix">

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp  Remark </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ValidationGroup="R1" Display="Dynamic" ControlToValidate="txtRemark" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" ValidationGroup="R1" OnClick="txtRemark_Click"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        
                       


                        <div class="col-sm-2 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font: bold; visibility: hidden; font-size: small">&nbsp&nbsp&nbsp  Registration fee details  </label>

                                <div class="col-sm-8">

                                    <asp:Button ID="btnApprove" runat="server" Text="Approved" Font-Bold="true" ValidationGroup="R1" AutoPostBack="true" OnClick="btnApprove_Click" Height="32px" Font-Size="small" class="btn btn-info" Width="120px"></asp:Button>
                                    <asp:Button ID="btnRejectPop" runat="server" Text="Reject" Font-Bold="true" ValidationGroup="R1" AutoPostBack="true" OnClick="btnRejectPop_Click" Height="32px" Font-Size="small" class="btn btn-info" Width="120px"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>

                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div id="ApplicationModel" class="modal fade confirm-modal" role="dialog">

        <div class="modal-dialog modalPopup" style="width: 900px; height: 900px; overflow: scroll">
            <div style="text-align: right; padding-bottom: -40px">

                <asp:Button ID="BtnPrint" OnClientClick="PrintDiv()" runat="server" Width="10%" Style="margin-top: 5px;" Text="Print" Font-Bold="true" BorderColor="WhiteSmoke" />

                <asp:Button ID="Button2" runat="server" Text="X" OnClientClick="HidePopup1();" Font-Size="Larger" />
            </div>
            <div class="clearfix" style="margin-bottom: 10px; margin-top: 10px; margin-left: 20px">


                <div class="header">
                    <h2>

                        <asp:Label ID="lblNotification" runat="server" Text="Migration Form"></asp:Label>
                    </h2>

                </div>

                <fieldset class="boxBody">
                    <div id="printarea" style="width: 99%;">



                        <%--<div style="width: 100%; margin-bottom: 10px; margin-left: 1%; margin-right: 1%; margin-top: 5px;">--%>
                        <div id="bill">
                            <table>
                                <tr>
                                    <td style="width: 1%"></td>
                                    <td style="width: 10%; text-align: left">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/UPDATEDLOGO.jpg" Width="70%" />

                                    </td>
                                    <td style="width: 30%; text-align: center">
                                        <p style="margin-bottom: -20px">
                                            <strong>
                                                <asp:Label ID="lblcentral" Font-Size="Large" Font-Bold="true" Text="TEERTHANKER MAHAVEER UNIVERSITY" runat="server"></asp:Label>
                                            </strong>
                                        </p>
                                        <br />
                                        <p style="margin-bottom: -20px">
                                            <strong>
                                                <asp:Label ID="lblAC" runat="server" Text="(Established under Govt. of U. P. Act No. 30, 2008)"></asp:Label>
                                            </strong>
                                        </p>
                                        <br />
                                        <p>
                                            <strong>
                                                <asp:Label ID="LblType" runat="server" Text="Delhi Road,Moradabad(U.P) India"></asp:Label>
                                            </strong>
                                        </p>
                                        <br />
                                    </td>
                                    <td style="width: 10%; text-align: center"></td>
                                </tr>
                            </table>


                            <div style="text-align: center;">
                                <asp:Label ID="Label2" runat="server" Text="APPLICATION FORM FOR MIGRATION CERTIFICATE" Font-Size="15pt" Font-Bold="true" ForeColor="Black" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                                <br />
                                <p style="margin-bottom: -15px">
                                    <strong>
                                        <asp:Label ID="Label3" runat="server" Text="(For migration of students from Teerthanker Mahaveer University to other Universities/Institutions)"></asp:Label>
                                    </strong>
                                </p>
                            </div>
                            <br />
                            <div id="divGeneralBody">
                                <div style="padding-left: 10px">
                                    <div class="form-horizontal">
                                        <div class="box-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <table>
                                                        <tr>
                                                            <td style="width: 90%">i. All particulars should be filled in by the candidate himself/herself.
                                                            <br />
                                                                ii. No person is entitled to apply or to receive the migration certificate on behalf of another person.
                                                                <br />
                                                                iii. A DD/University Fee Receipt for non refundable migration fee of Rs.1,000/- must be attached.
                                                               <br />
                                                                iv. Attested photocopy of the certificate of last examination passed/failed must be attached.
                                                               <br />
                                                                v. A self addressed duly stamped envelope should be attached.
                                                               <br />
                                                                vi. The office shall not be responsible for any delay if the form is found incomplete.
                                                              

                                                            </td>
                                                            <td style="width: 10%">
                                                                <asp:UpdatePanel ID="pnlpic" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:Image ID="ImgPrv" Height="100px" Width="100px" runat="server" />
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                    </table>




                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div style="text-align: center;">

                                    <p style="margin-bottom: -15px">
                                        <strong>
                                            <asp:Label ID="Label5" runat="server" Font-Bold="true" Text="A. APPLICANT’S DETAILS"></asp:Label>
                                        </strong>
                                    </p>
                                </div>


                                <div class="col-md-12" style="margin-top: 20px;">
                                    <table>
                                        <tr>
                                            <td style="text-align: left; width: 250px">

                                                <asp:Label ID="Label222" runat="server" Text="Name: (In capitals)" Font-Size="Small"></asp:Label>
                                            </td>
                                            <td style="text-align: left; padding-right: 20px; width: 250px">
                                                <asp:Label ID="lblName" runat="server" Text="BHUPENDRA SINGH" Font-Size="Small" AutoPostBack="false"></asp:Label>
                                            </td>
                                            <td style="text-align: left; width: 250px">
                                                <asp:Label ID="Label4" runat="server" Text="Father’s Name: (In capitals)" Font-Size="Small"></asp:Label>
                                            </td>
                                            <td style="text-align: left; padding-right: 20px; width: 250px">
                                                <asp:Label ID="Label6" runat="server" Text="BHUPENDRA SINGH" Font-Size="Small" AutoPostBack="false"></asp:Label>
                                            </td>

                                        </tr>
                                    </table>
                                </div>
                                <%-- <div class="col-md-12" style="height: 10px">--%>

                                <div class="col-md-12" style="margin-top: 10px;">
                                    <table>
                                        <tr>
                                            <td style="text-align: left; width: 250px">

                                                <asp:Label ID="Label7" runat="server" Text="Mother’s Name: (In capitals)" Font-Size="Small"></asp:Label>
                                            </td>
                                            <td style="text-align: left; padding-right: 20px; width: 250px">
                                                <asp:Label ID="Label8" runat="server" Text="BHUPENDRA SINGH" Font-Size="Small" AutoPostBack="false"></asp:Label>
                                            </td>
                                            <td style="text-align: left; width: 250px">
                                                <asp:Label ID="Label9" runat="server" Text="University Registration No.:" Font-Size="Small"></asp:Label>
                                            </td>
                                            <td style="text-align: left; padding-right: 20px; width: 250px">
                                                <asp:Label ID="Label10" runat="server" Text="TMU05573" Font-Size="Small" AutoPostBack="false"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <div class="col-md-12" style="margin-top: 10px;">
                                    <table>
                                        <tr>
                                            <td style="text-align: left; width: 228px">
                                                <asp:Label ID="Label11" runat="server" Text="Present Enrollment No.:" Font-Size="Small"></asp:Label>
                                            </td>
                                            <td style="text-align: left; padding-right: 20px; width: 250px">
                                                <asp:Label ID="Label12" runat="server" Text="TCA2201273" Font-Size="Small" AutoPostBack="false"></asp:Label>
                                            </td>
                                            <td style="text-align: left; width: 250px"></td>
                                            <td style="text-align: left; padding-right: 20px; width: 250px"></td>
                                        </tr>
                                    </table>
                                </div>

                                <div class="col-md-12" style="margin-top: 10px;">
                                    <div>
                                        <asp:Label ID="Label13" runat="server" Font-Bold="true" Text="Particulars of Last Examination of Teerthanker Mahaveer University in which passed/appeared:" Font-Size="Small"></asp:Label>
                                    </div>
                                    <div class="col-md-12" style="height: 10px">
                                    </div>
                                    <table>
                                        <tr>
                                            <td style="text-align: left; width: 250px">
                                                <asp:Label ID="Label14" runat="server" Text="Name of Last Exam" Font-Size="Small"></asp:Label>
                                            </td>
                                            <td style="text-align: left; padding-right: 20px; width: 250px">
                                                <asp:Label ID="Label15" runat="server" Text="B.C.A." Font-Size="Small" AutoPostBack="false"></asp:Label>
                                            </td>
                                            <td style="text-align: left; width: 250px">
                                                <asp:Label ID="Label16" runat="server" Text="Session/Year" Font-Size="Small"></asp:Label>
                                            </td>
                                            <td style="text-align: left; padding-right: 20px; width: 250px">
                                                <asp:Label ID="Label17" runat="server" Text="VI" Font-Size="Small" AutoPostBack="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="height: 10px"></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; width: 250px">
                                                <asp:Label ID="Label18" runat="server" Text="Roll No" Font-Size="Small"></asp:Label>
                                            </td>
                                            <td style="text-align: left; padding-right: 20px; width: 250px">
                                                <asp:Label ID="Label19" runat="server" Text="TCA2201273" Font-Size="Small" AutoPostBack="false"></asp:Label>
                                            </td>
                                            <td style="text-align: left; width: 250px">
                                                <asp:Label ID="Label20" runat="server" Text="Result" Font-Size="Small"></asp:Label>
                                            </td>
                                            <td style="text-align: left; padding-right: 20px; width: 250px">
                                                <asp:Label ID="Label21" runat="server" Text="PASS" Font-Size="Small" AutoPostBack="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="height: 10px"></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; width: 250px">
                                                <asp:Label ID="Label22" runat="server" Text="Name of College/Department" Font-Size="Small"></asp:Label>
                                            </td>
                                            <td style="text-align: left; padding-right: 20px; width: 1000px">
                                                <asp:Label ID="Label23" runat="server" Text="ERP" Font-Size="Small" AutoPostBack="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="height: 10px"></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; width: 420px">
                                                <asp:Label ID="Label62" runat="server" Text="Name of College/Department, if still on rolls :" Font-Size="Small"></asp:Label>
                                            </td>
                                            <td style="text-align: left; padding-right: 20px; width: 1000px">
                                                <asp:Label ID="Label63" runat="server"  Font-Size="Small" AutoPostBack="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="height: 10px"></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; width: 250px">
                                                <asp:Label ID="Label24" runat="server" Text="Session/Year" Font-Size="Small"></asp:Label>
                                            </td>
                                            <td style="text-align: left; padding-right: 20px; width: 250px">
                                                <asp:Label ID="Label25" runat="server" Text="24-25" Font-Size="Small" AutoPostBack="false"></asp:Label>
                                            </td>
                                            <td style="text-align: left; width: 250px">
                                                <asp:Label ID="Label64" runat="server" Text="Roll No" Font-Size="Small"></asp:Label>
                                            </td>
                                            <td style="text-align: left; padding-right: 20px; width: 250px">
                                                <asp:Label ID="Label65" runat="server" Text="TCA2201273" Font-Size="Small" AutoPostBack="false"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>


                                <div class="col-md-12" style="margin-top: 10px;">
                                    <div>
                                        <asp:Label ID="Label28" runat="server" Font-Bold="true" Text="Whether any unfair means case against the applicant is under consideration with this University? If so, give the following particulars :-" Font-Size="Small"></asp:Label>
                                    </div>
                                    <div class="col-md-12" style="height: 10px">
                                    </div>

                                    <table>
                                        <tr style="margin-top: 15px">
                                            <td style="text-align: left; width: 200px">
                                                <asp:Label ID="Label31" runat="server" Text="Name of Examination" Font-Size="Small"></asp:Label>
                                            </td>
                                            <td style="text-align: left; padding-right: 20px; width: 200px">
                                                <asp:Label ID="Label32" runat="server" Text="B.C.A." Font-Size="Small" AutoPostBack="false"></asp:Label>
                                            </td>
                                            <td style="text-align: left; width: 100px">
                                                <asp:Label ID="Label33" runat="server" Text="Session/Year" Font-Size="Small"></asp:Label>
                                            </td>
                                            <td style="text-align: left; padding-right: 20px; width: 200px">
                                                <asp:Label ID="Label34" runat="server" Text="VI" Font-Size="Small" AutoPostBack="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="height: 10px"></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; width: 100px">
                                                <asp:Label ID="Label35" runat="server" Text="Roll No" Font-Size="Small"></asp:Label>
                                            </td>
                                            <td style="text-align: left; padding-right: 20px; width: 200px">
                                                <asp:Label ID="Label36" runat="server" Text="TCA2201273" Font-Size="Small" AutoPostBack="false"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-md-12" style="height: 10px">
                                </div>

                                <div class="col-md-12" style="margin-top: 10px;">
                                    <div>
                                        <asp:Label ID="Label29" runat="server" Font-Bold="true" Text="Whether disqualified by this University at any point of time? if so, give the following particulars :-" Font-Size="Small"></asp:Label>
                                    </div>
                                    <div class="col-md-12" style="height: 10px">
                                    </div>


                                    <table>
                                        <tr>
                                            <td style="text-align: left; width: 200px">
                                                <asp:Label ID="Label38" runat="server" Text="College/Department" Font-Size="Small"></asp:Label>
                                            </td>
                                            <td style="text-align: left; padding-right: 20px; width: 200px">
                                                <asp:Label ID="Label39" runat="server" Text="B.C.A." Font-Size="Small" AutoPostBack="false"></asp:Label>
                                            </td>
                                            <td style="text-align: left; width: 300px">
                                                <asp:Label ID="Label40" runat="server" Text="Examination in which disqualified" Font-Size="Small"></asp:Label>
                                            </td>
                                            <td style="text-align: left; padding-right: 20px; width: 200px">
                                                <asp:Label ID="Label41" runat="server" Text="VI" Font-Size="Small" AutoPostBack="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="height: 10px"></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; width: 200px">
                                                <asp:Label ID="Label27" runat="server" Text="Session/Year" Font-Size="Small"></asp:Label>
                                            </td>
                                            <td style="text-align: left; padding-right: 20px; width: 200px">
                                                <asp:Label ID="Label42" runat="server" Text="20-23" Font-Size="Small"></asp:Label>
                                            </td>
                                            <td style="text-align: left; width: 200px">
                                                <asp:Label ID="Label26" runat="server" Text="Roll No" Font-Size="Small"></asp:Label>
                                            </td>
                                            <td style="text-align: left; padding-right: 20px; width: 200px">
                                                <asp:Label ID="Label43" runat="server" Text="TCA2201273" Font-Size="Small" AutoPostBack="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="height: 10px"></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; font: bold 100" colspan="4">Name of University/Institution/College where the applicant has joined/intends to join:</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left" colspan="4">
                                                <asp:Label ID="Label30" runat="server" Text="University/Institution/College xdfgdfgdf dfgdsfgh sdgsd" Font-Size="Small"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-md-12" style="height: 10px">
                                </div>
                                <div class="col-md-12" style="margin-top: 10px;">
                                    <table>
                                        <tr>
                                            <td style="text-align: left; width: 200px">


                                                <asp:Label ID="Label45" runat="server" Text="Programme/Course" Font-Size="Small"></asp:Label>
                                            </td>
                                            <td style="text-align: left; padding-right: 20px; width: 200px">
                                                <asp:Label ID="Label46" runat="server" Text="B.C.A." Font-Size="Small" AutoPostBack="false"></asp:Label>
                                            </td>
                                            <td style="text-align: left; width: 200px">
                                                <asp:Label ID="Label47" runat="server" Text="Year/Session" Font-Size="Small"></asp:Label>
                                            </td>
                                            <td style="text-align: left; padding-right: 20px; width: 200px">
                                                <asp:Label ID="Label48" runat="server" Text="VI" Font-Size="Small" AutoPostBack="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="height: 10px"></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; width: 200px">
                                                <asp:Label ID="Label49" runat="server" Text="Class" Font-Size="Small"></asp:Label>
                                            </td>
                                            <td style="text-align: left; padding-right: 20px; width: 200px">
                                                <asp:Label ID="Label50" runat="server" Text="BCA" Font-Size="Small" AutoPostBack="false"></asp:Label>
                                            </td>

                                            <td style="text-align: left; width: 200px">
                                                <asp:Label ID="Label52" runat="server" Text="Roll No." Font-Size="Small"></asp:Label>
                                            </td>

                                            <td style="text-align: left; padding-right: 20px; width: 200px">
                                                <asp:Label ID="Label53" runat="server" Text="BCA" Font-Size="Small" AutoPostBack="false"></asp:Label>
                                            </td>

                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="col-md-12" style="height: 10px">
                            </div>
                            <div class="form-group" style="margin-top: 20px;">
                                <div class="col-md-12" style="text-align: left; width: 550px">
                                    <asp:Label ID="Label54" runat="server" Font-Bold="true" Text="Postal Address to which the migration certificate should be sent (In capitals):" Font-Size="Small"></asp:Label>

                                    <asp:Label ID="Label55" runat="server" Text="Vill_narayanpur Post Kothi" Font-Size="Small"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-12" style="height: 10px">
                            </div>
                            <div class="form-group" style="margin-top: 20px;">
                                <div class="col-md-12" style="text-align: left; width: 900px">
                                    <asp:Label ID="Label56" runat="server" Font-Bold="true" Text="I solemnly declare that: -" Font-Size="Small"></asp:Label>
                                    <table style="margin-left: 20px">
                                        <tr>
                                            <td style="width: 90%">(i) The particulars filled in by me are correct and nothing has been concealed;
                                               <br />
                                                (ii) I did not appear in any other examination thereafter from this University;
                                                <br />
                                                (iii) In case of compartment/re-appear, I will not appear in the compartment/re-appear subjects of the last examination from Teerthanker Mahaveer University; and
                                                <br />
                                                (iv) I shall be responsible for the consequences, if the above statements are found incorrect.
                                              


                                            </td>

                                        </tr>
                                    </table>

                                </div>
                            </div>
                            <div class="col-md-12" style="height: 20px">
                            </div>
                            <div class="form-group" style="margin-top: 20px;">
                                <div class="col-md-12" style="text-align: left; width: 900px">
                                    <asp:Label ID="Label57" runat="server" Font-Bold="true" Text="Dated:" Font-Size="Small"></asp:Label>
                                    <asp:Label ID="Label58" runat="server" Text=" …………………………………" Font-Size="Small"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-12" style="height: 10px">
                            </div>
                            <div class="form-group" style="margin-top: 20px;">
                                <div class="col-md-6" style="text-align: left;">
                                    <asp:Label ID="Label59" runat="server" Font-Bold="true" Text="Place:" Font-Size="Small"></asp:Label>
                                    <asp:Label ID="Label60" runat="server" Text=" …………………………………" Font-Size="Small"></asp:Label>
                                </div>
                                <div class="col-md-6" style="text-align: right;">
                                    <asp:Label ID="Label61" runat="server" Font-Bold="true" Text="Signature of the Applicant" Font-Size="Small"></asp:Label>

                                </div>
                            </div>

                           <%-- <div style="text-align: center;">

                                <p style="margin-bottom: -15px">
                                    <strong>
                                        <asp:Label ID="Label37" runat="server" Font-Bold="true" Text="B. FEE PARTICULARS"></asp:Label>
                                    </strong>
                                </p>
                            </div>--%>


                           <%-- <div class="col-md-12" style="margin-top: 20px;">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="text-align: left; width: 50%">

                                            <asp:Label ID="Label44" runat="server" Text="Fee Remitted Rss……………………………" Font-Size="Small"></asp:Label>
                                            <br />
                                            <br />
                                            <asp:Label ID="Label66" runat="server" Text="Bank Draft/University Receipt No……………………………" Font-Size="Small"></asp:Label>
                                            <br />
                                            <br />
                                            <asp:Label ID="Label67" runat="server" Text="Dates………………………………" Font-Size="Small"></asp:Label>

                                        </td>
                                        <td style="text-align: center; width: 50%; border: 1px solid; margin-left: 5px; margin-right: 5px">
                                            <asp:Label ID="Label51" runat="server" Font-Bold="true" Text="For Use of Accounts Dept. Only" Font-Size="Small"></asp:Label>
                                            <br />
                                            <br />
                                            <asp:Label ID="Label68" runat="server" Text="Received Rs..............vide Univ. Receipt No." Font-Size="Small"></asp:Label>
                                            <br />
                                            <br />
                                            <asp:Label ID="Label69" runat="server" Text="................................. Dated........................" Font-Size="Small"></asp:Label>
                                            <br />
                                            <br />
                                            <asp:Label ID="Label70" runat="server" Text="ACCOUNTS OFFICE" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                            <br />
                                        </td>


                                    </tr>

                                </table>
                            </div>--%>

                        </div>
                    </div>
                </fieldset>
            </div>
        </div>



        <%--<asp:Button ID="Button1" runat="server" Text="Print" OnClientClick = "return PrintPanel();" />--%>
    </div>

    <div id="CertificateModel" class="modal fade confirm-modal" role="dialog">

        <div class="modal-dialog modalPopup" style="width: 1000px; height: 900px; overflow: scroll">
            <div style="text-align: right; padding-bottom: -40px">

                <asp:Button ID="Button3" OnClientClick="PrintDiv1()" runat="server" Width="10%" Style="margin-top: 5px;" Text="Print" Font-Bold="true" BorderColor="WhiteSmoke" />

                <asp:Button ID="Button4" runat="server" Text="X" OnClientClick="HideCertificateModel();" Font-Size="Larger" />
            </div>
            <div class="clearfix" style="margin-bottom: 10px; margin-top: 10px; margin-left: 20px">


                <div class="header">
                    <h2>

                        <asp:Label ID="Label71" runat="server" Text="Migration Certificate"></asp:Label></h2>

                </div>


                <div id="printareacertificate" style="width: 99%;">





                    <div style="margin-bottom: 10px; margin-top: 5px;" class="nav-justified">


                        <table style="width: 98%;">

                            <tr>
                                <td colspan="3" style="text-align: center">
                                    <h1>
                                        <asp:Label ID="Label73" Text="TEERTHANKER MAHAVEER UNIVERSITY" Font-Size="XX-Large" Font-Bold="true" Font-Names="Times New Roman" runat="server"></asp:Label></h1>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: center; height: 10px"></td>
                            </tr>
                            <tr>

                                <td style="width: 25%; text-align: left;">
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/images/tmulogo.png" Width="60%" Height="100px" />
                                </td>

                                <td style="width: 55%; text-align: center; vertical-align: top">
                                    <strong>
                                        <asp:Label ID="Label74" runat="server" Font-Size="X-Large" Font-Bold="true" Font-Names="Times New Roman" Text="DELHI ROAD, MORADABAD"></asp:Label></strong>

                                </td>

                                <td style="width: 20%; text-align: center">
                                   Sl. No.: <asp:Label ID="slNo" runat="server"></asp:Label></td>
                            </tr>
                        </table>

                        <div style="font-size: large; text-align: justify">




                            <p style="text-align: center; font-size: xx-large; text-decoration: underline; font-family: 'Times New Roman'; font-weight: bold;">MIGRATION CERTIFICATE</p>
                            <br />

                            <div style="text-align: justify; font-size: x-large">
                                <p class="custom-paragraph" style="line-height:40px" >
                                    Certified that Mr./Ms. 
                            <asp:Label ID="lblStudent" Font-Bold="true"  runat="server"></asp:Label>

                                    son/daugter of 
                                <asp:Label ID="lblGaurdiation" Font-Bold="true" runat="server"></asp:Label>
                                    bearing Enrolment No.
                            <asp:Label ID="lblenrollment" runat="server" Text="" Font-Bold="true" Font-Underline="false"></asp:Label>
                                    was last admitted in
                             <asp:Label ID="lblProgramCertificate" Text="his" Font-Bold="true" runat="server"></asp:Label>
                                    program of this University in the academic session    
                         <asp:Label ID="lblacedmicyear" runat="server" Text="" Font-Bold="true" Font-Underline="false"></asp:Label>
                                    under Roll No
                                 <asp:Label ID="lblRollNo" runat="server" Text="..............." Font-Bold="true" Font-Underline="false"></asp:Label>
                                    and has been declared Pass / Fail or has left the studies in between or his/her admission has been cancelled.
                                </p>
                                <p class="custom-paragraph" style="font-size: x-large">
                                    The University has no objection to his / her migration to any other University. 
                            
                                </p>


                            </div>




                        </div>

                        <br />
                        <table>
                            <tr>
                                <td style="text-align: left; width: 78%">
                                    <p class="custom-paragraph" style="text-align: left; font-size: large; font-weight: bold;">
                                        Prepared by:
                    <br />
                                        <br />
                                        Date :.................
                                    </p>
                                </td>
                                &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp&nbsp &nbsp &nbsp &nbsp &nbsp &nbsp&nbsp &nbsp &nbsp &nbsp &nbsp &nbsp
            <td style="text-align: right">
                <p class="custom-paragraph" style="text-align: right; font-size: large; font-weight: bold;">Deputy Registrar</p>
            </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <hr style="font-size: large; color: black" />
                                </td>

                            </tr>
                        </table>

                        <div style="text-align: justify; font-size: small">
                            <p class="custom-paragraph">
                                Note: 
                                <br />
                                <br />
                                1. After receiving the migration certificate from this university, if the student desires for readmission in this university, he/she will have to submit either this ORIGINAL Migration Certificate or the Migration Certificate
                                of the University from  where he / she has passed last examination.
                                <br />
                                <br />
                                2. The duplicate Migration Certificate would be issued by the university after submission of a declaration duly signed by the student along with original receipt of Rs. 1000.00 [Rupees One Thousand only] deposited in the Accounts Branch of the University.
                            </p>


                        </div>







                    </div>



                </div>

            </div>
        </div>



        <%--<asp:Button ID="Button1" runat="server" Text="Print" OnClientClick = "return PrintPanel();" />--%>
    </div>
</asp:Content>

