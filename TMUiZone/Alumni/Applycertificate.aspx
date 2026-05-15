<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/IndexMaster.master" AutoEventWireup="true" CodeFile="Applycertificate.aspx.cs" Inherits="Alumni_Applycertificate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function OpenNewWindow() {

            window.open('../Alumni/StudentScrutinyreport.aspx'); return false;
        }
    </script>
    <style type="text/css">
        .HiddenCol {
            display: none;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server" Text="CERTIFICATE APPLICATION FORM" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
   
    <fieldset class="boxBodyInner">
         <asp:Label  runat="server" Font-Bold="true" Font-Size="Large" ForeColor="Red" Text="The Migration Certificate will be available 3 days after the payment is received"></asp:Label>

        <asp:Panel ID="PnlDeatinedAttendence" runat="server">
            <fieldset class="boxBodyInner">
                <div class="loader" id="Loader1" style="display: none"></div>
            </fieldset>
            <br />

            <div id="divGeneralBodyenrollmentform">

                <div class="form-horizontal">
                    <div class="box-body">
                        <div class="row">

                            <div class="form-group">
                                <div class="col-md-2">
                                    <label style="width: 200px; font: bold; color: black; font-size: large">Enrollement No</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtenrollmentno" runat="server" ReadOnly="true" BackColor="White" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label style="width: 200px; font: bold; color: black; font-size: large">ST.No</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtstno" runat="server" ReadOnly="true" BackColor="White" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-2">
                                    <label style="width: 200px; font: bold; color: black; font-size: large">Student Name</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtstudentName" runat="server" ReadOnly="true" BackColor="White" CssClass="form-control"></asp:TextBox>
                                </div>


                                <div class="col-md-2">
                                    <label style="width: 200px; font: bold; color: black; font-size: large">Father's Name</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtfathername" runat="server" ReadOnly="true" BackColor="White" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-2">
                                    <label style="width: 200px; font: bold; color: black; font-size: large">College/Dept</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtcollegedept" runat="server" ReadOnly="true" BackColor="White" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label style="width: 200px; font: bold; color: black; font-size: large">Programme </label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtPrograme" runat="server" ReadOnly="true" BackColor="White" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-2">
                                    <label style="width: 200px; font: bold; color: black; font-size: large">Certificate Type</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="drpCertificatetype" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpCertificatetype_SelectedIndexChanged">
                                        <asp:ListItem Text="Original" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Duplicate" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label style="width: 200px; font: bold; color: black; font-size: large">Certificate List</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlCertificate" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>

                            </div>

                            <div class="form-group">
                                <div class="col-md-2">
                                    <label style="width: 200px; font: bold; color: black; font-size: large">Collected by</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="drpCollectType" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpCollectType_SelectedIndexChanged">
                                        <asp:ListItem Text="By Courier" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="By Hand" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label style="width: 200px; font: bold; color: black; font-size: large">Country</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="drpCountry" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpCountry_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>

                            </div>
                            <div class="form-group">
                                <div class="col-md-2">
                                    <label style="width: 200px; font: bold; color: black; font-size: large">State</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="drpState" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpState_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label style="width: 200px; font: bold; color: black; font-size: large">City</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="drpCity" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpCity_SelectedIndexChanged" runat="server">
                                    </asp:DropDownList>
                                </div>

                            </div>
                            <div class="form-group">
                                <div class="col-md-2">
                                    <label style="width: 200px; font: bold; color: black; font-size: large">Post Code</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtPost" CssClass="form-control" runat="server">
                                    </asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label style="width: 200px; font: bold; color: black; font-size: large">Address</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtaddress" CssClass="form-control" TextMode="MultiLine" runat="server">
                                    </asp:TextBox>
                                </div>

                            </div>
                            <div class="form-group" id="divCer" runat="server" visible="false">

                                <div class="col-md-2">
                                    <label style="width: 200px; font: bold; color: black; font-size: large">Upload Affidavit</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:FileUpload ID="flUpload" runat="server" Style="width: 200px; font: bold; color: black; font-size: large"></asp:FileUpload>
                                </div>
                                <div class="col-md-3">
                                    <asp:LinkButton ID="lnkDownloadSample" Text="Download Affidavit Sample" OnClick="lnkDownloadSample_Click" Font-Italic="true" ForeColor="Green" runat="server" class="col-form-label" Style="font-size: large; width: 300px"> </asp:LinkButton>


                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-3" style="text-align: right">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="form-control" BackColor="#ed7600" OnClientClick="if(!confirm('Do you want to submit'))return false;" OnClick="btnSubmit_Click" Text="Submit" class="button" />
                                </div>

                            </div>

                        </div>
                    </div>
                </div>

            </div>



            <fieldset class="boxBodyInner">
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
                                <asp:TemplateField HeaderText="View Details" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblView" runat="server" CommandArgument='<%# Eval("App_No") %>' Text="View" OnClick="lblView_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Make Payment" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblmake_Payment" runat="server" CommandArgument='<%# Eval("App_No") %>' Visible='<%# Eval("Payment_Status").ToString() == "Open" ? true : false %>'  Text="Make Payment" OnClick="lblmake_Payment_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>
                        </asp:GridView>

                    </asp:Panel>
                </div>
                <table>
                    <tr>
                        <td>

                            <asp:Panel ID="PnlFee" runat="server">
                                <asp:GridView ID="GridView1" CssClass="table table-striped table-bordered" AutoGenerateColumns="false" runat="server" BackColor="White" ShowFooter="true" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="1000px"
                                    GridLines="Horizontal" EmptyDataText="There are no data records to display."
                                    DataKeyNames="Amount,feecode">
                                    <Columns>
                                        <asp:TemplateField HeaderText="AcademicYear" ControlStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:Label ID="txtacademic" runat="server" Text='<%# Bind("[Academic Year]") %>' MaxLength="100"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Course Code" ControlStyle-Width="200px">
                                            <ItemTemplate>
                                                <asp:Label ID="txtcourse" runat="server" Text='<%# Bind("[Course Code]") %>' MaxLength="100"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Semester" ControlStyle-Width="150px">
                                            <ItemTemplate>
                                                <asp:Label ID="txtsem" runat="server" Text='<%# Bind("Semester") %>' MaxLength="100"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount" ControlStyle-Width="200px">
                                            <ItemTemplate>
                                                <asp:Label ID="txtamt" runat="server" Text='<%# Bind("Amount") %>' MaxLength="100"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description" ControlStyle-Width="350px">
                                            <ItemTemplate>
                                                <asp:Label ID="txtdesc" runat="server" Text='<%# Bind("Description") %>' MaxLength="100"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="" ControlStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkapply" runat="server" Text="Apply" MaxLength="100" CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>

                                </asp:GridView>

                            </asp:Panel>


                        </td>
                    </tr>

                </table>
            </fieldset>

        </asp:Panel>

    </fieldset>

    <div id="confirmModal1" class="modal fade confirm-modal" role="dialog">

        <div class="modal-dialog modalPopup" style="width: 1200px; height: 400px">
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
                                    <asp:Label ID="lblAppNo" runat="server">                                 

                                    </asp:Label>

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
                                <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp College</label>
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

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Examination Remark </label>

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

                        <div class="col-sm-2 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font: bold; visibility: hidden; font-size: small">&nbsp&nbsp&nbsp  Registration fee details  </label>

                                <div class="col-sm-8">

                                    <asp:Button ID="btnApprove" runat="server" Text="Approved" Font-Bold="true" Visible="false" AutoPostBack="true" OnClick="btnApprove_Click" Height="32px" Font-Size="small" class="btn btn-info" Width="120px"></asp:Button>
                                    <asp:Button ID="btnRejectPop" runat="server" Text="Reject" Font-Bold="true" Visible="false" ValidationGroup="XX" AutoPostBack="true" OnClick="btnRejectPop_Click" Height="32px" Font-Size="small" class="btn btn-info" Width="120px"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>

                </asp:UpdatePanel>
            </div>
        </div>
    </div>


</asp:Content>

