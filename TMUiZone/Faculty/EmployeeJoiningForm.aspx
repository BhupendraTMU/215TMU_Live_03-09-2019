<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="EmployeeJoiningForm.aspx.cs" EnableEventValidation="false" Inherits="Faculty_EmployeeJoiningForm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
 


 

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style6 {
            width: 10px;
        }

        .auto-style9 {
            height: 28px;
        }

        .auto-style10 {
            width: 10px;
            height: 28px;
        }

        .auto-style11 {
            width: 1200px;
        }

        .auto-style13 {
            width: 257px;
        }

        .auto-style14 {
            width: 134px;
        }

        .auto-style15 {
            width: 257px;
            visibility: hidden;
        }

        .auto-style16 {
            height: 10px;
        }
        
element.style {
    height: 100px;
    width: 100px;
    border-width: 0px;
}
    </style>
   </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
   <script type="text/javascript"> 
     function numeric(evt)
    {
   var charCode = (evt.which) ? evt.which : event.keyCode
   if(charCode > 31 && ((charCode >= 48 && charCode <= 57) || charCode == 46))
   return true;
   else
   {
    alert('Please Enter Number Only .');
    return false;
   }
}
    </script>
    <script type="text/javascript"> 


        function RefreshPage() {
            window.location.reload()
        }
</script>
    <script src="//code.jquery.com/jquery-1.11.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ShowImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=ImgPrv.ClientID%>').prop('src', e.target.result)
                        .width(100)
                        .height(100);
                };
                reader.readAsDataURL(input.files[0]);
                }
            }
    </script>
  
    <table>
        <tr>
            <td style="width: 20px"></td>
            <td>
                <asp:Button ID="btn_addnew" runat="server" CssClass="btn-sm btn-primary btn-block"  OnClick="btn_addnew_Click" ValidationGroup="g1" Height="30px" Width="150px" Text="Add New Joining" />
            </td>
            <td class="auto-style1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Employee Name:
            </td>

            <td style="width: 10px"></td>
            <td>

                <asp:TextBox ID="txtempname" placeholder="Employee Name" runat="server" Width="200px" BorderColor="Black" autocomplete="off" oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>


                <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="txtempname" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>


            </td>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            
        <td>
            <asp:Button ID="Search" runat="server" OnClick="Search_Click" CssClass="btn-sm btn-primary btn-block" Height="30px" Width="90px" Text="Show Report" />
        </td>
            <td style="width: 10px"></td>
            <td>
                <asp:Button ID="btnexporttoexel" runat="server" OnClick="btnexporttoexel_Click" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" Height="30px" Width="100px" Text=" Export To Excel" />
            </td>
           <td style="width: 10px"></td>
            <td>
                <asp:Button ID="BtnRejected" runat="server" CssClass="btn-sm btn-primary btn-block" OnClick="BtnRejected_Click" Visible="false" ValidationGroup="g1" Height="30px" Width="100px" Text="Delete" />
                 
            </td>
        </tr>
         </table>
    
     <fieldset class="boxBodyInner">
        <br />
         
        <asp:GridView ID="grdnewjoininglist" runat="server" DataKeyNames="ID" AlternatingRowStyle-CssClass="danger" PageSize="50" AllowPaging="true" AutoGenerateColumns="false" OnPageIndexChanging="grdnewjoininglist_PageIndexChanging" CssClass="table table-striped table-bordered table-hover" Visible="true">
            <PagerSettings Mode="NumericFirstLast" />
            <PagerStyle CssClass="csspager" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <%# Container.DataItemIndex +1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="ID" ItemStyle-Width="3%" Visible="false" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblemployeecode" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                        <asp:HiddenField ID="Hfemployeecode" Value='<%# Eval("ID") %>' runat="server" />
                        <asp:HiddenField ID="Hfhodname" Value='<%# Eval("ID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>


                 <asp:TemplateField HeaderText="Employee Name" ItemStyle-Width="10%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                    <ItemTemplate>
                        <asp:Label ID="lblemployeename" runat="server" Text='<%# Eval("FULL_NAME") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                
                <asp:TemplateField HeaderText="Edit" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkbutton" runat="server" OnClick="lnkbutton_Click" >Edit</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Father Name" ItemStyle-Width="10%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                    <ItemTemplate>
                        <asp:Label ID="lblemployeefathername" runat="server" Text='<%# Eval("FATHER_NAME") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date Of Birth" ItemStyle-Width="6%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                    <ItemTemplate>
                        <asp:Label ID="lbldateofbirth" runat="server" Text='<%# Eval("DATE_OF_BIRTH") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date Of Joining" ItemStyle-Width="7%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lbldateofjoining" runat="server" Text='<%#Eval("DATE_OF_JOINING") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Employee Type" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblemployeetype" runat="server" Text='<%# Eval("EMPLOYEE_TYPE") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Unit(College/Dept.)" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblunit" runat="server" Text='<%#Eval("UNIT_COLLEGE_DEPARTMENT") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Designation" ItemStyle-Width="2%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lbldesignation" runat="server" Text='<%# Eval("DESIGNATION") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Mobile No." ItemStyle-Width="2%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblmobile" runat="server" Text='<%# Eval("MOBILE_NO") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>
                
                 <asp:TemplateField HeaderText="Email ID" ItemStyle-Width="2%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                     <ItemTemplate>
                         <asp:Label ID="lblemailid" runat="server" Text='<%# Eval("E_MAIL_ID") %>'></asp:Label>
                     </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Category" ItemStyle-Width="2%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblcategory" runat="server" Text='<%# Eval("CATEGORY") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Domicile State" ItemStyle-Width="2%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lbldomicilestate" runat="server" Text='<%# Eval("DOMICILE_STATE") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Aadhar No." ItemStyle-Width="2%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblAadharno" runat="server" Text='<%# Eval("AADHAR_NO") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Gender" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Label ID="lblhrstatus" runat="server" Text='<%#Eval("GENDER") %>' Style="text-transform: uppercase;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" HeaderText="Select"  ItemStyle-CssClass="text-center">          
                    <ItemTemplate>
                        <asp:CheckBox ID="Chkemployee" runat="server" AutoPostBack="true" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>No Record To Display</EmptyDataTemplate>
        </asp:GridView>
          
          </fieldset>
