<%@ Page Title="" Language="C#" MasterPageFile="~/Student/IndexMaster.master" AutoEventWireup="true" CodeFile="StudentGrievances.aspx.cs" Inherits="Student_StudentGrievances" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript" src="../Scripts/jquery-1.9.1.min.js"></script>
     <script type="text/javascript" src="../bootstrap/js/jquery-1.11.2.min.js"></script>
    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../bootstrap/js/bootstrap.min.js"></script>
     <script>
         $(document).ready(function () {
             $('[id$=UploadAttecment]').hide();
         });
         function CheckOne(me) {
             if (me.checked)
                 $('[id$=UploadAttecment]').show();
             else
                 $('[id$=UploadAttecment]').hide();
         }
         $(document).ready(function () {
             $('[id$=txtGroundForAppeal]').keypress(function (e) {
                 if ($(this).val().length >= 500) {
                     e.preventDefault();
                 }
             });
             $('[id$=btnSave]').click(function () {
                 if (($('[id$=chkboxAttechment]').prop("checked") == true) && ($('[id$=UploadAttecment]').val() == '')) {
                     $('[id$=lblError]').text('Please choose the file!');
                 }
                 else
                     $('[id$=lblError]').text('');
             });
         });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"> </asp:ScriptManager>
<fieldset class="boxBody">
 <asp:Label ID="Label1" runat="server" 
            Text="Grievances" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 </fieldset>

 <fieldset class="boxBodyHeader"> 
  
 </fieldset>
  <fieldset class="boxBodyInner">
    <table cellpadding="0px" cellspacing="0px">  
     <tr> <td colspan="15" style="height:10px">
          <fieldset class="boxBodyHeader"> 
          <asp:Label ID="Label3" runat="server" 
            Text="Personal Information" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>              
          </fieldset>
         <br />
         <fieldset class="boxBodyInner">

             <table cellpadding="0px" cellspacing="0px">
                 <tr>
                     <td>
                         <table cellpadding="0px" cellspacing="0px">

                             <tr>
                                 <td>
                                     <label>Roll No:</label></td>
                                 <td style="width: 15px"></td>
                                 <td>
                                     <asp:Label ID="lblRollNo" runat="server" Height="25px" Width="260px" ></asp:Label>
                                 </td>
                                 <td colspan="5"></td>
                                 <td><%--<label >Student No. </label>--%></td>
                                 <td></td>
                                 <td><asp:HiddenField ID="lblStudentNo" runat="server" ></asp:HiddenField></td>
                             </tr>
                             <tr><td colspan="11" style="height: 10px"></td></tr>
                             <tr>
                                 <td>
                                     <label>Name </label>
                                 </td>
                                 <td style="width: 15px"></td>
                                 <td>
                                     <asp:TextBox ID="txtName" runat="server" Height="25px" Enabled="False" Width="260px"></asp:TextBox></td>
                                 <td style="width: 15px"></td>
                                 <td>
                                     <label>Date of Birth </label>
                                 </td>
                                 <td style="width: 15px"></td>
                                 <td>
                                     <asp:TextBox ID="txtDOB" runat="server" Height="25px" Enabled="False" Width="260px"></asp:TextBox></td>
                                 <td style="width: 15px"></td>
                                  <td><label> Course </label>
                                 </td>
                                 <td style="width: 15px"></td>
                                 <td>
                                     <asp:TextBox ID="txtCourse" runat="server" Height="25px" Enabled="False" Width="260px"></asp:TextBox>
                                 </td>

                             </tr>
                             <tr><td colspan="11" style="height: 10px"></td></tr>
                             <tr>
                                <td> <label>Semester/Year</label></td>
                                 <td style="width: 15px"></td>
                                 <td>
                                     <asp:TextBox ID="txtSemester" runat="server" Height="25px" Enabled="False" Width="260px"></asp:TextBox></td>
                                 <td style="width: 15px"></td>
                                 <td><label>Email Address</label></td>
                                 <td style="width: 15px"></td>
                                 <td>
                                     <asp:TextBox ID="txtEmailAddress" runat="server" Height="25px" Enabled="False" Width="260px"></asp:TextBox></td>
                                 <td style="width: 15px"></td>
                                 <td><label> Mobile No </label>
                                 </td>
                                 <td style="width: 15px"></td>
                                 <td>
                                     <asp:TextBox ID="txtMobileNo" runat="server" Height="25px" Width="260px" Enabled="false"></asp:TextBox>
                                 </td>
                             </tr>
                             <tr>
                                 <td colspan="11" style="height: 10px">&nbsp;</td>
                             </tr>
                         </table>
                     </td>
                 </tr>
             </table>

         </fieldset>
           </td></tr>  
        
         <tr> <td colspan="15" style="height:10px">
              <fieldset class="boxBodyHeader"> 
                  <asp:Label ID="Label4" runat="server" Text="Appeal Details" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>              
             </fieldset>
          <fieldset class="boxBodyInner"> 
           <table cellpadding="0px" cellspacing="0px">    
               <tr style="height:5px">
                   <td></td>
               </tr>           
              <tr> 
                  <td>
                      <label>I Submit an appeal according to the relevant section students appeals regarding.</label>
                  </td>
              </tr>
               <tr>
                   <td>
                       <asp:TextBox runat="server" ID="txtStudentAppeal1" MaxLength="250" Width="150%"></asp:TextBox>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" ValidationGroup="g1" ForeColor="Red" runat="server" ControlToValidate="txtStudentAppeal1" ErrorMessage="First give any input!"></asp:RequiredFieldValidator>
                   </td>
               </tr>
                <tr style="height:10px">
                   <td></td>
               </tr>  
                   <tr> 
                  <td>
                      <label>Specific course and unit code(s) and name(s) (If applicable to your appeal).</label>
                  </td>
              </tr>
               <tr>
                   <td>
                       <asp:TextBox runat="server" ID="txtStudentAppeal2" MaxLength="250" Width="150%"></asp:TextBox>
                   </td>
               </tr>
                 <tr> 
                  <td>
                      <label>Grounds for Appeal:</label>
                  </td>
              </tr>
               <tr>
                   <td>
                       <asp:TextBox runat="server" ID="txtGroundForAppeal" TextMode="MultiLine" Width="150%"></asp:TextBox>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ValidationGroup="g1" ForeColor="Red" runat="server" ControlToValidate="txtGroundForAppeal" ErrorMessage="First give any input!"></asp:RequiredFieldValidator>
                   </td>
               </tr>
               <tr style="height:10px">
                   <td></td>
               </tr>  
               <tr>
                   <td>                       
                    <asp:CheckBox ID="chkboxAttechment" runat="server"  onClick="CheckOne(this)" Text="  You have included any relevant evidence that may support your appeal" />
                   </td>
               </tr>
               <tr style="height:10px">
                   <td></td>
               </tr>
               <tr>
                   <td>
                       <asp:FileUpload runat="server" ID="UploadAttecment" />
                   </td>
               </tr>
               <tr>
                   <td>                       
                        <asp:Label runat="server" ID="lblError" ForeColor="Red" Text=""></asp:Label>
                   </td>
               </tr>
               <tr style="height:10px">
                   <td></td>
               </tr>
       </table>

               <div class="pull-right">
                  <asp:Button ID="btnSave" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1"   Height="30px" Width="90px" OnClick="btnSave_Click"  Text="SAVE" />
              </div>

          </fieldset>
           </td></tr>      
        </table>
    </fieldset>
</asp:Content>

