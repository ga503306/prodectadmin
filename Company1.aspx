<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/masterpage2.master" AutoEventWireup="true" CodeFile="Company1.aspx.cs" Inherits="Company1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--遮罩-->
    <div class="bannermasks">
        <img src="images/company.jpg" alt="&quot;&quot;" width="967" height="371" />
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
                <p><span>COMPANY </span></p>
                <ul>
                    <li><a href="Company.aspx">About Us</a></li>
                    <li><a href="Company1.aspx">Certificat</a></li>
                </ul>
            </div>
        </div>
        <!--------------------------------左邊選單結束---------------------------------------------------->

        <!--------------------------------右邊選單開始---------------------------------------------------->
        <div id="crumb"><a href="#">Home</a> >> <a href="#">Company  </a>>> <a href="#"><span class="on1">About Us</span></a></div>
        <div class="right">
            <div class="right1">
                <div class="title"><span>About Us</span></div>

                <!--------------------------------內容開始---------------------------------------------------->
                <div class="box3">
                    Tayana Yacht has the approval of ISO9001: 2000 quality certification by Bureau Veritas Certification (Taiwan) Co., Ltd in 2002. In August, 2011, formally upgraded to ISO9001: 2008. We will continue to adhere to quality-oriented, transparent and committed to delivering improvement customer satisfaction and build even stronger trusting relationships with customers.
                    <br />
                    <div class="pit">
                        <ul>
                            <li>
                                <p>
                                    <img src="../images/certificat001.jpg" alt="Tayana " /></p>
                            </li>
                            <li>
                                <p>
                                    <img src="images/certificat002.jpg" alt="Tayana " /></p>
                            </li>
                            <li>
                                <p>
                                    <img src="images/certificat003.jpg" alt="Tayana " /></p>
                            </li>
                            <li>
                                <p>
                                    <img src="images/certificat004.jpg" alt="Tayana " /></p>
                            </li>
                            <li>
                                <p>
                                    <img src="images/certificat005.jpg" alt="Tayana " /></p>
                            </li>
                            <li>
                                <p>
                                    <img src="images/certificat006.jpg" alt="Tayana " /></p>
                            </li>
                            <li>
                                <p>
                                    <img src="images/certificat007.jpg" alt="Tayana " width="319" height="234" /></p>
                            </li>
                            <li>
                                <p>
                                    <img src="images/certificat008.jpg" alt="Tayana " width="319" height="234" /></p>
                            </li>
                        </ul>
                    </div>
                </div>
                <!--------------------------------內容結束------------------------------------------------------>
            </div>
        </div>

        <!--------------------------------右邊選單結束---------------------------------------------------->
    </div>
</asp:Content>

