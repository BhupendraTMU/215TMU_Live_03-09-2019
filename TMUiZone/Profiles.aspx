<%@ Page Title="TMU" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Profiles.aspx.cs" Inherits="Profiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div style="width:830px">

    <table cellpadding="0px" cellspacing="0px" style="width:100%"> <tr> <td colspan="3"> <p class="Navigator">TMU &gt;&gt; Profile </p> </td></tr> 


        <tr> <td style="height:5px" colspan="3"> </td></tr>


        
        <tr> <td align="right">  Name : </td> <td style="width:10px"> </td> <td>  <asp:Label ID="lblName" runat="server" Text=""></asp:Label> </td></tr>

         <tr> <td style="height:15px" colspan="3"> </td></tr>

        <tr> <td align="right">  Father Name : </td> <td style="width:10px"> </td> <td> 
            <asp:TextBox ID="txtFatherName" runat="server" Width="400px" Font-Size="7pt"></asp:TextBox> </td></tr>

         <tr> <td style="height:15px" colspan="3"> </td></tr>

          <tr> <td align="right">  Mother Name : </td> <td style="width:10px"> </td> <td> 
              <asp:TextBox ID="txtMotherName" runat="server" Width="400px" Font-Size="7pt"></asp:TextBox> </td></tr>

         <tr> <td style="height:15px" colspan="3"> </td></tr>


         <tr> <td align="right">  DOB : </td> <td style="width:10px"> </td> <td>  <asp:Label ID="lblDOB" runat="server" Text=""></asp:Label> </td></tr>

         <tr> <td style="height:15px" colspan="3"> </td></tr>

          <tr> <td align="right">  Address : </td> <td style="width:10px"> </td> <td>  
              <asp:TextBox ID="txtAddress" runat="server" Width="400px" Font-Size="7pt"></asp:TextBox> </td></tr>

         <tr> <td style="height:15px" colspan="3"> </td></tr>
           <tr> <td align="right">  Address 2 : </td> <td style="width:10px"> </td> <td>  
               <asp:TextBox ID="txtAddress2" runat="server" Width="400px" Font-Size="7pt"></asp:TextBox> </td></tr>

         <tr> <td style="height:15px" colspan="3"> </td></tr>


           <tr> <td align="right">  City : </td> <td style="width:10px"> </td> <td>  <table cellpadding="0px" cellspacing="0px"> <tr> <td> 
               <asp:TextBox ID="txtCity" runat="server" Width="100px" Font-Size="7pt"></asp:TextBox></td> <td style="width:10px"> </td> <td>  Country </td> <td style="width:10px">  </td> <td> 
               <asp:TextBox ID="txtCountry" runat="server" Width="100px" Font-Size="7pt"></asp:TextBox> </td> <td style="width:10px"> </td> <td> Pin </td><td style="width:10px"> </td> <td> 
               <asp:TextBox ID="txtPin" runat="server" Width="100px" Font-Size="7pt"></asp:TextBox></td></tr> </table> </td></tr>

         <tr> <td style="height:15px" colspan="3"> </td></tr>

           <tr> <td align="right">  Email : </td> <td style="width:10px"> </td> <td>  
               <asp:TextBox ID="txtEmail" runat="server" Width="400px" Font-Size="7pt"></asp:TextBox> </td></tr>

         <tr> <td style="height:15px" colspan="3"> </td></tr>

           <tr> <td align="right">  Phone : </td> <td style="width:10px"> </td> <td>  
               <asp:TextBox ID="txtPhone" runat="server" Width="400px" Font-Size="7pt"></asp:TextBox> </td></tr>


         <tr> <td style="height:15px" colspan="3"> </td></tr>

           <tr> <td align="right">  Academic Year : </td> <td style="width:10px"> </td> <td>  <asp:Label ID="lblAcademicYRS" runat="server" Text=""></asp:Label></td></tr>

          <tr> <td style="height:15px" colspan="3"> </td></tr>

           <tr> <td align="right">  Course : </td> <td style="width:10px"> </td> <td>  <asp:Label ID="lblCourse" runat="server" Text=""></asp:Label></td></tr>

         <tr> <td style="height:15px" colspan="3"> </td></tr>
        

           <tr> <td align="right">  Gender : </td> <td style="width:10px"> </td> <td> <asp:RadioButton ID="rdmale" runat="server" Text="Male" GroupName="gender"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rdFemale" runat="server" Text="Female" GroupName="gender"/>  </td></tr>

         <tr> <td style="height:15px" colspan="3"> </td></tr>
         
         <tr> <td colspan="3" align="right">     <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </td></tr>
        
         

    </table>
        </div>
</asp:Content>

