<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/masterpage3.master" AutoEventWireup="true" CodeFile="News_view.aspx.cs" Inherits="News_view" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <style>
        .img img{
            max-height: 525px;
            max-width: 700px;
            width: 100%;
            height: 100%;
            margin: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="id" runat="server" />
    <!--------------------------------右邊選單開始---------------------------------------------------->
    <div id="crumb"><a href="News.aspx">Home</a> >> <a href="News.aspx">News </a>>> <a href="#"><span class="on1">News & Events</span></a></div>
    <div class="right">
        <div class="right1">
            <div class="title"><span>News & Events</span></div>

            <!--------------------------------內容開始---------------------------------------------------->
            <div class="img" >
                <asp:Repeater ID="Rpt_News" runat="server">
                    <ItemTemplate>
                       <%# Eval("Context") %>
                        </ItemTemplate>
                </asp:Repeater>
            </div>


            <div class="buttom001">
                <a href="javascript:window.history.back();">
                    <img src="images/back.gif" alt="&quot;&quot;" width="55" height="28" /></a>
            </div>

            <!--------------------------------內容結束------------------------------------------------------>
        </div>
    </div>

    <!--------------------------------右邊選單結束---------------------------------------------------->

</asp:Content>

