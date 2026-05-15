<%@ Page Title="" Language="C#" MasterPageFile="~/IndexMaster.master" AutoEventWireup="true" CodeFile="profile.aspx.cs" Inherits="AccountSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset class="boxBody">
 <asp:Label ID="Label1" runat="server" 
            Text="View Profile Detail" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 </fieldset>

 <fieldset class="boxBodyHeader"> 
 <asp:Label ID="Label2" runat="server" 
            Text="Personal Information" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 
 </fieldset>
  <fieldset class="boxBodyInner">
    <table cellpadding="0px" cellspacing="0px">
    
            <tr> <td colspan="15" style="height:5px"> 
                <asp:Panel ID="pnlApprovalDetail" runat="server" Visible="false" CssClass="boxBodyHeader">
              
              <table cellpadding="0px" cellspacing="0px"> <tr> <td> <label> <asp:Label ID="Label5" runat="server" 
            Text="Profile Updated Status" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label></label> </td> <td style="width:10px"> </td> <td>
                  <asp:Label ID="lblProfileStatus" runat="server" Font-Bold="False" Visible="false"
                      Font-Size="15pt" ForeColor="#990000"  Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label> </td> <td style="width:100px"> </td> <td> <asp:Label ID="Label6" runat="server" 
            Text="Profile Updated Date" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" Visible="false"></asp:Label> </td> <td style="width:10px"> </td><td><asp:Label ID="lblUpdatedDate" runat="server" 
            Text="Profile Updated Date" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" Visible="false"></asp:Label><asp:Label ID="lblHRUpdatedate" runat="server" 
            Text="Profile Updated Date" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" Visible="false" ></asp:Label> </td></tr></table>
              
                </asp:Panel>
              </td></tr>

            <tr> <td colspan="15" style="height:5px"> </td></tr>

    
      <tr>  <td> <label> Title </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txttytle" runat="server" Enabled="False" Width="50px"></asp:TextBox></td> <td style="width:10px"> </td>  <td> <label> First Name </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtFName" runat="server" Enabled="False" Width="130px"></asp:TextBox></td>  <td style="width:10px"> </td>  <td> <label> Second Name </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtSName" runat="server" Enabled="False" Width="110px"></asp:TextBox></td> <td style="width:10px"> </td>  <td> <label> Last Name </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtLName" runat="server" Enabled="False" Width="125px"></asp:TextBox></td></tr>
        
        
        <tr> <td colspan="15" style="height:10px"> </td></tr>




         


        <tr> <td> <label> Address </label>  </td> <td style="width:10px"> </td> <td colspan="14">   
            <asp:TextBox ID="txtAddress" runat="server" Enabled="False" Width="819px" 
                TextMode="MultiLine" MaxLength="30"></asp:TextBox></td></tr>

                 <tr> <td colspan="15" style="height:10px"> </td></tr>

                 <tr> <td> <label> Address2 </label>  </td> <td style="width:10px"> </td> <td colspan="14">   
            <asp:TextBox ID="txtAddress2" runat="server" Enabled="False" Width="819px" 
                         TextMode="MultiLine"></asp:TextBox></td></tr>


  <tr> <td colspan="15" style="height:10px"> </td></tr>

       <tr>  <td> <label> City</label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtCity" runat="server" Enabled="False" Width="110px"></asp:TextBox></td> <td style="width:10px"> </td>  <td> <label>State </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtSTate" runat="server" Enabled="False" Width="130px"></asp:TextBox></td>  <td style="width:10px"> </td>  <td> <label> Country </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtCountry" runat="server" Enabled="False" Width="110px"></asp:TextBox></td> <td style="width:10px"> </td>  <td> <label> Pin Code </label> </td> <td style="width:10px"> </td> <td> 
       <asp:TextBox ID="txtPincode" runat="server" Enabled="False" Width="125px"></asp:TextBox></td></tr>


          <tr> <td colspan="15" style="height:10px"> </td></tr>

         <tr>  <td> <label>Email Id </label> </td> <td style="width:10px"> </td> <td colspan="5"> 
             <asp:TextBox ID="txtEmailid" runat="server" Enabled="False" Width="400px"></asp:TextBox>
        </td> <td style="width:10px"> </td>
        <td> <label> Date Of Birth </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtDOB" runat="server" Enabled="False" Width="110px"></asp:TextBox></td>
        <td style="width:10px"> </td>
           <td> <label> Sex </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtSex" runat="server" Enabled="False" Width="125px"></asp:TextBox></td></tr>


          <tr> <td colspan="15" style="height:10px"> </td></tr>

     
       
       <tr> <td colspan="15" > 
       
       <table cellpadding="0px" cellspacing="0px"> 
       
       
       <tr> <td><label>  Father Name </label> </td> <td style="width:10px"> </td> <td>
           <asp:TextBox ID="txtFatherName" runat="server" Enabled="False" Width="200px"></asp:TextBox> </td> <td style="width:10px"> </td> <td><label>  Husband Name </label> </td> <td style="width:10px"> </td><td> 
           <asp:TextBox ID="txtHusBandName" runat="server" Enabled="False" Width="200px"></asp:TextBox></td> <td style="width:10px"> </td> <td></td> <td style="width:10px"> </td><td> 
           </td></tr>
           <tr> <td colspan="11" style="height:10px"> </td></tr>
           
            <tr> <td><label>  Marital Status </label> </td> <td style="width:10px"> </td> <td>
           <asp:TextBox ID="txtStatus" runat="server" Enabled="False" Width="200px"></asp:TextBox> </td> <td style="width:10px"> </td> <td><label>  Contact No </label> </td> <td style="width:10px"> </td><td> 
           <asp:TextBox ID="txtMobileNo" runat="server" Enabled="False" Width="200px"></asp:TextBox></td> <td style="width:10px"> </td> <td><label>  Phone No </label> </td> <td style="width:10px"> </td><td> 
           <asp:TextBox ID="txtPhoneNo" runat="server" Enabled="False" Width="190px"></asp:TextBox></td></tr>
           </table>
       
       </td></tr>

        <tr> <td colspan="15" style="height:10px"> </td></tr>

          <tr> <td colspan="15" style="height:10px">
          <fieldset class="boxBodyHeader"> 
          <asp:Label ID="Label3" runat="server" 
            Text="Permanent Address" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 

          </fieldset>
          <fieldset class="boxBodyInner"> 



         <table cellpadding="0px" cellspacing="0px"> 
         
         
     


         <tr> <td colspan="15" style="height:10px"> </td></tr>
         <tr> <td> <label> Address </label>  </td> <td style="width:10px"> </td> <td colspan="14">   
            <asp:TextBox ID="txtAddressPermanent" runat="server" Enabled="False" 
                 Width="785px" TextMode="MultiLine"></asp:TextBox></td></tr>

                 <tr> <td colspan="15" style="height:10px"> </td></tr>

                 <tr> <td> <label> Address2 </label>  </td> <td style="width:10px"> </td> <td colspan="14">   
            <asp:TextBox ID="txtAddress2Permanent" runat="server" Enabled="False" Width="785px" 
                         TextMode="MultiLine"></asp:TextBox></td></tr>



        <tr> <td colspan="15" style="height:10px"> </td></tr>


         <tr>  <td> <label> City </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtCityPermanent" runat="server" Enabled="False" Width="110px"></asp:TextBox></td> <td style="width:10px"> </td>  <td> <label> State </label> </td> <td style="width:10px"> </td> <td> 
        <asp:TextBox ID="txtStatePermanent" runat="server" Enabled="False" Width="130px"></asp:TextBox></td>  <td style="width:10px"> </td>  <td></td> <td style="width:10px"> </td> <td> 
        </td> <td style="width:10px"> </td>  <td> </td> <td style="width:10px"> </td> <td> 
        </td>
        
        
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



     




        <table cellpadding="0px" cellspacing="0px"> 

          <tr> <td colspan="15" > 
       
       <table cellpadding="0px" cellspacing="0px"> 
       
       
       <tr> <td><label>  Pan No </label> </td> <td style="width:10px"> </td> <td>
        <asp:TextBox ID="txtPanNo" runat="server" Enabled="False" Width="200px"></asp:TextBox> </td> <td style="width:10px"> </td> <td>
           ESI No<label>  </label> </td> <td style="width:10px"> </td><td> 
        <asp:TextBox ID="txtEsiNo" runat="server" Enabled="False" Width="200px"></asp:TextBox></td> <td style="width:10px"> </td> <td>
           PF No<label>  </label> </td> <td style="width:10px"> </td><td> 
        <asp:TextBox ID="txtPFNo" runat="server" Enabled="False" Width="190px"></asp:TextBox></td></tr>
           <tr> <td colspan="11" style="height:10px"> </td></tr>
           
            <tr> <td>Department<label>  </label> </td> <td style="width:10px"> </td> <td>
        <asp:TextBox ID="txtDepartment" runat="server" Enabled="False" Width="200px"></asp:TextBox> </td> <td style="width:10px"> </td> <td>
                Email
                <label>  &nbsp;</label></td> <td style="width:10px"> </td><td> 
        <asp:TextBox ID="txtCompanyMail" runat="server" Enabled="False" Width="200px"></asp:TextBox></td> <td style="width:10px"> </td> <td>
                AC/No<label>  </label> </td> <td style="width:10px"> </td><td> 
             <asp:TextBox ID="txtACNO" runat="server" Enabled="False" Width="190px"></asp:TextBox>
                </td></tr>

                 <tr> <td colspan="11" style="height:10px"> </td></tr>
           
            <tr> <td>
                <label>  Branch  </label> </td> <td style="width:10px"> </td> <td>
        <asp:TextBox ID="txtBranch" runat="server" Enabled="False" Width="200px"></asp:TextBox> </td> <td style="width:10px"> </td> <td>
                DOJ
                <label>  &nbsp;</label></td> <td style="width:10px"> </td><td> 
             <asp:TextBox ID="txtDOJ" runat="server" Enabled="False" Width="200px"></asp:TextBox>
                </td> <td style="width:10px"> </td> <td>
                Designation<label>  </label> </td> <td style="width:10px"> </td><td> 
        <asp:TextBox ID="txtDesignation" runat="server" Enabled="False" Width="190px"></asp:TextBox>
                </td></tr>


                      <tr> <td colspan="11" style="height:10px"> </td></tr>
           
            <tr> <td>
                <label>  Incharge  </label> </td> <td style="width:10px"> </td> <td>
        <asp:TextBox ID="txtIncharge" runat="server" Enabled="False" Width="200px"></asp:TextBox> </td> <td style="width:10px"> </td> <td>
                HOD
                <label>  &nbsp;</label></td> <td style="width:10px"> </td><td> 
             <asp:TextBox ID="txtHOD" runat="server" Enabled="False" Width="200px"></asp:TextBox>
                </td> <td style="width:10px"> </td> <td>
                <label>  Pay Method  </label> </td> <td style="width:10px"> </td><td> 
             <asp:TextBox ID="txtPaymethod" runat="server" Enabled="False" Width="190px"></asp:TextBox>
                </td></tr>


           </table>
       
       </td></tr>
       </table>

          </fieldset>
           </td></tr>
        


    
        </table>
    




    </fieldset>

    
</asp:Content>

