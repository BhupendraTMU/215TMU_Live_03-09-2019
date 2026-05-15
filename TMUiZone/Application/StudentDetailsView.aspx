<%@ Page Title="" Language="C#" MasterPageFile="~/Application/IndexMaster.master" AutoEventWireup="true" CodeFile="StudentDetailsView.aspx.cs" Inherits="Alumni_StudentDetailsView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <style type="text/css">
        #confirmModal.modal-dialog.modalPopup {
            width: 80%;
        }

        table thead tr th:first-child, .table > tbody > tr > th:first-child {
            border-left: 1px solid #60594f;
            padding: 5px 8px;
        }

        .modal-dialog.modalPopup {
            -webkit-transform: translate(0,0);
            -ms-transform: translate(0,0);
            -o-transform: translate(0,0);
            transform: translate(0,0);
            position: absolute;
            left: 50%;
            top: 50%;
            transform: translate(-50%,-50%) !important;
            margin: 0;
        }

        .modalPopup .modal-header {
            padding: 8px 15px;
            border-bottom: 1px solid #e5e5e5;
            background-color: #2FBDF1;
            -webkit-border-top-left-radius: 4px;
            -webkit-border-top-right-radius: 4px;
            -moz-border-radius-topleft: 4px;
            -moz-border-radius-topright: 4px;
            border-top-left-radius: 6px;
            border-top-right-radius: 6px;
        }

            .modalPopup .modal-header b {
                color: #fff;
            }

        #ContentPlaceHolder1_updatepnl table td {
            padding: 5px 0px;
        }

        .auto-style1 {
            width: 104%;
        }

        .auto-style2 {
            width: 38px;
        }

        .auto-style3 {
            width: 22px;
        }

        .auto-style5 {
            width: 10px;
        }
    </style>
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
      <script type="text/javascript" src="https://www.google.com/jsapi">
    </script>
  <script src="https://www.google.com/jsapi" type="text/javascript">  
