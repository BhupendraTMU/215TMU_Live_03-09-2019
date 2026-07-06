<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master"
    AutoEventWireup="true" CodeFile="CadaverReception.aspx.cs" Inherits="Faculty_CadaverReception" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function ViewUserDetails() {
            $("#CadaverModal").modal("show");
        }
        function SaveValues() {
            document.getElementById('<%= hfAge.ClientID %>').value =
                document.getElementById('<%= uptxtApproximateAge.ClientID %>').value;

            document.getElementById('<%= hfReceivedDate.ClientID %>').value =
                document.getElementById('<%= uptxtPMReceivedDate.ClientID %>').value;

            document.getElementById('<%= hfPanchanamaDate.ClientID %>').value =
                document.getElementById('<%= uptxtPanchanamaDate.ClientID %>').value;

            document.getElementById('<%= hfCreateReceivedDate.ClientID %>').value =
                document.getElementById('<%= txtReceivedDate.ClientID %>').value;

            document.getElementById('<%= hfCreatePanchanamaDate.ClientID %>').value =
                document.getElementById('<%= txtPanchanamaDate.ClientID %>').value;

            document.getElementById('<%= hfCreatePMPMStartDate.ClientID %>').value =
                document.getElementById('<%= uptxtPMPMStartDate.ClientID %>').value;

              document.getElementById('<%= hfCreatePMPMEndDate.ClientID %>').value =
                  document.getElementById('<%= uptxtPMPMEndDate.ClientID %>').value;
            
        }
        

        function SaveValues1() {
              
        document.getElementById('<%= hfCreateReceivedDate.ClientID %>').value =
            document.getElementById('<%= txtReceivedDate.ClientID %>').value;

        document.getElementById('<%= hfCreatePanchanamaDate.ClientID %>').value =
            document.getElementById('<%= txtPanchanamaDate.ClientID %>').value;

        document.getElementById('<%= hfCreateApproximateAge.ClientID %>').value =
            document.getElementById('<%= txtApproximateAge.ClientID %>').value;

            document.getElementById('<%= hfCreatePMPMStartDate.ClientID %>').value =
                document.getElementById('<%= uptxtPMPMStartDate.ClientID %>').value;

        document.getElementById('<%= hfCreatePMPMEndDate.ClientID %>').value =
            document.getElementById('<%= uptxtPMPMEndDate.ClientID %>').value;
        }
    </script>
    <style>
        .grid11 th,
.grid11 td {
    font-size: 11px !important;
}
     
.fixedGrid{
    width:max-content;
    min-width:100%;
}

.fixedGrid th,
.fixedGrid td{
    white-space:nowrap;
}

