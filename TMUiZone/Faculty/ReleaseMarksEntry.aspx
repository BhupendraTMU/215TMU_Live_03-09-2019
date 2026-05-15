<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="ReleaseMarksEntry.aspx.cs" Inherits="Faculty_ReleaseMarksEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
          <script>
              function isNumberKey(evt) {

                  var charCode = (evt.which) ? evt.which : evt.keyCode;
                  if (charCode > 31 && (charCode < 48 || charCode > 57))
                      return false;
                  return true;
              }


</script>
    
     <style type="text/css">
     .redBorder {
            border:1px solid red;
        }
        .loader
        {
            position: fixed;
            left: 45%;
            top: 45%;
            width: 100px;
            height: 100px;
            z-index: 9999;
            background: url('../images/loader.gif') 50% 50% no-repeat rgb(249,249,249);
        }
    </style>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">
 <asp:Label ID="Label1" runat="server" 
            Text="Release Marks Entry" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 </fieldset>
  

    <asp:UpdatePanel ID="mrak" runat="server">
        <ContentTemplate>



  <fieldset class="boxBodyInner">       
                 <table cellpadding="0px" cellspacing="0px">                
               <caption>
                   <br />

                   <tr>  
                      <td>Academic Year  </td> 
                      <td style="width:10px"> </td>
                      <td > 
                         <asp:DropDownList ID="drpAcademicYear" Width="150px" Height="20px" runat="server"  OnSelectedIndexChanged="drpAcademicYear_SelectedIndexChanged"  AutoPostBack="true" ></asp:DropDownList>
                      </td> 
                       <td style="width:20px"> </td>
                       <td>
                           <td>Course <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpCourse" InitialValue="" ErrorMessage="**" ForeColor="Red" ></asp:RequiredFieldValidator></td> 
                      <td style="width:10px"></td>
                      <td > 
                         <asp:DropDownList ID="drpCourse" runat="server" AutoPostBack="true" Height="20px" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged" Width="200px"></asp:DropDownList>
                      
                      </td> 
                       <td style="width:20px"></td>
                       <td>
                           Semester/Year
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpSemester" InitialValue="" ErrorMessage="**" ForeColor="Red" ></asp:RequiredFieldValidator> 
                       </td>
                       <td style="width:10px"></td>
                       <td>
                           <asp:DropDownList ID="drpSemester" runat="server" OnSelectedIndexChanged="drpSemester_SelectedIndexChanged" AutoPostBack="true" Height="20px"  Width="120px">
                           </asp:DropDownList>
                       </td>
                      
                       <td style="width:10px"></td>
                       <td>                       
<asp:Button ID="btnShow" runat="server"  CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" OnClick="btnShow_Click"    Height="30px" Width="90px"  Text="SHOW" />                        
        
                         </td>
                       <td style="width:20px"> </td>
                         <td>
 <asp:Button ID="btnSubmit" runat="server"  CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" OnClick="btnSubmit_Click"   Height="30px" Visible="false" Width="90px"/>                        
           
                         </td>
                       <td style="width:10px"> </td>

             <td>
 <asp:Button ID="brnReject" runat="server"  CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1"   Height="30px" Visible="false" Width="90px"  OnClick="brnReject_Click"/>  
                 <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                         </td>
                        <td style="width:10px"> </td>
                       <td>
                       
           

                         </td>


                   </tr>
        </table>
          </fieldset>

