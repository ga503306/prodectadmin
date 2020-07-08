<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="Auth_edit.aspx.cs" Inherits="Admin_Auth_Auth_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>權限管理</title>
    <script>
        $(function () {

           
        });

        //初始化
        function Init() {
            var G_no = { id: getUrlParameter("id") };
            $.ajax({
                type: "POST",
                url: "../../api/auth.aspx/init",
                data: JSON.stringify({ 'G_no': G_no }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    loading(true);
                },
                complete: function () {
                    loading(false);
                },
                success: function (data) {
                    var arr = JSON.parse(data.d);
                    if (arr.Type == "成功") {
                        arr.detail.forEach(function (item, index, array) {
                            $('input[id="' + item.Group_view + '"]').prop("checked", true)
                        });
                        //console.log(arr.detail);
                    }
                    else
                        swal("失敗");
                }
            });
        }

        //全部啟用按鈕
        function btn_Save(state) {
            var postData = new Array();
            $('input[name="Grid_checkbox"]:checked').each(function () {
                postData.push({
                    id: $(this).attr("id")
                });
            });
            var G_no = { id: getUrlParameter("id") };
            $.ajax({
                type: "POST",
                url: "../../api/auth.aspx/Save",
                data: JSON.stringify({ 'postData': postData, 'G_no': G_no }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    loading(true);
                },
                complete: function () {
                    loading(false);
                },
                success: function (data) {
                    var arr = JSON.parse(data.d);
                    if (arr.Type == "成功") {
                        swal("儲存成功")
                    }
                    else
                        swal("失敗");
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-title-box">
        <h4 id="head_title" class="page-title">權限管理</h4>
        <div class="clearfix"></div>
    </div>
    <div class="row">
        <div id="view" class="col-md-12 col-sm-9">
            <div class="white-box list2">
                <div class="col-lg-5 m-t-5 p-l-0 p-r-0">
                    <div class="col-lg-12">
                        <a class="btn btn-info" href="Auth.aspx?type=auth"><i class="ti-arrow-left"></i>&nbsp;<span>回列表</span>&nbsp;</a>
                        <a class="btn btn-info" onclick="save()"><i class="ti-save"></i>&nbsp;<span>儲存</span>&nbsp;</a>
                        <a id="del_btn" class="btn btn-danger" onclick="data_del('')" runat="server"><i class="fa fa-times"></i>&nbsp;<span>刪除</span>&nbsp;</a>
                    </div>
                </div>

                <div class="clear"></div>
                <asp:HiddenField runat="server" ID="id" />
                <div class="col-lg-12 m-t-5">
                    <div>
                        <asp:Button ID="save" runat="server" Text="Button" OnClick="save_Check" Style="display: none" />
                        <%--<asp:Button ID="del" runat="server" Text="刪除" OnClick="del_Click" Style="display: none" />--%>
                    </div>
                    <div>
                        <h2 class="header-title">權限管理</h2>
                        <hr />
                    </div>
                </div>

                <div class="clear"></div>

                <div class="col-lg-12">
                    <asp:GridView ID="Grid_Auth" runat="server" class="table table-bordered" Width="100%" AutoGenerateColumns="False" EmptyDataText="尚無資料" ShowHeaderWhenEmpty="True" OnPreRender="Grid_Auth_PreRender" OnRowDataBound="Grid_Auth_RowDataBound" >
                        <Columns>
                            <asp:TemplateField ShowHeader="True">
                                <ItemTemplate>
                                    <div class="custom-control custom-checkbox">
                                        <asp:CheckBox ID="Choose"  name="Grid_checkbox" runat="server" />
                                        <%--<input type="checkbox" class="custom-control-input" id="<%#Eval("value")%>" name="Grid_checkbox" /><label class="custom-control-label" for="<%#Eval("value")%>"></label>--%>
                                    </div>
                                </ItemTemplate>
                                <ControlStyle Width="15px" />
                                <HeaderStyle Width="15px" />
                            </asp:TemplateField>
                             <asp:BoundField DataField="value" HeaderText="隱藏頁面英" >
                             
                            </asp:BoundField>
                            <asp:BoundField DataField="value" HeaderText="隱藏頁面" >
                             
                            </asp:BoundField>
                        </Columns>
                        <EmptyDataRowStyle HorizontalAlign="Center" />
                        <PagerStyle CssClass="fvPagerStyle" BackColor="White" HorizontalAlign="Center" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

