<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="keywords" content="" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>登入</title>
    <link href="css/icons.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
    <link href="css/responsive.css" rel="stylesheet" />
    <link href="plugins/sweetalert/sweetalert.css" rel="stylesheet" />

    <!--Begin core plugin -->
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="plugins/sweetalert/sweetalert-dev.js"></script>
    <!-- End core plugin -->

    <style>
        .body_type {
            margin: 0px;
            padding: 0px;
            background: #fff center center fixed no-repeat;
            background-image: url(images/Background.png);
            background-size: 100% 100%;
            /**/
        }

        @media screen and (min-width: 1080px) {
            .aa {
                margin: 300px 0px 100px;
            }
        }
    </style>
    <script>
        function alert_swal() {
            swal({
                title: "<span style='font-family:Microsoft JhengHei;'>帳號或密碼錯誤</span>",
                text: "<span style='font-family:Microsoft JhengHei; font-weight:bolder;'>帳號或密碼輸入錯誤，請重新輸入。</span>",
                html: true
            })
        }
    </script>
</head>
<body class="body_type">
    <form id="form1" runat="server">
        <!--Start login Section-->
        <section class="login-section">
            <div class="container">
                <div class="row">
                    <div class="login-wrapper">
                        <div class="login-inner aa">

                            <div class="logo m-b-30">
                                <img src="images/LOGO.png" alt="logo" />
                            </div>
                            <br />
                            <br />
                            <div class="form-group">
                                <input id="Username" runat="server" type="text" class="form-control" placeholder="Username" style="border-radius: 30px;" />
                                <br />
                            </div>
                            <div class="form-group">
                                <input id="Password" runat="server" type="password" class="form-control" placeholder="Password" style="border-radius: 30px;" />
                                <br />
                                <br />
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btn_login" runat="server" OnClick="btn_login_Click" class="btn btn-block" Style="background-color: #6AE1C1; color: #fdfeff; border-radius: 30px;" Text="登入" Font-Bold="True" />
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </section>
        <!--End login Section-->
    </form>
</body>


</html>
