<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master"
    AutoEventWireup="true" CodeFile="BodyStorageAllocation.aspx.cs" Inherits="Faculty_BodyStorageAllocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <table cellpadding="0px" cellspacing="0px" style="width:100%">
            <tr> <td style="height:13px"> </td></tr>
         <tr> <td>  


      &nbsp;&nbsp;&nbsp; &nbsp;  <asp:Label ID="Label3" runat="server" 
            Text="Storage Allocation" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
            </td></tr> 

            <tr> <td style="height:13px"> </td></tr>


        <tr> <td class="leftm">  </td></tr>

        <tr> <td style="height:13px"> </td></tr>

        <tr> <td align="center">
            <table cellpadding="0px" cellspacing="0px"> 
                <tr>
                    <td> Cadaver Code/Aadhaar</td> 
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
          <asp:GridView ID="grdBodyStorage" runat="server"
    AutoGenerateColumns="False" Width="100%"
    CssClass="table table-striped table-bordered"
    EmptyDataText="No Data Found"
    OnRowCommand="grdBodyStorage_RowCommand"  AllowPaging="true"
 PageSize="20"
 OnPageIndexChanging="grdBodyStorage_PageIndexChanging">

    <Columns>
        <asp:BoundField DataField="DonorIID" HeaderText="Donor Code" />
        <asp:BoundField DataField="CadaverCode" HeaderText="Cadaver Code" />
        <asp:BoundField DataField="Name" HeaderText="Name" />
        <asp:BoundField DataField="Gender" HeaderText="Gender" />
        <asp:BoundField DataField="DateOfDeath" HeaderText="Date Of Death"
            DataFormatString="{0:dd-MM-yyyy}" />
        <asp:BoundField DataField="AadhaarNumber" HeaderText="Aadhaar Card" />
        <asp:BoundField DataField="PlaceOfDeath" HeaderText="Place Of Death" /> 
        <asp:BoundField DataField="CauseOfDeath" HeaderText="Cause Of Death" />
        <asp:BoundField DataField="RoomNumber" HeaderText="Room No" />
           <asp:BoundField DataField="FreezerNumber" HeaderText="Freezer No" />
           <asp:BoundField DataField="RackNumber" HeaderText="Rack No" />
         <asp:BoundField DataField="StorageStatus" HeaderText="Storage Status" />
        
    <%--     <asp:BoundField DataField="AllocatedAt" HeaderText="Allocated At"
     DataFormatString="{0:dd-MM-yyyy}" />
  --%>        <asp:BoundField DataField="AllocatedAt" HeaderText="AllocatedAt" />
      <asp:BoundField DataField="StatusName" HeaderText="Status" />
        <asp:TemplateField HeaderText="Action">
    <ItemTemplate>
        <asp:LinkButton ID="btnView" runat="server"
            Text="View"
            CommandName="ViewCadaver"
            CommandArgument='<%# Eval("StorageId") %>'
            CssClass="btn btn-sm btn-primary" />

    </ItemTemplate>
</asp:TemplateField>
           <%-- <asp:TemplateField HeaderText="Action">
<ItemTemplate>
        <asp:LinkButton ID="btnRelease" runat="server"
            Text="Release"
            CommandName="ReleaseStorage"
            CommandArgument='<%# Eval("StorageId") + "," + Eval("CadaverId") %>'
            Visible='<%# Eval("IsActive").ToString() == "True" %>' CssClass="btn btn-sm btn-primary" />
    </ItemTemplate>
</asp:TemplateField>--%>

    </Columns>
</asp:GridView>

                     </td></tr>

                <tr> <td style="height:90px"> </td></tr>
    </table>

<!--Add MODAL -->
<div class="modal fade" id="StorageModal">
    <div class="modal-dialog modal-xl">
        <div class="modal-content">

                       <div class="modal-header text-white" style="background-color:#e6812b;color:white">
               <h5 class="mb-0">Storage Allocation</h5>
                <button type="button" class="close text-white" data-dismiss="modal" style="margin-top: -22px;">
    <span>&times;</span>
</button>
           </div>
            <div class="modal-body" >

                <!-- 🔍 SEARCH -->
                <div class="row">
                    <div class="col-md-9">
                        <label>Search (CadaverCode / Aadhaar)</label>
                        <asp:TextBox ID="txtSearchCadaver" runat="server" CssClass="form-control" />
                                                                    <asp:RequiredFieldValidator ID="rfvSearch" runat="server"
ControlToValidate="txtSearchCadaver"
ErrorMessage="Please enter Cadaver Code or Aadhaar"
ForeColor="Red"
Display="Dynamic"
ValidationGroup="search" />
                    </div>
                    <div class="col-md-3">
                        <label>&nbsp;</label>   
                        <asp:Button ID="btnSearchCadaver" runat="server"
                            Text="Search"
                            CssClass="btn btn-primary form-control"
                            OnClick="btnSearchCadaver_Click" ValidationGroup="search" />
                    </div>
                </div>

                <hr />

                <!-- 📄 DETAILS -->
                <div class="row">
                    <div class="col-md-4">
                        <label>Cadaver Code<span>*</span></label>
                        <asp:TextBox ID="txtCadaverCode" runat="server" CssClass="form-control" ReadOnly="true" />
                    </div>

                    <div class="col-md-4">
                        <label>Name<span>*</span></label>
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" ReadOnly="true" />
                    </div>

                    <div class="col-md-4">
                        <label>Aadhaar<span>*</span></label>
                        <asp:TextBox ID="txtAadhaar" runat="server" CssClass="form-control" ReadOnly="true" />
                    </div>
                </div>

                <hr />

                <!-- 🧊 STORAGE -->
                <div class="row">
                    <div class="col-md-4">
                        <label>Room<span>*</span></label>
                        <asp:TextBox ID="txtRoom" runat="server" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="rfvRoom" runat="server"
ControlToValidate="txtRoom"
ErrorMessage="Room is required"
ForeColor="Red"
Display="Dynamic"
ValidationGroup="save" />
                    </div>

                    <div class="col-md-4">
                        <label>Freezer<span>*</span></label>
                        <asp:TextBox ID="txtFreezer" runat="server" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
ControlToValidate="txtFreezer"
ErrorMessage="Freezer is required"
ForeColor="Red"
Display="Dynamic"
ValidationGroup="save" />
                    </div>

                    <div class="col-md-4">
                        <label>Rack<span>*</span></label>
                        <asp:TextBox ID="txtRack" runat="server" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
ControlToValidate="txtRack"
ErrorMessage="Rack is required"
ForeColor="Red"
Display="Dynamic"
ValidationGroup="save" />
                    </div>
                </div>

            </div>

            <div class="modal-footer">
                <asp:Button ID="btnSaveStorage" runat="server"
                    Text="Allocate Storage"
                    CssClass="btn btn-success" ValidationGroup="save" 
                    OnClick="btnSaveStorage_Click" />
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

            </div>
        </div>
    </div>
</div>
</asp:Content>