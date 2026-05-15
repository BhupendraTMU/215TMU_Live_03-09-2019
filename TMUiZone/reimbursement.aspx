<%@ Page Title="" Language="C#" MasterPageFile="~/IndexMaster.master" AutoEventWireup="true" CodeFile="reimbursement.aspx.cs" Inherits="reimbursement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
    <script type="text/javascript">
    function Confirm_Delete() {
        var confirm_value = document.createElement("INPUT");
        confirm_value.type = "hidden";
        confirm_value.name = "confirm_value";
        if (confirm("Do you want to delete this record?")) {
            confirm_value.value = "Yes";
        } else {
            confirm_value.value = "No";
        }
        document.forms[0].appendChild(confirm_value);
    }


</script>
    <style  type="text/css">

            #grdLeavedetailwrap
{
width:866px;
height:100%;
overflow:scroll;
}

                    #grdReimEntry
{
width:866px;
height:100%;
overflow:scroll;
}

              #GRDSearchDetailwrap
{
width:866px;
height:100%;
overflow:scroll;
}


                    #GRDPendingApproval
{
width:866px;
height:100%;
overflow:scroll;
}


                    
                    #GRDClaimAprStatus
{
width:866px;
height:100%;
overflow:scroll;
}

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <fieldset class="boxBody">
 <asp:Label ID="Label1" runat="server" 
            Text="Claims" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 </fieldset>


    <fieldset class="boxBody"> 

<table cellpadding="0px" cellspacing="0px">  <tr>  <td  style="width:10px">  </td><td  style="width:200px" valign="top"> 

<table cellpadding="0px" cellspacing="0px" class="leftbg1" style="width:200px; height:430px">  <tr> <td style="width:10px"> </td> <td>


<table cellpadding="0px" cellspacing="0px" >
 <tr> <td style="height:10px"> </td></tr>
 <tr> <td class="leftmMenu">  <img src="logo/Star.png" /> 
    <asp:LinkButton ID="lnkreimbursmentApply" runat="server" 
         onclick="lnkreimbursmentApply_Click">  Claim Reimbursement </asp:LinkButton></td></tr>
    <tr> <td style="height:10px"> </td></tr>
     <tr> <td class="leftmMenu">   <img src="logo/Star.png" />
    <asp:LinkButton ID="lnkviewReimbursment" runat="server" onclick="LinkButton2_Click">Reimbursement Status </asp:LinkButton></td></tr>
    

    <tr> <td style="height:10px"> </td></tr>
     <tr> <td class="leftmMenu">   <img src="logo/Star.png" runat="server" id="imgClaim_Approval"/>
    <asp:LinkButton ID="lnkClaim_Approval" runat="server" OnClick="lnkClaim_Approval_Click" >Pending Approval </asp:LinkButton></td></tr>
     <tr> <td style="height:10px"> </td></tr>

    
   
     
     <tr> <td class="leftmMenu">   <img src="logo/Star.png" runat="server" id="imgAprovalstatus"/>
    <asp:LinkButton ID="lnkAprovalstatus" runat="server" OnClick="LinkButton2_Click1" >Approval Status</asp:LinkButton></td></tr>
     <tr> <td style="height:10px"> </td></tr>
    </table>
 </td> <td style="width:10px"> </td></tr></table>



    
