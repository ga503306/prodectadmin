<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/masterpage2.master" AutoEventWireup="true" CodeFile="Dealers.aspx.cs" Inherits="Dealers" %>

<%@ Register Src="~/Pagination.ascx" TagPrefix="uc1" TagName="Pagination" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="css/pagination.css" />
    <style>
        .img {
            max-height: 161px;
            max-width: 210px;
            width: 100%;
            height: 100%;
            margin: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--遮罩-->
    <div class="bannermasks">
        <img src="images/DEALERS.jpg" alt="&quot;&quot;" width="967" height="381" />
    </div>
    <!--遮罩結束-->
    <!--------------------------------換圖開始---------------------------------------------------->

    <div class="banner">
        <ul>
            <li>
                <img src="images/newbanner.jpg" alt="Tayana Yachts" /></li>
        </ul>

    </div>
    <!--------------------------------換圖結束---------------------------------------------------->
    <div class="conbg">
        <!--------------------------------左邊選單開始---------------------------------------------------->
        <div class="left">

            <div class="left1">
                <p><span>DEALERS</span></p>
                <ul>
                    <asp:Repeater ID="Rpt_Region" runat="server" OnItemCommand="Rpt_region_ItemCommand">
                        <ItemTemplate>
                            <li>
                                <asp:LinkButton ID="btn_Region" runat="server" CommandName="Edit" CommandArgument='<%# Eval("R_no") + "," +Eval("Region") %>'><span> <%# Eval("Region") %> </span></asp:LinkButton></li>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%--                    <li><a href="#">Unite States</a></li>
                    <li><a href="#">Europe</a></li>
                    <li><a href="#">Asia</a></li>--%>
                </ul>
            </div>
        </div>
        <!--------------------------------左邊選單結束---------------------------------------------------->

        <!--------------------------------右邊選單開始---------------------------------------------------->
        <div id="crumb"><a href="Default.aspx">Home</a> >> <a href="#">Dealers </a>>> <a href="#"><span id="title_nav" runat="server" class="on1"></span></a></div>
        <div class="right">
            <div class="right1">
                <div class="title"><span id="title" runat="server"></span></div>

                <!--------------------------------內容開始---------------------------------------------------->
                <div class="box2_list">
                    <ul>
                        <asp:Repeater ID="Rpt_Detail" runat="server">
                            <ItemTemplate>
                                <li>
                                    <div class="list02">
                                        <ul>
                                            <li class="list02li">
                                                <div>
                                                    <p>
                                                        <img class="img" src='<%# string.IsNullOrEmpty(Eval("Img").ToString()) ? "images/noimage.png" : "/sqlimages/dealers/" + Eval("D_no")+ "/" + Eval("Img") + "?" + DateTime.Now.ToString("yyyyMMddHHmmss")%>' />>
                                                    </p>
                                                </div>
                                            </li>
                                            <li class="list02li02"><span><%# Eval("Country") %></span><br />
                                                <%# Eval("Info") %></li>
                                        </ul>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                        <%--                        <li>
                            <div class="list02">
                                <ul>
                                    <li class="list02li">
                                        <div>
                                            <p>
                                                <img src="images/dealers002.jpg" alt="&quot;&quot;" />
                                            </p>
                                        </div>
                                    </li>
                                    <li class="list02li02"><span>San Francisco</span><br />
                                        Pacific Yacht Imports<br />
                                        Contact：Mr. Neil Weinberg<br />
                                        Address：Grand Marina 2051 Grand Street# 12 Alameda, CA 94501, USA<br />
                                        TEL：(510)865-2541<br />
                                        FAX：(510)865-2369<br />

                                        <a href="http://www.pacificyachtimports.net" target="_blank">www.pacificyachtimports.net</a></li>
                                </ul>
                            </div>
                        </li>

                        <li>
                            <div class="list02">
                                <ul>
                                    <li class="list02li">
                                        <div>
                                            <img src="images/dealers003.jpg" alt="&quot;&quot;" />
                                        </div>
                                    </li>
                                    <li class="list02li02"><span>Seattle</span><br />
                                        Seattle Yachts<br />
                                        Contact：Ted Griffin<br />
                                        Address：Shilshole Bay Marina 7001 Seaview Ave NW, Suite 150 Seattle
                                        <br />
                                        WA 98117<br />
                                        TEL：(206.789.8044<br />
                                        FAX：(206.789.3976<br />
                                        Cell：(206.819.7137<br />
                                        E-mail：ted@seattleyachts.com<br />
                                        <a href="http://www.seattleyachts.com" target="_blank">www.seattleyachts.com</a><br />
                                    </li>
                                </ul>
                            </div>
                        </li>


                        <li>
                            <div class="list02">
                                <ul>
                                    <li class="list02li">
                                        <div>
                                            <img src="images/dealers004.jpg" alt="&quot;&quot;" />
                                        </div>
                                    </li>
                                    <li class="list02li02"><span>Seattle</span><br />
                                        Seattle Yachts<br />
                                        Contact：Ted Griffin<br />
                                        Address：Shilshole Bay Marina 7001 Seaview Ave NW, Suite 150 Seattle
                                        <br />
                                        WA 98117<br />
                                        TEL：(206.789.8044<br />
                                        FAX：(206.789.3976<br />
                                        Cell：(206.819.7137<br />
                                        E-mail：ted@seattleyachts.com<br />
                                        <a href="http://www.seattleyachts.com" target="_blank">www.seattleyachts.com</a><br />
                                    </li>
                                </ul>
                            </div>
                        </li>--%>
                    </ul>

                    <%--   <div class="pagenumber">| <span>1</span> | <a href="#">2</a> | <a href="#">3</a> | <a href="#">4</a> | <a href="#">5</a> |  <a href="#">Next</a>  <a href="#">LastPage</a></div>
                    <div class="pagenumber1">Items：<span>89</span>  |  Pages：<span>1/9</span></div>--%>
                </div>
                <uc1:Pagination runat="server" ID="Pagination1" />
                <!--------------------------------內容結束------------------------------------------------------>
            </div>
        </div>

        <!--------------------------------右邊選單結束---------------------------------------------------->
    </div>
</asp:Content>

