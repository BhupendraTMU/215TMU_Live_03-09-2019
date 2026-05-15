<%@ Page Title="" Language="C#" MasterPageFile="~/Student/IndexMaster.master" AutoEventWireup="true" CodeFile="ExamResultDisplay.aspx.cs" Inherits="Student_ExamResultDisplay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="tab-pane active">

        <div class="right_col_bg">
            <div class="right_col_content border-box">
                <div class="row">
                    <asp:UpdatePanel ID="mrak" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div id="Panlehid" class="label-responsive" runat="server">
                                <div class="col-md-12 p-0 clearfix">
                                    <div class="btn pull-right" style="display:none">
                                        <asp:Button ID="BtnPrint" OnClientClick="PrintDiv();" CssClass="btn btn-warning" runat="server" Text="Print" />

                                    </div>
                                    <div class="col-md-6 p-0" style="display: none">
                                        <div class="col-sm-4 col-md-3">
                                            <label>Semester</label>
                                        </div>
                                        <div class="col-sm-8 col-md-9 form-group">
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlSemester" AutoPostBack="true">
                                                <asp:ListItem Value="0" Text="--select--"></asp:ListItem>
                                                <asp:ListItem Value="I" Text="I"></asp:ListItem>
                                                <asp:ListItem Value="II" Text="II"></asp:ListItem>
                                                <asp:ListItem Value="III" Text="III"></asp:ListItem>
                                                <asp:ListItem Value="IV" Text="IV"></asp:ListItem>
                                                <asp:ListItem Value="V" Text="V"></asp:ListItem>
                                                <asp:ListItem Value="VI" Text="VI"></asp:ListItem>
                                                <asp:ListItem Value="VII" Text="VII"></asp:ListItem>
                                                <asp:ListItem Value="VIII" Text="VIII"></asp:ListItem>
                                            </asp:DropDownList>

                                        </div>

                                    </div>

                                    <div class="col-md-6 p-0">
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div id="printarea">
                                <div class="clearfix">
                                    <div class="col-sm-12"  >
                                        <asp:Panel ID="pnlheader" runat="server">
                                            <div class="panel-heading" style="background-color: #2b5b69">
                                                <center>
                                                    <div class="panel-title" style="fit-position: center;">
                                                        <b>
                                                            <p style="color: white; font-size: 20px">
                                                           INTERNAL RESULTS
                                                            </p>
                                                        </b>
                                                    </div>
                                                </center>
                                            </div>
                                        </asp:Panel>
                                        <br />

                                        <div class="form-group clearfix">
            <table>
                <tr>
                   
                     
                    <td valign="top">
                        <label id="lblsem" style="font-weight: bold">Sem/Year</label>
                    </td>
                    <td style="width: 20px"></td>
                    <td>
                        <asp:DropDownList ID="ddlSem" Width="200px" runat="server" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 20px"></td>
                    <td valign="top" id="examtype" runat="server" visible="false">
                        <label id="lblExam" style="font-weight: bold">Exam Type</label>
                    </td>
                    <td style="width: 20px"></td>
                    <td runat="server" visible="false">
                        <asp:DropDownList ID="drpExam" Width="200px" runat="server"  AutoPostBack="true">
                            <asp:ListItem Text="Main" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Re-Appear" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                     <td style="width: 20px"></td>
                    <td valign="top" id="Academic" >
                        <label id="lblAcademic" style="font-weight: bold" runat="server" visible="false">Academic Year</label>
                    </td>
                    <td style="width: 20px"></td>
                    <td>
                        <asp:DropDownList ID="drpAcademic" Width="200px" runat="server" Visible="false"  AutoPostBack="true">
                           
                        </asp:DropDownList>
                    </td>
                    
                </tr>
            </table>




        </div>
                                        <div style="margin-right:5%" >
                                              <fieldset class="boxBodyHeader">
                            <asp:Label ID="lblExamination" runat="server"
                                Text="THEORY INTERNAL RESULT" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                        </fieldset>
                        <br />
                                        <asp:GridView ID="GrdExamResults" runat="server" CssClass="table table-striped table-bordered"
                                            AutoGenerateColumns="true" Style="width: 98%; margin-left: 2%; margin-right: 2%" EmptyDataText="No Data to display">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.no">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex +1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                                                <fieldset class="boxBodyHeader">
                            <asp:Label ID="Label1" runat="server"
                                Text="PRACTICAL INTERNAL RESULT" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                        </fieldset>
                        <br />
                                               <asp:GridView ID="GridPV" runat="server" CssClass="table table-striped table-bordered"
                                            AutoGenerateColumns="true" Style="width: 98%; margin-left: 2%; margin-right: 2%" EmptyDataText="No Data to display">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.no">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex +1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                            </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

            </div>
        </div>
    </div>
</asp:Content>