</td>  <td style="width:30px">  </td> <td valign="top">


    <asp:Panel ID="pnlMain" runat="server">

        <table cellpadding="0px" cellspacing="0px"> 

        <tr> <td > 
    
   
    <br />
    <asp:Label ID="Label7" runat="server" 
            Text=" Claim Reimbursement" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
           
  </td></tr>

             <tr> <td style="height:13px"> </td></tr>

    <tr> <td class="leftm"> </td></tr>
     <tr> <td style="height:13px"> 
         <asp:Label ID="lblCSVLimit" runat="server" Visible="False"></asp:Label>
         <asp:Label ID="lblBasicPay" runat="server" Visible="False"></asp:Label>
         </td></tr>

            <tr> <td> 

                <table cellpadding="0px" cellspacing="0px"> <tr>  <td style="width:10px"> </td><td> <asp:RadioButton ID="rdEntryForm" runat="server" Text="Entry Level Form" GroupName="entryReim" AutoPostBack="True" OnCheckedChanged="rdEntryForm_CheckedChanged"/> </td> <td style="width:100px"> </td> <td>  <asp:RadioButton ID="rdCSVFile" runat="server" Text="Import From CSV File" GroupName="entryReim" AutoPostBack="True" OnCheckedChanged="rdCSVFile_CheckedChanged" /></td></tr></table>

                 </td></tr>

             <tr> <td style="height:13px"> </td></tr>
        </table>

    </asp:Panel>



    <asp:Panel ID="pnlCSVFile" runat="server" CssClass="leftBackground" Visible="false">

        <table cellpadding="0px" cellspacing="0px">
            <tr> <td colspan="3" style="height:13px"> </td></tr>
            
             <tr> <td style="width:10px">    </td> <td>  



            <table cellpadding="0px" cellspacing="0px">

               

                 <tr><td> Select CSV File  </td> <td style="width:10px">  </td> <td style="width:10px"> </td> <td> <asp:FileUpload ID="flPCSV" runat="server" /></td> <td style="width:10px"> </td> <td> <asp:RegularExpressionValidator ID="regexValidator" runat="server"  ControlToValidate="flPCSV" 
     ErrorMessage="Only csv files are allowed"  
     ValidationExpression="(.*\.([cC][sS][vV])$)" Font-Bold="True" ForeColor="Red" SetFocusOnError="True"></asp:RegularExpressionValidator></td> <td>
                <asp:Button ID="btnImport" runat="server" Text="Import" CssClass="btnLogin" OnClick="btnImport_Click" /> </td></tr>  </table>


                                                                                          </td> <td style="width:10px"> </td></tr> 

               <tr> <td colspan="3" style="height:13px"> </td></tr>

            <tr> <td colspan="3"> 
                
                
                 <asp:GridView ID="grdcsvData" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" PageSize="7" Width="866px" OnPageIndexChanging="grdcsvData_PageIndexChanging" OnRowDataBound="grdcsvData_RowDataBound" OnRowEditing="grdcsvData_RowEditing" OnRowCancelingEdit="grdcsvData_RowCancelingEdit" OnRowUpdating="grdcsvData_RowUpdating">
                <AlternatingRowStyle BackColor="White" />
                <Columns>

                     <asp:TemplateField HeaderText="Sl No.">
                                  <ItemTemplate>
                                     <%#Container.DataItemIndex+1 %>
                                  </ItemTemplate>
                              </asp:TemplateField>
               
                  
                        <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Left">
                                                                                                <ItemTemplate>
                                                                                                    <div style="width: 100px; overflow:auto; white-space: nowrap; text-overflow:clip">
                                                                                                        <asp:Label ID="lblclaimtypecsvd" runat="server" Text='<%# Eval("Claim_type") %>'></asp:Label>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>


                    <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Left">
                         <EditItemTemplate>
                             <asp:TextBox ID="txtdatecsvgrid" runat="server" Text= '<%# Eval("Claim_Date","{0:yyyy-MM-dd}") %>' Width="60px"></asp:TextBox>

                             <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtdatecsvgrid" Format="yyyy-MM-dd"></cc1:CalendarExtender>

                         </EditItemTemplate>
                         <ItemTemplate>
                             <asp:Label ID="lbldatecsvgrid" runat="server" Text= '<%# Eval("Claim_Date","{0:yyyy-MM-dd}") %>'  Width="60px"></asp:Label>
                         </ItemTemplate>
                               <ItemStyle HorizontalAlign="Left" />
                     </asp:TemplateField>


                   <%--   <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Left">
                                                                                                <ItemTemplate>
                                                                                                    <div style="width: 70px; overflow:auto; white-space: nowrap; text-overflow:clip">
                                                                                                        <%# Eval("Claim_Date","{0:yyyy-MM-dd}") %>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>--%>


                     <%--  <asp:BoundField DataField="Claim_Date" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd}" />
         --%>
                   
                           <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Left">
                         <EditItemTemplate>
                             <asp:TextBox ID="txtClaim_Amount" runat="server" Text= '<%# Eval("Claim_Amount") %>' Width="60px"></asp:TextBox>
                         </EditItemTemplate>
                         <ItemTemplate>
                             <asp:Label ID="lblClaim_Amount" runat="server" Text= '<%# Eval("Claim_Amount") %>'  Width="60px"></asp:Label>
                         </ItemTemplate>
                               <ItemStyle HorizontalAlign="Left" />
                     </asp:TemplateField>
                      <asp:TemplateField HeaderText="Bill Attach" ItemStyle-HorizontalAlign="Left">
                         <EditItemTemplate>
                             <asp:TextBox ID="txtBill_Detail" runat="server" Text= '<%# Eval("Bill_Detail") %>' Width="60px"></asp:TextBox>
                         </EditItemTemplate>
                         <ItemTemplate>
                             <asp:Label ID="lblBill_Detail" runat="server" Text= '<%# Eval("Bill_Detail") %>'  Width="60px"></asp:Label>
                         </ItemTemplate>
                          <ItemStyle HorizontalAlign="Left" />
                     </asp:TemplateField>


                    <asp:TemplateField HeaderText="Hard Copies">
                                                                                                <EditItemTemplate>
                                                                                                    <asp:TextBox ID="txtHardCopiesCSV" runat="server" Text= '<%# Eval("HardCopies") %>' Width="50px"></asp:TextBox>
                                                                                                </EditItemTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblHardCopiesCSV" runat="server" Text= '<%# Eval("HardCopies") %>' ></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
              

                            <asp:TemplateField HeaderText="Attached">
                                                                                                <EditItemTemplate>
                                                                                                    <asp:TextBox ID="txtAttachedcsv" runat="server" Text= '<%# Eval("Attached") %>' Width="50px"></asp:TextBox>
                                                                                                </EditItemTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblAttachedcsv" runat="server" Text= '<%# Eval("Attached") %>' ></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>


                         <asp:TemplateField HeaderText="No of Bills">
                                                                                                <EditItemTemplate>
                                                                                                    <asp:TextBox ID="txtNoOfBillscsv" runat="server" Text= '<%# Eval("No_Of_Bills") %>' Width="50px" Enabled="true"></asp:TextBox>
                                                                                                </EditItemTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblNoOfBillscsv" runat="server" Text= '<%# Eval("No_Of_Bills") %>' ></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>



                         <asp:TemplateField HeaderText="No of Month">
                                                                                                <EditItemTemplate>
                                                                                                    <asp:TextBox ID="txtNoof_Monthcsv" runat="server" Text= '<%# Eval("No_Of_Bills") %>' Width="50px" Enabled="true"></asp:TextBox>
                                                                                                </EditItemTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblNoof_Month" runat="server" Text= '<%# Eval("Noof_Month") %>' ></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>


                      <asp:TemplateField HeaderText="Remarks">
                                                                                                <EditItemTemplate>
                                                                                                    <asp:TextBox ID="txtRemarkscsv" runat="server" Text= '<%# Eval("Remarks") %>' Width="100px" TextMode="MultiLine" Height="20px"></asp:TextBox>
                                                                                                </EditItemTemplate>
                                                                                                <ItemTemplate>
                                                                                                <div style="width: 100px; overflow:auto; white-space: nowrap; text-overflow:clip; ">
                                                                                                        <%# Eval("Remarks") %>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>


                        
                     <asp:TemplateField HeaderText="Error Remarks" ItemStyle-HorizontalAlign="Left">
                         <ItemTemplate>
                             <div style="width: 100px; overflow:auto; white-space: nowrap; text-overflow:clip; color:red">
                                 <%# Eval("Error_Remarks") %>
                             </div>
                         </ItemTemplate>
                         <ItemStyle HorizontalAlign="Left" />
                     </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>

                            <asp:Label ID="lblApplicable" runat="server" Text= '<%# Eval("Applicable") %>' Visible="false"></asp:Label>
                       
                            <asp:Button ID="btnDeletecsv" runat="server" Text="Delete" OnCommand="btnDeletecsv_Command" CommandArgument='<%#Bind("id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:CommandField ShowEditButton="True" ButtonType="Button" />
                 
                

                     <asp:TemplateField>
                         <ItemTemplate>
                             <asp:Label ID="lblid" runat="server" Text= '<%# Eval("id") %>' Visible="false"></asp:Label>
                               <asp:Label ID="lblAmountExceed" runat="server" Text= '<%# Eval("AmountExceed") %>' Visible="false"></asp:Label>
                              <asp:Label ID="lblAppr_type" runat="server" Text= '<%# Eval("Approval_type") %>' Visible="false"></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                 
                

                </Columns>
                <EditRowStyle  />
                 <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="White" 
                      HorizontalAlign="Center" CssClass="cssGridheaderfont" />
        <PagerStyle BackColor="#C5122F" ForeColor="#4A3C8C" HorizontalAlign="Right" />
        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" CssClass="cssGridheaderfont" HorizontalAlign="Center"/>
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
            </td></tr>
               <tr> <td colspan="3" style="height:8px"> </td></tr>




            <tr><td colspan="3">
                <table cellpadding="0px" cellspacing="0px"> <tr> <td>        <asp:GridView ID="grdAttachmentcsv" runat="server" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
    RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#000"
    AutoGenerateColumns="false" Width="300px" AllowPaging="True"  PageSize="1" OnPageIndexChanging="grdAttachmentcsv_PageIndexChanging">
                <AlternatingRowStyle BackColor="White" ForeColor="#000000" />
                <Columns>
                    <asp:BoundField DataField="File_Attachment_Name" HeaderStyle-HorizontalAlign="Left" HeaderText="Attachment">
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDownloadcsv" runat="server" CommandArgument='<%# Eval("id") %>' OnClick="DownloadFile" Text="Download"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDeketeattachementcsv" runat="server" CommandArgument='<%# Eval("id") %>'  Text="Delete" OnCommand="lnkDeketeattachementcsv_Command"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="White" 
                      HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" CssClass="cssGridheaderfont" />
                 <PagerStyle BackColor="#C5122F" ForeColor="#4A3C8C" HorizontalAlign="Left" />
