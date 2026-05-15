<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="Super_checkList.aspx.cs" Inherits="Faculty_Super_checkList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
      <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="सुपरवाइज़र चेक लिस्ट" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
</fieldset><br />
    <table class="boxBody" width="1200px">

            <tr>
                <td colspan="11" style="height: 10px"></td>
            </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="सुपरवाइज़र का नामः"></asp:Label></td>

            <td>&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtsupervisorname" runat="server" Enabled="false"></asp:TextBox>
                 <asp:requiredfieldvalidator id="RequiredFieldValidator1" ControlToValidate="txtsupervisorname" ValidationGroup="g1" errormessage="कृपया सुपरवाइज़र का नाम डाले " runat="Server"></asp:requiredfieldvalidator>
                </td>
         
            
                 <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:Label ID="Label3" runat="server" Text="वार्ड में मरीजों की संख्याः"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtpatientdetail" runat="server" placeholder="वार्ड में मरीजों की संख्या" TextMode="Number"></asp:TextBox>
                <asp:requiredfieldvalidator id="RequiredFieldValidator7" ControlToValidate="txtpatientdetail" ValidationGroup="g1" errormessage="कृपया वार्ड में मरीजों की संख्या डाले " runat="Server"></asp:requiredfieldvalidator>
            </td>

        </tr>

        <tr> <td colspan="11" style="height:10px"> </td></tr> 
        <tr>

            <td>

                <asp:Label ID="Label4" runat="server" Text="रिपोर्ट की तारीखः"> </asp:Label>
                

            </td>
            <td>
                       <asp:TextBox ID="txtreportdate" runat="server" TextMode="DateTimeLocal" Width="160px" ></asp:TextBox>
                 <%--<cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtreportdate" Format="dd/MM/yyyy" SelectedDate="<%# DateTime.Today %>>
                                    </cc1:CalendarExtender>--%>
                 <asp:requiredfieldvalidator id="RequiredFieldValidator3" ControlToValidate="txtreportdate" ValidationGroup="g1" errormessage="कृपया रिपोर्ट की तारीख डाले " runat="Server"></asp:requiredfieldvalidator>
                                    </td>
             <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label5" runat="server" Text="वार्ड में उपस्थित वार्ड सहायकों की संख्याः"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txthelperdetail" runat="server" placeholder="वार्ड में उपस्थित वार्ड सहायकों की संख्या" TextMode="Number"></asp:TextBox>
                <asp:requiredfieldvalidator id="RequiredFieldValidator4" ControlToValidate="txthelperdetail" ValidationGroup="g1" errormessage="कृपया वार्ड सहायकों की संख्या डाले " runat="Server"></asp:requiredfieldvalidator>
             
            </td>
        </tr>
        <tr> <td colspan="11" style="height:10px"> </td></tr> 
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="वार्ड का नामः"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtwardname" runat="server" placeholder="वार्ड का नाम"></asp:TextBox>
                <asp:requiredfieldvalidator id="RequiredFieldValidator5" ControlToValidate="txtwardname" ValidationGroup="g1" errormessage="कृपया वार्ड का नाम डाले " runat="Server"></asp:requiredfieldvalidator>
            </td>
          
             <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label11" runat="server" Text="वार्ड का कमरा संख्याः"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtwardroomno" runat="server" placeholder="वार्ड का कमरा संख्या"></asp:TextBox>
                <asp:requiredfieldvalidator id="RequiredFieldValidator2" ControlToValidate="txtwardroomno" ValidationGroup="g1" errormessage="कृपया वार्ड का कमरा संख्या डाले " runat="Server"></asp:requiredfieldvalidator>
                 </td>
        </tr> 
    </table>
    <br />
    <asp:Table class="boxBody" width="1200px" style="border:1px solid" id="tblData" runat="server">
        <asp:TableRow>
            <asp:TableHeaderCell style="border:1px solid">
        <asp:Label ID="Label7" runat="server" Text="क्रमांक."></asp:Label>
                </asp:TableHeaderCell>
            
            <asp:TableHeaderCell style="border:1px solid">
        <asp:Label ID="Label8" runat="server" Text="जांच के बिंदु."></asp:Label>
                </asp:TableHeaderCell>
            <asp:TableCell style="border:1px solid">
        <asp:Label ID="Label9" runat="server" Text="स्थिति."></asp:Label>
                </asp:TableCell>
            <asp:TableCell style="border:1px solid">
        <asp:Label ID="Label10" runat="server" Text="टिप्पणि."></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell style="border:1px solid">1.</asp:TableCell>
            <asp:TableCell style="border:1px solid">
                <asp:Label ID="LabelR1" runat="server" Text="सभी वार्ड बॉय/आया अपने स्थान पर मौजूद हैं"></asp:Label>
