<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="FacultyLessonPlanMD.aspx.cs" Inherits="Faculty_FacultyLessonPlanMD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
      <fieldset class="boxBody">
 <asp:Label ID="Label1" runat="server" 
            Text="Lesson Plan" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 </fieldset>
    <fieldset class="boxBodyHeader"> 
  
 </fieldset>
     <fieldset class="boxBodyInner"> 
    <center>
        <br />
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="pull-left">
                    <asp:CheckBox runat="server" ID="chkboxPrinciple" AutoPostBack="true" Font-Bold="true" Visible="false" Text="Show more" OnCheckedChanged="chkboxPrinciple_CheckedChanged"/>
                </div>
                <div class="pull-right">
                    <asp:Label ID="lblFaculty" runat="server" Text="Faculty :" Visible="false" Font-Bold="true"  ></asp:Label>
                    <asp:DropDownList runat="server" ID="ddlFaculty" AutoPostBack="true" Font-Bold="true" Visible="false" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged"  />
                     <asp:RequiredFieldValidator ID="rfvFaculty" Display="Dynamic" Visible="false" runat="server" ValidationGroup="g1" ErrorMessage="Please select Faculty!"  ForeColor="Red" ControlToValidate="ddlFaculty"></asp:RequiredFieldValidator>
                </div>
                     <div class="clearfix"></div>
                     <br />
                 <asp:Panel runat="server" ID="pnlMain"  BorderColor="#e8e8e8" BorderWidth="0px" Width="100%">
    <table style=" background-color:rgba(0, 0, 0, 0.06);" width="100%" >
        <tr style="height:10px"><td colspan="10"></td></tr>
        <tr>
            <td >&nbsp&nbsp&nbsp&nbsp&nbsp</td>
            <td><label>Course:</label></td>
            <td style="width:5px"></td>
            <td><asp:DropDownList ID="drpCourse" runat="server" Width="150px" Height="20px" AutoPostBack="true" BackColor="#e8e8e8" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged1"></asp:DropDownList></td>
          
            <td ><label>Semester/Year:</label></td>
            <td style="width:5px"></td>
            <td><asp:DropDownList ID="drpSemesterYear" runat="server" Width="150px" Height="20px" BackColor="#e8e8e8" AutoPostBack="true" OnSelectedIndexChanged="drpSemesterYear_SelectedIndexChanged"></asp:DropDownList></td>
            
            <td ><label>Section:</label></td>
            <td style="width:5px"></td>
            <td><asp:DropDownList ID="drpSection" runat="server" Width="150px" Height="20px" BackColor="#e8e8e8" AutoPostBack="true" OnSelectedIndexChanged="drpSection_SelectedIndexChanged"></asp:DropDownList></td>
            <td ></td>
            <td ><label>Subject Code:</label></td>
            <td style="width:5px"></td>
            <td><asp:DropDownList ID="drpSubjectCode" runat="server" Width="150px" Height="20px" BackColor="#e8e8e8"></asp:DropDownList></td>
            <td></td>
            <td style="width:200px">
                <center>
                <asp:LinkButton  id="btnShow" ValidationGroup="g1"  class="btn btn-info btn-sm" runat="server"  OnClick="btnShow_Click">
                         <span class="glyphicon glyphicon-upload"></span> Show 
                          </asp:LinkButton>&nbsp
                </center>
                 <%--<center>
            <asp:Button ID="btnShow" ImageUrl="~/images/home.png" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" Text="Show Plans"  Height="25px" Width="40px" OnClick="btnShow_Click"   />
                    </center>--%>
            </td>
        </tr>
        <tr>
            <td ></td>
            <td colspan="3">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ValidationGroup="g1" ErrorMessage="Please select the Course!"  ForeColor="Red" ControlToValidate="drpCourse"></asp:RequiredFieldValidator>
            </td>
             <td ></td>
            <td colspan="3">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ValidationGroup="g1" ErrorMessage="Please select the Semester!"  ForeColor="Red" ControlToValidate="drpSemesterYear"></asp:RequiredFieldValidator>
            </td>
            <td colspan="3"></td>
            <td colspan="3">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ValidationGroup="g1" ForeColor="Red" ControlToValidate="drpSubjectCode" ErrorMessage="Please select the Subject!"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr style="height:15px"><td></td></tr>   
        </table>       
                <br />
                     <center>
                     <asp:Label ID="lblProposedLessonPlan" Visible="false" runat="server" ForeColor="#0099cc"><u>Proposed Lesson Plan</u></asp:Label>                        
                         &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                         <asp:ImageButton ID="btnExportToexcellProp" runat="server" ImageUrl="~/images/excel.jpg" OnClick="btnExportToexcellProp_Click" Width="40px" Height="30px" Visible="false"></asp:ImageButton>
                         </center>
                     <br />
                       <%--<asp:GridView ID="grdLessonPlan" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"  GridLines="Horizontal"   EmptyDataText="There are no data records to display." >
               <AlternatingRowStyle BackColor="#F7F7F7" />--%>
                            <asp:GridView ID="grdLessonPlan" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" 
                                    AutoGenerateColumns="False"  EmptyDataText="There are no data records to display."  >
                        <Columns> 
                             <asp:BoundField DataField="ScheduledDate" HeaderText="Date" SortExpression="ScheduledDate"  HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" Visible="false"/>                         
                            <asp:BoundField DataField="Unit Code" HeaderText="Unit Code" SortExpression="Unit Code"  HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Unit Name" HeaderText="Unit Name" SortExpression="Unit Name"  HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Chapter Code" HeaderText="Chapter Code" SortExpression="Chapter Code"  HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Chapter Name" HeaderText="Chapter Name" SortExpression="Chapter Name"  HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Lecture" HeaderText="Lecture" SortExpression="Lecture"  HeaderStyle-CssClass="visible-lg"  ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="No of Minuites" HeaderText="No. of Minutes" SortExpression="No of Minuites"  HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" Visible="false"  />
                            <asp:BoundField DataField="Week" HeaderText="Weeks" SortExpression="Week"  HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" Visible="false" />
                            <asp:BoundField DataField="Topics" HeaderText="Topics" SortExpression="Topics"  HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                          </Columns>                       
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

                     <br />
                     <center>
                     <asp:Label ID="lblActualLessonPlan" Visible="false" runat="server" ForeColor="#0099cc"><u>Actual Lesson Plan</u></asp:Label>
                        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                         <asp:ImageButton ID="btnExportToexcellAct" runat="server" ImageUrl="~/images/excel.jpg" OnClick="btnExportToexcellAct_Click" Width="40px" Height="30px" Visible="false"></asp:ImageButton>
                           </center>
                     <br />
                       <%--<asp:GridView ID="grdActualLessonPlan" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="866px" GridLines="Horizontal"   EmptyDataText="There are no data records to display." >
               <AlternatingRowStyle BackColor="#F7F7F7" />--%>
                     <asp:GridView ID="grdActualLessonPlan" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" 
                                    AutoGenerateColumns="False"  EmptyDataText="There are no data records to display."  >
                        <Columns> 
                            <asp:BoundField DataField="Date1" HeaderText="Date" SortExpression="ApplicantName"  HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Hour" HeaderText="Lecture No." SortExpression="ApplicantName"  HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Select Unit" HeaderText="Unit Name" SortExpression="ApplicantName"  HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Topic Covered" HeaderText="Topic to be Covered" SortExpression="ApplicantName"  HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="NoOfPresentStudent" HeaderText="Present Student" SortExpression="ApplicantName"  HeaderStyle-CssClass="visible-lg"  ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="TotalStudent" HeaderText="Total Student" SortExpression="ApplicantName"  HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            </Columns>                       
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
                      </asp:Panel>       
            </ContentTemplate>
            <Triggers>
                     <asp:PostBackTrigger ControlID="btnExportToexcellProp" />
                     <asp:PostBackTrigger ControlID="btnExportToexcellAct" />
                 </Triggers>
        </asp:UpdatePanel>
        <br />
        
        </center>
        </fieldset>
</asp:Content>

