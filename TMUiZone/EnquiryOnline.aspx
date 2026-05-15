<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnquiryOnline.aspx.cs" Inherits="EnquiryOnline" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="CSS/Amizone.css" rel="stylesheet" type="text/css" />
    <link href="CSS/Style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/maincss.css" rel="stylesheet" type="text/css" />
    <link href="CSS/ajaxtabs.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/ajaxtabs.js"></script>
    <script src="Scripts/jquery-1.4.1-vsdoc.js"></script>
    <script src="Scripts/jquery-1.4.1.js"></script>
    <script src="Scripts/jquery-1.4.1.min.js"></script>
    <link href="CSS/Ajaxcal.css" rel="stylesheet" />
    <script type="text/javascript" src="bootstrap/js/jquery-1.11.2.min.js"></script>
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="bootstrap/js/bootstrap.min.js"></script>
    <link href="dropdown_one.css" rel="stylesheet" />
    <link href="CSS/mainpage.css" rel="stylesheet" type="text/css" />  
     <link href="CSS/reset.css" rel="stylesheet" type="text/css" />
    <link href="CSS/structure.css" rel="stylesheet" type="text/css" />
   <link rel="shortcut icon" type="image/x-icon" href="logo/Bg.jpg" /> 
    <link href="CSS/style.css" rel="stylesheet" />     
    <link href="bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="Content/alertify/alertify.bootstrap.css" rel="stylesheet" />
    <script src="Scripts/alertify.js"></script>
    <script src="Scripts/alertify.min.js"></script>
    <link href="Content/alertify/alertify.default.css" rel="stylesheet" />
    <link href="Content/alertify/alertify.core.css" rel="stylesheet" />
    

    <script type="text/javascript">

        $(document).ready(function () {
            $('[id$=btnSave]').click(function Save() {

                $('[id$=txtName]').css('border-color', 'red');
                $('[id$=drpCourse]').css('border-color', 'red');
                $('[id$=ddlCity]').css('border-color', 'red');
                $('[id$=txtContactNo]').css('border-color', 'red');
                var a = '';
                if ($('[id$=txtName]').val() == '') { a = 'false'; } else $('[id$=txtName]').css('border-color', '');
                if ($('[id$=drpCourse]').val() == '') { a = "false" } else $('[id$=drpCourse]').css('border-color', '');
                if ($('[id$=ddlCity]').val() == '') { a = "false" } else $('[id$=ddlCity]').css('border-color', '');
                if ($('[id$=txtContactNo]').val().length < 10) { a = "false" } else $('[id$=txtContactNo]').css('border-color', '');
                if (a == "false") {
                    return false;
                }
                debugger
                var arr = {
                    ContactNo: $('[id$=txtContactNo]').val(), Course: $('[id$=drpCourse]').val(), Name: $('[id$=txtName]').val(),
                    City: $('[id$=ddlCity]').val(), Email: $('[id$=txtEmail]').val()
                };
                $.ajax({
                    type: "POST",
                    url: "EnquiryOnline.aspx/Save",
                    data: JSON.stringify(arr),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: SaveEnquiryDetails,
                    error: function (xmlHttpRequest, textStatus, errorThrown) {
                        alert('Error');
                    }
                });
            });
        });

        function SaveEnquiryDetails(data) {
            if (data.d == 'Error') {
                alertify.success('Record All ready Saved');
            }
            else {
                alertify.success(data.d);
                $('[id$=txtContactNo]').val('');
                $('[id$=drpCourse]').val('');
                $('[id$=txtName]').val('');
                $('[id$=ddlCity]').val('');
                $('[id$=txtEmail]').val('');
            }

        }
    </script>
    <style>
       .button
        {  background-color: #ed7600; /*This is your regular color*/
           color:white;
       } 
       input[type = "submit"]:hover
       { 
           
           color:black;
       }
    </style>
    <title>Enquiry</title>
</head>
<body  color:#333; font-family:'helveticaneuemedium'; bgcolor="ACE9FB">
    <form id="form1" runat="server" >
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="background-color: #ACE9FB" class="col-lg-10">
            <div >
                <br />  
                    <div >                      
                      <center> <b><p style="color:black;font-size:25px">Register Your Interest</p></b></center>
                    </div>
                
                <div  >
                    
                    <p>
                       <span> <asp:TextBox ID="txtName" runat="server" CssClass="form-control input-sm" placeholder="Name*" MaxLength="50" Width="100%"></asp:TextBox></span>                       
                        
                    </p>
                   <p><div class="clearfix"></div></p>
                    <p>
                       <span> <asp:DropDownList ID="drpCourse" CssClass="form-control input-sm" placeholder="Course" runat="server"></asp:DropDownList></span>                        
                    </p>
                    
                    <p>
                      <span>  <asp:DropDownList ID="ddlCity" CssClass="form-control input-sm" placeholder="Select City" runat="server"></asp:DropDownList></span>                        
                    </p>
                     <p>
                       <span>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control input-sm" placeholder="E-Mail ID" MaxLength="80" Width="100%"></asp:TextBox>                                          
                       </span>                       
                    </p>
                     <p><div class="clearfix"></div></p>
                        <table style="width:100%">                           
                            <tr>
                                <td><asp:Label ID="txtcontactPrefix" runat="server" Text="+91" CssClass="form-control input-sm"></asp:Label></td>
                                <td><asp:TextBox ID="txtContactNo" runat="server" MaxLength="10" CssClass="form-control input-sm" placeholder="10 Digit Contact No*"></asp:TextBox>
                                 <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtContactNo" FilterType="Numbers, Custom"></asp:FilteredTextBoxExtender></td>
                              
                           <td >
                               <div class="pull-right">
                               &nbsp&nbsp&nbsp <asp:Button ID="btnSave" runat="server" BackColor="#ed7600"  ValidationGroup="g1" Text="Submit" CssClass="btn btn-info" Height="30px" Width="93px" OnClientClick="return false" />
                                   </div>
                                    </td>

                            </tr>
                            <tr style="height:8px">
                                <td></td>
                            </tr>
                        </table>
                      <p>
                          <div> <asp:RegularExpressionValidator ID="revEmailID" runat="server" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" SetFocusOnError="true" ValidationGroup="g1">* Invalid email address.</asp:RegularExpressionValidator> </div>
                      </p>
                        
                    
                    <div>
                       
                    </div>
                </div>
              
            </div>
        </div>
       
    </form>
</body>
</html>

