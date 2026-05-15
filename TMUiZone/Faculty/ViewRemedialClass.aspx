<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="ViewRemedialClass.aspx.cs" Inherits="Faculty_ViewRemedialClass" EnableEventValidation="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <style type="text/css">

        
     .GridPager a, .GridPager span
    {
        display: block;
        height: 15px;
        width: 15px;
        font-weight: bold;
        text-align: center;
        text-decoration: none;
    }
    .GridPager a
    {
        background-color: #f5f5f5;
       
        border: 1px solid #969696;
    }
    .GridPager span
    {
        background-color: #A1DCF2;
       
        border: 1px solid #3AC0F2;
    }   


    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--  <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

        <ContentTemplate>--%>


       <fieldset class="boxBody" >
 <asp:Label ID="Label1" runat="server" 
            Text="Remedial Classes /Additional / Extra" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 </fieldset>



    <table cellpadding="0px" cellspacing="0px" style="width:100%"><tr> 
        <td style="width:10px">  </td> <td>

         <table cellpadding="0px" cellspacing="0px" style="width:100%"> 
              <tr> <td style="height:10px">  </td></tr>
        <tr> <td align="right"> <asp:Button ID="btnCreate" runat="server" Text="Create New " OnClick="btnCreate_Click" /> </td></tr>

        <tr> <td style="height:10px">  </td></tr>

             <tr> <td> <table cellpadding="0px" cellspacing="0px" > <tr> <td>  Filter with </td> <td style="width:10px">  </td> <td>  <asp:RadioButton ID="rdCourse" runat="server" Text="Course" GroupName="remclassd" AutoPostBack="True" OnCheckedChanged="rdCourse_CheckedChanged"/></td> <td style="width:10px"> </td> <td> <asp:RadioButton ID="rdSubject" runat="server" Text="Subject" GroupName="remclassd" AutoPostBack="True" OnCheckedChanged="rdSubject_CheckedChanged"/> </td><td style="width:10px"> </td> <td>  <asp:RadioButton ID="rdStudentNo" runat="server" Text="Student No" GroupName="remclassd" AutoPostBack="True" OnCheckedChanged="rdStudentNo_CheckedChanged" /></td><td style="width:10px"> </td>  <td>
                 <asp:RadioButton ID="rdEnrollmentno" runat="server" Text="Enrollment No" GroupName="remclassd" AutoPostBack="True" OnCheckedChanged="rdEnrollmentno_CheckedChanged"/> </td> <td style="width:10px">  </td> <td><asp:RadioButton ID="rdFaculty" runat="server" Text="Faculty" GroupName="remclassd" AutoPostBack="True" OnCheckedChanged="rdFaculty_CheckedChanged" /> </td> <td style="width:10px">  </td> <td>  <asp:DropDownList ID="ddFilterdata" runat="server" Height="28px" AutoPostBack="True" OnSelectedIndexChanged="ddFilterdata_SelectedIndexChanged" Visible="False"></asp:DropDownList></td> <td style="width:10px"> </td>  <td> <asp:Button ID="btnExportToexcel" runat="server" Text="Export to Excel" OnClick="btnExportToexcel_Click" CssClass="btn"/></td>
                 <td>
                     <asp:Button ID="btnpdf" runat="server" Text="Export to PDF" OnClick="btnpdf_Click" /></td></tr> </table> </td> </tr>
 <tr> <td style="height:10px">  </td></tr>
        <tr> <td>  
            <asp:GridView ID="grdListofStudent" runat="server" AutoGenerateColumns="False" ForeColor="#333333" GridLines="None" CssClass="table table-striped table-bordered table-hover" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" AllowPaging="True" OnPageIndexChanging="grdListofStudent_PageIndexChanging" OnRowDataBound="grdListofStudent_RowDataBound"  >
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>

                  
                     <asp:TemplateField HeaderText="Course">
                        <ItemTemplate>
                            <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("Course") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

