<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master"
    AutoEventWireup="true" CodeFile="BodyDonors.aspx.cs" Inherits="Faculty_BodyDonors" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <script>
       function ViewUserDetails() {
           $("#DonorModal").modal("show");
       }

       $(document).ready(function () {

           $('#<%= txtDOB.ClientID %>').on('change', function () {

            let dob = new Date($(this).val());
            let today = new Date();

            let age = today.getFullYear() - dob.getFullYear();

            let monthDiff = today.getMonth() - dob.getMonth();
            if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < dob.getDate())) {
                age--;
            }

            $('#<%= txtAge.ClientID %>').val(age);
        });

       });
   </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <table cellpadding="0px" cellspacing="0px" style="width:100%">
            <tr> <td style="height:13px"> </td></tr>
         <tr> <td>  


      &nbsp;&nbsp;&nbsp; &nbsp;  <asp:Label ID="Label3" runat="server" 
            Text="Donors Registration" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
            </td></tr> 

            <tr> <td style="height:13px"> </td></tr>


        <tr> <td class="leftm">  </td></tr>

        <tr> <td style="height:13px"> </td></tr>

        <tr> <td align="center">
            <table cellpadding="0px" cellspacing="0px"> 
                <tr>
                    <td>  Donor No.</td> 
                        <td style="width:10px"> </td>
                    <td> 
                        <asp:TextBox ID="txtDonorNo" runat="server" Height="29px" ValidationGroup="g11"></asp:TextBox>
                    </td>
                    <td style="width:10px"> </td>
                    <td> 
                        <asp:Button ID="btnGet" runat="server" Text="Search" OnClick="btnSearch_Click" ValidationGroup="g12"/>

                    </td>
                      <td style="width:620px"> </td>
                      <td> 
                          <asp:Button ID="btnCreate" runat="server" Text="Create" OnClientClick="ViewUserDetails(); return false;" />
                      </td>
                    <td style="width:10px"> </td>
                    <td>  <asp:Button ID="btnexporttoexcel" runat="server" Text="Export To Excel" OnClick="btnexporttoexcel_Click" ValidationGroup="g13" /></td>
                </tr> 

            </table> 

             </td></tr>

                <tr> <td style="height:13px"> </td></tr>

                <tr> <td>

                     <!-- GRID -->
          <asp:GridView ID="grdDonors" runat="server"
    AutoGenerateColumns="False" Width="100%"
    CssClass="table table-striped table-bordered"
    EmptyDataText="No Data Found"
    OnRowCommand="grdDonors_RowCommand"  AllowPaging="true"
 PageSize="20"
 OnPageIndexChanging="grdDonors_PageIndexChanging">

    <Columns>
        <asp:BoundField DataField="DonorId" HeaderText="Donor ID" />
        <asp:BoundField DataField="Name" HeaderText="Name" />
        <asp:BoundField DataField="FatherOrHusbandName" HeaderText="Father/Husband" />
        <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
        <asp:BoundField DataField="Gender" HeaderText="Gender" />
        <asp:BoundField DataField="CreatedAt" HeaderText="Date"
            DataFormatString="{0:dd-MM-yyyy}" />

        <asp:TemplateField HeaderText="Action">
    <ItemTemplate>
        <asp:LinkButton ID="btnView" runat="server"
            Text="View"
            CommandName="ViewDonor"
            CommandArgument='<%# Eval("Id") %>'
            CssClass="btn btn-sm btn-primary" />
    </ItemTemplate>
</asp:TemplateField>

    </Columns>
</asp:GridView>

                     </td></tr>

                <tr> <td style="height:90px"> </td></tr>
    </table>

