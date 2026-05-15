<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="AppraisalNonTechList.aspx.cs" Inherits="Faculty_AppraisalNonTechList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script type="text/javascript">



            function PrintDiv() {

                var divToPrint = document.getElementById('printarea');

                var popupWin = window.open('', '_blank', 'width=300,height=400,location=no,left=200px, margin:0mm');
                popupWin.document.open();
                popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
                popupWin.document.close();
            }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div style="width: 100%; margin-bottom: 10px; border: 2px solid">
        <asp:ScriptManager ID="ty" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="fe" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                      <div style="text-align: right; width: 98%;margin-bottom:40px;margin-top:10px;margin-left:1%;">
                        <asp:Button ID ="btnback" runat="server" Text="Back"  Visible="false"  OnClick="btnback_Click"  />
                                
                <div class="text-center">
                    <asp:GridView ID="GrdExamList" runat="server"  AlternatingRowStyle-CssClass="danger"  
                        AllowPaging="true" PageSize="15" OnPageIndexChanging="GrdExamList_PageIndexChanging" AllowSorting="true" OnSorting="GrdExamList_Sorting" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" Visible="true">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex +1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Faculty Code" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" SortExpression="HOD" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="Fid"  runat="server" Text='<%# Bind("[HOD]") %>'></asp:Label>
                                    <asp:HiddenField ID="HfStudentNo" Value='<%# Eval("[HOD]") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Name" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" SortExpression="Full Name" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblName"  runat="server" Text='<%# Bind("[Full Name]") %>'></asp:Label>
                                    <asp:HiddenField ID="hfFName" runat="server" Value='<%# Bind("[Full Name]") %>' />
                                  
                                </ItemTemplate>
                            </asp:TemplateField>


                             <asp:TemplateField HeaderText="Department" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" SortExpression="Global Dimension 1 Code" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblDepartment"  runat="server" Text='<%# Bind("[Global Dimension 1 Code]") %>'></asp:Label>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Status" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" SortExpression="ST" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus"  runat="server" Text='<%# Bind("[ST]") %>'></asp:Label>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="View" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <ItemTemplate>

                                    <asp:LinkButton ID="lblview"  runat="server" Text="View" OnClick="lblview_Click"/>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <EmptyDataTemplate>No Record To Display</EmptyDataTemplate>
                    </asp:GridView>
                </div>
           

                            </div>


                  <div id="DivForm" runat="server" visible="false">
                <asp:HiddenField ID="FF" runat="server" />

                <div id="printarea">
                    <div style="width: 100%; margin-bottom: 10px; margin-left: 1%; margin-right: 1%; margin-top: 5px;">
                        <table style="width: 98%;">
                            <tr><td colspan="4" style="text-align:left;width:90%;height:10px;"><asp:Label ID="lblEmpName" Font-Bold="true" runat="server"></asp:Label></td></tr>
                            <tr>
                                <td style="width: 1%"></td>
                                <td style="width: 12%; text-align: left">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/rightlogo.png" Width="55%" />
                                </td>
                                <td style="width: 65%; text-align: center">
                                    <strong>
                                        <asp:Label ID="lblCName" Font-Size="Large" Text="Teerthanker Mahaveer University, Moradabad" runat="server"></asp:Label></strong>
                                    <br />
                                    <strong>
                                        <asp:Label ID="lblAC" runat="server" Text="(Established under Govt. of U. P. Act No. 30, 2008)"></asp:Label></strong>

                                    <br />
                                    <strong>
                                        <asp:Label ID="LblType" runat="server" Text="Delhi Road, Moradabad (U.P)"></asp:Label>
                                    </strong>
                                    <br />

                                </td>
                                <td style="width: 10%; text-align: center"></td>
                            </tr>
                            <tr>
                                <td colspan="4" style="width: 90%; margin-right: 10%; background-color: lightgray; text-align: center; font-size: large;">Non-Teaching Staff Performance Assessment</td>

                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center; width: 90%; height: 10px;">Assessment of each employee is to be done by the Director / Principal / Head, as the case is, in consultation with the officer who supervises the day-to-day work of the concerned employee (reporting Officer), on a scale of 1-10 (poor to excellent); 1 being poor and 10 as excellent.</td>
                            </tr>
                            <tr>
                                <td colspan="3" style="width: 75%; height: 10px;">Name of College/Department/Centre/ Division/ Office/ Section:
                                    <asp:Label ID="lblDepart" runat="server"></asp:Label></td>

                                <td style="width: 15%; height: 10px;">
                                    <p style="font-family: Arial; font-size: 12px; color: black"><b>Academic Session:
                                        <asp:Label ID="lblAcad" runat="server"></asp:Label>
                                    </b></p>
                                </td>

                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center; width: 90%; height: 10px;"></td>
                            </tr>

                        </table>


                    </div>
                    <div style="width: 100%; margin-bottom: 10px; margin-left: 1%; margin-right: 1%;">
                        <table style="width: 96%; margin-left: 2%; margin-right: 2%; border: 1px solid; border-color: black;">
                            <tr>

                                <td style="width: 10%; border: 1px solid" colspan="2">Employee Code(New)</td>
                                <td style="width: 10%; border: 1px solid">Employee's Name</td>
                                <td style="width: 10%; border: 1px solid">Date of joining</td>
                                <td style="width: 10%; border: 1px solid">Degignation</td>
                                <td colspan="6" style="width: 50%; border: 1px solid">

                                    <table>
                                        <tr>
                                            <td colspan="6" style="width: 50%; text-align: center;">Assessment Criteria / parameters</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 8%; border: 1px solid">Job Knowledge / Work Output quality </td>
                                            <td style="width: 8%; border: 1px solid">Inclination to learn / Initiative power to take new assignment(s)</td>
                                            <td style="width: 8%; border: 1px solid">Capacity to work with others / in team</td>
                                            <td style="width: 8%; border: 1px solid">Capacity to carryout job(s) in assignment time with accuracy</td>
                                            <td style="width: 8%; border: 1px solid">General behavior including honesty and integrity (based on past behavior and reported case (s) of indiscipline and dishonesty)</td>
                                            <td style="width: 5%; border: 1px solid">Total Score</td>
                                        </tr>
                                          <tr>
                                            <td style="width: 10%; border: 0.5px solid">10</td>
                                            <td style="width: 10%; border: 0.5px solid">10</td>
                                            <td style="width: 10%; border: 0.5px solid">10</td>
                                            <td style="width: 10%; border: 0.5px solid">10</td>
                                            <td style="width: 10%; border: 0.5px solid">10</td>
                                            <td style="width: 5%; border: 1px solid;">50</td>
                                        </tr>
                                    </table>

                                </td>
                            </tr>
                            <tr>
                                <td colspan="11" style="width: 100%;">
                                    <asp:Repeater ID="grdaddedEmployee" runat="server">
                                        <ItemTemplate >
                                            <table style="width: 100%;">
                                                <tr>
                                           <td style="border: 1px solid; width: 10%;"> <asp:Label ID="Label8" runat="server" Text='<%# Eval("EmpCode") %>'></asp:Label></td>
                                           <td style="width: 10%; border: 1px solid"> <asp:Label ID="lblempname" runat="server" Text='<%# Eval("EmpName") %>'></asp:Label></td>
                                           <td style="width: 10%; border: 1px solid"> <asp:Label ID="lblDateofjoin" runat="server" Text='<%# Eval("DatJob") %>'></asp:Label></td>
                                           <td style="width: 10%; border: 1px solid"> <asp:Label ID="Label1" runat="server"  Text='<%# Eval("Designation") %>'></asp:Label></td>
                                            <td style="width: 10%; border: 1px solid"><asp:Label ID="Label2" runat="server" Text='<%# Eval("txt15") %>'></asp:Label></td>
                                            <td style="width: 10%; border: 1px solid"><asp:Label ID="Label3" runat="server"  Text='<%# Eval("txt16") %>'></asp:Label></td>
                                            <td style="width: 10%; border: 1px solid"><asp:Label ID="Label4" runat="server" Text='<%# Eval("txt17") %>'></asp:Label></td>
                                           <td style="width: 10%; border: 1px solid"> <asp:Label ID="Label5" runat="server"  Text='<%# Eval("txt18") %>'></asp:Label></td>
                                           <td style="width: 10%; border: 1px solid"> <asp:Label ID="Label6" runat="server"  Text='<%# Eval("txt19") %>'></asp:Label></td>
                                           <td style="border: 1px solid; width: 5%;"> <asp:Label ID="Label7" runat="server"  Text='<%# Eval("txtT1") %>'></asp:Label></td>
                                               
                                                </tr> 
                                                </table>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
