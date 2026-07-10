<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master"
    AutoEventWireup="true" CodeFile="CadaverRegistration.aspx.cs" Inherits="Faculty_CadaverRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function ViewUserDetails() {
            $("#CadaverModal").modal("show");
        }
        function SaveValues() {
            document.getElementById('<%= hfAge.ClientID %>').value =
                document.getElementById('<%= uptxtApproximateAge.ClientID %>').value;

    document.getElementById('<%= hfReceivedDate.ClientID %>').value =
        document.getElementById('<%= uptxtReceivedDate.ClientID %>').value;

            document.getElementById('<%= hfPanchanamaDate.ClientID %>').value =
                document.getElementById('<%= uptxtPanchanamaDate.ClientID %>').value;

            document.getElementById('<%= hfCreateReceivedDate.ClientID %>').value =
                document.getElementById('<%= txtReceivedDate.ClientID %>').value;

            document.getElementById('<%= hfCreatePanchanamaDate.ClientID %>').value =
                    document.getElementById('<%= txtPanchanamaDate.ClientID %>').value;
        }

        function SaveValues1() {
              
        document.getElementById('<%= hfCreateReceivedDate.ClientID %>').value =
            document.getElementById('<%= txtReceivedDate.ClientID %>').value;

        document.getElementById('<%= hfCreatePanchanamaDate.ClientID %>').value =
            document.getElementById('<%= txtPanchanamaDate.ClientID %>').value;

        document.getElementById('<%= hfCreateApproximateAge.ClientID %>').value =
         document.getElementById('<%= txtApproximateAge.ClientID %>').value;
       }
    </script>
    <link rel="stylesheet"
      href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <table cellpadding="0px" cellspacing="0px" style="width:100%">
            <tr> <td style="height:13px"> </td></tr>
         <tr> <td>  


      &nbsp;&nbsp;&nbsp; &nbsp;  <asp:Label ID="Label3" runat="server" 
            Text="Cadavers Registration" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
            </td></tr> 

            <tr> <td style="height:13px"> </td></tr>


        <tr> <td class="leftm">  </td></tr>

        <tr> <td style="height:13px"> </td></tr>

        <tr> <td align="center">
            <table cellpadding="0px" cellspacing="0px"> 
                <tr>
                    <td>  Cadaver No.</td> 
                        <td style="width:10px"> </td>
                    <td> 
                        <asp:TextBox ID="txtCadaverNo" runat="server" Height="29px" ValidationGroup="g11"></asp:TextBox>
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
          <asp:GridView ID="grdCadavers" runat="server"
    AutoGenerateColumns="False" Width="100%"
    CssClass="table table-striped table-bordered"
    EmptyDataText="No Data Found"
    OnRowCommand="grdCadavers_RowCommand"  AllowPaging="true"
 PageSize="10"
 OnPageIndexChanging="grdCadavers_PageIndexChanging">

    <Columns>
        <asp:BoundField DataField="srno" HeaderText="Sr No" />
        <asp:BoundField DataField="body_id" HeaderText="Cadaver ID" />
        <asp:BoundField DataField="is_unknown" HeaderText="Is Unknown" />
         <asp:BoundField DataField="police_station_pm_no" HeaderText="PM No." />
        <asp:BoundField DataField="body_name" HeaderText="Cadaver Name" />
        <asp:BoundField DataField="gender" HeaderText="Gender" />
        <asp:BoundField DataField="approximate_age" HeaderText="Age" />    
         <asp:BoundField DataField="panchanama_date" HeaderText="Panchanama Date"
     DataFormatString="{0:dd-MM-yyyy}" />
        <asp:BoundField DataField="received_date" HeaderText="Rceived Date"
            DataFormatString="{0:dd-MM-yyyy HH:mm}" />
           
       <asp:TemplateField HeaderText="Police Members">
    <ItemTemplate>

        <div style="
            width:250px;
            white-space:normal;
            word-wrap:break-word;
            line-height:18px;">
            <%# Eval("PoliceMembers") %>
        </div>

    </ItemTemplate>

