<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="Dealers.aspx.cs" Inherits="Admin_Dealers_Dealers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>國家別,地區管理</title>
    <script>
        $(function () {
            if ($('#<%=Grid_Dealers.ClientID%>')[0].rows.length > 2)
                $('#<%=Grid_Dealers.ClientID%>').DataTable();
        });
        function pageLoad() {
            if ($('#<%=Grid_Dealers.ClientID%>')[0].rows.length > 2)
                $('#<%=Grid_Dealers.ClientID%>').DataTable();
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-title-box">
        <h4 id="head_title" class="page-title">國家別,地區管理</h4>
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
            <asp:GridView ID="Grid_Dealers" runat="server" class="table table-bordered" Width="100%" AutoGenerateColumns="False" OnRowCommand="Grid_Dealers_RowCommand" EmptyDataText="尚無資料" ShowHeaderWhenEmpty="True" OnPreRender="Grid_Dealers_PreRender">
                <Columns>
                    <asp:ButtonField ButtonType="Image" ImageUrl="~/images/icon/list.png" HeaderStyle-Width="18px" CommandName="Edit">
                        <ControlStyle Width="18px" />
                        <HeaderStyle Width="18px"></HeaderStyle>
                    </asp:ButtonField>
                    <asp:BoundField DataField="R_no" HeaderText="流水號" HeaderStyle-Width="90px" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                        <HeaderStyle Width="90px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Region" HeaderText="國家"></asp:BoundField>
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
        </div>
    </div>
</asp:Content>

