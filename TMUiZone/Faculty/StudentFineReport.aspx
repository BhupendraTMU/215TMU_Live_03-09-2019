<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="StudentFineReport.aspx.cs" Inherits="StudentFineReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="bootstrap/js/jquery-1.11.2.min.js"></script>
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="bootstrap/js/bootstrap.min.js"></script>

    <style>
        .red-border {
            border: 1px solid red;
        }
    </style>
    <style type="text/css">
        .completionList {
            border: solid 1px Gray;
            margin: 0px;
            padding: 3px;
            height: 140px;
            overflow: scroll;
            background-color: #FFFFFF;
        }

        .listItem {
            color: #191919;
        }

        .itemHighlighted {
            background-color: #ADD6FF;
        }
    </style>
    <script type="text/javascript">

       
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="stud" runat="server"></asp:ScriptManager>
   <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <asp:UpdatePanel runat="server" ID="updmain">
        <ContentTemplate>
    <fieldset class="boxBody">
        <table>
            <tr>
<td>
        <asp:Label ID="lblHeader" runat="server"
            Text="Student Fine Report" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</td>
                <td>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    Academic Year:&nbsp&nbsp
                    <asp:DropDownList ID="ddlAcademicYear" Width="100px" Height="20px" runat="server" AutoPostBack="true"  ></asp:DropDownList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Font-Size="13px"  runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="ddlAcademicYear" ValidationGroup="sr1" ErrorMessage="Please select the Academic Year!"></asp:RequiredFieldValidator>
                         </td>

            </tr>
        </table>         
    </fieldset>
    <fieldset class="boxBodyHeader">
    </fieldset>
    <fieldset class="boxBodyInner">
        <div style="width: 98%; text-align: center">


            <table cellpadding="0px" cellspacing="0px" width="100%">                              
                <tr>
                    <td style="padding-left: 3%">
                               
            <asp:TextBox runat="server" ID="txtFromtDate" CssClass="form-control input-sm" Width="120px" ToolTip="Imposed From Date" onkeypress="return false"
                                       onKeyDown="preventBackspace();" placeholder="From Date"></asp:TextBox>&nbsp
                                <asp:Image src="../Images/Calendar.png" runat="server" Height="30px" Width="30px" alt="" ID="fdate" />
                                   <asp:CalendarExtender ID="cleFromDate" Format="dd MMM yyyy" runat="server" CssClass="cal_Theme1"
                                       PopupButtonID="fdate" Enabled="true" TargetControlID="txtFromtDate" />  &nbsp&nbsp&nbsp&nbsp                     

                               
                           </td>
                           <td style="padding-left: 2px">
                               <asp:TextBox runat="server" ID="txtToDate" CssClass="form-control input-sm" Width="120px" ToolTip="Imposed To Date" onkeypress="return false"
                                   onKeyDown="preventBackspace();" placeholder="To Date"></asp:TextBox>&nbsp
                                <asp:Image src="../Images/Calendar.png" runat="server" Height="30px" Width="30px" alt="" ID="tdate" />
                               <asp:CalendarExtender ID="cleToDate" Format="dd MMM yyyy" runat="server" CssClass="cal_Theme1"
                                   PopupButtonID="tdate" Enabled="true" TargetControlID="txtToDate" />
                               &nbsp&nbsp&nbsp&nbsp                    
                           </td>
                
                    <td style="padding-left: 2px">
                        <asp:DropDownList runat="server" ID="drpCourse" AutoPostBack="true" CssClass="form-control input-sm" Width="120px" Height="30px" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged"></asp:DropDownList>
                       
                        <asp:HiddenField ID="hfStudentId" runat="server" />
                    </td>
                    
                    <td>
                        <asp:DropDownList runat="server" ID="drpSemester" Width="120px" CssClass="form-control input-sm" Height="30px"></asp:DropDownList>
                    </td>
                    <td align="right">
                        <label style="line-height: 35px">&nbsp;&nbsp; &nbsp;&nbsp;ActionTaken</label>
                    </td>                    
                    <td align="right">
                        <asp:DropDownList runat="server" ID="drpAction" CssClass="form-control input-sm" Width="120px" Height="30px">
                        </asp:DropDownList>
                    </td>

                    <td style="width: 10px; padding-left: 30px" align="left">
                        <asp:Button ID="Button1" runat="server" Text="Show" class="btn btn-info btn" ValidationGroup="sr1" OnClick="Button1_Click" />
                    </td>

                    <td  align="left">
                   <asp:ImageButton ID="btnExportToExcel" runat="server" ImageUrl="~/images/excel.jpg"  OnClick="btnExportToExcel_Click" Width="40px" Height="30px" Visible="true" ValidationGroup="show"></asp:ImageButton>                  
                     <asp:ImageButton ID="Btnpdf" runat="server" ImageUrl="~/images/pdf.jpg"  OnClick="Btnpdf_Click" Width="40px" Height="30px" Visible="true" ValidationGroup="show"></asp:ImageButton>                  
                    
                    </td>
                </tr>
                <tr >
                    <td colspan="8" style="height:2px;" >
                        

                <asp:HiddenField ID="hfR_Session" runat="server"  />
                <asp:HiddenField ID="hfR_Course" runat="server" />
                <asp:HiddenField ID="hfR_SemYear" runat="server" />
                <asp:HiddenField ID="hfR_Action" runat="server" />
                <asp:Label ID="lblpdf" runat="server" Visible="false"></asp:Label>





                    </td>
                </tr>
                <tr>
                    <td colspan="8" style="height: 10px; width: 99%; padding-left: 40px" align="center">
                        <asp:GridView ID="GridView1"  runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false"  
                            OnDataBound="OnDataBound" BackColor="White" EmptyDataText="There are no data records to display." BorderColor="#E7E7FF" 
                            BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="99%" GridLines="Horizontal" AllowPaging="false"
                            PageSize="20" OnPageIndexChanging="GridView1_PageIndexChanging" ShowFooter="true">
                            <AlternatingRowStyle BackColor="#F7F7F7" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sr. No.">
                           <ItemTemplate >
                          <%# Container.DataItemIndex + 1 %>
                                     </ItemTemplate>
                                       <ItemStyle Width="7%" />
                                    </asp:TemplateField>
                                <asp:BoundField ItemStyle-Width="150px" DataField="Student Name" HeaderText="Name">
                                    <ItemStyle Width="150px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField ItemStyle-Width="150px" DataField="Enrollment No_" HeaderText="Enroll. No">
                                    <ItemStyle Width="150px"></ItemStyle>
                                </asp:BoundField> 
                                <asp:BoundField ItemStyle-Width="150px" DataField="Global Dimension 1 Code" HeaderText="college" >
                                    <ItemStyle Width="150px"></ItemStyle>
                                </asp:BoundField>

                                 <asp:BoundField ItemStyle-Width="150px" DataField="Staff Code" HeaderText="Staff" Visible="false">
                                    <ItemStyle Width="150px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField ItemStyle-Width="150px"  DataField="Course Code" HeaderText="Course/Department">
                                    <ItemStyle Width="150px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField ItemStyle-Width="150px" DataField="Semester\Year" HeaderText="Semester">
                                    <ItemStyle Width="150px"></ItemStyle>
                                </asp:BoundField>

                                <asp:BoundField ItemStyle-Width="150px" DataField="Action Taken" HeaderText="Action Taken">
                                    <ItemStyle Width="150px"></ItemStyle>
                                </asp:BoundField>
                                <%--<asp:BoundField ItemStyle-Width="150px" DataField="Fine Amount" DataFormatString="{0:n}" HeaderText="Amount">
                                    <ItemStyle Width="150px"></ItemStyle>
                                    </asp:BoundField>--%>
                                <asp:BoundField ItemStyle-Width="150px" DataField="Created Date" DataFormatString="{0:dd MMM yyyy}" HeaderText="Imposed Date">
                                    <ItemStyle Width="150px"></ItemStyle>
                               
                                     </asp:BoundField>
                                <asp:BoundField ItemStyle-Width="150px" DataField="Date Commited" DataFormatString="{0:dd MMM yyyy}" HeaderText="Date Commited">
                                    <ItemStyle Width="150px"></ItemStyle>

                                </asp:BoundField>
                                <asp:BoundField ItemStyle-Width="150px" DataField="Description" HeaderText="Reason">
                                    <ItemStyle Width="150px"></ItemStyle>
                                </asp:BoundField>
                                 <asp:BoundField DataField="Fine Amount" HeaderText="Amount" ItemStyle-Width="60" DataFormatString="{0:N2}"
        ItemStyle-HorizontalAlign="Right" />
                            </Columns>
                            <FooterStyle BackColor="#ed7600" ForeColor="#4A3C8C" />
                            
                            <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <SortedAscendingCellStyle BackColor="#F4F4FD" />
                            <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                            <SortedDescendingCellStyle BackColor="#D8D8F0" />
                            <SortedDescendingHeaderStyle BackColor="#3E3277" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>

            
        </div>
    </fieldset>
    </ContentTemplate> 
         <Triggers>
                     <asp:PostBackTrigger ControlID="btnExportToexcel" />
                    <asp:PostBackTrigger ControlID="Btnpdf" />
                 </Triggers>       
    </asp:UpdatePanel>

    <asp:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1"
        TargetControlID="updmain" runat="server">
        <Animations>
            <OnUpdating>
               <Parallel duration="0">
                    <ScriptAction Script="onUpdating();" />
<%--                    <EnableAction AnimationTarget="BtnShow" Enabled="false" /> --%>                  
                   <EnableAction AnimationTarget="BtnExportToExcel" Enabled="false" /> 
                            
                   <EnableAction AnimationTarget="Btnpdf" Enabled="false" />             
                </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                    <ScriptAction Script="onUpdated();" />
<%--                    <EnableAction AnimationTarget="BtnShow" Enabled="true" />--%>
                    <EnableAction AnimationTarget="BtnExportToExcel" Enabled="true" />
                    <EnableAction AnimationTarget="Btnpdf" Enabled="true" />
                </Parallel>
            </OnUpdated>
        </Animations>
        </asp:UpdatePanelAnimationExtender>

</asp:Content>

