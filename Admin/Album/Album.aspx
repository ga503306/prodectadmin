<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="Album.aspx.cs" Inherits="Admin_Album_Album" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>電子相簿</title>
    <script>
        $(function () {
            if ($('#<%=Grid_Yachts.ClientID%>')[0].rows.length > 2)
                $('#<%=Grid_Yachts.ClientID%>').DataTable();
        });
        function pageLoad() {
            if ($('#<%=Grid_Yachts.ClientID%>')[0].rows.length > 2)
                $('#<%=Grid_Yachts.ClientID%>').DataTable();
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-title-box">
        <h4 id="head_title" class="page-title">電子相簿</h4>
        <div class="clearfix"></div>
    </div>
      <div class="white-box list2">
        <div class="clear"></div>
        <div class="col-lg-9 text-right m-t-5  p-l-0 p-r-0"></div>
        <div class="clear"></div>
        <div class="col-lg-12">
            <asp:GridView ID="Grid_Yachts" runat="server" class="table table-bordered" Width="100%" AutoGenerateColumns="False" OnRowCommand="Grid_Yachts_RowCommand" EmptyDataText="尚無資料" ShowHeaderWhenEmpty="True" OnPreRender="Grid_Yachts_PreRender">
                <Columns>
                    <asp:ButtonField ButtonType="Image" ImageUrl="~/images/icon/list.png" HeaderStyle-Width="18px" CommandName="Edit">
                        <ControlStyle Width="18px" />
                        <HeaderStyle Width="18px"></HeaderStyle>
                    </asp:ButtonField>
                    <asp:BoundField DataField="Yachtsno" HeaderText="流水號" HeaderStyle-Width="90px" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                        <HeaderStyle Width="90px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Modal" HeaderText="遊艇型" HeaderStyle-Width="90px">
                        <HeaderStyle Width="90px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Modal_n" HeaderText="遊艇型號" HeaderStyle-Width="90px">
                        <HeaderStyle Width="90px" />
                    </asp:BoundField>
                </Columns>
                <EmptyDataRowStyle HorizontalAlign="Center" />
                <PagerStyle CssClass="fvPagerStyle" BackColor="White" HorizontalAlign="Center" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>

