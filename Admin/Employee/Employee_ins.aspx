<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="Employee_ins.aspx.cs" Inherits="Admin_Employee_Employee_ins" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>帳號</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-title-box">
        <h4 id="head_title" class="page-title">帳號</h4>
        <div class="clearfix"></div>
    </div>
      <asp:HiddenField runat="server" ID="id"/>
    <div class="row">
        <div id="view" class="col-md-12 col-sm-9">
            <div class="white-box list2">
                <div class="col-lg-5 m-t-5 p-l-0 p-r-0">
                    <div class="col-lg-12">
                        <a class="btn btn-info" href="Employee.aspx?type=basic"><i class="ti-arrow-left"></i>&nbsp;<span>回列表</span>&nbsp;</a>
                        <a class="btn btn-info" onclick="save()"><i class="ti-save"></i>&nbsp;<span>儲存</span>&nbsp;</a>
                    </div>
                </div>

                <div class="clear"></div>

                <div class="col-lg-12 m-t-5">
                    <div>
                        <asp:Button ID="save" runat="server" Text="Button" OnClick="save_Check" Style="display: none" />
                        <asp:Button ID="del" runat="server" Text="刪除" OnClick="del_Click" Style="display: none" />
                    </div>
                    <div>
                        <h2 class="header-title">帳號</h2>
                        <hr />
                    </div>
                </div>

                <div class="clear"></div>

                <div class="col-lg-12 m-t-5">
                    <div class="col-lg-3 form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-4 control-label"><span style="color: #ff0000">*</span>帳號:</label>
                            <div class="col-sm-8">
                                <input class="form-control" id="Username" type="text" runat="server" maxlength="15" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 form-horizontal" >
                        <div class="form-group">
                            <label class="col-sm-4 control-label"><span style="color: #ff0000">*</span>密碼:</label>
                            <div class="col-sm-8">
                                <input class="form-control" id="Password" type="text" runat="server" maxlength="20" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-4 control-label">權限:</label>
                            <div class="col-sm-8">
                                <asp:DropDownList class="form-control" ID="Auth" runat="server">
                                </asp:DropDownList>
                                <%-- <input class="form-control" id="Auth" type="text" runat="server" maxlength="20" />--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

