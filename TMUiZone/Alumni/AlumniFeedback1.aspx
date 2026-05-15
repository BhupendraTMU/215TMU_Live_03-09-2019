<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/IndexMaster.master" AutoEventWireup="true" CodeFile="AlumniFeedback1.aspx.cs" Inherits="Alumni_AlumniFeedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .chkChoiceRadio input {
            margin-left: 15px;
            height: 15px;
            width: 18px;
        }

        .chkChoiceRadio td {
            padding-left: 20px;
            /*background-color: #f4ecce;*/
        }

        .chkChoiceRadio1 input {
            margin-left: 15px;
            height: 15px;
            width: 18px;
        }

        .chkChoiceRadio1 td {
            padding-left: 20px;
            /*background-color: #f4ecce;*/
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="width: 100%; margin-bottom: 10px; border: 2px solid">
        <div style="width: 100%; margin-bottom: 10px; margin-left: 1%; margin-right: 1%; margin-top: 5px; border: 1px; border-color: black; table-layout: fixed;">
            <table style="width: 98%;">
                <tr>
                    <td style="height: 50px; text-align: center">
                        <asp:Image ID="Image1" runat="server" ImageUrl="../logo/logo123.png" /></td>
                </tr>
            </table>
            <br />
            <table style="width: 98%;">
                <tr>
                    <td style="border: 1px solid; height:15px;width:50%"><P style="font-size:large;color:black">Alumni Name :<asp:Label ID="lblAlumniName"  runat="server"  Text=""></asp:Label></P></td>
                  
                    <td style="border: 1px solid; height: 15px;width:50%" ><P style="font-size:large;color:black;height:50px">Designation/Business Type/<br />Higher Studies Institute : <asp:Label ID="lblCurrOrgWthDesg" runat="server" Text=""></asp:Label></P></td>
                </tr>

                  <tr>
                    <td style="border: 1px solid; height:15px;width:50%"><P style="font-size:large;color:black">Programme with specialization :<asp:Label ID="lblProgWthSpc" runat="server" Text=""></asp:Label></P></td>
                  
                    <td style="border: 1px solid; height: 15px;width:50%" ><P style="font-size:large;color:black;height:50px">Employer/Company Name/<br />Higher Studies Program/Others  :<asp:Label ID="lblDesg"  runat="server"  Text=""></asp:Label></P></td>
                </tr>
                <tr>
                    <td style="border: 1px solid; height: 15px;width:50%"><P style="font-size:large;color:black">Year of passing out :<asp:Label ID="lblYearPass" runat="server" Text=""></asp:Label></P></td>
                    <td style="border: 1px solid; height: 15px;width:50%"><P style="font-size:large;color:black">Email :<asp:Label ID="lblEmail" runat="server" Text=""> </asp:Label></P></td>
                </tr>
                <tr>
                    <td style="border: 1px solid; height: 15px;width:50%" colspan="2"><P style="font-size:large;color:black">Mobile:<asp:Label ID="lblMob" runat="server" Text=""></asp:Label></P></td>
                   <%-- <td style="border: 1px solid; height: 15px;width:50%"><P style="font-size:large;color:black">
                    </P></td>--%>
                </tr>
            </table>
            <br />
            <asp:HiddenField ID="hfcount" runat="server" />
            <asp:HiddenField ID="hfcountYesNo" runat="server" />
            <table style="width: 98%;">
                <tr>
                    <td style="border: 1px solid; height: 15px; width: 50%; text-align:center"><P style="font-size:large;color:black">Attributes</P></td>
                    <td style="border: 1px solid; height: 15px; width: 50%;"></td>
                </tr>
                <tr>
                    <td colspan="7" style="width: 100%;">
                        <asp:Repeater ID="grdaddedEmployee" runat="server" OnItemDataBound="grdaddedEmployee_ItemDataBound">
                            <ItemTemplate>
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 5%; border: 1px solid">
                                            <asp:Label ID="lblQuestionNo" runat="server" Text='<%# Eval("QuestionCode") %>'></asp:Label>
                                            <asp:HiddenField ID="hfSequence" runat="server" Value='<%# Eval("Sequence") %>' />
                                             <asp:HiddenField ID="HfQuestionNo" runat="server" Value='<%# Eval("[Question No]") %>' />
                                            <asp:HiddenField ID="HfRequired" runat="server" Value='<%# Eval("[Feedback Required]") %>' />
                                        </td>
                                        <td style="border: 1px solid; width: 45%;">
                                            <asp:Label ID="lblQuestions" runat="server" Text='<%# Eval("Questions") %>'></asp:Label></td>
                                        <td style="border: 1px solid; height: 15px; width: 50%;">
                                            <asp:RadioButtonList ID="ddlRating1" runat="server" Enabled='<%# Eval("DisableEnable") %>' Visible='<%# Eval("ChkEnable") %>' onclick="ShowHideDiv(this)"  RepeatDirection="Horizontal" CssClass="chkChoiceRadio" >
                                                <asp:ListItem Value="1" Text="Excellent"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="V. Good"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Good"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="Average"></asp:ListItem>
                                                <asp:ListItem Value="5" Text="Poor"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
            </table>
            <br />
            <table style="width: 98%;">
                <tr>
                    <td style="border: 1px solid; height: 15px; text-align: center; width:50%;"><p style="font:bolder;color:black;font-size:large">Attributes<p></td>
                    <td style="border: 1px solid; height: 15px; width:50%;"></td>
                </tr>
                <tr>
                    <td colspan="3" style="width: 100%; ">
                        <asp:Repeater ID="rptrYesNo" runat="server" OnItemDataBound="rptrYesNo_ItemDataBound">
                            <ItemTemplate>
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 5%; border: 1px solid">
                                            <asp:Label ID="lblrptrYesNoQuestionNo" runat="server" Text='<%# Eval("QuestionCode") %>'></asp:Label>
                                            <asp:HiddenField ID="hfrptrYesNoSequence" runat="server" Value='<%# Eval("Sequence") %>' />
                                        </td>
                                        <td style="border: 1px solid; width: 45%;">
                                            <asp:Label ID="lblrptrYesNoQuestions" runat="server" Text='<%# Eval("Questions") %>'></asp:Label></td>
                                        <td style="border: 1px solid; width: 50%;text-align: center;">
                                            <asp:RadioButtonList ID="ddrptrYesNo" runat="server" Enabled='<%# Eval("DisableEnable") %>'  RepeatDirection="Horizontal" CssClass="chkChoiceRadio">
                                                <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
            </table>
            <br />
            <table style="width: 98%;">
                <tr>
                    <td style="border: 1px solid; height: 15px; text-align: center; width:50%"><P style="font-size:large;color:black">Comment & Suggestions</P></td>
                    <td style="border: 1px solid; height: 15px;width:50%"></td>
                </tr>
                <tr>
                    <td colspan="3" style="border: 1px solid; height: 15px;">
                        <asp:Repeater ID="rptrCommnt" runat="server" OnItemDataBound="rptrCommnt_ItemDataBound">
                            <ItemTemplate>
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 5%; border: 1px solid">
                                            <asp:Label ID="lblrptrCommntQuestionNo" runat="server" Text='<%# Eval("QuestionCode") %>'></asp:Label>
                                            <asp:HiddenField ID="hfrptrCommntSequence" runat="server" Value='<%# Eval("Sequence") %>' />
                                        </td>
                                        <td style="border: 1px solid; width: 45%;">
                                            <asp:Label ID="lblrptrCommntQuestions" runat="server" style="width: 100%" Text='<%# Eval("Questions") %>'></asp:Label></td>
                                        <td style="border: 1px solid; width: 50%;">
                                            <asp:TextBox ID="txtrptrCommnt" style="width: 100%" Enabled='<%# Eval("DisableEnable") %>' runat="server" MaxLength="100"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
           
            </table>
            <br />
            <table style="width: 98%;">
                <tr>
                    <td style="height: 15px; text-align: right">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="return rdoBtnCount();" /></td>
                </tr>
            </table>
            <br />
            <table style="width: 98%;">
                <tr>
                    <td><P style="font-size:large;color:black">Date: <asp:Label ID="lblfDate" runat="server"></asp:Label></P></P></td>
                    <td><P style="font-size:large;color:black"></td>
                </tr>
            </table>
            <br />
            <table style="width: 98%;">
                <tr>
                    <td style="height: 15px; text-align: right">
                        <p style="font-family: Arial; font-size: 12px; color: black"><b>Alumni Feedback Form (<asp:Label ID="lblAF" runat="server"></asp:Label>) </b></p>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <script type="text/javascript">
        //function ShowHideDiv(Id) {
        //    alert(Id.ClientID);
        //    var chkYes = document.getElementById("ddlRating1");
            
           
        //}
        function rdoBtnCount() {
            var count = document.getElementById('<%= hfcount.ClientID %>').value
            var RadioCount = 0;
            var count1 = count;
            for (var i = 0; i < count; i++) {
               
                if(document.getElementById("ContentPlaceHolder1_grdaddedEmployee_HfRequired_" + i).value==1)
                {
                    //alert('bhupii yes');
                    for (var j = 0; j < 5; j++) {
                        if (document.getElementById("ContentPlaceHolder1_grdaddedEmployee_ddlRating1_" + i + "_" + j + "_" + i).checked  ) {
                            RadioCount = RadioCount + 1;
                        }
                    }
                }
                else
                {
                    count1 = count1 - 1;
                }
            }
           
            var countYesNo = document.getElementById('<%= hfcountYesNo.ClientID %>').value
            var RadioCountYesNo = 0;
            for (var i = 0; i < countYesNo; i++) {

                for (var j = 0; j < 2; j++) {
                    if (document.getElementById("ContentPlaceHolder1_rptrYesNo_ddrptrYesNo_" + i + "_" + j + "_" + i).checked) {
                        RadioCountYesNo = RadioCountYesNo + 1;
                    }
                }
            }
            debugger
            if (parseInt(count1) != parseInt(RadioCount)) {
                alert("Please mark all rating question.")
                return false;
            }
            else {
                debugger
                if (parseInt(countYesNo) != parseInt(RadioCountYesNo)) {
                    alert("Please mark all yes no question.")
                    return false;
                }
                else {
                    return true;
                }
            }

        }
    </script>
</asp:Content>