</asp:TemplateField>
         <asp:BoundField DataField="is_cms_registered" HeaderText="CMS Status" />
        <asp:TemplateField HeaderText="Action">
    <ItemTemplate>
       <asp:LinkButton ID="btnView" runat="server"
            Text="View"
            CommandName="ViewCadaver"
            CommandArgument='<%# Eval("srno") %>'
            CssClass="btn btn-sm btn-primary"  Style="color:white;" CausesValidation="false"/>

        &nbsp;

        <asp:LinkButton ID="btnUpdate" runat="server"
            Text="Update"
            CommandName="UpdateCadaver"
            CommandArgument='<%# Eval("srno") %>'
            CssClass="btn btn-sm btn-warning" Style="color:white;" CausesValidation="false" />
 </ItemTemplate>
</asp:TemplateField>

    </Columns>
</asp:GridView>

                     </td></tr>

                <tr> <td style="height:90px"> </td></tr>
    </table>

<!--Add MODAL -->
<div class="modal fade" id="CadaverModal">
    <div class="modal-dialog modal-xl" style="width: 770px;">
        <div class="modal-content">

            <!-- HEADER -->
            <div class="modal-header text-white" style="background-color:#e6812b;color:white">
                <h5 class="mb-0">Cadaver Registration</h5>
                 <button type="button" class="close text-white" data-dismiss="modal" style="margin-top: -22px;">
     <span>&times;</span>
 </button>
            </div>

            <!-- BODY -->
            <div class="modal-body">

                <asp:UpdatePanel runat="server">
                    <ContentTemplate>

                        <div class="container-fluid">

    <!-- BODY DETAILS -->
    <div class="card mb-3">

        <div class="card-header bg-primary text-white" style="margin-bottom:15px;">
            Body Details
        </div>

        <div class="card-body">

            <div class="row" style="margin-bottom:7px;">
                            <asp:HiddenField ID="hfCreateReceivedDate" runat="server" />
             <asp:HiddenField ID="hfCreatePanchanamaDate" runat="server" />
                <asp:HiddenField ID="hfCreateApproximateAge" runat="server" />
                <div class="col-md-4">
                    <label>Body Name</label>
                     <asp:TextBox ID="txtBodyName"
                            runat="server"
                            Text="Unknown"
                            CssClass="form-control"
                            />
                    <asp:RequiredFieldValidator
                            ID="rfvBodyName"
                            runat="server"
                            ControlToValidate="txtBodyName"  ValidationGroup="CadaverReg"
                            ErrorMessage="Body Name is required."
                            ForeColor="Red"
                            Display="Dynamic" />
                </div>

                <div class="col-md-4">
                    <label>Police Station PM No</label>
                   <asp:TextBox ID="txtPoliceStationPMNo"
                        runat="server"
                        CssClass="form-control"
                        placeholder="Enter PM Number" />
                    <asp:RequiredFieldValidator
                            ID="rfvPMNo"
                            runat="server"  ValidationGroup="CadaverReg"
                            ControlToValidate="txtPoliceStationPMNo"
                            ErrorMessage="Police Station PM No is required."
                            ForeColor="Red"
                            Display="Dynamic" />
                </div>

                <div class="col-md-4">
                    <label>Gender</label>
                    <asp:DropDownList ID="ddlGender"
                        runat="server"
                        CssClass="form-control">

                        <asp:ListItem Text="--Select--" Value="" />
                        <asp:ListItem Text="Male" Value="Male" />
                        <asp:ListItem Text="Female" Value="Female" />
                        <asp:ListItem Text="Other" Value="Other" />

                    </asp:DropDownList>
                </div>

            </div>

            <div class="row mt-3" style="margin-bottom:7px;">

                <div class="col-md-4">
                    <label>Age</label>
                   <asp:TextBox ID="txtApproximateAge"
        runat="server"
        CssClass="form-control"
        TextMode="Number" />
                    <asp:RangeValidator
    ID="rvAge"
    runat="server"
    ControlToValidate="txtApproximateAge"
    MinimumValue="1"
    MaximumValue="120"  ValidationGroup="CadaverReg"
    Type="Integer"
     ErrorMessage="Age must be between 1 and 120." />
                </div>

                <div class="col-md-4">
                    <label>Received Date</label>
                    <asp:TextBox ID="txtReceivedDate"
                                    runat="server"
                                    CssClass="form-control"
                                    TextMode="DateTimeLocal" />
                    <asp:RequiredFieldValidator
    ID="rfvReceivedDate"
    runat="server"
    ControlToValidate="txtReceivedDate"  ValidationGroup="CadaverReg"
    ErrorMessage="Received Date is required."
    ForeColor="Red"
    Display="Dynamic" />
                </div>

                <div class="col-md-4">
                    <label>Panchanama Date</label>
                   <asp:TextBox ID="txtPanchanamaDate"
                        runat="server"
                        CssClass="form-control"
                        TextMode="DateTimeLocal" />
                    <asp:RequiredFieldValidator
    ID="rfvPanchanamaDate"
    runat="server" ValidationGroup="CadaverReg"
    ControlToValidate="txtPanchanamaDate"
    ErrorMessage="Panchanama Date is required."
    ForeColor="Red"
    Display="Dynamic" />
                </div>

            </div>

            <div class="row mt-3" style="margin-bottom:7px;">

                <div class="col-md-6">
                    <label>Police Station Name</label>
                  <asp:TextBox ID="txtPoliceStationName"
                        runat="server"
                        CssClass="form-control"
                        placeholder="Police Station Name" />
                    <asp:RequiredFieldValidator
    ID="rfvPoliceStation"
    runat="server"
    ControlToValidate="txtPoliceStationName" ValidationGroup="CadaverReg"
    ErrorMessage="Police Station Name is required."
    ForeColor="Red"
    Display="Dynamic" />
                </div>

                <div class="col-md-6" style="margin-top: 30px;">
                    <div class="custom-control custom-checkbox mt-2">
                     <asp:CheckBox ID="chkCMSRegistered" Checked="true"
                            runat="server"
                            Text="CMS Approval" />
                </div>
                </div>

            </div>

            <div class="row mt-3" style="margin-bottom:7px;">

                <div class="col-md-12">
                    <label>Remarks</label>
                    <asp:TextBox ID="txtRemarks"
                    runat="server"
                    CssClass="form-control"
                    TextMode="MultiLine"
                    Rows="3" />
                </div>

            </div>

        </div>

    </div>

    <!-- POLICE MEMBER DETAILS -->
    <div class="card mb-3">

        <div class="card-header bg-info text-white">
            Police Details
        </div>

        <div class="card-body">

            <div class="row">

                <div class="col-md-3">
                    <label>Constable No</label>
                   <asp:TextBox ID="txtConstableNo"
                        runat="server"
                        CssClass="form-control" />
                      <asp:RequiredFieldValidator
        ID="rfvConstableNo"
        runat="server"
        ControlToValidate="txtConstableNo"
        ErrorMessage="Constable No is required."
        ForeColor="Red"
        Display="Dynamic"
        ValidationGroup="Police" />
                </div>

                <div class="col-md-4">
                    <label>Member Name</label>
                   <asp:TextBox ID="txtMemberName"
                        runat="server"
                        CssClass="form-control" />
                     <asp:RequiredFieldValidator
        ID="rfvMemberName"
        runat="server"
        ControlToValidate="txtMemberName"
        ErrorMessage="Member Name is required."
        ForeColor="Red"
        Display="Dynamic"
        ValidationGroup="Police" />
                </div>

                <div class="col-md-3">
                    <label>Mobile No</label>
                  <asp:TextBox ID="txtMobileNo"
                        runat="server"
                        CssClass="form-control" />
                    <asp:RegularExpressionValidator
    ID="revMobile"
    runat="server"
    ControlToValidate="txtMobileNo"
    ValidationExpression="^[6-9]\d{9}$"
    ErrorMessage="Enter a valid 10-digit Mobile Number."
    ForeColor="Red"
    Display="Dynamic" ValidationGroup="Police" />
                    <asp:RequiredFieldValidator
    ID="rfvMobile"
    runat="server"
    ControlToValidate="txtMobileNo"
    ErrorMessage="Mobile Number is required."
    ForeColor="Red"
    Display="Dynamic" ValidationGroup="Police" />
                </div>

                <div class="col-md-2">
                    <label>&nbsp;</label>
                    <asp:Button ID="btnAddPoliceMember"
                            runat="server"
                            Text="Add"  ValidationGroup="Police"
                            CssClass="btn btn-success btn-block"
                            OnClick="btnAddPoliceMember_Click" OnClientClick="SaveValues1();"/>
                </div>

            </div>

            <hr />
    <asp:HiddenField ID="hfEditIndex"
