<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LearningWebform.aspx.cs" Inherits="Document.App.LearningWebform" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Kya Maaal Hai</title>
    <script src="https://code.jquery.com/jquery-3.5.1.js" integrity="sha256-QWo7LDvxbWT2tbbQ97B53yJnYU3WhH/C8ycbRAkjPDc=" crossorigin="anonymous"></script>

    <!-- UIkit CSS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/uikit@3.5.9/dist/css/uikit.css" />

    <!-- UIkit JS -->
    <script src="https://cdn.jsdelivr.net/npm/uikit@3.5.9/dist/js/uikit.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/uikit@3.5.9/dist/js/uikit-icons.js"></script>

    <script src="./Js/Editor.js"></script>
    <script src="./Js/page/Page.LearningWebform.js"></script>


</head>


<body>

    <div>
        <b>First Name:</b><br>
        <input id="firstName" type="text" class="get-first-name uk-input uk-width-1-6"></input><br>
        <b>Middle Name:</b><br>
        <input id="middleName" type="text" class="uk-input uk-width-1-6"></input><br>
        <b>Last Name:</b><br>
        <input id="lastName" type="text" class="uk-input uk-width-1-6"></input><br>

        <b>DOB</b><br>
        <input id="DOB" type="date" name="days"><br>
        <br>

        <b>Gender:</b><br>
        <select id="Gender">
            <option value="male"><b>Male</b></option>
            <option value="female"><b>Female</b></option>
        </select><br>
        <br>


        <b>Enter your mail<b><br>
            <input id="enterYourMailtype" type="text" name="" size="25">

            <div id="data"></div>
            <input id="btnSubmit" type="button" value="Submit" class="uk-margin-small-top uk-button uk-button-secondary uk-text-uppercase"></input>
    </div>
</body>
</html>

