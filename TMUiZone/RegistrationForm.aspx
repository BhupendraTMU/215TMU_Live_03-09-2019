<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="RegistrationForm.aspx.cs" Inherits="RegistrationForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width:830px"> 
<h3><p>Registration Form</p> </h3>
    
    </div>
     <asp:Panel ID="pnl1" runat="server" BorderColor="Gray" BorderWidth="2" Width="98%">
         <div>
    <table id="tbl1" width="98%" runat="server">
        <tr>
            <td>
                <asp:Label ID="lblName" runat="server" Text="Name" Font-Bold="true"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server" Text=""></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
            <td rowspan="5" align="right">
                &nbsp;</td>
        </tr>
       <%-- <tr>
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
           
        </tr>--%>
       

    </table>
             </div>
         <br />
     </asp:Panel>
   
    <asp:Panel ID="pnl2" runat="server" BorderColor="Gray" BorderWidth="2" Width="98%">
     <div>
         <br />
 <table id="Table2" width="98%" runat="server">
        <tr>
            <td colspan="4">
                <asp:Label ID="lblPaymentDetails" runat="server" Text="Payment Details :-" Font-Bold="true"></asp:Label>
            </td>                      
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPaymentID" runat="server" Text="Payment ID" ></asp:Label>
            </td>
            <td >
                <asp:TextBox ID="txtRegistrationID" runat="server" Text="" ></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblPaymentAmount" runat="server" Text="Payment Amount" ></asp:Label>
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtPaymentAmount" runat="server" Text="" ></asp:TextBox>
            </td>
            
           
        </tr>
       <tr>
            <td>
                <asp:Button ID="btnPay" runat="server" Text="Pay" Font-Bold="true" OnClick="btnPay_Click" ></asp:Button>
            </td>
            <td>
                
            </td>
            <td>
               
            </td>
            <td>
               
            </td>
           
        </tr>
        <%-- <tr>
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
          </tr>--%>
     <%--<tr>
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
              
          </tr>--%>
    </table>

    </div>
    </asp:Panel>
    <div>

    </div>
</asp:Content>

