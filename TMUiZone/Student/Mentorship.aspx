<%@ Page Title="" Language="C#" MasterPageFile="~/Student/StudentMaster.master" AutoEventWireup="true" CodeFile="Mentorship.aspx.cs" Inherits="Student_Mentorship" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row panel panel-success panel-body" style="border:none">
            
            <div class="panel-body" style="border:none">                      
                    <div class="row">
                         <div class="col-lg-6 col-md-6">
                                <b class="text-success" style="font-size:15px">Roll No: </b>
                                                                <b><asp:Label ID="lblRollNo" runat="server" style="font-size:15px"></asp:Label></b>
                            </div>
                            <div class="col-lg-6 col-md-6">
                                <b class="text-success" style="font-size:15px">Mentor: </b>
                                                                <b><asp:Label ID="lblMentor" runat="server" style="font-size:15px"></asp:Label></b>
                            </div>
                        <div class="col-lg-12 col-md-12">
                           
                            <br />
                            <div class="row">
                                
                                <div class="col-lg-9 col-md-9">
                                    <ul class="nav nav-tabs">
                                        <li class="active"><a data-toggle="tab" href="#General" class="text-success"><i class="fa fa-indent"></i> Details</a></li>
                                        <li><a data-toggle="tab" href="#Contact" class="text-success"><i class="fa fa-bookmark-o"></i> Contact Info</a></li>
                                        <li><a data-toggle="tab" href="#Address" class="text-success"><i class="fa fa-home"></i> Qualification</a></li>
                                    </ul>

                                    <div class="tab-content">
                                        <div id="General" class="tab-pane fade in active">

                                            <div class="table-responsive panel">
                                                <table class="table">
                                                    <tbody>
    
                                                            <tr>
                                                                <td class="text-success">Name </td>
                                                                <td><asp:Label ID="lblName" runat="server"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="text-success"> Program</td>
                                                                <td><asp:Label ID="lblProgram" runat="server"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="text-success"> Batch</td>
                                                                <td><asp:Label ID="lblBatch" runat="server"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="text-success"> Hostler/Day Scholar</td>
                                                                <td><asp:Label ID="lblHostlerDayScholar" runat="server"></asp:Label></td>
                                                            </tr>
                                                        <tr>
                                                                <td class="text-success"> Date of Birth</td>
                                                                <td><asp:Label ID="lblDOB" runat="server"></asp:Label></td>
                                                            </tr>
                                                        <tr>
                                                                <td class="text-success"> Father's Name</td>
                                                                <td><asp:Label ID="lblFathersName" runat="server"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="text-success"> Mother'sName</td>
                                                                <td><asp:Label ID="lblMothersName" runat="server"></asp:Label></td>
                                                            </tr>
                                                          
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>

                                        <div id="Contact" class="tab-pane fade">
                                            <div class="table-responsive panel">
                                                <table class="table">
                                                    <tbody>
    										
                                                            <tr>
                                                                <td class="text-success"> Phone No.</td>
                                                                <td><asp:Label ID="lblPhoneNo" runat="server"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="text-success"> Phone No. Father/Mother</td>
                                                                <td><asp:Label ID="lblFathersPhoneNo" runat="server"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="text-success"> E-Mail(Student)</td>
                                                                <td><asp:Label ID="lblEMailStudent" runat="server"></asp:Label></td>
                                                            </tr>
                                                        <tr>
                                                                <td class="text-success"> E-Mail(Parent)</td>
                                                                <td><asp:Label ID="lblEMailParent" runat="server"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="text-success">Correspondance Address</td>
                                                                <td><asp:Label ID="lblCorrespondanceAddress" runat="server"></asp:Label></td>
                                                            </tr>
                                                            
        										
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>

                                        <div id="Address" class="tab-pane fade">
                                            <div class="table-responsive panel">
                                                <table class="table">
                                                    <tbody>
    									
                                                            <tr>
                                                                <td class="text-success"> High School %</td>
                                                                <td><asp:Label ID="lblHighSchool" runat="server"></asp:Label></td>
                                                            </tr>
                                                        <tr>
                                                                <td class="text-success"> Intermediate %</td>
                                                                <td><asp:Label ID="lblIntermediate" runat="server"></asp:Label></td>
                                                            </tr>
                                                        <tr>
                                                                <td class="text-success"> Graduation %</td>
                                                                <td><asp:Label ID="lblGraduation" runat="server"></asp:Label></td>
                                                            </tr>
                                                            </tbody>
                                                </table>
                                            </div>
                                        </div>
                                        

                                    </div>

                                </div>

                            </div>
                        </div>
                    </div>
                <!-- /.table-responsive -->
                
            </div>
        </div>
</asp:Content>

