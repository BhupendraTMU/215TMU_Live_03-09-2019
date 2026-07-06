<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master"
    AutoEventWireup="true" CodeFile="BodyRegistration.aspx.cs" Inherits="Faculty_BodyRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function ViewUserDetails() {
            $("#CadaverModal").modal("show");
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <table cellpadding="0px" cellspacing="0px" style="width:100%">
            <tr> <td style="height:13px"> </td></tr>
         <tr> <td>  


      &nbsp;&nbsp;&nbsp; &nbsp;  <asp:Label ID="Label3" runat="server" 
            Text="Body Registration" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
            </td></tr> 

            <tr> <td style="height:13px"> </td></tr>


        <tr> <td class="leftm">  </td></tr>

        <tr> <td style="height:13px"> </td></tr>

        <tr> <td align="center">
            <table cellpadding="0px" cellspacing="0px"> 
                <tr>
                    <td>Donor No/Aadhaar/Name</td> 
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
                          <asp:Button ID="btnCreate" runat="server" Text="Create" OnClick="btnCreate_Click" />
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
    AutoGenerateColumns="False"
    Width="100%"
    CssClass="table table-striped table-bordered table-hover"
    EmptyDataText="No Data Found"
    OnRowCommand="grdDonors_RowCommand"
    OnRowDataBound="grdDonors_RowDataBound"
    AllowPaging="true"
    PageSize="20"
    OnPageIndexChanging="grdDonors_PageIndexChanging">

    <Columns>
        <asp:BoundField DataField="CadaverCode" HeaderText="Cadaver Code" />
        <asp:BoundField DataField="DonorId" HeaderText="Donor Code" />
        <asp:BoundField DataField="Name" HeaderText="Name" />
        <asp:BoundField DataField="Gender" HeaderText="Gender" />

        <asp:BoundField DataField="DateOfDeath"
            HeaderText="Death Date"
            DataFormatString="{0:dd-MM-yyyy}" />
         <asp:BoundField DataField="AadhaarNumber" HeaderText="Aadhaar Card" />
        <asp:BoundField DataField="PlaceOfDeath" HeaderText="Place" />
        <asp:BoundField DataField="CauseOfDeath" HeaderText="Cause" />
        <asp:BoundField DataField="RegisteredDonor" HeaderText="Registered" />
        <asp:BoundField DataField="StatusName" HeaderText="Status" />
        <asp:TemplateField HeaderText="Documents">
            <ItemTemplate>
                <asp:HyperLink ID="lnkDeathDoc" runat="server" Target="_blank" Text="Death" /><br />
                <asp:HyperLink ID="lnkIDDoc" runat="server" Target="_blank" Text="ID" /><br />
                <asp:HyperLink ID="lnkPhotoDoc" runat="server" Target="_blank" Text="Photo" /><br />
                <asp:HyperLink ID="lnkPoliceDoc" runat="server" Target="_blank" Text="Police" /><br />
                <asp:HyperLink ID="lnkVolontaryBodyDonationDoc" runat="server" Target="_blank" Text="Volontary" /><br />
                <asp:HyperLink ID="lnkThanksLetterDoc" runat="server" Target="_blank" Text="Thanks Letter" />
            </ItemTemplate>
</asp:TemplateField>
         <asp:TemplateField HeaderText="Cadaver (Receive / Not Received)">
     <ItemTemplate>
           <!-- 🔥 NEW BUTTON -->
            <asp:LinkButton ID="btnReceive" runat="server"
                CommandName="ReceiveCadaver"
                CommandArgument='<%# Eval("CadaverId") %>'
                CssClass="btn btn-success btn-sm" style="color: black;">
                Receive
            </asp:LinkButton>
             </ItemTemplate>