runat="server"
Value="-1" />
          <asp:GridView ID="gvPoliceMembers"
    runat="server"
    AutoGenerateColumns="False"
    DataKeyNames="SrNo"
    OnRowCommand="gvPoliceMembers_RowCommand"
    CssClass="table table-bordered table-striped">

    <Columns>
       
        <asp:BoundField HeaderText="#" DataField="SrNo" />
        <asp:BoundField HeaderText="Constable No" DataField="ConstableNo" />
        <asp:BoundField HeaderText="Member Name" DataField="MemberName" />
        <asp:BoundField HeaderText="Mobile No" DataField="MobileNo" />

        <asp:TemplateField HeaderText="Action">

            <ItemTemplate>

                <asp:LinkButton ID="btnEdit"
                    runat="server"
                    CssClass="btn btn-warning btn-sm"
                    CommandName="EditRow" OnClientClick="SaveValues1();"
                    CommandArgument='<%# Container.DataItemIndex %>'
                    ToolTip="Edit">

                    <i class="fa fa-edit"></i>

                </asp:LinkButton>

             <asp:LinkButton ID="btnDelete"
                runat="server"
                CssClass="btn btn-danger btn-sm"
                CommandName="DeleteRow"
                CommandArgument='<%# Container.DataItemIndex %>'
                ToolTip="Delete"
                OnClientClick="SaveValues(); return confirm('Delete this member?');">

                <i class="fa fa-trash"></i>

            </asp:LinkButton>

            </ItemTemplate>

        </asp:TemplateField>

    </Columns>