</asp:TableCell>
            <asp:TableCell style="border:1px solid">
            <asp:DropDownList ID="DropDownList1" runat="server" Height="26px" Width="130px" style="border:1px solid">
            <asp:ListItem Text="--स्थति--"></asp:ListItem>
               <asp:ListItem Text="हाँ"></asp:ListItem>
                <asp:ListItem Text="नही"></asp:ListItem>
                
                 </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="DropDownList1" ValidationGroup="g1" ErrorMessage="*" runat="Server"></asp:RequiredFieldValidator>
                </asp:TableCell>
            <asp:TableCell style="border:1px solid">
                <asp:TextBox ID="TextBoxR1" runat="server" Width="296px" Height="26px"></asp:TextBox>
            </asp:TableCell>
            
        </asp:TableRow>
         <asp:TableRow>
            <asp:TableCell style="border:1px solid">2.</asp:TableCell>
            <asp:TableCell style="border:1px solid">
                <asp:Label ID="LabelR2" runat="server" Text="वार्ड सहायकों की पोशाक और सफाई"></asp:Label>
</asp:TableCell>
            <asp:TableCell style="border:1px solid">
            <asp:DropDownList ID="DropDownList2" runat="server" Height="26px" Width="130px">
            <asp:ListItem Text="--स्थति--"></asp:ListItem>
               <asp:ListItem Text="हाँ"></asp:ListItem>
                <asp:ListItem Text="नही"></asp:ListItem>
            </asp:DropDownList>
                </asp:TableCell>
            <asp:TableCell style="border:1px solid">
                <asp:TextBox ID="TextBoxR2" runat="server" Width="296px"></asp:TextBox>
            </asp:TableCell>
            
        </asp:TableRow>
         <asp:TableRow>
            <asp:TableCell style="border:1px solid">3.</asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="LabelR3" runat="server" Text="क्या वार्ड का फर्श साफ सुथरा था"></asp:Label>
</asp:TableCell>
            <asp:TableCell style="border:1px solid">
            <asp:DropDownList ID="DropDownList3" runat="server" Height="26px" Width="130px">
            <asp:ListItem Text="--स्थति--"></asp:ListItem>
               <asp:ListItem Text="हाँ"></asp:ListItem>
                <asp:ListItem Text="नही"></asp:ListItem>
            </asp:DropDownList>
                </asp:TableCell>
            <asp:TableCell style="border:1px solid">
                <asp:TextBox ID="TextBoxR3" runat="server" Width="296px"></asp:TextBox>
            </asp:TableCell>
            
        </asp:TableRow>
         <asp:TableRow>
            <asp:TableCell style="border:1px solid">4.</asp:TableCell>
            <asp:TableCell style="border:1px solid">
                <asp:Label ID="LabelR4" runat="server" Text="क्या शौचालय साफ और गंधहीन थे"></asp:Label>