<!--Add MODAL -->
<div class="modal fade" id="DonorModal">
    <div class="modal-dialog modal-xl">
        <div class="modal-content">

            <!-- HEADER -->
            <div class="modal-header text-white" style="background-color:#e6812b;color:white">
                <h5 class="mb-0">Donor Registration</h5>
                 <button type="button" class="close text-white" data-dismiss="modal" style="margin-top: -22px;">
     <span>&times;</span>
 </button>
            </div>

            <!-- BODY -->
            <div class="modal-body">

                <asp:UpdatePanel runat="server">
                    <ContentTemplate>

                        <div class="container-fluid">
  
                            <!-- DONOR DETAILS -->
                            <div class="card mb-3">
                            <div class="card-header bg-info text-white" style="margin-top: 12px;margin-bottom: 12px;">
                                 Donor Details
                             </div>
                             
                                <div class="card-body">

                                    <div class="row">
                                        <div class="col-md-4">
                                            <label>Name <span>*</span></label>
                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
                                             <asp:RequiredFieldValidator ID="rfvName" runat="server"
                                                ControlToValidate="txtName" 
                                                ErrorMessage="Name is required"  ValidationGroup="SaveGroup"
                                                CssClass="text-danger" />
                                        </div>

                                        <div class="col-md-4">
                                            <label>Father/Husband</label>
                                            <asp:TextBox ID="txtFather" runat="server" CssClass="form-control" />
                                        </div>

                                        <div class="col-md-4">
                                            <label>DOB</label>
                                            <asp:TextBox ID="txtDOB" runat="server" TextMode="Date" CssClass="form-control" />
                                          
                                        </div>
                                    </div>

                                    <div class="row mt-2">
                                        <div class="col-md-4">
                                            <label>Age</label>
                                            <asp:TextBox ID="txtAge" runat="server" CssClass="form-control" />
                                            <asp:RangeValidator ID="rvAge" runat="server" 
                                            ControlToValidate="txtAge"
                                            MinimumValue="1"  ValidationGroup="SaveGroup"
                                            MaximumValue="120"
                                            Type="Integer"
                                            ErrorMessage="Enter valid age"
                                            CssClass="text-danger" />
                                        </div>

                                        <div class="col-md-4">
                                            <label>Gender <span>*</span></label>
                                            <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="--Select--" Value="" />
                                            <asp:ListItem Text="Male" />
                                            <asp:ListItem Text="Female" />
                                            <asp:ListItem Text="Other" />
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvGender" runat="server"
                                            ControlToValidate="ddlGender"
                                            InitialValue=""   ValidationGroup="SaveGroup"
                                            ErrorMessage="Select gender"
                                            CssClass="text-danger" />
                                        </div>

                                        <div class="col-md-4">
                                            <label>Religion</label>
                                            <asp:TextBox ID="txtReligion" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>

                                    <div class="row mt-2">
                                        <div class="col-md-4">
                                            <label>Mobile <span>*</span></label>
                                            <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="rfvMobile" runat="server"
                                            ControlToValidate="txtMobile"  ValidationGroup="SaveGroup"
                                            ErrorMessage="Mobile is required"
                                            CssClass="text-danger" />

                                        <asp:RegularExpressionValidator ID="revMobile" runat="server"
                                            ControlToValidate="txtMobile" 
                                            ValidationExpression="^[6-9]\d{9}$"  ValidationGroup="SaveGroup"
                                            ErrorMessage="Enter valid 10-digit mobile"
                                            CssClass="text-danger" />
                                        </div>

                                        <div class="col-md-4">
                                            <label>Email</label>
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                                            <asp:RegularExpressionValidator ID="revEmail" runat="server"
                                            ControlToValidate="txtEmail" 
                                            ValidationExpression="^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$"
                                            ErrorMessage="Invalid email"  ValidationGroup="SaveGroup"
                                            CssClass="text-danger" />
                                        </div>

                                        <div class="col-md-4">
                                            <label>Aadhaar</label>
                                            <asp:TextBox ID="txtAadhaar" runat="server" CssClass="form-control" />
                                            <asp:RegularExpressionValidator ID="revAadhaar" runat="server"
                                            ControlToValidate="txtAadhaar" 
                                            ValidationExpression="^\d{12}$"  ValidationGroup="SaveGroup"
                                            ErrorMessage="Aadhaar must be 12 digits"
                                            CssClass="text-danger" />
                                        </div>
                                    </div>

                                    <div class="row mt-2">
                                        <div class="col-md-12">
                                            <label>Address <span>*</span></label>
                                            <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="rfvAddress" runat="server"
                                            ControlToValidate="txtAddress"   ValidationGroup="SaveGroup"
                                            ErrorMessage="Address required"
                                            CssClass="text-danger" />
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <!-- WITNESS 1 -->
                            <div class="card mb-3">
                                <div class="card-header bg-info text-white" style="margin-top: 12px;margin-bottom: 12px;">
                                    Witness 1 Details
                                </div>

                                <div class="card-body">

                                    <div class="row">
                                        <div class="col-md-4">
                                            <label>Name</label>
                                            <asp:TextBox ID="txtW1Name" runat="server" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="rfvW1Name" runat="server"
                                            ControlToValidate="txtW1Name"   ValidationGroup="SaveGroup"
                                            ErrorMessage="Witness name required"
                                            CssClass="text-danger" />
                                        </div>

                                        <div class="col-md-4">
                                            <label>Relationship</label>
                                            <asp:TextBox ID="txtW1Relation" runat="server" CssClass="form-control" />
                                        </div>

                                        <div class="col-md-4">
                                            <label>Mobile/Email</label>
                                            <asp:TextBox ID="txtW1Mobile" runat="server" CssClass="form-control" />
                                            <asp:RegularExpressionValidator ID="revW1Mobile" runat="server"
                                            ControlToValidate="txtW1Mobile"  ValidationGroup="SaveGroup"
                                            ValidationExpression="^[6-9]\d{9}$"
                                            ErrorMessage="Valid mobile required"
                                            CssClass="text-danger" />
                                        </div>
                                    </div>

                                    <div class="row mt-2">
                                        <div class="col-md-6">
                                            <label>Address</label>
                                            <asp:TextBox ID="txtW1Address" runat="server" CssClass="form-control" />
                                            
                                        </div>

                                        <div class="col-md-6">
                                            <label>Aadhaar</label>
                                            <asp:TextBox ID="txtW1Aadhaar" runat="server" CssClass="form-control" />
                                            <asp:RegularExpressionValidator ID="revW1Aadhaar" runat="server"
                                            ControlToValidate="txtW1Aadhaar"   ValidationGroup="SaveGroup"
                                            ValidationExpression="^\d{12}$"
                                            ErrorMessage="12 digit Aadhaar"
                                            CssClass="text-danger" />
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <!-- NEXT OF KIN -->
                            <div class="card mb-3">
                                <div class="card-header bg-warning text-dark" style="margin-top: 12px;margin-bottom: 12px;">
                                     Witness 2 Details / Next Of Kin
                                </div>

                                <div class="card-body">

                                    <div class="row">
                                        <div class="col-md-4">
                                            <label>Name <span>*</span></label>
                                            <asp:TextBox ID="txtKinName" runat="server" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="rfvKinName" runat="server"
                                            ControlToValidate="txtKinName" 
                                            ErrorMessage="Kin name required"  ValidationGroup="SaveGroup"
                                            CssClass="text-danger" />
                                        </div>

                                        <div class="col-md-4">
                                            <label>Relationship</label>
                                            <asp:TextBox ID="txtKinRelation" runat="server" CssClass="form-control" />
                                        </div>

                                        <div class="col-md-4">
                                            <label>Mobile/Email</label>
                                            <asp:TextBox ID="txtKinMobile" runat="server" CssClass="form-control" />
                                            <asp:RegularExpressionValidator ID="revKinMobile" runat="server"
                                            ControlToValidate="txtKinMobile"  ValidationGroup="SaveGroup"
                                            ValidationExpression="^[6-9]\d{9}$"
                                            ErrorMessage="Valid mobile required"
                                            CssClass="text-danger" />
                                        </div>
                                    </div>

                                    <div class="row mt-2">
                                        <div class="col-md-6">
                                            <label>Address <span>*</span></label>
                                            <asp:TextBox ID="txtKinAddress" runat="server" CssClass="form-control" />
                                            <asp:RegularExpressionValidator ID="revKinAadhaar" runat="server"
                                            ControlToValidate="txtKinAadhaar"   ValidationGroup="SaveGroup"
                                            ValidationExpression="^\d{12}$"
                                            ErrorMessage="12 digit Aadhaar"
                                            CssClass="text-danger" />
                                        </div>

                                        <div class="col-md-6">
                                            <label>Aadhaar</label>
                                            <asp:TextBox ID="txtKinAadhaar" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>

                                </div>
                            </div>

                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>

            <!-- FOOTER -->
            <div class="modal-footer">
                <asp:Button ID="btnSave" runat="server"
                    Text="Save Donor"  ValidationGroup="SaveGroup"
                    CssClass="btn btn-success px-4"
                    OnClick="btnSave_Click" />
            </div>

        </div>
    </div>