</asp:GridView>

        </div>

    </div>

    
</div>

                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>

            <!-- FOOTER -->
            <div class="modal-footer">
                <asp:Button ID="btnSave"
                        runat="server"
                        Text="Save Unknown Body"
                        CssClass="btn btn-success px-4"
                        OnClick="btnSave_Click" ValidationGroup="CadaverReg" />

                   <asp:Button ID="btnClear"
                    runat="server"
                    Text="Clear"
                    CssClass="btn btn-secondary"
                    OnClick="btnClear_Click"
                    OnClientClick="return confirm('Are you sure you want to clear all data?');" />

                    <button type="button"
                        class="btn btn-danger"
                        data-dismiss="modal">
                        Cancel
                    </button>
            </div>

        </div>
    </div>
</div>


<div class="modal fade" id="CadaverModal1" tabindex="-1">
    <div class="modal-dialog modal-xl">
        <div class="modal-content">

            <div class="modal-header text-white" style="background-color:#e6812b;color:white">
                <h5 class="modal-title">Cadaver Registration</h5>
                <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close" style="margin-top: -20px;color:white;">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">

    <h3 class="mb-3"><b>Cadaver Details</b></h3>

  <table class="table table-bordered">

<tr>
    <th>Body ID</th>
    <td><asp:Label ID="lblBodyId" runat="server" /></td>

    <th>PM No</th>
    <td><asp:Label ID="lblPMNo" runat="server" /></td>
</tr>

<tr>
    <th>Body Name</th>
    <td><asp:Label ID="lblBodyName" runat="server" /></td>

    <th>Gender</th>
    <td><asp:Label ID="lblGender" runat="server" /></td>
</tr>

<tr>
    <th>Age</th>
    <td><asp:Label ID="lblAge" runat="server" /></td>

    <th>CMS Registered</th>
    <td><asp:Label ID="lblCMS" runat="server" /></td>
</tr>

<tr>
    <th>Received Date</th>
    <td><asp:Label ID="lblReceivedDate" runat="server" /></td>

    <th>Panchanama Date</th>
    <td><asp:Label ID="lblPanchanamaDate" runat="server" /></td>
</tr>

<tr>
    <th>Father Name</th>
    <td><asp:Label ID="lblFatherName" runat="server" /></td>

    <th>Address</th>
    <td><asp:Label ID="lblAddress" runat="server" /></td>
</tr>
<tr>
    <th>Police Station</th>
    <td colspan="3">
        <asp:Label ID="lblPoliceStation" runat="server" />
    </td>
</tr>

<tr>
    <th>Remarks</th>
    <td colspan="3">
        <asp:Label ID="lblRemarks" runat="server" />
    </td>
</tr>

