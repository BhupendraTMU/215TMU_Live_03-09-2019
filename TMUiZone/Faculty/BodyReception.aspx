<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master"
    AutoEventWireup="true" CodeFile="BodyReception.aspx.cs" Inherits="Faculty_BodyReception" %>

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
            Text="Body Reception" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
            </td></tr> 

            <tr> <td style="height:13px"> </td></tr>


        <tr> <td class="leftm">  </td></tr>

        <tr> <td style="height:13px"> </td></tr>

        <tr> <td align="center">
            <table cellpadding="0px" cellspacing="0px"> 
                <tr>
                    <td>Cadaver Code/Donor Code/Aadhaar Card/Name</td> 
                        <td style="width:10px"> </td>
                    <td> 
                        <asp:TextBox ID="txtDonorNo" runat="server" Height="29px" ValidationGroup="g11"></asp:TextBox>
                    </td>
                    <td style="width:10px"> </td>
                    <td> 
                        <asp:Button ID="btnGet" runat="server" Text="Search" OnClick="btnSearch_Click" ValidationGroup="g12"/>

                    </td>
                      <td style="width:630px"> </td>
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
        <asp:BoundField DataField="CadaverCode" HeaderText="Cadaver Code" >
    <HeaderStyle Width="100px" />
    <ItemStyle Width="100px" />
</asp:BoundField>
        <asp:BoundField DataField="DonorId" HeaderText="Donor Code" />
        <asp:BoundField DataField="Name" HeaderText="Name" />
        <asp:BoundField DataField="Gender" HeaderText="Gender" />

        <asp:BoundField DataField="DateOfDeath"
            HeaderText="Death Date"
            DataFormatString="{0:dd-MM-yyyy}" />
         <asp:BoundField DataField="AadhaarNumber" HeaderText="Aadhaar Card" />
        <asp:BoundField DataField="ArrivalDateTime" HeaderText="Arrival Date Time" />
        <asp:BoundField DataField="SourceName" HeaderText="Source" />
        <asp:BoundField DataField="IdentificationDetails" HeaderText="Identification Details" />
        <asp:BoundField DataField="WitnessAadhaar" HeaderText="Witness Aadhaar" />
        <asp:BoundField DataField="StatusName" HeaderText="Status" />
           <asp:BoundField DataField="ConditionName" HeaderText="Condition">
    <HeaderStyle Width="75px" />
    <ItemStyle Width="75px" />
</asp:BoundField>
        <asp:TemplateField HeaderText="Documents">
            <HeaderStyle Width="160px" />
    <ItemStyle Width="160px" />
            <ItemTemplate>
                <asp:HyperLink ID="lnkDeathDoc" runat="server" Target="_blank" Text="Death" /><br />
                <asp:HyperLink ID="lnkIDDoc" runat="server" Target="_blank" Text="ID" /><br />
                <asp:HyperLink ID="lnkPhotoDoc" runat="server" Target="_blank" Text="Photo" /><br />
                <asp:HyperLink ID="lnkPoliceDoc" runat="server" Target="_blank" Text="Police" /><br />
                <asp:HyperLink ID="lnkVolontaryBodyDonationDoc" runat="server" Target="_blank" Text="Volontary" /><br />
                <asp:HyperLink ID="lnkThanksLetterDoc" runat="server" Target="_blank" Text="Thanks Letter" />
            </ItemTemplate>
</asp:TemplateField>
         <asp:TemplateField HeaderText="Condition">
     <ItemTemplate>
         <asp:LinkButton ID="btnEnd" runat="server"
     Text="Update"
     CommandName="OpenEndModal"
     CommandArgument='<%# Eval("CadaverId") %>'
     CssClass="btn btn-sm btn-primary" style="color: white;" />
             </ItemTemplate>
</asp:TemplateField>
                 <asp:TemplateField HeaderText="Final Disposal">
     <ItemTemplate>
         <asp:LinkButton ID="btnFinalDisposal" runat="server"
     Text="Dispose"
     CommandName="OpenFinalDisposalModal"
     CommandArgument='<%# Eval("CadaverId") %>'
     CssClass="btn btn-sm btn-primary" style="color: white;" />
             </ItemTemplate>