</asp:TableCell>
            <asp:TableCell style="border:1px solid">
            <asp:DropDownList ID="DropDownList4" runat="server" Height="26px" Width="130px">
            <asp:ListItem Text="--स्थति--"></asp:ListItem>
               <asp:ListItem Text="हाँ"></asp:ListItem>
                <asp:ListItem Text="नही"></asp:ListItem>
            </asp:DropDownList>
                </asp:TableCell>
            <asp:TableCell style="border:1px solid">
                <asp:TextBox ID="TextBoxR4" runat="server" Width="296px"></asp:TextBox>
            </asp:TableCell>
            
        </asp:TableRow>
         <asp:TableRow>
            <asp:TableCell style="border:1px solid">5.</asp:TableCell>
            <asp:TableCell style="border:1px solid">
                <asp:Label ID="LabelR5" runat="server" Text="क्या डस्टिंग ठीक से की गई थी"></asp:Label>
</asp:TableCell>
            <asp:TableCell style="border:1px solid">
            <asp:DropDownList ID="DropDownList5" runat="server" Height="26px" Width="130px">
            <asp:ListItem Text="--स्थति--"></asp:ListItem>
               <asp:ListItem Text="हाँ"></asp:ListItem>
                <asp:ListItem Text="नही"></asp:ListItem>
            </asp:DropDownList>
                </asp:TableCell>
            <asp:TableCell style="border:1px solid">
                <asp:TextBox ID="TextBoxR5" runat="server" Width="296px"></asp:TextBox>
            </asp:TableCell>
            
        </asp:TableRow>
         <asp:TableRow>
            <asp:TableCell style="border:1px solid">6.</asp:TableCell>
            <asp:TableCell style="border:1px solid">
                <asp:Label ID="LabelR6" runat="server" Text="क्या रोगी स्थानांतरण उपकरण जैसे व्हील चेयर/ट्रॉली काम करने की स्थिति में है"></asp:Label>
</asp:TableCell>
            <asp:TableCell style="border:1px solid">
            <asp:DropDownList ID="DropDownList6" runat="server" Height="26px" Width="130px">
            <asp:ListItem Text="--स्थति--"></asp:ListItem>
               <asp:ListItem Text="हाँ"></asp:ListItem>
                <asp:ListItem Text="नही"></asp:ListItem>
            </asp:DropDownList>
                </asp:TableCell>
            <asp:TableCell style="border:1px solid">
                <asp:TextBox ID="TextBoxR6" runat="server" Width="296px"></asp:TextBox>
            </asp:TableCell>
            
        </asp:TableRow>
         <asp:TableRow>
            <asp:TableCell style="border:1px solid">7.</asp:TableCell>
            <asp:TableCell style="border:1px solid">
                <asp:Label ID="LabelR7" runat="server" Text="क्या वार्ड में साफ लिनेन उपलब्ध है"></asp:Label>
</asp:TableCell>
            <asp:TableCell style="border:1px solid">
            <asp:DropDownList ID="DropDownList7" runat="server" Height="26px" Width="130px">
            <asp:ListItem Text="--स्थति--"></asp:ListItem>
               <asp:ListItem Text="हाँ"></asp:ListItem>
                <asp:ListItem Text="नही"></asp:ListItem>
            </asp:DropDownList>
                </asp:TableCell>
            <asp:TableCell style="border:1px solid">
                <asp:TextBox ID="TextBoxR7" runat="server" Width="296px"></asp:TextBox>
            </asp:TableCell>
            
        </asp:TableRow>
         <asp:TableRow>
            <asp:TableCell style="border:1px solid">8.</asp:TableCell>
            <asp:TableCell style="border:1px solid">
                <asp:Label ID="LabelR8" runat="server" Text="क्या वार्ड से समय रहते गंदे लिनन को हटाया गया?"></asp:Label>