<asp:Panel ID="pnlGridViewdata" CssClass="modalPopup"  Width="85%" runat="server" ScrollBars="Vertical" Height="950px" Style="display: none; margin-top:180px">
       <fieldset class="boxBody">
           <div class="close">
                    <asp:Button ID="btnclose" OnClick="btnclose_Click" runat="server" Text="X" ForeColor="Red" BackColor="White"/>
                </div>
         

           

          <div style="width:100%;margin-bottom:10px;margin-left:1%;margin-right:1%;margin-top:5px;">

              <table>
                  

                      <tr>
                          <td style="width: 1%"></td>
                          <td style="width: 12%; text-align: left">
                              <asp:Image ID="Image1" runat="server" ImageUrl="~/images/UPDATEDLOGO.jpg" Width="55%" />

                          </td>
                          <td style="width: 65%; text-align: center">
                              <strong>
                                  <asp:Label ID="lblCName" Font-Size="Large" Text="Teerthanker Mahaveer University, Moradabad" runat="server"></asp:Label></strong>
                              <br />
                              <strong>
                                  <asp:Label ID="lblAC" runat="server" Text="(Established under Govt. of U. P. Act No. 30, 2008)"></asp:Label></strong>

                              <br />
                              <strong>
                                  <asp:Label ID="LblType" runat="server" Text="Delhi Road,(146 Kms from Delhi on N.H. 24) Moradabad(U.P) India"></asp:Label>
                              </strong>
                              <br />
                              <strong>
                                  <asp:Label ID="lbltel" runat="server" Text=" Tel.:+91-2360222 , 2360777"></asp:Label>
                              </strong>
                              <br />
                              <strong>
                                  <asp:Label ID="lblemail" runat="server" Text="Email:university@tmu.ac.in;  hr@tmu.ac.in;  Website:www.tmu.ac.in"></asp:Label>
                              </strong>
                          </td>
                          <td style="width: 10%; text-align: center"></td>
                      </tr>
                  </div>
       
        </table>
     </fieldset>
    <fieldset class="boxBody" style="text-align: center;border-color:black; background-color:black;">
 <asp:Label ID="Label1" runat="server" Text="EMPLOYEE JOINING FORM" Font-Size="15pt"  ForeColor="White" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

 </fieldset>
    <br />
       <fieldset class="boxBody" style="text-align: left; width:1200px">
 <asp:Label ID="Label2" runat="server" Text="EMPLOYEE DETAIL" Font-Size="15pt"  ForeColor="Black" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

           :</fieldset>
    <br />
    
    <div id="divGeneralBody">
                     <fieldset class="boxBodyInner"> 

                         <table cellpadding="0px" cellspacing="0px"> 
                          <tr> 
                              <td colspan="15" >      
                                <table cellpadding="0px" cellspacing="0px">       
                               <tr> 
                                    <td class="auto-style9"><label style="line-height:25px">  Title. </label> </td> 
                                    <td class="auto-style10"> </td> 
                                    <td class="auto-style9">
                                        <asp:DropDownList ID="drptitle" runat="server" Width="180px" Height="28px" BorderColor="Black">
                                         <asp:ListItem Text="--Select Employee Title--" Value="0"></asp:ListItem>
                                         <asp:ListItem Text="Dr." Value="1"></asp:ListItem>
                                         <asp:ListItem Text="Mr." Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Ms." Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="drptitle" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                    </td> 
                                    <td class="auto-style10"> </td> 
                                    <td class="auto-style9"><label style="line-height:25px"> Name. </label> </td> 
                                    <td class="auto-style10"> </td>
                                    <td class="auto-style9"> 
                                        <asp:TextBox ID="txtemployeename" runat="server" BorderColor="Black"  Width="180px"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtemployeename" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                    </td> 
                                    <td class="auto-style10"> </td> 
                                    <td class="auto-style9"><label style="line-height:25px"> Employee Type.</label> </td> 
                                    <td class="auto-style10"> </td>
                                    <td class="auto-style9"> 
                                            <asp:DropDownList ID="drpemployeetype" runat="server" Width="180px" Height="28px" BorderColor="Black">
                                         <asp:ListItem Text="--Select Employee Type--" Value="0"></asp:ListItem>
                                         <asp:ListItem Text="Teaching" Value="1"></asp:ListItem>
                                         <asp:ListItem Text="Non-Teaching" Value="2"></asp:ListItem>
                                           
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="drpemployeetype" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                    </td>
                                   <td rowspan="6">
                                      <asp:UpdatePanel ID="pnlpic" runat="server">
                                          <ContentTemplate>
        <asp:Image ID="ImgPrv" Height="100px" Width="100px" runat="server" />
                                              </ContentTemplate>
                                          </asp:UpdatePanel>
        <%--<asp:FileUpload ID="FileUpload2" runat="server" Width="150px"/>
                                       <asp:Button ID="btnUpload" runat="server" Text="Upload"/>--%>
                    <asp:FileUpload ID="FileUpload2" runat="server" Width="150px" onchange="ShowImagePreview(this);" />

                                   </td>
                                </tr>
                               <tr> <td colspan="11" style="height:10px"> </td></tr>                      
                               <tr>  
                                   <td>                
                                        <label style="line-height:25px">Nature Of Appointment.</label>
                                   </td>
                                   <td style="width:10px"> </td>
                                   <td> 
                                     <asp:DropDownList ID="drpnatureofappoint" runat="server" Width="180px" Height="28px">
                                         <asp:ListItem Text="--Select Employee Nature Of Appointment--" Value="0"></asp:ListItem>
                                         <asp:ListItem Text="Regular" Value="1"></asp:ListItem>
                                         <asp:ListItem Text="Ad-hoc" Value="2"></asp:ListItem>
                                           
                                        </asp:DropDownList>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="drpnatureofappoint" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                    </td> 
                                   <td style="width:10px"> </td>
                                   <td><label style="line-height:25px">Unit(College/Dept.) </label> </td> 
                                   <td style="width:10px"> </td>
                                   <td> 
                                       <asp:DropDownList ID="drpunitcollege" runat="server" Height="28px" Width="180px" BorderColor="Black">
                                           
                                          </asp:DropDownList>  

                                   </td> 
                                   <td style="width:10px"> </td> 
                                   <td><label style="line-height:25px"> Dept.(If any) </label> </td> 
                                   <td style="width:10px"> </td>
                                   <td> 
                                       <asp:TextBox ID="txtdept" runat="server" Width="180px" BorderColor="Black"></asp:TextBox>
                                   </td>

                               </tr>
                               <tr> <td colspan="11" style="height:10px"> </td> </tr>
                               <tr> 
                                    <td>                
                                        <label style="line-height:25px">Sub.Dept(If any)</label>
                                    </td> 
                                   <td style="width:10px"> </td>
                                   <td> 
                                         <asp:TextBox ID="txtsubdept" runat="server" Width="180px" BorderColor="Black"></asp:TextBox>
                                    </td> 
                                   <td style="width:10px"> </td>
                                   <td><label style="line-height:25px"> Date Of Joining  </label> </td> 
                                   <td style="width:10px"> </td>
                                   <td> 
                     <asp:TextBox ID="txtdateofjoining" runat="server" BorderColor="Black" Width="180px" onkeydown="return false;" OnTextChanged="txtdateofjoining_TextChanged" autocomplete="off" oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                     <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtdateofjoining" Format="dd MMM yyyy"></asp:CalendarExtender>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdateofjoining" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>    
                                   </td> 
                                   <td style="width:10px"> </td> 
                                   <td><label style="line-height:25px"> Designation: </label> </td> 
                                   <td style="width:10px"> </td>
                                   <td> 
                                        <asp:TextBox ID="txtdesignation" runat="server"  Width="180px" BorderColor="Black"></asp:TextBox>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtdesignation" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                   </td>
                               </tr>     
                                           </tr>       
                    </table>
                                 