</asp:GridView> </td>  <td style="width:40px"> </td> <td> <asp:Panel ID="pnlCSvFileuploadattached" runat="server" Visible="false">

    <table cellpadding="0px" cellspacing="0px"> <tr> <td colspan="3" style="height:20px;background-color:#C5122F;color:white"> <asp:Label ID="Label2" runat="server" Text=" Bill Attachment "></asp:Label>  </td> 



                                                </tr>

        <tr> <td> <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="true"  /> </td> <td> <asp:Button ID="Button1" runat="server" Text="Upload File" OnClick="Button1_Click" /> </td> <td></td></tr>
    </table>


                                                          </asp:Panel> </td></tr> </table>
         

                </td></tr>

            <tr> <td colspan="3"> 

        <%--        <asp:Panel ID="pnlCSvAttach" runat="server">

                     <table cellpadding="0px" cellspacing="0px"> <tr> <td>    </td> <td>    </td> <td>  &nbsp;&nbsp;</td> </tr> </table>
            
                </asp:Panel>       --%>
            
       

                <asp:Button ID="btnsendforapprovalcsv" runat="server" Text="Send for Approval" CssClass="btnLogin" OnClick="btnsendforapprovalcsv_Click" />   <asp:Button ID="btnClearCSV" runat="server" Text="Clear"  CssClass="btnLogin" OnClick="btnClearCSV_Click" />


                 </td></tr>
              <tr> <td colspan="3" style="height:8px"> </td></tr>
        </table>

    </asp:Panel>

    <asp:Panel ID="pnlreimbursmentregister" runat="server" CssClass="leftBackground" Visible="false" >

    <table cellpadding="0px" cellspacing="0px"> <tr> <td style="width:10px"> </td> <td>
    
    <table cellpadding="0px" cellspacing="0px"> 
        
     <%--    <tr> <td > 
    
   
    <br />
    <asp:Label ID="Label2" runat="server" 
            Text=" Claim Reimbursement" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
           
  </td></tr>
    <tr> <td style="height:13px"> </td></tr>

    <tr> <td class="leftm"> </td></tr>
     <tr> <td style="height:13px"> </td></tr>--%>

         <tr> <td style="height:13px"> </td></tr>
        <tr> <td>  


            <table cellpadding="0px" cellspacing="0px">  

               
   
       
                
                <tr><td colspan="11">  <table cellpadding="0px" cellspacing="0px"> <tr> <td style="width:120px">Claim Type : </td> <td style="width:10px"> </td> <td ><%-- <asp:DropDownList ID="ddExpenseType" runat="server" AutoPostBack="True" 
          Width="200px" AppendDataBoundItems="true" OnSelectedIndexChanged="ddExpenseType_SelectedIndexChanged">
             <asp:ListItem Text="" Value="" />
        </asp:DropDownList>--%>


                     <asp:DropDownList ID="ddExpenseType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddExpenseType_SelectedIndexChanged1" Height="29px" >
                    </asp:DropDownList>

                    <%--<asp:DropDownList ID="ddExpenseType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddExpenseType_SelectedIndexChanged" ></asp:DropDownList>--%>
                     <asp:Label ID="Label4" runat="server" Text="Label" Visible="False"></asp:Label> <asp:TextBox ID="txtDocumentno" runat="server" Width="100px" Enabled="False" Visible="False"></asp:TextBox> 
                    <asp:Label ID="Label5" runat="server" Text="Label" Visible="False"></asp:Label>
                   
                    </td> <td style="width:50px"> </td> <td> <asp:Panel ID="pnllimit" runat="server" ForeColor="Red">

                        <table cellpadding="0px" cellspacing="0px"> <tr>  <td> Limit :</td> <td style="width:10px">  </td> <td>  <asp:Label ID="lblMaxLimit" runat="server" Text=""></asp:Label> </td> </tr> </table>

                                                             </asp:Panel> </td><td style="width:50px">  </td> <td>  <table cellpadding="0px" cellspacing="0px"> <tr> <td> <asp:Panel ID="pnlbalancelimit" runat="server" ForeColor="Red"> <table cellpadding="0px" cellspacing="0px">  <tr> <td> Balance : </td> <td style="width:10px"> </td> <td> <asp:Label ID="lblbalanceAmount" runat="server"></asp:Label> </td></tr></table> </asp:Panel> </td></tr> </table>    </td></tr> </table>  </td></tr>
                 <tr><td style="height:10px" colspan="11"> </td></tr>

                <tr> <td colspan="11">  <table cellpadding="0px" cellspacing="0px"> 
                    
                     <tr><td>  Date : </td> <td style="width:10px"> </td> <td> 


                    <asp:TextBox ID="txtDate" runat="server" Width="100px"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Fill Date" ControlToValidate="txtDate" ForeColor="Red" SetFocusOnError="True" ValidationGroup="claimr"></asp:RequiredFieldValidator>
                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDate" Format="yyyy-MM-dd"></cc1:CalendarExtender>

                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtDate" WatermarkText="yyyy-MM-dd"></cc1:TextBoxWatermarkExtender>

    

                                                                                                              </td> <td style="width:10px"> </td>  <td>  Amount : </td> <td style="width:10px"> </td> <td>  <asp:TextBox ID="txtAmount" runat="server" Width="70px" OnTextChanged="txtAmount_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                                                                                  <br />
                                                                                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Fill Amount" ControlToValidate="txtAmount" ForeColor="Red" SetFocusOnError="True" ValidationGroup="claimr"></asp:RequiredFieldValidator>
                                                                                                                                                                                                      </td>    <td style="width:10px"> </td>  <td>  Bill Attach</td> <td style="width:10px"> </td> <td>  
                          <asp:DropDownList ID="ddBillAttach" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddBillAttach_SelectedIndexChanged" Height="29px">
                              <asp:ListItem></asp:ListItem>
                              <asp:ListItem>Yes</asp:ListItem>
                              <asp:ListItem>No</asp:ListItem>
                          </asp:DropDownList>
                          </td>  <td style="width:10px">  </td> <td> Pay back in </td> <td style="width:100px"> 
                         <asp:TextBox ID="txtNoofMonth" runat="server" Width="20px"></asp:TextBox> Months
                         </td> <td> &nbsp;</td> </tr>




                      <tr><td >  Hard Copies : </td> <td style="width:10px"> </td> <td> 


                   <%-- <cc1:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtDate" Format="yyyy-MM-dd"></cc1:CalendarExtender>

                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtDate" WatermarkText="yyyy-MM-dd"></cc1:TextBoxWatermarkExtender>

    --%>

                                                                                                              <asp:TextBox ID="txtHardCopies" runat="server" Width="30px" AutoPostBack="True" OnTextChanged="txtHardCopies_TextChanged"></asp:TextBox>

    

                                                                                                              </td> <td style="width:10px"> </td>  <td>  Attached : </td> <td style="width:10px"> </td> <td>  <asp:TextBox ID="txtAttached" runat="server" Width="40px"  AutoPostBack="True" OnTextChanged="txtAttached_TextChanged"></asp:TextBox>
                                                                                                                  <br />
                                                                                                                                                                                                      </td>    <td style="width:10px"> </td>  <td>  No of Bills : </td> <td style="width:10px"> </td> <td>  
                          <asp:TextBox ID="txtNoofBills" runat="server" Enabled="False" Width="40px" ></asp:TextBox>
                          </td>  <td style="width:10px">  </td> <td>  </td> <td style="width:10px"> </td> <td> &nbsp;</td> </tr>



                        <tr><td style="height:10px" colspan="15" align="center"><asp:Label ID="lblError" runat="server" Font-Size="10pt" ForeColor="Red"></asp:Label> </td></tr>





            




                       <tr> <td>  Remarks / Reason : </td> <td style="width:10px"> </td> <td colspan="13"> 


                 <asp:TextBox ID="txtRemarks" runat="server" Width="700px" TextMode="MultiLine"></asp:TextBox>

     

                                                                                                              </td></tr>

                                        </table>   </td></tr>
             

 
                 <tr><td style="height:5px" colspan="11" align="center">

                     


                     </td></tr>

                   <%--   <tr><td>  Remarks : </td> <td style="width:10px"> </td> <td> 


                 <asp:TextBox ID="txtRemarks" runat="server" Width="200px" TextMode="MultiLine"></asp:TextBox>

     

                                                                                                              </td> <td style="width:10px"> </td>  <td>  Bill Detail : </td> <td style="width:10px"> </td> <td>  
                          <asp:FileUpload ID="FLU_BillDetails" runat="server" />
                          </td> </tr>--%>

                  <tr><td style="height:5px" colspan="11"> </td></tr>


                  <tr><td align="right" colspan="11">

                      <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btnLogin" Visible="false" OnClick="btncancel_Click" />

                      <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btnLogin" Visible="false" OnClick="btnUpdate_Click" ValidationGroup="claimr" />

                      <asp:Button ID="btnAdd" runat="server" Text="Add New " CssClass="btnLogin" OnClick="btnAdd_Click" ValidationGroup="claimr"/>


                        <asp:TextBox ID="txtTotalNoOFBills" runat="server" Visible="False"></asp:TextBox>


                        <asp:Label ID="lblBasicPayForCSV" runat="server"></asp:Label>
                      <asp:Label ID="lblApproval_type" runat="server" Visible="False"></asp:Label>


                      <asp:Label ID="lbllimit_text" runat="server" Visible="False"></asp:Label>
                      <asp:Label ID="lblBalanceAmountcsv" runat="server" Text="Label" Visible="False"></asp:Label>
                      


                      </td></tr>
                 <%-- <tr><td style="height:10px" colspan="11"> </td></tr>--%>

               <%-- <tr> <td>  Remarks </td> <td> <asp:TextBox ID="txtRemarks" runat="server"></asp:TextBox></td> <td style="width:10px"> </td> <td> Bill Detail </td> <td style="width:10px"> </td> <td>  <asp:FileUpload ID="FileUpload1" runat="server" /></td></tr>--%>
            </table>



           
             </td></tr>

   
      

  


         

    <tr> <td  colspan="8" class="leftm"style="width:100%"> </td></tr>
    
    <tr> <td style="height:3px"> </td></tr>
    
    <tr> <td> 

        <%--<div id="grdReimEntry"> --%>

    <asp:GridView ID="grdReimbursment" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" GridLines="Horizontal" Width="900px" AllowPaging="True" 
            onpageindexchanging="grdReimbursment_PageIndexChanging" PageSize="3" OnRowDataBound="grdReimbursment_RowDataBound" 
           >
        <AlternatingRowStyle BackColor="#F7F7F7" />
        <Columns>
             <asp:TemplateField HeaderText="Sl No.">
                                  <ItemTemplate>
                                     <%#Container.DataItemIndex+1 %>
                                  </ItemTemplate>
                              </asp:TemplateField>
            <asp:BoundField DataField="Claim_type" HeaderText="Type" />
            <asp:BoundField DataField="Claim_Date" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd}"/>
            <asp:BoundField DataField="Claim_Amount" HeaderText="Amount" />
              <asp:BoundField DataField="Bill_Attach" HeaderText="Bill Attach" />
             <asp:BoundField DataField="Noof_Month" HeaderText="No of Month" />
            <asp:BoundField DataField="Attached" HeaderText="Attached" />
            <asp:BoundField DataField="HardCopies" HeaderText="Hard Copies" />
             <asp:BoundField DataField="No_of_Attachment" HeaderText="No of Bills" />
            <%--  <asp:BoundField DataField="Bill_Details" HeaderText="Bill Detail" />--%>
            <%--<asp:BoundField DataField="Remarks" HeaderText="Remarks" />--%>
               <asp:TemplateField HeaderText="Remarks/Reason" ItemStyle-HorizontalAlign="Left">
                                                                                                <ItemTemplate>
                                                                                                    <div style="width: 100px; overflow:auto; white-space: nowrap; text-overflow:clip">
                                                                                                        <%# Eval("Remarks") %>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>

          <%--  <asp:BoundField DataField="Approval_Status" HeaderText="Status" />--%>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblAmountExceedentrylevel" runat="server" Text='<%# Eval("AmountExceed") %>' Visible="false"></asp:Label>


                    <asp:Button ID="btnDeleteclaim" runat="server" OnCommand="btnDeleteclaim_Command" Text="Delete" CommandArgument='<%#Bind("id") %>'  />
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" OnCommand="btnEdit_Command" CommandArgument='<%#Bind("id") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            There is no record found...........
        </EmptyDataTemplate>
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="White" 
                      HorizontalAlign="Left" CssClass="cssGridheaderfont" />
        <PagerStyle BackColor="#C5122F" ForeColor="#4A3C8C" HorizontalAlign="Right" />
        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" CssClass="cssGridheaderfont"/>
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
        <SortedAscendingCellStyle BackColor="#F4F4FD" />
        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
        <SortedDescendingCellStyle BackColor="#D8D8F0" />
        <SortedDescendingHeaderStyle BackColor="#3E3277" />
        </asp:GridView>
    
