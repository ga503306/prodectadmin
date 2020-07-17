<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="Account.aspx.cs" Inherits="Admin_Account_Account" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-title-box">
        <h4 id="head_title" class="page-title">變更密碼</h4>
        <div class="clearfix"></div>
    </div>
    <div class="row">
        <div id="view" class="col-md-12 col-sm-9">
            <div class="white-box list2">
                <div class="col-lg-5 m-t-5 p-l-0 p-r-0">
                    <div class="col-lg-12">
                        <%--<a class="btn btn-info" href="News.aspx?type=news"><i class="ti-arrow-left"></i>&nbsp;<span>回列表</span>&nbsp;</a>--%>
                        <a class="btn btn-info" onclick="save()"><i class="ti-save"></i>&nbsp;<span>儲存</span>&nbsp;</a>
                     <%--   <a id="del_btn" class="btn btn-danger" onclick="data_del('')" runat="server"><i class="fa fa-times"></i>&nbsp;<span>刪除</span>&nbsp;</a>--%>
                    </div>
                </div>

                <div class="clear"></div>

                <div class="col-lg-12 m-t-5">
                    <div>
                        <asp:Button ID="save" runat="server" Text="Button" OnClick="save_Check" Style="display: none" />
                      <%--  <asp:Button ID="del" runat="server" Text="刪除" OnClick="del_Click" Style="display: none" />--%>
                    </div>
                    <div>
                        <h2 class="header-title">變更密碼</h2>
                        <hr />
                    </div>
                </div>

                <div class="clear"></div>
                <asp:HiddenField ID="id" runat="server" />
                <div class="col-lg-12 m-t-5">
                    <div class="col-lg-3 form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-4 control-label">舊密碼:</label>
                            <div class="col-sm-8">
                                <input class="form-control" id="pwd" type="password" runat="server" maxlength="50" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12 m-t-5">
                    <div class="col-lg-3 form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-4 control-label">新密碼:</label>
                            <div class="col-sm-8">
                                <input class="form-control" id="npwd" type="password" runat="server" maxlength="100" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-4 control-label">確認密碼:</label>
                            <div class="col-sm-8">
                                <input class="form-control" id="chknpwd" type="password" runat="server" maxlength="100" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