<br />
 <fieldset class="boxBody" style="text-align: left;">
 <asp:Label ID="Label3" runat="server" Text="PERSONAL DETAIL:" Font-Size="15pt"  ForeColor="Black" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

           </fieldset>      
                                <table cellpadding="0px" cellspacing="0px">       
                               <tr> 
                                    <td><label style="line-height:25px">  Date Of Birth: </label> </td> 
                                    <td style="width:10px"> </td> 
                                    <td>
                                     <asp:TextBox ID="txtdateofbirth" runat="server" BorderColor="Black" Width="200px" onkeydown="return false;" OnTextChanged="txtdateofbirth_TextChanged" AutoPostBack="true" autocomplete="off" oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                     <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdateofbirth" Format="dd MMM yyyy"></asp:CalendarExtender>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtdateofbirth" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>    
                                    </td> 
                                    <td style="width:10px"> </td> 
                                    <td>Father Name:</td> 
                                    <td style="width:10px"> </td>
                                    <td> 
                                        <asp:TextBox ID="txtfathername" runat="server" BorderColor="Black"  Width="200px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtfathername" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                    </td> 
                                    <td class="auto-style6"> </td> 
                                    <td>Mother Name: </td> 
                                    <td style="width:10px"> </td>
                                    <td> 
                                            <asp:TextBox ID="txtmothername" runat="server" BorderColor="Black"  Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                               <tr> <td colspan="11" style="height:10px"> </td></tr>                      
                               <tr>  
                                   <td>                
                                        Gender:</td>
                                   <td style="width:10px"> </td>
                                   <td> 
                                       <asp:DropDownList ID="drpgender" runat="server" Width="200px" Height="28px" BorderColor="Black">
                                         <asp:ListItem Text="--Select Employee Gender--" Value="0"></asp:ListItem>
                                         <asp:ListItem Text="MALE" Value="1"></asp:ListItem>
                                         <asp:ListItem Text="FEMALE" Value="2"></asp:ListItem>

                                           </asp:DropDownList>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="drpgender" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                    </td> 
                                   <td style="width:10px"> </td>
                                   <td><label style="line-height:25px"> Marital Status: </label> </td> 
                                   <td style="width:10px"> </td>
                                   <td> 
                                           <asp:DropDownList ID="drpmaritalstatus" runat="server" Width="200px" Height="28px" BorderColor="Black" AutoPostBack="true" OnSelectedIndexChanged="drpmaritalstatus_SelectedIndexChanged">
                                         <asp:ListItem Text="--Select Employee Marital Status--" Value="0"></asp:ListItem>
                                         <asp:ListItem Text="Single" Value="1"></asp:ListItem>
                                         <asp:ListItem Text="Married" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Divorce" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Widow" Value="4"></asp:ListItem>
                                           </asp:DropDownList>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="drpmaritalstatus" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                   </td> 
                                   <td class="auto-style6"> </td> 
                                   <td><label style="line-height:25px"> Spouse Name: </label> </td> 
                                   <td style="width:10px"> </td>
                                   <td> 
                                       <asp:TextBox ID="txtspousename" runat="server"  Width="200px" BorderColor="Black"></asp:TextBox>
                                   </td>

                               </tr>
                               <tr> <td colspan="11" style="height:10px"> </td> </tr>
                               <tr> 
                                    <td>                
                                        Spouse Profession:
                                    </td> 
                                   <td style="width:10px"> </td>
                                   <td> 
                                           <asp:DropDownList ID="drpspouseprofession" runat="server" Width="200px"  Height="28px" BorderColor="Black">
                                         <asp:ListItem Text="--Select Employee Spouse Profession--" Value="0"></asp:ListItem>
                                         <asp:ListItem Text="House Wife" Value="1"></asp:ListItem>
                                         <asp:ListItem Text="Govt.Job" Value="2"></asp:ListItem>
                                         <asp:ListItem Text="Pvt.Job" Value="3"></asp:ListItem>
                                         <asp:ListItem Text="Agricultar" Value="4"></asp:ListItem>
                                         <asp:ListItem Text="Other" Value="5"></asp:ListItem>

                                           </asp:DropDownList>
                                    </td> 
                                   <td style="width:10px"> </td>
                                   <td><label style="line-height:25px"> Mobile No: </label> </td> 
                                   <td style="width:10px"> </td>
                                   <td> 
                                          <asp:TextBox ID="txtmobileno" runat="server"  Width="200px" BorderColor="Black" placeholder="Mobile"  onkeypress="return numeric(event)" MaxLength="10" TargetControlID="txtmobileno" FilterType="Numbers, Custom"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtmobileno" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps" ValidationExpression='(^([0-9]*|\d*\d{1}?\d*)$)' Display='Dynamic'></asp:RequiredFieldValidator>
                                   </td> 
                                   <td class="auto-style6"> </td> 
                                   <td><label style="line-height:25px"> Alter. Mobile No.: </label> </td> 
                                   <td style="width:10px"> </td>
                                   <td> 
                                        <asp:TextBox ID="txtaltmobileno" onkeypress="return numeric(event)" runat="server"  Width="200px" BorderColor="Black" placeholder="Alter. Mobile" MaxLength="10"></asp:TextBox>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtaltMobileno" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                   </td>
                               <tr> <td colspan="11" style="height:10px"> </td> </tr>
                                   
                      </tr>
                                     <tr> 
                                    <td>                
                                        E-mail ID:
                                    </td> 
                                   <td style="width:10px"> </td>
                                   <td> 
                                         <asp:TextBox ID="txtemailid" runat="server"  Width="200px" BorderColor="Black"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtemailid" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                    </td> 
                                   <td style="width:10px"> </td>
                                   <td><label style="line-height:25px"> Religion:: </label> </td> 
                                   <td style="width:10px"> </td>
                                   <td> 
                                          <asp:DropDownList ID="drpreligion" runat="server" Height="28px" Width="200px" BorderColor="Black">
                                           
                                          </asp:DropDownList>
                                   </td> 
                                   <td class="auto-style6"> </td> 
                                   <td><label style="line-height:25px"> Category: </label> </td> 
                                   <td style="width:10px"> </td>
                                   <td> 
                                        <asp:DropDownList ID="drpcategory" runat="server" Height="28px" Width="200px" BorderColor="Black">
                                            <asp:ListItem Text="--Select Employee Category--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="GENERAL/UNRESERVED" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="OBC (OBC)" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="SC (SC)" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="ST (ST)" Value="4"></asp:ListItem>
                                        </asp:DropDownList>
                                   </td>
                               <tr> <td colspan="11" style="height:10px"> </td> </tr>
                                   
                      </tr>
                                     <tr> 
                                    <td>                
                                        Blood Group:
                                    </td> 
                                   <td style="width:10px"> </td>
                                   <td> 
                                         <asp:TextBox ID="txtbloodgroup" runat="server"  Width="200px" BorderColor="Black"></asp:TextBox>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtbloodgroup" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                    </td> 
                                   <td style="width:10px"> </td>
                                   <td><label style="line-height:25px"> Physically Challenged: </label> </td> 
                                   <td style="width:10px"> </td>
                                   <td> 
                                          <asp:DropDownList ID="drpphysicallychallenged" runat="server" Height="28px" Width="200px" BorderColor="Black">
                                              <asp:ListItem Text="--Select Employee Physically Challenged--" Value="0"></asp:ListItem>
                                              <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                              <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                          </asp:DropDownList>
                                   </td> 
                                   <td class="auto-style6"> </td> 
                                   <td><label style="line-height:25px"> Nationality: </label> </td> 
                                   <td style="width:10px"> </td>
                                   <td> 
                                        <asp:TextBox ID="txtnationality" runat="server"  Width="200px" BorderColor="Black"></asp:TextBox>
                                   </td>
                               <tr> <td colspan="11" style="height:10px"> </td> </tr>
                                   
                      </tr>
                                     <tr> 
                                    <td>                
                                        Domicile State:
                                    </td> 
                                   <td style="width:10px"> </td>
                                   <td> 
                                         <asp:DropDownList ID="drpState" runat="server" Height="28px" Width="200px" BorderColor="Black">
                                             
                                         </asp:DropDownList>
                                    </td> 
                                   <td style="width:10px"> </td>
                                   <td><label style="line-height:25px"> Voter ID No.: </label> </td> 
                                   <td style="width:10px"> </td>
                                   <td> 
                                          <asp:TextBox ID="txtvoterid" runat="server"  Width="200px" BorderColor="Black"></asp:TextBox>
                                   </td> 
                                   <td class="auto-style6"> </td> 
                                   <td><label style="line-height:25px"> Driving License No.: </label> </td> 
                                   <td style="width:10px"> </td>
                                   <td> 
                                        <asp:TextBox ID="txtdrivinglice" runat="server"  Width="200px" BorderColor="Black"></asp:TextBox>
                                   </td>
                               <tr> <td colspan="11" style="height:10px"> </td> </tr>
                                   
                      </tr>
                                     <tr> 
                                    <td>                
                                        Passport No.:
                                    </td> 
                                   <td style="width:10px"> </td>
                                   <td> 
                                         <asp:TextBox ID="txtpassport" runat="server"  Width="200px" BorderColor="Black"></asp:TextBox>
                                    </td> 
                                   <td style="width:10px"> </td>
                                   <td><label style="line-height:25px"> Valid up to: </label> </td> 
                                   <td style="width:10px"> </td>
                                   <td>
                                     <asp:TextBox ID="txtvalidupto" runat="server" BorderColor="Black" Width="200px" onkeydown="return false;" autocomplete="off" oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                     <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtvalidupto" Format="dd MMM yyyy"></asp:CalendarExtender>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="txtvalidupto" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>    
                                    </td>  
                                   <td class="auto-style6"> </td> 
                                   <td><label style="line-height:25px"> PAN: </label> </td> 
                                   <td style="width:10px"> </td>
                                   <td> 
                                        <asp:TextBox ID="txtpanno" runat="server"  Width="200px" BorderColor="Black"></asp:TextBox>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtpanno" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                   </td>
                               <tr> <td colspan="11" style="height:10px"> </td> </tr>
                                   
                      </tr>
                                     <tr> 
                                    <td>                
                                        Aadhar No.:
                        
                                    </td> 
                                   <td style="width:10px"> </td>
                                   <td> 
                                         <asp:TextBox ID="txtaadharno" runat="server"  Width="200px" BorderColor="Black" onkeypress="return numeric(event)" MaxLength="12"></asp:TextBox>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtaadharno" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                    </td> 
                                   
                               <tr> <td colspan="11" style="height:10px"> </td> </tr>
                                   
                      </tr>
                    </table>
                         
                        <fieldset class="boxBody" style="text-align:left;">
 <asp:Label ID="Label4" runat="server" Text=" EMERGENCY CONTACT PERSON DETAIL" Font-Size="15pt"  ForeColor="Black" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                            :</fieldset>

                                <table cellpadding="0px" cellspacing="0px">       
                               <tr> 
                                    <td><label style="line-height:25px">  Emergency Contact Person Name.: </label> </td> 
                                    <td style="width:10px"> </td> 
                                    <td>
                                      <asp:TextBox ID="txtemergencyname" runat="server" Width="200px" BorderColor="Black"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtemergencyname" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                    </td> 
                                    <td style="width:10px"> </td> 
                                    <td><label style="line-height:25px"> Mobile No.: </label> </td> 
                                    <td style="width:10px"> </td>
                                    <td> 
                                        <asp:TextBox ID="txtemergenmobile" runat="server"  Width="200px" BorderColor="Black" placeholder="Emergency Mobile" onkeypress="return numeric(event)" MaxLength="10"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtemergenmobile" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                    </td> 
                                    <td style="width:10px"> </td> 
                                    <td><label style="line-height:25px"> E-mail ID:</label> </td> 
                                    <td style="width:10px"> </td>
                                    <td> 
                                            <asp:TextBox ID="txtemergencymail" runat="server"  Width="200px" BorderColor="Black"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtemergencymail" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                               <tr> <td colspan="11" class="auto-style16"> </td></tr>                      
                               <tr>  
                                   <td>                
                                        <label style="line-height:25px">  Relationship:</label>
                                   </td>
                                   <td style="width:10px"> </td>
                                   <td> 
                                       <asp:DropDownList ID="drprelationship" runat="server" Height="28px" Width="200px" BorderColor="Black">
                                           <asp:ListItem Text="--Select Employee Relaion--" Value="0"></asp:ListItem>
                                           <asp:ListItem Text="Mother" Value="1"></asp:ListItem>
                                           <asp:ListItem Text="Father" Value="2"></asp:ListItem>
                                           <asp:ListItem Text="Sister" Value="3"></asp:ListItem>
                                           <asp:ListItem Text="Brother" Value="4"></asp:ListItem>
                                           <asp:ListItem Text="Wife" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="Other" Value="6"></asp:ListItem>
                                       </asp:DropDownList>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="drprelationship" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                    </td> 
                                   </tr>
                                    </table>
                  <br />
                       <fieldset class="boxBody" style="text-align: left;"">
 <asp:Label ID="Label5" runat="server" Text=" EMPLOYEE ADDRESS:/PRESENT ADDRESS" Font-Size="15pt"  ForeColor="Black" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
 </fieldset>
                                <table cellpadding="0px" cellspacing="0px">       
                               <tr> 
                                    <td>
                                        <label style="line-height: 25px">  Present Address.:                      <td style="width:10px"> </td> 
                                    <td>
                                        <asp:TextBox ID="txtpreaddress" runat="server" Width="220px"  TextMode="MultiLine" BorderColor="Black"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtpreaddress" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                     
                                    </td> 
                                    <td style="width:10px"> </td> 
                                    <td><label style="line-height:25px"> City.: </label> </td> 
                                    <td style="width:10px"> </td>
                                    <td> 
                                        <asp:TextBox ID="txtprecity" runat="server" Width="220px" BorderColor="Black"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtprecity" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                    </td> 
                                    <td style="width:10px"> </td> 
                                    <td><label style="line-height:25px"> District:</label> </td> 
                                    <td style="width:10px"> </td>
                                    <td> 
                                            <asp:TextBox ID="txtpredisatrict" runat="server"  Width="220px" BorderColor="Black"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtpredisatrict" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                               <tr> <td colspan="11" style="height:10px"> </td></tr>                      
                               <tr>  
                                   <td>                
                                        <label style="line-height:25px">  Post Code:</label>
                                   </td>
                                   <td style="width:10px"> </td>
                                   <td> 
                                       <asp:TextBox ID="txtprepostcode" runat="server" onkeypress="return numeric(event)"  Width="220px" BorderColor="Black" MaxLength="6"></asp:TextBox>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txtprepostcode" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                    </td> 
                                   </tr>
                                    </table>
                  <br />
                      <fieldset class="boxBody">
                         <table>   
                             <tr>

                                 <td style="text-align: right;">
                                     <asp:Label ID="Label6" runat="server" Text=" PERMANENT ADDRESS" Font-Size="15pt" ForeColor="Black" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                                 </td>

                                
                                 <td>
                                     <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" Text="Same As Above" Font-Size="15pt" ForeColor="Black" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                 </td>
                             </tr> 
                             </table>  
                               
 </fieldset>
                         <table cellpadding="0px" cellspacing="0px">       
                               <tr> 
                                    <td class="auto-style14"><label style="line-height:24px">  Permanent Address.:  </label> </td>     
                                    <td style="width:10px"> </td> 
                                    <td class="auto-style13">
                                      <asp:TextBox ID="txtpermentadd" runat="server" Width="220px"  TextMode="MultiLine" BorderColor="Black"></asp:TextBox>
                                    </td> 
                                    <td style="width:10px"> </td> 
                                    <td><label style="line-height:25px"> City.: </label> </td> 
                                    <td style="width:10px"> </td>
                                    <td> 
                                        <asp:TextBox ID="txtpercity" runat="server"  Width="210px" BorderColor="Black"></asp:TextBox>
                                    </td> 
                                    <td style="width:10px"> </td> 
                                    <td><label style="line-height:25px"> District:</label> </td> 
                                    <td style="width:10px"> </td>
                                    <td> 
                                            <asp:TextBox ID="txtperdistrict" runat="server"  Width="210px" BorderColor="Black"></asp:TextBox>
                                    </td>
                                </tr>
                               <tr> <td colspan="11" class="auto-style16"> </td></tr>                      
                               <tr>  
                                   <td class="auto-style14">                
                                        <label style="line-height:25px">  Post Code:</label>
                                   </td>
                                   <td style="width:10px"> </td>
                                   <td class="auto-style13"> 
                                       <asp:TextBox ID="txtperpostcode" runat="server" onkeypress="return numeric(event)"  Width="220px"  BorderColor="Black" MaxLength="6"></asp:TextBox>
                                    </td> 
                                   </tr>
                                     <tr>
                    <td class="auto-style14"></td>
                    <td style="width: 10px"></td>
                                         <td class="auto-style15">
                        <asp:TextBox ID="txtID" runat="server" Width="200px"></asp:TextBox>
                                             </td>
                </tr>
                                
                                  
                  
                      
                    
                        
                        </table>
                         <table>
                             <tr>
                                 <td style="width:800px"></td>
                                 <td style="text-align:right">
                                     <asp:Button ID="Button1" runat="server" BackColor="#ff9933" ForeColor="White" Height="30px" OnClick="Button1_Click" Text="Save" Visible="false" Width="70px" />
                                 </td>
                                 <td style="text-align:right">
                                     <asp:Button ID="Button2" runat="server" BackColor="#ff9933" ForeColor="White" Height="30px" OnClick="Button2_Click" Text="Update" Visible="false" Width="70px" />
                                 </td>

                             </tr>
                             <tr>
                                <td style="width:800px;height:200px"></td>
                                 <td style="text-align:right">
                                   
                                 </td>
                                 <td style="text-align:right">
                                     
                                 </td>
                             </tr>
                         </table>
                         </fieldset>
                        </div>
    
                        
     
      
    
                        
     
       </table>
    
                        
     
      
    
                        
     
    </asp:Panel>
    
     <asp:Button ID="btnDummy" runat="server" Style="display: none;" />
                            
<asp:ModalPopupExtender ID="GridViewdata" runat="server" TargetControlID="btnDummy"
            PopupControlID="pnlGridViewdata" BackgroundCssClass="modalBackground" />
    <div style="height:220px">

    </div>
</asp:Content>