<%--                            <tr>
                                <td style="border: 1px solid; width: 8%;" colspan="2">
                                    <asp:DropDownList ID="ddlEmployeeList" runat="server" Width="100%" Height="30px" OnSelectedIndexChanged="ddlEmployeeList_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </td>
                                <td style="border: 1px solid; width: 10%;">
                                    <asp:TextBox ID="txtEmpName" MaxLength="50" ReadOnly="true" runat="server"></asp:TextBox></td>
                                <td style="border: 1px solid; width: 10%;">
                                    <asp:TextBox ID="txtDatJob" MaxLength="30" ReadOnly="true" runat="server"></asp:TextBox></td>
                                <td style="border: 1px solid; width: 10%;">
                                    <asp:TextBox ID="txtDesignation" MaxLength="30" ReadOnly="true" runat="server"></asp:TextBox></td>
                                <td style="border: 1px solid; width: 10%;">
                                    <asp:TextBox ID="txt15" Width="100%" MaxLength="2" onkeypress="return onlyNumbers(event)"  onchange ="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>

                                <td style="border: 1px solid; width: 10%;">
                                    <asp:TextBox ID="txt16" Width="100%" MaxLength="2" onkeypress="return onlyNumbers(event)" onchange ="return funChkEnterAndMaxMarksNested(this,this.value);"  runat="server"></asp:TextBox></td>
                                <td style="border: 1px solid; width: 10%;">
                                    <asp:TextBox ID="txt17" Width="100%" MaxLength="2" onkeypress="return onlyNumbers(event)" onchange ="return funChkEnterAndMaxMarksNested(this,this.value);"  runat="server"></asp:TextBox></td>
                                <td style="border: 1px solid; width: 10%;">
                                    <asp:TextBox ID="txt18" Width="100%" MaxLength="2" onkeypress="return onlyNumbers(event)" onchange ="return funChkEnterAndMaxMarksNested(this,this.value);"  runat="server"></asp:TextBox></td>

                                <td style="border: 1px solid; width: 10%;">
                                    <asp:TextBox ID="txt19" Width="100%" MaxLength="2" onkeypress="return onlyNumbers(event)" onchange ="return funChkEnterAndMaxMarksNested(this,this.value);"  runat="server"></asp:TextBox></td>
                                <td style="border: 1px solid; width: 5%;">
                                    <asp:TextBox ID="txtT1" Width="100%" MaxLength="2" onkeypress="return onlyNumbers(event)" onchange ="return funChkEnterAndMaxMarksNested1(this,this.value);"  runat="server"></asp:TextBox>
                                </td>
                            </tr>--%>
                        </table>
                    </div>
                </div>
                <div style="text-align: right; width: 98%; margin-bottom: 40px;">
                    <asp:Button ID="btnsave" runat="server" Text="Approve" Visible="false"  OnClick="btnsave_Click" />
                    <asp:Button ID="btnSubmit" runat="server" Text="Reject" Visible="false"  OnClick="btnSubmit_Click" />
                    <asp:Button ID="btntest" OnClientClick="PrintDiv();" runat="server" Visible="false" Text="Print" />
                </div>
                      </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

