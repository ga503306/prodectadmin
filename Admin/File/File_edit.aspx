<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="File_edit.aspx.cs" Inherits="Admin_File_File_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .card {
            box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
            transition: 0.3s;
            width: 300px;
            border-radius: 10px;
        }

            .card:hover {
                box-shadow: 0 8px 16px 0 rgba(0,0,0,0.2);
            }

        .btnn {
            border-radius: 10px;
            height: 60px;
        }
    </style>
    <script>
        function back() {
            $("#ContentPlaceHolder1_btn_back").click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-title-box">
        <h4 id="head_title" class="page-title">船隻文件-編輯</h4>
        <div class="clearfix"></div>
    </div>

    <div class="white-box list2">
        <div class="col-lg-5 m-t-5 p-l-0 p-r-0">
            <div class="col-lg-12">
                <a class="btn btn-info" href="File.aspx?type=yachts"><i class="ti-arrow-left"></i>&nbsp;<span>回列表</span>&nbsp;</a>
                <a class="btn btn-info" onclick="back()"><i class="ti-back-right"></i>&nbsp;<span>回主檔</span>&nbsp;</a>
                <asp:Button ID="btn_back" runat="server" Text="Button" OnClick="btn_back_Click" Style="display: none" />
            </div>
        </div>

        <div class="clear"></div>

        <div class="col-lg-12 m-t-5">
            <div>
                <h2 class="header-title">船隻文件-編輯</h2>
                <hr />
            </div>
        </div>

        <div class="clear"></div>
        <asp:HiddenField ID="id" runat="server" />
        <div class="col-lg-12 m-t-5">
            <div class="col-lg-3 ">

                <asp:FileUpload ID="SaveFile" runat="server" onchange="previewFile()" />
            </div>
            <div class="col-lg-3 form-horizontal">
                <div class="form-group">
                    <label class="col-sm-4 control-label">自訂檔名:</label>
                    <div class="col-sm-8">
                        <input class="form-control" id="Custom_name" type="text" runat="server" maxlength="20" />
                    </div>
                </div>
            </div>
            <div class="col-lg-2 ">
                <asp:Button ID="save" runat="server" Text="上傳" CssClass="btn btn-info" OnClick="save_Click" />
            </div>


        </div>

        <div class="col-lg-12 m-t-5">
            <%--  <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
            <asp:Repeater ID="Rpt_File" runat="server" OnItemCommand="Rpt_File_ItemCommand" OnItemCreated="Rpt_File_ItemCreated">
                <ItemTemplate>
                    <div class="col-lg-3 col-sm-6" style="margin-top: 20px">
                        <div class="card">
                            <img src="../../images/icon/document.png" alt="Avatar" runat="server" style="width: 20%" id="show_icon" />
                            <asp:Label ID="FileName" runat="server"><a href='../../<%# Eval("FileMapPath")%>' target="_blank"><%# Eval("FileName")%></a></asp:Label>
                            <asp:Button ID="del" runat="server" Text="刪除" CssClass="btn btn-danger btnn pull-right" CommandName="del"  OnClientClick="return Del_check(this);" CommandArgument='<%# Eval("Filename_full") %>' />

                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <%--</ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="save" />
                </Triggers>
            </asp:UpdatePanel>--%>
        </div>
    </div>
</asp:Content>