</asp:TemplateField>
        <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <asp:LinkButton ID="btnView" runat="server"
                    Text="View"
                    CommandName="ViewCadaver"
                    CommandArgument='<%# Eval("CadaverId") %>'
                    CssClass="btn btn-sm btn-primary" style="color: white;" />      
            </ItemTemplate>
        </asp:TemplateField>

    </Columns>
</asp:GridView>

                     </td></tr>

                <tr> <td style="height:90px"> </td></tr>
    </table>
<div class="modal fade" id="endModal" tabindex="-1">
    <div class="modal-dialog">
        <div class="modal-content">

              <div class="modal-header text-white" style="background-color:#e6812b;">
                  <h5 class="modal-title">Condition Update</h5>
                  <button type="button" class="close text-white" data-dismiss="modal" style="margin-top: -20px;">
                      <span>&times;</span>
                  </button>
              </div>

            <div class="modal-body" >

                <asp:HiddenField ID="hfCadaverId" runat="server" />

                <label>Condition</label>
                <asp:DropDownList ID="ddlCondition" runat="server" CssClass="form-control"></asp:DropDownList>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
ControlToValidate="ddlCondition"
ErrorMessage="Condition is required"
ForeColor="Red"
Display="Dynamic"
ValidationGroup="saveCondition" />
                <br />

                <label>Remarks</label>
                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
ControlToValidate="txtRemarks"
ErrorMessage="Remarks is required"
ForeColor="Red"
Display="Dynamic"
ValidationGroup="saveCondition" />
            </div>

            <div class="modal-footer">
                <asp:Button ID="btnConfirmEnd" runat="server"
                    Text="Confirm End"
                    CssClass="btn btn-danger" ValidationGroup="saveCondition"
                    OnClick="btnConfirmEnd_Click" />

                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
            </div>

        </div>
    </div>
</div>
<div class="modal fade" id="cadaverViewModal" tabindex="-1">
       <div class="modal-dialog modal-lg">
        <div class="modal-content">

            <div class="modal-header text-white" style="background-color:#e6812b;">
                <h5 class="modal-title">Cadaver Full Details</h5>
                <button type="button" class="close text-white" data-dismiss="modal" style="margin-top: -20px;">
                    <span>&times;</span>
                </button>
            </div>

            <div class="modal-body" style="max-height: 553px;overflow: auto;">
                <asp:Panel ID="pnlDonor" runat="server" Visible="false">
                <!-- ================= DONOR ================= -->
                <h3><b>DONOR DETAILS</b></h3>

                <div class="row">

                    <!-- LEFT → DONOR WITNESS -->
                      <div class="col-md-6">
      <h5><b>Details</b></h5>
      <table class="table table-bordered">
          <tr>
              <th>Name</th>
              <td><asp:Label ID="lblName" runat="server" /></td>
          </tr>

          <tr>
              <th>Father</th>
              <td><asp:Label ID="lblFather" runat="server" /></td>
          </tr>

          <tr>
              <th>DOB</th>
              <td><asp:Label ID="lblDOB" runat="server" /></td>
          </tr>

          <tr>
              <th>Age / Gender</th>
              <td>
                  <asp:Label ID="lblAge" runat="server" /> /
                  <asp:Label ID="lblGender" runat="server" />
              </td>
          </tr>

          <tr>
              <th>Mobile</th>
              <td><asp:Label ID="lblMobile" runat="server" /></td>
          </tr>

          <tr>
              <th>Email</th>
              <td><asp:Label ID="lblEmail" runat="server" /></td>
          </tr>

          <tr>
              <th>Religion</th>
              <td><asp:Label ID="lblReligion" runat="server" /></td>
          </tr>

          <tr>
              <th>Aadhaar</th>
              <td><asp:Label ID="lblAadhaar" runat="server" /></td>
          </tr>

          <tr>
              <th>Address</th>
              <td><asp:Label ID="lblAddress" runat="server" /></td>
          </tr>
      </table>
  </div>

                    <!-- RIGHT → DONOR DETAILS -->
                  
                    <div class="col-md-6">
                    <h5><b>Witness</b></h5>
                    <table class="table table-bordered">
                        <tr><th></th><th>W1</th><th>W2</th></tr>

                        <tr>
                            <th>Name</th>
                            <td><asp:Label ID="lblDW1Name" runat="server" /></td>
                            <td><asp:Label ID="lblDW2Name" runat="server" /></td>
                        </tr>

                        <tr>
                            <th>Address</th>
                            <td><asp:Label ID="lblDW1Address" runat="server" /></td>
                            <td><asp:Label ID="lblDW2Address" runat="server" /></td>
                        </tr>

                        <tr>
                            <th>Relation</th>
                            <td><asp:Label ID="lblDW1Relation" runat="server" /></td>
                            <td><asp:Label ID="lblDW2Relation" runat="server" /></td>
                        </tr>

                        <tr>
                            <th>Mobile</th>
                            <td><asp:Label ID="lblDW1Mobile" runat="server" /></td>
                            <td><asp:Label ID="lblDW2Mobile" runat="server" /></td>
                        </tr>
                      
                    </table>

                     
