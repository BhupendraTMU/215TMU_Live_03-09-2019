<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="DailyAttendanceDetails.aspx.cs" Inherits="Faculty_DailyAttendanceDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <style type="text/css">
   
    .parent
    {
        text-align:center;
        display: block;
        border: 1px solid outset;
    }
    .child
    {
        display: inline-block;       
        width: 200px;
    }
     .example-print {
            display: none;
        }

        @media print {
            .example-screen {
                display: none;
            }

            .example-print {
                display: block;
            }
        }
</style>
       <script type="text/javascript">

           function PrintDiv() {
               //document.getElementById('PanelHeader').style.visibility = 'visible';
               //alert("bhup");
               var divToPrint = document.getElementById('printarea');
               var popupWin = window.open('', '_blank', 'width=300,height=400,location=no,left=200px, margin:0mm');
               popupWin.document.open();
               popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
               popupWin.document.close();
           }

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
               if (sender._selectedDate > new Date()) {
                   alert("You cannot select greater than current date!");
                   sender._selectedDate = new Date();
                   sender._textbox.set_Value(sender._selectedDate.format(sender._format))
               }
           }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <asp:UpdatePanel runat="server" ID="updmain">
        <ContentTemplate>
    <asp:Panel ID="pnlList" runat="server" BorderWidth="2px" BorderColor="#ACE9FB" ScrollBars="Vertical">
    
    <div class="parent" style="background-color: #ACE9FB" >
                            <center>
                                <div class="child" style="fit-position: left;" >
                                    <b>
                                        <p style="color: white; font-size: 25px">Daily Attendance</p>
                                    </b>
                                </div>                              
                                <div class="child" style="fit-position: right;">
                                    <b>
               <asp:TextBox runat="server" ID="txtDateOfAttendance" CssClass="form-control input-sm"  Width="150px" onkeypress="return false" onKeyDown="preventBackspace();" AutoPostBack="true" ></asp:TextBox>        
                         <asp:Image src="../Images/Calendar.png" runat="server" Height="30px" Width="25px" alt="" id="fdate" />
                        <asp:CalendarExtender ID="cleDate" Format="dd MMM yyyy" runat="server" OnClientDateSelectionChanged="checkDate" CssClass="cal_Theme1" 
                            PopupButtonID="fdate" Enabled="true" TargetControlID="txtDateOfAttendance" />                                      
                                    
                                                      
            
                                         </b>
                                </div>
                            </center>
              
                        </div>
               <fieldset id="filterdata" runat="server" class="boxBodyInner">
             <div style="width: 98%; text-align: center">
            <table cellpadding="0px" cellspacing="0px" width="100%">
                                 
                 <tr><td style="width:10px"></td>
                      <td>Course  </td>
                      <td style="width:10px"> </td>
                     <td> <asp:DropDownList ID="ddCourse" runat="server" CssClass="form-control input-sm" AutoPostBack="True"  OnSelectedIndexChanged="ddCourse_SelectedIndexChanged" Width="140px" Height="28px"></asp:DropDownList> </td> <td style="width:10px">   
                                          </td>
 
                      <td> <asp:Label ID="lblSemester" runat="server" Text="Semester"></asp:Label><asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label> </td> <td style="width:10px">  </td>
                     
                       <td> <asp:DropDownList ID="ddSemester_Year" runat="server" CssClass="form-control input-sm" AutoPostBack="True" OnSelectedIndexChanged="ddSemester_Year_SelectedIndexChanged"  Height="28px" Width="90px"></asp:DropDownList> </td> 
                     <td style="width:10px">  
                     
                     </td>
                     
                     <td> Section  </td>
                      <td style="width:10px"> </td> 
                     <td> <asp:DropDownList ID="ddSection" runat="server" CssClass="form-control input-sm" AutoPostBack="True"  Height="28px" Width="90px">
                        

                          </asp:DropDownList> </td>
                      
                     <td style="width:10px"> &nbsp;</td> <td> Subject </td> <td style="width:10px"> </td>
                      <td> 
                     <asp:DropDownList ID="ddSubject" runat="server" AutoPostBack="True" CssClass="form-control input-sm"  Width="140px" Height="28px">
                      

                     </asp:DropDownList> </td> <td style="width:10px">  
                     
                     </td>
                      <td runat="server" visible="false">Faculty</td>
                      <td style="width:10px">  </td> 
                     <td runat="server" visible="false">                      
                         <asp:DropDownList ID="ddlFaculty" runat="server" AutoPostBack="True"  CssClass="form-control input-sm" Width="100px" Height="28px">
                       

                     </asp:DropDownList> 

