<%@ Page Title="" Language="C#" MasterPageFile="~/Student/IndexMaster.master" AutoEventWireup="true" CodeFile="~/Student/PlacementRegistration.aspx.cs" Inherits="PlacementRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <fieldset class="boxBody">
 <asp:Label ID="Label3" runat="server" 
            Text="Placement Registration" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
 </fieldset>
 <fieldset class="boxBodyHeader"> 
  
 </fieldset>

    <fieldset style="background: #fefefe; border-top: 1px solid #dde0e8; border-bottom: 1px solid #dde0e8; padding: 10px 20px; height: 100%">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
     <asp:UpdatePanel ID="upnlWithdrawal" runat="server">
        <ContentTemplate>
    <div class="tab-pane active">
        
        <div class="clearfix border-between">
            <div class="col-sm-12 padding-tb">
                <div class="table table-responsive">
                    <asp:GridView ID="grdplacementform" runat="server" EmptyDataText="There are no data records to display."
                        AllowPaging="false" HorizontalAlign="Center"  OnRowDataBound="grdplacementform_RowDataBound"  AutoGenerateColumns="false" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" 
                         GridLines="Both">
                         <AlternatingRowStyle BackColor="#F7F7F7" />
                         <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                         <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont"   />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkbox" runat="server" OnCheckedChanged="chkbox_CheckedChanged" 
                                        Checked='<%# Bind ("[Registered]") %>' Enabled='<%# Bind ("[EnableDisable]") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Company Name">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfScheduleNo" runat="server" Value='<%# Bind ("[Schedule No_]") %>'></asp:HiddenField>
                                    <asp:Label ID="lblcompanyname" runat="server" Text='<%# Bind("[Company Name]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Job Description">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtnJobDesciption"  runat="server" OnClick="lnkbtnJobDesciption_Click" Text='<%# Bind("[Job description]") %>'></asp:LinkButton>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Registration From">
                                <ItemTemplate>
                                    <asp:Label ID="lblregsd" runat="server" Text='<%# Bind("[Registration Start Date]") %>'></asp:Label>
                                    <asp:HiddenField ID="hfcompanyCode" runat="server" Value='<%# Bind("[Company Code]") %>' />

                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Registration To">
                                <ItemTemplate>
                                    <asp:Label ID="lblreged" runat="server" Text='<%# Bind("[Registration End Date]") %>'></asp:Label>
                                   

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Drive Start Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblFromdate" runat="server" Text='<%# Bind("[From Date]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Drive End Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbltodate" runat="server" Text='<%# Bind("[To Date]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start Time">
                                <ItemTemplate>
                                    <asp:Label ID="lblFromTime" runat="server" Text='<%# Bind("[From Time]") %>'></asp:Label>
                                    <asp:HiddenField ID="hftimein" runat="server" Value='<%# Bind("FromTime") %>'></asp:HiddenField>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Time">
                                <ItemTemplate>
                                    <asp:Label ID="lblToTime" runat="server" Text='<%# Bind("[To Time]") %>'></asp:Label>
                                     </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Venue">
                                <ItemTemplate>
                                    <asp:Label ID="lblVenue" runat="server" Text='<%# Bind("[Venue]") %>'></asp:Label>
                                     </ItemTemplate>
                            </asp:TemplateField>
                            
                        </Columns>
                    </asp:GridView>

                </div>
            </div>
            <div class="btn pull-right">
                <asp:Button ID="Btnsubmit" runat="server" OnClick="Btnsubmit_Click" Text="Submit" Height="35px" Width="90px" CssClass="btn-sm btn-primary btn-block" />
            </div>
        </div>

        <div id="confirmModal" class="modal fade confirm-modal" role="dialog">
                                <div class="modal-dialog modal-sm">
                                    <div class="modal-content">
                                        <div class="modal-header" style="background-color:#88CCFF;">
                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                            <h4 class="modal-title text-center">
                                                <asp:Label ID="lblCompany" runat="server">&times;</asp:Label>
                                            </h4>
                                        </div>
                                        <div class="modal-body text-left">
                                            <p>
                                                <asp:Repeater ID="dtAlertJobDescriptionMsg" runat="server">
                                                    <ItemTemplate>
                                                        <tr>                                                           
                                                            <td class="text-center"><%#Eval("RowNo")%>.</td>
                                                            <td><%#Eval("[Job Description]")%></td>
                                                             <br />
                                                        </tr>
                                                    </ItemTemplate>
                                          </asp:Repeater></p>
                                        </div>
                                        <div class="modal-footer text-center" >                                            
                                           <asp:HyperLink id="hlnk" Target="_blank" runat="server"></asp:HyperLink>
                                        </div>
                                    </div>
                                </div>
                            </div>
    </div>
           
    <script>
                    function CloseModalConfirm() {
                        $('#confirmModal').modal('hide');
                        $('body').removeClass('modal-open');
                        $('.modal-backdrop').remove();
                    };
                </script>
             </ContentTemplate></asp:UpdatePanel>
    </fieldset>
</asp:Content>