</div>

                </div>
                </asp:Panel>
                <hr />

                <!-- ================= CADAVER ================= -->
                <h3><b>CADAVER DETAILS</b></h3>

                <div class="row">

                    <!-- LEFT → CADAVER WITNESS -->
                    <div class="col-md-6">
    <h5><b>Details</b></h5>
    <table class="table table-bordered">
        <tr>
            <th>Name</th>
            <td><asp:Label ID="lblCadaverName" runat="server" /></td>
        </tr>

        <tr>
            <th>Age / Gender</th>
            <td>
                <asp:Label ID="lblCadaverAge" runat="server" /> /
                <asp:Label ID="lblCadaverGender" runat="server" />
            </td>
        </tr>

        <tr>
            <th>Aadhaar</th>
            <td><asp:Label ID="lblCadaverAadhaar" runat="server" /></td>
        </tr>

        <tr>
            <th>Date Of Death</th>
            <td><asp:Label ID="lblDOD" runat="server" /></td>
        </tr>

        <tr>
            <th>Place Of Death</th>
            <td><asp:Label ID="lblPlace" runat="server" /></td>
        </tr>
    </table>
</div>
                   

                    <!-- RIGHT → CADAVER DETAILS -->
              <div class="col-md-6">
     <h5><b>Witness</b></h5>
     <table class="table table-bordered">
         <tr><th></th><th>W1</th><th>W2</th></tr>

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
             <th>Relation</th>
             <td><asp:Label ID="lblCW1Relation" runat="server" /></td>
             <td><asp:Label ID="lblCW2Relation" runat="server" /></td>
         </tr>

         <tr>
             <th>Mobile</th>
             <td><asp:Label ID="lblCW1Mobile" runat="server" /></td>
             <td><asp:Label ID="lblCW2Mobile" runat="server" /></td>
         </tr>
             <tr>
                <th>Adhar Card</th>
               <td><asp:Label ID="lblCW1Aadhaar" runat="server" /></td>
               <td><asp:Label ID="lblCW2Aadhaar" runat="server" /></td>
            </tr>
                                    <tr>
                <th>Adhar Card Download</th>
                <td><asp:HyperLink ID="hlCW1Aadhaar" runat="server" Target="_blank" Style="color:#FF9900" /></td>
                <td><asp:HyperLink ID="hlCW2Aadhaar" runat="server" Target="_blank" Style="color:#FF9900" /></td>
            </tr>
     </table>
 </div>       

                </div>

                <hr />
                                <!-- ================= Documents ================= -->
                <h3><b>DOCUMENT DETAILS</b></h3>

                <div class="row">

                    <!--  Documents WITNESS -->
                    <div class="col-md-12">
                    <h5><b>Details</b></h5>
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

                <hr />
                <!-- STORAGE -->
                <h3><b>STORAGE HISTORY</b></h3>

                <asp:GridView ID="grdStorageHistory" runat="server"
                    CssClass="table table-bordered"
                    AutoGenerateColumns="false">

                    <Columns>
                        <asp:BoundField DataField="RoomNumber" HeaderText="Room" />
                        <asp:BoundField DataField="FreezerNumber" HeaderText="Freezer" />
                        <asp:BoundField DataField="RackNumber" HeaderText="Rack" />
                        <asp:BoundField DataField="AllocatedAt" HeaderText="Allocated" />
                        <asp:BoundField DataField="ReleasedAt" HeaderText="Released" />
                    </Columns>

                </asp:GridView>
                 <hr />
 <!-- STORAGE -->
 <h3><b>CADAVER CONDITION</b></h3>

 <asp:GridView ID="grdCadaverCondition" runat="server"
     CssClass="table table-bordered"
     AutoGenerateColumns="false">

     <Columns>
         <asp:BoundField DataField="ConditionName" HeaderText="Condition" />
         <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
         <asp:BoundField DataField="UpdatedAt" HeaderText="Updated At" />
     </Columns>

 </asp:GridView>