</asp:TemplateField>
        <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <asp:LinkButton ID="btnView" runat="server"
                    Text="👁 View"
                    CommandName="ViewCadaver"
                    CommandArgument='<%# Eval("CadaverId") %>'
                    CssClass="btn btn-sm btn-primary" style="color: white;" />
                <asp:LinkButton ID="btnEdit" runat="server"
                    Text="✏ Edit"
                    CommandName="EditCadaver"
                    CommandArgument='<%# Eval("CadaverId") %>'
                    CssClass="btn btn-sm btn-warning" style="color: white;" />         
            </ItemTemplate>
        </asp:TemplateField>

    </Columns>
</asp:GridView>

                     </td></tr>

                <tr> <td style="height:90px"> </td></tr>
    </table>

<!--Add MODAL -->
<div class="modal fade" id="CadaverModal">
    <div class="modal-dialog modal-xl">
        <div class="modal-content">

            <!-- HEADER -->
             <div class="modal-header text-white" style="background-color:#e6812b;color:white">
                 <h5 class="modal-title">Body Registration (After Death)</h5>
                 <button type="button" class="close text-white" data-dismiss="modal" style="margin-top: -22px;">
                     <span>&times;</span>
                 </button>
             </div>
            <div class="modal-body">

                <div class="container-fluid">

                    <!-- CHECK DONOR -->
                    <div class="card mb-9" style="margin-bottom: 10px;">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-9">
                                    <label>Donor Reg No/Aadhaar</label>
                                    <asp:TextBox ID="txtSearchValue" runat="server" CssClass="form-control" />
                                     <asp:RequiredFieldValidator ID="rfvSearch" runat="server"
                                        ControlToValidate="txtSearchValue"
                                        ErrorMessage="Please enter Donor Reg No or Aadhaar"
                                        ForeColor="Red"
                                        Display="Dynamic"
                                        ValidationGroup="search" />
                                </div>

                                <div class="col-md-2">
                                    <label>&nbsp;</label>
                                    <asp:Button ID="btnCheckDonor" runat="server"
                                        Text="Check"
                                        CssClass="btn btn-primary form-control"
                                        OnClick="btnCheckDonor_Click" ValidationGroup="search"  />
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- DECEASED DETAILS -->
                    <div class="card mb-3">
                        <div class="card-header bg-info text-white">
                            Deceased Details
                        </div>

                        <div class="card-body">

                            <div class="row">
                                <div class="col-md-4">
                                    <label>Name</label>
                                    <asp:TextBox ID="txtDName" runat="server" CssClass="form-control" />
                                                                                <asp:RequiredFieldValidator ID="rfvRoom" runat="server"
ControlToValidate="txtDName"
ErrorMessage="Name is required"
ForeColor="Red"
Display="Dynamic"
ValidationGroup="save" />
                                </div>

                                <div class="col-md-4">
                                    <label>DOB</label>
                                    <asp:TextBox ID="txtDDOB" runat="server" TextMode="Date" CssClass="form-control" />
                                   
                                </div>

                                <div class="col-md-4">
                                    <label>Age</label>
                                    <asp:TextBox ID="txtDAge" runat="server" CssClass="form-control" />
                                                                        
                                </div>
                            </div>

                            <div class="row mt-2">
                                <div class="col-md-4">
                                    <label>Gender</label>
                                    <asp:DropDownList ID="ddlDGender" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="-- Select Gender --" Value=""></asp:ListItem>
                                        <asp:ListItem>Male</asp:ListItem>
                                        <asp:ListItem>Female</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
ControlToValidate="ddlDGender"
ErrorMessage="Gender is required"
ForeColor="Red"
Display="Dynamic"
ValidationGroup="save" />
                                </div>

                                <div class="col-md-4">
                                    <label>Date of Death</label>
                                    <asp:TextBox ID="txtDeathDate" runat="server" TextMode="Date" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