<%--                    <asp:BoundField DataField="Course" HeaderText="Course" />--%>
<%--                    <asp:BoundField DataField="Semester" HeaderText="Semester / Year" />--%>

                    <asp:TemplateField HeaderText="Exam Method">
                        <ItemTemplate>
                            <asp:Label ID="lblExamMethed" runat="server" Text='<%# Eval("Exam Methed") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Semester/ Year">
                        <ItemTemplate>
                            <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("Semester") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

<%--                    <asp:BoundField DataField="Section" HeaderText="Section" />--%>
                       <asp:TemplateField HeaderText="Section">
                        <ItemTemplate>
                            <asp:Label ID="lblSection" runat="server" Text='<%# Eval("Section") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                                                             <asp:TemplateField HeaderText="AC. Year">
                        <ItemTemplate>
                            <asp:Label ID="lblAcademicyrs" runat="server" Text='<%# Eval("[Academic Year]") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

<%--                    <asp:BoundField DataField="Subject" HeaderText="Subject" />--%>
                       <asp:TemplateField HeaderText="Subject">
                        <ItemTemplate>
                            <asp:Label ID="lblSubject" runat="server" Text='<%# Eval("Subject") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:BoundField DataField="Subject Type" HeaderText="Type" />

                
                    <asp:BoundField DataField="Room Allocation" HeaderText="Room Allocation" />
                     <asp:BoundField DataField="Subject Classification" ControlStyle-Font-Size="X-Small" HeaderText="Classification" />
                    <%--<asp:BoundField DataField="Faculty Code" HeaderText="Faculty ID" />--%>
                         <asp:TemplateField HeaderText="Faculty">
                        <ItemTemplate>
                            <asp:Label ID="lblFacultyCode" runat="server" Text='<%# Eval("[Faculty Code]") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:BoundField DataField="Faculty Name" HeaderText="Faculty" />


<%--                    <asp:BoundField DataField="Start Date" HeaderText="Start Date" DataFormatString="{0:yyyy-MM-dd}" />--%>
                       <asp:TemplateField HeaderText="Start Date">
                        <ItemTemplate>
                            <asp:Label ID="lblStartDate" runat="server" Text='<%# Eval("[Start Date]","{0:dd MMM yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                      <asp:TemplateField HeaderText="End Date">
                        <ItemTemplate>
                            <asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("[End Date]","{0:dd MMM yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
<%--                    <asp:BoundField DataField="End Date" HeaderText="End Date" DataFormatString="{0:yyyy-MM-dd}"/>--%>
                    <asp:BoundField DataField="Hour No From" HeaderText="Hour No From" />
                    <asp:BoundField DataField="Hour No Till" HeaderText="Till" />
<%--                    <asp:BoundField DataField="Student No" HeaderText="Student No" />--%>
                                          <asp:TemplateField HeaderText="Student No.">
                        <ItemTemplate>
                            <asp:Label ID="lblStudentNo" runat="server" Text='<%# Eval("[Student No]") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

<%--                    <asp:BoundField DataField="Enrollment No" HeaderText="Enrollment No" />--%>

                     <asp:TemplateField HeaderText="Enrollment No">
                        <ItemTemplate>
                            <asp:Label ID="lblEnrollmentNo" runat="server" Text='<%# Eval("[Enrollment No]") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                                         <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblgldimension" runat="server" Text='<%# Eval("[Global Dimension 1]") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="Name" HeaderText="Name" Visible="false" />

                  
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="false" OnCommand="btnDelete_Command"  CommandArgument='<%# Eval("id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <EmptyDataTemplate>
                    There are no record found..
                </EmptyDataTemplate>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle  BackColor="#ff9900" Font-Bold="True" ForeColor="White" Font-Size="9px" />
                <PagerStyle CssClass = "GridPager" HorizontalAlign="Left" BackColor="#ff9900"/>
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Size="8.9px" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>

             </td></tr>

        <tr> <td style="height:90px">  </td></tr>
    </table>
                                                                                   </td> <td style="width:10px"> </td></tr> </table>

   

     <%--   </ContentTemplate>

    </asp:UpdatePanel>--%>


</asp:Content>

