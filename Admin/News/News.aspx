<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="News.aspx.cs" Inherits="Admin_News_News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>最新消息</title>
    <style type="text/css">
        .hiddencol {
            display: none;
        }

        .viscol {
            display: block;
        }
    </style>
    <script>
        $(function () {
            if ($('#<%=Grid_News.ClientID%>')[0].rows.length > 2)
                $('#<%=Grid_News.ClientID%>').DataTable();
        });
        function pageLoad() {
            if ($('#<%=Grid_News.ClientID%>')[0].rows.length > 2)
                $('#<%=Grid_News.ClientID%>').DataTable();
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-title-box">
        <h4 id="head_title" class="page-title">最新消息</h4>
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
            <asp:GridView ID="Grid_News" runat="server" class="table table-bordered" Width="100%" AutoGenerateColumns="False" OnRowCommand="Grid_News_RowCommand" EmptyDataText="尚無資料" ShowHeaderWhenEmpty="True" OnPreRender="Grid_News_PreRender" OnRowDataBound="Grid_News_RowDataBound">
                <Columns>
                    <asp:ButtonField ButtonType="Image" ImageUrl="~/images/icon/list.png" HeaderStyle-Width="18px" CommandName="Edit">
                        <ControlStyle Width="18px" />
                        <HeaderStyle Width="18px"></HeaderStyle>
                    </asp:ButtonField>
                    <asp:BoundField DataField="Newsno" HeaderText="流水號" HeaderStyle-Width="90px" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                        <HeaderStyle Width="90px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Title" HeaderText="標題"></asp:BoundField>
                    <asp:BoundField DataField="Info" HeaderText="簡介"></asp:BoundField>
                    <asp:BoundField DataField="Priority" HeaderText="是否置頂" HeaderStyle-Width="90px">
                        <HeaderStyle Width="90px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Inday" HeaderText="日期" HeaderStyle-Width="90px" DataFormatString="{0:yyyy/MM/dd}">
                        <HeaderStyle Width="90px" />
                    </asp:BoundField>
                    <asp:TemplateField ShowHeader="False" HeaderStyle-Width="18px">
                        <ItemTemplate>
                            <asp:ImageButton ID="Button2" ImageUrl="~/images/icon/刪除紅.png" runat="server" CausesValidation="False" CommandName="Del" Text="刪除" OnClientClick="return Del_check(this);" Width="18px" CommandArgument='<%#Container.DataItemIndex%>'/>
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
        </div>
    </div>
</asp:Content>

