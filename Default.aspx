﻿<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/masterpage4.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .imgnews {
            max-height: 95px;
            max-width: 95px;
            width: 100%;
            height: 100%;
            margin: 0px;
        }
        .imgship {
            max-height: 95px;
            max-width: 95px;
            width: 100%;
            height: 100%;
            margin: 0px;
        }
    </style>
    <script type="text/javascript">


        $(function () {

            // 先取得 #abgne-block-20110111 , 必要參數及輪播間隔
            var $block = $('#abgne-block-20110111'),
                timrt, speed = 4000;


            // 幫 #abgne-block-20110111 .title ul li 加上 hover() 事件
            var $li = $('.title ul li', $block).hover(function () {
                // 當滑鼠移上時加上 .over 樣式
                $(this).addClass('over').siblings('.over').removeClass('over');
            }, function () {
                // 當滑鼠移出時移除 .over 樣式
                $(this).removeClass('over');
            }).click(function () {
                // 當滑鼠點擊時, 顯示相對應的 div.info
                // 並加上 .on 樣式

                $(this).addClass('on').siblings('.on').removeClass('on');
                $('#abgne-block-20110111 .bd .banner ul:eq(0)').children().hide().eq($(this).index()).fadeIn(1000);
            });

            // 幫 $block 加上 hover() 事件
            $block.hover(function () {
                // 當滑鼠移上時停止計時器
                clearTimeout(timer);
            }, function () {
                // 當滑鼠移出時啟動計時器
                timer = setTimeout(move, speed);
            });
            //首次進入
            var _index = $('.title ul li.on', $block).index();
            $li.eq(0).click()
            // 控制輪播
            function move() {
                var _index = $('.title ul li.on', $block).index();
                _index = (_index + 1) % $li.length;
                $li.eq(_index).click();

                timer = setTimeout(move, speed);
            }

            // 啟動計時器
            timer = setTimeout(move, speed);

            var len = 25; // 超過兩行以"..."取代
            $(".JQellipsis").each(function (i) {
                if ($(this)[0].textContent.length > len) {
                    $(this).attr("title", $(this).text());
                    var text = $(this).text().substring(0, len - 1) + "...";
                    $(this).text(text);
                }
            });
        });

        function get_url(id) {
            document.location.href = "News_view.aspx?id=" + id;
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--遮罩-->
    <div class="bannermasks">
        <img src="images/banner00_masks.png" alt="&quot;&quot;" />
    </div>
    <!--遮罩結束-->

    <!--------------------------------換圖開始---------------------------------------------------->
    <div id="abgne-block-20110111">
        <div class="bd">
            <div class="banner">

                <ul>
                    <asp:Repeater ID="Rpt_shipw" runat="server">
                        <ItemTemplate>
                            <li class="info "><a href="#">
                                <img src='<%# string.IsNullOrEmpty(Eval("Img").ToString()) ? "images/noimage.png" : "/sqlimages/Yachts/" + Eval("Yachtsno")+ "/" + Eval("Img") + "?" + DateTime.Now.ToString("yyyyMMddHHmmss")%>' /></a><!--文字開始--><div class="wordtitle">
                                    <%# Eval("Modal") %> <span><%# Eval("Modal_n") %></span><br />
                                    <p>SPECIFICATION SHEET</p>
                                </div>
                                <!--文字結束-->
                                <!--新船型開始  54型才出現其於隱藏 -->
                                <div class="new" style="<%# isnew(Eval("Isnew").ToString()) %>">
                                    <img src="images/new01.png" alt="new" />
                                </div>
                                <!--新船型結束-->
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>

                    <%--  <li class="info"><a href="#">
                        <img src="images/banner002b.jpg" /></a><!--文字開始--><div class="wordtitle">
                            TAYANA <span>54</span><br />
                            <p>SPECIFICATION SHEET</p>
                        </div>
                        <!--文字結束-->
                        <!--新船型開始  54型才出現其於隱藏 -->
                        <div class="new">
                            <img src="images/new01.png" alt="new" />
                        </div>
                        <!--新船型結束-->
                    </li>
                    <li class="info"><a href="#">
                        <img src="images/banner003b.jpg" /></a><!--文字開始--><div class="wordtitle">
                            TAYANA <span>37</span><br />
                            <p>SPECIFICATION SHEET</p>
                        </div>
                        <!--文字結束-->
                    </li>
                    <li class="info"><a href="#">
                        <img src="images/banner004b.jpg" /></a><!--文字開始--><div class="wordtitle">
                            TAYANA <span>64</span><br />
                            <p>SPECIFICATION SHEET</p>
                        </div>
                        <!--文字結束-->
                    </li>
                    <li class="info"><a href="#">
                        <img src="images/banner005b.jpg" /></a><!--文字開始--><div class="wordtitle">
                            TAYANA <span>58</span><br />
                            <p>SPECIFICATION SHEET</p>
                        </div>
                        <!--文字結束-->
                    </li>
                    <li class="info"><a href="#">
                        <img src="images/banner006b.jpg" /></a><!--文字開始--><div class="wordtitle">
                            TAYANA <span>55</span><br />
                            <p>SPECIFICATION SHEET</p>
                        </div>
                        <!--文字結束-->
                    </li>--%>
                </ul>


                <!--小圖開始-->
                <div class="bannerimg title" style="display:none;">
                    <ul>
                        <asp:Repeater ID="Rpt_ships" runat="server">
                            <ItemTemplate>
                                <li class="on">
                                    <div>
                                        <p class="bannerimg_p">
                                            <img class="imgship" src='<%# string.IsNullOrEmpty(Eval("Img").ToString()) ? "images/noimage.png" : "/sqlimages/Yachts/" + Eval("Yachtsno")+ "/" + Eval("Img") + "?" + DateTime.Now.ToString("yyyyMMddHHmmss")%>' alt="&quot;&quot;" />
                                        </p>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                       <%-- <li>
                            <div>
                                <p class="bannerimg_p">
                                    <img src="images/i002.jpg" alt="&quot;&quot;" />
                                </p>
                            </div>
                        </li>
                        <li>
                            <div>
                                <p class="bannerimg_p">
                                    <img src="images/i003.jpg" alt="&quot;&quot;" />
                                </p>
                            </div>
                        </li>
                        <li>
                            <div>
                                <p class="bannerimg_p">
                                    <img src="images/i004.jpg" alt="&quot;&quot;" />
                                </p>
                            </div>
                        </li>
                        <li>
                            <div>
                                <p class="bannerimg_p">
                                    <img src="images/i005.jpg" alt="&quot;&quot;" />
                                </p>
                            </div>
                        </li>
                        <li>
                            <div>
                                <p class="bannerimg_p">
                                    <img src="images/i006.jpg" alt="&quot;&quot;" />
                                </p>
                            </div>
                        </li>--%>
                    </ul>
                </div>
                <!--小圖結束-->
            </div>
        </div>
    </div>
    <!--------------------------------換圖結束---------------------------------------------------->


    <!--------------------------------最新消息---------------------------------------------------->
    <div class="news">
        <div class="newstitle">
            <p class="newstitlep1">
                <img src="images/news.gif" alt="news" />
            </p>
            <p class="newstitlep2"><a href="News.aspx">More>></a></p>
        </div>

        <ul>
            <!--TOP第一則最新消息-->
            <asp:Repeater ID="Rpt_News" runat="server">
                <ItemTemplate>
                    <li>
                        <div class="news01">
                            <!--TOP標籤-->
                            <div class="newstop">
                                <img src="images/new_top01.png" alt="&quot;&quot;" />
                            </div>
                            <!--TOP標籤結束-->
                            <div class="news02p1">
                                <p class="news02p1img">
                                    <img class="imgnews" src='<%# string.IsNullOrEmpty(Eval("Img").ToString()) ? "images/noimage.png" : "/sqlimages/News/" + Eval("Newsno")+ "/" + Eval("Img") + "?" + DateTime.Now.ToString("yyyyMMddHHmmss")%>' alt="&quot;&quot;" />
                                </p>
                            </div>
                            <p class="news02p2"><span class="JQellipsis"><%# Eval("Title") %></span> <a onclick='get_url(<%# Eval("Newsno") %>);' href="#"><%# Eval("Info") %></a></p>
                        </div>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
            <!--TOP第一則最新消息結束-->

            <%--   <!--TOP第一則最新消息-->
            <li>
                <div class="news01">
                    <!--TOP標籤-->
                    <div class="newstop">
                        <img src="images/new_top01.png" alt="&quot;&quot;" />
                    </div>
                    <!--TOP標籤結束-->
                    <div class="news02p1">
                        <p class="news02p1img">
                            <img src="images/pit002.jpg" alt="&quot;&quot;" />
                        </p>
                    </div>
                    <p class="news02p2"><span>Tayana 54 CE Certifica..</span> <a href="#">For Tayana 54 entering the EU, CE Certificates are AVAILABLE to ensure conformity to all applicable European ...</a></p>
                </div>
            </li>
            <!--TOP第一則最新消息結束-->

            <!--第二則-->
            <li>

                <div class="news01">
                    <!--TOP標籤-->
                    <div class="newstop">
                        <img src="images/new_top01.png" alt="&quot;&quot;" />
                    </div>
                    <!--TOP標籤結束-->
                    <div class="news02p1">
                        <p class="news02p1img">
                            <img src="images/pit001.jpg" alt="&quot;&quot;" />
                        </p>
                    </div>
                    <p class="news02p2">
                        <span>Tayana 58 CE Certifica..</span>
                        <a href="#">For Tayana 58 entering the EU, CE Certificates are AVAILABLE to ensure conformity to all applicable European ...</a>
                    </p>
                </div>
            </li>
            <!--第二則結束-->

            <li>
                <div class="news02">
                    <!--TOP標籤-->
                    <div class="newstop">
                        <img src="images/new_top01.png" alt="&quot;&quot;" />
                    </div>
                    <!--TOP標籤結束-->
                    <div class="news02p1">
                        <p class="news02p1img">
                            <img src="images/pit001.jpg" alt="&quot;&quot;" />
                        </p>
                    </div>
                    <p class="news02p2">
                        <span>Big Cruiser in a Small ..</span>
                        <a href="#">Tayana 37 is our classical product and full of skilful craftsmanship. We only plan to build TWO units in a year.</a>
                    </p>
                </div>
            </li>--%>
        </ul>
    </div>
    <!--------------------------------最新消息結束---------------------------------------------------->

</asp:Content>