<fieldset class="boxBodyInner"> 
              
                       <asp:GridView ID="grdmarktable"  runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" ShowFooter="true" BorderWidth="1px" CellPadding="3" Width="100%" GridLines="Horizontal"  EmptyDataText="There are no data records to display." >
                       <AlternatingRowStyle BackColor="#F7F7F7" />
                              <Columns>
          
                           <asp:TemplateField HeaderText="Sr.No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Group No">
                                            <ItemStyle Width="5%" />
                                            <ItemTemplate>
                                      <asp:Label ID="grdlblDocumentNo" runat="server" Text='<%#Eval("Group Document No_") %>'></asp:Label>
                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                   <asp:TemplateField HeaderText="Method">
                                            <ItemStyle Width="8%" />
                                            <ItemTemplate>
                                      <asp:Label ID="grdlblMethod" runat="server" Text='<%#Eval("ExamMethod") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Group">
                                            <ItemStyle Width="8%" />
                                            <ItemTemplate>
                                      <asp:Label ID="grdlblgroup" runat="server" Text='<%#Eval("ExamGroup") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                       <asp:TemplateField HeaderText="Course Code">
                                            <ItemStyle Width="8%" />
                                            <ItemTemplate>
                                      <asp:Label ID="grdlblCourseCode" runat="server" Text='<%#Eval("Course Code") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Subject Name">
                                            <ItemStyle Width="15%" />
                                            <ItemTemplate>
                                                <asp:Label ID="grdlblSubjectName" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                                              <asp:HiddenField ID="hfSubject" runat="server" Value='<%#Eval("Subject Code") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Subject Type">
                                            <ItemStyle Width="10%" />
                                            <ItemTemplate>
                                      <asp:Label ID="grdlblSubjectType" runat="server" Text='<%#Eval("Subject Type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Section">
                                            <ItemStyle Width="8%" />
                                            <ItemTemplate>
                              <asp:Label ID="grdlblSection" runat="server" Text='<%#Eval("Section") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Semester" ItemStyle-Width="8%" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblSemester" runat="server" Text='<%#Eval("Semester") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Status" ItemStyle-Width="8%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                       <asp:Label ID="lblStatus" runat="server"   Text='<%#Eval("Statust") %>'></asp:Label>
                                           
                                        <asp:HiddenField ID="hfStatus" runat="server" Value='<%#Eval("Status") %>' />
                                                 </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MarkEntry" >
                                            <ItemStyle Width="15%" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btntblmarksshow" Width="50%" CssClass="btn-sm btn-primary btn-block" OnClick="btntblmarksshow_Click" runat="server">Marks Entry</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                  </Columns>                      
                                       <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                       <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                       <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                       <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF"  CssClass="cssGridheaderfont"   />
                           
                       <SelectedRowStyle BackColor="#88dde3" Font-Bold="True"  ForeColor="#F7F7F7" />
                       <SortedAscendingCellStyle BackColor="#F4F4FD" />
                       <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                       <SortedDescendingCellStyle BackColor="#D8D8F0" />
                       <SortedDescendingHeaderStyle BackColor="#3E3277" />
                            </asp:GridView>
    
    
    
                

<%--    <after submit>--%>

    <asp:GridView ID="grdViewmarksEntrySubmit"  DataKeyNames="Student No_" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" ShowFooter="true" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" GridLines="Horizontal"  EmptyDataText="There are no data records to display."  Visible="false">
                       <AlternatingRowStyle BackColor="#F7F7F7" />
                                 <Columns>
                                    <asp:TemplateField HeaderText="Sr. No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle Width="3%" />
                                    </asp:TemplateField>           
                                  
                                   <asp:BoundField DataField="Enrollement No" HeaderText="EnrollementNo"  ItemStyle-CssClass="visible-lg" /> 

                                    <asp:BoundField DataField="Student Name" HeaderText="Student Name"  ItemStyle-CssClass="visible-lg" />                                     
                                      <asp:BoundField DataField="Examgroup" HeaderText="Group"  ItemStyle-CssClass="visible-lg" />                                     
                                    
                                    <asp:BoundField DataField="ExamMethod" HeaderText="Method"  ItemStyle-CssClass="visible-lg" /> 
                                    
                                    <asp:TemplateField HeaderText="Maximunm Marks">
                                                <ItemTemplate>
                                                <asp:Label ID="grdlMaximummark" runat="server" Text='<%#Eval("maxmark" ,"{0:N0}") %>'></asp:Label>

                                                </ItemTemplate>
                                          </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="visible-lg">
                                                    <ItemTemplate>
 <asp:DropDownList ID="grdattedGet" CssClass="form-control" Enabled="false" SelectedValue='<%#Eval("Attendance Type") %>'    AutoPostBack="true" runat="server" Width="60%">
                                                            <asp:ListItem Value="1" Text="Present" ></asp:ListItem>
                                                            <asp:ListItem Value="0" Text="Absent"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="Detained"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="UFM"></asp:ListItem>

                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <HeaderTemplate>Attendance</HeaderTemplate>
                                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Marks">
                                                <ItemTemplate>
                                                <asp:TextBox ID="grdtxtMarks" runat="server" Width="50%"  CssClass="form-control"  Text='<%# Bind("Internalmark","{0:N0}") %>' onkeypress="return isNumberKey(event)" MaxLength="3"></asp:TextBox>
                                                
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                           <asp:TemplateField ItemStyle-Width="5%"  HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-center">
                        <HeaderTemplate>
                        <%--    <asp:CheckBox ID="SChlAll"  Checked="true" AutoPostBack="true" runat="server" />--%>
                           
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="SChkD" runat="server" Checked='<%# Bind("Chk") %>' AutoPostBack="true" />

                        </ItemTemplate>
                   </asp:TemplateField>

                                  </Columns>                       
                                       <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                       <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                       <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                       <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont"   />
                       <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
                       <SortedAscendingCellStyle BackColor="#F4F4FD" />
                       <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                       <SortedDescendingCellStyle BackColor="#D8D8F0" />
                       <SortedDescendingHeaderStyle BackColor="#3E3277" />
                            </asp:GridView>
    <br />
    <br />
                      </fieldset>
           
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>

