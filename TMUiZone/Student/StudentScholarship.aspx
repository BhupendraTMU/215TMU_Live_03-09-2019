<%@ Page Title="" Language="C#" MasterPageFile="~/Student/IndexMaster.master" AutoEventWireup="true" CodeFile="StudentScholarship.aspx.cs" Inherits="Student_StudentScholarship" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <link rel="stylesheet" href='http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css' media="screen" />
<script type="text/javascript" src='http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
<script type="text/javascript" src='http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js'></script>
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript">

        function txtHSpercentage() {

            var Sub1 = 0, Sub2 = 0, Sub3 = 0, Sub4 = 0, Sub5 = 0, Sub6 = 0;


            if (document.getElementById("ContentPlaceHolder1_txtobtsubject1").value == "") {
                Sub1 = 0;
            }
            else {
                Sub1 = document.getElementById("ContentPlaceHolder1_txtobtsubject1").value;
            }

            if (document.getElementById("ContentPlaceHolder1_txtobtsubject2").value == "") {
                Sub2 = 0;
            }
            else {
                Sub2 = document.getElementById("ContentPlaceHolder1_txtobtsubject2").value;
            }

            if (document.getElementById("ContentPlaceHolder1_txtobtsubject3").value == "") {
                Sub3 = 0;
            }
            else {
                Sub3 = document.getElementById("ContentPlaceHolder1_txtobtsubject3").value;
            }

            if (document.getElementById("ContentPlaceHolder1_txtobtsubject4").value == "") {
                Sub4 = 0;
            }
            else {
                Sub4 = document.getElementById("ContentPlaceHolder1_txtobtsubject4").value;
            }

            if (document.getElementById("ContentPlaceHolder1_txtobtsubject5").value == "") {
                Sub5 = 0;
            }
            else {
                Sub5 = document.getElementById("ContentPlaceHolder1_txtobtsubject5").value;
            }
            if (document.getElementById("ContentPlaceHolder1_txtobtsubject6").value == "") {
                Sub6 = 0;
            }
            else {
                Sub6 = document.getElementById("ContentPlaceHolder1_txtobtsubject6").value;
            }
            document.getElementById("ContentPlaceHolder1_txtobtainedmarks").value = parseFloat(Sub1) + parseFloat(Sub2) + parseFloat(Sub3) + parseFloat(Sub4) + parseFloat(Sub5) + parseFloat(Sub6);

            var Sub7 = 0, Sub8 = 0, Sub9 = 0, Sub10 = 0, Sub11 = 0, Sub12 = 0;


            if (document.getElementById("ContentPlaceHolder1_txtmaxsubject1").value == "") {
                Sub7 = 0;
            }
            else {
                Sub7 = document.getElementById("ContentPlaceHolder1_txtmaxsubject1").value;
            }

            if (document.getElementById("ContentPlaceHolder1_txtmaxsubject2").value == "") {
                Sub8 = 0;
            }
            else {
                Sub8 = document.getElementById("ContentPlaceHolder1_txtmaxsubject2").value;
            }

            if (document.getElementById("ContentPlaceHolder1_txtmaxsubject3").value == "") {
                Sub9 = 0;
            }
            else {
                Sub9 = document.getElementById("ContentPlaceHolder1_txtmaxsubject3").value;
            }

            if (document.getElementById("ContentPlaceHolder1_txtmaxsubject4").value == "") {
                Sub10 = 0;
            }
            else {
                Sub10 = document.getElementById("ContentPlaceHolder1_txtmaxsubject4").value;
            }

            if (document.getElementById("ContentPlaceHolder1_txtmaxsubject5").value == "") {
                Sub11 = 0;
            }
            else {
                Sub11 = document.getElementById("ContentPlaceHolder1_txtmaxsubject5").value;
            }
            if (document.getElementById("ContentPlaceHolder1_txtmaxsubject6").value == "") {
                Sub12 = 0;
            }
            else {
                Sub12 = document.getElementById("ContentPlaceHolder1_txtmaxsubject6").value;
            }
            document.getElementById("ContentPlaceHolder1_txttotalmarks").value = parseFloat(Sub7) + parseFloat(Sub8) + parseFloat(Sub9) + parseFloat(Sub10) + parseFloat(Sub11) + parseFloat(Sub12);

            D1 = $('[id$=txtobtainedmarks]').val();
            D2 = $('[id$=txttotalmarks]').val();
            D3 = (D1 / D2) * 100;
            document.getElementById("ContentPlaceHolder1_txtpercent").value = (D3.toFixed(2));



            ch1 = $('[id$=txtobtsubject1]').val();
            ch2 = $('[id$=txtmaxsubject1]').val();
            ch3 = (ch1 / ch2) * 100;
            document.getElementById("ContentPlaceHolder1_txtpersubject1").value = (ch3.toFixed(2));

            ch4 = $('[id$=txtobtsubject2]').val();
            ch5 = $('[id$=txtmaxsubject2]').val();
            ch6 = (ch4 / ch5) * 100;
            document.getElementById("ContentPlaceHolder1_txtpersubject2").value = (ch6.toFixed(2));

            ch7 = $('[id$=txtobtsubject3]').val();
            ch8 = $('[id$=txtmaxsubject3]').val();
            ch9 = (ch7 / ch8) * 100;
            document.getElementById("ContentPlaceHolder1_txtpersubject3").value = (ch9.toFixed(2));

            ch10 = $('[id$=txtobtsubject4]').val();
            ch11 = $('[id$=txtmaxsubject4]').val();
            ch12 = (ch10 / ch11) * 100;
            document.getElementById("ContentPlaceHolder1_txtpersubject4").value = (ch12.toFixed(2));

            ch13 = $('[id$=txtobtsubject5]').val();
            ch14 = $('[id$=txtmaxsubject5]').val();
            ch15 = (ch13 / ch14) * 100;
            document.getElementById("ContentPlaceHolder1_txtpersubject5").value = (ch15.toFixed(2));

            ch16 = $('[id$=txtobtsubject6]').val();
            ch17 = $('[id$=txtmaxsubject6]').val();
            ch18 = (ch16 / ch17) * 100;
            document.getElementById("ContentPlaceHolder1_txtpersubject6").value = (ch18.toFixed(2));
        }
    </script>
    <script type="text/javascript">
        function numeric(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && ((charCode >= 48 && charCode <= 57) || charCode == 46))
                return true;
            else {
                alert('Please Enter Number Only .');
                return false;
            }
        }
    </script>
      <style  type="text/css">
          .required::after {
              content: "*";
              font-weight: bold;
              color: red;
          }
      </style>
  

     <script language="javascript" type="text/javascript">

         function onlyNumbers(event) {
             var charCode = (event.which) ? event.which : event.keyCode
             if (charCode > 31 && (charCode < 48 || charCode > 57))
                 return false;
             return true;
             alert("Please Enter Numeric Value")
         }




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

    <script type="text/javascript">
        function MutExChkList(chk) {
            var chkList = chk.parentNode.parentNode.parentNode;
            var chks = chkList.getElementsByTagName("input");
            for (var i = 0; i < chks.length; i++) {
                if (chks[i] != chk && chk.checked) {
                    chks[i].checked = false;
                }
            }
        }

