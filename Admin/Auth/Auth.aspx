<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="Auth.aspx.cs" Inherits="Admin_Auth_Auth" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>權限群組管理</title>
    <script>
        $(function () {
            if ($('#<%=Grid_Auth.ClientID%>')[0].rows.length > 2)
                $('#<%=Grid_Auth.ClientID%>').DataTable();
        });
        function pageLoad() {
            if ($('#<%=Grid_Auth.ClientID%>')[0].rows.length > 2)
                $('#<%=Grid_Auth.ClientID%>').DataTable();
        };
    </script>

    <style type="text/css">
        .hiddencol {
            display: none;
        }

        .viscol {
            display: block;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-title-box">
        <h4 id="head_title" class="page-title">權限群組管理</h4>
        <div class="clearfix"></div>
    </div>

    <div class="white-box list2">
        <div class="col-lg-3 m-t-5 p-l-0 p-r-0">
            <div class="col-lg-12">
                <a class="btn btn-info" onclick="edit('ins','')"><i class="fa fa fa-plus"></i>&nbsp;<span>新增</span>&nbsp;</a>
                <asp:Button ID="ins" runat="server" Text="Button" OnClick="ins_Click" Style="display: none" />
            </div>
        </div>
        <div class="clear"></div>
        <div class="col-lg-9 text-right m-t-5  p-l-0 p-r-0"></div>
        <div class="clear"></div>
        <div class="col-lg-12">
            <asp:ScriptManager runat="server"></asp:ScriptManager>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:GridView ID="Grid_Auth" runat="server" class="table table-bordered" Width="100%" AutoGenerateColumns="False" OnRowCommand="Grid_Auth_RowCommand" EmptyDataText="尚無資料" ShowHeaderWhenEmpty="True" OnPreRender="Grid_Auth_PreRender">
                        <Columns>
                            <asp:ButtonField ButtonType="Image" ImageUrl="~/images/icon/list.png" HeaderStyle-Width="18px" CommandName="Edit">
                                <ControlStyle Width="18px" />
                                <HeaderStyle Width="18px"></HeaderStyle>
                            </asp:ButtonField>
                            <asp:BoundField DataField="G_no" HeaderText="群組流水" HeaderStyle-Width="90px" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                <HeaderStyle Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Group_name" HeaderText="群組名稱"></asp:BoundField>
                            <%--<asp:BoundField DataField="Auth" HeaderText="權限"  HeaderStyle-Width="90px">
                        <HeaderStyle Width="90px" />
                    </asp:BoundField>--%>
                            <asp:TemplateField ShowHeader="False" HeaderStyle-Width="18px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="Button2" ImageUrl="~/images/icon/刪除紅.png" runat="server" CausesValidation="False" CommandName="Del" Text="刪除" OnClientClick="return Del_check(this);" Width="18px" CommandArgument='<%#Container.DataItemIndex%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:ButtonField ButtonType="Image" ImageUrl="~/images/icon/刪除紅.png" HeaderStyle-Width="18px" CommandName="Del">
                                <ControlStyle Width="18px" />
                                <HeaderStyle Width="18px"></HeaderStyle>
                            </asp:ButtonField>--%>
                        </Columns>
                        <EmptyDataRowStyle HorizontalAlign="Center" />
                        <PagerStyle CssClass="fvPagerStyle" BackColor="White" HorizontalAlign="Center" />
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Enter" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="ins" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <!--群組名稱-->
    <div id="modal-ins" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">×</button>
                    <h4 class="modal-title">群組名稱</h4>
                </div>
                <div class="modal-body" style="margin-bottom: 10%;">
                    <div class="form-group">
                        <label class="col-sm-2 control-label" style="width: 20%; text-align: right; padding-right: 0px; padding-top: 10px;">群組名稱:</label>
                        <div class="col-sm-8">
                            <div class="input-group" style="width: 100%;">
                                <input class="form-control" id="Group_name" type="text" maxlength="15" runat="server" />
                            </div>
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
    <!---->
</asp:Content>

