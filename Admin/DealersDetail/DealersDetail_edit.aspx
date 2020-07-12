<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/MasterPage.master" ValidateRequest="False" AutoEventWireup="true" CodeFile="DealersDetail_edit.aspx.cs" Inherits="Admin_DealersDetail_DealersDetail_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>經銷商管理-編輯</title>
    <style>
        .img {
            max-height: 150px;
            max-width: 150px;
            margin: 20px;
        }
    </style>
    <script>
        $(function () {
            CKEDITOR.replace('ContentPlaceHolder1_Info'
                , {
                    filebrowserImageUploadUrl: '../../plugins/ckeditor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images'
                });
            init();
        });
        function opene(idControla, idControlb) {
            var OpenPage = window.open('../FindDealersR.aspx?idControla=' + idControla + '&idControlb=' + idControlb, '_blank', 'menubar=no, toolbar=no, location=no, directories=no, status=no, resizable=0, height=500,width=480,top=300,left=300,scrollbars=yes');
        }

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
        function init() {
            $("#ContentPlaceHolder1_Region").on('focus', function () {
                $("#ContentPlaceHolder1_btn_list").click();
            });
        }
        function getFileExtension3(filename) { //副檔名
            return filename.slice((filename.lastIndexOf(".") - 1 >>> 0) + 2);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-title-box">
        <h4 id="head_title" class="page-title">經銷商管理-編輯</h4>
        <div class="clearfix"></div>
    </div>
    <div class="row">
        <div id="view" class="col-md-12 col-sm-9">
            <div class="white-box list2">
                <div class="col-lg-5 m-t-5 p-l-0 p-r-0">
                    <div class="col-lg-12">
                        <a class="btn btn-info" href="DealersDetail.aspx?type=dealers"><i class="ti-arrow-left"></i>&nbsp;<span>回列表</span>&nbsp;</a>
                        <a class="btn btn-info" onclick="save()"><i class="ti-save"></i>&nbsp;<span>儲存</span>&nbsp;</a>
                        <a id="del_btn" class="btn btn-danger" onclick="data_del('')" runat="server"><i class="fa fa-times"></i>&nbsp;<span>刪除</span>&nbsp;</a>
                    </div>
                </div>

                <div class="clear"></div>

                <div class="col-lg-12 m-t-5">
                    <div>
                        <asp:Button ID="save" runat="server" Text="Button" OnClick="save_Check" Style="display: none" />
                        <asp:Button ID="del" runat="server" Text="刪除" OnClick="del_Click" Style="display: none" />
                        
                        <asp:Button ID="btn_list" runat="server" Text="查詢" CssClass="btn btn-success" Style="display: none;" OnClick="list_Click" />
                    </div>
                    <div>
                        <h2 class="header-title">經銷商管理-編輯</h2>
                        <hr />
                    </div>
                </div>

                <div class="clear"></div>
                <asp:HiddenField ID="id" runat="server" />
                <div class="col-lg-12 m-t-5">
                    <div class="col-lg-3 form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-4 control-label"><span style="color: #ff0000">*</span>流水號:</label>
                            <div class="col-sm-8">
                                <input class="form-control" id="D_no" type="text" readonly="" runat="server" maxlength="15" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-3 control-label"><span style="color: #ff0000">*</span>國家別:</label>
                            <div class="col-sm-9">
                                <div class="input-group" style="width: 100%;">
                                    <input id="R_no" class="form-control" style="width: 40%;" type="text" runat="server" readonly="" />
                                    <input id="Region" class="form-control" style="width: 60%;" type="text" runat="server" readonly="" />
                                    <span class="input-group-btn">
                                        <asp:Button ID="Btn_DealersR" runat="server" Text="..." CssClass="btn btn-default" />
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:ScriptManager runat="server"></asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="col-lg-3 form-horizontal">
                                <div class="form-group">
                                    <label class="col-sm-3 control-label"><span style="color: #ff0000">*</span>地區:</label>
                                    <div class="col-sm-9">
                                        <asp:DropDownList CssClass="form-control" ID="C_no" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_list" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
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
                                <textarea class="" name="Context_" id="Info" rows="7" maxlength="500" runat="server"></textarea>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

