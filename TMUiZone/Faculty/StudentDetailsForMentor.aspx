<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="StudentDetailsForMentor.aspx.cs" Inherits="Faculty_StudentDetailsForMentor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <script>
       $(document).ready(function () {
           $('[id$=divAttendanceReport]').hide();
           $('[id$=divAcademicPerformance]').hide();
           $('[id$=divInteractionDetails]').hide();

       });
       function PersonalInformation() {
           $('[id$=divPersonalInformation]').slideToggle();
       }
       function AttendanceReport() {
           $('[id$=divAttendanceReport]').slideToggle();
       }
       function AcademicPerformance() {
           $('[id$=divAcademicPerformance]').slideToggle();
       }
       function InteractionDetails() {
           $('[id$=divInteractionDetails]').slideToggle();
       }
   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"> </asp:ScriptManager>
<fieldset class="boxBody">
 <asp:Label ID="Label1" runat="server" 
            Text="Profile" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 </fieldset>

 <fieldset class="boxBodyHeader"> 
  
 </fieldset>
  <fieldset class="boxBodyInner">
    <table cellpadding="0px" cellspacing="0px">  
     <tr> <td colspan="15" style="height:10px">
         <div id="div1" onclick="PersonalInformation()">
              <fieldset class="boxBodyHeader" style="width:1100px"> 
              <asp:Label ID="Label3" runat="server" 
                Text="Personal Information" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>              
              </fieldset>
             </div>
         <div id="divPersonalInformation" >
             <br />
             <fieldset class="boxBodyInner">

                 <table cellpadding="0px" cellspacing="0px">
                     <tr>
                         <td>
                             <table cellpadding="0px" cellspacing="0px">

                                 <tr>
                                     <td>
                                         <label style="width:100px">Roll No:</label></td>
                                     <td style="width: 10px"></td>
                                     <td>
                                         <asp:Label ID="lblRollNo" Font-Underline="true" runat="server" Height="25px" Width="240px" ></asp:Label>
                                     </td>
                                     <td></td>
                                     <td></td>
                                     <td></td>
                                     <td></td>
                                     <td></td>
                                     <td><label>Mentor: </label></td>
                                     <td></td>
                                     <td><asp:Label ID="lblMentor" runat="server" Font-Underline="true" Height="25px" Width="240px" Enabled="false"></asp:Label></td>
                                 </tr>

                                 <tr>
                                     <td colspan="11" style="height: 10px"></td>
                                 </tr>
                                 <tr>
                                     <td>
                                         <label>Name </label>
                                     </td>
                                     <td style="width: 10px"></td>
                                     <td>
                                         <asp:TextBox ID="txtName" runat="server" Height="25px" Enabled="False" Width="240px"></asp:TextBox></td>
                                     <td style="width: 10px"></td>
                                     <td><label> Hostler/day scholar </label>
                                     </td>
                                     <td style="width: 10px"></td>
                                     <td>
                                         <asp:TextBox ID="txtHostlerScholar" runat="server" Height="25px" Enabled="False" Width="240px"></asp:TextBox>
                                     </td>
                                     <td></td>
                                     <td></td>
                                     <td></td>
                                     <td rowspan="5" align="center">
                                     <asp:Image ID="imgStudent" runat="server" Height="100px" Width="100px" />  

                                     </td>
                                     </tr>
                                 <tr>
                                     <td colspan="11" style="height: 10px"></td>
                                 </tr>
                                 <tr>
                                        <td> <label>Father's Name</label></td>
                                     <td style="width: 10px"></td>
                                     <td>
                                         <asp:TextBox ID="txtFatherName" runat="server" Height="25px" Enabled="False" Width="240px"></asp:TextBox></td>
                                     <td style="width: 10px"></td>
                                     <td>
                                         <label>Program </label>
                                     </td>
                                     <td style="width: 10px"></td>
                                     <td>
                                         <asp:TextBox ID="txtCourse" runat="server" Height="25px" Enabled="False" Width="240px"></asp:TextBox></td>
                                     </tr>
                                
                                  <tr>
                                     <td colspan="11" style="height: 10px"></td>
                                 </tr>
                                 <tr>
                                     <td><label>Mother's Name</label></td>
                                     <td style="width: 10px"></td>
                                     <td>
                                         <asp:TextBox ID="txtMotherName" runat="server" Height="25px" Enabled="False" Width="240px"></asp:TextBox></td>
                                     <td style="width: 10px"></td>
                                 
                                 </tr>
                                 <tr>
                                     <td colspan="11" style="height: 10px">&nbsp;</td>
                                 </tr>
                                 <tr>
                                     <td><label>Address </label>
                                     </td>
                                     <td style="width: 10px"></td>
                                     <td colspan="10">
                                         <asp:TextBox ID="txtCorrespondenceAddress" runat="server" Height="25px" Width="100%" Enabled="false"></asp:TextBox>
                                     </td>

                                 </tr>
                                 <tr>
                                     <td colspan="11" style="height: 10px"></td>
                                 </tr>
                                 <tr>
                                     <td>
                                         <label>Phone No (Stu) </label>
                                     </td>
                                     <td style="width: 10px"></td>
                                     <td>
                                         <asp:TextBox ID="txtPhoneNoStudent" runat="server" Height="25px" Enabled="False" Width="240px"></asp:TextBox></td>
                                     <td style="width: 10px"></td>
                                     <td>
                                         <label>Phone No (Parents) </label>
                                     </td>
                                     <td style="width: 10px"></td>
                                     <td>
                                         <asp:TextBox ID="txtPhoneNoParents" runat="server" Height="25px" Enabled="False" Width="240px"></asp:TextBox></td>
                                     <td style="width: 10px"></td>
                                      <td><label>D.O.B </label>
                                     </td>
                                     <td style="width: 10px"></td>
                                     <td>
                                         <asp:TextBox ID="txtDOB" runat="server" Height="25px" Enabled="False" Width="240px"></asp:TextBox>
                                     </td>

                                 </tr>
                                 <tr>
                                     <td colspan="11" style="height: 10px"></td>
                                 </tr>
                                 <tr>
                                     <td>
                                         <label>E-Mail ID (Stu) </label>
                                     </td>
                                     <td style="width: 10px"></td>
                                     <td>
                                         <asp:TextBox ID="txtEmailIDStudent" runat="server" Height="25px" Enabled="False" Width="240px"></asp:TextBox></td>
                                     <td style="width: 10px"></td>
                                     <td>
                                         <label>E-Mail ID (Parents) </label>
                                     </td>
                                     <td style="width: 10px"></td>
                                     <td>
                                         <asp:TextBox ID="txtEmailIDParents" runat="server" Height="25px" Enabled="False" Width="240px"></asp:TextBox></td>
                                     <td style="width: 10px"></td>
                                      <td><label> High School % </label>
                                     </td>
                                     <td style="width: 10px"></td>
                                     <td>
                                         <asp:TextBox ID="txtHighSchoolPercentage" runat="server" Height="25px" Enabled="False" Width="240px"></asp:TextBox>
                                     </td>

                                 </tr>
                                 <tr>
                                     <td colspan="11" style="height: 10px"></td>
                                 </tr>
                                 <tr>
                                     <td>
                                         <label>Intermediate % </label>
                                     </td>
                                     <td style="width: 10px"></td>
                                     <td>
                                         <asp:TextBox ID="txtIntermediatePercentage" runat="server" Height="25px" Enabled="False" Width="240px"></asp:TextBox></td>
                                     <td style="width: 10px"></td>
                                     <td>
                                         <label>Graduation % </label>
                                     </td>
                                     <td style="width: 10px"></td>
                                     <td>
                                         <asp:TextBox ID="txtGradutationPercentage" runat="server" Height="25px" Enabled="False" Width="240px"></asp:TextBox></td>
                                     <td style="width: 10px"></td>
                                 
                                 </tr>
                                  <tr>
                                     <td colspan="11" style="height: 10px"></td>
                                 </tr>
                             </table>
                         </td>
                     </tr>
                 </table>

             </fieldset>
         </div>
           </td></tr>  
        
         <tr> <td colspan="15" style="height:10px">
             <div id="div2" onclick="AttendanceReport()">
                  <fieldset class="boxBodyHeader"> 
                      <asp:Label ID="Label4" runat="server" Text="Attendance Report" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>              
                 </fieldset>
                 </div>
             <div id="divAttendanceReport" >
                  <br />
                  <fieldset class="boxBodyInner"> 
                  <center>
               <asp:GridView ID="grdAttendanceReport" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="866px" GridLines="Horizontal" OnPageIndexChanging="grdAttendanceReport_PageIndexChanging"  EmptyDataText="There are no data records to display." AllowPaging="true" PageSize="10" >
                   <AlternatingRowStyle BackColor="#F7F7F7" />
                            <Columns>                          
                                <asp:BoundField DataField="Sem/Year"  HeaderText="Year/Sem." SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="MDate" HeaderText="Month" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="year1" HeaderText="Year" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                 <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Percentage" HeaderText="Percentage" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                           </Columns>                       
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                   <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                   <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                   <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont"   />
                   <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
                   <SortedAscendingCellStyle BackColor="#F4F4FD" />
                   <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                   <SortedDescendingCellStyle BackColor="#D8D8F0" />
                   <SortedDescendingHeaderStyle BackColor="#3E3277" />
                        </asp:GridView>
                  </center>
              </fieldset>
                 <br />
             </div>
             <div id="div3" onclick="AcademicPerformance()">
                  <fieldset class="boxBodyHeader"> 
                      <asp:Label ID="Label2" runat="server" Text="Academic Performance" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>              
                 </fieldset>
                 </div>
             <div id="divAcademicPerformance">
                  <br />
                  <fieldset class="boxBodyInner"> 
              <center>
                       <asp:GridView ID="grdAcademicPerformance" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="866px" GridLines="Horizontal" OnPageIndexChanging="grdInteractionDetails_PageIndexChanging"  EmptyDataText="There are no data records to display." AllowPaging="true" PageSize="10" >
                   <AlternatingRowStyle BackColor="#F7F7F7" />
                            <Columns> 

                            </Columns>                       
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                   <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                   <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                   <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont"   />
                   <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
                   <SortedAscendingCellStyle BackColor="#F4F4FD" />
                   <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                   <SortedDescendingCellStyle BackColor="#D8D8F0" />
                   <SortedDescendingHeaderStyle BackColor="#3E3277" />
                        </asp:GridView>
              </center>

              </fieldset>
                 <br />
                 </div>
            <div id="div4" onclick="InteractionDetails()">
                  <fieldset class="boxBodyHeader"> 
                      <asp:Label ID="Label5" runat="server" Text="Interaction Details" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>              
                 </fieldset>
                </div>
              <div id="divInteractionDetails">
                  <br />
                  <fieldset class="boxBodyInner"> 
                  <center>
                     <asp:GridView ID="grdInteractionDetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="866px" GridLines="Horizontal" OnPageIndexChanging="grdInteractionDetails_PageIndexChanging"  EmptyDataText="There are no data records to display." AllowPaging="true" PageSize="10" >
                   <AlternatingRowStyle BackColor="#F7F7F7" />
                            <Columns>                          
                                <asp:BoundField DataField="Interaction Date"  HeaderText="Interaction Date" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Contact Person Phone No_" HeaderText="Contact Person Phone No_" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Contact Person Name" HeaderText="Contact Person Name" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Interaction Summary" HeaderText="Interaction Summary" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                           </Columns>                       
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                   <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                   <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                   <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont"   />
                   <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
                   <SortedAscendingCellStyle BackColor="#F4F4FD" />
                   <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                   <SortedDescendingCellStyle BackColor="#D8D8F0" />
                   <SortedDescendingHeaderStyle BackColor="#3E3277" />
                        </asp:GridView>
                  </center>
                  <%-- <div class="pull-right">
                      <asp:Button ID="btnSave" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1"   Height="30px" Width="90px" OnClick="btnSave_Click"  Text="SAVE" />
                  </div>--%>

              </fieldset>
                 <br />
                 </div>
           </td></tr>      
        </table>
    </fieldset>
</asp:Content>