</asp:TableCell>
            <asp:TableCell style="border:1px solid">
            <asp:DropDownList ID="DropDownList8" runat="server" Height="26px" Width="130px">
            <asp:ListItem Text="--स्थति--"></asp:ListItem>
               <asp:ListItem Text="हाँ"></asp:ListItem>
                <asp:ListItem Text="नही"></asp:ListItem>
            </asp:DropDownList>
                </asp:TableCell>
            <asp:TableCell style="border:1px solid">
                <asp:TextBox ID="TextBoxR8" runat="server" Width="296px"></asp:TextBox>
            </asp:TableCell>
            
        </asp:TableRow>
         <asp:TableRow>
            <asp:TableCell style="border:1px solid">9.</asp:TableCell>
            <asp:TableCell style="border:1px solid">
                <asp:Label ID="LabelR9" runat="server" Text="क्या सैंपल समय पर लैब में भेजे गए?"></asp:Label>
</asp:TableCell>
            <asp:TableCell style="border:1px solid">
            <asp:DropDownList ID="DropDownList9" runat="server" Height="26px" Width="130px">
            <asp:ListItem Text="--स्थति--"></asp:ListItem>
               <asp:ListItem Text="हाँ"></asp:ListItem>
                <asp:ListItem Text="नही"></asp:ListItem>
            </asp:DropDownList>
                </asp:TableCell>
            <asp:TableCell style="border:1px solid">
                <asp:TextBox ID="TextBoxR9" runat="server" Width="296px"></asp:TextBox>
            </asp:TableCell>
            
        </asp:TableRow>
         <asp:TableRow>
            <asp:TableCell style="border:1px solid">10.</asp:TableCell>
            <asp:TableCell style="border:1px solid">
                <asp:Label ID="LabelR10" runat="server" Text="क्या मरीज को जांच के लिए वॉर्ड से समय पर शिफ्ट किया गया था?"></asp:Label>
</asp:TableCell>
            <asp:TableCell style="border:1px solid">
            <asp:DropDownList ID="DropDownList10" runat="server" Height="26px" Width="130px">
            <asp:ListItem Text="--स्थति--"></asp:ListItem>
               <asp:ListItem Text="हाँ"></asp:ListItem>
                <asp:ListItem Text="नही"></asp:ListItem>
            </asp:DropDownList>
                </asp:TableCell>
            <asp:TableCell style="border:1px solid">
                <asp:TextBox ID="TextBoxR10" runat="server" Width="296px"></asp:TextBox>
            </asp:TableCell>
               </asp:TableRow>
         <asp:TableRow>
            <asp:TableCell style="border:1px solid">11.</asp:TableCell>
            <asp:TableCell style="border:1px solid">
                <asp:Label ID="LabelR11" runat="server" Text="क्या शौचालय सुचारू रूप से काम करने की स्तिथि में है"></asp:Label>
</asp:TableCell>
            <asp:TableCell style="border:1px solid">
            <asp:DropDownList ID="DropDownList11" runat="server" Height="26px" Width="130px">
            <asp:ListItem Text="--स्थति--"></asp:ListItem>
               <asp:ListItem Text="हाँ"></asp:ListItem>
                <asp:ListItem Text="नही"></asp:ListItem>
            </asp:DropDownList>
                </asp:TableCell>
            <asp:TableCell style="border:1px solid">
                <asp:TextBox ID="TextBoxR11" runat="server" Width="296px"></asp:TextBox>
            </asp:TableCell>
            
        </asp:TableRow>
         <asp:TableRow>
            <asp:TableCell style="border:1px solid">12.</asp:TableCell>
            <asp:TableCell style="border:1px solid">
                <asp:Label ID="LabelR12" runat="server" Text="क्या कोई वार्ड में अनावश्यक रूप से मोबाईल फोन का उपयोग करते पाया गया?"></asp:Label>
