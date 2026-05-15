<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fontconvert.aspx.cs" Inherits="fontconvert" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="https://www.google.com/jsapi">
    </script>
  <script src="https://www.google.com/jsapi" type="text/javascript">  
</script>  
<script type="text/javascript">
    google.load("elements", "1", {
        packages: "transliteration"
    });

    function onLoad() {

        document.getElementById("TxtSH").value = document.getElementById('<%= txtNameInKannada.ClientID %>').value;
       
        var options = {
            sourceLanguage: google.elements.transliteration.LanguageCode.ENGLISH,
            destinationLanguage: [google.elements.transliteration.LanguageCode.HINDI],
            shortcutKey: 'ctrl+m',
            transliterationEnabled: true
        };

        var control = new google.elements.transliteration.TransliterationControl(options);
        control.makeTransliteratable(['TxtSH']);
        

    }
    google.setOnLoadCallback(onLoad);
</script>
   
</head>
<Form runat="server">
    <asp:TextBox ID="txtNameInKannada" runat="server"></asp:TextBox>
    <input type="text" name="SHname" id="TxtSH" width:260px;   oncopy="return false;" onpaste="return false;" maxlength="50" />
</Form>
</html>