<%--  <asp:TextBox runat="server"  Id="txtname" Width="150px" Height="28px"></asp:TextBox>--%>
                           </td>

                     <td>
                         
<%--                          <asp:Button ID="Btnsubmit" runat="server" Text="Show"  class="btn btn-info btn"  />--%>
                      <asp:ImageButton ID="btnExportToExcel" runat="server" ImageUrl="~/images/excel.jpg"  OnClick="btnExportToExcel_Click" Width="40px" Height="30px" Visible="true" ></asp:ImageButton>                  
                     <asp:ImageButton ID="Btnpdf" runat="server" ImageUrl="~/images/pdf.jpg"    OnClientClick="PrintDiv();" Width="40px" Height="30px" Visible="true" ValidationGroup="show"></asp:ImageButton>

                         
                     </td>
                 
                 
                 </tr>
            


         </table>                              
                
</div>
        </fieldset>



            <div id="printarea"> 
                                    <asp:Panel ID="PanelHeader"  class="example-print" runat="server">
                                                                    <div class="panel-heading">
                                <center>
                                    <div class="panel-title" style="fit-position:left;">
                                        <b>
                                            <table style="width: 100%; text-align: left">
                                                <tr>
                                                    <td style="width: 20%; text-align:center">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/rightlogo.png" Width="55%" />
                                                    </td>
                                                    <td style="width: 60%; text-align:center">
                                                     <strong><asp:Label ID="lblCName" Text="Teerthanker Mahaveer University" runat="server"></asp:Label></strong> 
                                                        <br />
                                                         <strong><asp:Label ID="Label1" runat="server" Text="Delhi Road, Moradabad"></asp:Label></strong>
                                                                     <br />
                                                         <br />
                                                          <asp:Label ID="lblAC" runat="server" Text="Daily Attendance Details"></asp:Label>
                                                              
                                           
                                                    </td>
                                                    <td style="width: 20%; text-align:center;margin-top:150px;">
                                                        Date:&nbsp;&nbsp;<asp:Label ID="lblSerchDAte" runat="server"></asp:Label>
                                                        

                                                    </td>
                                                </tr>

                                            </table>
                                        </b>
                                    </div>
                                </center>
                            </div>
                                        </asp:Panel>
         <div class="table-responsive">
                    <asp:GridView ID="grdAttendance" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" 
                        EmptyDataText="There are no data records to display." AllowPaging="true" PageSize="50" OnPageIndexChanging="grdAttendance_PageIndexChanging"  >                        
                        <Columns>   
                        <asp:TemplateField HeaderText="Sr. No.">
                           <ItemTemplate >
                          <%# Container.DataItemIndex + 1 %>
                                     </ItemTemplate>
                                       <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                            <asp:BoundField ItemStyle-Width="20%" DataField="Name" HeaderText="Name">
                                    <ItemStyle Width="20%"></ItemStyle>
                                </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="15%" DataField="Course" HeaderText="Course">
                                    <ItemStyle Width="15%"></ItemStyle>
                                </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="20%" DataField="Subject" HeaderText="Subject" >
                                    <ItemStyle Width="20%"></ItemStyle>
                            </asp:BoundField>
                               
                              <asp:BoundField ItemStyle-Width="5%" DataField="Semester" HeaderText="Semester">
                                    <ItemStyle Width="5%"></ItemStyle>
                                </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="5%" DataField="Section" HeaderText="Section">
                                    <ItemStyle Width="5%"></ItemStyle>
                                </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="5%" DataField="Hour" HeaderText="Hour">
                                    <ItemStyle Width="5%"></ItemStyle>

                                </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="5%" DataField="Student" HeaderText="Student">
                                    <ItemStyle Width="5%"></ItemStyle>

                                </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="5%" DataField="Present" HeaderText="Present">
                                    <ItemStyle Width="5%"></ItemStyle>
                                </asp:BoundField>

                        </Columns>
                         <AlternatingRowStyle CssClass="danger" />
                        <PagerStyle HorizontalAlign = "Right" CssClass = "GridPager" />
                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" PreviousPageText="Previous" NextPageText="Next" FirstPageText="First" LastPageText="Last" Position="TopAndBottom"  />
                    </asp:GridView>
             <br />
              
                </div>
                                        
            </div>

      </asp:Panel>
  </ContentTemplate>
        
         <Triggers>
                     <asp:PostBackTrigger ControlID="btnExportToexcel" />
                    <asp:PostBackTrigger ControlID="Btnpdf" />
                 </Triggers>       
    </asp:UpdatePanel>
    
</asp:Content>

