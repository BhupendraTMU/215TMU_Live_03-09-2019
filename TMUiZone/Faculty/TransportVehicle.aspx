<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="TransportVehicle.aspx.cs" Inherits="Faculty_TransportVehicle" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

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

            <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
        Text="Transport Vehicle Requisition" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
         <asp:ScriptManager ID="ty" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="fe" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <br />
            <fieldset class="boxBodyInner"> 
               
                       <table cellpadding="0px" cellspacing="0px"> 
                           
                           <tr>
                               <td colspan="20" style="text-align:right"> 
                                   
                         <table cellpadding="0px" cellspacing="0px">

                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label6" runat="server" Text=" Note : Approval Authority : 1 ) " ForeColor="Green" Font-Size="12pt"></asp:Label>
                                                        <asp:Label ID="lblfirstApproval" runat="server" ForeColor="#FF3300" Font-Size="10pt"></asp:Label>
                                                        .&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 2) </td>
                                                    <td>
                                                        <asp:Label ID="lblSecondApproval" runat="server" ForeColor="#FF3300" Font-Size="10pt"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="lblApprovalAuthority1" runat="server" Text="Approval authority not tag . Please contact HR admin ,otherwise CO can't apply." Visible="False" Font-Bold="True" ForeColor="Red" Font-Size="11pt"></asp:Label></td>
                                                </tr>

                                       </table>

                               </td>

                           </tr>
                                                    <tr> 
      <td colspan="20" style="text-align:right">
                                <table cellpadding="0px" cellspacing="0px">       
                               <tr> 
                                   <td colspan="15" style="height:20px">
                                    </td>
                                </tr>
                                     
                                    </table>
                </td>
                </tr>

                                         <tr> 
      <td colspan="20" style="text-align:right">
                                <table cellpadding="0px" cellspacing="0px">       
                               <tr> 
                                    <td><label style="line-height:25px">Department</label> </td> 
                                    <td style="width:10px"> </td> 
                                    <td>
                          <asp:TextBox ID="lblDepartment" runat="server" Enabled="False" Width="150px"></asp:TextBox>  
                        
                                    </td> 
                                    <td style="width:25px"> </td> 
                                    <td style="width:100px;text-align:left"><label style="line-height:25px">Date</label> </td> 
                                    <td style="width:10px"> </td>
                                    <td>
                           <asp:TextBox ID="lbldate" runat="server" Enabled="False" Width="150px"></asp:TextBox>  
                               
                                        
                                    </td> 
                                    <td style="width:25px"> </td> 
                                    <td><label style="line-height:25px">Indented By</label> </td> 
                                    <td style="width:10px"> </td>
                                    <td> 

                                   <asp:TextBox ID="txtIndentedby" runat="server" Enabled="False"  Width="  160px"></asp:TextBox>
                                    </td>
                                    <td style="width:25px"> </td>
                                    <td style="width:112px;text-align:left">                
                                        <label style="line-height:25px">Mobile No</label>
                                   </td>
                                      <td style="width:10px"> </td>
                                    <td>
                                <asp:TextBox ID="lblmoNo" runat="server" Enabled="False" Width="150px"></asp:TextBox> 
                               
                                    </td>
                                </tr>
                                     
                                    </table>
                </td>
                </tr>
                                                     <tr> 
      <td colspan="20" style="text-align:right">
                                <table cellpadding="0px" cellspacing="0px">       
                               <tr> 
                                   <td colspan="15" style="height:20px">
                                    </td>
                                </tr>
                                     
                                    </table>
                </td>
                </tr>
                                          <tr> 
      <td colspan="20" style="text-align:right">
                                <table cellpadding="0px" cellspacing="0px">       
                               <tr> 
                                   <td style="width:75px;text-align:left"><label style="line-height:25px">Purpose</label> </td> 
                                   <td style="width:10px"></td>
                                   <td> 
               <asp:TextBox ID="txtPurpose" runat="server"  MaxLength="90" Width="150px"></asp:TextBox>
 <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPurpose" SetFocusOnError="true" ErrorMessage="**" ValidationGroup="TV"  Font-Bold="true" ForeColor="RED"></asp:RequiredFieldValidator>
                                   </td>  
                                    <td style="width:10px"></td> 
                                    <td style="width:100px;text-align:left"><label style="line-height:25px">Type of Vehicle</label> </td> 
                                    <td style="width:10px"></td>
                                    <td>
 <asp:DropDownList ID="ddltypVehicle" runat="server" Width="150px" style="line-height:25px">
                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>                                       
                                       <asp:ListItem Value="2" Text="Bus"></asp:ListItem>
                                       <asp:ListItem Value="3" Text="Car"></asp:ListItem>
                                       <asp:ListItem Value="4" Text="Truck"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="Tractor"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="Ambulance"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="Generator"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="Van"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="Tempo"></asp:ListItem>
                                        <asp:ListItem Value="10" Text="Tata"></asp:ListItem>
                                        <asp:ListItem Value="11" Text="Trolly"></asp:ListItem>
                                         <asp:ListItem Value="12" Text="Earth M E"></asp:ListItem>
                                        <asp:ListItem Value="13" Text="Others"></asp:ListItem>
                                    </asp:DropDownList>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddltypVehicle" ErrorMessage="**"  InitialValue="0" SetFocusOnError="True" ValidationGroup="TV"></asp:RequiredFieldValidator>
                                    
                               
                                        
                                    </td> 
                                    <td style="width:10px"> </td> 
                                    <td><label style="line-height:10px">Destination</label> </td> 
                                    <td style="width:10px"> </td>
                                   <td> 
                                    <asp:DropDownList ID="ddlDestination" runat="server" Width="160px"></asp:DropDownList>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDestination" InitialValue="" ErrorMessage="**" SetFocusOnError="True" ValidationGroup="TV"></asp:RequiredFieldValidator>
                                                                       </td> 
                                   <td style="width:10px"> </td>
                                   <td><label style="line-height:25px">Required At Place</label> </td> 
                                   <td style="width:10px"> </td>
                                   <td> 
                                        <asp:TextBox ID="txtPlace" runat="server"  MaxLength="59" Width="150px"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPlace" InitialValue="" ErrorMessage="**" SetFocusOnError="True" ValidationGroup="TV"></asp:RequiredFieldValidator>
                                   
                                   </td> 

                                </tr>
                                     
                                    </table>
                </td>
                </tr>

                                                                                <tr> 
      <td colspan="20" style="text-align:right">
                                <table cellpadding="0px" cellspacing="0px">       
                               <tr> 
                                   <td colspan="15" style="height:20px">
                                    </td>
                                </tr>
                                     
                                    </table>
                </td>
                </tr>

                                                     <tr> 
      <td colspan="20" style="text-align:right">
                                <table cellpadding="0px" cellspacing="0px">       
                                <tr> 
                                    <td style="width:75px;text-align:left">                
                                        <label style="line-height:10px">From Date</label>
                                    </td> 
                                   <td style="width:10px"> </td>
                                   <td> 
                                   <b>
                                <asp:TextBox runat="server" ID="txtFromtDate" CssClass="form-control input-sm" Width="110px" AutoPostBack="true" OnTextChanged="txtFromtDate_TextChanged" autocomplete="off" ToolTip="From Date" onkeypress="return false"
                                    onKeyDown="preventBackspace();" placeholder="From Date"></asp:TextBox>&nbsp&nbsp
                                <asp:Image src="../Images/Calendar.png" runat="server" Height="30px" Width="30px" alt="" ID="fdate" />
                               <asp:CalendarExtender ID="cleFromDate" Format="dd MMM yyyy" runat="server"  CssClass="cal_Theme1"
                                    PopupButtonID="fdate" Enabled="true"  TargetControlID="txtFromtDate" />
                        <asp:RequiredFieldValidator ID="reqFromDate" runat="server" ControlToValidate="txtFromtDate" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="TV"  Font-Bold="true" ForeColor="RED"></asp:RequiredFieldValidator>
                      
                                    </b>
                                    </td> 
                                   <td style="width:10px"> </td>
                                   <td style="width:100px;text-align:left"><label style="line-height:25px">To Date</label> </td> 
                                   <td style="width:10px"> </td>
                                   <td> 
   <asp:TextBox runat="server" ID="txtToDate" CssClass="form-control input-sm" Width="110px"  AutoPostBack="true" OnTextChanged="txtToDate_TextChanged" autocomplete="off" ToolTip="To Date" onkeypress="return false" 
                                    onKeyDown="preventBackspace();" placeholder="To Date"></asp:TextBox>&nbsp&nbsp
                                <asp:Image src="../Images/Calendar.png" runat="server" Height="30px" Width="30px" alt="" ID="tdate" />
                               <asp:CalendarExtender ID="cleToDate" Format="dd MMM yyyy" runat="server"  CssClass="cal_Theme1"
                                    PopupButtonID="tdate" Enabled="true" TargetControlID="txtToDate" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtToDate" SetFocusOnError="true" ErrorMessage="**" ValidationGroup="TV" Font-Bold="true" ForeColor="RED"></asp:RequiredFieldValidator>                   
                    &nbsp
                                   </td> 
                                   <td style="width:10px"> </td>
                                   <td><label style="line-height:25px">From Time</label>&nbsp;&nbsp;</td>
                                         <td style="width:10px"> </td>
                                        <td>                                            
                                           <asp:DropDownList ID="DdFhour" runat="server" Width="50px">
                                                <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                                <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                                <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                                <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                                <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                                <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                                <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                                <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                                <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                                <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                            </asp:DropDownList>
                                              <asp:DropDownList ID="DDFMinit" runat="server" Width="50px">
                                                <asp:ListItem Value="00" Text="00"></asp:ListItem>
                                                <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                                <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                                <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                                <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                                <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                                <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                                <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                                <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                                <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                                <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                                <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                                <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                                <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                                <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                                <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                                <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                                <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                                <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                                <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                                <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                                <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                                <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                                <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                                <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                                <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                                <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                                <asp:ListItem Value="32" Text="32"></asp:ListItem>
                                                <asp:ListItem Value="33" Text="33"></asp:ListItem>
                                                <asp:ListItem Value="34" Text="34"></asp:ListItem>
                                                <asp:ListItem Value="35" Text="35"></asp:ListItem>
                                                <asp:ListItem Value="36" Text="36"></asp:ListItem>
                                                <asp:ListItem Value="37" Text="37"></asp:ListItem>
                                                <asp:ListItem Value="38" Text="38"></asp:ListItem>
                                                <asp:ListItem Value="39" Text="39"></asp:ListItem>
                                                <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                                <asp:ListItem Value="41" Text="41"></asp:ListItem>
                                                <asp:ListItem Value="42" Text="42"></asp:ListItem>
                                                <asp:ListItem Value="43" Text="43"></asp:ListItem>
                                                <asp:ListItem Value="44" Text="44"></asp:ListItem>
                                                <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                                <asp:ListItem Value="46" Text="46"></asp:ListItem>
                                                <asp:ListItem Value="47" Text="47"></asp:ListItem>
                                                <asp:ListItem Value="48" Text="48"></asp:ListItem>
                                                <asp:ListItem Value="49" Text="49"></asp:ListItem>
                                                <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                                <asp:ListItem Value="51" Text="51"></asp:ListItem>
                                                <asp:ListItem Value="52" Text="52"></asp:ListItem>
                                                <asp:ListItem Value="53" Text="53"></asp:ListItem>
                                                <asp:ListItem Value="54" Text="54"></asp:ListItem>
                                                <asp:ListItem Value="55" Text="55"></asp:ListItem>
                                                <asp:ListItem Value="56" Text="56"></asp:ListItem>
                                                <asp:ListItem Value="57" Text="57"></asp:ListItem>
                                                <asp:ListItem Value="58" Text="58"></asp:ListItem>
                                                <asp:ListItem Value="59" Text="59"></asp:ListItem>
                                                
                                            </asp:DropDownList>
                                              <asp:DropDownList ID="ddFAM" runat="server" Width="50px">
                                                <asp:ListItem Value="AM" Text="AM"></asp:ListItem>
                                                <asp:ListItem Value="PM" Text="PM"></asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddFAM" ErrorMessage="**" InitialValue="" SetFocusOnError="True" ValidationGroup="TV"></asp:RequiredFieldValidator>
                                        </td>
                                      <td style="width:20px"></td>
                                        <td style="width:100px;text-align:left"><label style="line-height:25px">Till Time </label></td>
                                        <td style="width:10px"> </td>
                                        <td>
                                              <asp:DropDownList ID="ddThour" runat="server" Width="50px">
                                                <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                                <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                                <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                                <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                                <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                                <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                                <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                                <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                                <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                                <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                            </asp:DropDownList>
                                              <asp:DropDownList ID="ddTminit" runat="server" Width="50px">
                                                <asp:ListItem Value="00" Text="00"></asp:ListItem>
                                                <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                                <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                                <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                                <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                                <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                                <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                                <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                                <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                                <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                                <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                                <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                                <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                                <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                                <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                                <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                                <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                                <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                                <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                                <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                                <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                                <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                                <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                                <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                                <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                                <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                                <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                                <asp:ListItem Value="32" Text="32"></asp:ListItem>
                                                <asp:ListItem Value="33" Text="33"></asp:ListItem>
                                                <asp:ListItem Value="34" Text="34"></asp:ListItem>
                                                <asp:ListItem Value="35" Text="35"></asp:ListItem>
                                                <asp:ListItem Value="36" Text="36"></asp:ListItem>
                                                <asp:ListItem Value="37" Text="37"></asp:ListItem>
                                                <asp:ListItem Value="38" Text="38"></asp:ListItem>
                                                <asp:ListItem Value="39" Text="39"></asp:ListItem>
                                                <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                                <asp:ListItem Value="41" Text="41"></asp:ListItem>
                                                <asp:ListItem Value="42" Text="42"></asp:ListItem>
                                                <asp:ListItem Value="43" Text="43"></asp:ListItem>
                                                <asp:ListItem Value="44" Text="44"></asp:ListItem>
                                                <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                                <asp:ListItem Value="46" Text="46"></asp:ListItem>
                                                <asp:ListItem Value="47" Text="47"></asp:ListItem>
                                                <asp:ListItem Value="48" Text="48"></asp:ListItem>
                                                <asp:ListItem Value="49" Text="49"></asp:ListItem>
                                                <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                                <asp:ListItem Value="51" Text="51"></asp:ListItem>
                                                <asp:ListItem Value="52" Text="52"></asp:ListItem>
                                                <asp:ListItem Value="53" Text="53"></asp:ListItem>
                                                <asp:ListItem Value="54" Text="54"></asp:ListItem>
                                                <asp:ListItem Value="55" Text="55"></asp:ListItem>
                                                <asp:ListItem Value="56" Text="56"></asp:ListItem>
                                                <asp:ListItem Value="57" Text="57"></asp:ListItem>
                                                <asp:ListItem Value="58" Text="58"></asp:ListItem>
                                                <asp:ListItem Value="59" Text="59"></asp:ListItem>
                                              
                                            </asp:DropDownList>
                                              <asp:DropDownList ID="ddTA" runat="server" Width="50px">
                                                <asp:ListItem Value="AM" Text="AM"></asp:ListItem>
                                                <asp:ListItem Value="PM" Text="PM"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddTA" InitialValue="" ErrorMessage="**" SetFocusOnError="True" ValidationGroup="TV"></asp:RequiredFieldValidator>
                                        </td>
                               </tr>
                                     
                                    </table>
                </td>
                </tr>

                                                                                <tr> 
      <td colspan="20" style="text-align:right">
                                <table cellpadding="0px" cellspacing="0px">       
                               <tr> 
                                   <td colspan="15" style="height:20px">
                                    </td>
                                </tr>
                                     
                                    </table>
                </td>
                </tr>

                           <tr> 
      <td colspan="20" style="text-align:right">
              <table cellpadding="0px" cellspacing="0px">  
                           
                               <tr>  
                                   
                                  <td style="width:120px;text-align:left"><label style="line-height:25px">No. of Passengers</label></td> 
                                    <td style="width:10px"> </td>
                                   <td style="width:150px;"><label style="line-height:25px"><asp:TextBox ID="txtNop" runat="server" OnTextChanged="txtNop_TextChanged" AutoPostBack="true"  MaxLength="3" Width="60px"></asp:TextBox>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidatorAccount" runat="server" ErrorMessage="*" ControlToValidate="txtNop" ValidationGroup="TV" ForeColor="Red"></asp:RequiredFieldValidator>
                                       <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNop" ErrorMessage="Please Enter Only Numbers" ValidationGroup="TV" ForeColor="Red" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>

                                       </td> 
                                  
                                    <td style="width:10px"> </td>
                                       <td><asp:FileUpload ID="FileUpload1" Width="180px" Visible="false"  runat="server" />
                                           <asp:Label ID ="lblFS" runat="server" Text="Max File Size 700 KB" ForeColor="Red" Visible="false" Font-Bold="true" ></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvflUpload" runat="server" Font-Bold="true" ForeColor="Red" Visible="false"  ControlToValidate="FileUpload1" ValidationGroup="TV"
                     ErrorMessage="* Upload Attachment" Enabled="false" ></asp:RequiredFieldValidator>                         

                                       </td>
                                    <td rowspan="9"></td>
                                   <td style="width:50px">  </td>
                                   <td> <asp:Button ID="btnSave" runat="server"  OnClick="btnSave_Click" ValidationGroup="TV" OnClientClick="if(!confirm('Do you want to submit'))return false;" Text="Send For Approval" /></td>
                                   
                                   
                                                
                               </tr>
                  </table>
          </td>
                               </tr>

                           
                    </table>  
         


         <br />
                    </fieldset>

               </ContentTemplate>
       <Triggers>
   <asp:PostBackTrigger ControlID="btnSave" />
            

            </Triggers>
        </asp:UpdatePanel>
     <fieldset class="boxBodyInner">
  <asp:GridView ID="GrdTransport" runat="server" CssClass="table table-striped table-bordered table-hover"  Style="width: 99%; margin-left: 2%; margin-right: 2%" EmptyDataText="No Data to display" AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr.no">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex +1 %>


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

                                 <asp:TemplateField HeaderText="Indented By" ItemStyle-HorizontalAlign="Left">
                          <ItemTemplate>
                              <asp:Label ID="lbllName" runat="server" Text='<%#Bind("[Indented By]") %>'></asp:Label>
                              
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

