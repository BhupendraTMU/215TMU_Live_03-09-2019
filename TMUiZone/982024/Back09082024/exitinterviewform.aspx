<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="exitinterviewform.aspx.cs" Inherits="Faculty_exitinterviewform" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type = "text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Are You sure? After Submit You not able to modify !")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>

    <style type="text/css">
        .auto-style1 {
            height: 16px;
        }
        .auto-style3 {
            width: 101px;
        }
        .auto-style4 {
            width: 135px;
        }
        .auto-style7 {
            width: 2px;
        }
        .auto-style8 {
            width: 130px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Panel ID="Panel1" runat="server">
    <table cellpadding="0px" cellspacing="0px">
        <tr>
            <td style="height: 10px"></td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp; 
                <asp:Label ID="lblHeader" runat="server"
                    Text=" RESIGNATION FORM		" Font-Size="15pt" ForeColor="#093A62"
                    Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label></td>
        </tr>
        <tr>
            <td style="height: 30px"></td>
        </tr>


        <tr>
            <td>

                  <div>
                 <table cellpadding="0px" cellspacing="0px" align="center">
                             <caption>
                                     <tr>   
                                         <td style="width:140px"></td>                                      
                                         <td style="font-size:large" class="auto-style8">&nbsp; HOD_Status : </td>                                         
                                         <td class="auto-style4">
                                             <asp:Label ID="lblhodstatus" runat="server" ForeColor="Red" Text="" Font-Size="Large"></asp:Label>
                                         </td>  
                                         <td></td>                                       
                                         <td style="font-size:large" class="auto-style3">HR_Status : </td>
                                         <td class="auto-style7"></td>
                                         <td>
                                             <asp:Label ID="lblhrstatus" runat="server" ForeColor="Red" Text="" Font-Size="Large"></asp:Label>
                                         </td>
                                         <td>&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                                          <td style="font-size:large" class="auto-style3">Registrar_Status: </td>
                                         <td class="auto-style7"></td>
                                         <td>
                                             <asp:Label ID="lblregistrar" runat="server" ForeColor="Red" Text="" Font-Size="Large"></asp:Label>
                                         </td>
                                         <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                                           <td  style="font-size:large;" class="auto-style3" runat="server" id="vcss" visible="false"> VC_Status: </td>
                                         <td class="auto-style7"></td>
                                         <td>
                                             <asp:Label ID="lblvcstatus" runat="server" Visible="false" ForeColor="Red" Text="" Font-Size="Large"></asp:Label>
                                         </td>

                                 </tr>
                                                                    </caption>
                                                                </table>
                </div>
                <br />
                <table cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td style="width: 120px"></td>
                        <td>
                            <table cellpadding="0px" cellspacing="0px">

                                 
                                <tr>
                                    <td class="auto-style1"><strong>Personal Details</strong></td>
                                  
                                </tr>

                                <tr>
                                    <td style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td style="background-color: #808080; height: 1px"></td>
                                </tr>
                                <tr>
                                    <td style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td>Employee Name : </td>
                                                <td style="width: 107px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtEmployeeName" runat="server" Width="400px" Enabled="False"></asp:TextBox>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td>Employee Code : </td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtEmployeeCode" runat="server" Width="195px" Enabled="False"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="background-color: #808080; height: 1px"></td>
                                </tr>

                                <tr>
                                    <td>
                                        <table cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td>Designation :</td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="lbldesignation" runat="server" Enabled="False" Width="720px"></asp:TextBox></td>
                                            </tr>

                                            <tr>
                                                <td colspan="3" style="background-color: #808080; height: 1px"></td>
                                            </tr>


                                            <tr>
                                                <td>Institution :</td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="lblInstitution" runat="server" Enabled="false" Width="720px"></asp:TextBox></td>
                                            </tr>

                                            <tr>
                                                <td colspan="3" style="background-color: #808080; height: 1px"></td>
                                            </tr>
                                            <tr>
                                                <td>Name of Current Director / HOD :</td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="lblnameofHOD" runat="server" Enabled="false" Width="720px"></asp:TextBox></td>
                                            </tr>

                                            <tr>
                                                <td colspan="3" style="background-color: #808080; height: 1px"></td>
                                            </tr>
                                            <tr>
                                                <td>Date of Joining  :</td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="lblDateofJoining" runat="server" Enabled="false" Width="720px"></asp:TextBox></td>
                                            </tr>

                                            <tr>
                                                <td colspan="3" style="background-color: #808080; height: 1px"></td>
                                            </tr>
                                            <tr>
                                                <td>Date of Applying for  Resignation :</td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtapplyingresignation" runat="server"   BorderColor="Black" Width="200px" onkeydown="return false;" autocomplete="off" oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" AutoPostBack="True"></asp:TextBox>
                                                    <%--<asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtapplyingresignation" Format="dd MMM yyyy"></asp:CalendarExtender>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtapplyingresignation" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>--%>
                                           </td>
                                                     </tr>

                                            <tr>
                                                <td colspan="3" style="background-color: #808080; height: 1px"></td>
                                            </tr>
                                            <tr>
                                                <td>Notice Period :</td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtnoticeperiod" runat="server" Width="120px" Enabled="false"></asp:TextBox></td>
                                             
                                            </tr>
                                              <tr>
                                                <td colspan="3" style="background-color: #808080; height: 1px"></td>
                                            </tr>
                                            <tr>
                                                <td>Employee Type :</td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtemployeetype" runat="server" Width="120px" Enabled="false"></asp:TextBox></td>
                                             
                                            </tr>



                                            <tr>
                                                <td colspan="3" style="background-color: #808080; height: 1px"></td>
                                            </tr>

                                        </table>

                                    </td>
                                </tr>
                                <tr>
                                    <td><strong>Reasons For Job Switch (All applicable reasons with remarks can be mentioned)		
                                    </strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="background-color: #808080; height: 1px"></td>
                                </tr>

                                <tr>
                                    <td>
                                        <table cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td>Better Profile :</td>
                                                <td style="width: 1px"></td>
                                                <td style="width: 1px; background-color: #808080"></td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtbetterprofile" runat="server" Width="800px" MaxLength="200"></asp:TextBox></td>
                                            </tr>

                                            <tr>
                                                <td colspan="5" style="background-color: #808080; height: 1px"></td>
                                            </tr>


                                            <tr>
                                                <td>Better Emoluments :</td>
                                                <td style="width: 10px"></td>
                                                <td style="width: 1px; background-color: #808080"></td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtBetterEmoluments" runat="server" Width="800px" MaxLength="200"></asp:TextBox></td>
                                            </tr>

                                            <tr>
                                                <td colspan="5" style="background-color: #808080; height: 1px"></td>
                                            </tr>

                                            <tr>
                                                <td>Personal Reason :</td>
                                                <td style="width: 10px"></td>
                                                <td style="width: 1px; background-color: #808080"></td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtPersonalReason" runat="server" Width="800px" MaxLength="200"></asp:TextBox></td>
                                            </tr>

                                            <tr>
                                                <td colspan="5" style="background-color: #808080; height: 1px"></td>
                                            </tr>
                                        </table>

                                    </td>
                                </tr>


                                <tr>
                                    <td>
                                        <table cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td><strong>Any other reason :</strong> </td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtanyotherreason" runat="server" Width="823px" MaxLength="250"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="height: 1px; background-color: #808080"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td>Name of Organization Joining : </td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtNameofOrgJoining" runat="server" Width="748px" MaxLength="150"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="height: 1px; background-color: #808080"></td>
                                </tr>

                                <tr>
                                    <td>What triggered you to look for a change :  </td>
                                </tr>

                                <tr>
                                    <td style="height: 1px; background-color: #808080"></td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:TextBox ID="txttriggerdlookforchange" runat="server" Width="948px" MaxLength="250"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="height: 1px; background-color: #808080"></td>
                                </tr>

                                <tr>
                                    <td>Good/Enjoyable experiences with TMU: </td>
                                </tr>

                                <tr>
                                    <td style="height: 1px; background-color: #808080"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtGoodwithTMU" runat="server" Width="948px" MaxLength="250"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 1px; background-color: #808080"></td>
                                </tr>

                                <tr>
                                    <td>Difficult/upsetting experiences with TMU: </td>
                                </tr>

                                <tr>
                                    <td style="height: 1px; background-color: #808080"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtDifficultwithtmu" runat="server" Width="948px" MaxLength="250"></asp:TextBox>
                                    </td>
                                </tr>



                                <tr>
                                    <td style="height: 1px; background-color: #808080"></td>
                                </tr>

                                <tr>
                                    <td><strong>Please complete Responses (Unsatisfactory; Satisfactory; Good; Excellent)		
                                    </strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 1px; background-color: #808080"></td>
                                </tr>


                                <tr>
                                    <td>

                                        <table cellpadding="0px" cellspacing="0px">

                                            <tr>
                                                <td><strong>Questions </strong></td>
                                                <td style="width: 10px"></td>
                                                <td style="background-color: #808080; width: 1px"></td>
                                                <td style="width: 10px"></td>
                                                <td><strong>Response </strong></td>
                                                <td style="width: 10px"></td>
                                                <td style="background-color: #808080; width: 1px"></td>
                                                <td style="width: 10px"></td>
                                                <td><strong>Remarks</strong></td>
                                                <td style="width: 10px"></td>
                                                <td style="background-color: #808080; width: 1px"></td>
                                            </tr>


                                            <tr>
                                                <td colspan="11" style="background-color: #808080; height: 1px"></td>
                                            </tr>


                                            <tr>
                                                <td>Overall rating of Teerthankar Mahaveer University  as an organization </td>
                                                <td style="width: 10px"></td>
                                                <td style="background-color: #808080; width: 1px"></td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtovalallratingResponse" runat="server" MaxLength="50"></asp:TextBox>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td style="background-color: #808080; width: 1px"></td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtovalallratingRemarks" runat="server" Width="300px" MaxLength="250"></asp:TextBox></td>
                                                <td style="width: 10px"></td>
                                                <td style="background-color: #808080; width: 1px"></td>
                                            </tr>


                                            <tr>
                                                <td colspan="11" style="background-color: #808080; height: 1px"></td>
                                            </tr>

                                            <tr>
                                                <td>The performance measurement  and the feedback system  </td>
                                                <td style="width: 10px"></td>
                                                <td style="background-color: #808080; width: 1px"></td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtperformancemeasurementResponse" runat="server" MaxLength="50"></asp:TextBox>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td style="background-color: #808080; width: 1px"></td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtperformancemeasurementRemarks" runat="server" Width="300px" MaxLength="250"></asp:TextBox></td>
                                                <td style="width: 10px"></td>
                                                <td style="background-color: #808080; width: 1px"></td>
                                            </tr>


                                            <tr>
                                                <td colspan="11" style="background-color: #808080; height: 1px"></td>
                                            </tr>

                                            <tr>
                                                <td>The communication within the organization
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td style="background-color: #808080; width: 1px"></td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtCommunicationResponse" runat="server" MaxLength="50"></asp:TextBox>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td style="background-color: #808080; width: 1px"></td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtCommunicationRemarks" runat="server" Width="300px" MaxLength="250"></asp:TextBox></td>
                                                <td style="width: 10px"></td>
                                                <td style="background-color: #808080; width: 1px"></td>
                                            </tr>


                                            <tr>
                                                <td colspan="11" style="background-color: #808080; height: 1px"></td>
                                            </tr>

                                            <tr>
                                                <td>Recruitment and Induction procedures in TMU
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td style="background-color: #808080; width: 1px"></td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtRecruitmentResponse" runat="server" MaxLength="50"></asp:TextBox>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td style="background-color: #808080; width: 1px"></td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtRecruitmentRemarks" runat="server" Width="300px" MaxLength="250"></asp:TextBox></td>
                                                <td style="width: 10px"></td>
                                                <td style="background-color: #808080; width: 1px"></td>
                                            </tr>


                                            <tr>
                                                <td colspan="11" style="background-color: #808080; height: 1px"></td>
                                            </tr>


                                            <tr>
                                                <td>Willingness of superiors to listen and help in solving problems
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td style="background-color: #808080; width: 1px"></td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtWillingnessResponse" runat="server" MaxLength="50"></asp:TextBox>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td style="background-color: #808080; width: 1px"></td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtWillingnessRemarks" runat="server" Width="300px" MaxLength="250"></asp:TextBox></td>
                                                <td style="width: 10px"></td>
                                                <td style="background-color: #808080; width: 1px"></td>
                                            </tr>


                                            <tr>
                                                <td colspan="11" style="background-color: #808080; height: 1px"></td>
                                            </tr>

                                            <tr>
                                                <td>Recruitment and Induction procedures in TMU
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td style="background-color: #808080; width: 1px"></td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtRecruitment_Proc_Response" runat="server" MaxLength="50"></asp:TextBox>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td style="background-color: #808080; width: 1px"></td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtRecruitment_Proc_Remarks" runat="server" Width="300px" MaxLength="250"></asp:TextBox></td>
                                                <td style="width: 10px"></td>
                                                <td style="background-color: #808080; width: 1px"></td>
                                            </tr>


                                            <tr>
                                                <td colspan="11" style="background-color: #808080; height: 1px"></td>
                                            </tr>

                                            <tr>
                                                <td>The working environment
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td style="background-color: #808080; width: 1px"></td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtWorkingEnviron_Response" runat="server" MaxLength="50"></asp:TextBox>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td style="background-color: #808080; width: 1px"></td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtWorkingEnviron_Remarks" runat="server" Width="300px" MaxLength="250"></asp:TextBox></td>
                                                <td style="width: 10px"></td>
                                                <td style="background-color: #808080; width: 1px"></td>
                                            </tr>


                                            <tr>
                                                <td colspan="11" style="background-color: #808080; height: 1px"></td>
                                            </tr>


                                            <tr>
                                                <td>Growth opportunities

                                                </td>
                                                <td style="width: 10px"></td>
                                                <td style="background-color: #808080; width: 1px"></td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtgrowthOpportuniti_Response" runat="server" MaxLength="50"></asp:TextBox>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td style="background-color: #808080; width: 1px"></td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtgrowthOpportuniti_Remarks" runat="server" Width="300px" MaxLength="250"></asp:TextBox>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td style="background-color: #808080; width: 1px"></td>
                                            </tr>


                                            <tr>
                                                <td colspan="11" style="background-color: #808080; height: 1px"></td>
                                            </tr>


                                            <tr>
                                                <td>Effectiveness of Appraisal process

                                                </td>
                                                <td style="width: 10px"></td>
                                                <td style="background-color: #808080; width: 1px"></td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txteffectiveness_Response" runat="server" MaxLength="50"></asp:TextBox>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td style="background-color: #808080; width: 1px"></td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txteffectiveness_Remarks" runat="server" Width="300px" MaxLength="250"></asp:TextBox></td>
                                                <td style="width: 10px"></td>
                                                <td style="background-color: #808080; width: 1px"></td>
                                            </tr>


                                            <tr>
                                                <td colspan="11" style="background-color: #808080; height: 1px"></td>
                                            </tr>


                                        </table>

                                    </td>
                                </tr>



                                <tr>
                                    <td>
                                        <table cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td><strong>Any other Comment :</strong> </td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:TextBox ID="txtAnyOtherComment" runat="server" Width="800px" MaxLength="250"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="background-color: #808080; height: 1px"></td>
                                </tr>
                                <tr>
                                    <td><strong>Contact Details </strong>
                                        <br />
                                        <br />
                                        <br />
                                    </td>
                                </tr>

                                <tr>
                                    <td>

                                        <table cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td>
                                                    <table cellpadding="0px" cellspacing="0px">
                                                        <tr>
                                                            <td>Mobile No : </td>
                                                            <td style="width: 10px"></td>
                                                            <td>
                                                                <asp:TextBox ID="txtMobileNo" runat="server" Width="300px" Enabled="False"></asp:TextBox>
                                                            </td>
                                                        </tr>


                                                        <tr>
                                                            <td>Official Email-id : </td>
                                                            <td style="width: 10px"></td>
                                                            <td>
                                                                <asp:TextBox ID="txtofficial" runat="server" Width="300px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Personal Email-id : </td>
                                                            <td style="width: 10px"></td>
                                                            <td>
                                                                <asp:TextBox ID="txtEmail" runat="server" Width="300px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 400px"></td>
                                                <td style="vertical-align: bottom"><strong>Signature</strong> </td>
                                            </tr>
                                        </table>


                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td style="background-color: #808080; height: 1px"></td>
                                </tr>


                                <tr>
                                    <td style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                                <td>
                                                    </td>
                                            </tr>
                                        </table>


                                    </td>
                                </tr>

                                <tr>
                                    <td style="height: 10px"></td>
                                </tr>

                                <tr>
                                    <td style="background-color: #808080; height: 1px"></td>
                                </tr>
                                <tr>
                                    <td style="height: 90px">
                                        Please fill Interview form and after completion, cilck here for submit&nbsp;&nbsp;&nbsp; <br /> <asp:Label ID="lbltxt" runat="server" Text="Note: No changes will  be accepted after Submission." ForeColor="Red"></asp:Label>&nbsp;<br /> <asp:Label ID="Label1" runat="server" Text="Note: No leaves shall be applicable After Resignation." ForeColor="Red"></asp:Label>
                                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn"   OnClientClick = "Confirm()" OnClick="btnsubmit_Click"  /> </td>
                                       
                                     </tr>
                               
                            </table>
                        </td>
                        <td style="width: 10px"></td>
                    </tr>
                </table>


            </td>
        </tr>

    </table>

    </asp:Panel>
    <br />
    <br />


</asp:Content>

