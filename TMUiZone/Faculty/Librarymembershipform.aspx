<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="Librarymembershipform.aspx.cs" Inherits="Faculty_Librarymembershipform" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

            prtContent.innerHTML = strOldOne;
        }
    </script>

    <%--<script type="text/javascript">
        function PrintDiv() {
            var divToPrint = document.getElementById('widget-content');
            var popupWin = window.open('', '_blank', 'width=300,height=400,location=no,left=200px');
            popupWin.document.open();
            popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
            popupWin.document.close();
        }
         </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody" style="text-align: center;">
        <asp:Label ID="lblhodstatus" runat="server" ForeColor="Red" Visible="false" Font-Size="Medium" Text="HOD STATUS:"></asp:Label>
        <asp:Label ID="lblhodstatus1" runat="server" ForeColor="Red" Visible="false" Font-Size="Medium" Text=""></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Size="Medium" Visible="false" Text="DEPUTY LIBRARIAN STATUS:"></asp:Label>
        <asp:Label ID="lbldeputylibraianstatus" runat="server" ForeColor="Red" Visible="false" Font-Size="Medium" Text=""></asp:Label>

    </fieldset>
    <div id="bill">
        <fieldset class="boxBody">
            <%-- <div style="width: 100%; margin-bottom: 10px; margin-left: 1%; margin-right: 1%; margin-top: 5px;">--%>

            <table>

                <tr>
                    <td style="width: 1%"></td>
                    <td style="width: 12%; text-align: left">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/UPDATEDLOGO.jpg" Width="55%" />

                    </td>
                    <td style="width: 65%; text-align: center">
                        <strong>
                            <asp:Label ID="lblcentral" Font-Size="Large" Text="CENTRAL LIBRARY:GENERAL" runat="server"></asp:Label></strong>
                        <br />
                        <strong>
                            <asp:Label ID="lblCName" Font-Size="Large" Text="TEERTHANKER MAHAVEER UNIVERSITY" runat="server"></asp:Label></strong>
                        <br />
                        <strong>
                            <asp:Label ID="lblAC" runat="server" Text="(Established under Govt. of U. P. Act No. 30, 2008)"></asp:Label></strong>

                        <br />
                        <strong>
                            <asp:Label ID="LblType" runat="server" Text="Delhi Road,Moradabad(U.P) India"></asp:Label>
                        </strong>
                        <br />
                    </td>
                    <td style="width: 10%; text-align: center"></td>
                </tr>
    </div>

    </table>
        
    </fieldset>
    <fieldset class="boxBody" style="text-align: center; border-color: black; background-color: black;">
        <asp:Label ID="Label1" runat="server" Text="LIBRARY MEMBERSHIP FORM FOR TEACHING/NON-TEACHING EMPLOYEES" Font-Size="15pt" ForeColor="White" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
    <div id="divGeneralBody">
        <fieldset class="boxBodyInner">
            <div class="form-horizontal">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">

                            <div class="form-group">
                                <div class="col-md-2">
                                    <label style="width: 200px">Employee Code:.</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtemployeecode" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>

                                <div class="col-md-2">
                                    <label>Name:.</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtemployeename" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div>
                                    <asp:UpdatePanel ID="pnlpic" runat="server">
                                        <ContentTemplate>
                                            <asp:Image ID="ImgPrv" Height="100px" Width="100px" runat="server" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    </div>
     
                                <%-- <%--<asp:FileUpload ID="FileUpload2" runat="server" Width="150px"/>
                                       <asp:Button ID="btnUpload" runat="server" Text="Upload"/>--%>
                                <%--<asp:FileUpload ID="FileUpload2" runat="server" Width="150px" onchange="ShowImagePreview(this);" />--%>
                            </div>
                            <div class="form-group" style="margin-top: -70px">
                                <div class="col-md-2">
                                    <label style="width: 200px">Designation:.</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtdesignation" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label style="width: 200px">Department:.</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtdepartment" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-2">
                                    <label style="width: 200px">Date Of Birth:.</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtdateofbirth" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label style="width: 200px">Contact No:.</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtmobile" runat="server" onkeypress="return numeric(event)" MaxLength="10" MinLength="10" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-2">
                                    <label style="width: 200px">Local Address:.</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtlocaladdress" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label style="width: 200px">Permanent Address:.</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtpermentaddress" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-2">
                                    <label style="width: 200px">Upload Photo:.</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:FileUpload ID="FileUpload2" runat="server" CssClass="form-control"  onchange="ShowImagePreview(this);" />
                                    <asp:RequiredFieldValidator ID="rfvFileupload" ValidationGroup="validate" runat="server"
                                                ControlToValidate="FileUpload2"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-md-2">
                                    <label style="width: 200px">Email:.</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtemail" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtemail" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>

                                </div>
                            </div>
                            <%--  <fieldset class="boxBody" style="text-align: left; border-color: white;">
                                <asp:Label ID="Label2" runat="server" Text="(FOR OFFICE USE ONLY)" Font-Size="15pt" ForeColor="BLACK" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                            </fieldset>
                            <br />
                            &nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Label3" runat="server" Text="Library Membership(According to TMU I-Card)" Font-Size="15pt" ForeColor="BLACK" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                            <br />
                            <asp:Table class="boxBody" Width="1158px" Style="border: 1px solid" ID="tblData" runat="server"  Height="90px">
                                <asp:TableRow>
                                    <asp:TableHeaderCell Style="border: 1px solid" ColumnSpan="4">
                                        <asp:Label ID="Label7" runat="server" Text="TMU I-Card"></asp:Label>
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell Style="border: 1px solid" ColumnSpan="3">
                                        <asp:Label ID="Label8" runat="server" Text="Dues Status"></asp:Label>
                                    </asp:TableHeaderCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableHeaderCell Style="border: 1px solid">
                                        <asp:Label ID="Label4" runat="server" Text="Employee Code"></asp:Label>
                                    </asp:TableHeaderCell>
                                    <asp:TableCell Style="border: 1px solid">
                                        <asp:Label ID="Label9" runat="server" Text="Prepared by"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Style="border: 1px solid">
                                        <asp:Label ID="Label10" runat="server" Text="Date Of issue"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Style="border: 1px solid">
                                        <asp:Label ID="Label5" runat="server" Text="Received by Faculty/Staff"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Style="border: 1px solid">
                                        <asp:Label ID="Label11" runat="server" Text="Date"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Style="border: 1px solid">
                                        <asp:Label ID="Label12" runat="server" Text="Faculty/Staff"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Style="border: 1px solid">
                                        <asp:Label ID="Label13" runat="server" Text="Rec.By"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableHeaderCell Style="border: 1px solid">
                                        <asp:TextBox ID="txtemployeecodecode1" runat="server" BorderColor="Black"></asp:TextBox>
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell Style="border: 1px solid">
                                        <asp:TextBox ID="txtpreparedby1" runat="server" BorderColor="Black"></asp:TextBox>
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell Style="border: 1px solid">
                                        <asp:TextBox ID="txtdateofissue" runat="server" BorderColor="Black"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtdateofissue" Format="dd MMM yyyy"></asp:CalendarExtender>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdateofissue" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>    --%>
                            <%-- </asp:TableHeaderCell>
                                    <asp:TableHeaderCell Style="border: 1px solid">
                                        <asp:TextBox ID="txtreceivefaulty1" runat="server" BorderColor="Black"></asp:TextBox>
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell Style="border: 1px solid">
                                        <asp:TextBox ID="date1" runat="server" BorderColor="Black"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="date1" Format="dd MMM yyyy"></asp:CalendarExtender>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="date1" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>    --%>
                            <%-- </asp:TableHeaderCell>
                                    <asp:TableHeaderCell Style="border: 1px solid">
                                        <asp:TextBox ID="txtfacultystat1" runat="server" BorderColor="Black"></asp:TextBox>
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell Style="border: 1px solid">
                                        <asp:TextBox ID="txtreceivedby" runat="server" BorderColor="Black"></asp:TextBox>
                                    </asp:TableHeaderCell>
                                </asp:TableRow>


                            </asp:Table>--%>

                            <br />
                            <br />
                            <table>

                                <tr>
                                    <td style="width: 850px;"></td>
                                    <td style="width: 1100px; text-align: center">
                                        <strong>
                                            <asp:Label ID="Label14" Font-Size="Large" Text="Librarian" runat="server"></asp:Label></strong>
                                        <br />
                                        <strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label15" Font-Size="Large" Text="Teerthanker Mahaveer University" runat="server"></asp:Label>
                                            <br />
                                            <asp:Label ID="Label16" runat="server" Text="Moradabad"></asp:Label></strong>


                                        <br />
                                    </td>
                                </tr>
                            </table>
                            <fieldset class="boxBody" style="text-align: center; border-color: white;">
                                <asp:Label ID="Label6" runat="server" Text="RULES AND REGULATIONS OF LIBRARY" Font-Underline="true" Font-Size="15pt" ForeColor="black" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

                            </fieldset>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label style="font-size: medium">1. Entry to the library is through Identity Card.</label>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label style="font-size: medium">2. Six books will be issued for period of 90 Days. The Book shall have to be returned to the library on or before the due date.</label>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label style="font-size: medium">3. Borrower has to maintain the book in good condition, failing which she/he can asked to pay the current price/purchases price of the book as fine.</label>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label style="font-size: medium">4. Bags, Food items are not allowed in Library.</label>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label style="font-size: medium">
                                        5. Library books are for the use and benefit of not only the present but  also future members of the library. Therefore, all library books should be handled   with due care and 
                                        consideration. Member should not use markers, pen, pencil or disfigure the books in any way.
                                    </label>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label style="font-size: medium">6. Members should satisfy themselves about the physical condition of the books they wish to borrow before getting them issued; otherwise they will be held responsible for any damage or multilation noticed at the time of return.</label>

                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label style="font-size: medium">7. You are advised to take due care of electronic gadgets, jewellery etc, including cash and other valuables. The library shall not be responsible for any loss of theft in any way.</label>

                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label style="font-size: medium">8. All items taken into and out of the library are subject to security check.</label>

                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label style="font-size: medium">9. All users are requested to maintain silence in the library. Smoking, eating, sleeping and using mobile phones etc. are strictly prohibited in the library premises. Users are expected to behave decently and maintain decorum.</label>

                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label style="font-size: medium">10. Faculty/Staff Members can suggest standard books/journals for procurement in the library.</label>

                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label style="font-size: medium">
                                        <asp:CheckBox ID="chkaccept" runat="server" />I have read the Library Rules and agree to abide by them and shall obtain "Clearance Certificate" from the Library at the time of transfer/leaving the University.</label>

                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12" style="text-align: left">
                                    <label style="font-size: medium">Date:__________________.</label>

                                </div>


                            </div>
                            <div class="form-group">
                                <div class="col-md-12" style="text-align: right">
                                    <label style="font-size: medium">Signature of the Applicant</label>

                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
                            <table style="width: 1100px">
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Button ID="Button1" OnClientClick="javascript:CallPrint('bill')" Visible="false" runat="server" Width="10%" Style="margin-top: 5px;" Text="Print" Font-Bold="true" BorderColor="WhiteSmoke" />
                                    </td>
                                    <td style="text-align: center">&nbsp;
                                        <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" Enabled="true" Height="30px" Width="100px" BackColor="#ff9900" />
                                    </td>
                                    <%--<div style="text-align: right; width: 100%; margin-bottom: -30px;">
                         <asp:Button ID="BtnPrint" OnClientClick="PrintDiv();" runat="server" Width="10%" Style="margin-top: 5px;" Text="Print" Font-Bold="true" BorderColor="WhiteSmoke" />
                     </div>--%>

                                    <div class="form-group">
                                        <div class="col-md-2" style="visibility: hidden">
                                            <label style="width: 200px">
                                                <asp:TextBox ID="txthod" runat="server"></asp:TextBox></label>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-2" style="width: 200px">
                                                <label style="width: 200px"></label>
                                            </div>
                                            <div class="col-md-4" style="visibility: hidden">
                                                <asp:TextBox ID="txtID" runat="server" CssClass="form-control"></asp:TextBox>

                                            </div>

                                        </div>
                                    </div>
                                </tr>
                            </table>
</fieldset>

    </div>
   
               

    

    <br />
    <br />

</asp:Content>

