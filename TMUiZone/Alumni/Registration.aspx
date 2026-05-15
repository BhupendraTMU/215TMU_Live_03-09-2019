<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/IndexMaster.master" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Alumni_Registration" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" >
        function onlyNumbers(event) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;

        }

        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            <asp:UpdatePanel ID="updatepnl" runat="server">
                            <ContentTemplate>
                        <fieldset class="boxBodyInner">
                            <div style="width:75%;margin-left:5%;margin-right:5%;text-align:center;">
                        <table cellpadding="0px" cellspacing="0px" style="width:88%;">

                            <tr>
                                <td colspan="3" style="height:10px;width:100%;"></td>
                            </tr>
                                              <tr>
                                
                                            <td style="width:20%;text-align:left;vertical-align:middle">
                                                <label>Student Name</label></td>
                                            <td style="width:2%;"></td>
                                            <td style="width:60%;">
                                                <asp:TextBox ID="txtNAME" runat="server" Enabled="false" ></asp:TextBox>
                                            </td>
                                               <td style="width:20%;text-align:left;vertical-align:middle">
                                                <label">Date of Birth</label></td>
                                            <td style="width:2%;"></td>
                                            <td >
                                                <asp:TextBox ID="txtdob" runat="server"  Enabled="false"></asp:TextBox></td>
                                        </tr>
                            <tr>
                                <td colspan="3" style="height:10px;width:100%;"></td>
                            </tr>
                                
                             
                            <tr>
                                <td colspan="3" style="height:10px;width:100%;"></td>
                            </tr>
                               <tr>
                                
                                           
                                            <td style="width:20%;text-align:left;vertical-align:middle">
                                                <label">GENDER</label></td>
                                            <td style="width:2%;"></td>
                                            <td >
                                                <asp:TextBox ID="txtGender" runat="server"  Enabled="false"></asp:TextBox></td>

                                  <td style="width:20%;text-align:left;vertical-align:middle">
                                                <label">WHATSAPP NO</label></td>
                                            <td style="width:2%;"></td>
                                            <td >
                                                <asp:TextBox ID="txtWhatsApp" runat="server"  ></asp:TextBox></td>
                                        </tr>
                            <tr>
                                <td colspan="3" style="height:10px;width:100%;"></td>
                            </tr>
                            
                                   
                            <tr>
                                <td colspan="3" style="height:10px;width:100%"></td>
                            </tr>
                                         <tr>
                                            <td style="width:20%;text-align:left;vertical-align:middle">
                                                <label>PASSOUT YEAR</label></td>
                                            <td style="width:2%;"></td>
                                            <td style="width:60%;">
                                                <asp:TextBox ID="txtPY" runat="server" Enabled="false"  vertical-align:middle></asp:TextBox></td>

                                              <td style="vertical-align:middle;text-align:left">
                                                <label >COLLEGE</label></td>
                                            <td style="width:2%;"></td>
                                            <td >
                                                <asp:TextBox ID="txtCO" runat="server"  Enabled="false" ></asp:TextBox></td>
                                        </tr>
                            <tr>
                                <td colspan="3" style="height:10px;width:100%"></td>
                            </tr>
                                 
                             <tr>
                                            <td style="width:20%;text-align:left;vertical-align:middle">
                                                <label>MOBILE NO</label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtmob"  ValidationGroup="g1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                            <td style="width:2%;"></td>
                                            <td style="width:60%;">
                                                <asp:TextBox ID="txtmob" runat="server"  onkeypress="return onlyNumbers(event)" MaxLength ="13" ></asp:TextBox></td>
                                        <td style="vertical-align:middle;text-align:left">
                                                <label">EMAIL ID </td>
                                            <td style="width:2%;"></td>
                                            <td >
                                   <asp:TextBox ID="txtEmailID" runat="server"  MaxLength ="30" ></asp:TextBox></td>
                             
                             </tr>
                            
                            <tr>
                                <td colspan="3" style="height:10px;width:100%"></td>
                            </tr>      
                            <tr>
                                            <td style="width:20%;text-align:left;vertical-align:middle">
                                                <label>LINKEDIN URL</label></td>
                                            <td style="width:2%;"></td>
                                            <td >
                                                <asp:TextBox ID="txtLink" runat="server"    ></asp:TextBox></td>
                                        <td style="vertical-align:middle;text-align:left" >
                                                <label >FACEBOOK URL</label></td>
                                            <td style="width:2%;"></td>
                                            <td >
                                   <asp:TextBox ID="txtFace" runat="server"   ></asp:TextBox></td>
                             
                             </tr>
                            <tr>
                                <td colspan="3" style="height:10px;width:100%"></td>
                            </tr>      
                             <tr>
                                            <td style="width:20%;text-align:left;vertical-align:middle">
                                                <label>TWITTER HANDLE</label></td>
                                            <td style="width:2%;"></td>
                                            <td style="vertical-align:middle;text-align:left">
                                                <asp:TextBox ID="txtTwitter" runat="server"    ></asp:TextBox></td>
                                        <td  style="vertical-align:middle;text-align:left">
                                                <label >PROGRAMME</label></td>
                                          
                                                
                                            <td style="width:2%;"></td>
                                            <td style="width:60%;">
                                     <asp:TextBox ID="txtpro" runat="server" Enabled="false"  ></asp:TextBox></td>
                                            </td>
                                            <td >
                                   </td>
                             
                             </tr>
                            <tr>
                                <td colspan="3" style="height:10px;width:100%"></td>
                            </tr>      
                            
                        

                                         <tr>
                                               <td style="width:20%;text-align:left;vertical-align:middle">
                                                <label>ADMISSION YEAR</label></td>
                                            <td style="width:2%;"></td>
                                            <td style="width:60%;">
                                     <asp:TextBox ID="txtAdmissionYear1" runat="server" Enabled="false"  ></asp:TextBox></td>

                                           
                                             <td style="vertical-align:middle;text-align:left">
                                                <label>ENROLLMENT NO</label></td>
                                            <td style="width:2%;"></td>
                                            <td >
                                             <asp:TextBox ID="txtEnroll" runat="server" Enabled="false"></asp:TextBox></td>

                                                
                                                </td>

                                            
                                        </tr>
                            <tr>
                                <td colspan="3" style="height:10px;width:100%"></td>
                            </tr>
                                  <tr>
                                            <td style="width:20%;text-align:left;vertical-align:middle">
                                                <label>PRESENT ADDRESS</label><asp:RequiredFieldValidator ID="RequiredFieldValidator34" ControlToValidate="txtmob"  ValidationGroup="g1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                            <td style="width:2%;"></td>
                                            <td style="width:60%;text-align:left;vertical-align:middle">
                                                <asp:TextBox ID="txtPAddress" runat="server"  TextMode="MultiLine"   ></asp:TextBox></td>
                                        <td  style="vertical-align:middle;text-align:left">
                                                <label >PER ADDRESS </label></td>
                                            <td style="width:2%;"></td>
                                            <td >
                                   <asp:TextBox ID="txtPerAddress" runat="server" Width="100%" TextMode="MultiLine" ></asp:TextBox></td>
                             
                             </tr>
                            
                            <tr>
                                <td colspan="3" style="height:10px;width:100%"></td>
                            </tr>
                                 <tr>
                                <td colspan="3" style="height:10px;width:100%"></td>
                            </tr> <tr>
                                <td colspan="3" style="height:10px;width:100%"></td>
                            </tr>      
                            <tr>
                                           <td style="vertical-align:middle;text-align:left;width:500px" colspan="4">
                                               PROFESSIONAL STATUS&nbsp&nbsp&nbsp&nbsp&nbsp
                                            
                                          
                                             
                                                <asp:DropDownList ID="ddlEng" Width="172px" Height="30px" runat="server" OnSelectedIndexChanged="ddlEng_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="1" Text="Employed" Selected="True"></asp:ListItem>
                                                     <asp:ListItem Value="2" Text="Self Employed/Start Up"></asp:ListItem>
                                                     <asp:ListItem Value="3" Text="Further Education"></asp:ListItem>
                                                     <asp:ListItem Value="4" Text="Unemployed "></asp:ListItem>
                                                </asp:DropDownList>
                                                </td>

                                             <td >
                                                <label></label></td>
                                            <td style="width:2%;"></td>
                                            <td >
                                            </td>

                                                
                                                </td>

                                             
                                        </tr>
                            <tr>
                                <td colspan="3" style="height:10px;width:100%"></td>
                            </tr>
                                     
                              
                            
                            <asp:Panel id="pnlEmployee" runat="server">

                             
                                   <tr>
                                            <td style="width:20%;text-align:left;vertical-align:middle">
                                                <label>Employer Name</label><asp:RequiredFieldValidator ID="RetxtEmp" ControlToValidate="txtEmp" ValidationGroup="g1" runat="server" ErrorMessage="*" ></asp:RequiredFieldValidator></td>
                                            <td style="width:2%;"></td>
                                            <td style="width:60%;text-align:left">
                                                
                                   <asp:TextBox ID="txtEmp" runat="server"  placeholder="Employer Name"></asp:TextBox>
                                         
                                       </td>
                                             <td style="vertical-align:middle;text-align:left">
                                                <label>Designation</label></td>
                                            <td style="width:2%;"></td>
                                            <td >
                                                 <asp:TextBox ID="txtDesi" runat="server"  placeholder="Designation"></asp:TextBox></td>

                                                
                                                </td>

                                            
                                        </tr>
                                 
                                  <tr>
                                            <td style="width:20%;text-align:left;vertical-align:middle">
                                                <label>Address </label></td>
                                            <td style="width:2%;"></td>
                                            <td style="width:60%;">
                                   <asp:TextBox ID="txtAdd" runat="server"  placeholder="Address"></asp:TextBox></td>


                                             <td style="vertical-align:middle;text-align:left">
                                                <label>Industry </label> <asp:RequiredFieldValidator ID="RetxtComName" ControlToValidate="txtComName"  ValidationGroup="g1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                            <td style="width:2%;"></td>
                                            <td style="width:20%;">
                                                
                                               <asp:TextBox ID="txtComName" runat="server"  placeholder="Industry"></asp:TextBox>
                                                

                                            </td>

                                                
                                                </td>

                                            
                                        </tr>
                                 
                                 
                                  <tr>
                                            <td style="width:20%;text-align:left;vertical-align:middle">
                                                <label>Job Description </label></td>
                                            <td style="width:2%;"></td>
                                            <td style="width:60%;">
                                   <asp:TextBox ID="txtJobDes" runat="server"  placeholder="Job Description"></asp:TextBox></td>


                                             <td style="vertical-align:middle;text-align:left">
                                                <label>Job Status </label></td>
                                            <td style="width:2%;"></td>
                                            <td style="text-align:left;">
                                               <asp:DropDownList ID="drpJstatus" runat="server" Width="172px" Height="30px">
                                                   <asp:ListItem Text="Full Time" Value="0"> </asp:ListItem>
                                                   <asp:ListItem Text="Part Time" Value="1"> </asp:ListItem>
                                                   <asp:ListItem Text="Internship" Value="2"> </asp:ListItem>
                                               </asp:DropDownList></td>

                                                
                                                </td>

                                            
                                        </tr>
                                 
                                  <tr>
                                            <td style="width:20%;text-align:left;vertical-align:middle">
                                                <label>Start Date </label></td>
                                            <td style="width:2%;"></td>
                                            <td style="width:60%;">
                                   <asp:TextBox ID="txtStartDate" runat="server"  placeholder="Start Date"></asp:TextBox></td>
                                       <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStartDate" Format="dd MMM yyyy">
                                </asp:CalendarExtender>

                                             <td style="vertical-align:middle;text-align:left">
                                                <label>website </label></td>
                                            <td style="width:2%;"></td>
                                            <td >
                                              
                                                  <asp:TextBox ID="txtComWebSite" runat="server"  placeholder="Company Website"></asp:TextBox>
                                                
                                                </td>

                                            
                                        </tr>
                                  <tr>
                                            <td style="width:20%;text-align:left;vertical-align:middle">
                                                <label>Company Email </label></td>
                                            <td style="width:2%;"></td>
                                            <td style="width:60%;">
                                   <asp:TextBox ID="txtCEmail" runat="server"  placeholder="Company Email"></asp:TextBox></td>


                                             <td style="vertical-align:middle;text-align:left">
                                                <label>Telephone </label></td>
                                           <td style="width:2%;"></td>
                                            <td style="width:60%;">
                                              
                                                  <asp:TextBox ID="txtCTele" runat="server"  placeholder="Company Telephone"></asp:TextBox>
                                                
                                                </td>

                                            
                                        </tr>
                                 
                                 </asp:Panel>
                                 
                                 
                                 
                                 <asp:Panel ID="SelfEmployee" runat="server" Visible="false">
                                 
                                 
                                  <tr>
                                            <td style="width:20%;text-align:left;vertical-align:middle">
                                                <label>Enterprise Name</label></td>
                                            <td style="width:2%;"></td>
                                            <td style="width:60%;text-align:left">
                                                
                                   <asp:TextBox ID="txtEnterPrice" runat="server"  placeholder="Enterprise Name"></asp:TextBox>
                                         
                                       </td>
                                             <td style="vertical-align:middle;text-align:left;">
                                                <label>Industry</label></td>
                                            <td style="width:2%;"></td>
                                            <td >
                                                 <asp:TextBox ID="txtIndustry" runat="server"  placeholder="Industry"></asp:TextBox></td>

                                                
                                                </td>

                                            
                                        </tr>
                                 
                                  <tr>
                                            <td style="width:20%;text-align:left;vertical-align:middle">
                                                <label>Address </label></td>
                                            <td style="width:2%;"></td>
                                            <td style="width:60%;">
                                   <asp:TextBox ID="txtEnAdd" runat="server"  placeholder="Address"></asp:TextBox></td>


                                             <td style="vertical-align:middle;text-align:left">
                                                <label>Your Role </label></td>
                                            <td style="width:2%;"></td>
                                            <td style="width:20%;">
                                                
                                               <asp:TextBox ID="txtRole" runat="server"  placeholder="Your Role"></asp:TextBox>
                                                

                                            </td>

                                                
                                                </td>

                                            
                                        </tr>
                                 
                                 
                                  <tr>
                                            <td style="width:20%;text-align:left;vertical-align:middle">
                                                <label>Website URL</label></td>
                                            <td style="width:2%;"></td>
                                            <td style="width:60%;">
                                   <asp:TextBox ID="txtWebSite" runat="server"  placeholder="Website URL"></asp:TextBox></td>


                                             <td style="vertical-align:middle">
                                                <label> </label></td>
                                            <td style="width:2%;"></td>
                                            <td >
                                              </td>

                                                
                                                </td>

                                            
                                        </tr>

                        
                            
                           
                                 
                                 
                                     </td>
                            </tr>
                                
                            <tr>
                                <td colspan="3" style="height:10px;width:100%"></td>
                            </tr>
                           
                           
                                     </asp:Panel>
                            <asp:Panel ID="PnlFurther" runat="server" Visible="false">

                                 <tr>
                                            <td style="width:20%;text-align:left;vertical-align:middle">
                                                <label>College Name</label><asp:RequiredFieldValidator ID="RetxtInstitude" ControlToValidate="txtInstitude" ValidationGroup="g1" runat="server" ErrorMessage="*" ></asp:RequiredFieldValidator></td>
                                            <td style="width:2%;"></td>
                                            <td style="width:60%;text-align:left">
                                                
                                   <asp:TextBox ID="txtInstitude" runat="server"  placeholder="College Name"></asp:TextBox>
                                         
                                       </td>
                                             <td style="vertical-align:middle;text-align:left">
                                                <label>Address</label></td>
                                            <td style="width:2%;"></td>
                                            <td >
                                                 <asp:TextBox ID="txtFurAddress" runat="server"  placeholder="Address"></asp:TextBox></td>

                                                
                                                </td>

                                            
                                        </tr>
                                 
                                  <tr>
                                            <td style="width:20%;text-align:left;vertical-align:middle">
                                                <label>Website URL </label></td>
                                            <td style="width:2%;"></td>
                                            <td style="width:60%;">
                                   <asp:TextBox ID="txtWebURL" runat="server"  placeholder="University Website URL"></asp:TextBox></td>


                                             <td style="vertical-align:middle;text-align:left;">
                                                <label>Programme</label> <asp:RequiredFieldValidator ID="RetxtFurProgramme" ControlToValidate="txtProgram"  ValidationGroup="g1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                            <td style="width:2%;"></td>
                                            <td style="width:20%;">
                                                
                                               <asp:TextBox ID="txtProgram" runat="server"  placeholder="Programme Name "></asp:TextBox>
                                                

                                            </td>

                                                
                                                </td>

                                            
                                        </tr>
                                 
                                 
                                  <tr>
                                            <td style="width:20%;text-align:left;vertical-align:middle">
                                                <label>Admission Year </label></td>
                                            <td style="width:2%;"></td>
                                            <td style="width:60%;">
                                   <asp:TextBox ID="txtAdmissionYear" runat="server"  placeholder="Admission Year"></asp:TextBox></td>


                                             <td style="vertical-align:middle;text-align:left">
                                                <label>Graduation Year</label></td>
                                            <td style="width:2%;"></td>
                                            <td >
                                               <asp:TextBox ID="txtExpectedGraduationYear" runat="server"  placeholder="Expected Graduation Year" >                                             
                                               </asp:TextBox></td>

                                                
                                                </td>

                                            
                                        </tr>
                                 
                                  <tr>
                                            <td style="width:20%;text-align:left;vertical-align:middle">
                                                <label>Further Plans </label></td>
                                            <td style="width:2%;"></td>
                                            <td style="width:60%;">
                                   <asp:TextBox ID="txtFurPlan" runat="server"  placeholder="Further Plans (Short Paragraph)"></asp:TextBox></td>
                                       

                                             <td style="vertical-align:middle">
                                                <label> </label></td>
                                            <td style="width:2%;"></td>
                                            <td >
                                              
                                                  
                                                
                                                </td>

                                            
                                        </tr>



                            
                            </asp:Panel>
                            <asp:Panel ID="UnEmployee" runat="server" Visible="false">

                            </asp:Panel>

                                         <tr>
                                <td colspan="3" style="height:10px;width:100%"></td>
                            </tr> <tr>
                                <td colspan="3" style="height:10px;width:100%"></td>
                            </tr> <tr>
                                <td colspan="3" style="height:10px;width:100%"></td>
                            </tr>
                                     
                            
                            
                            <tr>
                                            <td style="width:20%;text-align:left;vertical-align:middle">
                                                <label>Current City </label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtLocation"  ValidationGroup="g1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width:2%;"></td>
                                            <td style="width:60%;">
                                   <asp:TextBox ID="txtLocation" runat="server"  placeholder="Current City" MaxLength ="30"  ></asp:TextBox>
                                                   <asp:FilteredTextBoxExtender ID="flttxtatt" runat="server" FilterMode="ValidChars"
                                                           validChars="zxcvbnmlkjhgfdsaqwertyuiopQWERTYUIOPLKJHGFDSAZXCVBNM"
                                                            TargetControlID="txtLocation">
                                                        </asp:FilteredTextBoxExtender>

                                             <td style="vertical-align:middle">
                                                <label>Current Country </label>
                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtCountry"  ValidationGroup="g1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                             </td>
                                            <td style="width:2%;"></td>
                                            <td style="width:60%;">                                           
                                                  <asp:TextBox ID="txtCountry" runat="server" placeholder="Current Country" TextMode="MultiLine" MaxLength ="30"  ></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterMode="ValidChars"
                                                           validChars="zxcvbnmlkjhgfdsaqwertyuiopQWERTYUIOPLKJHGFDSAZXCVBNM"
                                                            TargetControlID="txtCountry">
                                                        </asp:FilteredTextBoxExtender>
                                                
                                                </td>

                                            
                                        </tr>
                            
                                         <tr>
                                            <td style="width:20%;text-align:left" colspan="2">
                                                <label>SIGNIFICANT ACHIEVEMENTS:</label></td>
                                            
                                            <td >
                                                <asp:TextBox ID="txtSA" runat="server" MaxLength="200" Width="50%"  ></asp:TextBox></td>
                                             <td></td>
                                             <td></td>
                                             <td style="text-align:left">
                                                 <asp:Button ID="btnsave" runat="server" Text="Submit" BackColor="LightGreen" Font-Bold="true" ForeColor="White" Width="185px"  ValidationGroup="g1" OnClick="btnsave_Click" />
                                             </td>
                                        </tr>
                           
                                       
                        </table>
                                </div>

                         </fieldset>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                                
</asp:Content>

