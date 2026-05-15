<%@ Page Language="C#" AutoEventWireup="true" CodeFile="translation.aspx.cs" Inherits="translation" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hindi Textbox Demo</title>
   <script src="https://www.google.com/jsapi" type="text/javascript">
    </script>
    <script language="javascript" type="text/javascript">
        google.load("elements", "1", { packages: "transliteration" });
 </script> 
<script src="https://ajax.googleapis.com/ajax/libs/jquery/2.2.0/jquery.min.js"></script>
<script>
        function onLoad() {
            var options = {
                //Source Language
                sourceLanguage: google.elements.transliteration.LanguageCode.ENGLISH,
                // Destination language to Transliterate
                destinationLanguage: [google.elements.transliteration.LanguageCode.HINDI],
               // shortcutKey: 'ctrl+g',
                transliterationEnabled: true
            };
 
            var control = new google.elements.transliteration.TransliterationControl(options);
           control.makeTransliteratable(['TextBox1']);
           //control.makeTransliteratable(['TextBox2']);
 
        }
       // google.setOnLoadCallback(onLoad);
        google.setOnLoadCallback(onLoad);


       // var control = new google.elements.transliteration.TransliterationControl(options);
        //control.makeTransliteratable(["TextBox2"]);
        //var keyVal = 32; // Space key
        //$("#TextBox1").on('keydown', function (event) {
        //    if (event.keyCode === 32) {
        //        var engText = $("#TextBox1").val() + " ";
        //        var engTextArray = engText.split(" ");
        //        $("#TextBox2").val($("#TextBox2").val() + engTextArray[engTextArray.length - 2]);

        //        document.getElementById("TextBox2").focus();
        //        $("#TextBox2").trigger({
        //            type: 'keypress', keyCode: keyVal, which: keyVal, charCode: keyVal
        //        });
        //    }
        //});

        //$("#TextBox2").bind("keyup", function (event) {
        //    setTimeout(function () { $("#TextBox1").val($("#TextBox1").val() + " "); document.getElementById("TextBox1").focus() }, 0);
        //});
</script>
</head>
<body>
    <form id="form1" runat="server">
   <div>
    <asp:textbox id="TextBox1" runat="server" style="border: 1px solid black; height: 125px; margin-left: auto; width: 550px;" textmode="MultiLine"></asp:textbox>
       
</div>
        <div>
            <asp:Button id="btnConvert" runat="server" OnClick="btnConvert_Click"></asp:Button>
        </div> 
        <div>
            <asp:GridView id="grd" runat="server" AutoGenerateColumns="true">

            </asp:GridView>
        </div> 
</form> 
</body> 
</html> 