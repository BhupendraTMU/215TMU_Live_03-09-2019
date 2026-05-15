<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="managementvehicleapproval.aspx.cs" Inherits="Faculty_managementvehicleapproval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style>
        .rbl input[type="radio"] {
            margin-left: 10px;
            margin-right: 1px;
        }

        .GridHeader
       {
    text-align:center !important;   
          }
         </style>
        <script type="text/javascript">

            function preventBackspace(e) {
                var evt = e || window.event;
                if (evt) {
                    var keyCode = evt.charCode || evt.keyCode;
                    if (keyCode === 8) {
                        if (evt.preventDefault) {
                            evt.preventDefault();
                        } else {
                            evt.returnValue = false;
                        }
                    }
                }
            }
            
            
            
            
            </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:ScriptManager ID="ty" runat="server"></asp:ScriptManager>
  
     <fieldset class="boxBodyInner">
                <div class="row tmu-form ml-12 mr-5">
                    <div class="col-sm-4 p-0">
                        <p style="color: black; font-size: 20px">Transport Vehicle Approval</p>
                    </div>
                 
                    <div class="col-sm-8 p-0">
                        <asp:RadioButtonList ID="Rblist" runat="server"  AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="Rblist_SelectedIndexChanged" CssClass="rbl">
                           
                            <asp:ListItem Value="9" Text="Pending" Selected="True"></asp:ListItem>                            
                            <asp:ListItem Value="5" Text="Approved by Management"></asp:ListItem> 
                          <asp:ListItem Value="6" Text="Rejected by Management"></asp:ListItem>                            
                        </asp:RadioButtonList>
                    </div>


                </div>
            </fieldset>
      <br />
            <fieldset class="boxBodyInner">

                    <div class="col-sm-12 p-0">
                    <div class="col-sm-4 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label">From Date</label>
                            <div class="col-sm-10">
                                 <asp:TextBox runat="server" ID="txtFromtDate" CssClass="form-control input-sm" OnTextChanged="txtFromtDate_TextChanged" AutoPostBack="true" Width="120px" autocomplete="off" ToolTip="From Date" onkeypress="return false"
                                       onKeyDown="preventBackspace();" placeholder="From Date"></asp:TextBox>
                                <asp:Image src="../Images/Calendar.png" runat="server" Height="30px" Width="30px" alt="" ID="fdate" />
                                   <asp:CalendarExtender ID="cleFromDate" Format="dd MMM yyyy" runat="server"  CssClass="cal_Theme1"
                                       PopupButtonID="fdate" Enabled="true" TargetControlID="txtFromtDate" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label">To Date</label>
                            <div class="col-sm-10">
                                   <asp:TextBox runat="server" ID="txtToDate" CssClass="form-control input-sm" Width="120px" OnTextChanged="txtToDate_TextChanged" AutoPostBack="true" autocomplete="off" ToolTip="To Date" onkeypress="return false"
                                   onKeyDown="preventBackspace();" placeholder="To Date"></asp:TextBox>
                                <asp:Image src="../Images/Calendar.png" runat="server" Height="30px" Width="30px" alt="" ID="tdate" />
                               <asp:CalendarExtender ID="cleToDate" Format="dd MMM yyyy" runat="server" CssClass="cal_Theme1"
                                   PopupButtonID="tdate" Enabled="true"  TargetControlID="txtToDate" />
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-4 p-0">
                        <asp:Button ID="BtnShow" runat="server" Text="Show" CssClass="btn" />
                        <asp:Button ID="BtnSubmit" runat="server" Text="Approve"  OnClick="BtnSubmit_Click"  CssClass="btn" />                        
                        <asp:Button ID="BtnReject" runat="server"  Text="Reject" OnClick="BtnReject_Click" CssClass="btn" />

                    </div>

                </div>


                <br />


                            <div id="confirmModalB" class="modal fade confirm-modal" role="dialog">

                <div class="modal-dialog modalPopup border-box">
                    <div class="modal-content">
                        <div class="modal-header">
                       <button type="button" name="btn_close" id="Button1" class="close" data-dismiss="modal">&times;</button>

                        </div>
                        <div class="clearfix">
                            <div class="col-sm-12">
                                <div style="text-align:center">Are you sure</div>
                                <asp:Panel ID="PnlMain" runat="server">
                                    <div class="modal-footer">
                                        <asp:Button ID="BtnYes" runat="server" OnClick="BtnYes_Click" Text="Yes"
                                            UseSubmitBehavior="false" data-dismiss="modal" class="btn btn-success" />
                                        <button type="button" class="btn btn-danger" data-dismiss="modal">No</button>
                                    </div>

                                </asp:Panel>
                            </div>
                        </div>

                        </div>
                    </div>
                                </div>

                 <div id="confirmModalR" class="modal fade confirm-modal" role="dialog">

                <div class="modal-dialog modalPopup border-box">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" name="btn_close" id="Button2" class="close" data-dismiss="modal">&times;</button>

                        </div>
                        <div class="clearfix">
                            <div class="col-sm-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="col-md-12 p-0">
                                        <div class="col-sm-4 col-md-3">
                                            <label>Remarks</label>
                                        </div>
                                        <div class="col-sm-8 col-md-9 form-group">
                                            <asp:TextBox ID="txtRemarks" CssClass="form-control" MaxLength="30" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button ID="BtnRYes" runat="server" Text="Yes" OnClick="BtnRYes_Click" 
                                            UseSubmitBehavior="false" data-dismiss="modal" class="btn btn-success" />
                                        <button type="button" class="btn btn-danger" data-dismiss="modal">No</button>
                                    </div>


                                </asp:Panel>
                            </div>
                        </div>
                        </div>
                    </div>
                    </div>


                </fieldset>
        
    <fieldset class="boxBodyInner">
      <asp:GridView ID="GrdTransport" runat="server" CssClass="table table-striped table-bordered table-hover"  Style="width: 95%; margin-left: 2%; margin-right: 2%" EmptyDataText="No Data to display" AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr.no">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex +1 %>


                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                  <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true"  OnCheckedChanged="chkAll_CheckedChanged" />
                                        Select All
                                    </HeaderTemplate>
                                    <ItemTemplate>

                           <asp:CheckBox ID="chkDate" runat="server" AutoPostBack="true" OnCheckedChanged="chkDate_CheckedChanged"  />
                                        <asp:HiddenField ID="hdnStatus" runat="server" Value='<%# Eval("[Line No_]") %>' />
                                            <asp:HiddenField ID="JDate" runat="server" Value='<%# Eval("[Journey Date]","{0:dd MMM yyyy}") %>' />

                                        <asp:HiddenField ID="hdMoble" runat="server" Value='<%# Eval("[Mobile No]") %>' />
                                         <asp:HiddenField ID="hdReqNo" runat="server" Value='<%# Eval("[Requisition No]") %>' />
                                        <asp:HiddenField ID="hdDest" runat="server" Value='<%# Eval("[Destination]") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Attachment" ItemStyle-HorizontalAlign="Left">
                          <ItemTemplate>
                              <asp:Label ID="lblleaveAttachmentFilename" runat="server" Text='<%#Bind("AttachmentFilename") %>' Visible="false"></asp:Label>
                              <asp:LinkButton ID="lnkDownloadgrid" runat="server" Text='<%#Bind("Adownload") %>'  OnClick="lnkDownloadgrid_Click"
                    CommandArgument='<%# Eval("AutoNo") %>'></asp:LinkButton>
                           <%--   <asp:Button ID="btnViewAttachment" runat="server" CommandArgument='<%#Bind("AutoNo") %>' OnCommand="btnViewAttachment_Command" Text='<%# Eval("Upload") %>' />--%>
                              </div>
                          </ItemTemplate>
                          <HeaderStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Left" />
                      </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Requisition No" ItemStyle-HorizontalAlign="Left">
                          <ItemTemplate>
                              <asp:Label ID="lbllRequisition" runat="server" Text='<%#Bind("[Requisition No]") %>'></asp:Label>
                              
                          </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>

                 <asp:TemplateField HeaderText="Destination" ItemStyle-HorizontalAlign="Left">
                          <ItemTemplate>
                              <asp:Label ID="lbllDestination" runat="server" Text='<%#Bind("[Destination]") %>'></asp:Label>
                              
                          </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>

          <asp:TemplateField HeaderText="Route Distance" ItemStyle-HorizontalAlign="Left">
                          <ItemTemplate>
                              <asp:Label ID="lbllRouteDistance" runat="server" Text='<%#Bind("[Route Distance]") %>'></asp:Label>
                              
                          </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>


           <asp:TemplateField HeaderText="Type of Vehicle" ItemStyle-HorizontalAlign="Left">
                          <ItemTemplate>
                              <asp:Label ID="lbllTypeofVehicle" runat="server" Text='<%#Bind("[Type of Vehicle]") %>'></asp:Label>
                              
                          </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>

               <asp:TemplateField HeaderText="No. of Passengers" ItemStyle-HorizontalAlign="Left">
                          <ItemTemplate>
                              <asp:Label ID="lbllNoPassengers" runat="server" Text='<%#Bind("[No of Passengers]") %>'></asp:Label>
                              
                          </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
             <asp:TemplateField HeaderText="Journey Date" ItemStyle-HorizontalAlign="Left">
                          <ItemTemplate>
                              <asp:Label ID="lbllJourneyDate" runat="server" Text='<%#Bind("[Journey Date]") %>'></asp:Label>
                              
                          </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
    <asp:TemplateField HeaderText="To Date" ItemStyle-HorizontalAlign="Left">
                          <ItemTemplate>
                              <asp:Label ID="lbllToDate" runat="server" Text='<%#Bind("[To Date]") %>'></asp:Label>
                              
                          </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>

                                    <asp:TemplateField HeaderText="From Time" ItemStyle-HorizontalAlign="Left">
                          <ItemTemplate>
                              <asp:Label ID="lbllFromTime" runat="server" Text='<%#Bind("[From Time]") %>'></asp:Label>
                              
                          </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>

                         <asp:TemplateField HeaderText="Required at Place" ItemStyle-HorizontalAlign="Left">
                          <ItemTemplate>
                              <asp:Label ID="lbllPlace" runat="server" Text='<%#Bind("[Required at Place]") %>'></asp:Label>
                              
                          </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>



                                
                   

                                  <asp:TemplateField HeaderText="Purpose" ItemStyle-HorizontalAlign="Left">
                          <ItemTemplate>
                              <asp:Label ID="lbllPurpose" runat="server" Text='<%#Bind("[Purpose]") %>'></asp:Label>
                              
                          </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="Left">
                          <ItemTemplate>
                              <asp:Label ID="lbllName" runat="server" Text='<%#Bind("[Name]") %>'></asp:Label>
                              
                          </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Mobile No" ItemStyle-HorizontalAlign="Left">
                          <ItemTemplate>
                              <asp:Label ID="lbllMobileNo" runat="server" Text='<%#Bind("[Mobile No]") %>'></asp:Label>
                              
                          </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Department" ItemStyle-HorizontalAlign="Left">
                          <ItemTemplate>
                              <asp:Label ID="lblDepartment" runat="server" Text='<%#Bind("[Department]") %>'></asp:Label>
                              
                          </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left">
                          <ItemTemplate>
                              <asp:Label ID="lbllRemarks" runat="server" Text='<%#Bind("[Remarks]") %>'></asp:Label>
                              
                          </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Portal status" ItemStyle-HorizontalAlign="Left">
                          <ItemTemplate>
                              <asp:Label ID="lbllPortalstatus" runat="server" Text='<%#Bind("[Portal status]") %>'></asp:Label>
                              
                          </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
        </fieldset>
</asp:Content>

