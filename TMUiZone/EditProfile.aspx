<%@ Page Title="" Language="C#" MasterPageFile="~/IndexMaster.master" AutoEventWireup="true" CodeFile="EditProfile.aspx.cs" Inherits="EditProfile" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link href="css/structure.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<fieldset class="boxBody">
 <asp:Label ID="Label1" runat="server" 
            Text="Profile" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 </fieldset>

 <fieldset class="boxBodyHeader"> 
 <asp:Label ID="Label2" runat="server" 
            Text="Personal Information" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 
 </fieldset>
  <fieldset class="boxBodyInner">
    <table cellpadding="0px" cellspacing="0px">  <tr>  <td> <label> Title </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txttytle" runat="server" Enabled="False" Width="50px"></asp:TextBox></td> <td style="width:10px"> </td>  <td> <label> First Name </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtFName" runat="server" Enabled="False" Width="210px"></asp:TextBox></td>  <td style="width:10px"> </td>  <td> <label> Second Name </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtSName" runat="server" Enabled="False" Width="210px"></asp:TextBox></td> <td style="width:10px"> </td>  <td> <label> Last Name </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtLName" runat="server" Enabled="False" Width="190px"></asp:TextBox></td></tr>
        
        
        <tr> <td colspan="15" style="height:10px"> </td></tr>


        
         


        <tr> <td> <label> Address </label>  </td> <td style="width:10px"> </td> <td colspan="14">   
            <asp:TextBox ID="txtAddress" runat="server" Width="1040px" 
                TextMode="MultiLine"></asp:TextBox></td></tr>

                 <tr> <td colspan="15" style="height:10px"> </td></tr>

                 <tr> <td> <label> Address2 </label>  </td> <td style="width:10px"> </td> <td colspan="14">   
            <asp:TextBox ID="txtAddress2" runat="server" Width="1040px" 
                         TextMode="MultiLine"></asp:TextBox></td></tr>


  <tr> <td colspan="15" style="height:10px"> </td></tr>

       <tr>  <td> <label> City</label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtCity" runat="server" Width="110px" AutoPostBack="True" OnTextChanged="txtCity_TextChanged"></asp:TextBox>
           <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" ServiceMethod="GetCompletionList" MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtCity"
     FirstRowSelected = "false"></cc1:AutoCompleteExtender>
        
                                                                           </td> <td style="width:10px"> </td>  <td> <label>State </label> </td> <td style="width:10px"> </td> <td> 
           <%--<asp:TextBox ID="txtPincode" runat="server" Width="125px"></asp:TextBox>--%>

            <asp:DropDownList ID="txtSTate" runat="server" Height="29px"></asp:DropDownList>
                                                                                                                                                                                 </td>  <td style="width:10px"> </td>  <td> <label> Country </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtCountry" runat="server"></asp:TextBox></td> <td style="width:10px"> </td>  <td> <label> Pin Code </label> </td> <td style="width:10px"> </td> <td> 
           <%--   <table cellpadding="0px" cellspacing="0px"> 
         
         
          <tr>  <td> <label> PAN No </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtPanNo" runat="server" Enabled="True" Width="160px"></asp:TextBox></td> <td style="width:10px"> </td>  <td> <label> ESI No</label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtEsiNo" runat="server" Enabled="False" Width="160px"></asp:TextBox></td>  <td style="width:10px"> </td>  <td> <label> PF No </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtPFNo" runat="server" Enabled="False" Width="110px"></asp:TextBox></td> <td style="width:10px"> </td>  <td> <label> Branch</label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtBranch" runat="server" Enabled="False" Width="110px"></asp:TextBox></td>
        
        
        </tr>


        <tr> <td colspan="15" style="height:10px"> </td></tr>


         <tr>  <td> <label> Department </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtDepartment" runat="server" Enabled="False" Width="160px"></asp:TextBox></td> <td style="width:10px"> </td>  <td> <label> Email </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtCompanyMail" runat="server" Enabled="True" Width="160px"></asp:TextBox></td>  <td style="width:10px"> 
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                     ControlToValidate="txtCompanyMail" ErrorMessage="*" ForeColor="Red" 
                     SetFocusOnError="True" 
                     ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>  <td> <label> AC/No </label> </td> <td style="width:10px"> </td> <td> 
             <asp:TextBox ID="txtACNO" runat="server" Enabled="True" Width="110px"></asp:TextBox>
        </td> <td style="width:10px"> </td>  <td> <label>  DOJ</label> </td> <td style="width:10px"> </td> <td> 
             <asp:TextBox ID="txtDOJ" runat="server" Enabled="False" Width="110px"></asp:TextBox>
        </td>
        
        
        </tr>

         <tr> <td colspan="15" style="height:10px"> </td></tr>


         <tr>  <td> <label> Designation </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtDesignation" runat="server" Enabled="False" Width="160px"></asp:TextBox></td> <td style="width:10px"> </td>  <td> <label>  Incharge </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtIncharge" runat="server" Enabled="False" Width="160px"></asp:TextBox></td>  <td style="width:10px"> </td>  <td> <label> HOD</label> </td> <td style="width:10px"> </td> <td> 
             <asp:TextBox ID="txtHOD" runat="server" Enabled="False" Width="110px"></asp:TextBox>
        </td> <td style="width:10px"> </td>  <td> <label>  Pay Method</label> </td> <td style="width:10px"> </td> <td> 
             <asp:TextBox ID="txtPaymethod" runat="server" Enabled="False" Width="110px"></asp:TextBox>
        </td>
        
        
        </tr>


        
   <tr> <td colspan="15" style="height:10px"> <table cellpadding="0px" cellspacing="0px"><tr> <td>  <label> Profile Image </label></td> <td style="width:10px"> </td> <td> 
               <asp:FileUpload ID="FileUpload1" runat="server" /> </td></tr> </table>   </td></tr>




          <tr> <td colspan="15" >  
              <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btnLogin" 
                  onclick="btnUpdate_Click" /></td></tr>
          <tr> <td colspan="15" style="height:10px"> </td></tr>



        </table>--%>


            <asp:DropDownList ID="txtPincode" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="txtPincode_SelectedIndexChanged" Height="29px"></asp:DropDownList>                                                                                                                           </td></tr>


          <tr> <td colspan="15" style="height:10px"> </td></tr>

         <tr>  <td> <label>Email Id </label> </td> <td style="width:10px"> </td> <td colspan="5"> 
             <asp:TextBox ID="txtEmailid" runat="server" Width="370px"></asp:TextBox>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                 ControlToValidate="txtEmailid" ErrorMessage="*" ForeColor="Red" 
                 SetFocusOnError="True" 
                 ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
        </td> <td style="width:10px"> </td>
        <td> <label> Date Of Birth </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtDOB" runat="server" Width="110px"></asp:TextBox>

             <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDOB" Format="dd/MM/yyyy">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" WatermarkText="dd/MM/yyyy" TargetControlID="txtDOB">
      </cc1:TextBoxWatermarkExtender>

                                                                                </td>
        <td style="width:10px"> </td>
           <td> <label> Sex </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtSex" runat="server" Width="190px" Enabled="False"></asp:TextBox></td></tr>


          <tr> <td colspan="15" style="height:10px"> </td></tr>

     
       
       <tr> <td colspan="15" > 
       
       <table cellpadding="0px" cellspacing="0px"> 
       
       
       <tr> <td><label>  Father Name </label> </td> <td style="width:10px"> </td> <td>
           <asp:TextBox ID="txtFatherName" runat="server" Width="400px"></asp:TextBox> </td> <td style="width:10px"> </td> <td><label>  Husband Name </label> </td> <td style="width:10px"> </td><td colspan="6"> 
           <asp:TextBox ID="txtHusBandName" runat="server" Width="505px"></asp:TextBox></td> </tr>
           <tr> <td colspan="11" style="height:10px"> </td></tr>
           
            <tr> <td><label>  Marital Status </label> </td> <td style="width:10px"> </td> <td>
                <asp:DropDownList ID="txtStatus" runat="server" Height="29px">
                    <asp:ListItem Value="0">Single</asp:ListItem>
                    <asp:ListItem Value="1">Married</asp:ListItem>
                    <asp:ListItem Value="2">Divorced</asp:ListItem>
                    <asp:ListItem Value="3">Widow</asp:ListItem>
                </asp:DropDownList>
                </td> <td style="width:10px"> </td> <td><label>  Contact No </label> </td> <td style="width:10px"> </td><td> 
           <asp:TextBox ID="txtMobileNo" runat="server" Width="200px"></asp:TextBox></td> <td style="width:10px"> </td> <td><label>  Phone No </label> </td> <td style="width:10px"> </td><td> 
           <asp:TextBox ID="txtPhoneNo" runat="server" Width="215px"></asp:TextBox></td></tr>
           </table>
       
       </td></tr>



        <tr> <td colspan="15" style="height:10px"> </td></tr>

          <tr> <td colspan="15" style="height:10px">
          <fieldset class="boxBodyHeader"> 
          <asp:Label ID="Label3" runat="server" 
            Text="Permanent Address" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 

          </fieldset>
          <fieldset class="boxBodyInner"> 



      <%--   <table cellpadding="0px" cellspacing="0px"> 
         
         
          <tr>  <td> <label> PAN No </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtPanNo" runat="server" Enabled="True" Width="160px"></asp:TextBox></td> <td style="width:10px"> </td>  <td> <label> ESI No</label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtEsiNo" runat="server" Enabled="False" Width="160px"></asp:TextBox></td>  <td style="width:10px"> </td>  <td> <label> PF No </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtPFNo" runat="server" Enabled="False" Width="110px"></asp:TextBox></td> <td style="width:10px"> </td>  <td> <label> Branch</label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtBranch" runat="server" Enabled="False" Width="110px"></asp:TextBox></td>
        
        
        </tr>


        <tr> <td colspan="15" style="height:10px"> </td></tr>


         <tr>  <td> <label> Department </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtDepartment" runat="server" Enabled="False" Width="160px"></asp:TextBox></td> <td style="width:10px"> </td>  <td> <label> Email </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtCompanyMail" runat="server" Enabled="True" Width="160px"></asp:TextBox></td>  <td style="width:10px"> 
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                     ControlToValidate="txtCompanyMail" ErrorMessage="*" ForeColor="Red" 
                     SetFocusOnError="True" 
                     ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>  <td> <label> AC/No </label> </td> <td style="width:10px"> </td> <td> 
             <asp:TextBox ID="txtACNO" runat="server" Enabled="True" Width="110px"></asp:TextBox>
        </td> <td style="width:10px"> </td>  <td> <label>  DOJ</label> </td> <td style="width:10px"> </td> <td> 
             <asp:TextBox ID="txtDOJ" runat="server" Enabled="False" Width="110px"></asp:TextBox>
        </td>
        
        
        </tr>

         <tr> <td colspan="15" style="height:10px"> </td></tr>


         <tr>  <td> <label> Designation </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtDesignation" runat="server" Enabled="False" Width="160px"></asp:TextBox></td> <td style="width:10px"> </td>  <td> <label>  Incharge </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtIncharge" runat="server" Enabled="False" Width="160px"></asp:TextBox></td>  <td style="width:10px"> </td>  <td> <label> HOD</label> </td> <td style="width:10px"> </td> <td> 
             <asp:TextBox ID="txtHOD" runat="server" Enabled="False" Width="110px"></asp:TextBox>
        </td> <td style="width:10px"> </td>  <td> <label>  Pay Method</label> </td> <td style="width:10px"> </td> <td> 
             <asp:TextBox ID="txtPaymethod" runat="server" Enabled="False" Width="110px"></asp:TextBox>
        </td>
        
        
        </tr>


        
   <tr> <td colspan="15" style="height:10px"> <table cellpadding="0px" cellspacing="0px"><tr> <td>  <label> Profile Image </label></td> <td style="width:10px"> </td> <td> 
               <asp:FileUpload ID="FileUpload1" runat="server" /> </td></tr> </table>   </td></tr>




          <tr> <td colspan="15" >  
              <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btnLogin" 
                  onclick="btnUpdate_Click" /></td></tr>
          <tr> <td colspan="15" style="height:10px"> </td></tr>



        </table>--%>



           <table cellpadding="0px" cellspacing="0px"> 
         
         
     


         <tr> <td colspan="15" style="height:10px"> </td></tr>
         <tr> <td> <label> Address </label>  </td> <td style="width:10px"> </td> <td colspan="14">   
            <asp:TextBox ID="txtAddressPermanent" runat="server" 
                 Width="1000px" TextMode="MultiLine"></asp:TextBox></td></tr>

                 <tr> <td colspan="15" style="height:10px"> </td></tr>

                 <tr> <td> <label> Address2 </label>  </td> <td style="width:10px"> </td> <td colspan="14">   
            <asp:TextBox ID="txtAddress2Permanent" runat="server" Width="1000px" 
                         TextMode="MultiLine"></asp:TextBox></td></tr>



        <tr> <td colspan="15" style="height:10px"> </td></tr>


         <tr>  <td> <label> City </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtCityPermanent" runat="server" Width="400px"></asp:TextBox></td> <td style="width:160px"> </td>  <td> <label> State </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtStatePermanent" runat="server" Width="400px"></asp:TextBox></td>       
        </tr>


        </table>

          </fieldset>
           </td></tr>
        

         <tr> <td colspan="15" style="height:10px">
          <fieldset class="boxBodyHeader"> 
          <asp:Label ID="Label4" runat="server" 
            Text="Other Details" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 

          </fieldset>
          <fieldset class="boxBodyInner"> 



      <%--   <table cellpadding="0px" cellspacing="0px"> 
         
         
          <tr>  <td> <label> PAN No </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtPanNo" runat="server" Enabled="True" Width="160px"></asp:TextBox></td> <td style="width:10px"> </td>  <td> <label> ESI No</label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtEsiNo" runat="server" Enabled="False" Width="160px"></asp:TextBox></td>  <td style="width:10px"> </td>  <td> <label> PF No </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtPFNo" runat="server" Enabled="False" Width="110px"></asp:TextBox></td> <td style="width:10px"> </td>  <td> <label> Branch</label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtBranch" runat="server" Enabled="False" Width="110px"></asp:TextBox></td>
        
        
        </tr>


        <tr> <td colspan="15" style="height:10px"> </td></tr>


         <tr>  <td> <label> Department </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtDepartment" runat="server" Enabled="False" Width="160px"></asp:TextBox></td> <td style="width:10px"> </td>  <td> <label> Email </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtCompanyMail" runat="server" Enabled="True" Width="160px"></asp:TextBox></td>  <td style="width:10px"> 
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                     ControlToValidate="txtCompanyMail" ErrorMessage="*" ForeColor="Red" 
                     SetFocusOnError="True" 
                     ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>  <td> <label> AC/No </label> </td> <td style="width:10px"> </td> <td> 
             <asp:TextBox ID="txtACNO" runat="server" Enabled="True" Width="110px"></asp:TextBox>
        </td> <td style="width:10px"> </td>  <td> <label>  DOJ</label> </td> <td style="width:10px"> </td> <td> 
             <asp:TextBox ID="txtDOJ" runat="server" Enabled="False" Width="110px"></asp:TextBox>
        </td>
        
        
        </tr>

         <tr> <td colspan="15" style="height:10px"> </td></tr>


         <tr>  <td> <label> Designation </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtDesignation" runat="server" Enabled="False" Width="160px"></asp:TextBox></td> <td style="width:10px"> </td>  <td> <label>  Incharge </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtIncharge" runat="server" Enabled="False" Width="160px"></asp:TextBox></td>  <td style="width:10px"> </td>  <td> <label> HOD</label> </td> <td style="width:10px"> </td> <td> 
             <asp:TextBox ID="txtHOD" runat="server" Enabled="False" Width="110px"></asp:TextBox>
        </td> <td style="width:10px"> </td>  <td> <label>  Pay Method</label> </td> <td style="width:10px"> </td> <td> 
             <asp:TextBox ID="txtPaymethod" runat="server" Enabled="False" Width="110px"></asp:TextBox>
        </td>
        
        
        </tr>


        
   <tr> <td colspan="15" style="height:10px"> <table cellpadding="0px" cellspacing="0px"><tr> <td>  <label> Profile Image </label></td> <td style="width:10px"> </td> <td> 
               <asp:FileUpload ID="FileUpload1" runat="server" /> </td></tr> </table>   </td></tr>




          <tr> <td colspan="15" >  
              <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btnLogin" 
                  onclick="btnUpdate_Click" /></td></tr>
          <tr> <td colspan="15" style="height:10px"> </td></tr>



        </table>--%>




           <table cellpadding="0px" cellspacing="0px"> 

          <tr> <td colspan="15" > 
       
       <table cellpadding="0px" cellspacing="0px"> 
       
       
       <tr> <td><label>  Pan No </label> </td> <td style="width:10px"> </td> <td>
        <asp:TextBox ID="txtPanNo" runat="server" Width="260px"></asp:TextBox> </td> <td style="width:10px"> </td> <td>
           ESI No<label>  </label> </td> <td style="width:10px"> </td><td> 
        <asp:TextBox ID="txtEsiNo" runat="server" Enabled="False" Width="260px"></asp:TextBox></td> <td style="width:10px"> </td> <td>
           PF No<label>  </label> </td> <td style="width:10px"> </td><td> 
        <asp:TextBox ID="txtPFNo" runat="server" Enabled="False" Width="260px"></asp:TextBox></td></tr>
           <tr> <td colspan="11" style="height:10px"> </td></tr>
           
            <tr> <td>Department<label>  </label> </td> <td style="width:10px"> </td> <td>
        <asp:TextBox ID="txtDepartment" runat="server" Enabled="False" Width="260px"></asp:TextBox> </td> <td style="width:10px"> </td> <td>
                Email
                <label>  &nbsp;</label></td> <td style="width:10px"> </td><td> 
        <asp:TextBox ID="txtCompanyMail" runat="server" Enabled="False" Width="260px"></asp:TextBox></td> <td style="width:10px"> </td> <td>
                AC/No<label>  </label> </td> <td style="width:10px"> </td><td> 
             <asp:TextBox ID="txtACNO" runat="server" Width="260px"></asp:TextBox>
                </td></tr>

                 <tr> <td colspan="11" style="height:10px"> </td></tr>
           
            <tr> <td>
                <label>  Branch  </label> </td> <td style="width:10px"> </td> <td>
        <asp:TextBox ID="txtBranch" runat="server" Enabled="False" Width="260px"></asp:TextBox> </td> <td style="width:10px"> </td> <td>
                DOJ
                <label>  &nbsp;</label></td> <td style="width:10px"> </td><td> 
             <asp:TextBox ID="txtDOJ" runat="server" Enabled="False" Width="260px"></asp:TextBox>
                </td> <td style="width:10px"> </td> <td>
                Designation<label>  </label> </td> <td style="width:10px"> </td><td> 
        <asp:TextBox ID="txtDesignation" runat="server" Enabled="False" Width="260px"></asp:TextBox>
                </td></tr>


                      <tr> <td colspan="11" style="height:10px"> </td></tr>
           
            <tr> <td>
                <label>  Reporting Manager  </label> </td> <td style="width:10px"> </td> <td>
        <asp:TextBox ID="txtIncharge" runat="server" Enabled="False" Width="260px"></asp:TextBox> </td> <td style="width:10px"> </td> <td>
                HOD
                <label>  &nbsp;</label></td> <td style="width:10px"> </td><td> 
             <asp:TextBox ID="txtHOD" runat="server" Enabled="False" Width="260px"></asp:TextBox>
                </td> <td style="width:10px"> </td> <td>
                <label>  Pay Method  </label> </td> <td style="width:10px"> </td><td> 
             <asp:TextBox ID="txtPaymethod" runat="server" Enabled="False" Width="260px"></asp:TextBox>
                </td></tr>


                 <tr> <td colspan="11" style="height:10px"> </td></tr>

                  <tr> <td colspan="11" style="height:10px"> 
                  <table cellpadding="0px" cellspacing="0px"><tr> <td>  <label><%-- Profile Image--%> </label></td> <td style="width:10px"> </td> <td> 
               <asp:FileUpload ID="FileUpload1" runat="server" Visible="False"/> </td></tr> </table>   </td></tr>




          <tr> <td colspan="11" align="right" >  
              <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btnLogin" 
                  onclick="btnUpdate_Click" />
              <asp:Label ID="lblHRUserId" runat="server" Visible="False"></asp:Label>
              </td></tr>
          <tr> <td colspan="11" style="height:10px"> &nbsp;</td></tr>


           </table>
       
       </td></tr>
       </table>



          </fieldset>
           </td></tr>
        


    
        </table>
    




    </fieldset>

    
   
</asp:Content>

