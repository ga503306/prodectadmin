<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/MasterPage.master" ValidateRequest="False" AutoEventWireup="true" CodeFile="News_edit.aspx.cs" Inherits="Admin_News_News_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>最新消息-編輯</title>
    <style>
        .img {
            max-height: 150px;
            max-width: 150px;
            margin: 20px;
        }
    </style>
    <script>
        $(function () {
            var inday = document.getElementById("<%=this.Inday_.ClientID %>");
            $(inday).datepicker({
                format: "yyyy-mm-dd",
                autoclose: true,
                todayHighlight: true,
                orientation: "bottom left"
            });

            CKEDITOR.replace('ContentPlaceHolder1_Context_'
                , {
                    filebrowserImageUploadUrl: '../../plugins/ckeditor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images'
                })
        });

        function previewFileimg() {
            var ext = getFileExtension3($("#ContentPlaceHolder1_FileUploadimg")[0].files[0].name);
            if (ext != "jpg" && ext != "png" && ext != "jpeg" && ext != "gif") {
                $("#ContentPlaceHolder1_FileUploadimg").val("");
                swal('您的圖片格式不正確!');
                return;
            }
            var preview = document.querySelector('#<%=img.ClientID %>');
            var file = document.querySelector('#<%=FileUploadimg.ClientID %>').files[0];
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-title-box">
        <h4 id="head_title" class="page-title">最新消息-編輯</h4>
        <div class="clearfix"></div>
    </div>
    <div class="row">
        <div id="view" class="col-md-12 col-sm-9">
            <div class="white-box list2">
                <div class="col-lg-5 m-t-5 p-l-0 p-r-0">
                    <div class="col-lg-12">
                        <a class="btn btn-info" href="News.aspx?type=news"><i class="ti-arrow-left"></i>&nbsp;<span>回列表</span>&nbsp;</a>
                        <a class="btn btn-info" href="News_ins.aspx?type=news"><i class="fa fa fa-plus"></i>&nbsp;<span>新增</span>&nbsp;</a>
                        <a class="btn btn-info" onclick="save()"><i class="ti-save"></i>&nbsp;<span>儲存</span>&nbsp;</a>
                        <a id="del_btn" class="btn btn-danger" onclick="data_del('')" runat="server"><i class="fa fa-times"></i>&nbsp;<span>刪除</span>&nbsp;</a>
                    </div>
                </div>

                <div class="clear"></div>

                <div class="col-lg-12 m-t-5">
                    <div>
                        <asp:Button ID="save" runat="server" Text="Button" OnClick="save_Check" Style="display: none" />
                        <asp:Button ID="del" runat="server" Text="刪除" OnClick="del_Click" Style="display: none" />
                    </div>
                    <div>
                        <h2 class="header-title">最新消息-編輯</h2>
                        <hr />
                    </div>
                </div>

                <div class="clear"></div>
                <asp:HiddenField id="id" runat="server" />
                <div class="col-lg-12 m-t-5">
                    <div class="col-lg-3 form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-4 control-label"><span style="color: #ff0000">*</span>新聞流水號:</label>
                            <div class="col-sm-8">
                                <input class="form-control" id="Newsno" type="text" readonly="" runat="server" maxlength="15" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-4 control-label">是否置頂:</label>
                            <div class="col-sm-8">
                                <asp:DropDownList class="form-control" ID="Priority" runat="server">
                                <asp:ListItem Value="0" Text="否"></asp:ListItem>
                                <asp:ListItem Value="1" Text="是"></asp:ListItem>
                            </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-4 control-label">顯示日期:</label>
                            <div class="col-sm-8">
                                <input class="form-control" id="Inday_" autocomplete="off" type="text" runat="server" maxlength="10" />
                            </div>
                        </div>
                    </div>
                </div>
                  <div class="col-lg-12 m-t-5">
                    <div class="col-lg-12 form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-1 control-label">標題:</label>
                            <div class="col-sm-11">
                                <input class="form-control" id="Title_" type="text" runat="server" maxlength="50" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12 m-t-5">
                    <div class="col-lg-12 form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-1 control-label">簡介:</label>
                            <div class="col-sm-11">
                                <input class="form-control" id="Info" type="text" runat="server" maxlength="100" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12 m-t-5">
                    
                    <div class="col-lg-3 form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-4 control-label">上傳圖片:</label>
                            <div class="col-sm-8">
                                <asp:FileUpload ID="FileUploadimg" runat="server" onchange="previewFileimg()" Style="margin-bottom: 10px; margin-top: 6px;" />
                                <asp:ImageButton ID="img" runat="server" Enabled="false" ImageUrl="~/images/預設圖片.png" CssClass="img" />
                                <asp:HiddenField ID="img_temp" runat="server" />
                            </div>
                        </div>
                    </div>
                    
                </div>
                <div class="col-lg-12 m-t-5">
                    <div class="col-lg-12 form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-1 control-label">內文:</label>
                            <div class="col-sm-11">
                                <textarea class="" name="Context_" id="Context_" rows="7" maxlength="500" runat="server"></textarea>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

