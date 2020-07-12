<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FindDealersR.aspx.cs" Inherits="Admin_FindDealersR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/StyleSheet2.css" rel="stylesheet" />
    <title>國家別</title>
    <style>
        .padding {
            padding-left: 0;
            padding-right: 0;
        }
    </style>
    <script>
        function openA(controla, valuea, controlb, valueb) {

            window.opener.window.document.getElementById(controla).value = valuea;
            window.opener.window.document.getElementById(controlb).value = valueb;
            window.opener.window.document.getElementById(controlb).focus();
            window.opener.$("#ContentPlaceHolder1_Region").val(valueb).change();
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container" style="margin-top: 10px;">
            <div class="col-lg-12 form-horizontal">
                <div class="form-group">
                    <div class="col-xs-2 padding" style="padding-top: 6px;">
                        <asp:Label ID="Label1" runat="server" Text="流水號:" CssClass="control-label"></asp:Label>
                    </div>
                    <div class="col-xs-6 padding">
                        <asp:TextBox ID="ser1" runat="server" CssClass="col-xs-6 form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-2 padding" style="padding-top: 6px;">
                        <asp:Label ID="Label2" runat="server" Text="國家別:" CssClass="control-label"></asp:Label>
                    </div>
                    <div class="col-xs-6 padding">
                        <asp:TextBox ID="ser2" runat="server" CssClass="col-xs-6 form-control"></asp:TextBox>
                    </div>
                    <div class="col-xs-2">
                        <asp:Button ID="btnQuery" runat="server" Text="查詢" OnClick="btnQuery_Click" class="btn btn-info" />
                    </div>
                    <div class="col-xs-2">
                        <asp:Button ID="btnClose" runat="server" Text="關閉" OnClick="btnClose_Click" class="btn btn-secondary" />
                    </div>

                </div>
            </div>
            <div class="col-lg-12" style="min-height: 20px;"></div>
            <div class="col-lg-12">
                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" Width="100%" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" EmptyDataText="尚無資料" ShowHeaderWhenEmpty="True" OnRowCommand="GridView1_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="R_no" HeaderText="國家別流水號" HeaderStyle-Width="100px">
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Region" HeaderText="國家別"></asp:BoundField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="select" ImageUrl="~/images/icon/list.png"
                                    CommandArgument='<%#Eval("R_no") + "," + Eval("Region") %>' />
                            </ItemTemplate>
                            <ControlStyle Width="18px" />
                            <HeaderStyle Width="18px" />
                        </asp:TemplateField>
                        <%--<asp:ButtonField ButtonType="Image" ImageUrl="~/images/icon/list.png" HeaderStyle-Width="18px" CommandName="select">
                            <ControlStyle Width="18px" />
                            <HeaderStyle Width="18px"></HeaderStyle>
                        </asp:ButtonField>--%>
                    </Columns>
                    <PagerStyle CssClass="fvPagerStyle" BackColor="White" HorizontalAlign="Center" />
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
