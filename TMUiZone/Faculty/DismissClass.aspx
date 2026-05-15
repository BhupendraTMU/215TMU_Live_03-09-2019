<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="DismissClass.aspx.cs" Inherits="Faculty_DismissClass_" %>

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
        function checkDate(sender, args) {
            if (sender._selectedDate < new Date()) {
                alert("You cannot select Less than current date!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
    </script>
    <style type="text/css">
        [required] {
    
    box-shadow: none;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
      <fieldset class="boxBody">
 <asp:Label ID="Label1" runat="server" 
            Text="Dismiss Class" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

           &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    Academic Year:&nbsp&nbsp
                    <asp:DropDownList ID="ddlAcademicYear" Width="100px" Height="20px" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged"></asp:DropDownList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Font-Size="13px"  runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="ddlAcademicYear" ValidationGroup="g1" ErrorMessage="Please select the Academic Year!"></asp:RequiredFieldValidator>
 </fieldset>
    <fieldset class="boxBodyHeader"> 
  
 </fieldset>

     <fieldset class="boxBodyInner">

         <center>
             <table>
                 <tr>
                     <td>
                        

                     </td>
                     <td>
                         <asp:CheckBox ID="chkAllDay" runat="server"  Text="Dismiss All Class" AutoPostBack="True" OnCheckedChanged="chkAllDay_CheckedChanged"/>
                         <br />
                                                  <br />
                     </td>
                     <td align="right">
                         Date:&nbsp;
                     </td>
                     <td align="left">
                         &nbsp; <asp:TextBox ID="txtDate" runat="server" placeholder="Select Date" onkeypress="return false" onKeyDown="preventBackspace();"  ></asp:TextBox>

                         <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate" Format="dd MMM yyyy" ></asp:CalendarExtender> <%--OnClientDateSelectionChanged="checkDate"--%> 
                                                  <br />

                     </td>
                     
                                   </tr>
                 <tr> 

                     <!--course drp-->
                     <td>
                         <br />
                     </td>

                     
                     <td>Course: &nbsp;</td>
                     <td>
                         <asp:DropDownList ID="drpCourse" runat="server" AutoPostBack="true" Width="150px" Height="20px" BackColor="#e8e8e8" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged"></asp:DropDownList>

                     </td>
                
                                          <!--Semester/Year: drp-->

                     
                     <td> &nbsp;Semester/Year:</td>
                     
                     <td></td>
                     <td>
                         <asp:DropDownList ID="drpSemester" runat="server"  AutoPostBack="true" Width="150px" Height="20px" BackColor="#e8e8e8" OnSelectedIndexChanged="drpSemester_SelectedIndexChanged"></asp:DropDownList>
                     </td>
                     
                     
                     <!--Section: drp-->
                     
                     
                     <td>&nbsp;</td>
                     <td>Section:</td>

                     <td>
                         <asp:DropDownList ID="drpSection" runat="server"  AutoPostBack="true" Width="150px" Height="20px" BackColor="#e8e8e8" OnSelectedIndexChanged="drpSection_SelectedIndexChanged"></asp:DropDownList>
                     </td>

                     <!--Subject: drp-->
                   <td>&nbsp;</td>
                     <td>Subject:</td>
                     <td>
                         <asp:DropDownList ID="drpSubject" runat="server" Width="150px" Height="20px" BackColor="#e8e8e8" AutoPostBack="True" OnSelectedIndexChanged="drpSubject_SelectedIndexChanged"></asp:DropDownList>
                     </td>
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                        </tr>

                 <tr>
                      <td></td>
                     <td></td>
                     <td>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Font-Size="13px"  runat="server" Display="Dynamic" ErrorMessage="Please select the Course!" ValidationGroup="g1"  ForeColor="Red" ControlToValidate="drpCourse"></asp:RequiredFieldValidator>

                     </td>
                
                                          <!--Semester/Year: drp-->

                     
                     <td></td>
                     
                     <td></td>
                     <td>
                        
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Font-Size="13px"  runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="drpSemester" ValidationGroup="g1" ErrorMessage="Please select the Semester!"></asp:RequiredFieldValidator>
                         
                     </td>
                     
                    <td></td>
                     <td></td>
                     <td><br /></td>
                 </tr>


                 <tr>
                     <!--- Group---->
                     <td> </td>
                     <td>Group :</td>
                     <td>
                         <asp:DropDownList ID="ddlGroup" Width="150px" Height="20px" BackColor="#e8e8e8" runat="server" ></asp:DropDownList>
                     </td>

                     <!--- Batch---->



                     <td>&nbsp;Batch:</td>
                     <td></td>
                     <td>
                          <asp:DropDownList ID="ddlBatch" Width="150px" Height="20px" BackColor="#e8e8e8"  runat="server"  ></asp:DropDownList> 
                     </td>
                      <!--- Lecture---->
                     <td></td>
                     <td> Lecture:</td>
                     <td>
                         <asp:DropDownList ID="drpLecture" Width="150px" Height="20px" BackColor="#e8e8e8" AutoPostBack="true" runat="server" >
                             
                            


                         </asp:DropDownList>   
                     </td>



                     <!--- date---->


                     <td></td>
                     <td></td>
                     <td>


                        &nbsp;<asp:Button ID="btnDismiss" runat="server" Text="Dismiss" CssClass="btn-info" OnClick="btnDismiss_Click" />


                     </td>




                     <td></td>
                     <td></td>
                     <td>
                         </td>

                 </tr>


             </table>
             <table>
                 <tr>
                     <td>



                         <asp:GridView ID="grdDismissClass"  runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="866px" GridLines="Horizontal"  EmptyDataText="There are no data records to display." >
              
                             <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
               <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
               <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
               <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont"   />
               <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
               <SortedAscendingCellStyle BackColor="#F4F4FD" />
               <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
               <SortedDescendingCellStyle BackColor="#D8D8F0" />
               <SortedDescendingHeaderStyle BackColor="#3E3277" />

                         </asp:GridView>
                         
                     </td>
                 </tr>
             </table>
         </center>


     </fieldset>
    
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

