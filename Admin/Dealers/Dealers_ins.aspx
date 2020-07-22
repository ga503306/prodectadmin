<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="Dealers_ins.aspx.cs" Inherits="Admin_Dealers_Dealers_ins" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>國家別,地區管理-新增</title>
     <script>
        function edit(index, Country) {
            $("#ContentPlaceHolder1_index_detail").val(index);
            $("#ContentPlaceHolder1_Country_detail").val(Country);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="page-title-box">
        <h4 id="head_title" class="page-title">國家別,地區管理-新增</h4>
        <div class="clearfix"></div>
    </div>
     <div class="row">
        <div id="view" class="col-md-12 col-sm-9">
            <div class="white-box list2">
                <div class="col-lg-5 m-t-5 p-l-0 p-r-0">
                    <div class="col-lg-12">
                        <a class="btn btn-info" href="Dealers.aspx?type=dealers"><i class="ti-arrow-left"></i>&nbsp;<span>回列表</span>&nbsp;</a>
                        <a class="btn btn-info" onclick="save()"><i class="ti-save"></i>&nbsp;<span>儲存</span>&nbsp;</a>
                    <%--    <a id="del_btn" class="btn btn-danger" onclick="data_del('')" runat="server"><i class="fa fa-times"></i>&nbsp;<span>刪除</span>&nbsp;</a>--%>
                    </div>
                </div>

                <div class="clear"></div>

                <div class="col-lg-12 m-t-5">
                    <div>
                        <asp:Button ID="save" runat="server" Text="Button" OnClick="save_Check" Style="display: none" />
                        <%-- <asp:Button ID="del" runat="server" Text="刪除" OnClick="del_Click" Style="display: none" />--%>
                    </div>
                    <div>
                        <h2 class="header-title">國家別,地區管理-新增</h2>
                        <hr />
                    </div>
                </div>

                <div class="clear"></div>
                <asp:HiddenField ID="id" runat="server" />
                <asp:HiddenField ID="detailtype" Visible="false" runat="server" />
                <%--判斷detail 是否修改--%>
                <asp:HiddenField ID="DetailState" Visible="false" runat="server" />
                <%--判斷detail 新增編輯狀態--%>
                <asp:HiddenField ID="index_detail" Visible="true" runat="server" />
                <%--暫存detail 判斷row--%>
                <div class="col-lg-12 m-t-5">
                    <div class="col-lg-3 form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-4 control-label">流水號:</label>
                            <div class="col-sm-8">
                                <input class="form-control" id="R_no" placeholder="系統自動產生" readonly="" type="text" runat="server" maxlength="30" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-4 control-label">國家別:</label>
                            <div class="col-sm-8">
                                <input class="form-control" id="Region" type="text" runat="server" maxlength="30" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="clear"></div>
                <div class="col-lg-12 form-horizontal" style="margin-top: 20px">
                    <div class="col-lg-12" style="border: 1px solid #999; padding-top: 5px;">
                        <div class="form-group" style="margin-left: 0px">
                            <span class="input-group-btn">
                                <asp:Button ID="addBtn" runat="server" Text="+" CssClass="btn btn-default" OnClick="addBtn_Click" />
                            </span>
                        </div>
                        <div class="form-group" style="margin-left: 0px">
                            <asp:GridView ID="Grid_DealersC" runat="server" class="table table-bordered" Width="100%" AutoGenerateColumns="False" EmptyDataText="尚無資料" ShowHeaderWhenEmpty="True" OnRowCommand="Grid_DealersC_RowCommand" OnPreRender="Grid_DealersC_PreRender">
                                <Columns>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="Sel" ImageUrl="~/images/icon/list.png"
                                                CommandArgument='<%# Eval("Country")  %>' />
                                        </ItemTemplate>
                                        <ControlStyle Width="18px" />
                                        <HeaderStyle Width="18px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Country" HeaderText="地區"></asp:BoundField>
                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/images/icon/刪除紅.png" HeaderStyle-Width="18px" CommandName="Del">
                                        <ControlStyle Width="18px" />
                                        <HeaderStyle Width="18px"></HeaderStyle>
                                    </asp:ButtonField>
                                </Columns>
                                <PagerStyle CssClass="fvPagerStyle" BackColor="White" HorizontalAlign="Center" />
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="clear"></div>
                </div>
            </div>
        </div>
    </div>
    <div id="modal-normal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">×</button>
                    <h4 class="modal-title">地區</h4>
                </div>
                <div class="modal-body" style="margin-bottom: 50px">
                    <div class="form-group">
                        <label class="col-sm-2 control-label" style="text-align: right; padding-right: 0px; padding-top: 10px;"><span style="color: #ff0000">*</span>地區:</label>
                        <div class="col-sm-8">
                            <input class="form-control" id="Country_detail" type="text" maxlength="50" runat="server" />
                        </div>
                        <div class="col-sm-2"></div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="Enter" runat="server" Text="確定" class="btn btn-info" OnClick="Enter_Click" />
                    <asp:Button ID="Close" runat="server" Text="關閉" CssClass="btn btn-danger" OnClick="Close_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

