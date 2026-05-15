<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="AppraisalForm.aspx.cs" Inherits="Faculty_feedbackfaculty" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        input[type="text"] {
            font-family: "open Sans";
            font-size: 13px;
            float: left;
            padding: 0px;
            border: 1px solid #e1e1e1;
        }
    </style>

    <script type="text/javascript">

        function PrintDiv() {

            var divToPrint = document.getElementById('printarea');

            var popupWin = window.open('', '_blank', 'width=300,height=400,location=no,left=200px, margin:0mm');
            popupWin.document.open();
            popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
            popupWin.document.close();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <div>
                 Academic Year :  <asp:DropDownList ID="drpAcademic" Width="150px" Height="20px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpAcademic_SelectedIndexChanged" ></asp:DropDownList>
                                    
                </div>
    <br />
    <div style="width: 100%; margin-bottom: 10px; border: 2px solid">
        <asp:ScriptManager ID="ty" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="fe" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
               
                <div id="printarea" >

                    <div style="width: 100%; margin-bottom: 10px; margin-left: 1%; margin-right: 1%; margin-top: 5px;">

                        <table style="width: 98%;">
                            <tr>
                                <td style="width: 1%"></td>
                                <td style="width: 12%; text-align: left">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/rightlogo.png" Width="55%" />
                                </td>
                                <td style="width: 65%; text-align: center">
                                    <strong>
                                        <asp:Label ID="lblCName" Font-Size="Large" Text="Teerthanker Mahaveer University, Moradabad" runat="server"></asp:Label></strong>
                                    <br />
                                    <strong>
                                        <asp:Label ID="lblAC" runat="server" Text="(Established under Govt. of U. P. Act No. 30, 2008)"></asp:Label></strong>

                                    <br />
                                    <strong>
                                        <asp:Label ID="LblType" runat="server" Text="Delhi Road, Moradabad (U.P)"></asp:Label>
                                    </strong>
                                    <br />

                                </td>
                                <td style="width: 10%; text-align: center"></td>
                            </tr>
                            <tr>
                                <td colspan="4" style="width: 90%; margin-right: 10%; border: 2px solid;"></td>

                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center; width: 90%; height: 10px;"></td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center; width: 90%;">
                                    <p style="font-family: Arial; font-size: 12px; color: black">
                                        <b>Academic Session:
                                        <asp:Label ID="lblAcad" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="Label1" runat="server" Text="20-21"></asp:Label>
                                        </b>
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center; width: 90%; height: 10px;"></td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center; width: 90%;">
                                    <p style="font-family: Arial; font-size: 20px; color: black"><b><u>PMS – Teaching Faculty (TMU)</u></b></p>
                                </td>
                            </tr>
                        </table>


                    </div>
                    <div style="width: 100%; margin-bottom: 10px; margin-left: 1%; margin-right: 1%;">




                        <table style="width: 96%; margin-left: 2%; margin-right: 2%;">

                            <tr>
                                <td colspan="4" style="width: 98%">
                                    <p><b>Purpose:</b> The purpose of PMS is to facilitate the individuals to perform to one's potential through direction guidance, support & review. The performance evaluation system is developed to be flexible transparent & objective to support the performance.</p>
                                    <p style="text-align: center">The teaching faculty will be evaluated on the following relevant criteria :</p>
                                </td>

                            </tr>
                            <tr>
                                <td colspan="3" style="width: 49%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black">
                                        <b><u>Components:</u></b>
                                </td>
                                <td colspan="2" style="width: 49%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b><u>Points</u></b></p>
                                </td>

                            </tr>

                            <tr>
                                <td colspan="3" style="width: 49%; font: bold">A Teaching</td>
                                <td colspan="2" style="width: 49%;">(160)</td>

                            </tr>

                            <tr>
                                <td colspan="3" style="width: 49%; font: bold">B Research & Publication</td>
                                <td colspan="2" style="width: 49%;">(60)</td>

                            </tr>

                            <tr>
                                <td colspan="3" style="width: 49%; font: bold">C Institution building</td>
                                <td colspan="2" style="width: 49%;">(20)</td>

                            </tr>
                            <tr>
                                <td colspan="3" style="width: 49%; font: bold">D Self development</td>
                                <td colspan="2" style="width: 49%;">(15)</td>

                            </tr>
                            <tr>
                                <td colspan="3" style="width: 49%; font: bold">E Project, Consultancy & MDPs</td>
                                <td colspan="2" style="width: 49%;">(15)</td>

                            </tr>

                            <tr>
                                <td colspan="3" style="width: 49%; font: bold">F Per/industry/institutional connect</td>
                                <td colspan="2" style="width: 49%;">(10)</td>

                            </tr>

                            <tr>
                                <td colspan="3" style="width: 49%; font: bold">G Student support & counselling</td>
                                <td colspan="2" style="width: 49%;">(10)</td>

                            </tr>

                            <tr>
                                <td colspan="3" style="width: 49%; font: bold">H Conferences &/seminars / Workshops / Events organised & Participation</td>
                                <td colspan="2" style="width: 49%;">(10)</td>

                            </tr>
                            <tr>
                                <td colspan="3" style="width: 49%; font: bold"></td>
                                <td colspan="3" style="width: 49%; font: bold">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>Total (300)</b></p>
                                </td>


                            </tr>


                        </table>


                        <table style="margin-left: 2%; border: 1px black; width: 98%">
                            <tr>
                                <td colspan="4" style="width: 78%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black; margin-left: 2%;"><b>Part-I</b></p>
                                </td>
                            </tr>
                            <tr>

                                <td colspan="4" style="width: 96%; height: 10px;"></td>
                            </tr>


                            <tr>
                                <td style="width: 2%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>A Teaching</b></p>
                                </td>
                                <td style="width: 40%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b></b></p>
                                </td>
                                <td style="width: 7%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>(160)</b></p>
                                </td>
                                <td style="width: 49%;">
                                    <table style="border: 1px; border-color: black; table-layout: fixed; width: 85%">
                                        <tr>
                                            <td style="border: 1px solid;">
                                                <p style="margin-left: 2px; margin-right: 2px;">Targets (In points</p>
                                            </td>
                                            <td style="border: 1px solid;">
                                                <p style="margin-left: 2px; margin-right: 2px;">Self Assessment (Actual points earned)</p>
                                            </td>
                                            <td style="border: 1px solid;">
                                                <p style="margin-left: 2px; margin-right: 2px;">Points post moderation (Based on evidence provided)</p>
                                            </td>
                                        </tr>


                                    </table>


                                </td>


                            </tr>


                            <tr>

                                <td colspan="4" style="width: 96%; height: 10px;"></td>
                            </tr>
                            <tr>
                                <td style="width: 2%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b><i>i)</i> Courses taught (Max 30 credits load)</b></p>
                                </td>

                                <td style="width: 40%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b></b></p>
                                </td>
                                <td style="width: 7%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>(120)</b></p>
                                </td>
                                <td style="width: 49%;"></td>
                            </tr>

                            <tr>
                                <td colspan="2" style="width: 49%;">4 points for each credit taught  including practical(lab)  & tutorial     </td>
                                <td style="width: 7%;"></td>
                                <td style="width: 49%;">
                                    <table style="border: 1px; border-color: black; table-layout: fixed" width="85%">
                                        <tr>
                                            <td style="border: 1px solid; height: 15px; width: 32%;">
                                                <asp:TextBox ID="txtct1" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px; width: 32%;">
                                                <asp:TextBox ID="txtct2" Width="100%" MaxLength="3" runat="server" onkeypress="return onlyNumbers(event)" Enabled="false" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px; width: 32%;">
                                                <asp:TextBox ID="txtct3" Width="100%" MaxLength="3" runat="server" Enabled="false" onkeypress="return onlyNumbers(event)"></asp:TextBox></td>
                                        </tr>


                                    </table>


                                </td>


                            </tr>
                            <tr>

                                <td colspan="4" style="width: 96%; height: 10px;"></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 42%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black">
                                        <b>Students feedback (Use average of the two multipliers from                     
following a &b  criteria for specific course)
                                        </b>
                                    </p>
                                </td>

                                <td style="width: 7%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b></b></p>
                                    <%-- (18)--%>
                                </td>
                                <td style="width: 49%;"></td>
                            </tr>
                            <tr>
                                <td colspan="4" style="width: 98%; height: 10px;"></td>

                            </tr>

                            <tr>
                                <td style="width: 22%;" colspan="2">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b><i>a) Multiplier factor for course based  on student feedback taken on a scale of 1-5</i></b></p>
                                </td>
                                <%--<td style="width:40%;"><P style="font-family:Arial;font-size:15px;color:black"><b></b></P></td>--%>
                                <td style="width: 7%;"></td>
                                <td colspan="2" style="width: 49%;"></td>
                            </tr>
                            <tr>
                                <td style="width: 42%" colspan="2">
                                    <table>
                                        <tr>
                                            <td style="width: 100%; height: 20px">1 multiplier if feedback score is  > 4</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">.9 multiplier if feedback score is  3.5 - 4</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">.8 multiplier if feedback score is  3 < 3.5</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">.7 multiplier if feedback score is  2.5< 3</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">.6 multiplier if feedback score is  2< 2.5</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">.5 multiplier if feedback score is  1.5< 2</td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="width: 98%; height: 5px;"></td>

                                        </tr>
                                        <tr>

                                            <td style="width: 22%;" colspan="2">
                                                <p style="font-family: Arial; font-size: 15px; color: black"><b><i>b) Multiplier factor   per course for student result</i></b></p>
                                            </td>
                                            <%--<td style="width:40%;"><P style="font-family:Arial;font-size:15px;color:black"><b></b></P></td>--%>
                                            <td style="width: 7%;"></td>
                                            <td colspan="2" style="width: 49%;"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">1 multiplier if average pass result of the course > 90%</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">.9 multiplier if average pass result of the course  81%-90%</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">. 8 multiplier if average pass result of the course  71%-80%</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">.7 multiplier if average pass result of the course  61%-70%</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">.6 multiplier if average pass result of the course  51%-60%</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">.5  multiplier if average pass result of the course  41%-50%</td>
                                        </tr>
                                        <td colspan="4" style="width: 98%; height: 20px;"></td>
                                    </table>


                                </td>
                                <td style="width: 7%;"></td>
                                <td style="width: 49%" colspan="2">
                                    <table style="border: 1px; border-color: black; table-layout: fixed; width: 85%">
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf11" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf12" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf13" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf21" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf22" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf23" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                        </tr>

                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf31" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf32" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf33" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf41" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf42" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf43" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf51" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf52" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf53" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf61" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf62" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf63" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                        </tr>

                                    </table>
                                    <br />
                                    <table style="border: 1px; border-color: black; table-layout: fixed; width: 85%">
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf64" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf65" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf66" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf67" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf68" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf69" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                        </tr>

                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf70" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf71" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf72" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf73" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf74" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf75" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf76" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf77" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf78" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf79" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf80" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsf81" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                        </tr>

                                    </table>

                                </td>

                            </tr>


                            <tr>
                                <td colspan="4" style="width: 98%; height: 10px;"></td>

                            </tr>
                            <tr>
                                <td style="width: 42%;" colspan="2">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>ii) Innovative pedagogy</b></p>
                                </td>
                                <%--<td style="width:40%;"><P style="font-family:Arial;font-size:15px;color:black"><b></b></P></td>--%>
                                <td style="width: 7%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>(16)</b></p>
                                </td>
                                <td style="width: 49%;"></td>

                            </tr>
                            <tr>
                                <td colspan="2">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>points based on innovations (experiential learning methods) made in classroom delivery.</b></p>
                                </td>
                                <td style="width: 7%;"></td>
                                <td style="width: 49%;" colspan="2"></td>
                            </tr>

                            <tr>

                                <td colspan="2" style="width: 42%;">
                                    <table>
                                        <tr>
                                            <td>2 Points for every one guest lecture invited from top 50 institutions from NIRF or A+</td>
                                        </tr>
                                        <tr>
                                            <td>or higher accredited institutes (also IIMs, IITs & other equivalent institutions)</td>
                                        </tr>
                                        <tr>
                                            <td>2 Points for every one guest lecture invited from top firms</td>
                                        </tr>
                                        <tr>
                                            <td>2 points for delivery using recognised & proved innovative teaching methods</td>
                                        </tr>

                                    </table>


                                </td>
                                <td style="width: 7%;"></td>
                                <td colspan="2" style="width: 49%;">



                                    <table style="border: 1px; border-color: black; table-layout: fixed; width: 85%">
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtip11" Width="100%" Height="20px" Enabled="false" MaxLength="2" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtip12" Width="100%" Enabled="false" MaxLength="2" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtip13" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtip21" Width="100%" Height="20px" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtip22" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtip23" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                        </tr>

                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtip31" Width="100%" Height="20px" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtip32" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtip33" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                        </tr>


                                    </table>

                                </td>

                            </tr>

                            <tr>
                                <td colspan="4" style="width: 98%; height: 10px;"></td>

                            </tr>
                            <tr>
                                <td style="width: 42%;" colspan="2">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>iii) New courses / programmes developed</b></p>
                                </td>

                                <td style="width: 7%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>(5)</b></p>
                                </td>
                                <td style="width: 49%;"></td>
                            </tr>
                            <tr>

                                <td colspan="2" style="width: 42%;">
                                    <table>
                                        <tr>
                                            <td style="width: 100%; height: 20px">1 Point for developing a completely new course</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">2 Points for contribution in developing or modifying a new programme</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">1 Point for revising the syllabus of a course</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">1 point for developing or revising an experiment</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">1 point for developing lab manual</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">1 point for developing an FDP / MDP</td>
                                        </tr>


                                    </table>


                                </td>

                                <td style="width: 7%;"></td>
                                <td colspan="2" style="width: 49%;">



                                    <table style="border: 1px; border-color: black; table-layout: fixed; width: 85%">
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtNc11" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtNc12" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtNc13" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtNc21" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtNc22" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtNc23" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                        </tr>

                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtNc31" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtNc32" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtNc33" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtNc34" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtNc35" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtNc36" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtNc37" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtNc38" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtNc39" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                        </tr>

                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtNc40" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtNc41" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtNc42" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                        </tr>


                                    </table>

                                </td>

                            </tr>







                            <tr>
                                <td colspan="4" style="width: 98%; height: 10px;"></td>

                            </tr>
                            <tr>



                                <td style="width: 42%;" colspan="2">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>iv) Contribution as member of BOS, BOM, Senate/ Academic council, Curriculum development team etc.</b></p>
                                </td>

                                <td style="width: 7%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>(3)</b></p>
                                </td>
                                <td style="width: 42%;"></td>

                            </tr>


                            <tr>

                                <td style="width: 42%;" colspan="2">
                                    <table>
                                        <tr>
                                            <td>3 Points for being member of any of the above body either in TMU or on external institution</td>
                                        </tr>






                                    </table>
                                </td>
                                <td style="width: 7%;"></td>
                                <td colspan="2" style="width: 49%;">



                                    <table style="border: 1px; border-color: black; table-layout: fixed; width: 85%">
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtcsm11" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" MaxLength="2" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtcsm12" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" MaxLength="2" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtcsm13" onkeypress="return onlyNumbers(event)" Enabled="false" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtcsm21" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" MaxLength="2" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtcsm22" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" MaxLength="2" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtcsm23" onkeypress="return onlyNumbers(event)" Enabled="false" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>




                                    </table>

                                </td>

                            </tr>




                            <tr>
                                <td colspan="4" style="width: 98%; height: 10px;"></td>

                            </tr>
                            <tr>



                                <td style="width: 42%;" colspan="2">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>v) Project work supervised</b></p>
                                </td>

                                <td style="width: 7%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>(16)</b></p>
                                </td>
                                <td style="width: 42%;"></td>

                            </tr>


                            <tr>

                                <td style="width: 42%;" colspan="2">
                                    <table>
                                        <tr>
                                            <td style="width: 100%; height: 20px">2 points for every field project supervised</td>
                                        </tr>

                                        <tr>
                                            <td style="width: 100%; height: 20px">3 points for every industry project (real time problem) supervised</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">4 points for every project entered & won any first three positions in a competition (irrespective of no. of students supervised)</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">2 points for every project of the student that has entered in the competition</td>
                                        </tr>

                                    </table>
                                </td>
                                <td style="width: 7%;"></td>
                                <td colspan="2" style="width: 49%;">



                                    <table style="border: 1px; border-color: black; table-layout: fixed; width: 85%">
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpws11" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" MaxLength="2" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpws12" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" MaxLength="2" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpws13" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpws21" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" MaxLength="2" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpws22" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" MaxLength="2" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpws23" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>


                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpws31" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" MaxLength="2" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpws32" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" MaxLength="2" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpws33" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpws34" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" MaxLength="2" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpws35" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" MaxLength="2" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpws36" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>




                                    </table>

                                </td>

                            </tr>


                            <tr>
                                <td colspan="4" style="width: 98%; height: 10px;"></td>

                            </tr>
                            <tr>
                                <td style="width: 2%;"></td>
                                <td style="width: 47%;"></td>
                                <td style="width: 7%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>Total for 'A</b></p>
                                </td>
                                <td colspan="2" style="width: 49%;">



                                    <table style="border: 1px; border-color: black; table-layout: fixed; width: 85%">
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpwA11" onkeypress="return onlyNumbers(event)" Width="100%" MaxLength="2" Enabled="false" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpwA12" onkeypress="return onlyNumbers(event)" Width="100%" MaxLength="2" Enabled="false" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpwA13" onkeypress="return onlyNumbers(event)" Width="100%" MaxLength="2" Enabled="false" runat="server"></asp:TextBox></td>
                                        </tr>





                                    </table>

                                </td>

                            </tr>

                            <tr>
                                <td colspan="4" style="width: 98%; height: 10px;"></td>

                            </tr>
                            <tr>
                                <td style="width: 42%;" colspan="2">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>B Research & Publication</b></p>
                                </td>

                                <td style="width: 7%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>(60)</b></p>
                                </td>
                                <td colspan="2" style="width: 49%;"></td>



                            </tr>



                            <tr>

                                <td style="width: 42%;" colspan="2">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>i) Papers & Books published</b></p>
                                </td>
                                <td style="width: 7%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>(30)</b></p>
                                </td>
                                <td colspan="2" style="width: 49%;"></td>



                            </tr>

                            <tr>

                                <td style="width: 42%;" colspan="2">
                                    <table>
                                        <tr>
                                            <td style="width: 100%; height: 20px">10 points for each paper in indexed Journals (scopus, web of science, pubmed & ICI)</td>
                                        </tr>

                                        <tr>
                                            <td style="width: 100%; height: 20px">6 points for Paper published in UGC listed journals</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">3 points  for every one article in National magazine or newspaper</td>
                                        </tr>

                                        <tr>
                                            <td style="width: 100%; height: 20px">3 points designing manual, authoring or co-authoring edited book or conference proceedings </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">6 points for book published by national publishers</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">12 points for book published by international publisher</td>
                                        </tr>

                                    </table>
                                </td>
                                <td style="width: 7%;"></td>
                                <td colspan="2" style="width: 49%;">



                                    <table style="border: 1px; border-color: black; table-layout: fixed; width: 85%">
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpp11" Enabled="false" Width="100%" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpp12" Enabled="false" Width="100%" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpp13" Width="100%" onkeypress="return onlyNumbers(event)" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpp21" Enabled="false" Width="100%" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpp22" Enabled="false" Width="100%" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpp23" Width="100%" onkeypress="return onlyNumbers(event)" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>


                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpp31" Enabled="false" Width="100%" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpp32" Enabled="false" Width="100%" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpp33" Width="100%" onkeypress="return onlyNumbers(event)" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpp41" Enabled="false" Width="100%" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpp42" Enabled="false" Width="100%" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpp43" Width="100%" Enabled="false" onkeypress="return onlyNumbers(event)" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpp51" Enabled="false" Width="100%" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpp52" Enabled="false" Width="100%" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpp53" Width="100%" onkeypress="return onlyNumbers(event)" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpp61" Enabled="false" Width="100%" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpp62" Enabled="false" Width="100%" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpp63" Width="100%" onkeypress="return onlyNumbers(event)" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>



                                    </table>

                                </td>

                            </tr>
                            <tr>
                                <td colspan="4" style="width: 98%; height: 10px;"></td>

                            </tr>
                            <tr>

                                <td style="width: 42%;" colspan="2">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>Research supervision </b></p>
                                </td>
                                <td style="width: 7%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black">
                                        <b>(5)</b>
                                </td>
                                <td colspan="2" style="width: 49%;"></td>



                            </tr>

                            <tr>
                                <%--               <td style="width:2%;"><P style="font-family:Arial;font-size:15px;color:black"><b></b></P></td>--%>
                                <td style="width: 42%;" colspan="2">
                                    <table>
                                        <tr>
                                            <td>
                                                <p style="font-family: Arial; font-size: 12px; color: black"><b>ii)</b> (You can't claim twice the points for the same work in the same year) 10 points for every Ph.D awarded</p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">5 points for every Ph D awarded </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">3 points for pre submission presentation completed & approved by CRC</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">2 points for every active registered candidate (have been continually submitting progress report without any break)</td>
                                        </tr>

                                        <tr>
                                            <td style="width: 100%; height: 20px">1 points for PG / M.Phil level dissertation supervised & one paper published out of it</td>
                                        </tr>


                                    </table>
                                </td>
                                <td style="width: 7%;"></td>
                                <td colspan="2" style="width: 49%;">



                                    <table style="border: 1px; border-color: black; table-layout: fixed; width: 85%">
                                        <tr>
                                            <td style="height: 20px"></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrs11" Width="100%" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrs12" Width="100%" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrs13" Width="100%" onkeypress="return onlyNumbers(event)" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrs21" Width="100%" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrs22" Width="100%" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrs23" Width="100%" onkeypress="return onlyNumbers(event)" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>


                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrs31" Width="100%" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrs32" Width="100%" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrs33" Width="100%" onkeypress="return onlyNumbers(event)" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrs41" Width="100%" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrs42" Width="100%" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrs43" Width="100%" onkeypress="return onlyNumbers(event)" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>

                                    </table>

                                </td>

                            </tr>
                            <tr>
                                <td colspan="4" style="width: 98%; height: 10px;"></td>

                            </tr>
                            <tr>

                                <td style="width: 42%;" colspan="2">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>iii) Research paper presented in conferences</b></p>
                                </td>
                                <td style="width: 7%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>(10)</b></p>
                                </td>
                                <td colspan="2" style="width: 49%;"></td>



                            </tr>

                            <tr>

                                <td style="width: 42%;" colspan="2">
                                    <table>
                                        <tr>
                                            <td style="width: 100%; height: 20px">2 points for every research paper presentation  at national conference</td>
                                        </tr>

                                        <tr>
                                            <td style="width: 100%; height: 20px">4 points for every research paper presentation  at international conference</td>
                                        </tr>

                                        <tr>
                                            <td style="width: 100%; height: 20px">4 points for every paper published in conference proceedings</td>
                                        </tr>



                                    </table>
                                </td>
                                <td style="width: 7%;"></td>
                                <td colspan="2" style="width: 49%;">



                                    <table style="border: 1px; border-color: black; table-layout: fixed; width: 85%">
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrpp11" Width="100%" Enabled="false" MaxLength="2" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrpp12" Width="100%" Enabled="false" MaxLength="2" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrpp13" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrpp21" Width="100%" Enabled="false" MaxLength="2" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrpp22" Width="100%" Enabled="false" MaxLength="2" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrpp23" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                        </tr>


                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrpp31" Width="100%" Enabled="false" MaxLength="2" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrpp32" Width="100%" Enabled="false" MaxLength="2" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrpp33" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                        </tr>

                                    </table>

                                </td>

                            </tr>


                            <tr>
                                <td colspan="4" style="width: 98%; height: 10px;"></td>

                            </tr>
                            <tr>

                                <td style="width: 42%;" colspan="2">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>iv) Research project</b></p>
                                </td>
                                <td style="width: 7%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>(15)</b></p>
                                </td>
                                <td colspan="2" style="width: 49%;"></td>



                            </tr>
                            <tr>
                                <td colspan="4" style="width: 98%; height: 10px;"></td>

                            </tr>
                            <tr>

                                <td style="width: 42%;" colspan="2">
                                    <table>
                                        <tr>
                                            <td style="width: 100%; height: 20px">15 points for a patent awarded / published</td>
                                        </tr>

                                        <tr>
                                            <td style="width: 100%; height: 20px">10 points per research project funded by government bodies</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">5 points per research project funded by non - government bodies</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">5 points per research project funded by seed money (University)</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">3 points for publishing technical reports</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">2 points for submitting technical reports</td>
                                        </tr>




                                    </table>
                                </td>
                                <td style="width: 7%;"></td>
                                <td colspan="2" style="width: 49%;">



                                    <table style="border: 1px; border-color: black; table-layout: fixed; width: 85%">
                                        <tr>
                                            <td style="height: 5px"></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrpro11" Width="100%" Enabled="false" MaxLength="2" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrpro12" Width="100%" Enabled="false" MaxLength="2" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrpro13" Width="100%" Enabled="false" MaxLength="2" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrpro21" Width="100%" Enabled="false" MaxLength="2" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrpro22" Width="100%" Enabled="false" MaxLength="2" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrpro23" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                        </tr>


                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrpro31" Width="100%" Enabled="false" MaxLength="2" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrpro32" Width="100%" Enabled="false" MaxLength="2" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrpro33" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrpro41" Width="100%" Enabled="false" MaxLength="2" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrpro42" Width="100%" Enabled="false" MaxLength="2" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrpro43" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrpro51" Enabled="false" Width="100%" MaxLength="2" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrpro52" Width="100%" Enabled="false" MaxLength="2" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrpro53" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrpro54" Width="100%" MaxLength="2" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrpro55" Width="100%" MaxLength="2" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrpro56" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                        </tr>


                                    </table>

                                </td>

                            </tr>

                            <tr>
                                <td style="height: 10px"></td>
                            </tr>

                            <tr>
                                <td style="width: 2%;"></td>
                                <td style="width: 47%;"></td>
                                <td style="width: 7%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>Total for 'B</b></p>
                                </td>
                                <td colspan="2" style="width: 49%;">



                                    <table style="border: 1px; border-color: black; table-layout: fixed; width: 85%">
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrproT11" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrproT12" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtrproT13" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>





                                    </table>

                                </td>

                            </tr>

                            <tr>
                                <td colspan="4" style="width: 98%; height: 10px;"></td>

                            </tr>
                            <tr>

                                <td style="width: 42%;" colspan="2">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>C Institution building</b></p>
                                </td>
                                <td style="width: 7%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>(20)</b></p>
                                </td>
                                <td colspan="2" style="width: 49%;"></td>



                            </tr>



                            <tr>

                                <td style="width: 42%;" colspan="2">
                                    <table>
                                        <tr>
                                            <td style="width: 100%; height: 20px">10 points for university & college level administrative responsibility</td>
                                        </tr>

                                        <tr>
                                            <td style="width: 100%; height: 20px">6 points for departmental & programme level responsibility</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">5 points for each FDP delivered within or outside university</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">4 points for  organizing non-academic events</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">5 points for  contribution in admissions & placements</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">3 points for delivering workshop for students or member of faculty</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">8 points for publishing an institutional news letter / magazine & institutional journals (member of editorial Board)</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">3 points for submission of examination /assessment works within stipulated time & quality in terms of objectivity & fairness</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">2 points for contribution to the Non - academic committee as a member</td>
                                        </tr>





                                    </table>
                                </td>
                                <td style="width: 7%;"></td>
                                <td colspan="2" style="width: 49%;">



                                    <table style="border: 1px; border-color: black; table-layout: fixed; width: 85%">
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtinb11" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtinb12" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtinb13" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtinb21" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtinb22" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtinb23" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>


                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtinb31" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtinb32" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtinb33" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtinb41" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtinb42" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtinb43" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtinb51" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtinb52" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtinb53" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>

                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtinb61" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtinb62" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtinb63" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtinb71" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtinb72" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtinb73" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtinb74" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtinb75" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtinb76" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>


                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtinb77" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtinb78" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtinb79" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>


                                        </tr>

                                    </table>

                                </td>

                            </tr>




                            <tr>
                                <td colspan="4" style="height: 10px; width: 98%"></td>

                            </tr>



                            <tr>
                                <td style="width: 2%;"></td>
                                <td style="width: 47%;"></td>
                                <td style="width: 7%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>Total for 'C'</b></p>
                                </td>
                                <td colspan="2" style="width: 49%;">



                                    <table style="border: 1px; border-color: black; table-layout: fixed; width: 85%">
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtinbT11" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtinbT12" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtinbT13" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>





                                    </table>

                                </td>

                            </tr>

                            <tr>
                                <td colspan="4" style="height: 10px; width: 98%"></td>

                            </tr>

                            <tr>

                                <td style="width: 47%;" colspan="2">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>D Self development</b></p>
                                </td>
                                <td style="width: 7%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>(15)</b></p>
                                </td>
                                <td colspan="2" style="width: 49%;"></td>



                            </tr>

                            <tr>

                                <td style="width: 42%;" colspan="2">
                                    <table>
                                        <tr>
                                            <td style="width: 100%; height: 20px">10 points for Ph.D completed (current year)</td>
                                        </tr>

                                        <tr>
                                            <td>5 points  for Ph.D registered</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">10 points for industry immersion training (at  Least 2 weeks)</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">5 points for attending workshops for skill enhancemen</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">5 points for attending UGC / AICTE  sponsored refresh course / induction programme / FDPs conducted by any of the top University (NAAC 'A or higher graded or 'NIRF' ranking upto 150</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">5 points for each MOOC course completed</td>
                                        </tr>

                                        <tr>
                                            <td style="width: 100%; height: 20px">3 points for thesis examination</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">2 points for invitations for extension lectures / thesis / PG level  project evaluation</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">2 points for being member of professional bodies</td>
                                        </tr>



                                    </table>
                                </td>
                                <td style="width: 7%;"></td>
                                <td colspan="2" style="width: 49%;">



                                    <table style="border: 1px; border-color: black; table-layout: fixed; width: 85%">
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsd11" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsd12" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsd13" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsd21" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsd22" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsd23" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>


                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsd31" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsd32" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsd33" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsd41" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsd42" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsd43" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsd51" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsd52" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsd53" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>

                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsd61" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsd62" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsd63" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsd71" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsd72" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsd73" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsd74" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsd75" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsd76" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsd77" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsd78" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsd79" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                    </table>

                                </td>

                            </tr>




                            <tr>
                                <td style="width: 2%;"></td>
                                <td style="width: 47%;"></td>
                                <td style="width: 7%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>Total for 'D'</b></p>
                                </td>
                                <td colspan="2" style="width: 49%;">



                                    <table style="border: 1px; border-color: black; table-layout: fixed; width: 85%">
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsdT11" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsdT12" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsdT13" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>





                                    </table>

                                </td>

                            </tr>
                            <tr>
                                <td colspan="4" style="width: 98%; height: 10px;"></td>

                            </tr>
                            <tr>

                                <td style="width: 42%;" colspan="2">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>E Projects, Consultancy, MDPs/FDPs & Start-ups</b></p>
                                    (Separate points are for each category)
                                </td>
                                <td style="width: 7%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>(15)</b></p>
                                </td>
                                <td colspan="2" style="width: 49%;"></td>



                            </tr>

                            <tr>

                                <td style="width: 42%;" colspan="2">
                                    <table>
                                        <tr>
                                            <td style="width: 100%; height: 20px">4 points for revenue upto Rs. 50,000/-</td>
                                        </tr>

                                        <tr>
                                            <td style="width: 100%; height: 20px">6 points for revenue > Rs. 50,000 < Rs. 1,00,000</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">10 points for revenue > Rs. 1,00,000 < Rs. 2,00,000</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">15 points for revenue > Rs. 2,00,000 < Rs. 3,00,00</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">20 points for revenue > Rs. 3,00,000</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">20 points for incubating one start-up </td>
                                        </tr>






                                    </table>
                                </td>
                                <td style="width: 7%;"></td>
                                <td colspan="2" style="width: 49%;">



                                    <table style="border: 1px; border-color: black; table-layout: fixed; width: 85%">



                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpcm11" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpcm12" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpcm13" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpcm21" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpcm22" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpcm23" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpcm31" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpcm32" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpcm33" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>

                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpcm41" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpcm42" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpcm43" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpcm51" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpcm52" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpcm53" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>

                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpcm61" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpcm62" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpcm63" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>

                                    </table>

                                </td>

                            </tr>

                            <tr>
                                <td colspan="4" style="width: 98%; height: 10px;"></td>

                            </tr>
                            <tr>
                                <td style="width: 2%;"></td>
                                <td style="width: 47%;"></td>
                                <td style="width: 7%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>Total for 'E'</b></p>
                                </td>
                                <td colspan="2" style="width: 49%;">



                                    <table style="border: 1px; border-color: black; table-layout: fixed; width: 85%">
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpcmT11" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpcmT12" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpcmT13" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>





                                    </table>

                                </td>

                            </tr>

                            <tr>
                                <td colspan="4" style="width: 98%; height: 10px;"></td>

                            </tr>
                            <tr>

                                <td style="width: 42%;" colspan="2">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>F Peer / Institution / Industry connect</b></p>
                                </td>
                                <td style="width: 7%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>(10)</b></p>
                                </td>
                                <td colspan="2" style="width: 49%;"></td>



                            </tr>


                            <tr>

                                <td style="width: 42%;" colspan="2">
                                    <table>
                                        <tr>
                                            <td style="width: 100%; height: 20px">10 points per MOU for collaborative work & resource sharing etc at international leve</td>
                                        </tr>

                                        <tr>
                                            <td style="width: 100%; height: 20px">5 points per MOU for collaborative work & resource sharing etc at national level</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">5 points per Social / Rural / NSS / NCC / Red cross / NGO project or activity or etc.</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">5 points for award / fellowships received at national & international level</td>
                                        </tr>

                                        <tr>
                                            <td style="width: 100%; height: 20px">3 points for submission of examination /assessment works within stipulated time & quality in terms of objectivity & fairness</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">2 points for contribution to the Non - academic committee as a member</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 20px">5 points for industry MOU for placement / OJT / internship / industry immersion (company revenue > Rs. 100cr. or a technology start-up)</td>
                                        </tr>







                                    </table>
                                </td>
                                <td style="width: 7%;"></td>
                                <td colspan="2" style="width: 49%;">



                                    <table style="border: 1px; border-color: black; table-layout: fixed; width: 85%">



                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpiis11" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpiis12" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpiis13" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpiis21" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpiis22" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpiis23" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpiis31" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpiis32" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpiis33" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>

                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpiis41" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpiis42" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpiis43" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpiis51" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpiis52" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpiis53" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpiis54" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpiis55" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpiis56" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpiis57" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpiis58" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpiis59" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>


                                    </table>

                                </td>

                            </tr>


                            <tr>
                                <td colspan="4" style="width: 98%; height: 10px;"></td>

                            </tr>
                            <tr>
                                <td style="width: 2%;"></td>
                                <td style="width: 47%;"></td>
                                <td style="width: 7%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>Total for 'F'</b></p>
                                </td>
                                <td colspan="2" style="width: 49%;">



                                    <table style="border: 1px; border-color: black; table-layout: fixed; width: 85%">
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpiisT11" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpiisT12" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtpiisT13" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>





                                    </table>

                                </td>

                            </tr>

                            <tr>
                                <td colspan="4" style="width: 98%; height: 10px;"></td>

                            </tr>
                            <tr>

                                <td style="width: 42%;" colspan="2">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>G Student support & counselling</b></p>
                                </td>
                                <td style="width: 7%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>(10)</b></p>
                                </td>
                                <td colspan="2" style="width: 49%;"></td>



                            </tr>

                            <tr>

                                <td style="width: 42%;" colspan="2">
                                    <table>
                                        <tr>
                                            <td>5 points for organizing one student capability enhancement programme (Perforation for competitive exam like govt. services NET / GATE / CAT / JRF etc</td>
                                        </tr>

                                        <tr>
                                            <td>5 points for per batch mentoring</td>
                                        </tr>
                                        <tr>
                                            <td>5 points for contribution to slow learners</td>
                                        </tr>
                                        <tr>
                                            <td>5 points for organizing & accompanying  students for one industry visit / education trip / excursion </td>
                                        </tr>
                                        <tr>
                                            <td>5 points for organizing & accompanying  students on international trips</td>
                                        </tr>







                                    </table>
                                </td>
                                <td style="width: 7%;"></td>
                                <td colspan="2" style="width: 49%;">



                                    <table style="border: 1px; border-color: black; table-layout: fixed; width: 85%">



                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtssc11" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtssc12" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtssc13" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtssc21" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtssc22" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtssc23" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtssc31" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtssc32" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtssc33" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtssc34" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtssc35" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtssc36" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtssc37" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtssc38" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtssc39" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>





                                    </table>

                                </td>

                            </tr>

                            <tr>
                                <td colspan="4" style="width: 98%; height: 10px;"></td>

                            </tr>
                            <tr>
                                <td style="width: 2%;"></td>
                                <td style="width: 47%;"></td>
                                <td style="width: 7%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>Total for 'G'</b></p>
                                </td>
                                <td colspan="2" style="width: 49%;">



                                    <table style="border: 1px; border-color: black; table-layout: fixed; width: 85%">
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsscT1" onkeypress="return onlyNumbers(event)" Enabled="false" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsscT2" onkeypress="return onlyNumbers(event)" Enabled="false" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtsscT3" onkeypress="return onlyNumbers(event)" Enabled="false" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>





                                    </table>

                                </td>

                            </tr>


                            <tr>
                                <td colspan="4" style="width: 98%; height: 10px;"></td>

                            </tr>
                            <tr>

                                <td style="width: 42%;" colspan="2">
                                    <p style="font-family: Arial; font-size: 15px; color: black">
                                        <b>H Conferences / Seminars / Workshops /    Symposia organised & 
 Participation                                                                                   
                                        </b>
                                    </p>
                                </td>
                                <td style="width: 7%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>(10)</b></p>
                                </td>
                                <td colspan="2" style="width: 49%;"></td>



                            </tr>

                            <tr>

                                <td style="width: 42%;" colspan="2">
                                    <table>
                                        <tr>
                                            <td>5 points for international event organized(as a convenor, secretary & coordinator of an activity)</td>
                                        </tr>

                                        <tr>
                                            <td>2 points for national event organized(as a convenor, secretary & coordinator of an activity)</td>
                                        </tr>
                                        <tr>
                                            <td>4 points for paper presentation in international conference</td>
                                        </tr>

                                        <tr>
                                            <td>2 points for paper presentation in national conference</td>
                                        </tr>

                                        <tr>
                                            <td>5 points for organizing one workshop / seminar on IPR & industry - academia innovative practice (Convenor, Secretary & Coordinator of an activity)</td>
                                        </tr>







                                    </table>
                                </td>
                                <td style="width: 7%;"></td>
                                <td colspan="2" style="width: 49%;">



                                    <table style="border: 1px; border-color: black; table-layout: fixed; width: 85%">



                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtcsw11" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtcsw12" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtcsw13" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtcsw21" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtcsw22" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtcsw23" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtcsw31" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtcsw32" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtcsw33" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>

                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtcsw41" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtcsw42" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtcsw43" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtcsw51" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtcsw52" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtcsw53" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>





                                    </table>

                                </td>

                            </tr>


                            <tr>
                                <td colspan="4" style="width: 98%; height: 10px;"></td>

                            </tr>
                            <tr>
                                <td style="width: 2%;"></td>
                                <td style="width: 47%;"></td>
                                <td style="width: 7%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>Total for 'H'</b></p>
                                </td>
                                <td colspan="2" style="width: 49%;">



                                    <table style="border: 1px; border-color: black; table-layout: fixed; width: 85%">
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtcswT11" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtcswT12" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtcswT13" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>





                                    </table>

                                </td>

                            </tr>


                            <tr>
                                <td colspan="4" style="width: 98%; height: 10px;"></td>

                            </tr>
                            <tr>
                                <td style="width: 2%;"></td>
                                <td style="width: 47%;"></td>
                                <td style="width: 7%;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>Grand Total</b></p>
                                </td>
                                <td colspan="2" style="width: 49%;">



                                    <table style="border: 1px; border-color: black; table-layout: fixed; width: 85%">
                                        <tr>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtGGT11" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtGGT12" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                            <td style="border: 1px solid; height: 15px;">
                                                <asp:TextBox ID="txtGGT13" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <%--  --%>
                                    </table>

                                </td>

                            </tr>

                            <tr>
                                <td colspan="4" style="width: 98%; height: 10px;"></td>

                            </tr>

                            <tr>
                                <td colspan="4" style="width: 98%; height: 10px;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>Part II</b></p>
                                </td>

                            </tr>

                            <tr>
                                <td colspan="4" style="width: 98%; height: 10px;">
                                    <p style="font-family: Arial; font-size: 14px; color: black"><b><u>Comments & suggestions after mid-term review of individual progress</u></b></p>
                                </td>

                            </tr>
                            <tr>
                                <td colspan="4" style="height: 5px;"></td>

                            </tr>
                            <tr>
                                <td style="width: 2%; height: 10px; text-align: right">a)&nbsp;</td>
                                <td colspan="3" style="width: 98%; height: 10px;">
                                    <asp:TextBox ID="txtp2a" Width="95%" Enabled="false" MaxLength="150" Style="resize: none;" TextMode="MultiLine" runat="server"></asp:TextBox></td>

                            </tr>
                            <tr>
                                <td colspan="4" style="height: 5px;"></td>

                            </tr>
                            <tr>
                                <td style="width: 2%; height: 10px; text-align: right">b)&nbsp;</td>
                                <td colspan="3" style="width: 98%; height: 10px;">
                                    <asp:TextBox ID="txtp2b" Width="95%" Enabled="false" MaxLength="150" Style="resize: none;" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </td>

                            </tr>
                            <tr>
                                <td colspan="4" style="height: 5px;"></td>

                            </tr>
                            <tr>
                                <td style="width: 2%; height: 10px; text-align: right">c)&nbsp;</td>
                                <td colspan="3" style="width: 98%; height: 10px;">
                                    <asp:TextBox ID="txtp2c" Width="95%" Enabled="false" MaxLength="150" Style="resize: none;" TextMode="MultiLine" runat="server"></asp:TextBox></td>

                            </tr>
                            <tr>
                                <td colspan="4" style="height: 5px;"></td>

                            </tr>
                            <tr>
                                <td style="width: 2%; height: 10px; text-align: right">d)&nbsp;</td>
                                <td colspan="3" style="width: 98%; height: 10px;">
                                    <asp:TextBox ID="txtp2d" Width="95%" Enabled="false" MaxLength="150" Style="resize: none;" TextMode="MultiLine" runat="server"></asp:TextBox></td>

                            </tr>
                            <tr>
                                <td colspan="4" style="height: 5px;"></td>

                            </tr>
                            <tr>
                                <td style="width: 2%; height: 10px; text-align: right">e)&nbsp;</td>
                                <td colspan="3" style="width: 98%; height: 10px;">
                                    <asp:TextBox ID="txtp2e" Width="95%" Enabled="false" MaxLength="150" Style="resize: none;" TextMode="MultiLine" runat="server"></asp:TextBox></td>

                            </tr>

                            <tr>
                                <td colspan="4" style="width: 98%; height: 10px;"></td>

                            </tr>


                            <tr>
                                <td colspan="4" style="width: 98%">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 5%; text-align: right;">Date&nbsp;</td>
                                            <td style="width: 15%;">
                                                <asp:TextBox ID="datep2" Width="80%" runat="server" CssClass="form-control input-sm" Enabled="false" MaxLength="50" autocomplete="off" ToolTip="Date" onkeypress="return false"
                                                    onKeyDown="preventBackspace();"></asp:TextBox>
                                                <asp:CalendarExtender ID="cleFromDate" Format="dd MMM yyyy" runat="server" CssClass="cal_Theme1"
                                                    Enabled="true" TargetControlID="datep2" />

                                            </td>
                                            <td style="width: 20%; text-align: right;">Signature of assessee&nbsp;</td>
                                            <td style="width: 20%;">
                                                <asp:TextBox ID="SignAssp2" Width="50%" Enabled="false" MaxLength="75" runat="server"></asp:TextBox></td>
                                            <td style="width: 15%; text-align: right;">Controlling officer’s signatur&nbsp;</td>
                                            <td style="width: 20%;">
                                                <asp:TextBox ID="SignCos" Width="75%" Enabled="false" MaxLength="50" runat="server"></asp:TextBox></td>

                                        </tr>

                                    </table>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="4" style="width: 98%; height: 10px;"></td>

                            </tr>

                            <tr>
                                <td colspan="4" style="width: 98%; height: 10px;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>Part III</b></p>
                                </td>

                            </tr>

                            <tr>
                                <td colspan="4" style="width: 98%; height: 10px;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b><u>Qualitative assessment of Individual</u> (total 100 points & 10 points for each criteria)</b></p>
                                </td>

                            </tr>
                            <tr>

                                <td colspan="4" style="width: 98%; height: 10px;">

                                    <table>
                                        <tr>
                                            <td>a) Commitment to work / department / university </td>
                                            <td>
                                                <asp:TextBox ID="txtp3a" Width="100%" onkeypress="return onlyNumbers(event)" MaxLength="2" Enabled="false" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="height: 5px;"></td>

                                        </tr>
                                        <tr>
                                            <td>b) Relationship with colleagues & superiors </td>
                                            <td>
                                                <asp:TextBox ID="txtp3b" Width="100%" onkeypress="return onlyNumbers(event)" MaxLength="2" Enabled="false" runat="server"></asp:TextBox></td>

                                        </tr>
                                        <tr>
                                            <td colspan="2" style="height: 5px;"></td>

                                        </tr>
                                        <tr>
                                            <td>c) Punctuality & regularity (class room delivery)</td>
                                            <td>
                                                <asp:TextBox ID="txtp3c" Width="100%" onkeypress="return onlyNumbers(event)" MaxLength="2" Enabled="false" runat="server"></asp:TextBox></td>

                                        </tr>
                                        <tr>
                                            <td colspan="2" style="height: 5px;"></td>

                                        </tr>
                                        <tr>
                                            <td>d) Maturity & temperament</td>
                                            <td>
                                                <asp:TextBox ID="txtp3d" Width="100%" onkeypress="return onlyNumbers(event)" MaxLength="2" Enabled="false" runat="server"></asp:TextBox></td>

                                        </tr>
                                        <tr>
                                            <td colspan="2" style="height: 5px;"></td>

                                        </tr>
                                        <tr>
                                            <td>e) Work knowledge</td>
                                            <td>
                                                <asp:TextBox ID="txtp3e" Width="100%" onkeypress="return onlyNumbers(event)" MaxLength="2" Enabled="false" runat="server"></asp:TextBox></td>

                                        </tr>
                                        <tr>
                                            <td colspan="2" style="height: 5px;"></td>

                                        </tr>
                                        <tr>
                                            <td>f) Regularity & accessibility</td>
                                            <td>
                                                <asp:TextBox ID="txtp3f" Width="100%" onkeypress="return onlyNumbers(event)" MaxLength="2" Enabled="false" runat="server"></asp:TextBox></td>

                                        </tr>

                                        <tr>
                                            <td colspan="2" style="height: 5px;"></td>

                                        </tr>

                                        <tr>
                                            <td>g) Self improvement</td>
                                            <td>
                                                <asp:TextBox ID="txtp3g" Width="100%" onkeypress="return onlyNumbers(event)" MaxLength="2" Enabled="false" runat="server"></asp:TextBox></td>

                                        </tr>
                                        <tr>
                                            <td colspan="2" style="height: 5px;"></td>

                                        </tr>
                                        <tr>
                                            <td>h) Ability to deal with difficult situation</td>
                                            <td>
                                                <asp:TextBox ID="txtp3h" Width="100%" onkeypress="return onlyNumbers(event)" MaxLength="2" Enabled="false" runat="server"></asp:TextBox></td>

                                        </tr>
                                        <tr>
                                            <td colspan="2" style="height: 5px;"></td>

                                        </tr>
                                        <tr>
                                            <td>i) Communication skills</td>
                                            <td>
                                                <asp:TextBox ID="txtp3i" Width="100%" onkeypress="return onlyNumbers(event)" MaxLength="2" Enabled="false" runat="server"></asp:TextBox></td>

                                        </tr>
                                        <tr>
                                            <td colspan="2" style="height: 5px;"></td>

                                        </tr>
                                        <tr>
                                            <td>j) Leadership</td>
                                            <td>
                                                <asp:TextBox ID="txtp3j" Width="100%" onkeypress="return onlyNumbers(event)" MaxLength="2" Enabled="false" runat="server"></asp:TextBox></td>

                                        </tr>





                                    </table>


                                </td>


                            </tr>

                            <tr>
                                <td colspan="4" style="width: 98%; height: 10px;">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>Part IV</b></p>
                                </td>

                            </tr>

                            <tr>
                                <td colspan="4" style="width: 98%; height: 10px;"><u>Self assessment:</u></td>

                            </tr>
                            <tr>
                                <td colspan="2" style="width: 49%; font: bold; color: black">Part I </td>
                                <td colspan="2" style="width: 49%; font: bold; color: black">Part III</td>

                            </tr>

                            <tr>
                                <td style="width: 2%; text-align: right; font: bold; color: black">Points&nbsp;</td>
                                <td style="width: 47%; text-align: left">
                                    <asp:TextBox ID="txtp41point" Width="25%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                <td style="width: 2%; text-align: right; font: bold; color: black">Points&nbsp;</td>
                                <td style="width: 47%; text-align: left">
                                    <asp:TextBox ID="txtp43point" Width="25%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>

                            </tr>
                            <tr>
                                <td style="width: 49%" colspan="2"></td>
                                <td colspan="2" style="width: 49%">Signature of the assessee(with date)</td>

                            </tr>


                            <tr>
                                <td>
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>Part V</b></p>
                                </td>

                            </tr>
                            <tr>
                                <td colspan="4" style="font: bold; color: black"><b><u>Controlling/reporting officer's assessment</u></b> (based on evidence presented)</td>

                            </tr>
                            <tr>
                                <td colspan="2" style="width: 49%; font: bold; color: black">Part I </td>
                                <td colspan="2" style="width: 49%; font: bold; color: black">Part III</td>

                            </tr>

                            <tr>
                                <td style="width: 4%; text-align: right; font: bold; color: black">Points&nbsp;</td>
                                <td style="width: 40%; text-align: left">
                                    <asp:TextBox ID="txtp51point" Width="25%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                <td style="width: 4%; text-align: right; font: bold; color: black">Points&nbsp;</td>
                                <td style="width: 40%; text-align: left">
                                    <asp:TextBox ID="txtp53point" Width="25%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>

                            </tr>
                            <tr>
                                <td colspan="2" style="width: 49%">(Assessee signature with date)</td>
                                <td colspan="2" style="width: 49%">(Controlling/reporting officer signature with date)</td>

                            </tr>

                            <tr>
                                <td colspan="4" style="width: 98%">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>Part VI</b></p>
                                </td>


                            </tr>

                            <tr>
                                <td colspan="4" style="width: 98%">Reviewing officer’s approval</td>


                            </tr>

                            <tr>
                                <td colspan="1" style="width: 15%;">Comments, if any</td>
                                <td colspan="3" style="width: 80%">
                                    <asp:TextBox ID="txt6coments" Width="90%" Enabled="false" TextMode="MultiLine" Style="resize: none;" MaxLength="150" runat="server"></asp:TextBox></td>


                            </tr>

                            <tr>

                                <td colspan="2" style="width: 49%"></td>
                                <td colspan="2" style="width: 49%">Signature & seal</td>




                            </tr>

                            <tr>
                                <td colspan="4" style="width: 98%">
                                    <p style="font-family: Arial; font-size: 15px; color: black"><b>Part VII</b></p>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="width: 49%">HR department</td>
                            </tr>

                            <tr>
                                <td style="width: 9%">Total point</td>
                                <td style="width: 40%">
                                    <asp:TextBox ID="txt7tpoint" Width="50%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server"></asp:TextBox></td>
                                <td style="width: 14%">Final letter grade</td>
                                <td style="width: 36%">
                                    <asp:TextBox ID="txt7flg" Width="50%" MaxLength="10" Enabled="false" runat="server"></asp:TextBox></td>

                            </tr>
                            <tr>
                                <td colspan="4" style="width: 98%">Recommendations</td>
                            </tr>
                            <tr>
                                <td style="width: 1%; text-align: right">1&nbsp;</td>
                                <td colspan="3" style="text-align: left; width: 97%">
                                    <asp:TextBox ID="txtp7r1" Width="96%" MaxLength="50" Style="resize: none;" TextMode="MultiLine" Enabled="false" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="4" style="height: 5px;"></td>
                            </tr>
                            <tr>
                                <td style="width: 1%; text-align: right">2&nbsp;</td>
                                <td colspan="3" style="text-align: left; width: 97%">
                                    <asp:TextBox ID="txtp7r2" Width="96%" MaxLength="50" Style="resize: none;" Enabled="false" TextMode="MultiLine" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="4" style="height: 5px;"></td>
                            </tr>
                            <tr>
                                <td style="width: 1%; text-align: right">3&nbsp;</td>
                                <td colspan="3" style="text-align: left; width: 97%">
                                    <asp:TextBox ID="txtp7r3" Width="96%" MaxLength="50" Enabled="false" Style="resize: none;" TextMode="MultiLine" runat="server"></asp:TextBox></td>
                            </tr>

                            </tr>

       <tr>
           <td style="height: 5px;" colspan="4"></td>
           <tr>
               <td style="width: 1%; text-align: right"></td>
               <td colspan="3" style="text-align: left; width: 97%">
                   <asp:TextBox ID="txtVC" runat="server" Enabled="false" MaxLength="250" Style="resize: none;" TextMode="MultiLine" Width="96%"></asp:TextBox>
               </td>
           </tr>
           <tr>
               <td colspan="4" style="text-align: center; width: 98%">Approval &amp; comments</td>
           </tr>
           <tr>
               <td colspan="4" style="text-align: center; width: 98%">(Vice Chancellor)</td>
           </tr>
           <tr>
               <td colspan="4" style="width: 98%; height: 20px;"></td>
           </tr>
       </tr>



                        </table>



                    </div>




                </div>



                <div style="text-align: right; width: 98%; margin-bottom: 40px;">


                    <asp:Button ID="btnsave" runat="server" Text="Save" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" OnClick="btnsave_Click" />
                    <asp:Button ID="btncheck" runat="server" Text="Submit" OnClientClick="return CheckAttachment();"  OnClick="btncheck_Click" />
                    <asp:Button ID="btntest" OnClientClick="PrintDiv();" runat="server" Visible="true" Text="Print" />

                </div>


                <script type="text/javascript">

                    function CheckAttachment() {
                        var obj = document.getElementById('<%= grdDocument.ClientID%>');

                        if (parseInt(obj.rows.length) - 1 == 0) {
                            if (confirm("Are you sure you want to Submit Form without any Attachment..?")) {
                                return true;
                            }
                            else {
                                return false;
                            }
                        }


                    }
                    function fireMouseEvent(obj, evtName) {
                        if (obj.dispatchEvent) {
                            //var event = new Event(evtName);
                            var event = document.createEvent("MouseEvents");
                            event.initMouseEvent(evtName, true, true, window,
                                    0, 0, 0, 0, 0, false, false, false, false, 0, null);
                            obj.dispatchEvent(event);
                        } else if (obj.fireEvent) {
                            event = document.createEventObject();
                            event.button = 1;
                            obj.fireEvent("on" + evtName, event);
                            obj.fireEvent(evtName);
                        } else {
                            obj[evtName]();
                        }
                    }
                   
                    function funChkEnterAndMaxMarksNested(thisId, thisvalue) {



                        var vtxtmark = thisId.id;

                        if (thisvalue !== "") {
                            var txtct1 = document.getElementById("ContentPlaceHolder1_txtct1").value;

                            if (txtct1 > 120) {
                                alert("Can not enter more than 120");
                                document.getElementById("ContentPlaceHolder1_txtct1").value = '';
                                document.getElementById("ContentPlaceHolder1_txtct1").focus();
                                document.getElementById("ContentPlaceHolder1_txtct1").style.borderColor = 'red';
                                return false;
                            }
                            var txtct2 = document.getElementById("ContentPlaceHolder1_txtct2").value;
                            //if (txtct2 > 120) {
                            //    alert("Can not enter more than 120");
                            //    document.getElementById("ContentPlaceHolder1_txtct2").value = 120;
                            //    document.getElementById("ContentPlaceHolder1_txtct2").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtct2").style.borderColor = 'red';
                            //   return false;
                            //}

                            if (txtct1 != "") {
                                var SF11 = parseFloat(txtct1);
                                if (SF11 > 120) {
                                    alert("Can not enter more than 120");
                                    document.getElementById("ContentPlaceHolder1_txtct1").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtct1").focus();
                                    document.getElementById("ContentPlaceHolder1_txtct1").style.borderColor = 'red';

                                }
                            }


                            if (txtct2 != "") {
                                var SF12 = parseFloat(txtct2);

                                //if (SF12 > 120) {
                                // alert("Can not enter more than 120");
                                //document.getElementById("ContentPlaceHolder1_txtct2").value = 120;
                                //document.getElementById("ContentPlaceHolder1_txtct2").focus();
                                //document.getElementById("ContentPlaceHolder1_txtct2").style.borderColor = 'red';
                                //document.getElementById("ContentPlaceHolder1_txtsf12").value = '';
                                //document.getElementById("ContentPlaceHolder1_txtsf12").focus();
                                //document.getElementById("ContentPlaceHolder1_txtsf12").style.borderColor = 'red';
                                //document.getElementById("ContentPlaceHolder1_txtsf22").value = '';
                                //document.getElementById("ContentPlaceHolder1_txtsf22").focus();
                                //document.getElementById("ContentPlaceHolder1_txtsf22").style.borderColor = 'red';
                                //document.getElementById("ContentPlaceHolder1_txtsf32").value = '';
                                //document.getElementById("ContentPlaceHolder1_txtsf32").focus();
                                //document.getElementById("ContentPlaceHolder1_txtsf32").style.borderColor = 'red';
                                //document.getElementById("ContentPlaceHolder1_txtsf42").value = '';
                                //document.getElementById("ContentPlaceHolder1_txtsf42").focus();
                                //document.getElementById("ContentPlaceHolder1_txtsf42").style.borderColor = 'red';
                                //document.getElementById("ContentPlaceHolder1_txtsf52").value = '';
                                //document.getElementById("ContentPlaceHolder1_txtsf52").focus();
                                //document.getElementById("ContentPlaceHolder1_txtsf52").style.borderColor = 'red';
                                //document.getElementById("ContentPlaceHolder1_txtsf62").value = '';
                                //document.getElementById("ContentPlaceHolder1_txtsf62").focus();
                                //document.getElementById("ContentPlaceHolder1_txtsf62").style.borderColor = 'red';


                                //document.getElementById("ContentPlaceHolder1_txtsf65").value = '';
                                //document.getElementById("ContentPlaceHolder1_txtsf65").focus();
                                //document.getElementById("ContentPlaceHolder1_txtsf65").style.borderColor = 'red';
                                //document.getElementById("ContentPlaceHolder1_txtsf68").value = '';
                                //document.getElementById("ContentPlaceHolder1_txtsf68").focus();
                                //document.getElementById("ContentPlaceHolder1_txtsf68").style.borderColor = 'red';
                                //document.getElementById("ContentPlaceHolder1_txtsf71").value = '';
                                //document.getElementById("ContentPlaceHolder1_txtsf71").focus();
                                //document.getElementById("ContentPlaceHolder1_txtsf71").style.borderColor = 'red';
                                //document.getElementById("ContentPlaceHolder1_txtsf74").value = '';
                                //document.getElementById("ContentPlaceHolder1_txtsf74").focus();
                                //document.getElementById("ContentPlaceHolder1_txtsf74").style.borderColor = 'red';
                                //document.getElementById("ContentPlaceHolder1_txtsf77").value = '';
                                //document.getElementById("ContentPlaceHolder1_txtsf77").focus();
                                //document.getElementById("ContentPlaceHolder1_txtsf77").style.borderColor = 'red';
                                //document.getElementById("ContentPlaceHolder1_txtsf80").value = '';
                                //document.getElementById("ContentPlaceHolder1_txtsf80").focus();
                                //document.getElementById("ContentPlaceHolder1_txtsf80").style.borderColor = 'red';

                                //}
                            }



                            //ip
                            var txtip11 = document.getElementById("ContentPlaceHolder1_txtip11").value;
                            if (txtip11 > 16) {
                                alert("Can not enter more than 16");
                                document.getElementById("ContentPlaceHolder1_txtip11").value = '';
                                document.getElementById("ContentPlaceHolder1_txtip11").focus();
                                document.getElementById("ContentPlaceHolder1_txtip11").style.borderColor = 'red';
                                return false;
                            }

                            var txtip12 = document.getElementById("ContentPlaceHolder1_txtip12").value;
                            //if (txtip12 > 16) {
                            //    alert("Can not enter more than 16");
                            //    document.getElementById("ContentPlaceHolder1_txtip12").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtip12").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtip12").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtip21 = document.getElementById("ContentPlaceHolder1_txtip21").value;
                            if (txtip21 > 16) {
                                alert("Can not enter more than 16");
                                document.getElementById("ContentPlaceHolder1_txtip21").value = '';
                                document.getElementById("ContentPlaceHolder1_txtip21").focus();
                                document.getElementById("ContentPlaceHolder1_txtip21").style.borderColor = 'red';
                                return false;
                            }

                            var txtip22 = document.getElementById("ContentPlaceHolder1_txtip22").value;
                            //if (txtip22 > 16) {
                            //    alert("Can not enter more than 16");
                            //    document.getElementById("ContentPlaceHolder1_txtip22").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtip22").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtip22").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtip31 = document.getElementById("ContentPlaceHolder1_txtip31").value;
                            if (txtip31 > 16) {
                                alert("Can not enter more than 16");
                                document.getElementById("ContentPlaceHolder1_txtip31").value = '';
                                document.getElementById("ContentPlaceHolder1_txtip31").focus();
                                document.getElementById("ContentPlaceHolder1_txtip31").style.borderColor = 'red';
                                return false;
                            }

                            var txtip32 = document.getElementById("ContentPlaceHolder1_txtip32").value;
                            //if (txtip32 > 16) {
                            //    alert("Can not enter more than 16");
                            //    document.getElementById("ContentPlaceHolder1_txtip32").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtip32").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtip32").style.borderColor = 'red';
                            //    return false;
                            //}

                            if (txtip11 != "" && txtip21 != "" && txtip31 != "") {
                                var IP11 = parseFloat(txtip11) + parseFloat(txtip21) + parseFloat(txtip31);

                                if (IP11 > 16) {
                                    alert("Can not enter more than 16");
                                    document.getElementById("ContentPlaceHolder1_txtip11").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtip11").focus();
                                    document.getElementById("ContentPlaceHolder1_txtip11").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtip21").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtip21").focus();
                                    document.getElementById("ContentPlaceHolder1_txtip21").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtip31").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtip31").focus();
                                    document.getElementById("ContentPlaceHolder1_txtip31").style.borderColor = 'red';

                                }
                            }

                            if (txtip12 != "" && txtip22 != "" && txtip32 != "") {

                                var IP12 = parseFloat(txtip12) + parseFloat(txtip22) + parseFloat(txtip32);

                                //if (IP12 > 16) {
                                //alert("Can not enter more than 16");
                                //document.getElementById("ContentPlaceHolder1_txtip12").value = 16;
                                //document.getElementById("ContentPlaceHolder1_txtip12").focus();
                                //document.getElementById("ContentPlaceHolder1_txtip12").style.borderColor = 'red';
                                //document.getElementById("ContentPlaceHolder1_txtip22").value = '';
                                //document.getElementById("ContentPlaceHolder1_txtip22").focus();
                                //document.getElementById("ContentPlaceHolder1_txtip22").style.borderColor = 'red';
                                //document.getElementById("ContentPlaceHolder1_txtip32").value = '';
                                //document.getElementById("ContentPlaceHolder1_txtip32").focus();
                                //document.getElementById("ContentPlaceHolder1_txtip32").style.borderColor = 'red';


                                //}
                            }


                            //NC
                            var txtNc11 = document.getElementById("ContentPlaceHolder1_txtNc11").value;
                            if (txtNc11 > 5) {
                                alert("Can not enter more than 5");
                                document.getElementById("ContentPlaceHolder1_txtNc11").value = '';
                                document.getElementById("ContentPlaceHolder1_txtNc11").focus();
                                document.getElementById("ContentPlaceHolder1_txtNc11").style.borderColor = 'red';
                                return false;
                            }

                            var txtNc12 = document.getElementById("ContentPlaceHolder1_txtNc12").value;
                            //if (txtNc12 > 5) {
                            //    alert("Can not enter more than 5");
                            //    document.getElementById("ContentPlaceHolder1_txtNc12").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtNc12").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtNc12").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtNc21 = document.getElementById("ContentPlaceHolder1_txtNc21").value;
                            if (txtNc21 > 5) {
                                alert("Can not enter more than 5");
                                document.getElementById("ContentPlaceHolder1_txtNc21").value = '';
                                document.getElementById("ContentPlaceHolder1_txtNc21").focus();
                                document.getElementById("ContentPlaceHolder1_txtNc21").style.borderColor = 'red';
                                return false;
                            }

                            var txtNc22 = document.getElementById("ContentPlaceHolder1_txtNc22").value;
                            //if (txtNc22 > 5) {
                            //    alert("Can not enter more than 5");
                            //    document.getElementById("ContentPlaceHolder1_txtNc22").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtNc22").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtNc22").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtNc31 = document.getElementById("ContentPlaceHolder1_txtNc31").value;
                            if (txtNc31 > 5) {
                                alert("Can not enter more than 5");
                                document.getElementById("ContentPlaceHolder1_txtNc31").value = '';
                                document.getElementById("ContentPlaceHolder1_txtNc31").focus();
                                document.getElementById("ContentPlaceHolder1_txtNc31").style.borderColor = 'red';
                                return false;
                            }

                            var txtNc32 = document.getElementById("ContentPlaceHolder1_txtNc32").value;
                            //if (txtNc32 > 5) {
                            //    alert("Can not enter more than 5");
                            //    document.getElementById("ContentPlaceHolder1_txtNc32").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtNc32").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtNc32").style.borderColor = 'red';
                            //    return false;
                            //}
                            var txtNc34 = document.getElementById("ContentPlaceHolder1_txtNc34").value;
                            if (txtNc34 > 5) {
                                alert("Can not enter more than 5");
                                document.getElementById("ContentPlaceHolder1_txtNc34").value = '';
                                document.getElementById("ContentPlaceHolder1_txtNc34").focus();
                                document.getElementById("ContentPlaceHolder1_txtNc34").style.borderColor = 'red';
                                return false;
                            }
                            var txtNc35 = document.getElementById("ContentPlaceHolder1_txtNc35").value;
                            //if (txtNc35 > 5) {
                            //    alert("Can not enter more than 5");
                            //    document.getElementById("ContentPlaceHolder1_txtNc35").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtNc35").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtNc35").style.borderColor = 'red';
                            //    return false;
                            //}
                            var txtNc37 = document.getElementById("ContentPlaceHolder1_txtNc37").value;
                            if (txtNc37 > 5) {
                                alert("Can not enter more than 5");
                                document.getElementById("ContentPlaceHolder1_txtNc37").value = '';
                                document.getElementById("ContentPlaceHolder1_txtNc37").focus();
                                document.getElementById("ContentPlaceHolder1_txtNc37").style.borderColor = 'red';
                                return false;
                            }
                            var txtNc38 = document.getElementById("ContentPlaceHolder1_txtNc38").value;
                            //if (txtNc38 > 5) {
                            //    alert("Can not enter more than 5");
                            //    document.getElementById("ContentPlaceHolder1_txtNc38").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtNc38").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtNc38").style.borderColor = 'red';
                            //    return false;
                            //}
                            var txtNc40 = document.getElementById("ContentPlaceHolder1_txtNc40").value;
                            if (txtNc40 > 5) {
                                alert("Can not enter more than 5");
                                document.getElementById("ContentPlaceHolder1_txtNc40").value = '';
                                document.getElementById("ContentPlaceHolder1_txtNc40").focus();
                                document.getElementById("ContentPlaceHolder1_txtNc40").style.borderColor = 'red';
                                return false;
                            }
                            var txtNc41 = document.getElementById("ContentPlaceHolder1_txtNc41").value;
                            //if (txtNc41 > 5) {
                            //    alert("Can not enter more than 5");
                            //    document.getElementById("ContentPlaceHolder1_txtNc41").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtNc41").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtNc41").style.borderColor = 'red';
                            //    return false;
                            //}

                            if (txtNc11 != "" && txtNc21 != "" && txtNc31 != "" && txtNc34 != "" && txtNc37 != "" && txtNc40 != "") {

                                var NC11 = parseFloat(txtNc11) + parseFloat(txtNc21) + parseFloat(txtNc31) + parseFloat(txtNc34) + parseFloat(txtNc37) + parseFloat(txtNc40);

                                if (NC11 > 5) {
                                    alert("Can not enter more than 5");
                                    document.getElementById("ContentPlaceHolder1_txtNc11").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtNc11").focus();
                                    document.getElementById("ContentPlaceHolder1_txtNc11").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtNc21").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtNc21").focus();
                                    document.getElementById("ContentPlaceHolder1_txtNc21").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtNc31").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtNc31").focus();
                                    document.getElementById("ContentPlaceHolder1_txtNc31").style.borderColor = 'red';

                                    document.getElementById("ContentPlaceHolder1_txtNc34").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtNc34").focus();
                                    document.getElementById("ContentPlaceHolder1_txtNc34").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtNc37").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtNc37").focus();
                                    document.getElementById("ContentPlaceHolder1_txtNc37").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtNc40").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtNc40").focus();
                                    document.getElementById("ContentPlaceHolder1_txtNc40").style.borderColor = 'red';


                                }
                            }

                            if (txtNc12 != "" && txtNc22 != "" && txtNc32 != "" && txtNc35 != '' && txtNc38 != '' && txtNc41 != '') {

                                var NC12 = parseFloat(txtNc12) + parseFloat(txtNc22) + parseFloat(txtNc32) + parseFloat(txtNc35) + parseFloat(txtNc38) + parseFloat(txtNc41);

                                //if (NC12 > 5) {
                                //    alert("Can not enter more than 5");
                                //    document.getElementById("ContentPlaceHolder1_txtNc12").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtNc12").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtNc12").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtNc22").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtNc22").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtNc22").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtNc32").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtNc32").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtNc32").style.borderColor = 'red';

                                //    document.getElementById("ContentPlaceHolder1_txtNc35").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtNc35").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtNc35").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtNc38").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtNc38").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtNc38").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtNc41").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtNc41").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtNc41").style.borderColor = 'red';


                                //}
                            }

                            var txtcsm11 = document.getElementById("ContentPlaceHolder1_txtcsm11").value;
                            if (txtcsm11 > 3) {
                                alert("Can not enter more than 3");
                                document.getElementById("ContentPlaceHolder1_txtcsm11").value = '';
                                document.getElementById("ContentPlaceHolder1_txtcsm11").focus();
                                document.getElementById("ContentPlaceHolder1_txtcsm11").style.borderColor = 'red';
                                return false;
                            }

                            var txtcsm12 = document.getElementById("ContentPlaceHolder1_txtcsm12").value;
                            //if (txtcsm12 > 3) {
                            //    alert("Can not enter more than 3");
                            //    document.getElementById("ContentPlaceHolder1_txtcsm12").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtcsm12").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtcsm12").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtcsm21 = document.getElementById("ContentPlaceHolder1_txtcsm21").value;
                            if (txtcsm21 > 3) {
                                alert("Can not enter more than 3");
                                document.getElementById("ContentPlaceHolder1_txtcsm21").value = '';
                                document.getElementById("ContentPlaceHolder1_txtcsm21").focus();
                                document.getElementById("ContentPlaceHolder1_txtcsm21").style.borderColor = 'red';
                                return false;
                            }

                            var txtcsm22 = document.getElementById("ContentPlaceHolder1_txtcsm22").value;
                            //if (txtcsm22 > 3) {
                            //    alert("Can not enter more than 3");
                            //    document.getElementById("ContentPlaceHolder1_txtcsm22").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtcsm22").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtcsm22").style.borderColor = 'red';
                            //    return false;
                            //}


                            if (txtcsm11 != "" && txtcsm21 != "") {

                                var CSM11 = parseFloat(txtcsm11) + parseFloat(txtcsm21);

                                if (CSM11 > 3) {
                                    alert("Can not enter more than 3");
                                    document.getElementById("ContentPlaceHolder1_txtcsm11").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtcsm11").focus();
                                    document.getElementById("ContentPlaceHolder1_txtcsm11").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtcsm21").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtcsm21").focus();
                                    document.getElementById("ContentPlaceHolder1_txtcsm21").style.borderColor = 'red';

                                }
                            }

                            if (txtcsm12 != "" && txtcsm22 != "") {

                                var CSM12 = parseFloat(txtcsm12) + parseFloat(txtcsm22);

                                //if (CSM12 > 3) {
                                //    alert("Can not enter more than 3");
                                //    document.getElementById("ContentPlaceHolder1_txtcsm12").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtcsm12").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtcsm12").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtcsm22").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtcsm22").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtcsm22").style.borderColor = 'red';

                                //}
                            }

                            //pws

                            var txtpws11 = document.getElementById("ContentPlaceHolder1_txtpws11").value;
                            if (txtpws11 > 16) {
                                alert("Can not enter more than 16");
                                document.getElementById("ContentPlaceHolder1_txtpws11").value = '';
                                document.getElementById("ContentPlaceHolder1_txtpws11").focus();
                                document.getElementById("ContentPlaceHolder1_txtpws11").style.borderColor = 'red';
                                return false;
                            }

                            var txtpws12 = document.getElementById("ContentPlaceHolder1_txtpws12").value;
                            //if (txtpws12 > 16) {
                            //    alert("Can not enter more than 16");
                            //    document.getElementById("ContentPlaceHolder1_txtpws12").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpws12").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpws12").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpws21 = document.getElementById("ContentPlaceHolder1_txtpws21").value;
                            if (txtpws21 > 16) {
                                alert("Can not enter more than 16");
                                document.getElementById("ContentPlaceHolder1_txtpws21").value = '';
                                document.getElementById("ContentPlaceHolder1_txtpws21").focus();
                                document.getElementById("ContentPlaceHolder1_txtpws21").style.borderColor = 'red';
                                return false;
                            }
                            var txtpws22 = document.getElementById("ContentPlaceHolder1_txtpws22").value;
                            //if (txtpws22 > 16) {
                            //    alert("Can not enter more than 16");
                            //    document.getElementById("ContentPlaceHolder1_txtpws22").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpws22").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpws22").style.borderColor = 'red';
                            //    return false;
                            //}
                            var txtpws31 = document.getElementById("ContentPlaceHolder1_txtpws31").value;
                            if (txtpws31 > 16) {
                                alert("Can not enter more than 16");
                                document.getElementById("ContentPlaceHolder1_txtpws31").value = '';
                                document.getElementById("ContentPlaceHolder1_txtpws31").focus();
                                document.getElementById("ContentPlaceHolder1_txtpws31").style.borderColor = 'red';
                                return false;
                            }
                            var txtpws32 = document.getElementById("ContentPlaceHolder1_txtpws32").value;
                            //if (txtpws32 > 16) {
                            //    alert("Can not enter more than 16");
                            //    document.getElementById("ContentPlaceHolder1_txtpws32").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpws32").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpws32").style.borderColor = 'red';
                            //    return false;
                            //}
                            var txtpws34 = document.getElementById("ContentPlaceHolder1_txtpws34").value;
                            if (txtpws34 > 16) {
                                alert("Can not enter more than 16");
                                document.getElementById("ContentPlaceHolder1_txtpws34").value = '';
                                document.getElementById("ContentPlaceHolder1_txtpws34").focus();
                                document.getElementById("ContentPlaceHolder1_txtpws34").style.borderColor = 'red';
                                return false;
                            }

                            var txtpws35 = document.getElementById("ContentPlaceHolder1_txtpws35").value;
                            //if (txtpws35 > 16) {
                            //    alert("Can not enter more than 16");
                            //    document.getElementById("ContentPlaceHolder1_txtpws35").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpws35").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpws35").style.borderColor = 'red';
                            //    return false;
                            //}


                            if (txtpws11 != "" && txtpws21 != "" && txtpws31 != "" && txtpws34 != "") {

                                var PWS11 = parseFloat(txtpws11) + parseFloat(txtpws21) + parseFloat(txtpws31) + parseFloat(txtpws34);

                                if (PWS11 > 16) {
                                    alert("Can not enter more than 16");
                                    document.getElementById("ContentPlaceHolder1_txtpws11").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtpws11").focus();
                                    document.getElementById("ContentPlaceHolder1_txtpws11").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtpws21").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtpws21").focus();
                                    document.getElementById("ContentPlaceHolder1_txtpws21").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtpws31").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtpws31").focus();
                                    document.getElementById("ContentPlaceHolder1_txtpws31").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtpws34").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtpws34").focus();
                                    document.getElementById("ContentPlaceHolder1_txtpws34").style.borderColor = 'red';
                                }
                            }

                            if (txtpws12 != "" && txtpws22 != "" && txtpws32 != "" && txtpws35 != '') {

                                var PWS12 = parseFloat(txtpws12) + parseFloat(txtpws22) + parseFloat(txtpws32) + parseFloat(txtpws35);

                                if (PWS12 > 16) {
                                    // alert("Can not enter more than 16");
                                    document.getElementById("ContentPlaceHolder1_txtpws12").value = 16;
                                    //    document.getElementById("ContentPlaceHolder1_txtpws12").focus();
                                    //    document.getElementById("ContentPlaceHolder1_txtpws12").style.borderColor = 'red';
                                    //    document.getElementById("ContentPlaceHolder1_txtpws22").value = '';
                                    //    document.getElementById("ContentPlaceHolder1_txtpws22").focus();
                                    //    document.getElementById("ContentPlaceHolder1_txtpws22").style.borderColor = 'red';
                                    //    document.getElementById("ContentPlaceHolder1_txtpws32").value = '';
                                    //    document.getElementById("ContentPlaceHolder1_txtpws32").focus();
                                    //    document.getElementById("ContentPlaceHolder1_txtpws32").style.borderColor = 'red';
                                    //    document.getElementById("ContentPlaceHolder1_txtpws35").value = '';
                                    //    document.getElementById("ContentPlaceHolder1_txtpws35").focus();
                                    //    document.getElementById("ContentPlaceHolder1_txtpws35").style.borderColor = 'red';
                                }
                            }
                            //PP


                            var txtpp11 = document.getElementById("ContentPlaceHolder1_txtpp11").value;
                            if (txtpp11 > 30) {
                                alert("Can not enter more than 30");
                                document.getElementById("ContentPlaceHolder1_txtpp11").value = '';
                                document.getElementById("ContentPlaceHolder1_txtpp11").focus();
                                document.getElementById("ContentPlaceHolder1_txtpp11").style.borderColor = 'red';
                                return false;
                            }

                            var txtpp12 = document.getElementById("ContentPlaceHolder1_txtpp12").value;
                            //if (txtpp12 > 30) {
                            //    alert("Can not enter more than 30");
                            //    document.getElementById("ContentPlaceHolder1_txtpp12").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpp12").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpp12").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpp21 = document.getElementById("ContentPlaceHolder1_txtpp21").value;
                            if (txtpp21 > 30) {
                                alert("Can not enter more than 30");
                                document.getElementById("ContentPlaceHolder1_txtpp21").value = '';
                                document.getElementById("ContentPlaceHolder1_txtpp21").focus();
                                document.getElementById("ContentPlaceHolder1_txtpp21").style.borderColor = 'red';
                                return false;
                            }

                            var txtpp22 = document.getElementById("ContentPlaceHolder1_txtpp22").value;
                            //if (txtpp22 > 30) {
                            //    alert("Can not enter more than 30");
                            //    document.getElementById("ContentPlaceHolder1_txtpp22").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpp22").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpp22").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpp31 = document.getElementById("ContentPlaceHolder1_txtpp31").value;
                            if (txtpp31 > 30) {
                                alert("Can not enter more than 30");
                                document.getElementById("ContentPlaceHolder1_txtpp31").value = '';
                                document.getElementById("ContentPlaceHolder1_txtpp31").focus();
                                document.getElementById("ContentPlaceHolder1_txtpp31").style.borderColor = 'red';
                                return false;
                            }

                            var txtpp32 = document.getElementById("ContentPlaceHolder1_txtpp32").value;
                            //if (txtpp32 > 30) {
                            //    alert("Can not enter more than 30");
                            //    document.getElementById("ContentPlaceHolder1_txtpp32").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpp32").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpp32").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpp41 = document.getElementById("ContentPlaceHolder1_txtpp41").value;
                            if (txtpp41 > 30) {
                                alert("Can not enter more than 30");
                                document.getElementById("ContentPlaceHolder1_txtpp41").value = '';
                                document.getElementById("ContentPlaceHolder1_txtpp41").focus();
                                document.getElementById("ContentPlaceHolder1_txtpp41").style.borderColor = 'red';
                                return false;
                            }

                            var txtpp42 = document.getElementById("ContentPlaceHolder1_txtpp42").value;
                            //if (txtpp42 > 30) {
                            //    alert("Can not enter more than 30");
                            //    document.getElementById("ContentPlaceHolder1_txtpp42").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpp42").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpp42").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpp51 = document.getElementById("ContentPlaceHolder1_txtpp51").value;
                            if (txtpp51 > 30) {
                                alert("Can not enter more than 30");
                                document.getElementById("ContentPlaceHolder1_txtpp51").value = '';
                                document.getElementById("ContentPlaceHolder1_txtpp51").focus();
                                document.getElementById("ContentPlaceHolder1_txtpp51").style.borderColor = 'red';
                                return false;
                            }

                            var txtpp52 = document.getElementById("ContentPlaceHolder1_txtpp52").value;
                            //if (txtpp52 > 30) {
                            //    alert("Can not enter more than 30");
                            //    document.getElementById("ContentPlaceHolder1_txtpp52").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpp52").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpp52").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpp61 = document.getElementById("ContentPlaceHolder1_txtpp61").value;
                            if (txtpp61 > 30) {
                                alert("Can not enter more than 30");
                                document.getElementById("ContentPlaceHolder1_txtpp61").value = '';
                                document.getElementById("ContentPlaceHolder1_txtpp61").focus();
                                document.getElementById("ContentPlaceHolder1_txtpp61").style.borderColor = 'red';
                                return false;
                            }

                            var txtpp62 = document.getElementById("ContentPlaceHolder1_txtpp62").value;
                            //if (txtpp62 > 30) {
                            //    alert("Can not enter more than 30");
                            //    document.getElementById("ContentPlaceHolder1_txtpp62").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpp62").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpp62").style.borderColor = 'red';
                            //    return false;
                            //}


                            if (txtpp11 != "" && txtpp21 != "" && txtpp31 != "" && txtpp41 != "" && txtpp51 != "" && txtpp61 != "") {

                                var PP11 = parseFloat(txtpp11) + parseFloat(txtpp21) + parseFloat(txtpp31) + parseFloat(txtpp41) + parseFloat(txtpp51) + parseFloat(txtpp61);

                                if (PP11 > 30) {
                                    alert("Can not enter more than 30");
                                    document.getElementById("ContentPlaceHolder1_txtpp11").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtpp11").focus();
                                    document.getElementById("ContentPlaceHolder1_txtpp11").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtpp21").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtpp21").focus();
                                    document.getElementById("ContentPlaceHolder1_txtpp21").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtpp31").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtpp31").focus();
                                    document.getElementById("ContentPlaceHolder1_txtpp31").style.borderColor = 'red';

                                    document.getElementById("ContentPlaceHolder1_txtpp41").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtpp41").focus();
                                    document.getElementById("ContentPlaceHolder1_txtpp41").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtpp51").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtpp51").focus();
                                    document.getElementById("ContentPlaceHolder1_txtpp51").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtpp61").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtpp61").focus();
                                    document.getElementById("ContentPlaceHolder1_txtpp61").style.borderColor = 'red';
                                }
                            }


                            if (txtpp12 != "" && txtpp22 != "" && txtpp32 != "" && txtpp42 != "" && txtpp52 != "" && txtpp62 != "") {

                                var PP12 = parseFloat(txtpp12) + parseFloat(txtpp22) + parseFloat(txtpp32) + parseFloat(txtpp42) + parseFloat(txtpp52) + parseFloat(txtpp62);

                                //if (PP12 > 30) {
                                //    alert("Can not enter more than 30");
                                //    document.getElementById("ContentPlaceHolder1_txtpp12").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpp12").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpp12").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpp22").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpp22").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpp22").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpp32").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpp32").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpp32").style.borderColor = 'red';

                                //    document.getElementById("ContentPlaceHolder1_txtpp42").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpp42").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpp42").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpp52").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpp52").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpp52").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpp62").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpp62").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpp62").style.borderColor = 'red';
                                //}
                            }

                            var txtrs11 = document.getElementById("ContentPlaceHolder1_txtrs11").value;
                            if (txtrs11 > 5) {
                                alert("Can not enter more than 5");
                                document.getElementById("ContentPlaceHolder1_txtrs11").value = '';
                                document.getElementById("ContentPlaceHolder1_txtrs11").focus();
                                document.getElementById("ContentPlaceHolder1_txtrs11").style.borderColor = 'red';
                                return false;
                            }

                            var txtrs12 = document.getElementById("ContentPlaceHolder1_txtrs12").value;
                            //if (txtrs12 > 5) {
                            //    alert("Can not enter more than 5");
                            //    document.getElementById("ContentPlaceHolder1_txtrs12").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtrs12").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtrs12").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtrs21 = document.getElementById("ContentPlaceHolder1_txtrs21").value;
                            if (txtrs21 > 5) {
                                alert("Can not enter more than 5");
                                document.getElementById("ContentPlaceHolder1_txtrs21").value = '';
                                document.getElementById("ContentPlaceHolder1_txtrs21").focus();
                                document.getElementById("ContentPlaceHolder1_txtrs21").style.borderColor = 'red';
                                return false;
                            }

                            var txtrs22 = document.getElementById("ContentPlaceHolder1_txtrs22").value;
                            //if (txtrs22 > 5) {
                            //    alert("Can not enter more than 5");
                            //    document.getElementById("ContentPlaceHolder1_txtrs22").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtrs22").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtrs22").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtrs31 = document.getElementById("ContentPlaceHolder1_txtrs31").value;
                            if (txtrs31 > 5) {
                                alert("Can not enter more than 5");
                                document.getElementById("ContentPlaceHolder1_txtrs31").value = '';
                                document.getElementById("ContentPlaceHolder1_txtrs31").focus();
                                document.getElementById("ContentPlaceHolder1_txtrs31").style.borderColor = 'red';
                                return false;
                            }



                            var txtrs32 = document.getElementById("ContentPlaceHolder1_txtrs32").value;
                            //if (txtrs32 > 5) {
                            //    alert("Can not enter more than 5");
                            //    document.getElementById("ContentPlaceHolder1_txtrs32").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtrs32").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtrs32").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtrs41 = document.getElementById("ContentPlaceHolder1_txtrs41").value;
                            if (txtrs41 > 5) {
                                alert("Can not enter more than 5");
                                document.getElementById("ContentPlaceHolder1_txtrs41").value = '';
                                document.getElementById("ContentPlaceHolder1_txtrs41").focus();
                                document.getElementById("ContentPlaceHolder1_txtrs41").style.borderColor = 'red';
                                return false;
                            }

                            var txtrs42 = document.getElementById("ContentPlaceHolder1_txtrs42").value;
                            //if (txtrs42 > 5) {
                            //    alert("Can not enter more than 5");
                            //    document.getElementById("ContentPlaceHolder1_txtrs42").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtrs42").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtrs42").style.borderColor = 'red';
                            //    return false;
                            //}


                            if (txtrs11 != "" && txtrs21 != "" && txtrs31 != "" && txtrs41 != "") {

                                var RS11 = parseFloat(txtrs11) + parseFloat(txtrs21) + parseFloat(txtrs31) + parseFloat(txtrs41);

                                if (RS11 > 5) {
                                    alert("Can not enter more than 5");
                                    document.getElementById("ContentPlaceHolder1_txtrs11").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtrs11").focus();
                                    document.getElementById("ContentPlaceHolder1_txtrs11").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtrs21").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtrs21").focus();
                                    document.getElementById("ContentPlaceHolder1_txtrs21").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtrs31").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtrs31").focus();
                                    document.getElementById("ContentPlaceHolder1_txtrs31").style.borderColor = 'red';

                                    document.getElementById("ContentPlaceHolder1_txtrs41").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtrs41").focus();
                                    document.getElementById("ContentPlaceHolder1_txtrs41").style.borderColor = 'red';

                                }
                            }

                            if (txtrs12 != "" && txtrs22 != "" && txtrs32 != "" && txtrs42 != "") {

                                var RS12 = parseFloat(txtrs12) + parseFloat(txtrs22) + parseFloat(txtrs32) + parseFloat(txtrs42);

                                //if (RS12 > 5) {
                                //    alert("Can not enter more than 5");
                                //    document.getElementById("ContentPlaceHolder1_txtrs12").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtrs12").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtrs12").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtrs22").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtrs22").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtrs22").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtrs32").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtrs32").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtrs32").style.borderColor = 'red';

                                //    document.getElementById("ContentPlaceHolder1_txtrs42").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtrs42").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtrs42").style.borderColor = 'red';

                                //}
                            }



                            var txtrpp11 = document.getElementById("ContentPlaceHolder1_txtrpp11").value;
                            if (txtrpp11 > 10) {
                                alert("Can not enter more than 10");
                                document.getElementById("ContentPlaceHolder1_txtrpp11").value = '';
                                document.getElementById("ContentPlaceHolder1_txtrpp11").focus();
                                document.getElementById("ContentPlaceHolder1_txtrpp11").style.borderColor = 'red';
                                return false;
                            }

                            var txtrpp12 = document.getElementById("ContentPlaceHolder1_txtrpp12").value;
                            //if (txtrpp12 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtrpp12").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtrpp12").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtrpp12").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtrpp21 = document.getElementById("ContentPlaceHolder1_txtrpp21").value;
                            if (txtrpp21 > 10) {
                                alert("Can not enter more than 10");
                                document.getElementById("ContentPlaceHolder1_txtrpp21").value = '';
                                document.getElementById("ContentPlaceHolder1_txtrpp21").focus();
                                document.getElementById("ContentPlaceHolder1_txtrpp21").style.borderColor = 'red';
                                return false;
                            }

                            var txtrpp22 = document.getElementById("ContentPlaceHolder1_txtrpp22").value;
                            //if (txtrpp22 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtrpp22").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtrpp22").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtrpp22").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtrpp31 = document.getElementById("ContentPlaceHolder1_txtrpp31").value;
                            if (txtrpp31 > 10) {
                                alert("Can not enter more than 10");
                                document.getElementById("ContentPlaceHolder1_txtrpp31").value = '';
                                document.getElementById("ContentPlaceHolder1_txtrpp31").focus();
                                document.getElementById("ContentPlaceHolder1_txtrpp31").style.borderColor = 'red';
                                return false;
                            }

                            var txtrpp32 = document.getElementById("ContentPlaceHolder1_txtrpp32").value;
                            //if (txtrpp32 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtrpp32").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtrpp32").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtrpp32").style.borderColor = 'red';
                            //    return false;
                            //}



                            if (txtrpp11 != "" && txtrpp21 != "" && txtrpp31 != "") {

                                var RPP11 = parseFloat(txtrpp11) + parseFloat(txtrpp21) + parseFloat(txtrpp31);

                                if (RPP11 > 10) {
                                    alert("Can not enter more than 10");
                                    document.getElementById("ContentPlaceHolder1_txtrpp11").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtrpp11").focus();
                                    document.getElementById("ContentPlaceHolder1_txtrpp11").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtrpp21").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtrpp21").focus();
                                    document.getElementById("ContentPlaceHolder1_txtrpp21").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtrpp31").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtrpp31").focus();
                                    document.getElementById("ContentPlaceHolder1_txtrpp31").style.borderColor = 'red';


                                }
                            }

                            if (txtrpp12 != "" && txtrpp22 != "" && txtrpp32 != "") {

                                var RPP12 = parseFloat(txtrpp12) + parseFloat(txtrpp22) + parseFloat(txtrpp32);

                                //if (RPP12 > 10) {
                                //    alert("Can not enter more than 10");
                                //    document.getElementById("ContentPlaceHolder1_txtrpp12").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtrpp12").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtrpp12").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtrpp22").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtrpp22").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtrpp22").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtrpp32").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtrpp32").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtrpp32").style.borderColor = 'red';


                                //}
                            }





                            var txtrpro11 = document.getElementById("ContentPlaceHolder1_txtrpro11").value;
                            if (txtrpro11 > 15) {
                                alert("Can not enter more than 15");
                                document.getElementById("ContentPlaceHolder1_txtrpro11").value = '';
                                document.getElementById("ContentPlaceHolder1_txtrpro11").focus();
                                document.getElementById("ContentPlaceHolder1_txtrpro11").style.borderColor = 'red';
                                return false;
                            }

                            var txtrpro12 = document.getElementById("ContentPlaceHolder1_txtrpro12").value;
                            //if (txtrpro12 > 15) {
                            //    alert("Can not enter more than 15");
                            //    document.getElementById("ContentPlaceHolder1_txtrpro12").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtrpro12").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtrpro12").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtrpro21 = document.getElementById("ContentPlaceHolder1_txtrpro21").value;
                            if (txtrpro21 > 15) {
                                alert("Can not enter more than 15");
                                document.getElementById("ContentPlaceHolder1_txtrpro21").value = '';
                                document.getElementById("ContentPlaceHolder1_txtrpro21").focus();
                                document.getElementById("ContentPlaceHolder1_txtrpro21").style.borderColor = 'red';
                                return false;
                            }

                            var txtrpro22 = document.getElementById("ContentPlaceHolder1_txtrpro22").value;
                            //if (txtrpro22 > 15) {
                            //    alert("Can not enter more than 15");
                            //    document.getElementById("ContentPlaceHolder1_txtrpro22").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtrpro22").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtrpro22").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtrpro31 = document.getElementById("ContentPlaceHolder1_txtrpro31").value;
                            if (txtrpro31 > 15) {
                                alert("Can not enter more than 15");
                                document.getElementById("ContentPlaceHolder1_txtrpro31").value = '';
                                document.getElementById("ContentPlaceHolder1_txtrpro31").focus();
                                document.getElementById("ContentPlaceHolder1_txtrpro31").style.borderColor = 'red';
                                return false;
                            }

                            var txtrpro32 = document.getElementById("ContentPlaceHolder1_txtrpro32").value;
                            //if (txtrpro32 > 15) {
                            //    alert("Can not enter more than 15");
                            //    document.getElementById("ContentPlaceHolder1_txtrpro32").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtrpro32").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtrpro32").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtrpro41 = document.getElementById("ContentPlaceHolder1_txtrpro41").value;
                            if (txtrpro41 > 15) {
                                alert("Can not enter more than 15");
                                document.getElementById("ContentPlaceHolder1_txtrpro41").value = '';
                                document.getElementById("ContentPlaceHolder1_txtrpro41").focus();
                                document.getElementById("ContentPlaceHolder1_txtrpro41").style.borderColor = 'red';
                                return false;
                            }

                            var txtrpro42 = document.getElementById("ContentPlaceHolder1_txtrpro42").value;
                            //if (txtrpro42 > 15) {
                            //    alert("Can not enter more than 15");
                            //    document.getElementById("ContentPlaceHolder1_txtrpro42").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtrpro42").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtrpro42").style.borderColor = 'red';
                            //    return false;
                            //}


                            var txtrpro51 = document.getElementById("ContentPlaceHolder1_txtrpro51").value;

                            if (txtrpro51 > 15) {
                                alert("Can not enter more than 15");
                                document.getElementById("ContentPlaceHolder1_txtrpro51").value = '';
                                document.getElementById("ContentPlaceHolder1_txtrpro51").focus();
                                document.getElementById("ContentPlaceHolder1_txtrpro51").style.borderColor = 'red';
                                return false;
                            }

                            var txtrpro52 = document.getElementById("ContentPlaceHolder1_txtrpro52").value;
                            //if (txtrpro52 > 15) {
                            //    alert("Can not enter more than 15");
                            //    document.getElementById("ContentPlaceHolder1_txtrpro52").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtrpro52").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtrpro52").style.borderColor = 'red';
                            //    return false;
                            //}
                            var txtrpro54 = document.getElementById("ContentPlaceHolder1_txtrpro54").value;
                            if (txtrpro54 > 15) {
                                alert("Can not enter more than 15");
                                document.getElementById("ContentPlaceHolder1_txtrpro54").value = '';
                                document.getElementById("ContentPlaceHolder1_txtrpro54").focus();
                                document.getElementById("ContentPlaceHolder1_txtrpro54").style.borderColor = 'red';
                                return false;
                            }

                            var txtrpro55 = document.getElementById("ContentPlaceHolder1_txtrpro55").value;
                            //if (txtrpro55 > 15) {
                            //    alert("Can not enter more than 15");
                            //    document.getElementById("ContentPlaceHolder1_txtrpro55").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtrpro55").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtrpro55").style.borderColor = 'red';
                            //    return false;
                            //}


                            if (txtrpro11 != "" && txtrpro21 != "" && txtrpro31 != "" && txtrpro41 != "" && txtrpro51 != "" && txtrpro54 != "") {

                                var RPPF11 = parseFloat(txtrpro11) + parseFloat(txtrpro21) + parseFloat(txtrpro31) + parseFloat(txtrpro41) + parseFloat(txtrpro51) + parseFloat(txtrpro54);

                                if (RPPF11 > 15) {
                                    alert("Can not enter more than 15");
                                    document.getElementById("ContentPlaceHolder1_txtrpro11").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtrpro11").focus();
                                    document.getElementById("ContentPlaceHolder1_txtrpro11").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtrpro21").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtrpro21").focus();
                                    document.getElementById("ContentPlaceHolder1_txtrpro21").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtrpro31").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtrpro31").focus();
                                    document.getElementById("ContentPlaceHolder1_txtrpro31").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtrpro41").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtrpro41").focus();
                                    document.getElementById("ContentPlaceHolder1_txtrpro41").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtrpro51").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtrpro51").focus();
                                    document.getElementById("ContentPlaceHolder1_txtrpro51").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtrpro54").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtrpro54").focus();
                                    document.getElementById("ContentPlaceHolder1_txtrpro54").style.borderColor = 'red';
                                }
                            }



                            if (txtrpro12 != "" && txtrpro22 != "" && txtrpro32 != "" && txtrpro42 != "" && txtrpro52 != "" && txtrpro55 != "") {

                                var RPPF12 = parseFloat(txtrpro12) + parseFloat(txtrpro22) + parseFloat(txtrpro32) + parseFloat(txtrpro42) + parseFloat(txtrpro52) + parseFloat(txtrpro55);

                                //if (RPPF12 > 20) {
                                //    alert("Can not enter more than 20");
                                //    document.getElementById("ContentPlaceHolder1_txtrpro12").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtrpro12").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtrpro12").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtrpro22").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtrpro22").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtrpro22").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtrpro32").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtrpro32").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtrpro32").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtrpro42").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtrpro42").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtrpro42").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtrpro52").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtrpro52").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtrpro52").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtrpro55").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtrpro55").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtrpro55").style.borderColor = 'red';
                                //}
                            }



                            var txtinb11 = document.getElementById("ContentPlaceHolder1_txtinb11").value;
                            if (txtinb11 > 20) {
                                alert("Can not enter more than 20");
                                document.getElementById("ContentPlaceHolder1_txtinb11").value = '';
                                document.getElementById("ContentPlaceHolder1_txtinb11").focus();
                                document.getElementById("ContentPlaceHolder1_txtinb11").style.borderColor = 'red';
                                return false;
                            }

                            var txtinb12 = document.getElementById("ContentPlaceHolder1_txtinb12").value;
                            //if (txtinb12 > 20) {
                            //    alert("Can not enter more than 20");
                            //    document.getElementById("ContentPlaceHolder1_txtinb12").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtinb12").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtinb12").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtinb21 = document.getElementById("ContentPlaceHolder1_txtinb21").value;
                            if (txtinb21 > 20) {
                                alert("Can not enter more than 20");
                                document.getElementById("ContentPlaceHolder1_txtinb21").value = '';
                                document.getElementById("ContentPlaceHolder1_txtinb21").focus();
                                document.getElementById("ContentPlaceHolder1_txtinb21").style.borderColor = 'red';
                                return false;
                            }
                            var txtinb22 = document.getElementById("ContentPlaceHolder1_txtinb22").value;
                            //if (txtinb22 > 20) {
                            //    alert("Can not enter more than 20");
                            //    document.getElementById("ContentPlaceHolder1_txtinb22").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtinb22").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtinb22").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtinb31 = document.getElementById("ContentPlaceHolder1_txtinb31").value;
                            if (txtinb31 > 20) {
                                alert("Can not enter more than 20");
                                document.getElementById("ContentPlaceHolder1_txtinb31").value = '';
                                document.getElementById("ContentPlaceHolder1_txtinb31").focus();
                                document.getElementById("ContentPlaceHolder1_txtinb31").style.borderColor = 'red';
                                return false;
                            }

                            var txtinb32 = document.getElementById("ContentPlaceHolder1_txtinb32").value;
                            //if (txtinb32 > 20) {
                            //    alert("Can not enter more than 20");
                            //    document.getElementById("ContentPlaceHolder1_txtinb32").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtinb32").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtinb32").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtinb41 = document.getElementById("ContentPlaceHolder1_txtinb41").value;
                            if (txtinb41 > 20) {
                                alert("Can not enter more than 20");
                                document.getElementById("ContentPlaceHolder1_txtinb41").value = '';
                                document.getElementById("ContentPlaceHolder1_txtinb41").focus();
                                document.getElementById("ContentPlaceHolder1_txtinb41").style.borderColor = 'red';
                                return false;
                            }

                            var txtinb42 = document.getElementById("ContentPlaceHolder1_txtinb42").value;
                            //if (txtinb42 > 20) {
                            //    alert("Can not enter more than 20");
                            //    document.getElementById("ContentPlaceHolder1_txtinb42").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtinb42").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtinb42").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtinb51 = document.getElementById("ContentPlaceHolder1_txtinb51").value;
                            if (txtinb51 > 20) {
                                alert("Can not enter more than 20");
                                document.getElementById("ContentPlaceHolder1_txtinb51").value = '';
                                document.getElementById("ContentPlaceHolder1_txtinb51").focus();
                                document.getElementById("ContentPlaceHolder1_txtinb51").style.borderColor = 'red';
                                return false;
                            }

                            var txtinb52 = document.getElementById("ContentPlaceHolder1_txtinb52").value;
                            //if (txtinb52 > 20) {
                            //    alert("Can not enter more than 20");
                            //    document.getElementById("ContentPlaceHolder1_txtinb52").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtinb52").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtinb52").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtinb61 = document.getElementById("ContentPlaceHolder1_txtinb61").value;
                            if (txtinb61 > 20) {
                                alert("Can not enter more than 20");
                                document.getElementById("ContentPlaceHolder1_txtinb61").value = '';
                                document.getElementById("ContentPlaceHolder1_txtinb61").focus();
                                document.getElementById("ContentPlaceHolder1_txtinb61").style.borderColor = 'red';
                                return false;
                            }

                            var txtinb62 = document.getElementById("ContentPlaceHolder1_txtinb62").value;
                            //if (txtinb62 > 20) {
                            //    alert("Can not enter more than 20");
                            //    document.getElementById("ContentPlaceHolder1_txtinb62").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtinb62").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtinb62").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtinb71 = document.getElementById("ContentPlaceHolder1_txtinb71").value;
                            if (txtinb71 > 20) {
                                alert("Can not enter more than 20");
                                document.getElementById("ContentPlaceHolder1_txtinb71").value = '';
                                document.getElementById("ContentPlaceHolder1_txtinb71").focus();
                                document.getElementById("ContentPlaceHolder1_txtinb71").style.borderColor = 'red';
                                return false;
                            }

                            var txtinb72 = document.getElementById("ContentPlaceHolder1_txtinb72").value;
                            //if (txtinb72 > 20) {
                            //    alert("Can not enter more than 20");
                            //    document.getElementById("ContentPlaceHolder1_txtinb72").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtinb72").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtinb72").style.borderColor = 'red';
                            //    return false;
                            //}
                            var txtinb74 = document.getElementById("ContentPlaceHolder1_txtinb74").value;
                            if (txtinb74 > 20) {
                                alert("Can not enter more than 20");
                                document.getElementById("ContentPlaceHolder1_txtinb74").value = '';
                                document.getElementById("ContentPlaceHolder1_txtinb74").focus();
                                document.getElementById("ContentPlaceHolder1_txtinb74").style.borderColor = 'red';
                                return false;
                            }
                            var txtinb75 = document.getElementById("ContentPlaceHolder1_txtinb75").value;
                            //if (txtinb75 > 20) {
                            //    alert("Can not enter more than 20");
                            //    document.getElementById("ContentPlaceHolder1_txtinb75").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtinb75").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtinb75").style.borderColor = 'red';
                            //    return false;
                            //}
                            var txtinb77 = document.getElementById("ContentPlaceHolder1_txtinb77").value;
                            if (txtinb77 > 20) {
                                alert("Can not enter more than 20");
                                document.getElementById("ContentPlaceHolder1_txtinb77").value = '';
                                document.getElementById("ContentPlaceHolder1_txtinb77").focus();
                                document.getElementById("ContentPlaceHolder1_txtinb77").style.borderColor = 'red';
                                return false;
                            }
                            var txtinb78 = document.getElementById("ContentPlaceHolder1_txtinb78").value;
                            //if (txtinb78 > 20) {
                            //    alert("Can not enter more than 20");
                            //    document.getElementById("ContentPlaceHolder1_txtinb78").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtinb78").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtinb78").style.borderColor = 'red';
                            //    return false;
                            //}


                            if (txtinb11 != "" && txtinb21 != "" && txtinb31 != "" && txtinb41 != "" && txtinb51 != "" && txtinb61 != "" && txtinb71 != "" && txtinb74 != "" && txtinb77 != "") {

                                var INB11 = parseFloat(txtinb11) + parseFloat(txtinb21) + parseFloat(txtinb31) + parseFloat(txtinb41) + parseFloat(txtinb51) + parseFloat(txtinb61) + parseFloat(txtinb71) + parseFloat(txtinb74) + parseFloat(txtinb77);

                                if (INB11 > 20) {
                                    alert("Can not enter more than 20");
                                    document.getElementById("ContentPlaceHolder1_txtinb11").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtinb11").focus();
                                    document.getElementById("ContentPlaceHolder1_txtinb11").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtinb21").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtinb21").focus();
                                    document.getElementById("ContentPlaceHolder1_txtinb21").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtinb31").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtinb31").focus();
                                    document.getElementById("ContentPlaceHolder1_txtinb31").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtinb41").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtinb41").focus();
                                    document.getElementById("ContentPlaceHolder1_txtinb41").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtinb51").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtinb51").focus();
                                    document.getElementById("ContentPlaceHolder1_txtinb51").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtinb61").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtinb61").focus();
                                    document.getElementById("ContentPlaceHolder1_txtinb61").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtinb71").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtinb71").focus();
                                    document.getElementById("ContentPlaceHolder1_txtinb71").style.borderColor = 'red';

                                    document.getElementById("ContentPlaceHolder1_txtinb74").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtinb74").focus();
                                    document.getElementById("ContentPlaceHolder1_txtinb74").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtinb77").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtinb77").focus();
                                    document.getElementById("ContentPlaceHolder1_txtinb77").style.borderColor = 'red';
                                }
                            }


                            if (txtinb12 != "" && txtinb22 != "" && txtinb32 != "" && txtinb42 != "" && txtinb52 != "" && txtinb62 != "" && txtinb72 != "" && txtinb75 != "" && txtinb78 != "") {

                                var INB11 = parseFloat(txtinb12) + parseFloat(txtinb22) + parseFloat(txtinb32) + parseFloat(txtinb42) + parseFloat(txtinb52) + parseFloat(txtinb62) + parseFloat(txtinb72) + parseFloat(txtinb75) + parseFloat(txtinb78);

                                //if (INB11 > 20) {
                                //    alert("Can not enter more than 20");
                                //    document.getElementById("ContentPlaceHolder1_txtinb12").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtinb12").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtinb12").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtinb22").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtinb22").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtinb22").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtinb32").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtinb32").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtinb32").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtinb42").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtinb42").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtinb42").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtinb52").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtinb52").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtinb52").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtinb62").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtinb62").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtinb62").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtinb72").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtinb72").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtinb72").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtinb75").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtinb75").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtinb75").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtinb78").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtinb78").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtinb78").style.borderColor = 'red';
                                //}
                            }


                            var txtsd11 = document.getElementById("ContentPlaceHolder1_txtsd11").value;
                            if (txtsd11 > 15) {
                                alert("Can not enter more than 15");
                                document.getElementById("ContentPlaceHolder1_txtsd11").value = '';
                                document.getElementById("ContentPlaceHolder1_txtsd11").focus();
                                document.getElementById("ContentPlaceHolder1_txtsd11").style.borderColor = 'red';
                                return false;
                            }

                            var txtsd12 = document.getElementById("ContentPlaceHolder1_txtsd12").value;
                            //if (txtsd12 > 15) {
                            //    alert("Can not enter more than 15");
                            //    document.getElementById("ContentPlaceHolder1_txtsd12").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtsd12").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtsd12").style.borderColor = 'red';
                            //    return false;
                            //}


                            var txtsd21 = document.getElementById("ContentPlaceHolder1_txtsd21").value;
                            if (txtsd21 > 15) {
                                alert("Can not enter more than 15");
                                document.getElementById("ContentPlaceHolder1_txtsd21").value = '';
                                document.getElementById("ContentPlaceHolder1_txtsd21").focus();
                                document.getElementById("ContentPlaceHolder1_txtsd21").style.borderColor = 'red';
                                return false;
                            }

                            var txtsd22 = document.getElementById("ContentPlaceHolder1_txtsd22").value;
                            //if (txtsd22 > 15) {
                            //    alert("Can not enter more than 15");
                            //    document.getElementById("ContentPlaceHolder1_txtsd22").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtsd22").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtsd22").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtsd31 = document.getElementById("ContentPlaceHolder1_txtsd31").value;
                            if (txtsd31 > 15) {
                                alert("Can not enter more than 15");
                                document.getElementById("ContentPlaceHolder1_txtsd31").value = '';
                                document.getElementById("ContentPlaceHolder1_txtsd31").focus();
                                document.getElementById("ContentPlaceHolder1_txtsd31").style.borderColor = 'red';
                                return false;
                            }

                            var txtsd32 = document.getElementById("ContentPlaceHolder1_txtsd32").value;
                            //if (txtsd32 > 15) {
                            //    alert("Can not enter more than 15");
                            //    document.getElementById("ContentPlaceHolder1_txtsd32").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtsd32").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtsd32").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtsd41 = document.getElementById("ContentPlaceHolder1_txtsd41").value;
                            if (txtsd41 > 15) {
                                alert("Can not enter more than 15");
                                document.getElementById("ContentPlaceHolder1_txtsd41").value = '';
                                document.getElementById("ContentPlaceHolder1_txtsd41").focus();
                                document.getElementById("ContentPlaceHolder1_txtsd41").style.borderColor = 'red';
                                return false;
                            }

                            var txtsd42 = document.getElementById("ContentPlaceHolder1_txtsd42").value;
                            //if (txtsd42 > 15) {
                            //    alert("Can not enter more than 15");
                            //    document.getElementById("ContentPlaceHolder1_txtsd42").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtsd42").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtsd42").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtsd51 = document.getElementById("ContentPlaceHolder1_txtsd51").value;
                            if (txtsd51 > 15) {
                                alert("Can not enter more than 15");
                                document.getElementById("ContentPlaceHolder1_txtsd51").value = '';
                                document.getElementById("ContentPlaceHolder1_txtsd51").focus();
                                document.getElementById("ContentPlaceHolder1_txtsd51").style.borderColor = 'red';
                                return false;
                            }

                            var txtsd52 = document.getElementById("ContentPlaceHolder1_txtsd52").value;
                            //if (txtsd52 > 15) {
                            //    alert("Can not enter more than 15");
                            //    document.getElementById("ContentPlaceHolder1_txtsd52").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtsd52").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtsd52").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtsd61 = document.getElementById("ContentPlaceHolder1_txtsd61").value;
                            if (txtsd61 > 15) {
                                alert("Can not enter more than 15");
                                document.getElementById("ContentPlaceHolder1_txtsd61").value = '';
                                document.getElementById("ContentPlaceHolder1_txtsd61").focus();
                                document.getElementById("ContentPlaceHolder1_txtsd61").style.borderColor = 'red';
                                return false;
                            }

                            var txtsd62 = document.getElementById("ContentPlaceHolder1_txtsd62").value;
                            //if (txtsd62 > 15) {
                            //    alert("Can not enter more than 15");
                            //    document.getElementById("ContentPlaceHolder1_txtsd62").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtsd62").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtsd62").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtsd71 = document.getElementById("ContentPlaceHolder1_txtsd71").value;
                            if (txtsd71 > 15) {
                                alert("Can not enter more than 15");
                                document.getElementById("ContentPlaceHolder1_txtsd71").value = '';
                                document.getElementById("ContentPlaceHolder1_txtsd71").focus();
                                document.getElementById("ContentPlaceHolder1_txtsd71").style.borderColor = 'red';
                                return false;
                            }

                            var txtsd72 = document.getElementById("ContentPlaceHolder1_txtsd72").value;
                            //if (txtsd72 > 15) {
                            //    alert("Can not enter more than 15");
                            //    document.getElementById("ContentPlaceHolder1_txtsd72").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtsd72").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtsd72").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtsd74 = document.getElementById("ContentPlaceHolder1_txtsd74").value;
                            if (txtsd74 > 15) {
                                alert("Can not enter more than 15");
                                document.getElementById("ContentPlaceHolder1_txtsd74").value = '';
                                document.getElementById("ContentPlaceHolder1_txtsd74").focus();
                                document.getElementById("ContentPlaceHolder1_txtsd74").style.borderColor = 'red';
                                return false;
                            }
                            var txtsd75 = document.getElementById("ContentPlaceHolder1_txtsd75").value;
                            //if (txtsd75 > 15) {
                            //    alert("Can not enter more than 15");
                            //    document.getElementById("ContentPlaceHolder1_txtsd75").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtsd75").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtsd75").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtsd77 = document.getElementById("ContentPlaceHolder1_txtsd77").value;
                            if (txtsd77 > 15) {
                                alert("Can not enter more than 15");
                                document.getElementById("ContentPlaceHolder1_txtsd77").value = '';
                                document.getElementById("ContentPlaceHolder1_txtsd77").focus();
                                document.getElementById("ContentPlaceHolder1_txtsd77").style.borderColor = 'red';
                                return false;
                            }
                            var txtsd78 = document.getElementById("ContentPlaceHolder1_txtsd78").value;
                            //if (txtsd78 > 15) {
                            //    alert("Can not enter more than 15");
                            //    document.getElementById("ContentPlaceHolder1_txtsd78").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtsd78").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtsd78").style.borderColor = 'red';
                            //    return false;
                            //}



                            if (txtsd11 != "" && txtsd21 != "" && txtsd31 != "" && txtsd41 != "" && txtsd51 != "" && txtsd61 != "" && txtsd71 != "" && txtsd74 != "" && txtsd77 != "") {

                                var SD11 = parseFloat(txtsd11) + parseFloat(txtsd21) + parseFloat(txtsd31) + parseFloat(txtsd41) + parseFloat(txtsd51) + parseFloat(txtsd61) + parseFloat(txtsd71) + parseFloat(txtsd74) + parseFloat(txtsd77);

                                if (SD11 > 15) {
                                    alert("Can not enter more than 15");
                                    document.getElementById("ContentPlaceHolder1_txtsd11").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtsd11").focus();
                                    document.getElementById("ContentPlaceHolder1_txtsd11").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtsd21").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtsd21").focus();
                                    document.getElementById("ContentPlaceHolder1_txtsd21").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtsd31").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtsd31").focus();
                                    document.getElementById("ContentPlaceHolder1_txtsd31").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtsd41").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtsd41").focus();
                                    document.getElementById("ContentPlaceHolder1_txtsd41").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtsd51").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtsd51").focus();
                                    document.getElementById("ContentPlaceHolder1_txtsd51").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtsd61").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtsd61").focus();
                                    document.getElementById("ContentPlaceHolder1_txtsd61").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtsd71").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtsd71").focus();
                                    document.getElementById("ContentPlaceHolder1_txtsd71").style.borderColor = 'red';


                                    document.getElementById("ContentPlaceHolder1_txtsd74").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtsd74").focus();
                                    document.getElementById("ContentPlaceHolder1_txtsd74").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtsd77").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtsd77").focus();
                                    document.getElementById("ContentPlaceHolder1_txtsd77").style.borderColor = 'red';
                                }
                            }


                            if (txtsd12 != "" && txtsd22 != "" && txtsd32 != "" && txtsd42 != "" && txtsd52 != "" && txtsd62 != "" && txtsd72 != "" && txtsd75 != "" && txtsd78 != "") {

                                var SD12 = parseFloat(txtsd12) + parseFloat(txtsd22) + parseFloat(txtsd32) + parseFloat(txtsd42) + parseFloat(txtsd52) + parseFloat(txtsd62) + parseFloat(txtsd72) + parseFloat(txtsd75) + parseFloat(txtsd78);

                                //if (SD12 > 15) {
                                //    alert("Can not enter more than 15");
                                //    document.getElementById("ContentPlaceHolder1_txtsd12").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtsd12").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtsd12").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtsd22").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtsd22").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtsd22").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtsd32").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtsd32").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtsd32").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtsd42").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtsd42").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtsd42").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtsd52").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtsd52").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtsd52").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtsd62").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtsd62").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtsd62").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtsd72").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtsd72").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtsd72").style.borderColor = 'red';

                                //    document.getElementById("ContentPlaceHolder1_txtsd75").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtsd75").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtsd75").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtsd78").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtsd78").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtsd78").style.borderColor = 'red';
                                //}
                            }


                            var txtpcm11 = document.getElementById("ContentPlaceHolder1_txtpcm11").value;
                            if (txtpcm11 > 15) {
                                alert("Can not enter more than 15");
                                document.getElementById("ContentPlaceHolder1_txtpcm11").value = '';
                                document.getElementById("ContentPlaceHolder1_txtpcm11").focus();
                                document.getElementById("ContentPlaceHolder1_txtpcm11").style.borderColor = 'red';
                                return false;
                            }

                            var txtpcm12 = document.getElementById("ContentPlaceHolder1_txtpcm12").value;
                            //if (txtpcm12 > 20) {
                            //    alert("Can not enter more than 20");
                            //    document.getElementById("ContentPlaceHolder1_txtpcm12").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpcm12").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpcm12").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpcm21 = document.getElementById("ContentPlaceHolder1_txtpcm21").value;
                            if (txtpcm21 > 15) {
                                alert("Can not enter more than 15");
                                document.getElementById("ContentPlaceHolder1_txtpcm21").value = '';
                                document.getElementById("ContentPlaceHolder1_txtpcm21").focus();
                                document.getElementById("ContentPlaceHolder1_txtpcm21").style.borderColor = 'red';
                                return false;
                            }

                            var txtpcm22 = document.getElementById("ContentPlaceHolder1_txtpcm22").value;
                            //if (txtpcm22 > 20) {
                            //    alert("Can not enter more than 20");
                            //    document.getElementById("ContentPlaceHolder1_txtpcm22").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpcm22").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpcm22").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpcm31 = document.getElementById("ContentPlaceHolder1_txtpcm31").value;
                            if (txtpcm31 > 15) {
                                alert("Can not enter more than 15");
                                document.getElementById("ContentPlaceHolder1_txtpcm31").value = '';
                                document.getElementById("ContentPlaceHolder1_txtpcm31").focus();
                                document.getElementById("ContentPlaceHolder1_txtpcm31").style.borderColor = 'red';
                                return false;
                            }

                            var txtpcm32 = document.getElementById("ContentPlaceHolder1_txtpcm32").value;
                            //if (txtpcm32 > 20) {
                            //    alert("Can not enter more than 18");
                            //    document.getElementById("ContentPlaceHolder1_txtpcm32").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpcm32").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpcm32").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpcm41 = document.getElementById("ContentPlaceHolder1_txtpcm41").value;
                            if (txtpcm41 > 15) {
                                alert("Can not enter more than 15");
                                document.getElementById("ContentPlaceHolder1_txtpcm41").value = '';
                                document.getElementById("ContentPlaceHolder1_txtpcm41").focus();
                                document.getElementById("ContentPlaceHolder1_txtpcm41").style.borderColor = 'red';
                                return false;
                            }

                            var txtpcm42 = document.getElementById("ContentPlaceHolder1_txtpcm42").value;
                            //if (txtpcm42 > 20) {
                            //    alert("Can not enter more than 20");
                            //    document.getElementById("ContentPlaceHolder1_txtpcm42").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpcm42").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpcm42").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpcm51 = document.getElementById("ContentPlaceHolder1_txtpcm51").value;
                            if (txtpcm51 > 15) {
                                alert("Can not enter more than 15");
                                document.getElementById("ContentPlaceHolder1_txtpcm51").value = '';
                                document.getElementById("ContentPlaceHolder1_txtpcm51").focus();
                                document.getElementById("ContentPlaceHolder1_txtpcm51").style.borderColor = 'red';
                                return false;
                            }

                            var txtpcm52 = document.getElementById("ContentPlaceHolder1_txtpcm52").value;
                            //if (txtpcm52 > 20) {
                            //    alert("Can not enter more than 20");
                            //    document.getElementById("ContentPlaceHolder1_txtpcm52").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpcm52").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpcm52").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpcm61 = document.getElementById("ContentPlaceHolder1_txtpcm61").value;
                            if (txtpcm61 > 15) {
                                alert("Can not enter more than 15");
                                document.getElementById("ContentPlaceHolder1_txtpcm61").value = '';
                                document.getElementById("ContentPlaceHolder1_txtpcm61").focus();
                                document.getElementById("ContentPlaceHolder1_txtpcm61").style.borderColor = 'red';
                                return false;
                            }

                            var txtpcm62 = document.getElementById("ContentPlaceHolder1_txtpcm62").value;
                            //if (txtpcm62 > 20) {
                            //    alert("Can not enter more than 20");
                            //    document.getElementById("ContentPlaceHolder1_txtpcm62").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpcm62").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpcm62").style.borderColor = 'red';
                            //    return false;
                            //}


                            if (txtpcm11 != "" && txtpcm21 != "" && txtpcm31 != "" && txtpcm41 != "" && txtpcm51 != "" && txtpcm61 != "") {

                                var PCM11 = parseFloat(txtpcm11) + parseFloat(txtpcm21) + parseFloat(txtpcm31) + parseFloat(txtpcm41) + parseFloat(txtpcm51) + parseFloat(txtpcm61);

                                if (PCM11 > 15) {
                                    alert("Can not enter more than 15");
                                    document.getElementById("ContentPlaceHolder1_txtpcm11").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtpcm11").focus();
                                    document.getElementById("ContentPlaceHolder1_txtpcm11").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtpcm21").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtpcm21").focus();
                                    document.getElementById("ContentPlaceHolder1_txtpcm21").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtpcm31").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtpcm31").focus();
                                    document.getElementById("ContentPlaceHolder1_txtpcm31").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtpcm41").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtpcm41").focus();
                                    document.getElementById("ContentPlaceHolder1_txtpcm41").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtpcm51").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtpcm51").focus();
                                    document.getElementById("ContentPlaceHolder1_txtpcm51").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtpcm61").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtpcm61").focus();
                                    document.getElementById("ContentPlaceHolder1_txtpcm61").style.borderColor = 'red';

                                }
                            }
                            if (txtpcm12 != "" && txtpcm22 != "" && txtpcm32 != "" && txtpcm42 != "" && txtpcm52 != "" && txtpcm62 != "") {

                                var PCM12 = parseFloat(txtpcm12) + parseFloat(txtpcm22) + parseFloat(txtpcm32) + parseFloat(txtpcm42) + parseFloat(txtpcm52) + parseFloat(txtpcm62);

                                //if (PCM12 > 20) {
                                //    alert("Can not enter more than 20");
                                //    document.getElementById("ContentPlaceHolder1_txtpcm12").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpcm12").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpcm12").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpcm22").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpcm22").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpcm22").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpcm32").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpcm32").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpcm32").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpcm42").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpcm42").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpcm42").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpcm52").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpcm52").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpcm52").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpcm62").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpcm62").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpcm62").style.borderColor = 'red';

                                //}
                            }

                            var txtpiis11 = document.getElementById("ContentPlaceHolder1_txtpiis11").value;
                            if (txtpiis11 > 10) {
                                alert("Can not enter more than 10");
                                document.getElementById("ContentPlaceHolder1_txtpiis11").value = '';
                                document.getElementById("ContentPlaceHolder1_txtpiis11").focus();
                                document.getElementById("ContentPlaceHolder1_txtpiis11").style.borderColor = 'red';
                                return false;
                            }

                            var txtpiis12 = document.getElementById("ContentPlaceHolder1_txtpiis12").value;
                            //if (txtpiis12 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtpiis12").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpiis12").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpiis12").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpiis21 = document.getElementById("ContentPlaceHolder1_txtpiis21").value;
                            if (txtpiis21 > 10) {
                                alert("Can not enter more than 10");
                                document.getElementById("ContentPlaceHolder1_txtpiis21").value = '';
                                document.getElementById("ContentPlaceHolder1_txtpiis21").focus();
                                document.getElementById("ContentPlaceHolder1_txtpiis21").style.borderColor = 'red';
                                return false;
                            }


                            var txtpiis22 = document.getElementById("ContentPlaceHolder1_txtpiis22").value;
                            //if (txtpiis22 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtpiis22").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpiis22").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpiis22").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpiis31 = document.getElementById("ContentPlaceHolder1_txtpiis31").value;
                            if (txtpiis31 > 10) {
                                alert("Can not enter more than 10");
                                document.getElementById("ContentPlaceHolder1_txtpiis31").value = '';
                                document.getElementById("ContentPlaceHolder1_txtpiis31").focus();
                                document.getElementById("ContentPlaceHolder1_txtpiis31").style.borderColor = 'red';
                                return false;
                            }


                            var txtpiis32 = document.getElementById("ContentPlaceHolder1_txtpiis32").value;
                            //if (txtpiis32 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtpiis32").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpiis32").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpiis32").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpiis41 = document.getElementById("ContentPlaceHolder1_txtpiis41").value;
                            if (txtpiis41 > 10) {
                                alert("Can not enter more than 10");
                                document.getElementById("ContentPlaceHolder1_txtpiis41").value = '';
                                document.getElementById("ContentPlaceHolder1_txtpiis41").focus();
                                document.getElementById("ContentPlaceHolder1_txtpiis41").style.borderColor = 'red';
                                return false;
                            }

                            var txtpiis42 = document.getElementById("ContentPlaceHolder1_txtpiis42").value;
                            //if (txtpiis42 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtpiis42").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpiis42").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpiis42").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpiis51 = document.getElementById("ContentPlaceHolder1_txtpiis51").value;
                            if (txtpiis51 > 10) {
                                alert("Can not enter more than 10");
                                document.getElementById("ContentPlaceHolder1_txtpiis51").value = '';
                                document.getElementById("ContentPlaceHolder1_txtpiis51").focus();
                                document.getElementById("ContentPlaceHolder1_txtpiis51").style.borderColor = 'red';
                                return false;
                            }

                            var txtpiis52 = document.getElementById("ContentPlaceHolder1_txtpiis52").value;
                            //if (txtpiis52 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtpiis52").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpiis52").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpiis52").style.borderColor = 'red';
                            //    return false;
                            //}
                            var txtpiis54 = document.getElementById("ContentPlaceHolder1_txtpiis54").value;
                            if (txtpiis54 > 10) {
                                alert("Can not enter more than 10");
                                document.getElementById("ContentPlaceHolder1_txtpiis54").value = '';
                                document.getElementById("ContentPlaceHolder1_txtpiis54").focus();
                                document.getElementById("ContentPlaceHolder1_txtpiis54").style.borderColor = 'red';
                                return false;
                            }

                            var txtpiis55 = document.getElementById("ContentPlaceHolder1_txtpiis55").value;
                            //if (txtpiis55 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtpiis55").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpiis55").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpiis55").style.borderColor = 'red';
                            //    return false;
                            //}
                            var txtpiis57 = document.getElementById("ContentPlaceHolder1_txtpiis57").value;
                            if (txtpiis57 > 10) {
                                alert("Can not enter more than 10");
                                document.getElementById("ContentPlaceHolder1_txtpiis57").value = '';
                                document.getElementById("ContentPlaceHolder1_txtpiis57").focus();
                                document.getElementById("ContentPlaceHolder1_txtpiis57").style.borderColor = 'red';
                                return false;
                            }
                            var txtpiis58 = document.getElementById("ContentPlaceHolder1_txtpiis58").value;
                            //if (txtpiis58 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtpiis58").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpiis58").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpiis58").style.borderColor = 'red';
                            //    return false;
                            //}


                            if (txtpiis11 != "" && txtpiis21 != "" && txtpiis31 != "" && txtpiis41 != "" && txtpiis51 != "" && txtpiis54 != "" && txtpiis57 != "") {

                                var PPIIS11 = parseFloat(txtpiis11) + parseFloat(txtpiis21) + parseFloat(txtpiis31) + parseFloat(txtpiis41) + parseFloat(txtpiis51) + parseFloat(txtpiis54) + parseFloat(txtpiis57);

                                if (PPIIS11 > 10) {
                                    alert("Can not enter more than 10");
                                    document.getElementById("ContentPlaceHolder1_txtpiis11").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtpiis11").focus();
                                    document.getElementById("ContentPlaceHolder1_txtpiis11").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtpiis21").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtpiis21").focus();
                                    document.getElementById("ContentPlaceHolder1_txtpiis21").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtpiis31").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtpiis31").focus();
                                    document.getElementById("ContentPlaceHolder1_txtpiis31").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtpiis41").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtpiis41").focus();
                                    document.getElementById("ContentPlaceHolder1_txtpiis41").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtpiis51").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtpiis51").focus();
                                    document.getElementById("ContentPlaceHolder1_txtpiis51").style.borderColor = 'red';

                                    document.getElementById("ContentPlaceHolder1_txtpiis54").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtpiis54").focus();
                                    document.getElementById("ContentPlaceHolder1_txtpiis54").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtpiis57").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtpiis57").focus();
                                    document.getElementById("ContentPlaceHolder1_txtpiis57").style.borderColor = 'red';


                                }
                            }

                            if (txtpiis12 != "" && txtpiis22 != "" && txtpiis32 != "" && txtpiis42 != "" && txtpiis52 != "" && txtpiis55 != "" && txtpiis58 != "") {

                                var PPIIS12 = parseFloat(txtpiis12) + parseFloat(txtpiis22) + parseFloat(txtpiis32) + parseFloat(txtpiis42) + parseFloat(txtpiis52) + parseFloat(txtpiis55) + parseFloat(txtpiis58);

                                //if (PPIIS12 > 10) {
                                //    alert("Can not enter more than 10");
                                //    document.getElementById("ContentPlaceHolder1_txtpiis12").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpiis12").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpiis12").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpiis22").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpiis22").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpiis22").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpiis32").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpiis32").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpiis32").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpiis42").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpiis42").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpiis42").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpiis52").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpiis52").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpiis52").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpiis55").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpiis55").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpiis55").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpiis58").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpiis58").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpiis58").style.borderColor = 'red';
                                //}
                            }


                            var txtssc11 = document.getElementById("ContentPlaceHolder1_txtssc11").value;
                            if (txtssc11 > 10) {
                                alert("Can not enter more than 10");
                                document.getElementById("ContentPlaceHolder1_txtssc11").value = '';
                                document.getElementById("ContentPlaceHolder1_txtssc11").focus();
                                document.getElementById("ContentPlaceHolder1_txtssc11").style.borderColor = 'red';
                                return false;
                            }

                            var txtssc12 = document.getElementById("ContentPlaceHolder1_txtssc12").value;
                            //if (txtssc12 > 10) {
                            //    alert("Can not enter more than 18");
                            //    document.getElementById("ContentPlaceHolder1_txtssc12").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtssc12").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtssc12").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtssc21 = document.getElementById("ContentPlaceHolder1_txtssc21").value;
                            if (txtssc21 > 10) {
                                alert("Can not enter more than 10");
                                document.getElementById("ContentPlaceHolder1_txtssc21").value = '';
                                document.getElementById("ContentPlaceHolder1_txtssc21").focus();
                                document.getElementById("ContentPlaceHolder1_txtssc21").style.borderColor = 'red';
                                return false;
                            }

                            var txtssc22 = document.getElementById("ContentPlaceHolder1_txtssc22").value;
                            //if (txtssc22 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtssc22").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtssc22").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtssc22").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtssc31 = document.getElementById("ContentPlaceHolder1_txtssc31").value;
                            if (txtssc31 > 10) {
                                alert("Can not enter more than 10");
                                document.getElementById("ContentPlaceHolder1_txtssc31").value = '';
                                document.getElementById("ContentPlaceHolder1_txtssc31").focus();
                                document.getElementById("ContentPlaceHolder1_txtssc31").style.borderColor = 'red';
                                return false;
                            }

                            var txtssc32 = document.getElementById("ContentPlaceHolder1_txtssc32").value;
                            //if (txtssc32 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtssc32").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtssc32").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtssc32").style.borderColor = 'red';
                            //    return false;
                            //}
                            var txtssc34 = document.getElementById("ContentPlaceHolder1_txtssc34").value;
                            if (txtssc34 > 10) {
                                alert("Can not enter more than 10");
                                document.getElementById("ContentPlaceHolder1_txtssc34").value = '';
                                document.getElementById("ContentPlaceHolder1_txtssc34").focus();
                                document.getElementById("ContentPlaceHolder1_txtssc34").style.borderColor = 'red';
                                return false;
                            }
                            var txtssc35 = document.getElementById("ContentPlaceHolder1_txtssc35").value;
                            //if (txtssc35 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtssc35").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtssc35").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtssc35").style.borderColor = 'red';
                            //    return false;
                            //}
                            var txtssc37 = document.getElementById("ContentPlaceHolder1_txtssc37").value;
                            if (txtssc37 > 10) {
                                alert("Can not enter more than 10");
                                document.getElementById("ContentPlaceHolder1_txtssc37").value = '';
                                document.getElementById("ContentPlaceHolder1_txtssc37").focus();
                                document.getElementById("ContentPlaceHolder1_txtssc37").style.borderColor = 'red';
                                return false;
                            }
                            var txtssc38 = document.getElementById("ContentPlaceHolder1_txtssc38").value;
                            //if (txtssc38 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtssc38").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtssc38").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtssc38").style.borderColor = 'red';
                            //    return false;
                            //}
                            if (txtssc11 != "" && txtssc21 != "" && txtssc31 != "" && txtssc34 != "" && txtssc37 != "") {

                                var SSC11 = parseFloat(txtssc11) + parseFloat(txtssc21) + parseFloat(txtssc31) + parseFloat(txtssc34) + parseFloat(txtssc37);

                                if (SSC11 > 10) {
                                    alert("Can not enter more than 10");
                                    document.getElementById("ContentPlaceHolder1_txtssc11").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtssc11").focus();
                                    document.getElementById("ContentPlaceHolder1_txtssc11").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtssc21").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtssc21").focus();
                                    document.getElementById("ContentPlaceHolder1_txtssc21").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtssc31").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtssc31").focus();
                                    document.getElementById("ContentPlaceHolder1_txtssc31").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtssc34").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtssc34").focus();
                                    document.getElementById("ContentPlaceHolder1_txtssc34").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtssc37").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtssc37").focus();
                                    document.getElementById("ContentPlaceHolder1_txtssc37").style.borderColor = 'red';

                                }
                            }
                            if (txtssc12 != "" && txtssc22 != "" && txtssc32 != "" && txtssc35 != "" && txtssc38 != "") {

                                var SSC12 = parseFloat(txtssc12) + parseFloat(txtssc22) + parseFloat(txtssc32) + parseFloat(txtssc35) + parseFloat(txtssc38);

                                //if (SSC12 > 10) {
                                //    alert("Can not enter more than 10");
                                //    document.getElementById("ContentPlaceHolder1_txtssc12").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtssc12").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtssc12").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtssc22").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtssc22").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtssc22").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtssc32").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtssc32").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtssc32").style.borderColor = 'red';

                                //    document.getElementById("ContentPlaceHolder1_txtssc35").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtssc35").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtssc35").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtssc38").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtssc38").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtssc38").style.borderColor = 'red';

                                //}
                            }


                            var txtcsw11 = document.getElementById("ContentPlaceHolder1_txtcsw11").value;
                            if (txtcsw11 > 10) {
                                alert("Can not enter more than 10");
                                document.getElementById("ContentPlaceHolder1_txtcsw11").value = '';
                                document.getElementById("ContentPlaceHolder1_txtcsw11").focus();
                                document.getElementById("ContentPlaceHolder1_txtcsw11").style.borderColor = 'red';
                                return false;
                            }

                            var txtcsw12 = document.getElementById("ContentPlaceHolder1_txtcsw12").value;
                            //if (txtcsw12 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtcsw12").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtcsw12").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtcsw12").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtcsw21 = document.getElementById("ContentPlaceHolder1_txtcsw21").value;
                            if (txtcsw21 > 10) {
                                alert("Can not enter more than 10");
                                document.getElementById("ContentPlaceHolder1_txtcsw21").value = '';
                                document.getElementById("ContentPlaceHolder1_txtcsw21").focus();
                                document.getElementById("ContentPlaceHolder1_txtcsw21").style.borderColor = 'red';
                                return false;
                            }

                            var txtcsw22 = document.getElementById("ContentPlaceHolder1_txtcsw22").value;
                            //if (txtcsw22 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtcsw22").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtcsw22").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtcsw22").style.borderColor = 'red';
                            //    return false;
                            //}
                            var txtcsw31 = document.getElementById("ContentPlaceHolder1_txtcsw31").value;

                            if (txtcsw31 > 10) {
                                alert("Can not enter more than 10");
                                document.getElementById("ContentPlaceHolder1_txtcsw31").value = '';
                                document.getElementById("ContentPlaceHolder1_txtcsw31").focus();
                                document.getElementById("ContentPlaceHolder1_txtcsw31").style.borderColor = 'red';
                                return false;
                            }
                            var txtcsw32 = document.getElementById("ContentPlaceHolder1_txtcsw32").value;

                            //if (txtcsw32 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtcsw32").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtcsw32").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtcsw32").style.borderColor = 'red';
                            //    return false;
                            //}
                            var txtcsw41 = document.getElementById("ContentPlaceHolder1_txtcsw41").value;

                            if (txtcsw41 > 10) {
                                alert("Can not enter more than 10");
                                document.getElementById("ContentPlaceHolder1_txtcsw41").value = '';
                                document.getElementById("ContentPlaceHolder1_txtcsw41").focus();
                                document.getElementById("ContentPlaceHolder1_txtcsw41").style.borderColor = 'red';
                                return false;
                            }

                            var txtcsw42 = document.getElementById("ContentPlaceHolder1_txtcsw42").value;
                            //if (txtcsw42 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtcsw42").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtcsw42").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtcsw42").style.borderColor = 'red';
                            //    return false;
                            //}
                            var txtcsw51 = document.getElementById("ContentPlaceHolder1_txtcsw51").value;
                            if (txtcsw51 > 10) {
                                alert("Can not enter more than 10");
                                document.getElementById("ContentPlaceHolder1_txtcsw51").value = '';
                                document.getElementById("ContentPlaceHolder1_txtcsw51").focus();
                                document.getElementById("ContentPlaceHolder1_txtcsw51").style.borderColor = 'red';
                                return false;
                            }
                            var txtcsw52 = document.getElementById("ContentPlaceHolder1_txtcsw52").value;
                            //if (txtcsw52 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtcsw52").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtcsw52").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtcsw52").style.borderColor = 'red';
                            //    return false;
                            //}



                            if (txtcsw11 != "" && txtcsw11 != "" && txtcsw31 != "" && txtcsw41 != "" && txtcsw51 != "") {

                                var CSW11 = parseFloat(txtcsw11) + parseFloat(txtcsw21) + parseFloat(txtcsw31) + parseFloat(txtcsw41) + parseFloat(txtcsw51);

                                if (CSW11 > 10) {
                                    alert("Can not enter more than 10");
                                    document.getElementById("ContentPlaceHolder1_txtcsw11").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtcsw11").focus();
                                    document.getElementById("ContentPlaceHolder1_txtcsw11").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtcsw21").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtcsw21").focus();
                                    document.getElementById("ContentPlaceHolder1_txtcsw21").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtcsw31").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtcsw31").focus();
                                    document.getElementById("ContentPlaceHolder1_txtcsw31").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtcsw41").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtcsw41").focus();
                                    document.getElementById("ContentPlaceHolder1_txtcsw41").style.borderColor = 'red';
                                    document.getElementById("ContentPlaceHolder1_txtcsw51").value = '';
                                    document.getElementById("ContentPlaceHolder1_txtcsw51").focus();
                                    document.getElementById("ContentPlaceHolder1_txtcsw51").style.borderColor = 'red';

                                }
                            }

                            if (txtcsw12 != "" && txtcsw12 != "" && txtcsw32 != "" && txtcsw42 != "" && txtcsw52 != "") {

                                var CSW11 = parseFloat(txtcsw12) + parseFloat(txtcsw22) + parseFloat(txtcsw32) + parseFloat(txtcsw42) + parseFloat(txtcsw52);

                                //if (CSW11 > 10) {
                                //    alert("Can not enter more than 10");
                                //    document.getElementById("ContentPlaceHolder1_txtcsw12").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtcsw12").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtcsw12").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtcsw22").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtcsw22").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtcsw22").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtcsw32").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtcsw32").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtcsw32").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtcsw42").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtcsw42").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtcsw42").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtcsw52").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtcsw52").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtcsw52").style.borderColor = 'red';

                                //}
                            }

                        }

                        return false;
                    }










                    function onlyNumbers(event) {
                        var charCode = (event.which) ? event.which : event.keyCode
                        if (charCode > 31 && (charCode < 48 || charCode > 57))
                            return false;

                        return true;

                    }
                </script>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table style="margin-left: 100px; margin-bottom: 200px; margin-top: -70px"  >
            <tr>
                <td>
                    <asp:Label ID="lblUpload" runat="server" Text="Upload File" /></td>
                <td style="width: 40px"></td>
                <td>
                    <asp:FileUpload ID="UploadFile" BorderStyle="Solid" runat="server" Width="250px" Height="22px" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" ValidationGroup="g1" ForeColor="Red" runat="server" ControlToValidate="UploadFile" ErrorMessage="First Choose the file!"></asp:RequiredFieldValidator>

                </td>
                <td style="text-align: left; margin-right: 100px">
                    <asp:Button ID="btnUpload" Height="22px" ValidationGroup="g1" runat="server" Visible="true" Text="Upload Document" OnClick="btnUpload_Click" />

                </td>
            </tr>
            <tr>
                <td colspan="5" style="height: 10px"></td>
            </tr>
            <tr>


                <td colspan="5">
                    <asp:GridView ID="grdDocument" runat="server" AutoGenerateColumns="false" BackColor="White" BorderColor="#E7E7FF" HeaderStyle-BackColor="#ff9900" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="866px" GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                        <AlternatingRowStyle BackColor="#F7F7F7" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl. No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle Width="7%" />
                            </asp:TemplateField>
                           
                            <asp:BoundField DataField="Academic Year" HeaderText="Academic Year" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Content Type" HeaderText="Content Type" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="File Name" HeaderText="File Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                           <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton ID="lnkDownload" Text="Download" CommandArgument='<%# Eval("ID") %>'  runat="server" OnClick="DownloadInboxFile"></asp:LinkButton>
                                        </ContentTemplate>
                                       
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="lnkDownload" />
                                        <%--  <asp:PostBackTrigger ControlID="drpAcademic" />--%>
                                        </Triggers>
                                    </asp:UpdatePanel>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        
                                            <ContentTemplate>
                                            <asp:LinkButton ID="lnkdelete" Text="Delete" CommandArgument='<%# Eval("ID") %>'  runat="server" OnClick="DeleteFile"></asp:LinkButton>
                                        </ContentTemplate>
                                        <Triggers>
                                          <asp:PostBackTrigger ControlID="lnkdelete" />
                                          
                                        </Triggers>
                                    </asp:UpdatePanel>

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>







                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

