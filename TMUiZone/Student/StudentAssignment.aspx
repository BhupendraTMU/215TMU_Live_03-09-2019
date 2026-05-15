<%@ Page Title="" Language="C#" MasterPageFile="~/Student/IndexMaster.master" AutoEventWireup="true"  CodeFile="StudentAssignment.aspx.cs" Inherits="Student_StudentAssignment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function fileUpload1() {
            if ($('[id$=FileUpload1]').val() != '') {
                $('.progress-bar').width(100 + '%');
                $('[id$=progress]').removeClass('hidden');
            }
            else
                return false;
            $(document).ready(function () {
                debugger
                var query = window.location.search.substring(1);
                
            });
        }
 </script>
    <script>
        function CalljavascriptFunction() {
            var url = "MyDialogPage.aspx";
            var retval = window.showModalDialog(url, window, "center:yes;dialogWidth:800px;dialogHeight:600px");
        }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">
 <asp:Label ID="Label3" runat="server" 
            Text="Assignment" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
 </fieldset>
 <fieldset class="boxBodyHeader"> 
  
 </fieldset>
<fieldset  style="background:#fefefe; border-top:1px solid #dde0e8; border-bottom:1px solid #dde0e8; padding:10px 20px; height:100%">
            <br />
         <asp:UpdatePanel runat="server">
             <ContentTemplate>
                 <asp:GridView ID="grdAssignmentReport" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="866px" GridLines="Horizontal"  OnPageIndexChanging="grdAssignmentReport_PageIndexChanging"  EmptyDataText="There are no data records to display." AllowPaging="true" PageSize="10">
               <AlternatingRowStyle BackColor="#F7F7F7" />
                        <Columns>                            
                            <asp:BoundField DataField="FacultyName" HeaderText="Faculty Name" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="SubjectDescription" HeaderText="Subject Description" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="DueDate1" HeaderText="Due Date" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="CloseDate" HeaderText="Close Date" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="AssignmentStatus" HeaderText="Status" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                             <asp:TemplateField>
                            <ItemTemplate>
                                 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
             <ContentTemplate>
                                <asp:LinkButton ID="lnkDownload" Text = "Download"  CommandArgument = '<%# Eval("AssignmentNo_") %>' runat="server" OnClick = "DownloadFile"></asp:LinkButton>
                 </ContentTemplate>
                <Triggers>
                 <asp:PostBackTrigger ControlID="lnkDownload"/>
                 </Triggers>
                                 </asp:UpdatePanel>
                            </ItemTemplate>                                 
                        </asp:TemplateField>
                            <asp:TemplateField>
                                 <ItemTemplate>
                                <asp:LinkButton ID="lnkReply" Text = "Upload"   CommandArgument = '<%# Eval("AssignmentNo_") %>' runat="server" OnClick = "Reply"></asp:LinkButton>
                            <asp:HiddenField ID="hfCheckenable" runat="server" Value='<%#Eval("ChkEnable")%>' />
                                 </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>                       
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
               <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
               <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
               <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont"   />
               <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
               <SortedAscendingCellStyle BackColor="#F4F4FD" />
               <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
               <SortedDescendingCellStyle BackColor="#D8D8F0" />
               <SortedDescendingHeaderStyle BackColor="#3E3277" />
                    </asp:GridView>
                 
           <div class="modal fade" id="myModal2">
            <div class="modal-dialog" style="width:400px;height:100px">
              <div class="modal-content">
                <div class="modal-header" style="background-color:#88CCFF;">
                    <div>
                          <button type="button" class="close" data-dismiss="modal">&times;</button>
                          <h4 class="modal-title"><b style="font-family:Arial; font-size:15px; font-weight:bold">Upload Assignment</b></h4>
                     </div>
                </div>
                <div class="modal-body">
                            <br />
                            <center>
                             <asp:FileUpload ID="FileUpload1" CssClass="form-control input-sm" runat="server" />
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please choose the File!" Display="Dynamic" ForeColor="Red" ValidationGroup="g1" ControlToValidate="FileUpload1"></asp:RequiredFieldValidator>
                           <br />
                                </center>                                           
                </div>
                <div class="modal-footer" style="border-top-width:0px">
                  <asp:Button runat="server" ID="btnUpload"   CssClass="btn-sm btn-primary"  Width="20%"  OnClick="Submit" Text="Upload"></asp:Button>
                </div>
              </div>
      
            </div>
           </div>
             </ContentTemplate>
             <Triggers>
                 <asp:PostBackTrigger ControlID="btnUpload"/>
             </Triggers>
         </asp:UpdatePanel>   
        
                <br />   

    </fieldset>

</asp:Content>