/* Action column fixed */
.fixedGrid th:last-child{
    position:sticky;
    right:0;
    z-index:10;
}
.fixedGrid td:last-child{
    position:sticky;
    right:0;
    z-index:10;
    background-color:white;
}
    </style>
    <link rel="stylesheet"
      href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <table cellpadding="0px" cellspacing="0px" style="width:100%">
            <tr> <td style="height:13px"> </td></tr>
         <tr> <td>  


      &nbsp;&nbsp;&nbsp; &nbsp;  <asp:Label ID="Label3" runat="server" 
            Text="Cadavers Reception" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
            </td></tr> 

            <tr> <td style="height:13px"> </td></tr>


        <tr> <td class="leftm">  </td></tr>

        <tr> <td style="height:13px"> </td></tr>

        <tr> <td align="center1">
            <table cellpadding="0px" cellspacing="0px"> 
                <tr>
                    <td>  Cadaver No.</td> 
                        <td style="width:10px"> </td>
                    <td> 
                        <asp:TextBox ID="txtSearchingValue" runat="server" Height="29px" ValidationGroup="g11" OnClick="btnSearch_Click"></asp:TextBox>
                    </td>
                    <td style="width:10px"> </td>
                    <td> 
                        <asp:Button ID="btnGet" runat="server" Text="Search" OnClick="btnSearch_Click" ValidationGroup="g12"/>

                    </td>
                    <td style="width:730px"> </td>
                    <td>  <asp:Button ID="btnexporttoexcel" runat="server" Text="Export To Excel" OnClick="btnexporttoexcel_Click" ValidationGroup="g13" /></td>
                </tr> 

            </table> 

             </td></tr>

                <tr> <td style="height:13px"> </td></tr>

                <tr> <td>

                     <!-- GRID -->
                    <div style="width:1188px; overflow-x:auto;">
          <asp:GridView ID="grdCadavers" runat="server"
    AutoGenerateColumns="False" Width="100%" AllowPaging="true"
    CssClass="table table-striped table-bordered grid11 fixedGrid"
    EmptyDataText="No Data Found"
    OnRowCommand="grdCadavers_RowCommand"    OnRowDataBound="grdCadavers_RowDataBound"
 PageSize="10"
 OnPageIndexChanging="grdCadavers_PageIndexChanging">
    <Columns>
        <asp:BoundField DataField="srno" HeaderText="Sr No" />
        <asp:BoundField DataField="body_id" HeaderText="Cadaver ID" />
        <asp:BoundField DataField="police_station_pm_no" HeaderText="Police Station PM No." />
        <asp:BoundField DataField="received_date" HeaderText="Rceived Date" DataFormatString="{0:dd-MM-yyyy HH:mm}" />
         <asp:BoundField DataField="bodypmno" HeaderText="PM No." />
         <asp:BoundField DataField="body_name" HeaderText="Cadaver Name" />
        <asp:BoundField DataField="gender" HeaderText="Gender" />       
        <asp:BoundField DataField="approximate_age" HeaderText="Age" />    
        <asp:BoundField DataField="pm_startdate" HeaderText="PM Start Date" DataFormatString="{0:dd-MM-yyyy HH:mm}" />
         <asp:BoundField DataField="pm_enddate" HeaderText="PM End Date" DataFormatString="{0:dd-MM-yyyy HH:mm}" />
        <asp:BoundField DataField="police_station_name" HeaderText="Police Station" />  
                           <asp:TemplateField HeaderText="Return Police Members">
    <ItemTemplate>

        <div style="
            width:230px;
            white-space:normal;
            word-wrap:break-word;
            line-height:18px;">
            <%# Eval("ReturnPoliceMembers") %>
        </div>

    </ItemTemplate>

</asp:TemplateField>
          <asp:TemplateField HeaderText="Doctors List">
    <ItemTemplate>

        <div style="
            width:230px;
            white-space:normal;
            word-wrap:break-word;
            line-height:18px;">
            <%# Eval("DoctorMembers") %>
        </div>

    </ItemTemplate>

</asp:TemplateField>
        <asp:BoundField DataField="return_date" HeaderText="Body Return Date" />  
        <asp:BoundField DataField="pm_remarks" HeaderText="Remarks" />
            
        <asp:TemplateField HeaderText="Action">
    <ItemTemplate>
        
       <asp:LinkButton ID="btnView" runat="server"
            Text="View"
            CommandName="ViewCadaver"
            CommandArgument='<%# Eval("srno") %>'
            CssClass="btn btn-sm btn-primary"  Style="color:white;"/>

        &nbsp;

        <asp:LinkButton ID="btnUpdate" runat="server"
            Text="Update"
            CommandName="UpdateCadaver"
            CommandArgument='<%# Eval("srno") %>'
            CssClass="btn btn-sm btn-warning" Style="color:white;" />

         <asp:LinkButton ID="btnReturn" runat="server"
     Text="Return"
     CommandName="ReturnCadaver"
     CommandArgument='<%# Eval("srno") %>'
     CssClass="btn btn-sm btn-success" Style="color:white;" />
 </ItemTemplate>
</asp:TemplateField>

    </Columns>