</asp:TableCell>
            <asp:TableCell style="border:1px solid">
            <asp:DropDownList ID="DropDownList12" runat="server" Height="26px" Width="130px">
            <asp:ListItem Text="--स्थति--"></asp:ListItem>
               <asp:ListItem Text="हाँ"></asp:ListItem>
                <asp:ListItem Text="नही"></asp:ListItem>
            </asp:DropDownList>
                </asp:TableCell>
            <asp:TableCell style="border:1px solid">
                <asp:TextBox ID="TextBoxR12" runat="server" Width="296px"></asp:TextBox>
            </asp:TableCell>          
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell style="border:1px solid">13.</asp:TableCell>
            <asp:TableCell style="border:1px solid">
                <asp:Label ID="LabelR13" runat="server" Text="क्या आपने जिस वार्ड में देखा है उस वार्ड में मरीज़ का अंसतोष तो नहीं था"></asp:Label>
</asp:TableCell>
            <asp:TableCell style="border:1px solid">
            <asp:DropDownList ID="DropDownList13" runat="server" Height="26px" Width="130px">
            <asp:ListItem Text="--स्थति--"></asp:ListItem>
               <asp:ListItem Text="हाँ"></asp:ListItem>
                <asp:ListItem Text="नही"></asp:ListItem>
            </asp:DropDownList>
                </asp:TableCell>
            <asp:TableCell style="border:1px solid">
                <asp:TextBox ID="TextBoxR13" runat="server" Width="296px"></asp:TextBox>
            </asp:TableCell>
            
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell style="border:1px solid">14.</asp:TableCell>
            <asp:TableCell style="border:1px solid">
                <asp:Label ID="LabelR14" runat="server" Text="क्या कोई वार्ड आया/ वार्ड बॉय मरीज/स्टाफ/डॉक्टरों के साथ दुर्व्यवहार करता पाया गया?"></asp:Label>
</asp:TableCell>
            <asp:TableCell style="border:1px solid">
            <asp:DropDownList ID="DropDownList14" runat="server" Height="26px" Width="130px">
            <asp:ListItem Text="--स्थति--"></asp:ListItem>
               <asp:ListItem Text="हाँ"></asp:ListItem>
                <asp:ListItem Text="नही"></asp:ListItem>
            </asp:DropDownList>
                </asp:TableCell>
            <asp:TableCell style="border:1px solid">
                <asp:TextBox ID="TextBoxR14" runat="server" Width="296px"></asp:TextBox>
            </asp:TableCell>
            
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell style="border:1px solid">15.</asp:TableCell>
            <asp:TableCell style="border:1px solid">
                <asp:Label ID="LabelR15" runat="server" Text="क्या वार्ड में चोरी, गरमा-गरमी जैसी कोई अप्रिय घटना हुई थी?"></asp:Label>
</asp:TableCell>
            <asp:TableCell style="border:1px solid">
            <asp:DropDownList ID="DropDownList15" runat="server" Height="26px" Width="130px">
            <asp:ListItem Text="--स्थति--"></asp:ListItem>
               <asp:ListItem Text="हाँ"></asp:ListItem>
                <asp:ListItem Text="नही"></asp:ListItem>
            </asp:DropDownList>
                </asp:TableCell>
            <asp:TableCell style="border:1px solid">
                <asp:TextBox ID="TextBoxR15" runat="server" Width="296px"></asp:TextBox>
            </asp:TableCell>
             </asp:TableRow>
    </asp:Table>
    <table class="boxBody" width="1200px" style="border:1px solid">
        <tr>
           <td></td>
        </tr>
        <tr>
            <td> सुपरवाइज़र के हस्ताक्षर </td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr> <td colspan="11" style="height:20px"> </td></tr> 
        <tr>
            <td></td>
        </tr>
        <tr>
            <td>सुपरवाइ़जर का नाम</td>
        </tr>
        <tr class="pull-right">
            <td>
            <asp:Button ID="btnSave" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" Height="30px" Width="90px" Text="Save" OnClick="btnSave_Click" />
      </td>
                  </tr>
       
        </table>
    
</asp:Content>

