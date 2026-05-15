<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PatientLogin.aspx.cs" Inherits="PatientLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login Form</title>

 
    <style>
        body {
            font-family: Arial, Helvetica, sans-serif;
        }
        form {
            border: 3px solid #f1f1f1;
        }
        input[type=text], input[type=password] {
            width: 100%;
            padding: 12px 20px;
            margin: 8px 0;
            display: inline-block;
            border: 1px solid #ccc;
            box-sizing: border-box;
        }
        button:hover {
            opacity: 0.8;
        }
        .cnbtn {
            background-color: #ec3f3f;
            color: white;
            padding: 14px 20px;
            margin: 8px 0;
            border: none;
            cursor: pointer;
            width: 49%;
        }
        .lgnbtn {
            background-color: #4CAF50;
            color: white;
            padding: 14px 20px;
            margin: 8px 0;
            border: none;
            cursor: pointer;
            width: 50%;
        }
        .imgcontainer {
            text-align: center;
            margin: 24px 0 12px 0;
        }
        img.avatar {
            width: 60%;
            border-radius: 50%;
        }
        .container {
            padding: 16px;
        }
        span.psw {
            float: right;
            padding-top: 16px;
        }
        /* Change styles for span and cancel button on extra small screens */
        @media screen and (max-width: 300px) {
            span.psw {
                display: block;
                float: none;
            }
            .cnbtn {
                width: 100%;
            }
        }
        .frmalg {
            margin: auto;
            width: 60%;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server" class="frmalg" style="margin-top:50px">

        <div class="container" >
            <table>
                <tr >
                    <td>
                         <asp:Image ID="imglogo" ImageUrl="~/images/rightlogo.png" Height="80px" Width="80px" runat="server" />
                    </td>
                    <td style="width:85px">

                    </td>
                    <td style="text-align:right">
                        <h3>Patient Login </h3>
                    </td>
                </tr>
               
            </table>
           
          
            <label for="uname"><b>Username</b></label>
            <asp:TextBox runat="server" ID="txt_Username" placeholder="Enter Username"></asp:TextBox>
            <label for="psw"><b>Password</b></label>
            <asp:TextBox runat="server" ID="txt_password" TextMode="Password" placeholder="Enter Password"></asp:TextBox>
            <asp:Label ID="lblError" Width="60%" ForeColor="Red" runat="server"></asp:Label>
            <asp:Button runat="server" ID="btn_Login" CssClass="lgnbtn" Text="Login" OnClick="btn_Login_Click" />

 
            <asp:Button runat="server" ID="btn_cancel" Text="Cancel"  OnClick="btn_cancel_Click" class="cnbtn" />
        </div>
    </form>
</body>
</html>