</asp:GridView>
</div>
                     </td></tr>

                <tr> <td style="height:90px"> </td></tr>
    </table>

<!--Add MODAL -->
<div class="modal fade" id="CadaverModal">
    <div class="modal-dialog modal-xl" style="width: 770px;">
        <div class="modal-content">

            <!-- HEADER -->
            <div class="modal-header text-white" style="background-color:#e6812b;color:white">
                <h5 class="mb-0">Cadaver Reception</h5>
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
                 <asp:HiddenField ID="hfCreatePMPMStartDate" runat="server" />
                 <asp:HiddenField ID="hfCreatePMPMEndDate" runat="server" />
                <div class="col-md-4">
                    <label>Body Name</label>
                     <asp:TextBox ID="txtBodyName"
                            runat="server"
                            Text="Unknown"
                            CssClass="form-control" ReadOnly="true"
                            />
                </div>

                <div class="col-md-4">
                    <label>Police Station PM No</label>
                   <asp:TextBox ID="txtPoliceStationPMNo"
                        runat="server"
                        CssClass="form-control"
                        placeholder="Enter PM Number"  ReadOnly="true" />
                </div>

                <div class="col-md-4">
                    <label>Gender</label>
                    <asp:DropDownList ID="ddlGender"  ReadOnly="true"
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
                    <label>Approximate Age</label>
                   <asp:TextBox ID="txtApproximateAge"
        runat="server"  ReadOnly="true"
        CssClass="form-control"
        TextMode="Number" />
                </div>

                <div class="col-md-4">
                    <label>Received Date</label>
                    <asp:TextBox ID="txtReceivedDate"
                                    runat="server"  ReadOnly="true"
                                    CssClass="form-control"
                                    TextMode="DateTimeLocal" />
                </div>

                <div class="col-md-4">
                    <label>Panchanama Date</label>
                   <asp:TextBox ID="txtPanchanamaDate"  ReadOnly="true"
                        runat="server"
                        CssClass="form-control"
                        TextMode="DateTimeLocal" />
                </div>

            </div>

            <div class="row mt-3" style="margin-bottom:7px;">

                <div class="col-md-6">
                    <label>Police Station Name</label>
                  <asp:TextBox ID="txtPoliceStationName"
                        runat="server"  ReadOnly="true"
                        CssClass="form-control"
                        placeholder="Police Station Name" />
                </div>

                <div class="col-md-6" style="margin-top: 30px;">
                    <div class="custom-control custom-checkbox mt-2">
                     <asp:CheckBox ID="chkCMSRegistered"
                            runat="server"  ReadOnly="true"
                            Text="CMS Approval" />
                </div>
                </div>

            </div>

            <div class="row mt-3" style="margin-bottom:7px;">

                <div class="col-md-12">
                    <label>Remarks</label>
                    <asp:TextBox ID="txtRemarks"
                    runat="server"  ReadOnly="true"
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
            Police Personnel Details
        </div>

        <div class="card-body">

            <div class="row">

                <div class="col-md-3">
                    <label>Constable No</label>
                   <asp:TextBox ID="txtConstableNo"
                        runat="server"
                        CssClass="form-control" />
                </div>

                <div class="col-md-4">
                    <label>Member Name</label>
                   <asp:TextBox ID="txtMemberName"
                        runat="server"
                        CssClass="form-control" />
                </div>

                <div class="col-md-3">
                    <label>Mobile No</label>
                  <asp:TextBox ID="txtMobileNo"
                        runat="server"
                        CssClass="form-control" />
                </div>

                <div class="col-md-2">
                    <label>&nbsp;</label>
                    <asp:Button ID="btnAddDoctorMember"
                            runat="server"
                            Text="Add"
                            CssClass="btn btn-success btn-block"
                            OnClick="btnAddDoctorMember_Click" OnClientClick="SaveValues1();"/>
                </div>

            </div>

            <hr />
    <asp:HiddenField ID="hfEditIndex"
