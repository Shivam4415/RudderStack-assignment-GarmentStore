<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Document.App.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="./Js/uikit/uikit.theme.css" rel="stylesheet" />
    <script src="./Js/uikit/uikit.js"></script>
    <script src="./Js/jquery/jquery-3.4.1.min.js"></script>
    <script src="./Js/uikit/uikit-icons.js"></script>
</head>
<body class="uk-background-default">
    <form id="mainForm" runat="server" autopostback="false">
        <div id="registerPage" class="uk-position-relative uk-overflow-auto">
            <div class="uk-text-center uk-padding-small uk-text-large">
                Registration
            </div>
            <div id="registerForm" class="uk-position-relative uk-text-center uk-position-top-center">

                <div class="uk-width-1-1">
                    <div class="uk-margin"> 
                        <input id="name" runat="server" class="uk-input uk-width-medium" placeholder="Name">
                    </div>

                    <div class="uk-margin"> 
                        <input id="email" runat="server" class="uk-input uk-width-medium" placeholder="Email">
                    </div>
                    <div class="uk-margin"> 
                        <input id="company" runat="server" class="uk-input uk-width-medium" placeholder="Company">
                    </div>
                    <div class="uk-margin"> 
                        <input id="phone" runat="server" class="uk-input uk-width-medium" placeholder="Phone Number">
                    </div>
                    <div class="uk-margin"> 
                        <input id="password" runat="server" class="uk-input uk-width-medium" placeholder="Password">
                    </div>
                    <div class="uk-margin"> 
                        <input id="confirmpassword" runat="server" class="uk-input uk-width-medium" placeholder="Confirm Password">
                    </div>
                    <div id="labelDiv" runat="server" class="uk-margin uk-alert uk-text-danger" hidden> 
                        <label id="errorMessage" runat="server" class="uk-danger"></label>
                    </div>
                </div>

                <button type="button" class="uk-button uk-button-primary" runat="server" onserverclick="button_register">Register</button>

                <div class="uk-position-center" hidden uk-spinner></div>
            </div>
        </div>
    </form>
</body>
</html>
