<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="HindiNameUpdate.aspx.cs" Inherits="Faculty_HindiNameUpdate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
  <script src="https://www.google.com/jsapi" type="text/javascript">  
</script>  
<script type="text/javascript">
    google.load("elements", "1", {
        packages: "transliteration"
    });

    function onLoad() {
        
        document.getElementById("TxtSH").value = document.getElementById('<%= Studentname.ClientID %>').value;
        document.getElementById("TxtFH").value = document.getElementById('<%= fathername.ClientID %>').value;
        document.getElementById("TxtMH").value = document.getElementById('<%= mothername.ClientID %>').value;
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
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset style="background: #fefefe; border-top: 1px solid #dde0e8; border-bottom: 1px solid #dde0e8; padding: 10px 20px; height: 100%">
        <fieldset class="boxBodyHeader">
            <asp:Label ID="Label1" runat="server"
                Text="Name Updation" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

        </fieldset>
    </fieldset>
    <div class="clearfix pt-12">
        <div class="col-sm-8 padding-tb mb-10">
            <div class="row">
                <div class="col-xs-4 col-md-2">
                  <%--  <label>Enrollment/Student No : </label>--%>
                </div>
                <div class="col-xs-8">
                    <asp:TextBox ID="txtEnrollNo" Style="text-transform: uppercase" runat="server" CssClass="form-control" autocomplete="off" placeholder="Search Enrollment No OR Student No" AutoPostBack="true"></asp:TextBox>

                </div>
                
                <asp:Button ID="BtnShow" runat="server" Text="Show" CssClass="btn btn-warning" OnClick="BtnShow_Click" Width="95px" />
            </div>
        </div>
    </div>

    <fieldset class="boxBodyInner">
       
        <table>
            <tr>
                <td style="font-weight:bold">Enrollment No :&nbsp&nbsp&nbsp&nbsp</td>
                <td style="padding-right:10px">
  <asp:Label ID="LblEnrollment" runat="server"   Style="text-transform: uppercase;"></asp:Label>
                        <asp:HiddenField ID="HfStudentNo"  runat="server" />
                </td>
                &nbsp&nbsp&nbsp&nbsp
                 <td style="font-weight:bold">Student No :&nbsp&nbsp&nbsp&nbsp</td>
                <td style="padding-right:10px">
                      <asp:Label ID="lblStudentNo" runat="server"   Style="text-transform: uppercase;"></asp:Label>     
                </td>
                &nbsp&nbsp&nbsp&nbsp
                 <td style="font-weight:bold">Student Name :&nbsp&nbsp&nbsp&nbsp</td>
                <td style="padding-right:10px">
  <asp:Label ID="lblStudentName" runat="server"   Style="text-transform: uppercase;"></asp:Label>          
                </td>
                 &nbsp&nbsp&nbsp&nbsp
                 <td style="font-weight:bold">Mother Name :&nbsp&nbsp&nbsp&nbsp</td>
                <td style="padding-right:10px">
  <asp:Label ID="lblMother" runat="server"   Style="text-transform: uppercase;"></asp:Label>          
                </td>
                 &nbsp&nbsp&nbsp&nbsp
                 <td style="font-weight:bold">Father Name :&nbsp&nbsp&nbsp&nbsp</td>
                <td style="padding-right:10px">
  <asp:Label ID="lblFather" runat="server"   Style="text-transform: uppercase;"></asp:Label>          
                </td>
            </tr>
        </table>
    </fieldset>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
    <table>
          <tr> <td colspan="2" style="height:10px"></td></tr>
        <tr style="padding-right:20px;width:50%;text-align:center">
            
            <td style="padding-right:30px;text-align:right;font-weight:bold">
             &nbsp &nbsp  &nbsp&nbsp &nbsp  &nbsp    छात्र/छात्रा का नाम :
            </td>
            <td>
               
               <div class="Google-transliterate-Way2blogging">
                                    
                                    <input type="text" name="SHname" id="TxtSH" width:260px;   oncopy="return false;" <%--onpaste="return false;"--%> maxlength="50" value="<%= this.StudentHindi %>" />                               
                                     <asp:HiddenField ID="Studentname"  runat="server"  />
            </td>
            <td style="color:red"> &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp NOTE : Please Press Tab or Space Key After Fill The Name To Convert In Hindi.</td>


        </tr>
        <tr> <td colspan="2" style="height:10px"></td></tr>
        <tr style="padding-right:20px;width:50%;text-align:center">
            
            <td style="padding-right:30px;text-align:right;font-weight:bold">
             &nbsp &nbsp  &nbsp&nbsp &nbsp  &nbsp    पिता का नाम :
            </td>
            <td>
               <div class="Google-transliterate-Way2blogging">
                                    <input type="text" name="FHname" id="TxtFH" "width:260px;  oncopy="return false;" <%--onpaste="return false;"--%> maxlength="50" value="<%= this.FatherHindi %>"/>
                                         <asp:HiddenField ID="fathername"  runat="server"  /> 
               </div>
            </td>
            <td></td>
        </tr>
        <tr> <td colspan="2" style="height:10px"></td></tr>
        <tr style="padding-right:20px;width:50%;text-align:center">
            
            <td style="padding-right:30px;text-align:right;font-weight:bold">
             &nbsp &nbsp  &nbsp&nbsp &nbsp  &nbsp    माता का नाम :
            </td>
            <td>
                <div class="Google-transliterate-Way2blogging">
                                    
                                    <input type="text" name="MHname" id="TxtMH" "width:260px; oncopy="return false;"  <%--onpaste="return false;"--%> maxlength="50" value="<%= this.MotherHindi %>"/>
                                <asp:HiddenField ID="mothername"  runat="server"  />

                                 </div>
            </td>
            <td></td>
        </tr>
        <tr> <td colspan="2" style="height:10px"></td></tr>
        <tr>
            <td></td>
            <td style="text-align:right"><asp:Button ID="btnUpdate" CssClass="btn btn-warning" Text="Update" runat="server" OnClick="btnUpdate_Click"/></td>
        </tr>
    </table>
            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

