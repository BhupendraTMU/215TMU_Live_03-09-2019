<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="SecondaryLoad.aspx.cs" Inherits="Faculty_SecondaryLoad" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .parent {
            text-align: center;
            display: block;
            border: 1px solid outset;
        }

        .child {
            display: inline-block;
            width: 200px;
        }
    </style>
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

            var f = new Date($('[id$=txtDateTo]').val());
            if (sender._selectedDate > f) {
                alertify.error("You cannot select Greater than To date!");
                sender._textbox.set_Value('');
            }
        }



        function checkDate1(sender, args) {

            if ($('[id$=txtDateFrom]').val() == '') {
                alertify.error('First select the from date!');
                sender._textbox.set_Value('');
                return false;
            }
            else {
                var f = new Date($('[id$=txtDateFrom]').val());

                if (sender._selectedDate < f) {
                    alertify.error("You cannot select less than from date!");
                    sender._textbox.set_Value('');
                }
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="elist" runat="server">
        <ContentTemplate>
            <fieldset class="boxBody">
                <table>
                    <tr>

                        <td>
                            <asp:Label ID="Label1" runat="server" Visible="true" Text="Secondary Load" Font-Size="15pt" ForeColor="#093A62"
                                Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label></td>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                        <td>Academic Year  </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:DropDownList ID="drpAcademicYear" Width="150px" Height="20px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpAcademicYear_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        <td style="width: 20px"></td>
                        <td>
                            
                        </td>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                        </td>
                        <td>

                           
                        </td>

                    </tr>
                </table>



            </fieldset>
            <fieldset class="boxBodyHeader">
            </fieldset>


            <asp:Panel ID="pnlList" runat="server" BorderWidth="2px" BorderColor="#ACE9FB" ScrollBars="Vertical">

                <div class="parent" style="background-color: #ff6a00">

                    <table style="fit-position: left; padding-top: 2px;" width="100%">
                        <tr>                           
                            <td style="text-align: left; width: 20%; padding-left: 10px;">
                                <br />
                                <asp:DropDownList ID="drpCourse" runat="server" CssClass="form-control input-sm" AutoPostBack="True" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged" Width="120px" Height="25px"></asp:DropDownList>
                            </td>
                             <td style="text-align: left; width: 5%; padding-left: 10px;">
                                <br />
                            </td>                            
                            <td style="text-align: left; width: 20%; padding-left: 10px;">
                                <br />
                                <asp:DropDownList runat="server" ID="drpSemester" Width="120px" ToolTip="Semester" CssClass="form-control input-sm" Height="25px"></asp:DropDownList></td>
                             <td  style="text-align: left; width: 5%; padding-left: 10px;">
                                <br />
                                </td> 
                            <td style="text-align: left; width: 20%; padding-left: 10px;"><br />
                                <asp:RadioButtonList ID="rblUnivCollege" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rblUnivCollege_SelectedIndexChanged" RepeatDirection="Horizontal" TextAlign="Left">
                                     <asp:ListItem Value="0" Selected="True" >College</asp:ListItem>
                                    <asp:ListItem Value="1">University</asp:ListItem>
                                   
                                </asp:RadioButtonList>
                            </td>
                             <td style="text-align: left; width: 5%; padding-left: 10px;">
                                <br />
                            </td>                          
                           <%-- <td style="text-align: left; width: 20%; padding-left: 10px;">
                                <br />
  <asp:RadioButtonList ID="rblOddEvenYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rblOddEvenYear_SelectedIndexChanged" RepeatDirection="Horizontal" TextAlign="Left">
                                <asp:ListItem Value="0" Text="Yearly  "> </asp:ListItem>
                                <asp:ListItem Value="1" Text="Odd  "></asp:ListItem>
                                <asp:ListItem Value="2" Text="Even  "></asp:ListItem>
                            </asp:RadioButtonList>
                                </td>--%>
                            <td style="text-align: left; width: 20%; padding-left: 10px;"><br />                                
                                <asp:DropDownList runat="server" ID="ddlFaculty" Width="120px" AutoPostBack="true" CssClass="form-control input-sm" Height="25px" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                             <td style="text-align: left; width: 5%; padding-left: 1px;"> <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlFaculty" Display="Dynamic" ErrorMessage="**" InitialValue="" ForeColor="White" ValidationGroup="SL1"></asp:RequiredFieldValidator>
                            </td>                           
                            </tr>
                        <tr>  
                            <td style="text-align: left; width: 20%; padding-left: 10px;"><br />                                
                                <asp:DropDownList ID="ddlCollege" runat="server" Width="120px" AutoPostBack="true" CssClass="form-control input-sm" Height="25px"></asp:DropDownList>
                            </td>                           
                              <td style="text-align: left; width: 5%; padding-left: 10px;"> 
                                  <br />                                
                              </td>
                           <td style="text-align: left; width: 20%; padding-left: 10px;"><br />                                
                                <asp:DropDownList runat="server" ID="ddlload" Width="120px" AutoPostBack="true" CssClass="form-control input-sm" Height="25px"></asp:DropDownList>
                            </td>
                              <td style="text-align: left; width: 5%; padding-left: 1px;" align="left"> <br />                               
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlload" Display="Dynamic" ErrorMessage="**" InitialValue="-- Description --" ForeColor="White" ValidationGroup="SL1"></asp:RequiredFieldValidator>
                              </td>
                            <td style="text-align: left; width: 20%; padding-left: 10px;"><br />                               
                                <asp:TextBox ID="txtRemarks" CssClass="form-control" placeholder="Remarks" Width="150px" Height="25px" MaxLength="250" runat="server"></asp:TextBox>
                            </td>
                             <td style="text-align: left; width: 5%; padding-left: 10px;"> 
                                 <br />
                              </td> 
                            <td style="text-align: left; width: 20%; padding-left: 2px;" colspan="2"><br />                                                               
                                <asp:Button ID="Btnadd" type="button" Text="Add" class="btn btn-info btn" OnClick="Btnadd_Click" runat="server" ValidationGroup="SL1"></asp:Button>                               
                                <asp:Button ID="btnsearch" type="button" Text="search" class="btn btn-info btn" OnClick="btnsearch_Click" runat="server"></asp:Button>
                                <asp:HiddenField ID="hfEntryNo" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <br />
                </div>

                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" DataKeyNames="Entry No" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" BackColor="White" 
                        EmptyDataText="There are no data records to display." BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                        Width="100%" GridLines="Horizontal" AllowPaging="true"
                        PageSize="55">
                        <AlternatingRowStyle BackColor="#F7F7F7" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sr. No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle Width="6%" />
                            </asp:TemplateField>

                            <asp:BoundField ItemStyle-Width="150px" DataField="Employee Name" HeaderText="Faculty">
                                <ItemStyle Width="150px"></ItemStyle>
                            </asp:BoundField>                           
                            <asp:BoundField ItemStyle-Width="150px" DataField="Course Name" HeaderText="Course Name">
                                <ItemStyle Width="150px"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Semester/Year">
                                <ItemTemplate>
                                    <asp:Label ID="lblSemYear" runat="server" Text='<%#Eval("Semest") %>' />                                                                                 
                                </ItemTemplate>
                            </asp:TemplateField>
                           <%-- <asp:BoundField ItemStyle-Width="150px" DataField="Semest" HeaderText="Semester/Year">
                                <ItemStyle Width="150px"></ItemStyle>
                            </asp:BoundField>  --%>  
                            <asp:TemplateField HeaderText="College">
                                <ItemTemplate>
                                    <asp:Label ID="lblCollegeCode" runat="server" Text='<%#Eval("College Code") %>' />                                                                                 
                                </ItemTemplate>
                            </asp:TemplateField>                        
                            <%--<asp:BoundField ItemStyle-Width="150px" DataField="College Code" HeaderText="College Code">
                                <ItemStyle Width="150px"></ItemStyle>
                            </asp:BoundField>   --%>                         
                            <asp:BoundField ItemStyle-Width="150px" DataField="Secondary Load Description" HeaderText="Description">
                                <ItemStyle Width="150px"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Remarks") %>' />                                                                                 
                                </ItemTemplate>
                            </asp:TemplateField>  
                             <%--<asp:BoundField ItemStyle-Width="150px" DataField="Remarks" HeaderText="Remarks">
                                <ItemStyle Width="150px"></ItemStyle>
                            </asp:BoundField>--%>
                             <asp:TemplateField HeaderText="Inactive">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkInactive" runat="server" Text="" OnCheckedChanged="chkInactive_CheckedChanged" AutoPostBack="true"  />                                             
                                     <asp:HiddenField ID="hfCourseCode" runat="server" Value='<%#Eval("Course Code") %>' />                                             
                                    <asp:HiddenField ID="hfCollUniv" runat="server" Value='<%#Eval("University_College") %>'  /> 
                                    <asp:HiddenField ID="hfFacultyCode" runat="server" Value='<%#Eval("Employee Code") %>'  /> 
                                    <asp:HiddenField ID="hfDescriptionCode" runat="server" Value='<%#Eval("Secondary Load Code") %>' /> 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click"/>
     <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClientClick="return confirm('Are you sure? want to delete this Event.');"  OnClick="btnDelete_Click"
                                        />                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                        <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <SortedAscendingCellStyle BackColor="#F4F4FD" />
                        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                        <SortedDescendingCellStyle BackColor="#D8D8F0" />
                        <SortedDescendingHeaderStyle BackColor="#3E3277" />
                    </asp:GridView>
                    <br />
                </div>
            </asp:Panel>



        </ContentTemplate>
        <Triggers>
            <%-- <asp:PostBackTrigger ControlID="btnExportToexcel" />
            <asp:PostBackTrigger ControlID="BtnExportpdf" />--%>
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