runat="server"
Value="-1" />
          <asp:GridView ID="gvDoctorMembers"
    runat="server"
    AutoGenerateColumns="False"
    DataKeyNames="SrNo"
    OnRowCommand="gvDoctorMembers_RowCommand"
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
                        OnClick="btnSave_Click" />

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
                <h5 class="mb-0">Cadaver Reception</h5>
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
                            Text="Unknown" ReadOnly="true"
                            CssClass="form-control"
                            />
                </div>
                <div class="col-md-4">
                    <label>Police Station PM No</label>
                   <asp:TextBox ID="uptxtPoliceStationPMNo"
                        runat="server" ReadOnly="true"
                        CssClass="form-control"
                        placeholder="Enter PM Number" />
                </div>
            </div>

            <div class="row mt-3" style="margin-bottom:7px;">
                 <div class="col-md-4">
     <label>Gender</label>
                        <asp:TextBox ID="upddlGender"
         runat="server" ReadOnly="true"
         CssClass="form-control"
         placeholder="Gender" />
 </div>
                 <div class="col-md-4">
                    <label>Age</label>
                   <asp:TextBox ID="uptxtApproximateAge"
        runat="server" ReadOnly="true"
        CssClass="form-control"
        TextMode="Number" />
                </div>
                 <div class="col-md-4">
                     <label>Panchanama Date</label>
                    <asp:TextBox ID="uptxtPanchanamaDate"
                         runat="server" ReadOnly="true"
                         CssClass="form-control"
                         TextMode="DateTimeLocal" />
                 </div>
                   </div>
             <div class="card-header bg-primary text-white" style="margin-bottom:15px;margin-top:15px;">
     PM Details
 </div>
                <div class="row mt-3" style="margin-bottom:7px;">
                 
                <div class="col-md-4">
                    <label>Received Date</label>
                    <asp:TextBox ID="uptxtPMReceivedDate"
                                    runat="server"
                                    CssClass="form-control"
                                    TextMode="DateTimeLocal" />
                    <asp:RequiredFieldValidator
    ID="rfvPMReceivedDate"
    runat="server"
    ControlToValidate="uptxtPMReceivedDate"
    ErrorMessage="PM Received Date is required."
    ForeColor="Red"
    Display="Dynamic"
    ValidationGroup="ReceptionReg" />
                </div>
                <div class="col-md-4">
                    <label>PM No</label>
                  <asp:TextBox ID="uptxtPMPMNo"
                        runat="server" 
                        CssClass="form-control"
                        placeholder="PM No" />
                    <asp:RequiredFieldValidator
    ID="rfvPMPMNo"
    runat="server"
    ControlToValidate="uptxtPMPMNo"
    ErrorMessage="PM No is required."
    ForeColor="Red"
    Display="Dynamic"
    ValidationGroup="ReceptionReg" />
                </div>
               <div class="col-md-4">
                <label>PM Start Date</label>
                <asp:TextBox ID="uptxtPMPMStartDate"
                                runat="server"
                                CssClass="form-control"
                                TextMode="DateTimeLocal" />
                   <asp:RequiredFieldValidator
    ID="rfvPMPMStartDate"
    runat="server"
    ControlToValidate="uptxtPMPMStartDate"
    ErrorMessage="PM Start Date is required."
    ForeColor="Red"
    Display="Dynamic"
    ValidationGroup="ReceptionReg" />
            </div>

            </div>

            <div class="row mt-3" style="margin-bottom:7px;">
                   <div class="col-md-4">
    <label>PM End Date</label>
    <asp:TextBox ID="uptxtPMPMEndDate"
                    runat="server"
                    CssClass="form-control"
                    TextMode="DateTimeLocal" />
                       <asp:RequiredFieldValidator
    ID="rfvPMPMEndDate"
    runat="server"
    ControlToValidate="uptxtPMPMEndDate"
    ErrorMessage="PM End Date is required."
    ForeColor="Red"
    Display="Dynamic"
    ValidationGroup="ReceptionReg" />
