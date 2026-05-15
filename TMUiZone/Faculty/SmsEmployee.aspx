<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="smsEmployee.aspx.cs" Inherits="Faculty_smsEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
     
            <fieldset class="boxBody">
                 <asp:Label ID="Label2" runat="server"
                    Text="Sms To Employee" Font-Size="15pt" ForeColor="#093A62"
                    Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
</fieldset>
            <fieldset class="boxBodyHeader"> 
  
 </fieldset>
    <%--  <table cellpadding="0px" cellspacing="0px" style="width: 100%">
        <tr>
            <td style="height: 10px">
                 <asp:Label ID="lblHeader" runat="server"
                    Text="Sms To Employee" Font-Size="15pt" ForeColor="#093A62"
                    Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
            </td>
        </tr>
            </table>--%>
    <center>
         <fieldset class="boxBodyInner">

      <fieldset class="boxBodyInner">
        <table>
            <tr>
                <td>



                    <center>

                        <%--<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>--%>
                        <table>
            <tr>
                <td style=" padding: 5px;">College</td>
                <td style="padding: 5px;">
                    <asp:Label ID="lblCollege" runat="server" Text=""></asp:Label>
                    <asp:DropDownList ID="DrpCollege" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpCollege_SelectedIndexChanged" Width="250px"></asp:DropDownList>
                    </td>
                <td style=" padding: 5px;">Department</td>
                <td style=" padding: 5px;" colspan="2"><asp:DropDownList ID="DrpDepartment" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpDepartment_SelectedIndexChanged" Width="250px"></asp:DropDownList>
                    </td>

                </tr><tr>
                <td style="padding: 5px;">Grade</td>
                <td style=" padding: 5px;">
                    <asp:DropDownList ID="DrpGrade" runat="server" OnSelectedIndexChanged="DrpGrade_SelectedIndexChanged" Width="250px"></asp:DropDownList></td>
              <%--  <td>Category</td>
                <td>
                    <asp:DropDownList ID="DrpCategory" runat="server"></asp:DropDownList></td>
                <td>Designation</td>
                <td>
                    <asp:DropDownList ID="DrpDesignation" runat="server"></asp:DropDownList></td>--%>
                <td style="height: 10px; padding: 5px;">Religion</td>
                <td style="height: 10px; padding: 5px;">
                    <asp:DropDownList ID="DrpReligion" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpReligion_SelectedIndexChanged" Width="250px"></asp:DropDownList></td>
                
                     </tr><tr>
                    <td style="height: 10px; padding: 5px;">Gender</td>
                <td  style="height: 10px; padding: 5px;">
                    <asp:DropDownList ID="DrpGender" runat="server" Width="250px">
                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                        <asp:ListItem Value="1">Female</asp:ListItem>
                        <asp:ListItem Value="2">Male</asp:ListItem>
                    </asp:DropDownList></td>
                         <td>

                         </td>
                         <td>
                             <asp:CheckBox  runat="server" id="chkAll" Text="All" />
                         </td>
            </tr>
                            <tr>
                                <td style="height: 10px; padding: 15px;">Write Sms</td>
                                <td style="height: 10px; padding: 15px;" colspan="3">
                                    <asp:TextBox ID="Txtsms" runat="server" Width="800px" TextMode="MultiLine" Height="54px"></asp:TextBox></td>
                            </tr>
                           
        </table>

                    </center>
                </td>
            </tr>
            
        </table>
          </fieldset>
          <div style="text-align:center;padding-top:5px;">
             
                              
                                    <asp:Button ID="btnsendsms" runat="server" Text="Send" OnClick="btnsendsms_Click" />

                     
          </div>
        </fieldset> 
    </center>
            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