</script>
    <script type="text/javascript">
        google.load("elements", "1", {
            packages: "transliteration"
        });

        function onLoad() {
            var options = {
                sourceLanguage: google.elements.transliteration.LanguageCode.ENGLISH,
                destinationLanguage: [google.elements.transliteration.LanguageCode.HINDI],
                shortcutKey: 'ctrl+g',
                transliterationEnabled: true
            };

            var control = new google.elements.transliteration.TransliterationControl(options);
            control.makeTransliteratable(['TxtSH']);
            control.makeTransliteratable(['TxtFH']);
            control.makeTransliteratable(['TxtMH']);

        }
        google.setOnLoadCallback(onLoad);
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
    <%--     <script>
         function alphanumeric(inputtxt) {
             var letterNumber = /^[0-9a-zA-Z-+()!@#$%^&*/|]+$/;
             if ((inputtxt.value.match(letterNumber))) {
                 alert("Enter only Hindi");
                 return false;
             }
             else {


                 return true;
             }
         }
    </script>--%>

    <script type="text/javascript">
        $(document).ready(function () {
            $('[id$=txtAddress]').keypress(function (e) {
                if ($(this).val().length >= 50) {
                    e.preventDefault();
                }
            });
        });
    </script>
    <script type="text/javascript">
        function callFeedbackMessage(inputType, inputText) {

            if (inputType == 'Error') {
                alertify.error(inputText);
                return false;
            }
            else if (inputType == 'Success') {
                //alertify.confirm().set('overflow', false);
                alertify.success("Update Successfully");
                return false;
            }
            else {
                alertify.log(inputText, "", 10000);
                return false;
            }
        }
    </script>

    

    <%--<script>
        function CloseModalConfirm() {
            $('#confirmModal').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        };
    </script>--%>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>  



                            <asp:Panel ID="Panel2" runat="server" >
        <div id="confirmModal5" class="modal fade confirm-modal"  role="dialog">
            <div class="modal-dialog modalPopup" style="width:550px">
                <div class="modal-content" style="width:543px">
                   
                    <div class="clearfix" style="margin-bottom:10px;margin-top:10px;margin-left:20px">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                
                                 <table >
                                     
                                     <tr>
                                         <td style="height:45px">
                                             Student No_ 
                                         </td>
                                         <td style="width:5px">

                                         </td>
                                         <td>
                                             <asp:TextBox ID="txtstno" Enabled="false" Width="250px" runat="server"></asp:TextBox>
                                         </td>
                                         
                                     </tr>
                                     <tr>
                                                                                   <td>
    
 </td>
 <td style="width:5px">

 </td>
 <td>
    
 </td>
                                     </tr>
                                      <tr>
     <td style="height:45px">
         Student’s Name (As per 10th certificate)
     </td>
     <td style="width:3px">

     </td>
     <td style="margin-right:10px">
         <asp:TextBox ID="txtSTName10" Width="250px" Enabled="false" runat="server" ></asp:TextBox>
     </td>
 </tr>

                                     <tr>
    <td style="height:45px">
        Student’s Name (As per Aadhaar)
    </td>
    <td style="width:3px">

    </td>
    <td style="margin-right:10px">
        <asp:TextBox ID="txtSTNameA"  Width="250px" runat="server" ></asp:TextBox>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ErrorMessage="**" ValidationGroup="g1" ForeColor="Red" ControlToValidate="txtSTNameA"></asp:RequiredFieldValidator>
    </td>
</tr>

                                     <tr>
    <td style="height:45px">
        Date of birth (As per 10th certificate)
    </td>
    <td style="width:3px">

    </td>
    <td style="margin-right:10px">
        <asp:TextBox ID="txtDOB10" Enabled="false" Width="250px" runat="server" ></asp:TextBox>
    </td>
</tr>

                                     <tr>
    <td style="height:45px">
        ⁠Date of birth (As per Aadhaar)
    </td>
    <td style="width:3px">

    </td>
    <td style="margin-right:10px">
        <asp:TextBox ID="txtDOBA" Width="250px" runat="server" onkeypress="return false" onKeyDown="preventBackspace();"
                                            oncopy="return false;" onpaste="return false;" autocomplete="off" ></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" runat="server" ErrorMessage="**" ValidationGroup="g1" ForeColor="Red" ControlToValidate="txtDOBA"></asp:RequiredFieldValidator>
         <asp:CalendarExtender ID="CalendarExtender2" Format="dd MMM yyyy" runat="server"
     CssClass="cal_Theme1"  Enabled="true" TargetControlID="txtDOBA">
</asp:CalendarExtender>
    </td>
</tr>

                                     <tr>
    <td style="height:45px">
        Mobile number (linked with Aadhaar)
    </td>
    <td style="width:3px">

    </td>
    <td style="margin-right:10px">
        <asp:TextBox ID="txtMobileA" Width="250px" onkeypress="return isNumber(event)" MaxLength="10" runat="server" ></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" Display="Dynamic" runat="server" ErrorMessage="**" ValidationGroup="g1" ForeColor="Red" ControlToValidate="txtMobileA"></asp:RequiredFieldValidator>
    </td>
</tr>

                                     <tr>
    <td style="height:45px">
       Aadhar number
    </td>
    <td style="width:3px">

    </td>
    <td style="margin-right:10px">
        <asp:TextBox ID="txtAdhar" Width="250px" onkeypress="return isNumber(event)" MaxLength="12" runat="server" ></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" Display="Dynamic" runat="server" ErrorMessage="**" ValidationGroup="g1" ForeColor="Red" ControlToValidate="txtAdhar"></asp:RequiredFieldValidator>
    </td>
</tr>
                                    <tr>
                                        <td colspan="3" style="height:45px">
                                       <b style="color:red">The blank field must be filled in to proceed further.</b> 
                                        </td>
                                         <td style="width:3px">

</td>
                                    </tr>
                                     <tr>
    <td>
       
    </td>
    <td style="width:3px">

    </td>
    <td style="margin-right:10px;text-align:right">
        <asp:Button ID="btnSave" Width="150px" Text="SAVE"  ValidationGroup="g1" OnClick="btnSave_Click" runat="server" ></asp:Button>
    </td>
</tr>








                                 </table>
                            </ContentTemplate>

                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>






    <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>

    <asp:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
        PopupControlID="pnlPopup" TargetControlID="lnkDummy" BackgroundCssClass="modalBackground" CancelControlID="btnHide">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server" Height="70%" Width="65%" CssClass="modalPopup"  Style="display: none" ScrollBars="Both">
         
       <asp:UpdatePanel ID="pnl1" runat="server" >
                                        <ContentTemplate>
      <div class="header" style="text-align:center ; background-color:white" >
            <table cellpadding="0"  style="text-align:center">

                <tr>
                    <td style="width:500px">

                    </td>
                       
                    <td>
                     <asp:CheckBox ID="CheckBox4" OnCheckedChanged="CheckBox4_CheckedChanged" AutoPostBack="true" runat="server" />
                    </td>
                    <td>
                     <asp:Label ID="Label8" runat="server"  Text="English"></asp:Label>
                    </td>

                    <td style="width:20px"></td>
                    <td>
                     <asp:CheckBox ID="CheckBox5" OnCheckedChanged="CheckBox5_CheckedChanged" Visible="false" AutoPostBack="true" runat="server" />
                    </td>
       
                    <td>
                     <asp:Label ID="Label10" runat="server" Visible="false" Text="Hindi"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>                                     
        <div class="header" id="Notenglish" runat="server" visible="false" style="margin-top:-10px">
            <b>
                <asp:Label ID="lblNotification" runat="server" Font-Size="Large" Font-Bold="true" ForeColor="Black" Font-Underline="true" Text="IMPORTANT STEPS FOR DOCUMENT UPLOADING & PROFILE UPDATE"></asp:Label></b>
            <div class="close">
                    <asp:Button ID="btnHide" runat="server" BackColor="White" OnClick="btnHide_Click"  Font-Bold="true" ForeColor="Black"  Text="X" />
                </div>

        </div>

 <div id="divGeneralBodyEnglish" runat="server" visible="false">
 <fieldset class="boxBodyInner">
<div class="form-horizontal">
 <div class="box-body">
  <div class="row">
 <div class="col-md-12">                
<div class="form-group">
 <div class="col-md-12">
 <label style="font-size: medium; color:black" runat="server">
Student can login at portal: http://portal2.tmu.ac.in/Default.aspx by using his/her Student no. (Which is
mentioned in left upper corner of fee receipt as ST/0XXXXX) &amp; Use your date of birth as the password in the
format DDMMYY. For example, if your date of birth is 25th December 2005, your password will be 251205., kindly
go through the below mentioned steps for further actions: -
 </label>
  </div>
 </div>  
        <div class="form-group">
 <div class="col-md-12" style="font:bold;">
 <label style="font-size: medium; color:black"> 
    Step 1. Once student login with their St no., they have to correct their personal details if any correction