<hr />
<!-- Department -->
<h3><b>Department</b></h3>

<asp:GridView ID="grdDepartment" runat="server"
    CssClass="table table-bordered"
    AutoGenerateColumns="false">

    <Columns>
        <asp:BoundField DataField="DonorRegNo" HeaderText="Donor Code" />
        <asp:BoundField DataField="CadaverCode" HeaderText="Cadaver Code" />
        <asp:BoundField DataField="Name" HeaderText="Name" />
        <asp:BoundField DataField="Gender" HeaderText="Gender" />
        <asp:BoundField DataField="DateOfDeath" HeaderText="Date Of Death"  DataFormatString="{0:dd-MM-yyyy}" />
        <asp:BoundField DataField="PlaceOfDeath" HeaderText="Place Of Death" /> 
        <asp:BoundField DataField="CauseOfDeath" HeaderText="Cause Of Death" />
        <asp:BoundField DataField="Department" HeaderText="Department" />
        <asp:BoundField DataField="Batch" HeaderText="Batch" />
        <asp:BoundField DataField="DissectionHall" HeaderText="DissectionHall" />
        <asp:BoundField DataField="InChargeName" HeaderText="InChargeName" />
        <asp:BoundField DataField="StartDate" HeaderText="Start Session" />
         <asp:BoundField DataField="EndDate" HeaderText="End Session" />
    </Columns>

</asp:GridView>


            </div>
        </div>
    </div>
</div>

   <div class="modal fade" id="FinalDisposalModal" tabindex="-1">
    <div class="modal-dialog">
        <div class="modal-content">

            <div class="modal-header text-white" style="background-color:#dc3545;">
                <h5 class="modal-title">Final Disposal</h5>
                <button type="button" class="close text-white" data-dismiss="modal" style="margin-top: -20px;">
                    <span>&times;</span>
                </button>
            </div>

            <div class="modal-body">

                <asp:HiddenField ID="hfDisposeCadaverId" runat="server" />

                <!-- Disposal Date -->
                <label>Disposal Date</label>
                <asp:TextBox ID="txtDisposalDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
ControlToValidate="txtDisposalDate"
ErrorMessage="Disposal is required"
ForeColor="Red"
Display="Dynamic"
ValidationGroup="save" />
                <br />

                <!-- Method -->
                <label>Method</label>
                <asp:DropDownList ID="ddlMethod" runat="server" CssClass="form-control">
                    <asp:ListItem Value="">--Select Method--</asp:ListItem>
                    <asp:ListItem Value="Cremation">Cremation</asp:ListItem>
                    <asp:ListItem Value="Burial">Burial</asp:ListItem>
                </asp:DropDownList>
  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
ControlToValidate="ddlMethod"
ErrorMessage="Method is required"
ForeColor="Red"
Display="Dynamic"
ValidationGroup="save" />
                <br />

                <!-- Approved By -->
                <label>Approved By</label>
                <asp:TextBox ID="txtApprovedBy" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
ControlToValidate="txtApprovedBy"
ErrorMessage="Approved By is required"
ForeColor="Red"
Display="Dynamic"
ValidationGroup="save" />
                <br />

                <!-- Photo Upload -->
                <label>Upload Photo</label>
                <asp:FileUpload ID="fuDisposalPhoto" runat="server" CssClass="form-control" />

            </div>

            <div class="modal-footer">
                <asp:Button ID="btnDispose" runat="server"
                    Text="Save Disposal"
                    CssClass="btn btn-danger" ValidationGroup="save"
                    OnClick="btnDispose_Click" />

                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
            </div>

        </div>
    </div>
</div>
</asp:Content>