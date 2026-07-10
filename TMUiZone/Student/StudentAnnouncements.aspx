<%@ Page Title="Announcements" Language="C#"
    MasterPageFile="~/Student/IndexMaster.master"
    AutoEventWireup="true"
    CodeFile="StudentAnnouncements.aspx.cs"
    Inherits="Student_StudentAnnouncement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="../bootstrap/css/bootstrap.min.css" />
    <link href="css/font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" />

    <style>
        .table th {
            background-color: #337ab7;
            color: white;
        }

        <style >
        /* Announcement Box */
        .box-primary {
            border-top: 4px solid #3c8dbc;
            box-shadow: 0 2px 15px rgba(0,0,0,.08);
            border-radius: 8px;
        }

        .announcement-header {
            padding: 15px 20px;
            background: #fff;
        }

            .announcement-header .box-title {
                font-size: 22px;
                font-weight: 600;
                color: #093A62;
            }

        .notification-badge {
            background: #dc3545;
            color: #fff;
            padding: 8px 15px;
            border-radius: 25px;
            font-weight: 600;
        }

            .notification-badge i {
                margin-right: 5px;
            }

        /* Grid */
        .table {
            margin-bottom: 0;
        }

            .table th {
                background: #093A62;
                color: white;
                text-align: center;
                font-weight: 600;
            }

            .table td {
                vertical-align: middle !important;
            }

        .table-hover tbody tr:hover {
            background: #f8fbff;
        }

        /* Buttons */
        .btn-view {
            background: #17a2b8;
            color: #fff !important;
            border-radius: 20px;
            padding: 5px 12px;
            text-decoration: none;
        }

        .btn-download {
            background: #28a745;
            color: #fff !important;
            border-radius: 20px;
            padding: 5px 12px;
            text-decoration: none;
        }

        /* Modal */
        .modal-content {
            border-radius: 15px;
            overflow: hidden;
        }

        .modal-header {
            background: linear-gradient(135deg,#093A62,#1e88e5);
            color: white;
        }

        .modal-title {
            font-weight: 600;
        }

        .announcement-card {
            padding: 10px;
        }

        .announcement-top {
            background: #f8fbff;
            border-left: 5px solid #1e88e5;
            padding: 15px;
            border-radius: 8px;
        }

            .announcement-top h3 {
                margin: 0;
                color: #093A62;
                font-size: 22px;
            }

        .announcement-date {
            display: block;
            margin-top: 8px;
            color: #666;
        }

        .announcement-desc {
            margin-top: 20px;
            padding: 15px;
            background: #fafafa;
            border-radius: 8px;
        }

            .announcement-desc h4 {
                margin-top: 0;
                color: #093A62;
            }

        .attachment-box {
            margin-top: 20px;
        }

        .attachment-frame {
            width: 100%;
            height: 500px;
            border: 1px solid #ddd;
            border-radius: 10px;
        }

        .label-status {
            padding: 5px 10px;
            border-radius: 15px;
            color: white;
            font-size: 12px;
        }

        .read {
            background: #28a745;
        }

        .unread {
            background: #dc3545;
        }
    </style>
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="box box-primary">

        <div class="box-header with-border announcement-header">

            <h3 class="box-title">
                <i class="fa fa-bullhorn"></i>Announcements
            </h3>

            <div class="pull-right">
                <span class="notification-badge">
                    <i class="fa fa-bell"></i>
                    <asp:Label ID="lblNotificationCount" runat="server"></asp:Label>
                </span>
            </div>

        </div>

        <div class="box-body">

            <asp:GridView ID="gvStudentAnnouncement"
                runat="server"
                CssClass="table table-bordered table-striped table-hover"
                AutoGenerateColumns="False"
                EmptyDataText="No Announcement Available"
                Width="100%"
                DataKeyNames="AnnouncementID"
                OnRowCommand="gvStudentAnnouncement_RowCommand">

                <Columns>

                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <%# Eval("AnnouncementDate","{0:dd/MM/yyyy}") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="Title"
                        HeaderText="Title" />

                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <%# Eval("Description").ToString().Length > 80 ? Eval("Description").ToString().Substring(0,80)+"..." : Eval("Description") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>

                            <span class='<%# Convert.ToInt32(Eval("IsRead")) == 1 ? "label-status read" : "label-status unread" %>'>

                                <%# Convert.ToInt32(Eval("IsRead")) == 1 ? "Read" : "Unread" %>

                            </span>

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="View">
                        <ItemTemplate>

                            <asp:LinkButton ID="btnView"
                                runat="server"
                                CssClass="btn-view"
                                CommandName="ViewFile"
                                Text="<i class='fa fa-eye'></i> View"
                                CommandArgument='<%#
                            Eval("AnnouncementID") + "|" +
                            Eval("Title") + "|" +
                            Eval("Description") + "|" +
                            Eval("AnnouncementDate","{0:dd/MM/yyyy}") + "|" +
                            Eval("FilePath")
                            %>' />

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Download">
                        <ItemTemplate>

                            <asp:LinkButton ID="btnDownload"
                                runat="server"
                                CssClass="btn-download"
                                Text="<i class='fa fa-download'></i> Download"
                                CommandName="DownloadFile"
                                CommandArgument='<%# Eval("AnnouncementID") + "|" + Eval("FilePath") %>' />

                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>

            </asp:GridView>

        </div>


    </div>
    <div class="modal fade" id="fileModal" tabindex="-1">

        <div class="modal-dialog modal-lg">

            <div class="modal-content">

                <div class="modal-header">

                    <h4 class="modal-title">
                        <i class="fa fa-bullhorn"></i>
                        Announcement Details
                    </h4>

                    <button type="button"
                        class="close"
                        data-dismiss="modal">
                        &times;
                    </button>

                </div>

                <div class="modal-body">

                    <div class="announcement-card">

                        <div class="announcement-top">

                            <h3>
                                <asp:Label ID="lblModalTitle"
                                    runat="server">
                                </asp:Label>
                            </h3>

                            <span class="announcement-date">
                                <i class="fa fa-calendar"></i>

                                <asp:Label ID="lblDate"
                                    runat="server">
                                </asp:Label>
                            </span>

                        </div>

                        <div class="announcement-desc">

                            <h4>
                                <i class="fa fa-file-text-o"></i>
                                Description
                            </h4>

                            <asp:Label ID="lblDescription"
                                runat="server">
                            </asp:Label>

                        </div>

                        <div class="attachment-box">

                            <h4>
                                <i class="fa fa-paperclip"></i>
                                Attachment Preview
                            </h4>

                            <iframe id="iframeFile"
                                runat="server"
                                class="attachment-frame"
                                frameborder="0"></iframe>

                        </div>

                    </div>

                </div>

                <div class="modal-footer">

                    <asp:HiddenField ID="hfAnnouncementID"
                        runat="server" />

                    <asp:HiddenField ID="hfFilePath"
                        runat="server" />

                    <button type="button"
                        class="btn btn-default"
                        data-dismiss="modal">
                        Close
                    </button>

                </div>

            </div>

        </div>

    </div>
</asp:Content>
