<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="EventCreation.aspx.cs" Inherits="Faculty_EventCreation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .parent {
            text-align: center;
            display: block;
            border: 1px solid outset;
        }

        .child {
            display: inline-block;
            width: 200px;
        }

        .spaced input[type="radio"]
{
   margin-left: 50px; /* Or any other value */
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
        function checkDate(sender, args) {
            if (sender._selectedDate < new Date()) {
                alert("You cannot select Less than current date!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
        function checkDateT(sender, args) {
            var today = new Date();
            today.getHours(0, 0, 0, 0);
            if ($('[id$=txtFromDate]').val() == '') {
                alertify.error('First select the from date!');
                sender._textbox.set_Value('');
                return false;
            }
            else if (sender._selectedDate < today) {
                alertify.error("You cannot select greater than current date!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value('');
            }
            else {
                var f = new Date($('[id$=txtFromDate]').val());

                if (sender._selectedDate < f) {
                    alertify.error("You cannot select less than from date!");
                    sender._textbox.set_Value('');
                }
            }
        }

    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="stud" runat="server"></asp:ScriptManager>
  <fieldset class="boxBody"> 
       <table cellpadding="0px" cellspacing="0px">
           <tr>
     <td align="left"><asp:Label ID="Label2" runat="server" 
            Text="Create Event" Font-Size="12pt" ForeColor="#093A62"  Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label></td>
               <td align="right" style="fit-position: right; padding-left:50px">
        <asp:RadioButtonList ID="rblUniversity" runat="server" RepeatDirection="Horizontal" CssClass="spaced" AutoPostBack="true" OnSelectedIndexChanged="rblUniversity_SelectedIndexChanged" >
         <asp:ListItem Value="0" Selected="True">College</asp:ListItem>
         <asp:ListItem Value="1">University</asp:ListItem>          
         </asp:RadioButtonList>
                   </td> 
 <td align="right" style="fit-position: right; padding-left:50px">
                   <asp:DropDownList runat="server" ID="drpcollege"  Width="250px" CssClass="form-control input-sm" Height="30px">
        </asp:DropDownList>             
            <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="drpcollege" Display="Dynamic" ErrorMessage="*" ForeColor="Red" ValidationGroup="sr1"></asp:RequiredFieldValidator>
               </td>
               <td align="right" style="fit-position: right; padding-left:50px">                
                    <asp:TextBox runat="server" ID="txtFromDate" CssClass="form-control input-sm" Width="100px" ToolTip="From Date" onkeypress="return false" 
                                    onKeyDown="preventBackspace();" placeholder="Event From"></asp:TextBox>&nbsp
                                <asp:Image src="../Images/Calendar.png" runat="server" Height="30px" Width="30px" alt="" ImageAlign="Right" ID="fdate" />
                               <asp:CalendarExtender ID="cleFromDate" Format="dd MMM yyyy" runat="server" OnClientDateSelectionChanged="checkDate" CssClass="cal_Theme1"
                                    PopupButtonID="fdate" Enabled="true" TargetControlID="txtFromDate" />
                    <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="sr1" Font-Bold="true" ForeColor="white"></asp:RequiredFieldValidator>                  
               </td>
              <td align="right" style="fit-position: right; padding-left:50px">
              <asp:TextBox runat="server" ID="txtToDate" CssClass="form-control input-sm" Width="100px" ToolTip="To Date" onkeypress="return false" 
                                    onKeyDown="preventBackspace();" placeholder="Event To"></asp:TextBox>&nbsp
                                <asp:Image src="../Images/Calendar.png" runat="server" Height="30px" Width="30px" alt="" ImageAlign="Right" ID="tdate" />
                               <asp:CalendarExtender ID="cleToDate" Format="dd MMM yyyy" runat="server" OnClientDateSelectionChanged="checkDateT" CssClass="cal_Theme1"
                                    PopupButtonID="tdate" Enabled="true" TargetControlID="txtToDate" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToDate" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="sr1" Font-Bold="true" ForeColor="white"></asp:RequiredFieldValidator>                  
               </td>
               
           </tr>
       </table> 
          </fieldset>
     <fieldset class="boxBodyHeader">
    </fieldset>
                            <br />

    <fieldset class="boxBodyInner">
    <div style="width:98%;text-align:right">
        <table cellpadding="1px" cellspacing="1px" style="text-align:center;  border-bottom-width:initial;border-color:aliceblue" width="98%" >
            <tr>
                <td style="padding-left:50px; text-align:right">Event Type<asp:HiddenField ID="hfStudentId" runat="server" />  </td>
                <td style="padding-left:10px; text-align:left">
                    <asp:DropDownList runat="server" ID="ddlEventType" Width="250px" CssClass="form-control input-sm" Height="30px">       </asp:DropDownList>  
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlEventType" Display="Dynamic" ErrorMessage="*" InitialValue="0"  ForeColor="Red" ValidationGroup="sr1"></asp:RequiredFieldValidator> </td>
                 <td style="text-align:right">Event</td>
                <td style="padding-left:10px; text-align:left">
                    <asp:TextBox ID="txtEvent" runat="server"  Width="250px" Height="30px" TextMode="MultiLine"></asp:TextBox> 
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEvent" Display="Dynamic" ErrorMessage="*" ForeColor="Red" ValidationGroup="sr1"></asp:RequiredFieldValidator>
                </td>                
                <td style="text-align:right">Objective</td>
                <td style="padding-left:10px; text-align:left">
                    <asp:TextBox ID="txtobjectievent" runat="server"  Width="250px" Height="30px" TextMode="MultiLine"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtobjectievent" Display="Dynamic" ErrorMessage="*" ForeColor="Red" ValidationGroup="sr1"></asp:RequiredFieldValidator>
                    </td>
                <td style="text-align:center"></td>
                <td style="text-align:right"></td>
            </tr>
            <tr>
                <td colspan="8" style="height:10px"></td>

            </tr>
            <tr>
                <td style="padding-left:50px; text-align:right">Guest Faculty</td>
                 <td  style="padding-left:10px; text-align:left">
                       <asp:TextBox ID="txtguestfaculty" runat="server"  Width="250px" Height="30px" ></asp:TextBox>                     
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtguestfaculty" Display="Dynamic" ErrorMessage="*" ForeColor="Red" ValidationGroup="sr1"></asp:RequiredFieldValidator>

                 </td>
                
                <td style="padding-left:50px; text-align:right">Organization</td>
                  <td  style="padding-left:10px; text-align:left">
                       <asp:TextBox ID="txtorganization" runat="server"  Width="250px" Height="30px"> </asp:TextBox> 
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtorganization" Display="Dynamic" ErrorMessage="*" ForeColor="Red" ValidationGroup="sr1"></asp:RequiredFieldValidator>
                  </td>
                <td style="padding-left:1px;">
                    
                </td>

                <td style="padding-left:10px; text-align:left">
                    <asp:Button ID="btnaddc" runat="server" Text="Create Event" class="btn btn-info btn" ValidationGroup="sr1" OnClick="btnaddc_Click"/>
                    <asp:Button ID="btnUpdate" runat="server" class="btn btn-info btn" ValidationGroup="sr1" Text="Update" Visible="false" OnClick="btnUpdate_Click" />
                </td>
                <td style="text-align:center"></td>
                <td style="text-align:right"></td>
            </tr>
               <tr>
                <td colspan="8" style="height:10px"></td>

            </tr>
            <tr>
                <td colspan="8" style="padding-left:7%">
                    
                    <asp:GridView ID="GridView1" runat="server" DataKeyNames="Code"
                         CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" BackColor="White" 
                        EmptyDataText="There are no data records to display." BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="3" Width="94%" GridLines="Horizontal" AllowPaging="false">
              <AlternatingRowStyle BackColor="#F7F7F7" />
    <Columns>
         <asp:TemplateField HeaderText="Sr. No.">
                           <ItemTemplate >
                          <%# Container.DataItemIndex + 1 %>
                                     </ItemTemplate>
                                       <ItemStyle Width="7%" />
                                    </asp:TemplateField>
                            <asp:TemplateField HeaderText="Event">
                                <ItemTemplate>
                                    <asp:Label ID="lblEvent" runat="server" Text='<%#Eval("Event") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="College">
                                <ItemTemplate>
                   <asp:Label ID="lblcollege" runat="server" Text='<%#Eval("College") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
        <asp:TemplateField HeaderText="Faculty">
                                <ItemTemplate>
                   <asp:Label ID="lblfaculty" runat="server" Text='<%#Eval("[Name of Guest Faculty]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
        <asp:TemplateField HeaderText="Organization">
                                <ItemTemplate>
                   <asp:Label ID="lblorganization" runat="server" Text='<%#Eval("[Organization]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
        <asp:TemplateField HeaderText="Objective">
                                <ItemTemplate>
                   <asp:Label ID="lblobjEvent" runat="server" Text='<%#Eval("[Objective of Event]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
          <asp:TemplateField HeaderText="Event Type">
                                <ItemTemplate>
                   <asp:Label ID="lblEventType" runat="server" Text='<%#Eval("[EventType]") %>'></asp:Label>
                   <asp:HiddenField ID="hfEventType" runat="server" value='<%#Eval("[Type of Event]") %>'></asp:HiddenField>
                                </ItemTemplate>
                            </asp:TemplateField>
   
          <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbldate" runat="server"  Text='<%#Eval("Date","{0:dd MMM yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                         
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click"/>
     <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClientClick="return confirm('Are you sure? want to delete this Event.');"  OnClick="btnDelete_Click1"
                                        />
                                    <asp:Label ID="lblCustomerID" runat="server" Text='<%#Eval("Code") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

         <asp:TemplateField HeaderText="Image">
                                <ItemTemplate>
                                    <asp:Button ID="Btnpop" runat="server" Text="view"  OnClick="Btnpop_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>
       <%--  <asp:CommandField ShowDeleteButton="True" />--%>
    </Columns> 
                         
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
               <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
               <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="center" />
               <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont"   />
               <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
               <SortedAscendingCellStyle BackColor="#F4F4FD" />
               <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
               <SortedDescendingCellStyle BackColor="#D8D8F0" />
               <SortedDescendingHeaderStyle BackColor="#3E3277" />
</asp:GridView>


                </td>

            </tr>


        </table>
         <input type="hidden" runat="server" id="hidCustomerID" />


<%--/popup/--%> <%--CancelControlID="btnCancel"--%>
    <asp:Button ID="btnShowPopup" runat="server" style="display:none" />
 <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlpopup"
 BackgroundCssClass="modalBackground">
</asp:ModalPopupExtender>
<asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="369px" Width="700px" style="display:none">
<table width="100%" style="border:Solid 3px #D55500; width:100%; height:100%;" cellpadding="0" cellspacing="0">
<tr style="background-color:#D55500">
<td style=" height:10%; color:White; font-weight:bold; font-size:larger;width:90%" align="center" >EVENT IMAGE</td>
    <td style=" height:10%; color:White; font-weight:bold; font-size:larger;width:10%" align="right" ><asp:Button ID="btnCancel" runat="server" Text="X" BackColor="#D55500" /></td>
</tr>
    
    <tr>
        <td colspan="2">
        <table width="100%">  
            <tr>
        <td style="width:60%; padding-top:10px;" align="right" >
            <asp:FileUpload ID="FileUploadimg1" CssClass="form-control input-sm" runat="server" />
            <asp:RequiredFieldValidator ID="rfvFileUploadimg1" Display="Dynamic" ValidationGroup="g1" ForeColor="Red" runat="server" ControlToValidate="FileUploadimg1" ErrorMessage="First Choose the file!"></asp:RequiredFieldValidator>
        </td>
        <td style="width:40%; padding-top:10px;" align="left" >&nbsp&nbsp&nbsp&nbsp
                         <asp:LinkButton ID="btnUpload" ValidationGroup="g1" class="btn btn-info btn-sm" runat="server" OnClick="btnUpload_Click">
                         <span class="glyphicon glyphicon-upload"></span> Upload 
                         </asp:LinkButton>
        </td>
                </tr>
            </table>
            </td> 
    </tr>

<tr>
<td  align="center" style="padding-top:5px;"></td>
 <td align="left" style="padding-top:5px;">
  
</td>
</tr>
    <tr>
 <td  colspan="2" style="height:20px" align="right"> <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
     <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>
 </td>       

    </tr>
<tr>

<td colspan="2" align="center">
<asp:GridView ID="GridViewpop" runat="server"  CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" BackColor="White" EmptyDataText="There are no data records to display." BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="94%" GridLines="Horizontal" AllowPaging="false">
              <AlternatingRowStyle BackColor="#F7F7F7" />
    <Columns>      
        <asp:TemplateField HeaderText="IMAGE1" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>  
             <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("Image1") %>' Height="70px" Width="60px" AlternateText="NO IMAGE"/><br />
                                    
           <asp:LinkButton ID="lnkDownload" Text ='<%# Eval("Image1").ToString() == "" ? "" : "Download" %>' CommandArgument = '<%# Eval("Image1") %>' runat="server" OnClick="lnkDownload_Click"></asp:LinkButton>
                                       <br />
      <asp:LinkButton ID = "lnkDelete" Text ='<%# Eval("Image1").ToString() == "" ? "" : "Delete" %>' CommandArgument = '<%# Eval("Image1") %>' runat = "server"  OnClick="lnkDelete_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>

          <asp:TemplateField HeaderText="IMAGE2" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>                   
                   <asp:Image ID="Image2" runat="server" ImageUrl='<%# Eval("Image2") %>' Height="70px" ImageAlign="Middle" Width="60px" AlternateText="NO IMAGE" /><br />
                                    
           <asp:LinkButton ID="lnkDownload2" Text ='<%# Eval("Image2").ToString() == "" ? "" : "Download" %>' CommandArgument = '<%# Eval("Image2") %>' runat="server" OnClick="lnkDownload2_Click"></asp:LinkButton> <br />
      <asp:LinkButton ID = "lnkDelete2" Text ='<%# Eval("Image2").ToString() == "" ? "" : "Delete" %>' CommandArgument = '<%# Eval("Image2") %>' runat = "server" OnClick="lnkDelete2_Click"  />

                                </ItemTemplate>
                            </asp:TemplateField>
   
          <asp:TemplateField HeaderText="IMAGE3" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
         <asp:Image ID="Image3" runat="server" ImageUrl='<%# Eval("Image3") %>' Height="70px" ImageAlign="Middle" Width="60px"  AlternateText="NO IMAGE"/>
                                    <br />                                   
   <asp:LinkButton ID="lnkDownload3" Text ='<%# Eval("Image3").ToString() == "" ? "" : "Download" %>'
 CommandArgument = '<%# Eval("Image3") %>' runat="server" OnClick="lnkDownload3_Click"></asp:LinkButton>
 <br />
      <asp:LinkButton ID = "lnkDelete3" Text ='<%# Eval("Image3").ToString() == "" ? "" : "Delete" %>' CommandArgument = '<%# Eval("Image3") %>' runat = "server" OnClick="lnkDelete3_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField> 
 
    </Columns> 

                         
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
               <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
               <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="center" />
               <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont"   />
               <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
               <SortedAscendingCellStyle BackColor="#F4F4FD" />
               <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
               <SortedDescendingCellStyle BackColor="#D8D8F0" />
               <SortedDescendingHeaderStyle BackColor="#3E3277" />
</asp:GridView>
</td>
</tr>
 
</table>
</asp:Panel>
         </div>

 </fieldset>

</asp:Content>