</div>
                <div class="col-md-8">
                    <label>Remarks</label>
                    <asp:TextBox ID="uptxtPMRemarks"
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
            Doctor Details
        </div>

        <div class="card-body">

            <div class="row">

                <div class="col-md-3">
                    <label>Employee Code</label>
                   <asp:TextBox ID="uptxtPMEmployeeCode"
                        runat="server"
                        CssClass="form-control" />
                    <asp:RequiredFieldValidator
    ID="rfvPMEmployeeCode"
    runat="server"
    ControlToValidate="uptxtPMEmployeeCode"
    ErrorMessage="Employee Code is required."
    ForeColor="Red"
    Display="Dynamic"
    ValidationGroup="DoctorMember" />
                </div>

                <div class="col-md-4">
                    <label>Doctor Name</label>
                   <asp:TextBox ID="uptxtPMDoctorName"
                        runat="server"
                        CssClass="form-control" />
                    <asp:RequiredFieldValidator
    ID="rfvPMDoctorName"
    runat="server"
    ControlToValidate="uptxtPMDoctorName"
    ErrorMessage="Doctor Name is required."
    ForeColor="Red"
    Display="Dynamic"
    ValidationGroup="DoctorMember" />
                </div>

                <div class="col-md-3">
                    <label>Mobile No</label>
                  <asp:TextBox ID="uptxtPMMobileNo"
                        runat="server"
                        CssClass="form-control" />
                    <asp:RequiredFieldValidator
    ID="rfvPMMobileNo"
    runat="server"
    ControlToValidate="uptxtPMMobileNo"
    ErrorMessage="Mobile Number is required."
    ForeColor="Red"
    Display="Dynamic"
    ValidationGroup="DoctorMember" />

<asp:RegularExpressionValidator
    ID="revPMMobileNo"
    runat="server"
    ControlToValidate="uptxtPMMobileNo"
    ValidationExpression="^[6-9]\d{9}$"
    ErrorMessage="Enter valid 10 digit mobile number."
    ForeColor="Red"
    Display="Dynamic"
    ValidationGroup="DoctorMember" />
                </div>

                <div class="col-md-2">
                    <label>&nbsp;</label>
                    <asp:Button ID="upbtnAddDoctorMember"
                            runat="server"
                            Text="Add"
                            OnClientClick="SaveValues();"
                            CssClass="btn btn-success btn-block"
                            OnClick="upbtnAddDoctorMember_Click" ValidationGroup="DoctorMember" />
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
          <asp:GridView ID="upgvDoctorMembers"
    runat="server"
    AutoGenerateColumns="False"
    DataKeyNames="id"
               OnRowCommand="upgvDoctorMembers_RowCommand"
    CssClass="table table-bordered table-striped">

    <Columns>
       
        <asp:BoundField HeaderText="#" DataField="id" />
        <asp:BoundField HeaderText="Employee Code" DataField="employee_code" />
        <asp:BoundField HeaderText="Doctor Name" DataField="doctor_name" />
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
                    Text="Update Unknown Body" ValidationGroup="ReceptionReg"
                    CssClass="btn btn-success px-4"
                    OnClick="upbtnSave_Click" />

                    <button type="button"
                        class="btn btn-danger"
                        data-dismiss="modal">
                        Cancel
                    </button>
            </div>

        </div>
    </div>
</div>

    <!-- Return Model -->
    <!--Update MODAL -->