ControlToValidate="txtDeathDate"
ErrorMessage="Date of Death is required"
ForeColor="Red"
Display="Dynamic"
ValidationGroup="save" />
                                </div>

                                <div class="col-md-4">
                                    <label>Time of Death</label>
                                    <asp:TextBox ID="txtDeathTime" runat="server" TextMode="Time" CssClass="form-control" />
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
ControlToValidate="txtDeathTime"
ErrorMessage="Time of Death is required"
ForeColor="Red"
Display="Dynamic"
ValidationGroup="save" />
                                </div>
                            </div>

                            <div class="row mt-2">
                                <div class="col-md-6">
                                    <label>Aadhaar</label>
                                    <asp:TextBox ID="txtAadhaar" runat="server" CssClass="form-control" />
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
ControlToValidate="txtAadhaar"
ErrorMessage="Aadhaar is required"
ForeColor="Red"
Display="Dynamic"
ValidationGroup="save" />
                                </div>
                                <div class="col-md-6">
                                    <label>Place of Death</label>
                                    <asp:TextBox ID="txtPlace" runat="server" CssClass="form-control" />
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                    ControlToValidate="txtPlace"
                                    ErrorMessage="Place is required"
                                    ForeColor="Red"
                                    Display="Dynamic"
                                    ValidationGroup="save" />
                                </div>

                                <div class="col-md-6">
                                    <label>Cause of Death</label>
                                    <asp:TextBox ID="txtCause" runat="server" CssClass="form-control" />
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                       ControlToValidate="txtCause"
                                       ErrorMessage="Cause of Death is required"
                                       ForeColor="Red"
                                       Display="Dynamic"
                                       ValidationGroup="save" />
                                </div>
                            </div>

                        </div>
                    </div>

                    <!-- WITNESS 1 -->
                    <div class="card mb-3" style="margin-bottom: 10px;margin-top: 10px;">
                        <div class="card-header bg-info text-white">
                            Witness 1 Details
                        </div>

                        <div class="card-body">

                            <div class="row">
                                <div class="col-md-4">
                                    <label>Name</label>
                                    <asp:TextBox ID="txtCW1Name" runat="server" CssClass="form-control" />
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                      ControlToValidate="txtCW1Name"
                                      ErrorMessage="Name of Witness is required"
                                      ForeColor="Red"
                                      Display="Dynamic"
                                      ValidationGroup="save" />
                                </div>

                                <div class="col-md-4">
                                    <label>Relationship</label>
                                    <asp:TextBox ID="txtCW1Relation" runat="server" CssClass="form-control" />
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                      ControlToValidate="txtCW1Relation"
                                      ErrorMessage="Relation is required"
                                      ForeColor="Red"
                                      Display="Dynamic"
                                      ValidationGroup="save" />
                                </div>

                                <div class="col-md-4">
                                    <label>Mobile/Email</label>
                                    <asp:TextBox ID="txtCW1Mobile" runat="server" CssClass="form-control" />
                                     </div>
                            </div>

                            <div class="row mt-2">
                                <div class="col-md-6">
                                    <label>Address</label>
                                    <asp:TextBox ID="txtCW1Address" runat="server" CssClass="form-control" />
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
   ControlToValidate="txtCW1Address"
   ErrorMessage="Address is required"
   ForeColor="Red"
   Display="Dynamic"
   ValidationGroup="save" />
                                </div>

                                <div class="col-md-6">
                                    <label>Aadhaar</label>
                                    <asp:TextBox ID="txtCW1Aadhaar" runat="server" CssClass="form-control" />
                              
                                </div>
                                 <div class="col-md-6">
                                     <label>Aadhaar Upload</label>
                                       <asp:FileUpload ID="fuCW1Aadhaar" runat="server" CssClass="form-control" accept=".pdf,.doc,.docx,.jpg,.jpeg,.png,.gif,.bmp,.webp" />
                                 </div>
                            </div>

                        </div>
                    </div>

                    <!-- NEXT OF KIN -->
                    <div class="card mb-3"  style="margin-bottom: 10px;margin-top: 10px;">
                        <div class="card-header bg-info text-white">
                           Witness No. 2 / Next of Kin
                        </div>

                        <div class="card-body">

                            <div class="row">
                                <div class="col-md-4">
                                    <label>Name</label>
                                    <asp:TextBox ID="txtCW2Name" runat="server" CssClass="form-control" />
                                                                    
                                </div>

                                <div class="col-md-4">
                                    <label>Relationship</label>
                                    <asp:TextBox ID="txtCW2Relation" runat="server" CssClass="form-control" />
                                                                   
                                </div>

                                <div class="col-md-4">
                                    <label>Mobile/Email</label>
                                    <asp:TextBox ID="txtCW2Mobile" runat="server" CssClass="form-control" />
                                                                
                                </div>
                            </div>

                            <div class="row mt-2">
                                <div class="col-md-6">
                                    <label>Address</label>
                                    <asp:TextBox ID="txtCW2Address" runat="server" CssClass="form-control" />
                                                                       
                                </div>

                                <div class="col-md-6">
                                    <label>Aadhaar</label>
                                    <asp:TextBox ID="txtCW2Aadhaar" runat="server" CssClass="form-control" />                          
                                </div>
                                <div class="col-md-6">
                                    <label>Aadhaar Upload</label>
                                      <asp:FileUpload ID="fuCW2Aadhaar" runat="server" CssClass="form-control" accept=".pdf,.doc,.docx,.jpg,.jpeg,.png,.gif,.bmp,.webp"/>
                                </div>
                            </div>

                        </div>
                    </div>

                    <!-- DOCUMENTS -->
                    <div class="card mb-3">
                           <div class="card-header bg-info text-white">
                              Documents Submitted
                         </div>
                        <div class="card-body">

                            <div class="row">                               
                                 <div class="col-md-6">
                                     <label>Death Certificate/ Affidavit </label>
                                     <asp:FileUpload ID="fuDeath" runat="server" CssClass="form-control" accept=".pdf,.doc,.docx,.jpg,.jpeg,.png,.gif,.bmp,.webp"/>
                                 </div>
                                <div class="col-md-6">
                                    <label>ID Proof</label>
                                    <asp:FileUpload ID="fuID" runat="server" CssClass="form-control" accept=".pdf,.doc,.docx,.jpg,.jpeg,.png,.gif,.bmp,.webp" />
                                </div>

                                <div class="col-md-6">
                                    <label>Photograph</label>
                                    <asp:FileUpload ID="fuPhoto" runat="server" CssClass="form-control" accept=".pdf,.doc,.docx,.jpg,.jpeg,.png,.gif,.bmp,.webp"/>
                                </div>

                                <div class="col-md-6">
                                    <label>Police Intimation</label>
                                    <asp:FileUpload ID="fuPolice" runat="server" CssClass="form-control" accept=".pdf,.doc,.docx,.jpg,.jpeg,.png,.gif,.bmp,.webp"/>
                                </div>


                                  <div class="col-md-6">
                                      <label>Certificate For Volontary Body Donation</label>
                                      <asp:FileUpload ID="fuVolontaryBodyDonation" runat="server" CssClass="form-control" accept=".pdf,.doc,.docx,.jpg,.jpeg,.png,.gif,.bmp,.webp"/>
                                  </div>

                                  <div class="col-md-6">
                                      <label>Thanks Letter</label>
                                      <asp:FileUpload ID="fuThanksLetter" runat="server" CssClass="form-control" accept=".pdf,.doc,.docx,.jpg,.jpeg,.png,.gif,.bmp,.webp" />
                                  </div>



                            </div>

                        </div>
                    </div>

                </div>

            </div>

            <!-- FOOTER -->
            <div class="modal-footer">
                <asp:Button ID="btnSaveCadaver" runat="server"
                    Text="Save Body"
                    CssClass="btn btn-success px-4"
                    OnClick="btnSaveCadaver_Click" ValidationGroup="save"/>
            </div>

        </div>
    </div>
