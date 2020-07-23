<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="DealersDetail.aspx.cs" Inherits="Admin_DealersDetail_DealersDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        $(function () {
            if ($('#<%=Grid_DealerD.ClientID%>')[0].rows.length > 2)
                $('#<%=Grid_DealerD.ClientID%>').DataTable();
        });
        function pageLoad() {
            if ($('#<%=Grid_DealerD.ClientID%>')[0].rows.length > 2)
                $('#<%=Grid_DealerD.ClientID%>').DataTable();
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-title-box">
        <h4 id="head_title" class="page-title">經銷商管理</h4>
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
            <asp:GridView ID="Grid_DealerD" runat="server" class="table table-bordered" Width="100%" AutoGenerateColumns="False" OnRowCommand="Grid_DealerD_RowCommand" EmptyDataText="尚無資料" ShowHeaderWhenEmpty="True" OnPreRender="Grid_DealerD_PreRender">
                <Columns>
                    <asp:ButtonField ButtonType="Image" ImageUrl="~/images/icon/list.png" HeaderStyle-Width="18px" CommandName="Edit">
                        <ControlStyle Width="18px" />
                        <HeaderStyle Width="18px"></HeaderStyle>
                    </asp:ButtonField>
                    <asp:BoundField DataField="D_no" HeaderText="流水號" HeaderStyle-Width="90px" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                        <HeaderStyle Width="90px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="R_no" HeaderText="國家流水號" HeaderStyle-Width="90px" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                        <HeaderStyle Width="90px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Region" HeaderText="國家別" HeaderStyle-Width="90px">
                        <HeaderStyle Width="90px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Country" HeaderText="區域" HeaderStyle-Width="90px">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="C_no" HeaderText="地區流水號" HeaderStyle-Width="90px" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                        <HeaderStyle Width="90px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="縮圖">
                        <ItemTemplate>
                            <asp:ImageButton ID="Image" runat="server" CausesValidation="false" CommandName="Pic" ImageUrl='<%# "~/sqlimages/Dealers/" +Eval("D_no") + "/min_" + Eval("img") +"?" + DateTime.Now.ToString("yyyyMMddHHmmss")%>' Style="max-width: 160px; max-height: 90px;" />
                        </ItemTemplate>
                        <%--<ControlStyle Width="160px" />--%>
                        <%--<ControlStyle Height="90px" />--%>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Info" HeaderText="簡介" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"></asp:BoundField>
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