<%--     </div>--%>
     </td></tr>
        
    <tr> <td style="height:3px"> </td></tr>
                <tr> <td>

                    <table cellpadding="0px" cellspacing="0px"> <tr>  <td>         <asp:GridView ID="grdAttachment" runat="server" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
    RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#000"
    AutoGenerateColumns="false" Width="300px" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="1">
                <AlternatingRowStyle BackColor="White" ForeColor="#000000" />
                <Columns>
                    <asp:BoundField DataField="File_Attachment_Name" HeaderStyle-HorizontalAlign="Left" HeaderText="Attachment File">
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDownload" runat="server" CommandArgument='<%# Eval("id") %>' OnClick="DownloadFile" Text="Download"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDeketeattachement" runat="server" CommandArgument='<%# Eval("id") %>' OnCommand="lnkDeketeattachement_Command" Text="Delete"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="White" 
                      HorizontalAlign="Left" Font-Size="8pt" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" />
                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Font-Size="7pt" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"/>
                 <PagerStyle BackColor="#C5122F" ForeColor="#4A3C8C" HorizontalAlign="Left" />
</asp:GridView> </td> <td style="width:30px"> </td>  <td>
    
    <asp:Panel ID="pnlAttachEntry" runat="server">
    
     <table cellpadding="0px" cellspacing="0px"> 
         
         <tr> <td colspan="3" style="height:20px;background-color:#C5122F;color:white"> <asp:Label ID="lblBillaAttach" runat="server" Text=" Bill Attachment"></asp:Label>  </td></tr>
         
         <tr> <td>    </td> <td>   <asp:FileUpload ID="flp_file_attachement" runat="server" AllowMultiple="true"  /> </td> <td> <asp:Button ID="btnuploadFile" runat="server" Text="Upload File" OnClick="btnuploadFile_Click" /> &nbsp;&nbsp;</td></tr></table> 
        
        </asp:Panel>
        
        </td></tr></table>
    

             </td></tr>

        <tr>    <td align="right"> 
            
            
            <table cellpadding="0px" cellspacing="0px"> <tr>  <td> <br />   <asp:Button ID="btnclear" runat="server" Text="Clear"  Visible="false" OnClick="btnclear_Click" />&nbsp;&nbsp;</td> <td> <br />
                 <asp:Button ID="btnsend_for_approval" runat="server" Text="Send for Approval" Visible="false" OnClick="btnsend_for_approval_Click"/></td></tr> </table>
               
            
       
              
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            </td></tr>

 
        <tr> <td style="height:13px"> </td></tr>
    </table>
     </td> <td style="width:10px"> </td> </tr></table>

    
    </asp:Panel>




    <asp:Panel ID="pnlviewReimbursment" runat="server" CssClass="leftBackground" Visible="false">

    <table cellpadding="0px" cellspacing="0px"> <tr> <td style="width:10px"> </td> <td>
    
    <table cellpadding="0px" cellspacing="0px">  <tr> <td> 
    <br />
    <asp:Label ID="Label3" runat="server" 
            Text="Reimbursement Status " Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
  </td></tr>
   <tr> <td style="height:13px"> </td></tr>

    <tr> <td class="leftm"> </td></tr>
    
    
    <tr> <td>   <table cellpadding="0px" cellspacing="0px">  
     <tr> <td colspan="14" style ="height:20px"> </td></tr>
       
       <tr> <td> <label> From Date</label>   </td> <td style="width:10px"> </td> <td> 
           <asp:TextBox ID="txtFromdate" runat="server" Width="100px"></asp:TextBox>
           
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromdate" Format="yyyy-MM-dd">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtFromdate">
      </cc1:TextBoxWatermarkExtender>

           </td> <td style="width:10px">  </td> <td> <label> To Date</label>   </td> <td style="width:10px"> </td> <td> 
           <asp:TextBox ID="txtToDate" runat="server" Width="100px"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate" Format="yyyy-MM-dd">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtToDate">
      </cc1:TextBoxWatermarkExtender>
           </td> <td style="width:10px"> </td> <td style="width:10px"> </td><td><label > Status</label></td>  <td style="width:10px"> </td> <td> 
               <asp:DropDownList ID="ddStatus" runat="server" Height="29px">
                   <asp:ListItem>Select</asp:ListItem>
                   <asp:ListItem>Pending</asp:ListItem>
                   <asp:ListItem>Approved</asp:ListItem>
                    <asp:ListItem>Rejected</asp:ListItem>
                   <asp:ListItem>All</asp:ListItem>
               </asp:DropDownList>
           </td><td style="width:100px"> </td> <td> 
               <asp:Button ID="btnsearch" runat="server" Text="Show Detail" 
                   CssClass="btnLogin" onclick="btnsearch_Click" ValidationGroup="ddstatusreim" 
                  />
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                   ControlToValidate="ddStatus" ErrorMessage="*" InitialValue="Select" 
                   SetFocusOnError="True" ValidationGroup="ddstatusreim" ForeColor="Red"></asp:RequiredFieldValidator>
           </td></tr>

         <tr> <td colspan="14" style ="height:20px"> </td></tr>
        </table>  </td></tr>

        

    <tr> <td class="leftm"> </td></tr>
     <tr> <td style="height:13px"> </td></tr>
     <tr> <td> 

         <div id="GRDSearchDetailwrap"> 


              <asp:GridView ID="GRDSearchDetail" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" GridLines="Horizontal" Width="1200px" AllowPaging="True" 
            onpageindexchanging="grdReimbursment_PageIndexChanging" PageSize="6" 
           >
        <AlternatingRowStyle BackColor="#F7F7F7" />
        <Columns>
          <asp:TemplateField HeaderText="Sl No.">
                                  <ItemTemplate>
                                     <%#Container.DataItemIndex+1 %>
                                  </ItemTemplate>
                              </asp:TemplateField>

            <asp:BoundField DataField="Claim_Date" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd}"/>
            <asp:BoundField DataField="Claim_type" HeaderText="Type" />
            
            <asp:BoundField DataField="Claim_Amount" HeaderText="Amount" />
              <asp:BoundField DataField="Approval_Amount" HeaderText="Approval Amount" />
              <asp:BoundField DataField="Bill_Attach" HeaderText="Bill Attach" />
            <asp:BoundField DataField="No_of_Attachment" HeaderText="No of Attachment" />
              <asp:BoundField DataField="Noof_Month" HeaderText="No of Month" />
                <asp:TemplateField>
                <ItemTemplate>

                    <asp:LinkButton ID="lnkDocumentNon" runat="server" CommandArgument='<%#Bind("Document_No") %>' Text="Attachment" OnCommand="lnkDocumentNon_Command"></asp:LinkButton>

                 
                </ItemTemplate>
            </asp:TemplateField>


          
               <asp:TemplateField HeaderText="Claim Remarks" ItemStyle-HorizontalAlign="Left">
                                                                                                <ItemTemplate>
                                                                                                    <div style="width: 100px; overflow:auto; white-space: nowrap; text-overflow:clip">
                                                                                                        <%# Eval("Remarks") %>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>

            <asp:BoundField DataField="Approval_Status" HeaderText="Status" />
     <asp:TemplateField HeaderText="Approval Remarks" ItemStyle-HorizontalAlign="Left">
                                                                                                <ItemTemplate>
                                                                                                    <div style="width: 100px; overflow:auto; white-space: nowrap; text-overflow:clip">
                                                                                                        <%# Eval("Approval_Remarks") %>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>
            <asp:BoundField />
        </Columns>
        <EmptyDataTemplate>
            There is no record found...........
        </EmptyDataTemplate>
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="White" 
                      HorizontalAlign="Left" CssClass="cssGridheaderfont" />
        <PagerStyle BackColor="#C5122F" ForeColor="#4A3C8C" HorizontalAlign="Right" />
        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" CssClass="cssGridheaderfont" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
        <SortedAscendingCellStyle BackColor="#F4F4FD" />
        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
        <SortedDescendingCellStyle BackColor="#D8D8F0" />
        <SortedDescendingHeaderStyle BackColor="#3E3277" />
        </asp:GridView>
         

    
     </div>
     </td></tr>



    </table>
     </td> <td style="width:10px"> </td> </tr></table>

    
    </asp:Panel>

 


    <asp:Panel ID="pnlPendingClaimApproval" runat="server" CssClass="leftBackground" Visible="false">

    <table cellpadding="0px" cellspacing="0px"> <tr> <td style="width:10px"> </td> <td>
    
    <table cellpadding="0px" cellspacing="0px">  <tr> <td > 
    
   
    <br />
    <asp:Label ID="Label6" runat="server" 
            Text="Pending Approval" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
          
  </td></tr>
    <tr> <td style="height:13px"> </td></tr>

    <tr> <td class="leftm"> </td></tr>
    <tr> <td style="height:13px"> </td></tr>
    <tr> <td>  
    

    <table cellpadding="0px" cellspacing="0px"> <tr> <td> <label>Select Records by </label> </td> <td style="width:90px"> </td> <td> 
        <asp:RadioButton ID="rdEmployeeID" runat="server" Text="Employee Id" 
            GroupName="reimAprNew17"
            AutoPostBack="True" OnCheckedChanged="rdEmployeeID_CheckedChanged"/> </td> <td style="width:30px"> </td> <td> 
        <asp:RadioButton ID="rdEmployeeName" runat="server" Text="Employee Name" 
            GroupName="reimAprNew17" 
            AutoPostBack="True" OnCheckedChanged="rdEmployeeName_CheckedChanged"/> </td> <td style="width:30px"> </td> <td> 
        <asp:RadioButton ID="rdDatewise" runat="server" Text="Date Wise " 
            GroupName="reimAprNew17" 
            AutoPostBack="True" OnCheckedChanged="rdDatewise_CheckedChanged"/> </td><td style="width:30px"> </td> <td> 
        <asp:RadioButton ID="CHKAllPending" runat="server" Text="All" 
            GroupName="reimAprNew17" 
            AutoPostBack="True" OnCheckedChanged="CHKAllPending_CheckedChanged" /> </td>
        </tr></table>

   
        
        
        
         </td></tr>
         <tr> <td style="height:13px"> </td></tr>
         <tr> <td class="leftm"> </td></tr>
          <tr> <td style="height:20px"> </td></tr>

          <tr> <td >  
              <table cellpadding="0px" cellspacing="0px"> <tr> <td>  
              <asp:Panel ID="pnlEmployeeidName" runat="server" Visible="false">

               <table cellpadding="0px" cellspacing="0px" > <tr> <td> <label>Enter Employee Id/ Name </label> </td> <td style="width:110px"> </td><td> 
                   <asp:TextBox ID="txtSearchName" runat="server" Width="200px"></asp:TextBox></td> </tr></table>

              </asp:Panel>

                 <asp:Panel ID="pnlDate" runat="server" Visible="false">

               <table cellpadding="0px" cellspacing="0px" > <tr> <td> <label>From Date</label> </td> <td style="width:10px"> </td><td> 
                   <asp:TextBox ID="txtFromDate_Claim" runat="server"></asp:TextBox>
                   
                     <cc1:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtFromDate_Claim" Format="yyyy-MM-dd">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtFromDate_Claim">
      </cc1:TextBoxWatermarkExtender>

                   </td>  <td style="width:30px"> </td> <td>To Date </td>  <td style=" width:10px"> </td><td>
                       <asp:TextBox ID="txtTodate_claim" runat="server"></asp:TextBox> 
                       
                        <cc1:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtTodate_claim" Format="yyyy-MM-dd">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtTodate_claim">
      </cc1:TextBoxWatermarkExtender>

                       </td>  </tr></table>

              </asp:Panel>

          </td>   <td style="width:40px"> </td> <td>  
              <asp:Button ID="btnPendingApprovalSearch" runat="server" Text="Search" CssClass="btnLogin" 
                  Visible="False" OnClick="btnPendingApprovalSearch_Click" /></td></tr></table> </td></tr>

                  <tr> <td style="height:13px"> </td></tr>

    <tr> <td class="leftm"> </td></tr>

     <tr> <td >

         <div id="GRDPendingApproval">

         <asp:GridView ID="grdPendingClaim" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="1500px" OnRowDataBound="grdPendingClaim_RowDataBound">
             <Columns>
               <%--  <asp:BoundField DataField="AmountExceed" HeaderText="Amount Exceed" />--%>

            <%--      <asp:TemplateField>
                      <ItemTemplate>
                         
                      </ItemTemplate>
                 </asp:TemplateField>--%>
                 <asp:TemplateField>
                     <ItemTemplate>
                          <asp:Button ID="btnSendRM2" runat="server" Text="Send RM2 For Appoval" CommandArgument='<%#Bind("id") %>' OnCommand="btnSendRM2_Command" Visible="false" />
                         <asp:Button ID="btnApprovedRM1" runat="server" Text="Approved" CommandArgument='<%#Bind("id") %>' OnCommand="btnApprovedRM1_Command"  Visible="false"/>
                     </ItemTemplate>
                 </asp:TemplateField>


                  <asp:TemplateField>
                     <ItemTemplate>
                         <asp:Button ID="btnRejecta" runat="server" Text="Reject" OnCommand="btnRejecta_Command" CommandArgument='<%#Bind("id") %>'/>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField Visible="False">
                     <ItemTemplate>
                         <asp:CheckBox ID="CheckBox1" runat="server" />
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:BoundField DataField="Name" HeaderText="Name" />
                 <asp:BoundField DataField="Userid" HeaderText="Userid" />
                 <asp:BoundField DataField="Claim_Date" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Claim Date" />
                 <asp:BoundField DataField="Claim_type" HeaderText="Claim Type" />

                    <asp:TemplateField HeaderText="Claim Amount">
                     <ItemTemplate>

                         <asp:Label ID="lblClaminAmountPending" runat="server"  Text='<%# Eval("Claim_Amount") %>'></asp:Label>
                         <%--<asp:TextBox ID="txtApprovaAmounts" runat="server" Text='<%# Eval("Claim_Amount") %>' Width="80px"></asp:TextBox>--%>
                     </ItemTemplate>
                 </asp:TemplateField>

                  <asp:BoundField DataField="SelfApprovalAmount" HeaderText="Self Limit" />

                  <asp:TemplateField HeaderText="RM1 Limit">
                     <ItemTemplate >

                         <asp:Label ID="lblRM1Limit" runat="server"  Text='<%# Eval("RM1ApprovalAmount") %>' ForeColor="Black"  Font-Bold="true"   ></asp:Label>
                        
                     </ItemTemplate>
                    
                 </asp:TemplateField>

  <asp:TemplateField HeaderText="RM2 Limit">
                     <ItemTemplate>

                         <asp:Label ID="lblRM2Limit" runat="server"  Text='<%# Eval("RM2ApprovalAmount") %>' ForeColor="Black"  Font-Bold="true"></asp:Label>
                        
                     </ItemTemplate>
                 </asp:TemplateField>               <%--  <asp:BoundField DataField="Claim_Amount" HeaderText="Amount" />--%>
                 <%--<asp:BoundField DataField="RM1ApprovalAmount" HeaderText="RM1 Limit" />
                 <asp:BoundField DataField="RM2ApprovalAmount" HeaderText="RM2 Limit" />--%>
                 <asp:TemplateField HeaderText="Approve Amount">
                     <ItemTemplate>
                         <asp:TextBox ID="txtApprovaAmounts" runat="server" Text='<%# Eval("Claim_Amount") %>' Width="80px"></asp:TextBox>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:BoundField DataField="Bill_Attach" HeaderText="Bill Attach" />
                 <asp:BoundField DataField="HardCopies" HeaderText="Hard Copies" />
                 <asp:BoundField DataField="Attached" HeaderText="Attached" />
                 <asp:BoundField DataField="No_of_Attachment" HeaderText="No of Bills" />
                 <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left">
                     <ItemTemplate>
                         <div style="width: 100px; overflow:auto; white-space: nowrap; text-overflow:clip">
                             <%# Eval("Remarks") %>
                         </div>
                     </ItemTemplate>
                     <ItemStyle HorizontalAlign="Left" />
                 </asp:TemplateField>

                  <asp:TemplateField HeaderText="Amount Exceed">
                <ItemTemplate>
                    <asp:Label ID="lblAmountExceedentrylevelpending" runat="server" Text='<%# Eval("AmountExceed") %>' Visible="true"></asp:Label>


                    
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:BoundField DataField="ExceedAmount" HeaderText="Exceed Amount" />
                 <asp:TemplateField>
                     <ItemTemplate>
                         <asp:Label ID="lblRM1Approved" runat="server" Text='<%# Eval("RM1Approved") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
             </Columns>
             <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
             <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="White" 
                      HorizontalAlign="Left" CssClass="cssGridheaderfont"  />
             <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
             <RowStyle  BackColor="#E7E7FF" ForeColor="#4A3C8C" CssClass="cssGridheaderfont" />
             <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
             <SortedAscendingCellStyle BackColor="#FEFCEB" />
             <SortedAscendingHeaderStyle BackColor="#AF0101" />
             <SortedDescendingCellStyle BackColor="#F6F0C0" />
             <SortedDescendingHeaderStyle BackColor="#7E0000" />
         </asp:GridView>

          </div>


          </td></tr>

      
   
    </table>
     </td> <td style="width:10px"> </td> </tr></table>

    
    </asp:Panel>




      <asp:Panel ID="pnlClaimAprovalstatus" runat="server" CssClass="leftBackground" Visible="false">

    <table cellpadding="0px" cellspacing="0px"> <tr> <td style="width:10px"> </td> <td>
    
    <table cellpadding="0px" cellspacing="0px">  <tr> <td> 
    <br />
    <asp:Label ID="Label8" runat="server" 
            Text="Approval Status" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
  </td></tr>
   <tr> <td style="height:13px"> </td></tr>

    <tr> <td class="leftm"> </td></tr>
    
    
    <tr> <td>   <table cellpadding="0px" cellspacing="0px">  
     <tr> <td colspan="14" style ="height:20px"> </td></tr>
       
       <tr> <td> <label> From Date</label>   </td> <td style="width:10px"> </td> <td> 
           <asp:TextBox ID="txtClaimStatusFromDate" runat="server" Width="100px"></asp:TextBox>
           
            <cc1:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtClaimStatusFromDate" Format="yyyy-MM-dd">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtClaimStatusFromDate">
      </cc1:TextBoxWatermarkExtender>

           </td> <td style="width:10px">  </td> <td> <label> To Date</label>   </td> <td style="width:10px"> </td> <td> 
           <asp:TextBox ID="txtClaimStatusToDate" runat="server" Width="100px"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txtClaimStatusToDate" Format="yyyy-MM-dd">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtClaimStatusToDate">
      </cc1:TextBoxWatermarkExtender>
           </td> <td style="width:10px"> </td> <td style="width:10px"> </td><td><label > Status</label></td>  <td style="width:10px"> </td> <td> 
               <asp:DropDownList ID="ddClaimAprStatus" runat="server" Height="29px">
                   <asp:ListItem>Select</asp:ListItem>
                   <asp:ListItem>Approved</asp:ListItem>
                    <asp:ListItem>Rejected</asp:ListItem>
                   <asp:ListItem>All</asp:ListItem>
               </asp:DropDownList>
           </td><td style="width:100px">  </td> <td> 
               <asp:Button ID="btnShowClaimAprStatus" runat="server" Text="Show Detail" 
                   CssClass="btnLogin" ValidationGroup="clmAprStatus3" OnClick="btnShowClaimAprStatus_Click" 
                  />
               <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                   ControlToValidate="ddClaimAprStatus" ErrorMessage="*" InitialValue="Select" 
                   SetFocusOnError="True" ValidationGroup="clmAprStatus3" ForeColor="Red"></asp:RequiredFieldValidator>
           </td></tr>

         <tr> <td colspan="14" style ="height:20px"> </td></tr>
        </table>  </td></tr>

        

    <tr> <td class="leftm"> </td></tr>
     <tr> <td style="height:13px"> </td></tr>
     <tr> <td> 

         <div id="GRDClaimAprStatus"> 


              <asp:GridView ID="grdClaimApprovalStatus" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" GridLines="Horizontal" Width="1300px" AllowPaging="True" 
             PageSize="12" OnPageIndexChanging="grdClaimApprovalStatus_PageIndexChanging" 
           >
        <AlternatingRowStyle BackColor="#F7F7F7" />
        <Columns>
          <asp:TemplateField HeaderText="Sl No.">
                                  <ItemTemplate>
                                     <%#Container.DataItemIndex+1 %>
                                  </ItemTemplate>
                              </asp:TemplateField>

            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Userid" HeaderText="Userid" />

            <asp:BoundField DataField="Claim_Date" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd}"/>
            <asp:BoundField DataField="Claim_type" HeaderText="Type" />
            
            <asp:BoundField DataField="Claim_Amount" HeaderText="Amount" />
              <asp:BoundField DataField="Approval_Amount" HeaderText="Approval Amount" />
              <asp:BoundField DataField="Bill_Attach" HeaderText="Bill Attach" />
            <asp:BoundField DataField="No_of_Attachment" HeaderText="No of Attachment" />
              <asp:BoundField DataField="Noof_Month" HeaderText="No of Month" />
                <asp:TemplateField>
                <ItemTemplate>

                    <asp:LinkButton ID="lnkDocumentNon" runat="server" CommandArgument='<%#Bind("Document_No") %>' Text="Attachment" OnCommand="lnkDocumentNon_Command"></asp:LinkButton>

                 
                </ItemTemplate>
            </asp:TemplateField>


          
               <asp:TemplateField HeaderText="Claim Remarks/" ItemStyle-HorizontalAlign="Left">
                                                                                                <ItemTemplate>
                                                                                                    <div style="width: 100px; overflow:auto; white-space: nowrap; text-overflow:clip">
                                                                                                        <%# Eval("Remarks") %>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>

            <asp:BoundField DataField="Approval_Status" HeaderText="Status" />
     <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left">
                                                                                                <ItemTemplate>
                                                                                                    <div style="width: 100px; overflow:auto; white-space: nowrap; text-overflow:clip">
                                                                                                        <%# Eval("Approval_Remarks") %>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>
            <asp:BoundField />
        </Columns>
        <EmptyDataTemplate>
            There is no record found...........
        </EmptyDataTemplate>
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="White" 
                      HorizontalAlign="Left" CssClass="cssGridheaderfont"  />
        <PagerStyle BackColor="#C5122F" ForeColor="#4A3C8C" HorizontalAlign="Right" />
        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" CssClass="cssGridheaderfont" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
        <SortedAscendingCellStyle BackColor="#F4F4FD" />
        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
        <SortedDescendingCellStyle BackColor="#D8D8F0" />
        <SortedDescendingHeaderStyle BackColor="#3E3277" />
        </asp:GridView>
         

    
     </div>
     </td></tr>



    </table>
     </td> <td style="width:10px"> </td> </tr></table>

    
    </asp:Panel>