</div>
    <!-- 🔥 RECEPTION MODAL -->
<div class="modal fade" id="ReceptionModal">
    <div class="modal-dialog modal-xl">
        <div class="modal-content">

            <div class="modal-header bg-success text-white" style="background-color:#e6812b;color:white">
                <h5>Body Reception</h5>
                <button type="button" class="close text-white" data-dismiss="modal" style="margin-top: -22px;">
                    <span>&times;</span>
                </button>
            </div>

            <div class="modal-body">

                <div class="row">

                    <div class="col-md-6">
                        <label>Cadaver</label>
                        <asp:DropDownList ID="ddlCadaver" runat="server" CssClass="form-control"></asp:DropDownList>

                    </div>

                    <div class="col-md-6">
                        <label>Arrival Date Time</label>
                        <asp:TextBox ID="txtArrival" runat="server" CssClass="form-control" TextMode="DateTimeLocal"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server"
ControlToValidate="txtArrival"
ErrorMessage="Arrival Date/Time is required"
ForeColor="Red"
Display="Dynamic"
ValidationGroup="saveReceived" />
                    </div>

                    <div class="col-md-6 mt-3">
                        <label>Source</label>
                        <asp:DropDownList ID="ddlSource" runat="server" CssClass="form-control"></asp:DropDownList>
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server"
ControlToValidate="ddlSource"
ErrorMessage="Source is required"
ForeColor="Red"
Display="Dynamic"
ValidationGroup="saveReceived" />
                    </div>

                    <div class="col-md-6 mt-3">
                        <label>Witness Aadhaar</label>
                        <asp:TextBox ID="txtWitness" runat="server" CssClass="form-control"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server"
