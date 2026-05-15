<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="FacultyGrievances.aspx.cs" Inherits="Faculty_FacultyGrievances" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">  
   <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<link href="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css"
    rel="stylesheet" type="text/css" />
<script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js"></script>
<link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
<script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        $('[id*=lstGriv]').multiselect({
            includeSelectAllOption: true
        });
    });
</script>
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
              if (sender._selectedDate > new Date()) {
                  alert("You cannot select greater than current date!");
                  sender._selectedDate = new Date();
                  sender._textbox.set_Value(sender._selectedDate.format(sender._format))
              }
          }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<link href="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css"
    rel="stylesheet" type="text/css" />
<script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js"></script>
<link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
<script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js" type="text/javascript"></script>
     <asp:ScriptManager ID="ScriptManager1" runat="server"> </asp:ScriptManager>
<fieldset class="boxBody">
    <table width="100%">
 <asp:Label ID="Label1" runat="server" 
            Text="Grievances" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
    </table>
 </fieldset>

 <fieldset class="boxBodyHeader"> 
  
 </fieldset>
  <fieldset class="boxBodyInner">
    <table cellpadding="0px" cellspacing="0px" >  
     <tr>
          <td colspan="15" style="height:10px">
          <fieldset class="boxBodyHeader"> 
          <asp:Label ID="Label3" runat="server" 
            Text="Personal Information" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" > </asp:Label>              
          </fieldset>
         <br />
         <fieldset class="boxBodyInner">

             <table cellpadding="0px" cellspacing="0px">
                 <tr>
                     <td>
                         <table cellpadding="0px" cellspacing="0px">

                               <tr>
                                 <td>
                                     Employee Code
                                 </td>
                                 <td style="width: 15px"></td>
                                 <td>
                                     <asp:TextBox ID="txtEmpCode" runat="server" Height="25px" Enabled="False" Width="260px"></asp:TextBox>
                                 </td>
                                 <td style="width: 15px"></td>
                                 <td>
                                     Name
                                 </td>
                                 <td style="width: 15px"></td>
                                 <td>
                                     <asp:TextBox ID="txtName" runat="server" Height="25px" Enabled="False" Width="260px"></asp:TextBox>

                                 </td>
                                 <td style="width: 15px"></td>
                                  <td>Designation </td>
                                 <td style="width: 15px"></td>
                                 <td>
                                     <asp:TextBox ID="txtDesignation" runat="server" Height="25px" Enabled="False" Width="260px"></asp:TextBox>
                                 </td>

                             </tr>
                             <tr><td colspan="11" style="height: 10px"></td></tr>
                             <tr>
                                 <td>
                                     College Code
                                 </td>
                                 <td style="width: 15px"></td>
                                 <td>
                                     <asp:TextBox ID="txtCollegeCode" runat="server" Height="25px" Enabled="False" Width="260px"></asp:TextBox></td>
                                 <td style="width: 15px"></td>
                                 <td>
                                     College Name
                                 </td>
                                 <td style="width: 15px"></td>
                                 <td colspan="5">
                                     <asp:TextBox ID="txtCollegeName" runat="server" Height="25px" Enabled="False" Width="630px" ></asp:TextBox>
                                 </td>
                                 

                             </tr>
                             <tr><td colspan="11" style="height: 10px"></td></tr>
                             <tr>
                                <td> D.O.B.</td>
                                 <td style="width: 15px"></td>
                                 <td>
                                     <asp:TextBox ID="txtDOB" runat="server" Height="25px" Enabled="False" Width="260px"></asp:TextBox></td>
                                 <td style="width: 15px"></td>
                                 <td>D.O.J.</td>
                                 <td style="width: 15px"></td>
                                 <td>
                                     <asp:TextBox ID="txtDOJ" runat="server" Height="25px" Enabled="False" Width="260px"></asp:TextBox></td>
                                 <td style="width: 15px"></td>
                                 <td> Contact No 
                                 </td>
                                 <td style="width: 15px"></td>
                                 <td>
                                     <asp:TextBox ID="txtContactNo" runat="server" Height="25px" Width="260px" Enabled="false"></asp:TextBox>
                                 </td>
                             </tr>
                             <tr>
                                 <td colspan="11" style="height: 10px">&nbsp;</td>
                             </tr>
                             <tr>
                                <td> Email Address</td>
                                 <td style="width: 15px"></td>
                                 <td>
                                     <asp:TextBox ID="txtEmail" runat="server" Height="25px" Enabled="False" Width="260px"></asp:TextBox></td>
                                 <td style="width: 15px"></td>
                                 <td></td>
                                 <td style="width: 15px"></td>
                                 <td>
                                     </td>
                                 <td style="width: 15px"></td>
                                 <td>
                                 </td>
                                 <td style="width: 15px"></td>
                                 <td>
                                     
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
           </td>
     </tr>         
     <tr>
          <td colspan="15" style="height:10px">
              <fieldset class="boxBodyHeader"> 
                  <asp:Label ID="Label4" runat="server" Text="Appeal Details" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" > </asp:Label>              
             </fieldset>
              <fieldset class="boxBodyInner">
                  <asp:UpdatePanel runat="server" ID="updGriev">
                      <ContentTemplate>
                  <table cellpadding="0px" cellspacing="0px" width="100%">
                      <tr>
                          <td></td>
                          <td  style="width: 250px">
                            <b> (A) Grievable Matters</b> 
                          </td>
                          <td></td>
                          <td style="width: 15px" colspan="8" rowspan="3">
                                     <asp:TextBox ID="txtGrievableMatters" runat="server" TextMode="MultiLine" Width="400px" height="100px"></asp:TextBox>
                                 </td>
                      </tr>
                      <tr>
                          <td></td>
                          <td style="width: 25px">
                            Please check all that apply:
                          </td>
                          <td></td>
                      </tr>
                   <tr>
                             <td></td>   
                                 <td style="width: 15px">
                                     <asp:ListBox ID="lstGriv" runat="server" SelectionMode="Multiple">
                                         <asp:ListItem Text="Administration of polices" Value="1" />
                                         <asp:ListItem Text="Dismissal" Value="2" />
                                         <asp:ListItem Text="Infringement of faculty rights" Value="3" />
                                         <asp:ListItem Text="Promotion" Value="4" />
                                         <asp:ListItem Text="Question of Policies and procedures" Value="5" />
                                         <asp:ListItem Text="Salary" Value="6" />
                                         <asp:ListItem Text="Leave Matter" Value="7" />
                                         <asp:ListItem Text="Tenure" Value="8" />
                                         <asp:ListItem Text="Violation of academic freedom" Value="9" />
                                         <asp:ListItem Text="Working Condition" Value="10" />
                                         <asp:ListItem Text="Student Misbehave" Value="11" />
                                         <asp:ListItem Text="Faculty Misbehave" Value="12" />                                         
                                     </asp:ListBox>
                                 </td>
                                 <td valign="middle">
                                   <asp:Button ID="btnGrievableMatters" Text=">>" Font-Bold="true" runat="server" OnClick="btnGrievableMatters_Click"  /> 
                                 </td>                               
                                 
                             </tr>
                      <tr>
                          <td></td>
                          <td colspan="2"><asp:CheckBox ID="chkOther" runat="server" Text="Other Matters" AutoPostBack="true" OnCheckedChanged="chkOther_CheckedChanged"/></td>
                          <td colspan="8"> <asp:TextBox ID="txtOther"  runat="server" Width="400px" Enabled="false"  ></asp:TextBox>
                                           <asp:RequiredFieldValidator id="rfvOther" runat="server" Enabled="false"  ControlToValidate="txtOther" ErrorMessage="*" ForeColor="Red" 
                                               Font-Bold="true" SetFocusOnError="true" ValidationGroup="g1"></asp:RequiredFieldValidator>
                          </td>
                      </tr>
                      </table>
                          </ContentTemplate>
                  </asp:UpdatePanel> 
                  </fieldset>
          <fieldset class="boxBodyInner"> 
           <table cellpadding="0px" cellspacing="0px" >    
               <tr style="height:5px">                   
                   <td></td>
               </tr>           
              <tr> 
                  <td>
                   <asp:TextBox ID="txtText" runat ="server" Text="All grievances related to discrimination based upon age, career/family, carrier status,color, disability, domestic violence victim status, enthnicity, gender,I marital status, national origin, race, religion, veteran status and any other protected status." 
                       Enabled="false" width="1105px" Font-Bold="true" TextMode="MultiLine" Height="60px"></asp:TextBox>
                  </td>
                  
              </tr>
              
                <tr style="height:10px">
                   <td></td>
                   
               </tr>  
                   <tr> 
                  <td>
                      (B) Please provide specific details of the master giving rise to the grievance and how you believe it violates the Collective Bargaining Arrement.
                  </td>
                      
              </tr>
               <tr>
                   <td>
                       <asp:TextBox runat="server" ID="txtFacultySpecificDetails" MaxLength="250" Width="100%"></asp:TextBox>
                   </td>
                   
               </tr>
                 <tr> 
                  <td>
                      (C) What was the date of the event or the date on which you became aware of it?
                  </td>
                     
              </tr>
               <tr>
                   <td>
                       <asp:TextBox runat="server" ID="txtDateOfEvent" CssClass="form-control input-sm" runat="server" Width="150px" onkeypress="return false" onKeyDown="preventBackspace();" ></asp:TextBox>
        
                         <asp:Image src="../Images/Calendar.png" runat="server" Height="30px" Width="25px" alt="" id="fdate" />
                        <asp:CalendarExtender ID="cleDateOfEvent" Format="dd MMM yyyy" runat="server" OnClientDateSelectionChanged="checkDate" CssClass="cal_Theme1" 
                            PopupButtonID="fdate" Enabled="true" TargetControlID="txtDateOfEvent">
                                   </asp:CalendarExtender>
                       <asp:RequiredFieldValidator ID="rv1" Display="Dynamic" ValidationGroup="g1" ForeColor="Red" runat="server" ControlToValidate="txtDateOfEvent" ErrorMessage="First give any input!"></asp:RequiredFieldValidator>
                   </td>
                   
               </tr>
                <tr> 
                  <td>
                      (d) What specific remedy are you seeking?
                  </td>
                    
              </tr>
               <tr>
                   <td>
                       <asp:TextBox runat="server" ID="txtSpecificRemedy" TextMode="MultiLine" Width="100%"></asp:TextBox>
                       <asp:RequiredFieldValidator ID="rv2" Display="Dynamic" ValidationGroup="g1" ForeColor="Red" runat="server" ControlToValidate="txtSpecificRemedy" ErrorMessage="First give any input!"></asp:RequiredFieldValidator>
                   </td>
                   
               </tr>
               <tr style="height:10px">
                   <td></td>
                  
               </tr>  
              <%-- <tr>
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
               </tr>--%>
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

