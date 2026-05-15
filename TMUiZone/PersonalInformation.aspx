<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="PersonalInformation.aspx.cs" Inherits="PersonalInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width:830px"> 
<h3><p>PERSONAL INFORMATION </p> </h3>
    
    </div>
     <asp:Panel ID="Panel1" runat="server" BorderColor="Gray" BorderWidth="2" Width="98%">
         <div>
    <table width="98%" runat="server">
        <tr>
            <td>
                <asp:Label ID="lblName" runat="server" Text="Name" Font-Bold="true"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblbName" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblimage" runat="server" Text="Image" Font-Bold="true"></asp:Label>
            </td>
            <td rowspan="5" align="right">
               <asp:Image ID="imgStudent" runat="server" style="height:100px;width:85px;border-width:0px;" />                
              
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblEnrollmentNo" runat="server" Text="Enrollment No." Font-Bold="true"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblbEnrollmentNo" runat="server" Text=""></asp:Label>
            </td>
            <td>
                
            </td>
           
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblProgramme" runat="server" Text="Programme" Font-Bold="true" ></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblbProgramme" runat="server" Text=""></asp:Label>
            </td>
            <td>
                &nbsp;</td>
           
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblYesrSession" runat="server" Text="Year/Session" Font-Bold="true"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblbYearSession" runat="server" Text=""></asp:Label>
            </td>
            <td>
               
            </td>
           
        </tr>
        <tr>
            <td >
                <asp:Label ID="lblDept" runat="server" Text="Dept." Font-Bold="true"></asp:Label>
            </td>
            <td  colspan="2">
                <asp:Label ID="lblbDept" runat="server" Text=""></asp:Label>
            </td>
            
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDOB" runat="server" Text="DOB" Font-Bold="true"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblbDOB" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblBloodGroup" runat="server" Text="Blood Group" Font-Bold="true"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblbBloodGroup" runat="server" Text=""></asp:Label>
            </td>
        </tr><tr>
            <td>
                <asp:Label ID="lblMobileNumber" runat="server" Text="Mobile Number" Font-Bold="true"></asp:Label>
                
            </td>
            <td>
               <asp:Label ID="lblbMobileNumber" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEmailID" runat="server" Text="Email ID" Font-Bold="true"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblbEmailID" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblFatherName" runat="server" Text="Father's Name" Font-Bold="true"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblbFatherName" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblMotherName" runat="server" Text="Mother's Name" Font-Bold="true"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblbMotherName" runat="server" Text=""></asp:Label>
            </td>
        </tr>

    </table>
             </div>
         <br />
     </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" BorderColor="Gray" BorderWidth="2" Width="98%">
    <div >
       
        <table width="100%" runat="server" ID="tblAddress" border="1">
           <tr>
               <td align="center">
                   <b>Permanent Address</b>
               </td>
               <td align="center" >
                    <b>Correspondent Address</b>
               </td>
           </tr>
            <tr>
            <td>
                <table id="tblPAddress" width="98%" runat="server">
        <tr>
            <td>
                <asp:Label ID="lblPAddressLine1" runat="server" Text="Address Line 1" Font-Bold="true"></asp:Label>
            </td>
            <td colspan="3">
                <asp:Label ID="lblbPAddressLine1" runat="server" Text="" ></asp:Label>
            </td>            
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPAddressLine2" runat="server" Text="Address Line 2" ></asp:Label>
            </td>
            <td colspan="3">
                <asp:Label ID="lblbPAddressLine2" runat="server" Text="" ></asp:Label>
            </td>
            
           
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPDistt" runat="server" Text="Distt" Font-Bold="true" ></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblbPDistt" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblPPin" runat="server" Text="Pin"  ></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblbPPin" runat="server" Text=""></asp:Label>
            </td>
           
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPState" runat="server" Text="State" Font-Bold="true"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblbPState" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblPCountry" runat="server" Text="Country" Font-Bold="true"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblbPCountry" runat="server" Text=""></asp:Label>
            </td>
        </tr>       
          <tr>
              <td>
                <asp:Label ID="lblBankName" runat="server" Text="Bank Name" Font-Bold="true"></asp:Label>
              </td>
              <td colspan="3">
                <asp:Label ID="lblbBankName" runat="server" Text="" ></asp:Label>
              </td>
          </tr>
    </table>
            </td>
            <td>
                <table id="tblCAddress" width="98%" runat="server">
        <tr>
            <td>
                <asp:Label ID="lblCAddressLine1" runat="server" Text="Address Line 1" Font-Bold="true"></asp:Label>
            </td>
            <td colspan="3">
                <asp:Label ID="lblbCAddressLine1" runat="server" Text="" ></asp:Label>
            </td>            
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCAddressLine2" runat="server" Text="Address Line 2" ></asp:Label>
            </td>
            <td colspan="3">
                <asp:Label ID="lblbCAddressLine2" runat="server" Text="" ></asp:Label>
            </td>
            
            
           
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCDistt" runat="server" Text="Distt" Font-Bold="true" ></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblbCDistt" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCPin" runat="server" Text="Pin"  ></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblbCPin" runat="server" Text=""></asp:Label>
            </td>
           
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCState" runat="server" Text="State" Font-Bold="true"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblbCState" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCCountry" runat="server" Text="Country" Font-Bold="true"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblbCCountry" runat="server" Text=""></asp:Label>
            </td>
        </tr>       
        <tr>
              <td>
                <asp:Label ID="lblBankAcNo" runat="server" Text="Bank A/c No" Font-Bold="true"></asp:Label>
              </td>
              <td colspan="3">
                <asp:Label ID="lblbBankAcNo" runat="server" Text="" ></asp:Label>
              </td>
        </tr>
    </table>
            </td>
            </tr>
        </table>


    </div>
        
        </asp:Panel>
    <asp:Panel ID="Panel3" runat="server" BorderColor="Gray" BorderWidth="2" Width="98%">
     <div>
         <br />
 <table id="Table1" width="98%" runat="server">
        <tr>
            <td colspan="4">
                <asp:Label ID="lblImportantContactNo" runat="server" Text="IMPORTANT CONTACT NUMBERS" Font-Bold="true"></asp:Label>
            </td>                      
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDoctor" runat="server" Text="Doctor" ></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblbDoctor" runat="server" Text="" ></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDPhoneNumber" runat="server" Text="Phone Number" ></asp:Label>
            </td>
            <td colspan="3">
                <asp:Label ID="lblbDPhoneNumber" runat="server" Text="" ></asp:Label>
            </td>
            
           
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPrincipalHOD" runat="server" Text="Principal/HOD" Font-Bold="true" ></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblbPrincipalHOD" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDPPhoneNumber" runat="server" Text="Phone Number"  ></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblbPPhoneNumber" runat="server" Text=""></asp:Label>
            </td>
           
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblLocalGuardian" runat="server" Text="Local Guardian" Font-Bold="true"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblbLocalGuardian" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblRelation" runat="server" Text="Relation" Font-Bold="true"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblbRelation" runat="server" Text=""></asp:Label>
            </td>
        </tr>       
          <tr>
              <td>
                <asp:Label ID="lblImpContactNumber" runat="server" Text="Contact Number" Font-Bold="true"></asp:Label>
              </td>
              <td >
                <asp:Label ID="lblbImpContactNumber" runat="server" Text="" ></asp:Label>
              </td>
              <td>
                <asp:Label ID="lblImpEmailID" runat="server" Text="Email ID" Font-Bold="true"></asp:Label>
              </td>
              <td >
                <asp:Label ID="lblbImpEmailID" runat="server" Text="" ></asp:Label>
              </td>
          </tr>
     <tr>
              <td>
                <asp:Label ID="lblImpAddress" runat="server" Text="Address" Font-Bold="true"></asp:Label>
              </td>
              <td >
                <asp:Label ID="lblbImpAddress" runat="server" Text="" colspane="3"></asp:Label>
              </td>
              
          </tr>
      <tr>
              <td>
                <asp:Label ID="lblInCaseOf" runat="server" Text="IN CASE OF EMERGENCY, PLEASE NOTIFY:" Font-Bold="true"></asp:Label>
              </td>
              <td >
                <asp:Label ID="lblbInCaseOf" runat="server" Text="" colspane="3"></asp:Label>
              </td>
              
          </tr>
    </table>

    </div>
    </asp:Panel>
    <div>

    </div>
</asp:Content>


