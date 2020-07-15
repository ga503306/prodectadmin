<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/masterpage3.master" AutoEventWireup="true" CodeFile="News.aspx.cs" Inherits="News" %>

<%@ Register Src="~/Pagination.ascx" TagPrefix="uc1" TagName="Pagination" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .img {
            max-height: 161px;
            max-width: 187px;
            width: 100%;
            height: 100%;
            margin: 0px;
        }
    </style>
    <script>
        function get_url(id) {
            document.location.href = "News_view.aspx?id=" + id;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="crumb"><a href="Default.aspx">Home</a> >> <a href="News.aspx">News </a>>> <a href="#"><span class="on1">News & Events</span></a></div>
    <div class="right">
        <div class="right1">
            <div class="title"><span>News & Events</span></div>
            <!--------------------------------內容開始---------------------------------------------------->
            <div class="box2_list">
                <ul>
                    <asp:Repeater ID="Rpt_News" runat="server">
                        <ItemTemplate>
                            <li>
                                <div class="list01">
                                    <ul>
                                        <li>
                                            <div>
                                                <p>
                                                    <img class="img" src='<%# string.IsNullOrEmpty(Eval("Img").ToString()) ? "images/noimage.png" : "/sqlimages/News/" + Eval("Newsno")+ "/" + Eval("Img") + "?" + DateTime.Now.ToString("yyyyMMddHHmmss")%>' alt="&quot;&quot;" />
                                                </p>
                                            </div>
                                        </li>
                                        <li>
                                            <span><%# String.Format("{0:yyyy/MM/dd}",Eval("Inday"))  %></span><br />
                                            <a onclick='get_url(<%# Eval("Newsno") %>);' href="#"><%# Eval("Title") %></a></td>
                                        </li>
                                        <br>
                                        <li><%# Eval("Info") %></li>
                                    </ul>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%--                    <li>
                        <div class="list01">
                            <ul>
                                <li>
                                    <div>
                                        <p>
                                            <img src="images/pit006.jpg" alt="&quot;&quot;" />
                                        </p>
                                    </div>
                                </li>
                                <li>
                                    <span>2012-01-28</span><br />
                                    Tayana 58 CE Certificates are availableTayana 58 CE Certificates are availableTayana 58 CE Certificates are availableTayana 58 CE Certificates are availableTayana 58 CE Certificates are available
                                </li>
                                <li>availableTayana 58 CE Certificates are availableTayana 58 CE Certificates are availableTayana 58 CE Certificates are available</li>
                            </ul>
                        </div>
                    </li>--%>
                </ul>
                <uc1:Pagination runat="server" ID="Pagination1" />
              <%--  <div class="pagenumber">| <span>1</span> | <a href="#">2</a> | <a href="#">3</a> | <a href="#">4</a> | <a href="#">5</a> |  <a href="#">Next</a>  <a href="#">LastPage</a></div>
                <div class="pagenumber1">Items：<span>89</span>  |  Pages：<span>1/9</span></div>--%>


            </div>


            <!--------------------------------內容結束------------------------------------------------------>
        </div>
    </div>
</asp:Content>