required along with the required field of name in Hindi, ABC ID number by clicking the provided link at
respective field (Student should have Digi locker account for the ABC ID) &amp; upload the ABC ID as pdf or
JPG and after doing so click UPDATE button.
 </label>
  </div>
 </div>
        
        <div class="form-group">
 <div class="col-md-12" style="font:bold;">
 <label style="font-size: medium ; color:black"> 
  Step 2. If student updated his/her details, then it will be proceeding for the approval from the Director
(Admissions).


 </label>
  </div>
 </div>
          
 <div class="form-group">
 <div class="col-md-12" style="font:bold;">
 <label style="font-size: medium;color:black"> 
    Step 3. To generate the virtual ID card, students initiate the process by verifying their parents&#39; mobile
number which mentioned on ERP portal through OTP. After verifying the parent’s mobile no., students
generate a separate OTP for their own mobile number. After entering this OTP, students gain access to
download the virtual ID card, which holds significance for future reference and use.
 </label>
  </div>
 </div>
     
        <div class="form-group">
 <div class="col-md-12" style="font:bold;">
 <label style="font-size: medium ; color:black"> 
   Step 4. Anti-ragging undertaking will be downloaded through provided link only
(https://antiragging.in/affidavit_registration_disclaimer.html )*, once you click the provided link, you will
directly access to the page of anti-ragging.in, and click the university tab where you have to fill the form
which opened at the screen (Student have to fill the Name in the column of Authority/Director- Dr.
Sushil Kumar Singh (Chief Proctor) along with the contact detail 9520966413). After submit the form
you will get your reference number through text or mail, after that you can download your undertaking
by using that reference number by clicking https://www.antiragging.in/undertaking_request.php
 </label>
  </div>
 </div>
       
        <div class="form-group">
 <div class="col-md-12" style="font:bold;">
 <label style="font-size: medium ; color:black"> 
Step 5. Student’s mandatory to upload the required documents (mark sheet
10 th /12 th /Diploma/Graduation, Migration/Transfer certificate, Anti ragging Undertaking, Aadhar card
(student &amp; Parents) through document upload tab &amp; click the final submit button.
     </label>  </div>
 </div>
        <div class="form-group">
 <div class="col-md-12" style="font:bold;">

 <label style="font-size: medium ; color:black">
Step 6. Once the process of documents uploading, submission &amp; verification done, Enrollment form
(student must clear the dues of semester fee before filling the Enrollment form*) will be enable at
student portal to fill the form with required details &amp; filled printed form to be submitted to designated
staff of respective college along with the prequalification mark sheet, migration/TC &amp; Anti Ragging
Undertaking.
  </label>
     </div>
 </div>
      
<div class="form-group">
 <div class="col-md-12" style="font:bold;">
 <label style="font-size: medium ; color:black"> 
Note: -
 </label>
  </div>
 </div>
       <div class="form-group">
 <div class="col-md-12" style="font:bold;">
 <label style="font-size: medium ; color:black"> 
  1. In case if the any discrepancy found, then form can be rejected at any step with required remarks.
 </label>
  </div>
 </div>
      <div class="form-group">
 <div class="col-md-12" style="font:bold;">
 <label style="font-size: medium ; color:black"> 
2. It’s mandatory to complete the document uploading &amp; verification process to fill the Examination form. </label>
  </div>
 </div>

                            </div>
                        </div>
                        </div>
                    </div>
            </fieldset>
                </div>

         <div class="header" id="NotHindi" runat="server" visible="false" style="margin-top:-10px">
            <b>
                <asp:Label ID="Label7" runat="server" Font-Size="Large" Font-Bold="true" ForeColor="Black" Font-Underline="true" Text="दस्तावेज़ अपलोड करने और प्रोफ़ाइल अपडेट करने के लिए महत्वपूर्ण कदम"></asp:Label></b><div class="close">
                 
                </div>
             <div class="close">
                    <asp:Button ID="Button2" runat="server" BackColor="White" OnClick="Button2_Click"  Font-Bold="true" ForeColor="Black"  Text="X" />
                </div>

        </div>
             

<div id="divGeneralBodyHindi" runat="server" visible="false">
 <fieldset class="boxBodyInner">
<div class="form-horizontal">
 <div class="box-body">
  <div class="row">
 <div class="col-md-12">                
<div class="form-group">
 <div class="col-md-12">
 <label style="font-size: medium; color:black" runat="server">
छात्र अपने छात्र संख्या का उपयोग करके <a href="http://portal2.tmu.ac.in/Default.aspx" style="color:blue; text-decoration:none" target="_blank"> http://portal2.tmu.ac.in/Default.aspx </a> पर लॉग इन कर सकते हैं(जो फीस रसीद
के बाईं ऊपरी कोने मेंST/0XXXXX के रूप में उल्लिखित है) और अपने जन्म तिथि को पासवर्ड के रूप में DDMMYY प्रारूप में
उपयोग करें । उदाहरण के लिए, यदि आपकी जन्म तिथि 25 दिसंबर 2005 है, तो आपका पासवर्ड 251205 होगा।आगे की कारडवाई
के  लिए नीचे उल्लिखित चरणों के माध्यम से जाएं:-

</label>
  </div>
 </div>  
                                              
        <div class="form-group">
 <div class="col-md-12" style="font:bold;">
 <label style="font-size: medium; color:black"> 


चरण 1. एक बार छात्र अपने St नंबर से लॉग इन कर लेते हैं, उन्हें अपनी व्यक्तिगत जानकारी में कोई सुधार करना हो तो
वह करें और हिन्दी में नाम की आवश्यकता वाले क्षेत्र को भरकर, और ABC आईडी (छात्र के पास ABC आईडी के लिए
डिजी लॉकर खाता होना चाहिए) भरने के लिए उपलब्ध लिंक पर क्लिक करना होता है, और ABC आईडी भरनेके बाद
UPDATE बटन पर ल्लिक करें ।

 </label>
  </div>
 </div>
        
        <div class="form-group">
 <div class="col-md-12" style="font:bold;">
 <label style="font-size: medium ; color:black"> 
   
     चरण 2. यदि छात्र अपनी जानकारी अपडेट करते हैं, तो इसे निदेशक (प्रवेश) से सत्यापन के लिए आगे बढाया जाएगा।
 </label>
  </div>
 </div>
          
        <div class="form-group">
 <div class="col-md-12" style="font:bold;">
 <label style="font-size: medium;color:black"> 

चरण 3. VIRTUAL ID CARD डाउनलोड करने के लिए, छात्र अपने माता-पिता के मोबाइल नंबर जो ईआरपी  पोर्टल 
पर है, ओटीपी के माध्यम से मोबाइल नंबर की पुष्टि करके प्रक्रिया को प्रारंभ करते हैं। माता-पिता के मोबाइल नंबर की
पुष्टि करने के बाद, छात्र अपने खुद के मोबाइल नंबर के लिए एक अलग ओटीपी Generate करते हैं। इस ओटीपी को
दर्ज करने के बाद, छात्र VIRTUAL ID CARD डाउनलोड कर सकते हैं, जो भविष्य के संदर्भ और  उपयोग के लिए
महत्वपूर्ण है।


 </label>
  </div>
 </div>
     
        <div class="form-group">
 <div class="col-md-12" style="font:bold;">
 <label style="font-size: medium ; color:black"> 

     चरण 4.एंटी-रैगिंग शपथ पत्र केवल प्रदान किए गए लिंक
     <a href="https://antiragging.in/affidavit_registration_disclaimer.html" style="color:blue; text-decoration:none" target="_blank">(https://antiragging.in/affidavit_registration_disclaimer.html )*</a>,
के माध्यम से डाउनलोड किया जा सकता है, एक बार
आप लिंक पर क्लिक करेंगे, आप सीधे anti-ragging.in के पेज पर पहुँच जाएंगे, जहाँ आपको स्क्रीन पर खुलने वाले फार्म
को भरना होगा। फार्म सबमिट करने के बाद आपको टेक्स्ट या मेल के माध्यम से आपका Reference number मिलेगा,
उसके बाद आप उस Reference number का उपयोग करके
<a href="https://www.antiragging.in/undertaking_request.php" style="color:blue; text-decoration:none" target="_blank">https://www.antiragging.in/undertaking_request.php</a>  पर क्लिक  करके अपना शपथ पत्र डाउनलोड कर सकते
हैं। छात्र को आगे की आवश्यकताओं के लिए एंटी-रैगिंग शपथ पत्र डाउनलोड की गई प्रति  अपने पास रखनी चाहिए।
     </label>
  </div>
 </div>
       
        <div class="form-group">
 <div class="col-md-12" style="font:bold;">
 <label style="font-size: medium ; color:black"> 
 चरण 5. छात्रों को अनिवार्य रूप से आवश्यक दस्तावेज़(अंक पत्र 10वीं, 12वीं, डिप्लोमा और स्नातक, माइग्रेशन, ट्रांसफर प्रमाण पत्र, एंटी रैगिंग शपथ पत्र(चरण 3 के अनुसार), आधार कार्ड(छात्र और माता या पिता) को दस्तावेज़ अपलोड टैब के माध्यम से अपलोड करना अनिवार्य है।
     
 </label>
      </div>
        </div>
        <div class="form-group">
 <div class="col-md-12" style="font:bold;">

 <label style="font-size: medium ; color:black">
 चरण 6. एक बार दस्तावेज़ अपलोड करने, प्रस्तुति और सत्यापन  की प्रक्रिया पूरी होनेके बाद, छात्र पोर्टल पर एनरोलमेंट
फॉर्म (छात्र को नामांकन फॉर्म भरने से पहले सेमेस्टर शुल्क का बकाया चुकाना होगा*) भरने की सुविधा होगी,
 जिससे  आवश्यक विवरण भरने के बाद फॉर्म को प्रिंट करना होगा, और मार्कशीट, माइग्रेशन, टीसी के साथ आवंटित
कॉलेज के प्रतिनिधि के पास जमा करना होगा।

     </label>
     </div>
 </div>
      
<div class="form-group">
 <div class="col-md-12" style="font:bold;">
 <label style="font-size: medium ; color:black"> 
नोट: -
 </label>
  </div>
 </div>
       <div class="form-group">
 <div class="col-md-12" style="font:bold;">
 <label style="font-size: medium ; color:black"> 
1. यदि किसी भी अयोग्यता का पता चले, तो कोई भी फॉर्म किसी भी स्टेप पर आवश्यक टिप्पणियों के साथ अस्वीकृत किया जा सकता है।

 </label>
  </div>
 </div>
      <div class="form-group">
 <div class="col-md-12" style="font:bold;">
 <label style="font-size: medium ; color:black"> 
2. चिकित्सा/डेंटल/नर्सिग/फार्मेसी/पैरामेडिकल/फिजिओथेरेपी और कृषि विज्ञान के छात्रों के लिए मेरिट आधारित छात्रवृत्ति लागू नहीं होगी।
     </label>
       </div>
 </div>

                            </div>
                        </div>
                        </div>
                    </div>
            </fieldset>
                </div>

</ContentTemplate>
           </asp:UpdatePanel>

    </asp:Panel>


   
    <fieldset>
            <div>
            <marquee>
      <asp:Label ID="Label6" style="line-height:30px" runat="server" Font-Bold="true" Font-Size="X-Large" ForeColor="Red" Text="Label"><b>1.Kindly submit the Original Migration,TC, Character certificate, Gap certificate (If required), 4 passport size photographs student, 1 photo graph parents/guardian in admission cell.
2. Kindly submit the 1 photo copy set of 10th marksheet,12th marksheet ,graduation marksheet (If required), Aadhar student & Parents, Domicile in Admission cell.
*Kindly ignore if submitted the same.</b></asp:Label></marquee>
            </div>
        </fieldset>


    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="Profile" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
     <fieldset class="boxBody">
        <asp:Label ID="Label5" runat="server"
            Text="Kindly do Update/Ok your profile to enable the next tab." Font-Size="15pt" ForeColor="Green" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>

    <fieldset class="boxBodyHeader">
    </fieldset>
    <fieldset class="boxBodyInner">
        <table cellpadding="0px" cellspacing="0px">
            <tr>
                <td colspan="15" style="height: 10px">
                    <fieldset class="boxBodyHeader">
                        <asp:Label ID="Label3" runat="server"
                            Text="Personal Information" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

                    </fieldset>
                    <br />
                    <fieldset class="boxBodyInner">
                        <table cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td>
                                    <table cellpadding="0px" cellspacing="0px">

                                        <tr>
                                            <td>
                                                <label>Student Name</label></td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:TextBox ID="txtName" runat="server" Width="250px" Enabled="False"></asp:TextBox>

                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <label>Father's Name </label>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:TextBox ID="txtfathername" runat="server" Width="250px" Enabled="False"></asp:TextBox></td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <label style="width: 100px; float: right;">Mother's Name</label></td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtMothername" runat="server" Width="250px" Enabled="False"></asp:TextBox></td>
                                        </tr>
                                        
                                        <tr>
                                            <td colspan="11" style="height: 10px"></td>
                                        </tr>
                                         <tr>
                                            <td colspan="11" style="height: 10px"></td>
                                        </tr>
                                   <tr>
                                            <td>
                                                <label>छात्र/छात्रा का नाम</label>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                      <div id="DSH" runat="server" visible="false">          
                                                
                                      
                                   <div class="Google-transliterate-Way2blogging">
                                    
                                    <input type="text" name="SHname" id="TxtSH" "width:250px; maxlength="24"/>                               
                                       <asp:Label ID="hmsg" runat="server"></asp:Label>
                                 </div>
                                 </div>
                                 <asp:TextBox ID="txtStudentHindi" runat="server"  MaxLength="30"    Width="250px"></asp:TextBox>
                                        
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <label>पिता का नाम</label></td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <div id="DFH" runat="server" visible="false">          
                                      
                                        <div class="Google-transliterate-Way2blogging">
                                    <input type="text" name="FHname" id="TxtFH" "width:260px; maxlength="35"/>
                                          </div>
                                                    </div>
                                       <asp:TextBox ID="txtFatherHindi" runat="server"   MaxLength="35"  Width="250px"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>माता का नाम<label>  </label>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td> 
                                     <div id="DMH" runat="server" visible="false">          
                                      
                                        <div class="Google-transliterate-Way2blogging">
                                    
                                    <input type="text" name="MHname" id="TxtMH" "width:260px; maxlength="24"/>
                                
                                          
                                 </div> </div>
                                                <asp:TextBox ID="txtMotherHindi" runat="server"    MaxLength="30"  Width="250px"></asp:TextBox>
                                            </td>
                                        </tr>                


                                        <tr>
                                            <td colspan="11" style="height: 10px"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Programme </label>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:TextBox ID="txtCourse" runat="server" Enabled="False" Width="250px"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <label>Enrolment No </label>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>

                                                <asp:TextBox ID="txtRollNo" runat="server" Width="250px" Enabled="false"></asp:TextBox>

                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <label>Date of Birth </label>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:TextBox ID="txtDOB" runat="server" Width="250px" Enabled="False"></asp:TextBox></td>

                                        </tr>
                                        <tr>
                                            <td colspan="11" style="height: 10px"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Name Of College </label>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:TextBox ID="txtnameofcollege" runat="server" Enabled="False" Width="250px"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <label>Religion </label>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>

                                                <asp:TextBox ID="txtreligion" runat="server" Width="250px" Enabled="False"></asp:TextBox>

                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <label>Category </label>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:TextBox ID="txtcategory" runat="server" Width="250px" Enabled="False"></asp:TextBox></td>

                                        </tr>

                                        <tr>
                                            <td colspan="11" style="height: 10px"></td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <label>Year of Addmission</label>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:TextBox ID="txtyearofaddmission" runat="server" Enabled="False" Width="250px"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <label>Gender </label>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>

                                                <asp:TextBox ID="txtgender" runat="server" Width="250px" Enabled="False"></asp:TextBox>

                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <label>Nationality</label>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:TextBox ID="txtnationality" runat="server" Enabled="False" Width="250px"></asp:TextBox></td>

                                        </tr>


                                        <tr>
                                            <td colspan="11" style="height: 10px"></td>
                                        </tr>
                                      
                                        <tr>
                                            <td colspan="11" style="height: 10px"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>ABC ID: </label>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:TextBox ID="txtabcid" runat="server" onkeypress="return numeric(event)" MaxLength="12" MinLength="12"  Width="250px"></asp:TextBox>
                                            </td>
                                             <td style="width: 10px"></td>
                                             <td>
                                                <a href=" https://digilocker.meripehchaan.gov.in/" id="A1" runat="server" target="_blank" visible="true" style="color:blue; font-size:large"><b>Click this link to obtain ABC ID</b></a> 
                                            </td>


                                            </tr>
                                        <tr>
                                            <td colspan="11" style="height: 10px"></td>
                                        </tr>
                                      
                                    </table>
                                   
                                </td>
                            </tr>
                        </table>

                    </fieldset>
                </td>
            </tr>

            <tr>
                <td colspan="15" style="height: 10px">
                    <fieldset class="boxBodyHeader">
                        <asp:Label ID="Label2" runat="server"
                            Text="Contact Information" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

                    </fieldset>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <fieldset class="boxBodyInner">
                                <table cellpadding="0px" cellspacing="0px" class="auto-style1">

                                    <tr>
                                        <td colspan="12">
                                            <table cellpadding="0px" cellspacing="0px">

                                                <tr>
                                                    <td>
                                                        <label>Student Mob. </label>
                                                    </td>
                                                    <td class="auto-style2"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtmobileno" runat="server" Width="200px" BorderColor="Black" placeholder="Mobile"  Enabled="False"  onkeypress="return numeric(event)" MaxLength="10" TargetControlID="txtmobileno" FilterType="Numbers, Custom"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtmobileno" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps" ValidationExpression='(^([0-9]*|\d*\d{1}?\d*)$)' Display='Dynamic'></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td style="width: 10px"></td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <label>Student Email-Id </label>
                                                    </td>
                                                    <td class="auto-style3"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtsudentEmailID" runat="server" Width="250px" Enabled="False"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="sudentEmailID" Display="Dynamic" ControlToValidate="txtsudentEmailID" Text="Please fill the valid Email address!" ForeColor="Red" ValidationGroup="g1"
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server" /></td>


                                                </tr>
                                                <tr>
                                                    <td colspan="7" style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>Parents Mob. </label>
                                                    </td>
                                                    <td class="auto-style2"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtparentsmob" runat="server"  Width="200px" BorderColor="Black"  placeholder="Mobile" onkeypress="return numeric(event)" Enabled="False" MaxLength="10" TargetControlID="txtmobileno" FilterType="Numbers, Custom"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtparentsmob" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps" ValidationExpression='(^([0-9]*|\d*\d{1}?\d*)$)' Display='Dynamic'></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td style="width: 10px"></td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <label>Parents Email-Id </label>
                                                    </td>
                                                    <td class="auto-style3"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtEmailID" runat="server" Width="250px" Enabled="False"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="regEmail" Display="Dynamic" ControlToValidate="txtEmailID" Text="Please fill the valid Email address!" ForeColor="Red" ValidationGroup="g1"
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                    </tr>
                                </table>
                            </fieldset>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <fieldset class="boxBodyHeader">
                        <asp:Label ID="Label4" runat="server"
                            Text="Address Information" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

                    </fieldset>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>

                            <fieldset class="boxBodyInner">
                                <table cellpadding="0px" cellspacing="0px" class="auto-style1">

                                    <tr>
                                        <td colspan="12">
                                            <table cellpadding="0px" cellspacing="0px">

                                                <tr>
                                                    <td>
                                                        <label>Correspondence Address </label>
                                                    </td>
                                                    <td class="auto-style2"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtcorrespondence" runat="server" MaxLength="256" Enabled="False" Width="280px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <label>City </label>
                                                    </td>
                                                    <td class="auto-style3"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtdistrictcorres" Enabled="False" runat="server" Width="250px"></asp:TextBox></td>
                                                    <td class="auto-style5"></td>
                                                    <td>
                                                        <label>State </label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtstatecorres" Enabled="False" runat="server" Width="250px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="7" style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>Pin Code </label>
                                                    </td>
                                                    <td class="auto-style2"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtpincodecorre" runat="server" Width="200px" BorderColor="Black" placeholder="Pin Code" Enabled="False" onkeypress="return numeric(event)" MaxLength="6" TargetControlID="txtmobileno" FilterType="Numbers, Custom"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtpincodecorre" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps" ValidationExpression='(^([0-9]*|\d*\d{1}?\d*)$)' Display='Dynamic'></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <label>Country </label>
                                                    </td>
                                                    <td class="auto-style3"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtcountrycorre" runat="server" Width="250px" Enabled="False"></asp:TextBox></td>
                                                    <td class="auto-style5"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="7" style="height: 10px"></td>
                                                </tr>
                                                <td>
                                                    <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="true" Text="Same As Above" Font-Size="15pt" OnCheckedChanged="CheckBox2_CheckedChanged" ForeColor="Black" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" />
                                                </td>
                                                <tr>
                                                    <td colspan="11" style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>Permanent Address </label>
                                                    </td>
                                                    <td class="auto-style2"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtperaddress" runat="server" MaxLength="256" Height="28px" Width="280px" Enabled="False"></asp:TextBox>
                                                        <%-- <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterMode="ValidChars"
                                                            ValidChars="zxcvbnmlkjhgfdsaqwertyuiopQWERTYUIOPLKJHGFDSAZXCVBNM"
                                                            TargetControlID="txtCity">--%>
                                                        <%-- </asp:FilteredTextBoxExtender>--%>
                                                    </td>
                                                    <td style="width: 10px"></td>

                                                    <td>City
                                                    </td>
                                                    <td class="auto-style3"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtperdistrict" runat="server" Enabled="False" Width="250px"></asp:TextBox>
                                                        <%--<asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterMode="ValidChars"
                                                            ValidChars="zxcvbnmlkjhgfdsaqwertyuiopQWERTYUIOPLKJHGFDSAZXCVBNM"
                                                            TargetControlID="txtCountry">
                                                        </asp:FilteredTextBoxExtender>--%>
                                                        <%-- <asp:RegularExpressionValidator ID="regEmail" Display="Dynamic" ControlToValidate="txtEmailID" Text="Please fill the valid Email address!" ForeColor="Red" ValidationGroup="g1"
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server" />--%>
                                                        <%-- <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtMobileNo" FilterType="Numbers, Custom"></asp:FilteredTextBoxExtender>--%>
                                                    </td>
                                                    <td class="auto-style5"></td>
                                                    <td>
                                                        <label>State </label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtperstate" runat="server" Enabled="False" Width="250px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="7" style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>Pin Code </label>
                                                    </td>
                                                    <td class="auto-style2"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtperpincode" runat="server" Width="200px" BorderColor="Black" Enabled="False" placeholder="Pin Code" onkeypress="return numeric(event)" MaxLength="6" TargetControlID="txtmobileno" FilterType="Numbers, Custom"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtperpincode" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="odapps" ValidationExpression='(^([0-9]*|\d*\d{1}?\d*)$)' Display='Dynamic'></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <label>Country </label>
                                                    </td>
                                                    <td class="auto-style3"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtpercountry" runat="server" Enable="False" Width="250px" Enabled="False"></asp:TextBox></td>
                                                    <td class="auto-style5"></td>


                                                    <tr>
                                                        <td colspan="9" style="height: 10px;"></td>
                                                    </tr>
                                        </td>
                                    </tr>
                                </table>


                                </tr>
                                </table>

                                <div class="col-md-7">
                                </div>

                                <div class="col-md-1">
                                    <asp:Button ID="btn_save" runat="server" Text="Update" BackColor="#ff9933" ForeColor="White" Visible="false" CssClass="form-control" OnClick="btn_save_Click"  Height="30px" Width="80px" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btn_ok" runat="server" Text="OK" BackColor="#ff9933" ForeColor="White" OnClick="btn_ok_Click" CssClass="form-control" Height="30px" Width="80px" />
                                </div>
                            </fieldset>
                              

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </fieldset>
                                            
    <br />
    <br />
</asp:Content>