</table>
     <h4 class="mt-4">
    <b>Police Details</b>
</h4>

<asp:GridView ID="gvPoliceDetail"
    runat="server"
    AutoGenerateColumns="False"
    CssClass="table table-bordered table-striped">

    <Columns>

        <asp:BoundField
            DataField="constable_no"
            HeaderText="Constable No" />

        <asp:BoundField
            DataField="member_name"
            HeaderText="Member Name" />

        <asp:BoundField
            DataField="mobile_no"
            HeaderText="Mobile No" />

    </Columns>

</asp:GridView>
   <h4 class="mt-4">
    <b>Doctor Details</b>
</h4>

<asp:GridView ID="gvDoctorMembersView"
    runat="server"
    AutoGenerateColumns="False"
    CssClass="table table-bordered table-striped">

    <Columns>

        <asp:BoundField
            DataField="employee_code"
            HeaderText="Employee Code" />

        <asp:BoundField
            DataField="doctor_name"
            HeaderText="Doctor Name" />

        <asp:BoundField
            DataField="mobile_no"
            HeaderText="Mobile No" />

    </Columns>

</asp:GridView>
 <h4 class="mt-4">
  <b>PM Details</b>

     <table class="table table-bordered">

<tr>
    <th>Received Date</th>
    <td><asp:Label ID="lblPMReceivedDate" runat="server" /></td>
    <th>PM No</th>
    <td><asp:Label ID="lblPMPMNo" runat="server" /></td>
</tr>

<tr>   
    <th>PM Start Date</th>
    <td><asp:Label ID="lblPMPMStartDate" runat="server" /></td>
      <th>PM End Date</th>
     <td><asp:Label ID="lblPMPMEndDate" runat="server" /></td>
</tr>
 <tr>   
     <th>Remarks</th>
    <td colspan="3"><asp:Label ID="lblPMRemarks" runat="server" /></td>
</tr>
</table>

     <h4 class="mt-4">
    <b>Return Police Details</b>
</h4>

<asp:GridView ID="gvReturnDoctorMembersView"
    runat="server"
    AutoGenerateColumns="False"
    CssClass="table table-bordered table-striped">

    <Columns>

        <asp:BoundField
            DataField="constable_no"
            HeaderText="Constable No" />

        <asp:BoundField
            DataField="member_name"
            HeaderText="Member Name" />

        <asp:BoundField
            DataField="mobile_no"
            HeaderText="Mobile No" />

    </Columns>

</asp:GridView>

   

</div>

        </div>
    </div>
</div>


