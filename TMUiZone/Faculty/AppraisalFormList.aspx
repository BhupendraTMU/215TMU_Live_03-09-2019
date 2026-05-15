<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="AppraisalFormList.aspx.cs" Inherits="Faculty_pmsapproval" %>

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server" onload="f1()">

     <div>
                 Academic Year :  <asp:DropDownList ID="drpAcademic" Width="150px" Height="20px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpAcademic_SelectedIndexChanged" ></asp:DropDownList>
                                    
                </div>

    <br />
    <div style="width: 100%; margin-bottom: 10px; border: 2px solid">
        <asp:ScriptManager ID="ty" runat="server"></asp:ScriptManager>

        <asp:UpdatePanel ID="fe" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div style="text-align: right; width: 98%; margin-bottom: 40px; margin-top: 10px; margin-left: 1%;">
                    <asp:Button ID="btnback" runat="server" Text="Back" Visible="false" OnClick="btnback_Click" />

                    <div class="text-center">
                        <asp:GridView ID="GrdExamList" runat="server" AlternatingRowStyle-CssClass="danger"
                            AllowPaging="true" PageSize="15" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" Visible="true" AllowSorting="true" OnSorting="OnSorting" OnPageIndexChanging="GrdExamList_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex +1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Faculty Code" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" SortExpression="Fid" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="Fid" runat="server" Text='<%# Bind("[Fid]") %>'></asp:Label>
                                        <asp:HiddenField ID="HfStudentNo" Value='<%# Eval("[Fid]") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" SortExpression="Full Name" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%# Bind("[Full Name]") %>'></asp:Label>
                                        <asp:HiddenField ID="hfFName" runat="server" Value='<%# Bind("[Full Name]") %>' />

                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="College" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" SortExpression="Global Dimension 1 Code" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCollege" runat="server" Text='<%# Bind("[Global Dimension 1 Code]") %>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" SortExpression="ST" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("[ST]") %>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>

                                        <asp:LinkButton ID="lblview" runat="server" Text="View" OnClick="lblview_Click" />

                                    </ItemTemplate>
                                </asp:TemplateField>


                                <%--                            <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" Checked="true" OnCheckedChanged="chkAll_CheckedChanged" />
                                    Select All
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkStudent" runat="server" AutoPostBack="true" Checked="true" OnCheckedChanged="chkStudent_CheckedChanged" />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            </Columns>
                            <EmptyDataTemplate>No Record To Display</EmptyDataTemplate>
                        </asp:GridView>
                    </div>


                </div>

                <div id="DivForm" runat="server" visible="false">
                    <asp:HiddenField ID="FF" runat="server" />

                    <div id="printarea">

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
                                    <td colspan="4" style="text-align: left; width: 90%; height: 10px;">
                                        <asp:Label ID="lblEmpName" Font-Bold="true" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: center; width: 90%;">
                                        <p style="font-family: Arial; font-size: 12px; color: black">
                                            <b>Academic Session: 
                                                <asp:Label ID="lblAcad" runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
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
                                                    <asp:TextBox ID="txtct3" Width="100%" MaxLength="3" runat="server" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
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
                                            <tr>
                                                <td colspan="4" style="width: 98%; height: 20px;"></td>
                                            </tr>
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
                                                    <asp:TextBox ID="txtsf13" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsf21" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsf22" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsf23" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>

                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsf31" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsf32" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsf33" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsf41" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsf42" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsf43" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsf51" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsf52" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsf53" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsf61" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsf62" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsf63" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
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
                                                    <asp:TextBox ID="txtsf66" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsf67" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsf68" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsf69" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>

                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsf70" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsf71" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsf72" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsf73" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsf74" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsf75" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsf76" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsf77" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsf78" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsf79" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsf80" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsf81" Width="100%" MaxLength="3" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
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
                                                    <asp:TextBox ID="txtip13" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtip21" Width="100%" Height="20px" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtip22" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtip23" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>

                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtip31" Width="100%" Height="20px" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtip32" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtip33" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
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
                                                    <asp:TextBox ID="txtNc13" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtNc21" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtNc22" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtNc23" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>

                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtNc31" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtNc32" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtNc33" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtNc34" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtNc35" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtNc36" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtNc37" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtNc38" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtNc39" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>

                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtNc40" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtNc41" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtNc42" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
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
                                                    <asp:TextBox ID="txtcsm13" onkeypress="return onlyNumbers(event)" Enabled="false" Width="100%" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtcsm21" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" MaxLength="2" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtcsm22" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" MaxLength="2" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtcsm23" onkeypress="return onlyNumbers(event)" Enabled="false" Width="100%" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
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
                                                    <asp:TextBox ID="txtpws13" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpws21" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" MaxLength="2" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpws22" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" MaxLength="2" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpws23" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>


                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpws31" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" MaxLength="2" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpws32" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" MaxLength="2" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpws33" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpws34" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" MaxLength="2" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpws35" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" MaxLength="2" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpws36" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
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
                                                    <asp:TextBox ID="txtpp13" Width="100%" onkeypress="return onlyNumbers(event)" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpp21" Enabled="false" Width="100%" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpp22" Enabled="false" Width="100%" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpp23" Width="100%" onkeypress="return onlyNumbers(event)" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>


                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpp31" Enabled="false" Width="100%" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpp32" Enabled="false" Width="100%" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpp33" Width="100%" onkeypress="return onlyNumbers(event)" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpp41" Enabled="false" Width="100%" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpp42" Enabled="false" Width="100%" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpp43" Width="100%" Enabled="false" onkeypress="return onlyNumbers(event)" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpp51" Enabled="false" Width="100%" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpp52" Enabled="false" Width="100%" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpp53" Width="100%" onkeypress="return onlyNumbers(event)" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpp61" Enabled="false" Width="100%" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpp62" Enabled="false" Width="100%" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpp63" Width="100%" onkeypress="return onlyNumbers(event)" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
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
                                                    <asp:TextBox ID="txtrs13" Width="100%" onkeypress="return onlyNumbers(event)" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtrs21" Width="100%" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtrs22" Width="100%" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtrs23" Width="100%" onkeypress="return onlyNumbers(event)" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>


                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtrs31" Width="100%" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtrs32" Width="100%" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtrs33" Width="100%" onkeypress="return onlyNumbers(event)" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtrs41" Width="100%" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtrs42" Width="100%" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtrs43" Width="100%" onkeypress="return onlyNumbers(event)" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
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
                                                    <asp:TextBox ID="txtrpp13" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtrpp21" Width="100%" Enabled="false" MaxLength="2" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtrpp22" Width="100%" Enabled="false" MaxLength="2" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtrpp23" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>


                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtrpp31" Width="100%" Enabled="false" MaxLength="2" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtrpp32" Width="100%" Enabled="false" MaxLength="2" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtrpp33" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
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
                                                    <asp:TextBox ID="txtrpro13" Width="100%" Enabled="false" MaxLength="2" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtrpro21" Width="100%" Enabled="false" MaxLength="2" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtrpro22" Width="100%" Enabled="false" MaxLength="2" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtrpro23" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>


                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtrpro31" Width="100%" Enabled="false" MaxLength="2" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtrpro32" Width="100%" Enabled="false" MaxLength="2" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtrpro33" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtrpro41" Width="100%" Enabled="false" MaxLength="2" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtrpro42" Width="100%" Enabled="false" MaxLength="2" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtrpro43" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtrpro51" Enabled="false" Width="100%" MaxLength="2" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtrpro52" Width="100%" Enabled="false" MaxLength="2" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtrpro53" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtrpro54" Width="100%" MaxLength="2" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtrpro55" Width="100%" MaxLength="2" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtrpro56" Width="100%" MaxLength="2" Enabled="false" onkeypress="return onlyNumbers(event)" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
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
                                                    <asp:TextBox ID="txtinb13" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtinb21" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtinb22" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtinb23" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>


                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtinb31" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtinb32" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtinb33" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtinb41" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtinb42" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtinb43" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtinb51" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtinb52" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtinb53" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>

                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtinb61" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtinb62" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtinb63" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtinb71" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtinb72" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtinb73" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtinb74" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtinb75" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtinb76" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>


                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtinb77" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtinb78" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtinb79" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>


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
                                                    <asp:TextBox ID="txtsd13" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsd21" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsd22" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsd23" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>


                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsd31" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsd32" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsd33" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsd41" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsd42" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsd43" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsd51" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsd52" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsd53" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>

                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsd61" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsd62" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsd63" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsd71" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsd72" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsd73" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsd74" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsd75" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsd76" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsd77" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsd78" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtsd79" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
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
                                                    <asp:TextBox ID="txtpcm13" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpcm21" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpcm22" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpcm23" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpcm31" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpcm32" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpcm33" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>

                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpcm41" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpcm42" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpcm43" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpcm51" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpcm52" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpcm53" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>

                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpcm61" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpcm62" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpcm63" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
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
                                                    <asp:TextBox ID="txtpiis13" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpiis21" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpiis22" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpiis23" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpiis31" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpiis32" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpiis33" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>

                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpiis41" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpiis42" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpiis43" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpiis51" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpiis52" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpiis53" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpiis54" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpiis55" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpiis56" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpiis57" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpiis58" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtpiis59" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
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
                                                <td>5 points for per batch mentoring (10 students per batch)</td>
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
                                                    <asp:TextBox ID="txtssc13" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtssc21" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtssc22" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtssc23" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtssc31" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtssc32" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtssc33" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtssc34" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtssc35" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtssc36" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtssc37" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtssc38" Enabled="false" onkeypress="return onlyNumbers(event)" Width="100%" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtssc39" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
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
                                                    <asp:TextBox ID="txtcsw13" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtcsw21" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtcsw22" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtcsw23" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtcsw31" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtcsw32" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtcsw33" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>

                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtcsw41" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtcsw42" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtcsw43" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtcsw51" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtcsw52" Enabled="false" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" Width="100%" MaxLength="2" runat="server"></asp:TextBox></td>
                                                <td style="border: 1px solid; height: 15px;">
                                                    <asp:TextBox ID="txtcsw53" onkeypress="return onlyNumbers(event)" Width="100%" Enabled="false" MaxLength="2" runat="server" onchange="return funChkEnterAndMaxMarksNested(this,this.value);" OnTextChanged="txtcsw53_TextChanged" AutoPostBack="true"></asp:TextBox></td>
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
                                                    <asp:TextBox ID="txtp3j" Width="100%" onkeypress="return onlyNumbers(event)" onchange="return funChkEnterAndMaxMarkspart3(this,this.value);" MaxLength="2" Enabled="false" runat="server"></asp:TextBox></td>

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



                        <asp:Button ID="btnApproved" runat="server" Text="Approved" OnClick="btnApproved_Click" />
                        <asp:Button ID="btnReject" runat="server" Text="Reject" OnClick="btnReject_Click" />
                        <asp:Button ID="btntest" OnClientClick="PrintDiv();" runat="server" Visible="true" Text="Print" />
                        <asp:Button ID="btnSave" runat="server" Text="Save"  OnClick="btnSave_Click" />
                    </div>

                </div>
                <table style="margin-left: 100px; margin-bottom: 200px; margin-top: -70px">


                    <tr>


                        <td>
                            <asp:GridView ID="grdDocument" runat="server" AutoGenerateColumns="false" BackColor="White" BorderColor="#E7E7FF" HeaderStyle-BackColor="#ff9900" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="866px" GridLines="Horizontal" >
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
                                                    <asp:LinkButton ID="lnkDownload" Text="Download" CommandArgument='<%# Eval("ID") %>' runat="server" OnClick="DownloadInboxFile"></asp:LinkButton>
                                                </ContentTemplate>

                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkDownload" />

                                                </Triggers>
                                            </asp:UpdatePanel>

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>







                            </asp:GridView>
                        </td>
                    </tr>
                </table>


                <script type="text/javascript">





                    function funChkEnterAndMaxMarksNested(thisId, thisvalue) {
                        var vtxtmark = thisId.id;




                        if (thisvalue !== "") {


                            var txtct3 = document.getElementById("ContentPlaceHolder1_txtct3").value;

                            //if (txtct3 > 120) {
                            //    alert("Can not enter more than 120");
                            //    document.getElementById("ContentPlaceHolder1_txtct3").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtct3").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtct3").style.borderColor = 'red';
                            //    return false;
                            //}




                            if (txtct3 != "") {
                                var SF13 = parseFloat(txtct3);
                                //    if (SF13 > 120) {
                                //        alert("Can not enter more than 120");
                                //        document.getElementById("ContentPlaceHolder1_txtct3").value = '';
                                //        document.getElementById("ContentPlaceHolder1_txtct3").focus();
                                //        document.getElementById("ContentPlaceHolder1_txtct3").style.borderColor = 'red';

                                //    }
                            }


                            var txtip13 = document.getElementById("ContentPlaceHolder1_txtip13").value;
                            //if (txtip13 > 16) {
                            //    alert("Can not enter more than 16");
                            //    document.getElementById("ContentPlaceHolder1_txtip13").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtip13").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtip13").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtip23 = document.getElementById("ContentPlaceHolder1_txtip23").value;
                            //if (txtip23 > 16) {
                            //    alert("Can not enter more than 16");
                            //    document.getElementById("ContentPlaceHolder1_txtip23").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtip23").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtip23").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtip33 = document.getElementById("ContentPlaceHolder1_txtip33").value;
                            //if (txtip33 > 16) {
                            //    alert("Can not enter more than 16");
                            //    document.getElementById("ContentPlaceHolder1_txtip33").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtip33").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtip33").style.borderColor = 'red';
                            //    return false;
                            //}
                            if (txtip13 != "" && txtip23 != "" && txtip33 != "") {
                                var IP13 = parseFloat(txtip13) + parseFloat(txtip23) + parseFloat(txtip33);



                                //if (IP13 > 16) {
                                //    alert("Can not enter more than 16");
                                //    document.getElementById("ContentPlaceHolder1_txtip13").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtip13").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtip13").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtip23").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtip23").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtip23").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtip33").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtip33").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtip33").style.borderColor = 'red';

                                //}
                            }
                            //NC

                            var txtNc13 = document.getElementById("ContentPlaceHolder1_txtNc13").value;
                            //if (txtNc13 > 5) {
                            //    alert("Can not enter more than 5");
                            //    document.getElementById("ContentPlaceHolder1_txtNc13").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtNc13").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtNc13").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtNc23 = document.getElementById("ContentPlaceHolder1_txtNc23").value;
                            //if (txtNc23 > 5) {
                            //    alert("Can not enter more than 5");
                            //    document.getElementById("ContentPlaceHolder1_txtNc23").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtNc23").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtNc23").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtNc33 = document.getElementById("ContentPlaceHolder1_txtNc33").value;
                            //if (txtNc33 > 5) {
                            //    alert("Can not enter more than 5");
                            //    document.getElementById("ContentPlaceHolder1_txtNc33").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtNc33").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtNc33").style.borderColor = 'red';
                            //    return false;
                            //}
                            var txtNc36 = document.getElementById("ContentPlaceHolder1_txtNc36").value;
                            //if (txtNc36 > 5) {
                            //    alert("Can not enter more than 5");
                            //    document.getElementById("ContentPlaceHolder1_txtNc36").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtNc36").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtNc36").style.borderColor = 'red';
                            //    return false;
                            //}
                            var txtNc39 = document.getElementById("ContentPlaceHolder1_txtNc39").value;
                            //if (txtNc39 > 5) {
                            //    alert("Can not enter more than 5");
                            //    document.getElementById("ContentPlaceHolder1_txtNc39").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtNc39").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtNc39").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtNc42 = document.getElementById("ContentPlaceHolder1_txtNc42").value;
                            //if (txtNc42 > 5) {
                            //    alert("Can not enter more than 5");
                            //    document.getElementById("ContentPlaceHolder1_txtNc42").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtNc42").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtNc42").style.borderColor = 'red';
                            //    return false;
                            //}


                            if (txtNc13 != "" && txtNc23 != "" && txtNc33 != "" && txtNc36 != "" && txtNc39 != "" && txtNc42 != "") {

                                var NC11 = parseFloat(txtNc13) + parseFloat(txtNc23) + parseFloat(txtNc33) + parseFloat(txtNc36) + parseFloat(txtNc39) + parseFloat(txtNc42);

                                //if (NC11 > 5) {
                                //    alert("Can not enter more than 5");
                                //    document.getElementById("ContentPlaceHolder1_txtNc13").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtNc13").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtNc13").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtNc23").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtNc23").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtNc23").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtNc33").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtNc33").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtNc33").style.borderColor = 'red';

                                //    document.getElementById("ContentPlaceHolder1_txtNc36").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtNc36").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtNc36").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtNc39").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtNc39").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtNc39").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtNc42").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtNc42").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtNc42").style.borderColor = 'red';


                                //}
                            }

                            //CAM


                            var txtcsm13 = document.getElementById("ContentPlaceHolder1_txtcsm13").value;
                            //if (txtcsm13 > 3) {
                            //    alert("Can not enter more than 3");
                            //    document.getElementById("ContentPlaceHolder1_txtcsm13").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtcsm13").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtcsm13").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtcsm23 = document.getElementById("ContentPlaceHolder1_txtcsm23").value;
                            //if (txtcsm23 > 3) {
                            //    alert("Can not enter more than 3");
                            //    document.getElementById("ContentPlaceHolder1_txtcsm23").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtcsm23").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtcsm23").style.borderColor = 'red';
                            //    return false;
                            //}

                            if (txtcsm13 != "" && txtcsm23 != "") {

                                var CSM13 = parseFloat(txtcsm13) + parseFloat(txtcsm23);

                                //if (CSM13 > 3) {
                                //    alert("Can not enter more than 3");
                                //    document.getElementById("ContentPlaceHolder1_txtcsm13").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtcsm13").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtcsm13").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtcsm23").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtcsm23").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtcsm23").style.borderColor = 'red';

                                //}
                            }
                            //

                            var txtpws13 = document.getElementById("ContentPlaceHolder1_txtpws13").value;
                            //if (txtpws13 > 16) {
                            //    alert("Can not enter more than 18");
                            //    document.getElementById("ContentPlaceHolder1_txtpws13").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpws13").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpws13").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpws23 = document.getElementById("ContentPlaceHolder1_txtpws23").value;
                            //if (txtpws23 > 16) {
                            //    alert("Can not enter more than 18");
                            //    document.getElementById("ContentPlaceHolder1_txtpws23").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpws23").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpws23").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpws33 = document.getElementById("ContentPlaceHolder1_txtpws33").value;
                            //if (txtpws33 > 16) {
                            //    alert("Can not enter more than 18");
                            //    document.getElementById("ContentPlaceHolder1_txtpws33").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpws33").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpws33").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpws36 = document.getElementById("ContentPlaceHolder1_txtpws36").value;
                            //if (txtpws36 > 16) {
                            //    alert("Can not enter more than 18");
                            //    document.getElementById("ContentPlaceHolder1_txtpws36").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpws36").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpws36").style.borderColor = 'red';
                            //    return false;
                            //}
                            if (txtpws13 != "" && txtpws23 != "" && txtpws33 != "" && txtpws36 != "") {

                                var PWS11 = parseFloat(txtpws13) + parseFloat(txtpws23) + parseFloat(txtpws33) + parseFloat(txtpws36);

                                //if (PWS11 > 16) {
                                //    alert("Can not enter more than 16");
                                //    document.getElementById("ContentPlaceHolder1_txtpws13").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpws13").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpws13").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpws23").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpws23").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpws23").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpws33").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpws33").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpws33").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpws36").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpws36").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpws36").style.borderColor = 'red';
                                //}

                            }
                            //PP


                            var txtpp13 = document.getElementById("ContentPlaceHolder1_txtpp13").value;
                            //if (txtpp13 > 30) {
                            //    alert("Can not enter more than 30");
                            //    document.getElementById("ContentPlaceHolder1_txtpp13").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpp13").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpp13").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpp23 = document.getElementById("ContentPlaceHolder1_txtpp23").value;
                            //if (txtpp23 > 30) {
                            //    alert("Can not enter more than 30");
                            //    document.getElementById("ContentPlaceHolder1_txtpp23").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpp23").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpp23").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpp33 = document.getElementById("ContentPlaceHolder1_txtpp33").value;
                            //if (txtpp33 > 30) {
                            //    alert("Can not enter more than 30");
                            //    document.getElementById("ContentPlaceHolder1_txtpp33").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpp33").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpp33").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpp43 = document.getElementById("ContentPlaceHolder1_txtpp43").value;
                            //if (txtpp43 > 30) {
                            //    alert("Can not enter more than 30");
                            //    document.getElementById("ContentPlaceHolder1_txtpp43").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpp43").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpp43").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpp53 = document.getElementById("ContentPlaceHolder1_txtpp53").value;
                            //if (txtpp53 > 30) {
                            //    alert("Can not enter more than 30");
                            //    document.getElementById("ContentPlaceHolder1_txtpp53").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpp53").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpp53").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpp63 = document.getElementById("ContentPlaceHolder1_txtpp63").value;
                            //if (txtpp63 > 30) {
                            //    alert("Can not enter more than 30");
                            //    document.getElementById("ContentPlaceHolder1_txtpp63").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpp63").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpp63").style.borderColor = 'red';
                            //    return false;
                            //}

                            if (txtpp13 != "" && txtpp23 != "" && txtpp33 != "" && txtpp43 != "" && txtpp53 != "" && txtpp63 != "") {

                                var PP11 = parseFloat(txtpp13) + parseFloat(txtpp23) + parseFloat(txtpp33) + parseFloat(txtpp43) + parseFloat(txtpp53) + parseFloat(txtpp63);

                                //if (PP11 > 55) {
                                //    alert("Can not enter more than 55");
                                //    document.getElementById("ContentPlaceHolder1_txtpp13").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpp13").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpp13").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpp23").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpp23").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpp23").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpp33").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpp33").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpp33").style.borderColor = 'red';

                                //    document.getElementById("ContentPlaceHolder1_txtpp43").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpp43").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpp43").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpp53").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpp53").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpp53").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpp63").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpp63").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpp63").style.borderColor = 'red';
                                //}
                            }
                            //

                            var txtrs13 = document.getElementById("ContentPlaceHolder1_txtrs13").value;
                            //if (txtrs13 > 5) {
                            //    alert("Can not enter more than 5");
                            //    document.getElementById("ContentPlaceHolder1_txtrs13").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtrs13").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtrs13").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtrs23 = document.getElementById("ContentPlaceHolder1_txtrs23").value;
                            //if (txtrs23 > 5) {
                            //    alert("Can not enter more than 5");
                            //    document.getElementById("ContentPlaceHolder1_txtrs23").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtrs23").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtrs23").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtrs33 = document.getElementById("ContentPlaceHolder1_txtrs33").value;
                            //if (txtrs33 > 5) {
                            //    alert("Can not enter more than 5");
                            //    document.getElementById("ContentPlaceHolder1_txtrs33").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtrs33").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtrs33").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtrs43 = document.getElementById("ContentPlaceHolder1_txtrs43").value;
                            //if (txtrs43 > 5) {
                            //    alert("Can not enter more than 5");
                            //    document.getElementById("ContentPlaceHolder1_txtrs43").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtrs43").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtrs43").style.borderColor = 'red';
                            //    return false;
                            //}

                            if (txtrs13 != "" && txtrs23 != "" && txtrs33 != "" && txtrs43 != "") {

                                var RS11 = parseFloat(txtrs13) + parseFloat(txtrs23) + parseFloat(txtrs33) + parseFloat(txtrs43);

                                //if (RS11 > 5) {
                                //    alert("Can not enter more than 5");
                                //    document.getElementById("ContentPlaceHolder1_txtrs13").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtrs13").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtrs13").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtrs23").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtrs23").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtrs23").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtrs33").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtrs33").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtrs33").style.borderColor = 'red';

                                //    document.getElementById("ContentPlaceHolder1_txtrs43").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtrs43").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtrs43").style.borderColor = 'red';

                                //}
                            }
                            //

                            var txtrpp13 = document.getElementById("ContentPlaceHolder1_txtrpp13").value;
                            //if (txtrpp13 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtrpp13").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtrpp13").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtrpp13").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtrpp23 = document.getElementById("ContentPlaceHolder1_txtrpp23").value;
                            //if (txtrpp23 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtrpp23").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtrpp23").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtrpp23").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtrpp33 = document.getElementById("ContentPlaceHolder1_txtrpp33").value;
                            //if (txtrpp33 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtrpp33").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtrpp33").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtrpp33").style.borderColor = 'red';
                            //    return false;
                            //}
                            if (txtrpp13 != "" && txtrpp23 != "" && txtrpp33 != "") {

                                var RPP11 = parseFloat(txtrpp13) + parseFloat(txtrpp23) + parseFloat(txtrpp33);

                                //if (RPP11 > 10) {
                                //    alert("Can not enter more than 10");
                                //    document.getElementById("ContentPlaceHolder1_txtrpp13").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtrpp13").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtrpp13").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtrpp23").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtrpp23").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtrpp23").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtrpp33").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtrpp33").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtrpp33").style.borderColor = 'red';


                                //}
                            }
                            //

                            var txtrpro13 = document.getElementById("ContentPlaceHolder1_txtrpro13").value;
                            //if (txtrpro13 > 15) {
                            //    alert("Can not enter more than 15");
                            //    document.getElementById("ContentPlaceHolder1_txtrpro13").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtrpro13").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtrpro13").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtrpro23 = document.getElementById("ContentPlaceHolder1_txtrpro23").value;
                            //if (txtrpro23 > 15) {
                            //    alert("Can not enter more than 15");
                            //    document.getElementById("ContentPlaceHolder1_txtrpro23").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtrpro23").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtrpro23").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtrpro33 = document.getElementById("ContentPlaceHolder1_txtrpro33").value;
                            //if (txtrpro33 > 15) {
                            //    alert("Can not enter more than 15");
                            //    document.getElementById("ContentPlaceHolder1_txtrpro33").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtrpro33").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtrpro33").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtrpro43 = document.getElementById("ContentPlaceHolder1_txtrpro43").value;
                            //if (txtrpro43 > 15) {
                            //    alert("Can not enter more than 15");
                            //    document.getElementById("ContentPlaceHolder1_txtrpro43").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtrpro43").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtrpro43").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtrpro53 = document.getElementById("ContentPlaceHolder1_txtrpro53").value;
                            //if (txtrpro53 > 15) {
                            //    alert("Can not enter more than 15");
                            //    document.getElementById("ContentPlaceHolder1_txtrpro53").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtrpro53").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtrpro53").style.borderColor = 'red';
                            //    return false;
                            //}
                            var txtrpro56 = document.getElementById("ContentPlaceHolder1_txtrpro56").value;
                            //if (txtrpro56 > 15) {
                            //    alert("Can not enter more than 15");
                            //    document.getElementById("ContentPlaceHolder1_txtrpro56").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtrpro56").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtrpro56").style.borderColor = 'red';
                            //    return false;
                            //}

                            if (txtrpro13 != "" && txtrpro23 != "" && txtrpro33 != "" && txtrpro43 != "" && txtrpro53 != "" && txtrpro56 != "") {

                                var RPPF11 = parseFloat(txtrpro13) + parseFloat(txtrpro23) + parseFloat(txtrpro33) + parseFloat(txtrpro43) + parseFloat(txtrpro53) + parseFloat(txtrpro56);

                                //if (RPPF11 > 15) {
                                //    alert("Can not enter more than 15");
                                //    document.getElementById("ContentPlaceHolder1_txtrpro13").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtrpro13").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtrpro13").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtrpro23").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtrpro23").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtrpro23").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtrpro33").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtrpro33").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtrpro33").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtrpro43").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtrpro43").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtrpro43").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtrpro53").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtrpro53").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtrpro53").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtrpro56").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtrpro56").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtrpro56").style.borderColor = 'red';
                                //}
                            }
                            //INB

                            var txtinb13 = document.getElementById("ContentPlaceHolder1_txtinb13").value;
                            //if (txtinb13 > 20) {
                            //    alert("Can not enter more than 20");
                            //    document.getElementById("ContentPlaceHolder1_txtinb13").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtinb13").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtinb13").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtinb23 = document.getElementById("ContentPlaceHolder1_txtinb23").value;
                            //if (txtinb23 > 20) {
                            //    alert("Can not enter more than 20");
                            //    document.getElementById("ContentPlaceHolder1_txtinb23").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtinb23").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtinb23").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtinb33 = document.getElementById("ContentPlaceHolder1_txtinb33").value;
                            //if (txtinb33 > 20) {
                            //    alert("Can not enter more than 20");
                            //    document.getElementById("ContentPlaceHolder1_txtinb33").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtinb33").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtinb33").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtinb43 = document.getElementById("ContentPlaceHolder1_txtinb43").value;
                            //if (txtinb43 > 20) {
                            //    alert("Can not enter more than 20");
                            //    document.getElementById("ContentPlaceHolder1_txtinb43").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtinb43").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtinb43").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtinb53 = document.getElementById("ContentPlaceHolder1_txtinb53").value;
                            //if (txtinb53 > 20) {
                            //    alert("Can not enter more than 20");
                            //    document.getElementById("ContentPlaceHolder1_txtinb53").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtinb53").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtinb53").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtinb63 = document.getElementById("ContentPlaceHolder1_txtinb63").value;
                            //if (txtinb63 > 20) {
                            //    alert("Can not enter more than 20");
                            //    document.getElementById("ContentPlaceHolder1_txtinb63").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtinb63").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtinb63").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtinb73 = document.getElementById("ContentPlaceHolder1_txtinb73").value;
                            //if (txtinb73 > 20) {
                            //    alert("Can not enter more than 20");
                            //    document.getElementById("ContentPlaceHolder1_txtinb73").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtinb73").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtinb73").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtinb76 = document.getElementById("ContentPlaceHolder1_txtinb76").value;
                            //if (txtinb76 > 20) {
                            //    alert("Can not enter more than 20");
                            //    document.getElementById("ContentPlaceHolder1_txtinb76").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtinb76").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtinb76").style.borderColor = 'red';
                            //    return false;
                            //}
                            var txtinb79 = document.getElementById("ContentPlaceHolder1_txtinb79").value;
                            //if (txtinb79 > 20) {
                            //    alert("Can not enter more than 20");
                            //    document.getElementById("ContentPlaceHolder1_txtinb79").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtinb79").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtinb79").style.borderColor = 'red';
                            //    return false;
                            //}
                            if (txtinb13 != "" && txtinb23 != "" && txtinb33 != "" && txtinb43 != "" && txtinb53 != "" && txtinb63 != "" && txtinb73 != "" && txtinb76 != "" && txtinb79 != "") {

                                var INB11 = parseFloat(txtinb13) + parseFloat(txtinb23) + parseFloat(txtinb33) + parseFloat(txtinb43) + parseFloat(txtinb53) + parseFloat(txtinb63) + parseFloat(txtinb73) + parseFloat(txtinb76) + parseFloat(txtinb79);

                                //if (INB11 > 20) {
                                //    alert("Can not enter more than 20");
                                //    document.getElementById("ContentPlaceHolder1_txtinb13").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtinb13").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtinb13").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtinb23").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtinb23").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtinb23").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtinb33").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtinb33").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtinb33").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtinb43").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtinb43").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtinb43").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtinb53").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtinb53").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtinb53").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtinb63").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtinb63").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtinb63").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtinb73").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtinb73").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtinb73").style.borderColor = 'red';


                                //    document.getElementById("ContentPlaceHolder1_txtinb76").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtinb76").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtinb76").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtinb79").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtinb79").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtinb79").style.borderColor = 'red';


                                //}
                            }
                            //SD

                            var txtsd13 = document.getElementById("ContentPlaceHolder1_txtsd13").value;
                            //if (txtsd13 > 15) {
                            //    alert("Can not enter more than 15");
                            //    document.getElementById("ContentPlaceHolder1_txtsd13").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtsd13").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtsd13").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtsd23 = document.getElementById("ContentPlaceHolder1_txtsd23").value;
                            //if (txtsd23 > 15) {
                            //    alert("Can not enter more than 15");
                            //    document.getElementById("ContentPlaceHolder1_txtsd23").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtsd23").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtsd23").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtsd33 = document.getElementById("ContentPlaceHolder1_txtsd33").value;
                            //if (txtsd33 > 15) {
                            //    alert("Can not enter more than 15");
                            //    document.getElementById("ContentPlaceHolder1_txtsd33").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtsd33").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtsd33").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtsd43 = document.getElementById("ContentPlaceHolder1_txtsd43").value;
                            //if (txtsd43 > 15) {
                            //    alert("Can not enter more than 15");
                            //    document.getElementById("ContentPlaceHolder1_txtsd43").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtsd43").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtsd43").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtsd53 = document.getElementById("ContentPlaceHolder1_txtsd53").value;
                            //if (txtsd53 > 15) {
                            //    alert("Can not enter more than 15");
                            //    document.getElementById("ContentPlaceHolder1_txtsd53").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtsd53").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtsd53").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtsd63 = document.getElementById("ContentPlaceHolder1_txtsd63").value;
                            //if (txtsd63 > 15) {
                            //    alert("Can not enter more than 15");
                            //    document.getElementById("ContentPlaceHolder1_txtsd63").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtsd63").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtsd63").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtsd73 = document.getElementById("ContentPlaceHolder1_txtsd73").value;
                            //if (txtsd73 > 15) {
                            //    alert("Can not enter more than 15");
                            //    document.getElementById("ContentPlaceHolder1_txtsd73").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtsd73").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtsd73").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtsd76 = document.getElementById("ContentPlaceHolder1_txtsd76").value;
                            //if (txtsd76 > 15) {
                            //    alert("Can not enter more than 15");
                            //    document.getElementById("ContentPlaceHolder1_txtsd76").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtsd76").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtsd76").style.borderColor = 'red';
                            //    return false;
                            //}
                            var txtsd79 = document.getElementById("ContentPlaceHolder1_txtsd79").value;
                            //if (txtsd79 > 15) {
                            //    alert("Can not enter more than 15");
                            //    document.getElementById("ContentPlaceHolder1_txtsd79").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtsd79").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtsd79").style.borderColor = 'red';
                            //    return false;
                            //}
                            if (txtsd13 != "" && txtsd23 != "" && txtsd33 != "" && txtsd43 != "" && txtsd53 != "" && txtsd63 != "" && txtsd73 != "" && txtsd76 != "" && txtsd79 != "") {

                                var SD11 = parseFloat(txtsd13) + parseFloat(txtsd23) + parseFloat(txtsd33) + parseFloat(txtsd43) + parseFloat(txtsd53) + parseFloat(txtsd63) + parseFloat(txtsd73) + parseFloat(txtsd76) + parseFloat(txtsd79);

                                //if (SD11 > 15) {
                                //    alert("Can not enter more than 15");
                                //    document.getElementById("ContentPlaceHolder1_txtsd13").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtsd13").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtsd13").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtsd23").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtsd23").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtsd23").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtsd33").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtsd33").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtsd33").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtsd43").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtsd43").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtsd43").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtsd53").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtsd53").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtsd53").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtsd63").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtsd63").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtsd63").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtsd73").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtsd73").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtsd73").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtsd76").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtsd76").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtsd76").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtsd79").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtsd79").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtsd79").style.borderColor = 'red';
                                //}
                            }
                            //

                            var txtpcm13 = document.getElementById("ContentPlaceHolder1_txtpcm13").value;
                            //if (txtpcm13 > 20) {
                            //    alert("Can not enter more than 20");
                            //    document.getElementById("ContentPlaceHolder1_txtpcm13").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpcm13").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpcm13").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpcm23 = document.getElementById("ContentPlaceHolder1_txtpcm23").value;
                            //if (txtpcm23 > 20) {
                            //    alert("Can not enter more than 20");
                            //    document.getElementById("ContentPlaceHolder1_txtpcm23").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpcm23").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpcm23").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpcm33 = document.getElementById("ContentPlaceHolder1_txtpcm33").value;
                            //if (txtpcm33 > 20) {
                            //    alert("Can not enter more than 20");
                            //    document.getElementById("ContentPlaceHolder1_txtpcm33").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpcm33").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpcm33").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpcm43 = document.getElementById("ContentPlaceHolder1_txtpcm43").value;
                            //if (txtpcm43 > 20) {
                            //    alert("Can not enter more than 20");
                            //    document.getElementById("ContentPlaceHolder1_txtpcm43").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpcm43").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpcm43").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpcm53 = document.getElementById("ContentPlaceHolder1_txtpcm53").value;
                            //if (txtpcm53 > 20) {
                            //    alert("Can not enter more than 20");
                            //    document.getElementById("ContentPlaceHolder1_txtpcm53").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpcm53").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpcm53").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpcm63 = document.getElementById("ContentPlaceHolder1_txtpcm63").value;
                            //if (txtpcm63 > 20) {
                            //    alert("Can not enter more than 20");
                            //    document.getElementById("ContentPlaceHolder1_txtpcm63").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpcm63").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpcm63").style.borderColor = 'red';
                            //    return false;
                            //}
                            if (txtpcm13 != "" && txtpcm23 != "" && txtpcm33 != "" && txtpcm43 != "" && txtpcm53 != "" && txtpcm63 != "") {

                                var PCM11 = parseFloat(txtpcm13) + parseFloat(txtpcm23) + parseFloat(txtpcm33) + parseFloat(txtpcm43) + parseFloat(txtpcm53) + parseFloat(txtpcm63);

                                //if (PCM11 > 20) {
                                //    alert("Can not enter more than 20");
                                //    document.getElementById("ContentPlaceHolder1_txtpcm13").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpcm13").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpcm13").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpcm23").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpcm23").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpcm23").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpcm33").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpcm33").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpcm33").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpcm43").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpcm43").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpcm43").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpcm53").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpcm53").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpcm53").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpcm63").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpcm63").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpcm63").style.borderColor = 'red';

                                //}
                            }
                            //IIS

                            var txtpiis13 = document.getElementById("ContentPlaceHolder1_txtpiis13").value;
                            //if (txtpiis13 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtpiis13").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpiis13").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpiis13").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpiis23 = document.getElementById("ContentPlaceHolder1_txtpiis23").value;
                            //if (txtpiis23 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtpiis23").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpiis23").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpiis23").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpiis33 = document.getElementById("ContentPlaceHolder1_txtpiis33").value;
                            //if (txtpiis33 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtpiis33").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpiis33").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpiis33").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpiis43 = document.getElementById("ContentPlaceHolder1_txtpiis43").value;
                            //if (txtpiis43 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtpiis43").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpiis43").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpiis43").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpiis53 = document.getElementById("ContentPlaceHolder1_txtpiis53").value;
                            //if (txtpiis53 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtpiis53").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpiis53").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpiis53").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtpiis56 = document.getElementById("ContentPlaceHolder1_txtpiis56").value;
                            //if (txtpiis56 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtpiis56").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpiis56").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpiis56").style.borderColor = 'red';
                            //    return false;
                            //}
                            var txtpiis59 = document.getElementById("ContentPlaceHolder1_txtpiis59").value;
                            //if (txtpiis59 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtpiis59").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtpiis59").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtpiis59").style.borderColor = 'red';
                            //    return false;
                            //}

                            if (txtpiis13 != "" && txtpiis23 != "" && txtpiis33 != "" && txtpiis43 != "" && txtpiis53 != "" && txtpiis56 != "" && txtpiis59 != "") {

                                var PPIIS11 = parseFloat(txtpiis13) + parseFloat(txtpiis23) + parseFloat(txtpiis33) + parseFloat(txtpiis43) + parseFloat(txtpiis53) + parseFloat(txtpiis56) + parseFloat(txtpiis59);

                                //if (PPIIS11 > 10) {
                                //    alert("Can not enter more than 10");
                                //    document.getElementById("ContentPlaceHolder1_txtpiis13").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpiis13").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpiis13").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpiis23").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpiis23").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpiis23").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpiis33").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpiis33").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpiis33").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpiis43").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpiis43").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpiis43").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpiis53").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpiis53").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpiis53").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpiis56").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpiis56").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpiis56").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtpiis59").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtpiis59").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtpiis59").style.borderColor = 'red';
                                //}
                            }
                            //SSC

                            var txtssc13 = document.getElementById("ContentPlaceHolder1_txtssc13").value;
                            //if (txtssc13 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtssc13").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtssc13").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtssc13").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtssc23 = document.getElementById("ContentPlaceHolder1_txtssc23").value;
                            //if (txtssc23 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtssc23").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtssc23").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtssc23").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtssc33 = document.getElementById("ContentPlaceHolder1_txtssc33").value;
                            //if (txtssc33 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtssc33").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtssc33").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtssc33").style.borderColor = 'red';
                            //    return false;
                            //}
                            var txtssc36 = document.getElementById("ContentPlaceHolder1_txtssc36").value;
                            //if (txtssc36 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtssc36").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtssc36").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtssc36").style.borderColor = 'red';
                            //    return false;
                            //}
                            var txtssc39 = document.getElementById("ContentPlaceHolder1_txtssc39").value;
                            //if (txtssc39 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtssc39").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtssc39").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtssc39").style.borderColor = 'red';
                            //    return false;
                            //}
                            if (txtssc13 != "" && txtssc23 != "" && txtssc33 != "" && txtssc36 != "" && txtssc39 != "") {

                                var SSC11 = parseFloat(txtssc13) + parseFloat(txtssc23) + parseFloat(txtssc33) + parseFloat(txtssc36) + parseFloat(txtssc39);

                                //if (SSC11 > 10) {
                                //    alert("Can not enter more than 10");
                                //    document.getElementById("ContentPlaceHolder1_txtssc13").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtssc13").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtssc13").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtssc23").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtssc23").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtssc23").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtssc33").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtssc33").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtssc33").style.borderColor = 'red';

                                //    document.getElementById("ContentPlaceHolder1_txtssc36").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtssc36").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtssc36").style.borderColor = 'red';

                                //    document.getElementById("ContentPlaceHolder1_txtssc39").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtssc39").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtssc39").style.borderColor = 'red';

                                //}
                            }
                            //CWS

                            var txtcsw13 = document.getElementById("ContentPlaceHolder1_txtcsw13").value;
                            //if (txtcsw13 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtcsw13").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtcsw13").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtcsw13").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtcsw23 = document.getElementById("ContentPlaceHolder1_txtcsw23").value;
                            //if (txtcsw23 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtcsw23").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtcsw23").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtcsw23").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtcsw33 = document.getElementById("ContentPlaceHolder1_txtcsw33").value;
                            //if (txtcsw33 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtcsw33").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtcsw33").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtcsw33").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtcsw43 = document.getElementById("ContentPlaceHolder1_txtcsw43").value;
                            //if (txtcsw43 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtcsw43").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtcsw43").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtcsw43").style.borderColor = 'red';
                            //    return false;
                            //}

                            var txtcsw53 = document.getElementById("ContentPlaceHolder1_txtcsw53").value;
                            //if (txtcsw53 > 10) {
                            //    alert("Can not enter more than 10");
                            //    document.getElementById("ContentPlaceHolder1_txtcsw53").value = '';
                            //    document.getElementById("ContentPlaceHolder1_txtcsw53").focus();
                            //    document.getElementById("ContentPlaceHolder1_txtcsw53").style.borderColor = 'red';
                            //    return false;
                            //}

                            if (txtcsw13 != "" && txtcsw13 != "" && txtcsw33 != "" && txtcsw43 != "" && txtcsw53 != "") {

                                var CSW11 = parseFloat(txtcsw13) + parseFloat(txtcsw23) + parseFloat(txtcsw33) + parseFloat(txtcsw43) + parseFloat(txtcsw53);

                                //if (CSW11 > 10) {

                                //    alert("Can not enter more than 10");
                                //    document.getElementById("ContentPlaceHolder1_txtcsw13").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtcsw13").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtcsw13").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtcsw23").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtcsw23").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtcsw23").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtcsw33").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtcsw33").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtcsw33").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtcsw43").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtcsw43").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtcsw43").style.borderColor = 'red';
                                //    document.getElementById("ContentPlaceHolder1_txtcsw53").value = '';
                                //    document.getElementById("ContentPlaceHolder1_txtcsw53").focus();
                                //    document.getElementById("ContentPlaceHolder1_txtcsw53").style.borderColor = 'red';
                                //    return false;

                                //}

                            }
                            if (SF13 == '' || SF13 == null) {
                                SF13 = 0;
                            }
                            if (IP13 == '' || IP13 == null) {
                                IP13 = 0;
                            }
                            if (NC11 == '' || NC11 == null) {
                                NC11 = 0;
                            }
                            if (CSM13 == '' || CSM13 == null) {
                                CSM13 = 0;
                            }
                            if (PWS11 == '' || PWS11 == null) {
                                PWS11 = 0;
                            }

                            if (SF13 + IP13 + NC11 + CSM13 + PWS11 > 160) {
                                document.getElementById("ContentPlaceHolder1_txtpwA13").value = 160;
                            }
                            else {

                                document.getElementById("ContentPlaceHolder1_txtpwA13").value = SF13 + IP13 + NC11 + CSM13 + PWS11;
                            }

                            if (PP11 == '' || PP11 == null) {
                                PP11 = 0;
                            }
                            if (RS11 == '' || RS11 == null) {
                                RS11 = 0;
                            }
                            if (RPP11 == '' || RPP11 == null) {
                                RPP11 = 0;
                            }
                            if (CSM13 == '' || CSM13 == null) {
                                CSM13 = 0;
                            }
                            if (RPPF11 == '' || RPPF11 == null) {
                                RPPF11 = 0;
                            }
                            if (PP11 + RS11 + RPP11 + RPPF11 > 60) {
                                document.getElementById("ContentPlaceHolder1_txtrproT13").value = 60;
                            }
                            else {


                                document.getElementById("ContentPlaceHolder1_txtrproT13").value = PP11 + RS11 + RPP11 + RPPF11;
                            }

                            if (INB11 == '' || INB11 == null) {
                                INB11 = 0;
                            }
                            if (INB11 > 20) {
                                document.getElementById("ContentPlaceHolder1_txtinbT13").value = 20;
                            }
                            else {
                                document.getElementById("ContentPlaceHolder1_txtinbT13").value = INB11;
                            }
                            if (SD11 == '' || SD11 == null) {
                                SD11 = 0;
                            }
                            if (SD11 > 15) {
                                document.getElementById("ContentPlaceHolder1_txtsdT13").value = 15;
                            }
                            else {
                                document.getElementById("ContentPlaceHolder1_txtsdT13").value = SD11;
                            }





                            if (PCM11 == '' || PCM11 == null) {
                                PCM11 = 0;
                            }
                            if (PCM11 > 20) {
                                document.getElementById("ContentPlaceHolder1_txtpcmT13").value = 20;
                            }
                            else {
                                document.getElementById("ContentPlaceHolder1_txtpcmT13").value = PCM11;
                            }

                            if (PPIIS11 == '' || PPIIS11 == null) {
                                PPIIS11 = 0;
                            }

                            if (PPIIS11 > 10) {
                                document.getElementById("ContentPlaceHolder1_txtpiisT13").value = 10;
                            }
                            else {
                                document.getElementById("ContentPlaceHolder1_txtpiisT13").value = PPIIS11;
                            }

                            if (SSC11 == '' || SSC11 == null) {
                                SSC11 = 0;
                            }

                            if (SSC11 > 10) {
                                document.getElementById("ContentPlaceHolder1_txtsscT3").value = 10;
                            }
                            else {
                                document.getElementById("ContentPlaceHolder1_txtsscT3").value = SSC11;
                            }



                            if (CSW11 == '' || CSW11 == null) {
                                CSW11 = 0;
                            }
                            if (CSW11 > 10) {
                                document.getElementById("ContentPlaceHolder1_txtcswT13").value = 10;
                            }
                            else {
                                document.getElementById("ContentPlaceHolder1_txtcswT13").value = CSW11;
                            }
                            if (CSW11 + SSC11 + PPIIS11 + PCM11 + SD11 + INB11 + PP11 + RS11 + RPP11 + RPPF11 + SF13 + IP13 + NC11 + CSM13 + PWS11 > 300) {
                                document.getElementById("ContentPlaceHolder1_txtGGT13").value = 300;


                            }
                            else {
                                document.getElementById("ContentPlaceHolder1_txtGGT13").value = CSW11 + SSC11 + PPIIS11 + PCM11 + SD11 + INB11 + PP11 + RS11 + RPP11 + RPPF11 + SF13 + IP13 + NC11 + CSM13 + PWS11;
                            }
                            document.getElementById("ContentPlaceHolder1_txtp51point").value = document.getElementById("ContentPlaceHolder1_txtGGT13").value;

                            return true;

                        }
                    }






                    function onlyNumbers(event) {
                        var charCode = (event.which) ? event.which : event.keyCode
                        if (charCode > 31 && (charCode < 48 || charCode > 57))
                            return false;

                        var txtct3 = document.getElementById("ContentPlaceHolder1_txtct3").value;
                        //if (txtct3 > 120) {
                        //    alert("Can not enter more than 120");
                        //    document.getElementById("ContentPlaceHolder1_txtct3").value = '';
                        //    document.getElementById("ContentPlaceHolder1_txtct3").focus();
                        //    document.getElementById("ContentPlaceHolder1_txtct3").style.borderColor = 'red';
                        //    return false;
                        //}

                        // part III
                    }



                    function funChkEnterAndMaxMarkspart3(thisId, thisvalue) {
                        var vtxtmark = thisId.id;

                        if (thisvalue !== "") {


                            var txtp3a = document.getElementById("ContentPlaceHolder1_txtp3a").value;
                            if (txtp3a > 10) {
                                alert("Can not enter more than 10");
                                document.getElementById("ContentPlaceHolder1_txtp3a").value = '';
                                document.getElementById("ContentPlaceHolder1_txtp3a").focus();
                                document.getElementById("ContentPlaceHolder1_txtp3a").style.borderColor = 'red';
                                return false;
                            }

                            var txtp3b = document.getElementById("ContentPlaceHolder1_txtp3b").value;
                            if (txtp3b > 10) {
                                alert("Can not enter more than 10");
                                document.getElementById("ContentPlaceHolder1_txtp3b").value = '';
                                document.getElementById("ContentPlaceHolder1_txtp3b").focus();
                                document.getElementById("ContentPlaceHolder1_txtp3b").style.borderColor = 'red';
                                return false;
                            }

                            var txtp3c = document.getElementById("ContentPlaceHolder1_txtp3c").value;
                            if (txtp3c > 10) {
                                alert("Can not enter more than 10");
                                document.getElementById("ContentPlaceHolder1_txtp3c").value = '';
                                document.getElementById("ContentPlaceHolder1_txtp3c").focus();
                                document.getElementById("ContentPlaceHolder1_txtp3c").style.borderColor = 'red';
                                return false;
                            }

                            var txtp3d = document.getElementById("ContentPlaceHolder1_txtp3d").value;
                            if (txtp3d > 10) {
                                alert("Can not enter more than 10");
                                document.getElementById("ContentPlaceHolder1_txtp3d").value = '';
                                document.getElementById("ContentPlaceHolder1_txtp3d").focus();
                                document.getElementById("ContentPlaceHolder1_txtp3d").style.borderColor = 'red';
                                return false;
                            }

                            var txtp3e = document.getElementById("ContentPlaceHolder1_txtp3e").value;
                            if (txtp3e > 10) {
                                alert("Can not enter more than 10");
                                document.getElementById("ContentPlaceHolder1_txtp3e").value = '';
                                document.getElementById("ContentPlaceHolder1_txtp3e").focus();
                                document.getElementById("ContentPlaceHolder1_txtp3e").style.borderColor = 'red';
                                return false;
                            }

                            var txtp3f = document.getElementById("ContentPlaceHolder1_txtp3f").value;
                            if (txtp3f > 10) {
                                alert("Can not enter more than 10");
                                document.getElementById("ContentPlaceHolder1_txtp3f").value = '';
                                document.getElementById("ContentPlaceHolder1_txtp3f").focus();
                                document.getElementById("ContentPlaceHolder1_txtp3f").style.borderColor = 'red';
                                return false;
                            }

                            var txtp3g = document.getElementById("ContentPlaceHolder1_txtp3g").value;
                            if (txtp3g > 10) {
                                alert("Can not enter more than 10");
                                document.getElementById("ContentPlaceHolder1_txtp3g").value = '';
                                document.getElementById("ContentPlaceHolder1_txtp3g").focus();
                                document.getElementById("ContentPlaceHolder1_txtp3g").style.borderColor = 'red';
                                return false;
                            }

                            var txtp3h = document.getElementById("ContentPlaceHolder1_txtp3h").value;
                            if (txtp3h > 10) {
                                alert("Can not enter more than 10");
                                document.getElementById("ContentPlaceHolder1_txtp3h").value = '';
                                document.getElementById("ContentPlaceHolder1_txtp3h").focus();
                                document.getElementById("ContentPlaceHolder1_txtp3h").style.borderColor = 'red';
                                return false;
                            }

                            var txtp3i = document.getElementById("ContentPlaceHolder1_txtp3i").value;
                            if (txtp3i > 10) {
                                alert("Can not enter more than 10");
                                document.getElementById("ContentPlaceHolder1_txtp3i").value = '';
                                document.getElementById("ContentPlaceHolder1_txtp3i").focus();
                                document.getElementById("ContentPlaceHolder1_txtp3i").style.borderColor = 'red';
                                return false;
                            }
                            var txtp3j = document.getElementById("ContentPlaceHolder1_txtp3j").value;
                            if (txtp3j > 10) {
                                alert("Can not enter more than 10");
                                document.getElementById("ContentPlaceHolder1_txtp3j").value = '';
                                document.getElementById("ContentPlaceHolder1_txtp3j").focus();
                                document.getElementById("ContentPlaceHolder1_txtp3j").style.borderColor = 'red';
                                return false;
                            }

                            if (txtp3a != "" && txtp3b != "" && txtp3c != "" && txtp3d != "" && txtp3e != "" && txtp3f != "" && txtp3g != "" && txtp3h != "" && txtp3i != "" && txtp3j != "") {

                                document.getElementById("ContentPlaceHolder1_txtp43point").value = parseFloat(txtp3a) + parseFloat(txtp3b) + parseFloat(txtp3c) + parseFloat(txtp3d) + parseFloat(txtp3e) + parseFloat(txtp3f) + parseFloat(txtp3g) + parseFloat(txtp3h) + parseFloat(txtp3i) + parseFloat(txtp3j);
                                document.getElementById("ContentPlaceHolder1_txtp53point").value = parseFloat(txtp3a) + parseFloat(txtp3b) + parseFloat(txtp3c) + parseFloat(txtp3d) + parseFloat(txtp3e) + parseFloat(txtp3f) + parseFloat(txtp3g) + parseFloat(txtp3h) + parseFloat(txtp3i) + parseFloat(txtp3j);



                            }

                        }


                    }

                </script>
              <%--  <script type="text/javascript">

                    setTimeout(document.getElementById('ContentPlaceHolder1_btnSave').click(), 5000);
                    //setTimeout("CallButton()", 60000)
                    //function CallButton() {
                    //    alert('Bhupii');
                    //    //document.getElementById("ContentPlaceHolder1_btnSave").click();
                    //}
                </script>--%>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</asp:Content>