</script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
   <br />
    <fieldset class="boxBody" style="text-align: left;">
        <marquee>
        <asp:Label ID="Label3" runat="server" Text=" \* Student can apply only one type of scholarship. विद्यार्थी केवल एक ही प्रकार की छात्रवृत्ति के लिए आवेदन कर सकता है। */" Font-Size="15pt" ForeColor="Red" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
        </marquee>
    </fieldset>
    <fieldset class="boxBody" style="text-align: left;">
        <asp:Label ID="lblPrincipal" runat="server" ForeColor="Red"  Font-Size="Medium" Text="Principal:"></asp:Label>
        <asp:Label ID="lblhodstatus1" runat="server" ForeColor="Red"  Font-Size="Medium" Text=""></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label13" runat="server" ForeColor="Red" Font-Size="Medium"  Text="Addmission Director:"></asp:Label>
        <asp:Label ID="lblAddmissionDirector" runat="server" ForeColor="Red" Font-Size="Medium" Text=""></asp:Label>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblenrollment" runat="server" ForeColor="Red" Font-Size="Medium"  Text="Registrar:"></asp:Label>
        <asp:Label ID="lblregistrar" runat="server" ForeColor="Red" Font-Size="Medium" Text=""></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label14" runat="server" ForeColor="Red" Font-Size="Medium"  Text="Account Dept:"></asp:Label>
        <asp:Label ID="lblAccountdept" runat="server" ForeColor="Red" Font-Size="Medium" Text=""></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Size="Medium"  Text="Rejection Reasons:"></asp:Label>
        <asp:Label ID="lblRejectionReasons" runat="server" ForeColor="Red" Font-Size="Medium" Text=""></asp:Label>

    </fieldset>
   
    <fieldset class="boxBody">     
        <div  style="width: 100%; margin-bottom: 10px; margin-left: 1%; margin-right: 1%; margin-top: 5px;">

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
            </table>
        </div>
    </fieldset>
    <fieldset class="boxBody" style="text-align: center; border-color: black; background-color: black;">
        <asp:Label ID="Label1" runat="server" Text="Scholarship Declaration Form Session(2023-2024)" Font-Size="15pt" ForeColor="White" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
    <div id="divGeneralBodyenrollmentform">
        <fieldset id="Print" class="boxBodyInner" >
            <div class="form-horizontal">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                              <div class="form-group">
                                  <div class="col-md-1">
                                   
                                </div>
                                <div class="col-md-2">
                                    <label style="width: 200px; font-size:medium">Student's Name</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtstudentname" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>

                                <div class="col-md-2">
                                    <label style="width: 200px; font-size:medium">Father's Name</label>
                                </div>
                                <div class="col-md-3">
                                      <asp:TextBox ID="txtfathername" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                  </div>  
                              <div class="form-group">
                                   <div class="col-md-1">
                                   
                                </div>
                                <div class="col-md-2">
                                    <label style="width: 200px;font-size:medium">Student No.</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtStudentNo" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>

                                <div class="col-md-2">
                                    <label style="width: 200px; font-size:medium">Programme</label>
                                </div>
                                <div class="col-md-3">
                                      <asp:TextBox ID="txtprogramme" runat="server" BorderColor="Black" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                  </div>
                              <%--<div class="form-group">
                                     <div class="col-md-1">
                                   
                                    </div>
                                  <div class="col-md-3">
                                       <label style="width: 200px; font-size:large; font:bold">Nature of Scholarship</label>
                                      </div>
                                  </div> --%>                               
                            </div>
                        </div>
                    </div>
                </div>
             <%--<asp:UpdatePanel ID="pnlpic" runat="server">
                       <ContentTemplate>--%>
             <fieldset class="boxBody" style="text-align: left;"">
                 <div class="form-group">
                                  <div class="col-md-3">
                                     <asp:Label ID="Label5" runat="server" Text=" Nature of Scholarship" Font-Size="15pt" ToolTip="Tick The Applicable"   ForeColor="Black" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                                </div>
               <%-- <label style="width: 250px; font:bold; color:red">(Tick as Applicable)</label>--%>
                    
                  <div class="col-md-4">
                 <asp:DropDownList ID="drpnatureofscholarship" runat="server"  OnSelectedIndexChanged="drpnatureofscholarship_SelectedIndexChanged" AutoPostBack="true" BorderColor="Black"  CssClass="form-control">
                 </asp:DropDownList>
                   </div>
                  <div class="col-md-4">
                      <asp:TextBox ID="txttypeofexam" runat="server" BorderColor="Black" placeholder="Exam Scholarship is claimed" Visible="false" style='text-transform:uppercase'  CssClass="form-control"></asp:TextBox>
                      </div>

                     </div>
             </fieldset>
            <div id="maxmarks" runat="server" visible="false">
            <fieldset class="boxBodyInner">
            <div class="form-horizontal">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                             <div class="form-group">
                                 <div class="col-md-2">
                                   
                                </div>
                                    <div class="col-md-2">
                                   <label style="font-size:medium; font:bold" class="required">Subject</label>
                                </div>

                                  <div class="col-md-2">
                                   <label style="font-size:medium; font:bold" class="required">Obtained Marks</label>
                                </div>
                                 <div class="col-md-2">
                                   <label style="font-size:medium; font:bold" class="required">Max Marks</label>
                                </div>
                                 <div class="col-md-2">
                                   <label style="font-size:medium; font:bold" class="required">Percentage (%)</label>
                                </div>
                                 </div>
                                  

                             <div class="form-group">
                                 <div class="col-md-2">
                                   
                                </div>
                                    <div class="col-md-2">
                                  <asp:TextBox ID="txtsubject1" runat="server" BorderColor="Black" style='text-transform:uppercase' class="required" CssClass="form-control"></asp:TextBox>
                                </div>

                                  <div class="col-md-2">
                                  <asp:TextBox ID="txtobtsubject1" runat="server" BorderColor="Black" onchange="txtHSpercentage()" onkeypress="return numeric(event)"   CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="txtmaxsubject1" runat="server" BorderColor="Black" onchange="txtHSpercentage()" onkeypress="return numeric(event)" CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="txtpersubject1" runat="server" BorderColor="Black"  CssClass="form-control"></asp:TextBox>
                                </div>
                                 </div>

                              <div class="form-group">
                                 <div class="col-md-2">
                                   
                                </div>
                                    <div class="col-md-2">
                                  <asp:TextBox ID="txtsubject2" runat="server" BorderColor="Black" style='text-transform:uppercase'  CssClass="form-control"></asp:TextBox>
                                </div>

                                  <div class="col-md-2">
                                  <asp:TextBox ID="txtobtsubject2" runat="server" BorderColor="Black" onchange="txtHSpercentage()" onkeypress="return numeric(event)"  CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="txtmaxsubject2" runat="server" BorderColor="Black" onchange="txtHSpercentage()" onkeypress="return numeric(event)" CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="txtpersubject2" runat="server" BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                </div>
                                 </div>

                              <div class="form-group">
                                 <div class="col-md-2">
                                   
                                </div>
                                    <div class="col-md-2">
                                  <asp:TextBox ID="txtsubject3" runat="server" BorderColor="Black" style='text-transform:uppercase'  CssClass="form-control"></asp:TextBox>
                                </div>

                                  <div class="col-md-2">
                                  <asp:TextBox ID="txtobtsubject3" runat="server" BorderColor="Black" onchange="txtHSpercentage()" onkeypress="return numeric(event)"   CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="txtmaxsubject3" runat="server" BorderColor="Black" onchange="txtHSpercentage()" onkeypress="return numeric(event)"   CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="txtpersubject3" runat="server" BorderColor="Black"  CssClass="form-control"></asp:TextBox>
                                </div>
                                 </div>
                              
                              <div class="form-group">
                                 <div class="col-md-2">
                                   
                                </div>
                                    <div class="col-md-2">
                                  <asp:TextBox ID="txtsubject4" runat="server" BorderColor="Black" style='text-transform:uppercase'  CssClass="form-control"></asp:TextBox>
                                </div>

                                  <div class="col-md-2">
                                  <asp:TextBox ID="txtobtsubject4" runat="server" BorderColor="Black" onchange="txtHSpercentage()"  onkeypress="return numeric(event)"  CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="txtmaxsubject4" runat="server" BorderColor="Black" onchange="txtHSpercentage()"  onkeypress="return numeric(event)"   CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="txtpersubject4" runat="server" BorderColor="Black"  CssClass="form-control"></asp:TextBox>
                                </div>
                                 </div>

                             <div id="subject5" class="form-group">
                                 <div class="col-md-2">
                                   
                                </div>
                                    <div  class="col-md-2">
                                  <asp:TextBox ID="txtsubject5" runat="server" BorderColor="Black" style='text-transform:uppercase'  CssClass="form-control"></asp:TextBox>
                                </div>

                                  <div class="col-md-2">
                                  <asp:TextBox ID="txtobtsubject5" runat="server" BorderColor="Black"   onchange="txtHSpercentage()" onkeypress="return numeric(event)" CssClass="form-control"></asp:TextBox>
                                    
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="txtmaxsubject5" runat="server" BorderColor="Black"   onchange="txtHSpercentage()"  onkeypress="return numeric(event)" CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="txtpersubject5" runat="server" BorderColor="Black"  CssClass="form-control"></asp:TextBox>
                                </div>
                                 </div>

                               <div id="subject6" class="form-group" runat="server" style="visibility:hidden">
                                 <div class="col-md-2">
                                   
                                </div>
                                    <div class="col-md-2">
                                  <asp:TextBox ID="txtsubject6" runat="server" BorderColor="Black" style='text-transform:uppercase' CssClass="form-control"></asp:TextBox>
                                </div>

                                  <div class="col-md-2">
                                  <asp:TextBox ID="txtobtsubject6" runat="server"  BorderColor="Black"  onchange="txtHSpercentage()" onkeypress="return numeric(event)" CssClass="form-control">
                                      
                                  </asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="txtmaxsubject6" runat="server" BorderColor="Black"   onchange="txtHSpercentage()"  onkeypress="return numeric(event)" CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="txtpersubject6" runat="server" BorderColor="Black"  CssClass="form-control"></asp:TextBox>
                                </div>
                                 </div>

                             <div class="form-group">
                                 <div class="col-md-2">
                                   
                                </div>
                                    <div class="col-md-2">
                                 
                                </div>

                                  <div class="col-md-2">
                                  <asp:TextBox ID="txtobtainedmarks" runat="server" BorderColor="Black"   Enabled="false"  CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="txttotalmarks" runat="server" BorderColor="Black" Enabled="false"  CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="txtpercent" runat="server" BorderColor="Black" CssClass="form-control" Enabled="false" AutoPostBack="true"></asp:TextBox>
                                </div>
                                 </div>
                            <div class="form-group">  
                                           <div class="col-md-6">                                   
                                </div>                            
                             <div class="col-md-2">
                          <asp:Button ID="btn_Changesubject" runat="server" Text="Four Subject Selected by Faculty" Visible="false" OnClick="btn_Changesubject_Click"  Height="30px"  CssClass="form-control" BackColor="#ff9933" ForeColor="White" />
                                        </div>
                                       </div>
                            </div>
                        </div>
                    </div>
                </div>
                </fieldset>
                </div>
            <div id="Mainfoursubject" runat="server" visible="false">
            <fieldset class="boxBodyInner">
            <div class="form-horizontal">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                             <div class="form-group">
                                 <div class="col-md-3">
                                   
                                </div>
                                    <div class="col-md-2">
                                   <label style="font-size:medium; font:bold" class="required">Subject</label>
                                </div>

                                  <div class="col-md-2">
                                   <label style="font-size:medium; font:bold" class="required">Obtained Marks</label>
                                </div>
                                 <div class="col-md-2">
                                   <label style="font-size:medium; font:bold" class="required">Max Marks</label>
                                </div>
                             
                                 </div>
                                  

                             <div class="form-group">
                                 <div class="col-md-3">
                                   
                                </div>
                                    <div class="col-md-2">
                                  <asp:TextBox ID="TextBox1" runat="server" BorderColor="Black" style='text-transform:uppercase' Enabled="false" class="required" CssClass="form-control"></asp:TextBox>
                                </div>

                                  <div class="col-md-2">
                                  <asp:TextBox ID="TextBox2" runat="server" BorderColor="Black" onchange="txtHSpercentage()" Enabled="false" onkeypress="return numeric(event)"   CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="TextBox3" runat="server" BorderColor="Black" onchange="txtHSpercentage()" Enabled="false" onkeypress="return numeric(event)" CssClass="form-control"></asp:TextBox>
                                </div>
                                
                                 </div>

                              <div class="form-group">
                                 <div class="col-md-3">
                                   
                                </div>
                                    <div class="col-md-2">
                                  <asp:TextBox ID="TextBox5" runat="server" BorderColor="Black" style='text-transform:uppercase' Enabled="false"  CssClass="form-control"></asp:TextBox>
                                </div>

                                  <div class="col-md-2">
                                  <asp:TextBox ID="TextBox6" runat="server" BorderColor="Black" onchange="txtHSpercentage()" Enabled="false" onkeypress="return numeric(event)"  CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="TextBox7" runat="server" BorderColor="Black" onchange="txtHSpercentage()" Enabled="false" onkeypress="return numeric(event)" CssClass="form-control"></asp:TextBox>
                                </div>
                                
                                 </div>

                              <div class="form-group">
                                 <div class="col-md-3">
                                   
                                </div>
                                    <div class="col-md-2">
                                  <asp:TextBox ID="TextBox9" runat="server" BorderColor="Black" style='text-transform:uppercase' Enabled="false"  CssClass="form-control"></asp:TextBox>
                                </div>

                                  <div class="col-md-2">
                                  <asp:TextBox ID="TextBox10" runat="server" BorderColor="Black" onchange="txtHSpercentage()" Enabled="false" onkeypress="return numeric(event)"   CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="TextBox11" runat="server" BorderColor="Black" onchange="txtHSpercentage()" Enabled="false" onkeypress="return numeric(event)"   CssClass="form-control"></asp:TextBox>
                                </div>
                             
                                 </div>
                               <div class="form-group">
                                 <div class="col-md-3">
                                   
                                </div>
                                    <div class="col-md-2">
                                  <asp:TextBox ID="TextBox13" runat="server" BorderColor="Black" style='text-transform:uppercase' Enabled="false"  CssClass="form-control"></asp:TextBox>
                                </div>

                                  <div class="col-md-2">
                                  <asp:TextBox ID="TextBox14" runat="server" BorderColor="Black" onchange="txtHSpercentage()" Enabled="false" onkeypress="return numeric(event)"   CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <asp:TextBox ID="TextBox15" runat="server" BorderColor="Black" onchange="txtHSpercentage()" Enabled="false" onkeypress="return numeric(event)"   CssClass="form-control"></asp:TextBox>
                                </div>
                              
                                 </div>

                            <div class="form-group">
                                 <div class="col-md-3">                                  
                                </div>
                                    <div class="col-md-2">
                                   <label style="font-size:medium; font:bold">Obtained Marks</label>
                                </div>
                                  <div class="col-md-2">
                                   <label style="font-size:medium; font:bold">Total Marks</label>
                                </div>
                                 <div class="col-md-2">
                                   <label style="font-size:medium; font:bold" class="required">Percentage</label>
                                </div>                             
                                 </div>
                             <div class="form-group">
                                 <div class="col-md-3">
                                   
                                </div>
                                    <div class="col-md-2">
                                 <asp:TextBox ID="TextBox4" runat="server" BorderColor="Black" onchange="txtHSpercentage()" Enabled="false" onkeypress="return numeric(event)"   CssClass="form-control"></asp:TextBox>
                                </div>

                                  <div class="col-md-2">
                                   <asp:TextBox ID="TextBox8" runat="server" BorderColor="Black" onchange="txtHSpercentage()" Enabled="false" onkeypress="return numeric(event)"   CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                  <asp:TextBox ID="TextBox12" runat="server" BorderColor="Black" onchange="txtHSpercentage()" Enabled="false" onkeypress="return numeric(event)"   CssClass="form-control"></asp:TextBox>
                                </div>
                             
                                 </div>                          
                              </div>
                        </div>
                    </div>
                </div>
                </fieldset>
                </div>   

            <div id="divchancellorscholarship" runat="server" visible="false">
            <fieldset class="boxBodyInner">
            <div class="form-horizontal">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-16">
                             <div class="form-group">   
                                 <div class="col-md-1">
                                   
                                </div>         
                                  <div class="col-md-2">
                                   <label style="font-size:medium; font:bold" class="required">Employee Code:</label>
                                </div>
                                 <div class="col-md-3">
                                  <asp:DropDownlist ID="drpemployeelist" runat="server" BorderColor="Black" OnSelectedIndexChanged="drpemployeelist_SelectedIndexChanged" AutoPostBack="true"  style='text-transform:uppercase' CssClass="form-control">

                                  </asp:DropDownlist>
                                </div>                  
                                    <div class="col-md-2">
                                   <label style="font-size:medium; font:bold" class="required">Employee Name:</label>
                                </div>

                                  <div class="col-md-3">
                                      <asp:TextBox ID="txtemployeename" runat="server" BorderColor="Black" style='text-transform:uppercase' Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                
                                 </div>
                              <div class="form-group">
                                   <div class="col-md-1">
                                   
                                </div>
                                   <div class="col-md-2">
                                   <label style="font-size:medium; font:bold" class="required">Designation:</label>
                                </div>
                                 <div class="col-md-3">
                                  <asp:TextBox ID="txtdesignation" runat="server" BorderColor="Black" style='text-transform:uppercase' Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                   <div class="col-md-2">
                                   <label style="font-size:medium; font:bold" class="required">Aadhar No:</label>
                                </div>
                                 <div class="col-md-3">
                                  <asp:TextBox ID="txtaadharno" runat="server" BorderColor="Black" style='text-transform:uppercase' Enabled="false" onkeypress="return numeric(event)" MaxLength="12" MinLength="12" CssClass="form-control"></asp:TextBox>
                                </div>
                                 </div>                            

                            </div>
                        </div>
                    </div>
                </div>
                </fieldset>
                </div>

             <div id="divjainscholarship" runat="server" visible="false">
            &nbsp;<fieldset class="boxBodyInner">
            <div class="form-horizontal">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                             <div class="form-group">   
                                 <div class="col-md-1">
                                   
                                </div>                           
                                    <div class="col-md-2">
                                   <label style="font-size:medium ;width: 200px; font:bold">Student Name:</label>
                                </div>

                                  <div class="col-md-2">
                                      <asp:TextBox ID="txtstudentnamejain" runat="server" BorderColor="Black" style='text-transform:uppercase' Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                   <div class="col-md-2">
                                   <label id="lblreligion" style="font-size:medium; font:bold" runat="server" visible="false">Religion</label>
                                </div>
                                 <div class="col-md-2">
                                    <asp:TextBox ID="txtreligion1" runat="server" BorderColor="Black" style='text-transform:uppercase' Enabled="false" visible="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                   <label id="lbljaincertificate" style="font-size:medium; font:bold" runat="server" visible="false">Jain Certificate:</label>
                                </div>
                                 <div class="col-md-2">
                                  
                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" Visible="false" Width="200px" />
                                &nbsp;<asp:LinkButton ID="lnkCertificate" runat="server" Enabled='<%# (Eval("[Jain Certificate]").ToString() ==  "" ?  false : true) %>' Text="View Certificate" OnClick="lnkCertificate_Click" Visible="false" Font-Size="Larger" Font-Bold="true"  />
                               &nbsp;
                                </div>                                                                 
                                 </div>  
                              <div class="col-md-2">
                               </div>                            
                                 
                                 <div class="form-group">   
                                  <div class="col-md-1">                                  
                                     <asp:TextBox ID="txtreligion" runat="server" BorderColor="Black" style='text-transform:uppercase' Visible="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                 </div>
                             
                            

                            </div>
                        </div>
                    </div>
                </div>
                </fieldset>
                </div>


                             

                <div id="divGeneralBodyScholarshiptform">
        <fieldset class="boxBodyInner">
            <div class="form-horizontal">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                           
                             <div class="form-group">
                                  
                                   <div class="col-md-1">
                                   
                                </div>
                                     <div class="col-md-2">
                                   <label style="font-size:medium; font:bold">Applicable Scholarship</label>
                                    </div>
                                 <div class="col-md-2">
                                    <asp:TextBox ID="txtapplicablescholarship" runat="server" Enabled="false"   BorderColor="Black" CssClass="form-control">
                                                                            
                                    </asp:TextBox>
                                    </div>
                                 <div class="col-md-2"  style="text-align:center">
                                   <label id="lblfee" runat="server" style="font-size:medium; font:bold">Yearly Tuition Fee</label>
                                    </div>
                                  <div class="col-md-2">
                                    <asp:TextBox ID="txtfee" runat="server"  BorderColor="Black"  onchange="txtHSpercentage1()" Enabled="false"   onkeypress="return numeric(event)"  CssClass="form-control"></asp:TextBox>
                                    </div>
                                  <div class="col-md-1" style="text-align:right">
                                   <label id="lbldis" runat="server" style="font-size:medium; font:bold">Dis.Per(%)</label>
                                    </div>
                                  <div class="col-md-2">
                                    <asp:TextBox ID="txtdiscountper" runat="server" onchange="txtHSpercentage1()" onkeypress="return numeric(event)" Enabled="false"  BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                    </div>

                        </div>
                              <div class="form-group">
                                    <div class="col-md-1">
                                   
                                </div>
                                     <div class="col-md-2">
                                   <label id="lblscamount" runat="server" style="width: 600px; font-size:medium; font:bold">Scholarship Amount</label>
                                    </div>
                                 <div class="col-md-2">
                                    <asp:TextBox ID="txtscholarshipamount" runat="server" onkeypress="return numeric(event)"  Enabled="false"   BorderColor="Black" CssClass="form-control"></asp:TextBox>
                                    </div>
                        </div>
                          
                         
                                </div>
                                     <div class="col-md-11">
                                   <label style=" font-size:medium; font:bold">Undertaking from Student/parents I/We understand that scholarship amount granted allowed hereby is provisional and is applicable for the session 2023-2024
                                       only.If any discrepancy is found later on,due to which the amount of scholarship availed gets changed, the amount of scholarship so changed after taking into consideration the discrepancy, shall be acceptable
                                       to me as final and binding.
                                   </label>
                                    </div>
                                 
                                                                                     </div>
                                                                                       <div class="form-group">
                                                                                            <div class="col-md-1">
                                                                                                
                                                                                            </div>
                                                                                            <div class="col-md-2">
                                                                                                <label style="width: 200px; font-size:large">Upload Document</label>
                                                                                            </div>
                                                                                            <div class="col-md-2">
                                                                                                <asp:UpdatePanel ID="updatePanel5" runat="server">
                                                                                                    <ContentTemplate>
                                                                                                        <asp:DropDownList ID="drpuploaddpocument" runat="server" AutoPostBack="true" CssClass="form-control">
                                                                                                            <asp:ListItem Text="---SELECT---" Value="0"></asp:ListItem>
                                                                                                            <asp:ListItem Text="Pre Qualification Marksheet" Value="1"></asp:ListItem>
                                                                                                           
                                                                                                        </asp:DropDownList>
                                                                                                    </ContentTemplate>
                                                                                                </asp:UpdatePanel>
                                                                                            </div>
                                                                                            <div class="col-md-2">
                                                                                                <%-- <div class="input-group" >--%>
                                                                                                <%--<span class="input-group-addon"><span class="glyphicon glyphicon-registration-mark" ></span></span>--%>
                                                                                                <asp:FileUpload ID="FileUpload2" runat="server" />

                                                                                            </div>
                                                                                          
                                                                                            <div class="col-md-2">
                                                                                            </div>
                                                                                              <div class="col-md-1">
                                                                                                 <asp:LinkButton ID="lblTran" runat="server" Enabled='<%# (Eval("Pre_Education").ToString() ==  "" ?  false : true) %>' Text="View" Font-Size="Larger" Font-Bold="true" OnClick="lblTran_Click" />
                                                                                            </div>
                                                                                           <div class="col-md-3" style="visibility:hidden">
                                    <asp:TextBox ID="txtid" runat="server" BorderColor="Black" Enabled="false"  CssClass="form-control"></asp:TextBox>
                                </div>
                                                                                           </div>
                         <br />
                            
                              <div class="form-group">
                       <div class="col-md-5">
                      </div>
                                                      
                <div class="col-md-1">
                    <asp:Button ID="btn_Save" runat="server" Text="Save"  OnClick="btn_Save_Click" CssClass="form-control" BackColor="#ff9933" ForeColor="White" Width="80px" Height="30px" />
                    </div>
                                <%--  <div class="col-md-1">
                    <asp:Button ID="btn_Print" runat="server" Text="Print" CssClass="form-control" OnClientClick="javascript:CallPrint('Print')" BackColor="#ff9933" ForeColor="White" Width="80px" Height="30px" />
                    </div>--%>
                                  <div class="col-md-1">
                    <asp:Button ID="btn_reset" runat="server" Text="Reset"  OnClick="btn_reset_Click" CssClass="form-control" BackColor="#ff9933" ForeColor="White" Width="80px" Height="30px" />
                    </div>

                                     <div class="col-md-2">
                                    <asp:TextBox ID="TextBox16" runat="server" BorderColor="Black" style='text-transform:uppercase' Enabled="false" visible="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                  </div>
                            
                </div>
                        
            </div>
                    </div>
                </div>
            </fieldset>
                    </div>
            </fieldset>

       
        </div>
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
    <br />
    <br />
    <br />
    
</asp:Content>