<div class="modal fade" id="upCadaverModalReturn">
    <div class="modal-dialog modal-xl" style="width: 770px;">
        <div class="modal-content">

            <!-- HEADER -->
            <div class="modal-header text-white" style="background-color:#e6812b;color:white">
                <h5 class="mb-0">Cadaver Reception</h5>
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
      <asp:TextBox ID="uptxtBodyIdReturn"
             runat="server"
             Text="Unknown"
             CssClass="form-control" ReadOnly="true"
             />
 </div>
                <div class="col-md-4">
                    <label>Body Name</label>
                     <asp:TextBox ID="uptxtBodyNameReturn"
                            runat="server"
                            Text="Unknown" ReadOnly="true"
                            CssClass="form-control"
                            />
                </div>
                <div class="col-md-4">
                    <label>Police Station PM No</label>
                   <asp:TextBox ID="uptxtPoliceStationPMNoReturn"
                        runat="server" ReadOnly="true"
                        CssClass="form-control"
                        placeholder="Enter PM Number" />
                </div>
            </div>

            <div class="row mt-3" style="margin-bottom:7px;">
                 <div class="col-md-4">
     <label>Gender</label>
                        <asp:TextBox ID="upddlGenderReturn"
         runat="server" ReadOnly="true"
         CssClass="form-control"
         placeholder="Gender" />
 </div>
                 <div class="col-md-4">
                    <label>Age</label>
                   <asp:TextBox ID="uptxtApproximateAgeReturn"
        runat="server" ReadOnly="true"
        CssClass="form-control"
         />
                </div>
                 <div class="col-md-4">
                     <label>Panchanama Date</label>
                    <asp:TextBox ID="uptxtPanchanamaDateReturn"
                         runat="server" ReadOnly="true"
                         CssClass="form-control" 
                         />
                 </div>
                   </div>
             <div class="card-header bg-primary text-white" style="margin-bottom:15px;margin-top:15px;">
     PM Details
 </div>
                <div class="row mt-3" style="margin-bottom:7px;">
                 
                <div class="col-md-4">
                    <label>Received Date</label>
                    <asp:TextBox ID="uptxtPMReceivedDateReturn"
                                    runat="server" ReadOnly="true"
                                    CssClass="form-control"
                                     />
                </div>
                <div class="col-md-4">
                    <label>PM No</label>
                  <asp:TextBox ID="uptxtPMPMNoReturn"
                        runat="server"  ReadOnly="true"
                        CssClass="form-control"
                        placeholder="PM No" />
                </div>
               <div class="col-md-4">
                <label>PM Start Date</label>
                <asp:TextBox ID="uptxtPMPMStartDateReturn"
                                runat="server" ReadOnly="true"
                                CssClass="form-control"
                                 />
            </div>

            </div>

            <div class="row mt-3" style="margin-bottom:7px;">
                   <div class="col-md-4">
    <label>PM End Date</label>
    <asp:TextBox ID="uptxtPMPMEndDateReturn"
                    runat="server"
                    CssClass="form-control"
                    ReadOnly="true" />
</div>
                                   <div class="col-md-4">
    <label>Return Date</label>
    <asp:TextBox ID="uptxtPMReturnDateReturn"
                    runat="server"
                    CssClass="form-control"
                    TextMode="DateTimeLocal" />
                                       <asp:RequiredFieldValidator
    ID="rfvPMReturnDateReturn"
    runat="server"
    ControlToValidate="uptxtPMReturnDateReturn"
    ErrorMessage="Return Date is required."
    ForeColor="Red"
    Display="Dynamic"
    ValidationGroup="ReturnCadaver" />
