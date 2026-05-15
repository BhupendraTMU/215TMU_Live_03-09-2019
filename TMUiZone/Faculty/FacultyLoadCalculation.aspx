<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="FacultyLoadCalculation.aspx.cs" Inherits="Faculty_FacultyLoadCalculation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <script type="text/javascript">
          function callFeedbackMessage(inputType, inputText) {

              if (inputType == 'Error') {
                  alertify.error(inputText);
                  return false;
              }
              else if (inputType == 'Success') {
                  //alertify.confirm().set('overflow', false);
                  alertify.success("Save Successfully");
                  return false;
              }
              else {
                  alertify.log(inputText, "", 10000);
                  return false;
              }
          }


          function myFunction() {
              return confirm("Are you Sure !");
          }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
  <fieldset class="boxBody">
     <table>
         <tr>
             <td>
                 <asp:Label ID="Label1" runat="server" 
            Text="Assign Subject" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
             </td>
             <td>&nbsp&nbsp </td>
             <td>
                  Academic Year:&nbsp&nbsp
                    <asp:DropDownList ID="ddlAcademicYear" Width="100px" Height="20px" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged"></asp:DropDownList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Font-Size="13px"  runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="ddlAcademicYear" ValidationGroup="g1" ErrorMessage="Please select the Academic Year!"></asp:RequiredFieldValidator>

             </td>
         </tr>
     </table>
      
 </fieldset>
    <fieldset class="boxBodyHeader">   
        
    </fieldset>


    <fieldset class="boxBodyInner"> 
        <center>
        <div class="row">
            
            <div class="col-lg-1">
               
                <asp:DropDownList ID="ddlCourse" width="170px" CssClass="form-control dropdown-toggle" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvddlCourse" ControlToValidate="ddlCourse" Font-Size="13px" Display="Dynamic" ForeColor="Red" ErrorMessage="*" ValidationGroup="g1" runat="server" ></asp:RequiredFieldValidator>
            </div>
            <div class="col-lg-1 col-md-offset-1" >
                
                <asp:DropDownList ID="ddlSemYear" width="150px" CssClass="form-control dropdown-toggle" runat="server"  AutoPostBack="True" OnSelectedIndexChanged="ddlSemYear_SelectedIndexChanged"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvddlSemYear" ControlToValidate="ddlSemYear" Font-Size="13px" Display="Dynamic" ForeColor="Red" ErrorMessage="*" ValidationGroup="g1" runat="server" ></asp:RequiredFieldValidator>
            </div>
            <div class="col-lg-2 col-md-offset-1" >
                <asp:DropDownList ID="ddlSubject" width="250px" CssClass="form-control dropdown-toggle"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvddlSubject" ControlToValidate="ddlSubject" runat="server" Font-Size="13px" Display="Dynamic" ForeColor="Red" ErrorMessage="*" ValidationGroup="g1"></asp:RequiredFieldValidator>
            </div>
            <div class="col-lg-1 col-md-offset-1" >
                <asp:DropDownList ID="ddlFaculty" width="300px" CssClass="form-control dropdown-toggle" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvddlFaculty" ControlToValidate="ddlFaculty" runat="server" Font-Size="13px" Display="Dynamic" ForeColor="Red" ErrorMessage="*" ValidationGroup="g1"></asp:RequiredFieldValidator>
            </div>
            
            <div class="col-lg-1 col-md-offset-1">
                
            </div>
            <div class="col-lg-1 "><br />
                <br />
            </div>
            <div class="col-lg-1 col-md-offset-1"><br />
                </div>
        </div>
        <div class="row">
            <div class="col-lg-1">
                 <asp:DropDownList ID="ddlSection" width="150px" CssClass="form-control dropdown-toggle" AutoPostBack="True" runat="server" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="col-lg-1 col-md-offset-1">

                <asp:DropDownList ID="ddlGroup" runat="server" width="150px" CssClass="form-control dropdown-toggle" ></asp:DropDownList>
            </div>
            <div class="col-lg-1 col-md-offset-1">
                 <asp:DropDownList ID="ddlBatch" runat="server" width="250px" CssClass="form-control dropdown-toggle"></asp:DropDownList>
            </div> 
            <div class="col-lg-1 col-md-offset-2">
                <asp:Button ID="btnAdd" runat="server" width="150px" class="btn btn-info btn-sm" Text="Add" ValidationGroup="g1" OnClick="btnAdd_Click" /><br />
                </div>
            <div class="col-lg-1 col-md-offset-1">

                <asp:Button ID="btnSearch" runat="server" width="100px" class="btn btn-info btn-sm" Text="Search" OnClick="btnSearch_Click" />
            </div>
        </div>
            <div class="row">
                <div class="col-lg-1"">
                    <br />


                    <asp:GridView ID="grvAssignSubject" runat="server" EmptyDataText="There are no data records to display." 
         Width="1000px" CssClass="table table-striped table-bordered table-hover" BackColor="White" BorderColor="#3366CC" BorderStyle="None" 
         BorderWidth="1px" CellPadding="4" AutoGenerateColumns="False" DataKeyNames="LineNo_">
                        <Columns>
                            <asp:TemplateField HeaderText="Sl. No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle Width="7%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Course" HeaderText="Course" />
                            <asp:BoundField DataField="Subject" HeaderText="Subject" />
                            <asp:BoundField DataField="Semester/Year" HeaderText="Semester/Year" />
                            <asp:BoundField DataField="Faculty" HeaderText="Faculty" />
                            <asp:BoundField DataField="Section" HeaderText="Section" />
                            <asp:BoundField DataField="Group" HeaderText="Group" />
                            <asp:BoundField DataField="Batch" HeaderText="Batch" />
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <%--<asp:CheckBox ID="chkDelete" runat="server" onclick="return alert(hi);" OnCheckedChanged="chkDelete_CheckedChanged" AutoPostBack="True" />--%>
                                    <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure you want to delete ?');">Delete</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                        </Columns>
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#ff9900" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" ForeColor="#003399" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                    </asp:GridView>
                </div>
                <div class="col-lg-9 col-md-offset-2"></div>
            </div>
            </center>
        </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