</td></tr></table>

</fieldset>

    <asp:Button ID="Button2" runat="server" Text="Button" style="display:none"/>
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button2" PopupControlID="Panel1" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" style="display:none">

        <table cellpadding="0px" cellspacing="0px"> <tr> <td align="right">  <asp:LinkButton ID="lnkClose" runat="server" OnClick="lnkClose_Click"> X </asp:LinkButton> </td></tr>


            <tr> <td style="height:10px">  </td></tr>
            <tr> <td>Rejected Remarks 
                <asp:Label ID="lblIdofClaim" runat="server" Text="Label" Visible="false"></asp:Label>
                </td> </tr>
            <tr> <td>  <asp:TextBox ID="txtRemarksRejected" runat="server" TextMode="MultiLine" Width="380px" Height="100px"></asp:TextBox></td></tr>
              <tr> <td style="height:10px">  </td></tr>

              <tr> <td align="right"> <asp:Button ID="btnsaverejected" runat="server" Text="Save" OnClick="btnsaverejected_Click" /> </td></tr>
                <tr> <td style="height:10px">  </td></tr>
        </table> 
     

    </asp:Panel>



    <asp:Button ID="Button3" runat="server" Text="Button" style="display:none"/>
    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="Button3" PopupControlID="pnlRemarksForsendingData" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>
    <asp:Panel ID="pnlRemarksForsendingData" runat="server" CssClass="modalPopup">

        <table cellpadding="0px" cellspacing="0px"> <tr> <td align="right">  <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lnkClose_Click"> X </asp:LinkButton> </td></tr>


            <tr> <td style="height:10px">  </td></tr>
            <tr> <td> Remarks 
                <asp:Label ID="lblSendforRM2Approval2" runat="server" Text="Label" Visible="false"></asp:Label>
                </td> </tr>
            <tr> <td>  <asp:TextBox ID="txtRemarksSendRM2aproval" runat="server" TextMode="MultiLine" Width="380px" Height="100px"></asp:TextBox></td></tr>
              <tr> <td style="height:10px">  </td></tr>

              <tr> <td align="right"> <asp:Button ID="btnsaveRM2Save" runat="server" Text="Send For Approval" OnClick="btnsaveRM2Save_Click" /> </td></tr>
                <tr> <td style="height:10px">  </td></tr>
        </table> 
     

    </asp:Panel>

</asp:Content>