</div>
                <div class="col-md-8">
                    <label>Remarks</label>
                    <asp:TextBox ID="uptxtPMRemarksReturn"
                    runat="server" ReadOnly="true"
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
                   <asp:TextBox ID="uptxtConstableNoReturn"
                        runat="server"
                        CssClass="form-control" />
                    <asp:RequiredFieldValidator
    ID="rfvConstableNoReturn"
    runat="server"
    ControlToValidate="uptxtConstableNoReturn"
    ErrorMessage="Constable No is required."
    ForeColor="Red"
    Display="Dynamic"
    ValidationGroup="ReturnPolice" />
                </div>

                <div class="col-md-4">
                    <label>Member Name</label>
                   <asp:TextBox ID="uptxtMemberNameReturn"
                        runat="server"
                        CssClass="form-control" />
                    <asp:RequiredFieldValidator
    ID="rfvMemberNameReturn"
    runat="server"
    ControlToValidate="uptxtMemberNameReturn"
    ErrorMessage="Member Name is required."
    ForeColor="Red"
    Display="Dynamic"
    ValidationGroup="ReturnPolice" />
                </div>

                <div class="col-md-3">
                    <label>Mobile No</label>
                  <asp:TextBox ID="uptxtMobileNoReturn"
                        runat="server"
                        CssClass="form-control" />
                    <asp:RequiredFieldValidator
    ID="rfvMobileNoReturn"
    runat="server"
    ControlToValidate="uptxtMobileNoReturn"
    ErrorMessage="Mobile Number is required."
    ForeColor="Red"
    Display="Dynamic"
    ValidationGroup="ReturnPolice" />

<asp:RegularExpressionValidator
    ID="revMobileNoReturn"
    runat="server"
    ControlToValidate="uptxtMobileNoReturn"
    ValidationExpression="^[6-9]\d{9}$"
    ErrorMessage="Enter valid 10 digit mobile number."
    ForeColor="Red"
    Display="Dynamic"
    ValidationGroup="ReturnPolice" />
                </div>

                <div class="col-md-2">
                    <label>&nbsp;</label>
                    <asp:Button ID="upbtnAddPoliceMemberReturn"
                            runat="server"
                            Text="Add"
                            OnClientClick="SaveValuesReturn();"
                            CssClass="btn btn-success btn-block"
                            OnClick="upbtnAddPoliceMemberReturn_Click"  ValidationGroup="ReturnPolice" />
                </div>

            </div>

            <hr />
    <asp:HiddenField ID="uphfEditIndexReturn"
runat="server"
Value="-1" />
            <asp:HiddenField ID="uphfBodyIdReturn" runat="server" />
            <asp:HiddenField ID="hfAgeReturn" runat="server" />
            <asp:HiddenField ID="hfReceivedDateReturn" runat="server" />
            <asp:HiddenField ID="hfPanchanamaDateReturn" runat="server" />
          <asp:GridView ID="upgvPoliceMembersReturn"
    runat="server"
    AutoGenerateColumns="False"
    DataKeyNames="id"
               OnRowCommand="upgvPoliceMembersReturn_RowCommand"
    CssClass="table table-bordered table-striped">

    <Columns>
       
        <asp:BoundField HeaderText="#" DataField="id" />
        <asp:BoundField HeaderText="Constable No" DataField="constable_no" />
        <asp:BoundField HeaderText="Member Name" DataField="member_name" />
        <asp:BoundField HeaderText="Mobile No" DataField="mobile_no" />

        <asp:TemplateField HeaderText="Action">

            <ItemTemplate>

                <asp:LinkButton ID="upbtnEditReturn"
                    OnClientClick="SaveValuesReturn();"
                    runat="server"
                    CssClass="btn btn-warning btn-sm"
                    CommandName="EditRow"
                    CommandArgument='<%# Container.DataItemIndex %>'
                    ToolTip="Edit">

                    <i class="fa fa-edit"></i>

                </asp:LinkButton>

               <asp:LinkButton ID="upbtnDeleteReturn"
                    runat="server"
                    CssClass="btn btn-danger btn-sm"
                    CommandName="DeleteRow"
                    CommandArgument='<%# Container.DataItemIndex %>'
                    ToolTip="Delete"
                    OnClientClick="SaveValuesReturn(); return confirm('Delete this member?');">

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
              <asp:Button ID="Button1"
                    runat="server"
                    Text="Update Unknown Body" ValidationGroup="ReturnCadaver"
                    CssClass="btn btn-success px-4"
                    OnClick="upbtnReturn_Click" />

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