<!--Update MODAL -->
<div class="modal fade" id="upCadaverModal">
    <div class="modal-dialog modal-xl" style="width: 770px;">
        <div class="modal-content">

            <!-- HEADER -->
            <div class="modal-header text-white" style="background-color:#e6812b;color:white">
                <h5 class="mb-0">Cadaver Registration</h5>
                 <button type="button" class="close text-white" data-dismiss="modal" style="margin-top: -22px;">
     <span>&times;</span>
 </button>
            </div>

            <!-- BODY -->
            <div class="modal-body">

                <asp:UpdatePanel runat="server">
                    <ContentTemplate>

                        <div class="container-fluid">

    <!-- BODY DETAILS -->
    <div class="card mb-3">

        <div class="card-header bg-primary text-white" style="margin-bottom:15px;">
            Body Details
        </div>

        <div class="card-body">

            <div class="row" style="margin-bottom:7px;">
                 <div class="col-md-4">
            <label>Body Id</label>
      <asp:TextBox ID="uptxtBodyId"
             runat="server"
             Text="Unknown"
             CssClass="form-control" ReadOnly="true"
             />
 </div>
                <div class="col-md-4">
                    <label>Body Name</label>
                     <asp:TextBox ID="uptxtBodyName"
                            runat="server"
                            Text="Unknown"
                            CssClass="form-control"
                            />
                    <asp:RequiredFieldValidator
    ID="uprfvBodyName"
    runat="server"
    ControlToValidate="uptxtBodyName"
    ErrorMessage="Body Name is required."
    ForeColor="Red"
    Display="Dynamic"
    ValidationGroup="UpDateCadaverReg" />
                </div>

                <div class="col-md-4">
                    <label>Police Station PM No</label>
                   <asp:TextBox ID="uptxtPoliceStationPMNo"
                        runat="server"
                        CssClass="form-control"
                        placeholder="Enter PM Number" />
                    <asp:RequiredFieldValidator
    ID="uprfvPMNo"
    runat="server"
    ControlToValidate="uptxtPoliceStationPMNo"
    ErrorMessage="PM No is required."
    ForeColor="Red"
    Display="Dynamic"
    ValidationGroup="UpDateCadaverReg" />
                </div>

               

            </div>

            <div class="row mt-3" style="margin-bottom:7px;">
                 <div class="col-md-4">
     <label>Gender</label>
     <asp:DropDownList ID="upddlGender"
         runat="server"
         CssClass="form-control">

         <asp:ListItem Text="--Select--" Value="" />
         <asp:ListItem Text="Male" Value="Male" />
         <asp:ListItem Text="Female" Value="Female" />
         <asp:ListItem Text="Other" Value="Other" />

     </asp:DropDownList>
 </div>
                <div class="col-md-4">
                    <label> Age</label>
                   <asp:TextBox ID="uptxtApproximateAge"
        runat="server"
        CssClass="form-control"
        TextMode="Number" />
                </div>

                <div class="col-md-4">
                    <label>Received Date</label>
                    <asp:TextBox ID="uptxtReceivedDate"
                                    runat="server"
                                    CssClass="form-control"
                                    TextMode="DateTimeLocal" />
                    <asp:RequiredFieldValidator
    ID="uprfvReceivedDate"
    runat="server"
    ControlToValidate="uptxtReceivedDate"
    ErrorMessage="Received Date is required."
    ForeColor="Red"
    Display="Dynamic"
    ValidationGroup="UpDateCadaverReg" />
                </div>
                </div>
            <div class="row mt-3" style="margin-bottom:7px;">
                <div class="col-md-4">
                    <label>Panchanama Date</label>
                   <asp:TextBox ID="uptxtPanchanamaDate"
                        runat="server"
                        CssClass="form-control"
                        TextMode="DateTimeLocal" />
                    <asp:RequiredFieldValidator
    ID="uprfvPanchanamaDate"
    runat="server"
    ControlToValidate="uptxtPanchanamaDate"
    ErrorMessage="Panchanama Date is required."
    ForeColor="Red"
    Display="Dynamic"
    ValidationGroup="UpDateCadaverReg" />
                </div>
                  <div class="col-md-4">
                      <label>Father Name</label>
                     <asp:TextBox ID="uptxtFatherName"
                       runat="server"
                       CssClass="form-control"
                       placeholder="Father Name" />
                 </div>
                 <div class="col-md-4">
                     <label>Address</label>
                    <asp:TextBox ID="uptxtAddress"
                      runat="server"
                      CssClass="form-control"
                      placeholder="Address" />
                </div>
            </div>

            <div class="row mt-3" style="margin-bottom:7px;">

                <div class="col-md-6">
                    <label>Police Station Name</label>
                  <asp:TextBox ID="uptxtPoliceStationName"
                        runat="server"
                        CssClass="form-control"
                        placeholder="Police Station Name" />
                    <asp:RequiredFieldValidator
    ID="uprfvPoliceStation"
    runat="server"
    ControlToValidate="uptxtPoliceStationName"
    ErrorMessage="Police Station Name is required."
    ForeColor="Red"
    Display="Dynamic"
    ValidationGroup="UpDateCadaverReg" />
                </div>

                <div class="col-md-6" style="margin-top: 30px;">
                    <div class="custom-control custom-checkbox mt-2">
                     <asp:CheckBox ID="upchkCMSRegistered"
                            runat="server"
                            Text="CMS Registered" />
                </div>
                </div>

            </div>

            <div class="row mt-3" style="margin-bottom:7px;">

                <div class="col-md-12">
                    <label>Remarks</label>
                    <asp:TextBox ID="uptxtRemarks"
                    runat="server"
                    CssClass="form-control"
                    TextMode="MultiLine"
                    Rows="3" />
                </div>

            </div>

        </div>

    </div>

    <!-- POLICE MEMBER DETAILS -->
    <div class="card mb-3">

        <div class="card-header bg-info text-white">
            Police Details
        </div>

        <div class="card-body">

            <div class="row">

                <div class="col-md-3">
                    <label>Constable No</label>
                   <asp:TextBox ID="uptxtConstableNo"
                        runat="server"
                        CssClass="form-control" />
                    <asp:RequiredFieldValidator
    ID="uprfvConstableNo"
    runat="server"
    ControlToValidate="uptxtConstableNo"
    ErrorMessage="Constable No is required."
    ForeColor="Red"
    Display="Dynamic"
    ValidationGroup="UpdatePolice" />
                </div>

                <div class="col-md-4">
                    <label>Member Name</label>
                   <asp:TextBox ID="uptxtMemberName"
                        runat="server"
                        CssClass="form-control" />
                                    <asp:RequiredFieldValidator
