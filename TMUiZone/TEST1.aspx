<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TEST1.aspx.cs" Inherits="TEST1" %>

<!DOCTYPE html>
<html>

<div id="content">
    <h3>Hello, this is a H3 tag</h3>

    <p>A paragraph</p>
</div>
<div id="editor"></div>
<button id="cmd">generate PDF</button>

<!--import the script-->
<script src = "https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.4/jspdf.min.js"></script>

<!--the script-->
<script>

    var doc = new jsPDF();
    var specialElementHandlers = {
        '#editor': function (element, renderer) {
            return true;
        }
    };

    $('#cmd').click(function () {
        doc.fromHTML($('#content').html(), 15, 15, {
            'width': 170,
            'elementHandlers': specialElementHandlers
        });
        doc.save('sample-file.pdf');
    });
</script>
</html>
