<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Forgotpassword.aspx.cs" Inherits="Forgotpassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/stylesforgotpassword.css" rel="stylesheet" type="text/css" />
</head>
<body style="background:#094e95;">
    <form id="form1" runat="server">
    <div id="sdf">
<div id="inline1">
<div id="modal-loginbox2">
<div id="contents">
<h2 class="brown" align="center">Password Recovery</h2></p>

<div class="form-elements2">
<div class="label-txt">User Name</div>   <input name="txtCode" type="text" id="txtCode" /><br />
          <span id="RequiredFieldValidator3" style="color:Red;display:none;"></span>
</div>
<div class="form-elements2">
<div >
    <%--<img id="img2" src="JpegImage.aspx?random=635834412371042000" style="border-width:0px;" />--%>
    <div class="label-txt">Code</div> 
    <div class="label-txt">xyz</div> 
      <%--<input name="lblcode" type="text"  id="Text1" /><br />--%>
    
</div>
    
</div>
<div class="form-elements2">
<div class="label-txt">&nbsp;</div> &nbsp;<span class="brown">Enter the code shown above:</span>
<div class="label-txt">&nbsp;</div>  <input name="txtCap" type="text" maxlength="10" id="txtCap" />
            <span id="RequiredFieldValidator1" style="color:Red;display:none;"></span>
  
</div>
<br clear="all" /><br />
<div class="label-txt">&nbsp;</div>

<input type="submit" name="btnSubmit" value="Get Password" onclick="javascript:WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions(&quot;btnSubmit&quot;, &quot;&quot;, true, &quot;Valid&quot;, &quot;&quot;, false, false))" id="btnSubmit" />

             <br />
             <div id="vs" style="color:Red;display:none;">

</div><br /><br />
              <span id="lblMessage" style="color:Red;"></span>
</div>
<div id="bottom"></div>
</div>		
</div>
</div>

    
    </form>
</body>
</html>