ID="RequiredFieldValidator1"
runat="server"
ControlToValidate="uptxtMemberName"
ErrorMessage="Member Name is required."
ForeColor="Red"
Display="Dynamic"
ValidationGroup="UpdatePolice" />
                </div>

                <div class="col-md-3">
                    <label>Mobile No</label>
                  <asp:TextBox ID="uptxtMobileNo"
                        runat="server"
                        CssClass="form-control" />
                    <asp:RequiredFieldValidator
    ID="uprfvMobile"
    runat="server"
    ControlToValidate="uptxtMobileNo"
    ErrorMessage="Mobile Number is required."
    ForeColor="Red"
    Display="Dynamic"
    ValidationGroup="UpdatePolice" />
                </div>

                <div class="col-md-2">
                    <label>&nbsp;</label>
                    <asp:Button ID="upbtnAddPoliceMember"
                            runat="server"
                            Text="Add"
                            OnClientClick="SaveValues();"
                            CssClass="btn btn-success btn-block"
                            OnClick="upbtnAddPoliceMember_Click" ValidationGroup="UpdatePolice" CausesValidation="true"/>
                </div>

            </div>

            <hr />
    <asp:HiddenField ID="uphfEditIndex"
runat="server"
Value="-1" />
            <asp:HiddenField ID="uphfBodyId" runat="server" />
            <asp:HiddenField ID="hfAge" runat="server" />
            <asp:HiddenField ID="hfReceivedDate" runat="server" />
            <asp:HiddenField ID="hfPanchanamaDate" runat="server" />
          <asp:GridView ID="upgvPoliceMembers"
    runat="server"
    AutoGenerateColumns="False"
    DataKeyNames="id"
               OnRowCommand="upgvPoliceMembers_RowCommand"
    CssClass="table table-bordered table-striped">

    <Columns>
       
        <asp:BoundField HeaderText="#" DataField="id" />
        <asp:BoundField HeaderText="Constable No" DataField="constable_no" />
        <asp:BoundField HeaderText="Member Name" DataField="member_name" />
        <asp:BoundField HeaderText="Mobile No" DataField="mobile_no" />

        <asp:TemplateField HeaderText="Action">

            <ItemTemplate>

                <asp:LinkButton ID="upbtnEdit"
                    OnClientClick="SaveValues();"
                    runat="server"
                    CssClass="btn btn-warning btn-sm"
                    CommandName="EditRow"
                    CommandArgument='<%# Container.DataItemIndex %>'
                    ToolTip="Edit">

                    <i class="fa fa-edit"></i>

                </asp:LinkButton>

               <asp:LinkButton ID="upbtnDelete"
                    runat="server"
                    CssClass="btn btn-danger btn-sm"
                    CommandName="DeleteRow"
                    CommandArgument='<%# Container.DataItemIndex %>'
                    ToolTip="Delete"
                    OnClientClick="SaveValues(); return confirm('Delete this member?');">

                    <i class="fa fa-trash"></i>

                </asp:LinkButton>

            </ItemTemplate>

        </asp:TemplateField>

    </Columns>

</asp:GridView>

        </div>

    </div>

    
</div>

                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>

            <!-- FOOTER -->
            <div class="modal-footer">
              <asp:Button ID="upbtnSave"
    runat="server"
    Text="Update Unknown Body"
    CssClass="btn btn-success px-4"
    OnClick="upbtnSave_Click"
    ValidationGroup="UpDateCadaverReg"
    CausesValidation="true" />

                    <button type="button"
                        class="btn btn-danger"
                        data-dismiss="modal">
                        Cancel
                    </button>
            </div>

        </div>
    </div>
</div>

</asp:Content>