<%@ Page Language="c#" CodeBehind="Logon.aspx.cs" AutoEventWireup="false" Inherits="DM.UBP.PBIRS.Security.Logon, DM.UBP.PBIRS.Security"
    Culture="auto" UICulture="auto" %>

<!DOCTYPE HTML>
<html>
<head>
    <title>江西五十铃Power BI</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1.0" name="viewport">

    <style type="text/css">
        body {
            display: block;
            background-color: #3d3d3d;
            color: #333333;
            font-family: "Open Sans", sans-serif;
            padding: 0px !important;
            margin: 0px !important;
            direction: "ltr";
            font-size: 14px;
        }

        .login {
            background-color: #364150 !important;
        }

            .login .logo {
                margin: 0 auto;
                margin-top: 60px;
                padding: 15px;
                text-align: center;
                font-size: 40px;
                font-weight: 400 !important;
                font-weight: bold;
                color: red;
            }

            .login .content {
                background-color: #eceef1;
                -webkit-border-radius: 7px;
                -moz-border-radius: 7px;
                -ms-border-radius: 7px;
                -o-border-radius: 7px;
                border-radius: 7px;
                width: 400px;
                margin: 40px auto 10px auto;
                padding: 30px;
                padding-top: 10px;
                overflow: hidden;
                position: relative;
            }

                .login .content .form-title {
                    font-weight: 300;
                    margin-bottom: 25px;
                }

                .login .content h3 {
                    color: #4db3a5;
                    text-align: center;
                    font-size: 28px;
                    font-weight: 400 !important;
                }

        .form-group {
            margin-bottom: 15px;
        }

        .has-error, .help-block {
            color: #e73d4a;
        }

        .control-label {
            margin-top: 1px;
            font-weight: normal;
        }

        *, :after, :before {
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            box-sizing: border-box;
        }

        .visible-ie9 {
            display: none;
        }

        label {
            display: inline-block;
            margin-bottom: 5px;
        }

        .img-responsive, .img-thumbnail, .table, label {
            max-width: 100%;
        }

        .login .content .form-control {
            border: none;
            background-color: #dde3ec;
            height: 43px;
            color: #8290a3;
            border: 1px solid #dde3ec;
        }

        .form-control {
            outline: none !important;
            box-shadow: none !important;
            width: 100%;
            padding: 6px 12px;
            border-radius: 4px;
            transition: border-color； font-size: 14px;
            line-height: 1.42857;
            display: block;
            background-image: none;
        }

        button, input {
            font-family: inherit;
            font: inherit;
            margin: 0;
            text-rendering: auto;
            letter-spacing: normal;
            word-spacing: normal;
            text-transform: none;
            text-indent: 0px;
            text-shadow: none;
            text-align: start;
            -webkit-writing-mode: horizontal-tb !important;
        }

        input {
            -webkit-rtl-ordering: logical;
            cursor: text;
        }

        .help-block {
            margin-top: 5px;
            margin-bottom: 5px;
            display: block;
        }

        input[type="password" i] {
            -webkit-text-security: disc !important;
        }

        .login .content .form-actions {
            clear: both;
            border: 0px;
            border-bottom: 1px solid #eee;
            padding: 0px 30px 25px 30px;
            margin-left: -30px;
            margin-right: -30px;
        }

            .login .content .form-actions .btn-success {
                font-weight: 600;
                padding: 10px 20px !important;
            }

            .login .content .form-actions .btn {
                margin-top: 1px;
            }

        .btn:not(.md-skip):not(.bs-select-all):not(.bs-deselect-all) {
            font-size: 12px;
            transition: box-shadow 0.28s cubic-bezier(0.4, 0, 0.2, 1);
            border-radius: 2px;
            overflow: hidden;
            position: relative;
            user-select: none;
            box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1), 0 1px 2px rgba(0, 0, 0, 0.18);
        }

        .btn-success {
            color: #fff;
            background-color: #36c6d3;
            border-color: #2bb8c4;
            border: 1px solid transparent;
            vertical-align: middle;
        }

            .btn-success.active, .btn-success:active, .btn-success:hover, .open > .btn-success.dropdown-toggle {
                color: #fff;
                background-color: #27a4b0;
                border-color: #208992;
            }

        .btn {
            display: inline-block;
            margin-bottom: 0;
            text-align: center;
            vertical-align: middle;
            touch-action: manipulation;
            cursor: pointer;
            border: 1px solid transparent;
            white-space: nowrap;
        }

        .control-message {
            color: red;
            text-align: center;
            font-size: 16px;
            font-weight: 400 !important;
            margin-top: 1px;
            font-weight: normal;
        }
    </style>
</head>

<body class="login">
    <div class="logo">
        江西五十铃 Power BI系统
    </div>
    <div class="content">
        <h3 class="form-title">登录</h3>
        <form id="Form1" method="post" runat="server" class="login-form">
            <div class="form-group">
                <!--ie8, ie9 does not support html5 placeholder, so we just show field title for that-->
                <label class="control-label visible-ie8 visible-ie9">用户名</label>
                <asp:TextBox ID="txtUserName" runat="server" class="form-control form-control-solid placeholder-no-fix input-ltr" placeholder="用户名" name="username" value="" required="" aria-required="true">
                </asp:TextBox>
            </div>
            <div class="form-group">
                <label class="control-label visible-ie8 visible-ie9">密码</label>
                <asp:TextBox ID="txtPwd" runat="server" class="form-control form-control-solid placeholder-no-fix input-ltr" type="password" autocomplete="off" placeholder="密码" name="password">
                </asp:TextBox>
            </div>
            <div class="form-actions">
                <asp:Button ID="btnLogin" runat="server" type="submit" class="btn btn-success uppercase" Text="登录" OnClick="btnLogin_Click"></asp:Button>
                <br />
                <br />
                <asp:Label ID="lblMessage" runat="server" Text="" class="control-message"></asp:Label>
            </div>
        </form>
    </div>
</body>
</html>
