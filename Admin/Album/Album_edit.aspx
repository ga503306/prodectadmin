<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="Album_edit.aspx.cs" Inherits="Admin_Album_Album_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>電子相簿</title>
    <style>
        .img {
            max-height: 150px;
            max-width: 150px;
            margin: 20px;
        }
    </style>
    <script>
        function previewFileimg() {
            var ext = getFileExtension3($("#ContentPlaceHolder1_FileUpload1")[0].files[0].name);
            if (ext != "jpg" && ext != "png" && ext != "jpeg" && ext != "gif") {
                $("#ContentPlaceHolder1_FileUpload1").val("");
                swal('您的圖片格式不正確!');
                return;
            }
            var preview = document.querySelector('#<%=img.ClientID %>');
            var file = document.querySelector('#<%=FileUpload1.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            }
            else {
                preview.src = "";
            }
        }
        function getFileExtension3(filename) { //副檔名
            return filename.slice((filename.lastIndexOf(".") - 1 >>> 0) + 2);
        }
        function back() {
            $("#ContentPlaceHolder1_btn_back").click();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-title-box">
        <h4 id="head_title" class="page-title">電子相簿-編輯</h4>
        <div class="clearfix"></div>
    </div>
    <div class="row">
        <div id="view" class="col-md-12 col-sm-9">
            <div class="white-box list2">
                <div class="col-lg-5 m-t-5 p-l-0 p-r-0">
                    <div class="col-lg-12">
                        <a class="btn btn-info" href="Album.aspx?type=yachts"><i class="ti-arrow-left"></i>&nbsp;<span>回列表</span>&nbsp;</a>
                        <a class="btn btn-info" onclick="back()"><i class="ti-back-right"></i>&nbsp;<span>回主檔</span>&nbsp;</a>
                        <asp:Button ID="btn_back" runat="server" Text="Button" OnClick="btn_back_Click" Style="display: none" />
                    </div>
                </div>

                <div class="clear"></div>

                <div class="col-lg-12 m-t-5">
                    <div>
                        <h2 class="header-title">電子相簿-編輯</h2>
                        <hr />
                    </div>
                </div>

                <div class="clear"></div>
                <asp:HiddenField ID="id" runat="server" />

                <div class="col-lg-12 m-t-5">
                    <div class="col-lg-12 form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-1 control-label">圖片上傳:</label>

                            <div class="col-sm-11">
                                <div class="col-sm-2">
                                    <asp:FileUpload ID="FileUpload1" runat="server" onchange="previewFileimg()" />
                                </div>
                                <div class="col-sm-10">
                                    <asp:Button ID="upimg" runat="server" Text="上傳" CssClass="btn btn-secondary" Style="" OnClick="upimg_Click" />
                                </div>
                                <asp:ImageButton ID="img" runat="server" Enabled="false" ImageUrl="~/images/預設圖片.png" CssClass="img" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="mt-3 ">
                    <asp:ScriptManager runat="server"></asp:ScriptManager>
                    <asp:UpdatePanel ID="test" class="" runat="server">
                        <ContentTemplate>
                            <asp:Repeater ID="rpt_complaint" runat="server" OnItemCommand="rpt_complaint_ItemCommand">
                                <ItemTemplate>
                                    <div class="col-12 col-sm-12 col-md-6 col-lg-3 col-xl-2 m-t-5">
                                        <div>
                                            <img src='<%# Eval("strFilename","../../sqlimages/Album/{0}") %>' style="width: 100%; height: 200px;" />
                                        </div>
                                        <div class="text-right mt-2">
                                            <asp:Button ID="btn" runat="server" class="btn btn-danger" Text="刪除" CommandName="Del" CommandArgument='<%# Eval("strFilename") %>' />
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rpt_complaint" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="clear"></div>
            </div>
        </div>
    </div>
</asp:Content>

