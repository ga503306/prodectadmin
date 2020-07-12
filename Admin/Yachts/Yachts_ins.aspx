<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/MasterPage.master" validateRequest="False" AutoEventWireup="true" CodeFile="Yachts_ins.aspx.cs" Inherits="Admin_Yachts_Yachts_ins" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>遊艇主檔-新增</title>
    <script>
        $(function () {
            CKEDITOR.replace('ContentPlaceHolder1_Overview'
                , {
                    filebrowserImageUploadUrl: '../../plugins/ckeditor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images'
                });
            CKEDITOR.replace('ContentPlaceHolder1_Layout'
                , {
                    filebrowserImageUploadUrl: '../../plugins/ckeditor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images'
                });
            CKEDITOR.replace('ContentPlaceHolder1_Specification'
                , {
                    filebrowserImageUploadUrl: '../../plugins/ckeditor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images'
                });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-title-box">
        <h4 id="head_title" class="page-title">遊艇主檔-新增</h4>
        <div class="clearfix"></div>
    </div>
    <div class="row">
        <div id="view" class="col-md-12 col-sm-9">
            <div class="white-box list2">
                <div class="col-lg-5 m-t-5 p-l-0 p-r-0">
                    <div class="col-lg-12">
                        <a class="btn btn-info" href="Yachts.aspx?type=yachts"><i class="ti-arrow-left"></i>&nbsp;<span>回列表</span>&nbsp;</a>
                        <a class="btn btn-info" onclick="save()"><i class="ti-save"></i>&nbsp;<span>儲存</span>&nbsp;</a>
                    </div>
                </div>

                <div class="clear"></div>

                <div class="col-lg-12 m-t-5">
                    <div>
                        <asp:Button ID="save" runat="server" Text="Button" OnClick="save_Check" Style="display: none" />
                    </div>
                    <div>
                        <h2 class="header-title">遊艇主檔-新增</h2>
                        <hr />
                    </div>
                </div>

                <div class="clear"></div>
                <asp:HiddenField ID="id" runat="server" />
                <div class="col-lg-12 m-t-5">
                    <div class="col-lg-3 form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-4 control-label"><span style="color: #ff0000">*</span>遊艇流水號:</label>
                            <div class="col-sm-8">
                                <input class="form-control" id="Yachtsno" readonly="" placeholder="系統自動產生" type="text" runat="server" maxlength="30" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-4 control-label"><span style="color: #ff0000">*</span>遊艇型:</label>
                            <div class="col-sm-8">
                                <input class="form-control" id="Modal" type="text" runat="server" maxlength="30" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-4 control-label"><span style="color: #ff0000">*</span>遊艇型號:</label>
                            <div class="col-sm-8">
                                <input class="form-control" id="Modal_n" type="number" min="0" runat="server" maxlength="30" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-4 control-label">是否置頂:</label>
                            <div class="col-sm-8">
                                <asp:DropDownList class="form-control" ID="Isnew" runat="server">
                                <asp:ListItem Value="0" Text="否"></asp:ListItem>
                                <asp:ListItem Value="1" Text="是"></asp:ListItem>
                            </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12 m-t-5">
                    <div class="col-lg-12 form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-1 control-label">Overview內文:</label>
                            <div class="col-sm-11">
                                <textarea rows="3" class="form-control" id="Overview" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12 m-t-5">
                    <div class="col-lg-12 form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-1 control-label">Layout內文:</label>
                            <div class="col-sm-11">
                                <textarea rows="3" class="form-control" id="Layout" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12 m-t-5">
                    <div class="col-lg-12 form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-1 control-label">Specification內文:</label>
                            <div class="col-sm-11">
                                <textarea rows="3" class="form-control" id="Specification" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12 m-t-5">
                    <div class="col-lg-12 form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-1 control-label">檔案上傳:</label>
                            <div class="col-sm-11">
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="clear"></div>
            </div>
        </div>
    </div>
</asp:Content>

