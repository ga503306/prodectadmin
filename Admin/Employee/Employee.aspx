<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="Employee.aspx.cs" Inherits="Admin_Employee_Employee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>員工帳號管理</title>
    <script>
        $(function () {
            if ($('#<%=Grid_Employee.ClientID%>')[0].rows.length > 2)
                $('#<%=Grid_Employee.ClientID%>').DataTable();
        });
        function pageLoad() {
            if ($('#<%=Grid_Employee.ClientID%>')[0].rows.length > 2)
                   $('#<%=Grid_Employee.ClientID%>').DataTable();
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-title-box">
        <h4 id="head_title" class="page-title">帳號</h4>
        <div class="clearfix"></div>
    </div>

    <div class="white-box list2">
        <div class="col-lg-3 m-t-5 p-l-0 p-r-0">
            <div class="col-lg-12">
                <a class="btn btn-info" onclick="edit('ins','')" style="display:none"><i class="fa fa fa-plus"></i>&nbsp;<span>新增</span>&nbsp;</a>
                <asp:Button ID="ins" runat="server" Text="Button" OnClick="ins_Click" Style="display: none" />
            </div>
        </div>
        <div class="clear"></div>
        <div class="col-lg-9 text-right m-t-5  p-l-0 p-r-0"></div>
        <div class="clear"></div>
        <div class="col-lg-12">
            <asp:GridView ID="Grid_Employee" runat="server" class="table table-bordered" Width="100%" AutoGenerateColumns="False" OnRowCommand="Grid_Employee_RowCommand" EmptyDataText="尚無資料" ShowHeaderWhenEmpty="True" OnPreRender="Grid_Employee_PreRender" OnRowDataBound="Grid_Employee_RowDataBound" >
                <Columns>
                    <asp:ButtonField ButtonType="Image" ImageUrl="~/images/icon/list.png" HeaderStyle-Width="18px" CommandName="Edit">
                        <ControlStyle Width="18px" />
                        <HeaderStyle Width="18px"></HeaderStyle>
                    </asp:ButtonField>
                    <asp:BoundField DataField="Username" HeaderText="帳號"  HeaderStyle-Width="90px">
                        <HeaderStyle Width="90px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Group_name" HeaderText="權限"  HeaderStyle-Width="90px">
                        <HeaderStyle Width="90px" />
                    </asp:BoundField>
                    <asp:ButtonField ButtonType="Image" ImageUrl="~/images/icon/刪除紅.png" HeaderStyle-Width="18px" CommandName="Del">
                        <ControlStyle Width="18px" />
                        <HeaderStyle Width="18px"></HeaderStyle>
                    </asp:ButtonField>
                </Columns>
                <EmptyDataRowStyle HorizontalAlign="Center" />
                <PagerStyle CssClass="fvPagerStyle" BackColor="White" HorizontalAlign="Center" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>