ControlToValidate="txtWitness"
ErrorMessage="Witness Aadhaar is required"
ForeColor="Red"
Display="Dynamic"
ValidationGroup="saveReceived" />
                    </div>

                    <div class="col-md-12 mt-3">
                        <label>Identification Details</label>
                        <asp:TextBox ID="txtIdentification" runat="server" CssClass="form-control"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server"
ControlToValidate="txtIdentification"
ErrorMessage="Identification is required"
ForeColor="Red"
Display="Dynamic"
ValidationGroup="saveReceived" />
                    </div>

                    <div class="col-md-12 mt-3">
                        <label>Consent Details</label>
                        <asp:TextBox ID="txtConsent" runat="server" CssClass="form-control"></asp:TextBox>
                                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server"
ControlToValidate="txtConsent"
ErrorMessage="Consent is required"
ForeColor="Red"
Display="Dynamic"
ValidationGroup="saveReceived" />
                    </div>

                </div>

            </div>

            <div class="modal-footer">
                <asp:Button ID="btnSaveReception" runat="server"
                    Text="Save Reception"
                    CssClass="btn btn-success"
                    OnClick="btnSaveReception_Click" ValidationGroup="saveReceived" />
            </div>

        </div>
    </div>
</div>
<div class="modal fade" id="cadaverViewModal" tabindex="-1">
    <div class="modal-dialog modal-xl">
        <div class="modal-content">

            <!-- HEADER -->
            <div class="modal-header text-white" style="background-color:#e6812b;color:white">
                <h5 class="modal-title">Cadaver Details</h5>
                <button type="button" class="close text-white" data-dismiss="modal" style="margin-top: -22px;">
                    <span>&times;</span>
                </button>
            </div>

            <div class="modal-body">

                <!-- DECEASED DETAILS -->
                <h3 class="mb-3"><b>DECEASED DETAILS</b></h3>

                <table class="table table-bordered">
                    <tr>
                        <th>Name</th>
                        <td><asp:Label ID="lblCName" runat="server" /></td>
                        <th>Gender</th>
                        <td><asp:Label ID="lblCGender" runat="server" /></td>
                    </tr>

                    <tr>
                        <th>DOB</th>
                        <td><asp:Label ID="lblCDOB" runat="server" /></td>
                        <th>Age</th>
                        <td><asp:Label ID="lblCAge" runat="server" /></td>
                    </tr>

                    <tr>
                        <th>Date of Death</th>
                        <td><asp:Label ID="lblDeathDate" runat="server" /></td>
                        <th>Time</th>
                        <td><asp:Label ID="lblDeathTime" runat="server" /></td>
                    </tr>

                    <tr>
                        <th>Place of Death</th>
                        <td><asp:Label ID="lblPlace" runat="server" /></td>
                        <th>Cause of Death</th>
                        <td><asp:Label ID="lblCause" runat="server" /></td>
                    </tr>

                    <tr>
                        <th>Aadhaar</th>
                        <td colspan="3"><asp:Label ID="lblCAadhaar" runat="server" /></td>
                    </tr>
                </table>

                <!-- WITNESS -->
                <h3 class="mt-4 mb-3"><b>Witness Details</b></h3>

                <table class="table table-bordered">
                    <tr class="table-secondary">
                        <th>Field</th>
                        <th>Witness 1</th>
                        <th>Next of Kin</th>
                    </tr>

                    <tr>
                        <th>Name</th>
                        <td><asp:Label ID="lblCW1Name" runat="server" /></td>
                        <td><asp:Label ID="lblCW2Name" runat="server" /></td>
                    </tr>

                    <tr>
                        <th>Address</th>
                        <td><asp:Label ID="lblCW1Address" runat="server" /></td>
                        <td><asp:Label ID="lblCW2Address" runat="server" /></td>
                    </tr>

                    <tr>
                        <th>Relationship</th>
                        <td><asp:Label ID="lblCW1Relation" runat="server" /></td>
                        <td><asp:Label ID="lblCW2Relation" runat="server" /></td>
                    </tr>

                    <tr>
                        <th>Mobile</th>
                        <td><asp:Label ID="lblCW1Mobile" runat="server" /></td>
                        <td><asp:Label ID="lblCW2Mobile" runat="server" /></td>
                    </tr>

                    <tr>
                        <th>Aadhaar</th>
                        <td><asp:Label ID="lblCW1Aadhaar" runat="server" /></td>
                        <td><asp:Label ID="lblCW2Aadhaar" runat="server" /></td>
                    </tr>
                      <tr>
                      <th>Aadhaar Download</th>
                      <td><asp:HyperLink ID="hlCW1Aadhaar" runat="server" Target="_blank" /></td>
                      <td><asp:HyperLink ID="hlCW2Aadhaar" runat="server" Target="_blank" /></td>
                  </tr>
                </table>
                <h3 class="mt-4 mb-3"><b>Documents</b></h3>

<table class="table table-bordered">
    <tr class="table-secondary">
        <th>Document</th>
        <th>Download</th>
    </tr>

    <tr>
        <td>Death Certificate</td>
        <td><asp:HyperLink ID="lnkDeath" runat="server" Target="_blank" Text="View" /></td>
    </tr>

    <tr>
        <td>ID Proof</td>
        <td><asp:HyperLink ID="lnkID" runat="server" Target="_blank" Text="View" /></td>
    </tr>

    <tr>
        <td>Photograph</td>
        <td><asp:HyperLink ID="lnkPhoto" runat="server" Target="_blank" Text="View" /></td>
    </tr>

    <tr>
        <td>Police Intimation</td>
        <td><asp:HyperLink ID="lnkPolice" runat="server" Target="_blank" Text="View" /></td>
    </tr>
     <tr>
     <td>Volontary</td>
     <td><asp:HyperLink ID="lnkVolontaryBodyDonation" runat="server" Target="_blank" Text="View" /></td>
 </tr>

 <tr>
     <td>Thanks Letter</td>
     <td><asp:HyperLink ID="lnkThanksLetter" runat="server" Target="_blank" Text="View" /></td>
 </tr>
</table>
            </div>
        </div>
    </div>
</div>
</asp:Content>