</div>


<div class="modal fade" id="donorModal" tabindex="-1">
    <div class="modal-dialog modal-xl">
        <div class="modal-content">

            <div class="modal-header text-white" style="background-color:#e6812b;color:white">
                <h5 class="modal-title">Body Donor Registration</h5>
                <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close" style="margin-top: -20px;color:white;">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">

    <h3 class="mb-3"><b>DONOR DETAILS</b></h3>

    <table class="table table-bordered">
        <tr>
            <th>Name</th>
            <td><asp:Label ID="lblName" runat="server" /></td>
            <th>Father/Husband</th>
            <td><asp:Label ID="lblFather" runat="server" /></td>
        </tr>

        <tr>
            <th>DOB</th>
            <td><asp:Label ID="lblDOB" runat="server" /></td>
            <th>Age / Gender</th>
            <td>
                <asp:Label ID="lblAge" runat="server" /> /
                <asp:Label ID="lblGender" runat="server" />
            </td>
        </tr>

        <tr>
            <th>Mobile</th>
            <td><asp:Label ID="lblMobile" runat="server" /></td>
            <th>Email</th>
            <td><asp:Label ID="lblEmail" runat="server" /></td>
        </tr>

        <tr>
            <th>Religion</th>
            <td><asp:Label ID="lblReligion" runat="server" /></td>
            <th>Aadhaar</th>
            <td><asp:Label ID="lblAadhaar" runat="server" /></td>
        </tr>

        <tr>
            <th>Address</th>
            <td colspan="3">
                <asp:Label ID="lblAddress" runat="server" />
            </td>
        </tr>
    </table>

    <h3 class="mt-4 mb-3"><b>Witness Details</b></h3>

    <table class="table table-bordered">
        <tr class="table-secondary">
            <th>Field</th>
            <th>Witness 1</th>
            <th>Witness 2 / Next of Kin</th>
        </tr>

        <tr>
            <th>Name</th>
            <td><asp:Label ID="lblW1Name" runat="server" /></td>
            <td><asp:Label ID="lblW2Name" runat="server" /></td>
        </tr>

        <tr>
            <th>Address</th>
            <td><asp:Label ID="lblW1Address" runat="server" /></td>
            <td><asp:Label ID="lblW2Address" runat="server" /></td>
        </tr>

        <tr>
            <th>Relationship</th>
            <td><asp:Label ID="lblW1Relation" runat="server" /></td>
            <td><asp:Label ID="lblW2Relation" runat="server" /></td>
        </tr>

        <tr>
            <th>Mobile/Email</th>
            <td><asp:Label ID="lblW1Mobile" runat="server" /></td>
            <td><asp:Label ID="lblW2Mobile" runat="server" /></td>
        </tr>

        <tr>
            <th>Aadhaar</th>
            <td><asp:Label ID="lblW1Aadhaar" runat="server" /></td>
            <td><asp:Label ID="lblW2Aadhaar" runat="server" /></td>
        </tr>

        <tr>
            <th>Signature</th>
            <td>____________</td>
            <td>____________</td>
        </tr>
    </table>

</div>

        </div>
    </div>
</div>
</asp:Content>