<%@ Page Title="" Language="C#" MasterPageFile="~/Student/IndexMaster.master" AutoEventWireup="true" CodeFile="AssessmentTemp.aspx.cs" Inherits="Faculty_AssessmentTemp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .Bold {
            font-weight: bold;
            background-color: #66D2D0;
        }

        .Color {
            background-color: #AFAFAF;
        }

        .algin {
            text-align: left;
        }
    </style>
    <script>
        function callFeedbackMessage(inputType, inputText) {

            if (inputType == 'Error') {
                alertify.error(inputText, 10000);
                return false;
            }
            else if (inputType == 'Success') {
                //alertify.confirm().set('overflow', false);
                alertify.success("Save Successfully", 1000);
                return false;
            }
            else {
                alertify.log(inputText, "", 10000);
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <fieldset class="boxBody">
                <asp:Label ID="Label3" runat="server"
                    Text="Faculty Assessment" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
            </fieldset>
            <fieldset class="boxBodyHeader">
            </fieldset>
            <fieldset style="background: #fefefe; border-top: 1px solid #dde0e8; border-bottom: 1px solid #dde0e8; padding: 10px 20px; height: 100%">
                <center>
                    <asp:Label ID="lblmsg" runat="server" Text="Feed Back Already submitted !" Visible="false" Font-Size="Large" ForeColor="Green"></asp:Label>
                </center>
                <asp:Panel ID="StudetailPanel" runat="server">

                    <center>
                        <br />
                        <b style="font-size: 20px; font-weight: bold; font-family: Century Schoolbook">College:&nbsp;</b>
                        <asp:Label runat="server" Style="font-size: 20px; font-weight: bold" Font-Underline="true" Font-Names="Century Schoolbook" ID="lblCollegeName"></asp:Label>
                    </center>

                    <br />
                    <table>
                        <tr>
                            <td style="width: 10px"></td>
                            <td>
                                <label>Academic Year:</label></td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:Label runat="server" ID="lblAcad" Text="19-20"></asp:Label></td>
                            <td style="width: 70px"></td>
                            <td>
                                <label>Programme:</label></td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:Label runat="server" ID="lblCourse"></asp:Label></td>
                            <td style="width: 70px"></td>
                            <td>
                                <label>Semester:</label></td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:Label runat="server" ID="lblSemester">                                   

                                </asp:Label></td>
                            <td style="width: 70px"></td>
                            <td>
                                <label>Year:</label></td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:Label runat="server" ID="lblYear">
                                    
                                </asp:Label></td>
                            <td style="width: 70px"></td>
                            <td>
                                <label>Section:</label></td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:Label runat="server" ID="lblSection">
                                    
                                </asp:Label></td>
                            <td style="width: 70px"></td>
                            <td>
                                <label>Date:</label></td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:Label runat="server" ID="lblDate"></asp:Label></td>
                            
                        </tr>
                    </table>

                    <br />
                    <b style="font-size: 15px; font-weight: bold; font-family: Century Schoolbook"><u>Dear Student</u>,</b><br />
                    <label style="font-size: 15px; font-weight: bold; font-family: Century Schoolbook">&nbsp;&nbsp;(Please rate on given dimensions) as P-Poor, A-Average, G-Good, VG-Very Good & E-Excellent.</label>
                </asp:Panel>
                <br />
                <div id="DivASSIGN" runat="server" >
                    <asp:Panel ID="pnlTextBoxes" runat="server" ScrollBars="Vertical" Width="100%">

                        <br />
                        <asp:Table ID="FacultyAssessmentTable" runat="server" CssClass="table table-hover table-bordered"></asp:Table>
                    </asp:Panel>
                    <div class="pull-right">
                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn-sm btn-primary btn-block" UseSubmitBehavior="false" Height="30px" Width="90px" OnClick="btnSubmit_Click" Text="SUBMIT" />

                    </div>
                </div>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
</asp:Content>
