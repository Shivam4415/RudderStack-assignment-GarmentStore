
//Initially Invoked Function Expression
M.Page.LearningWebform = (function () {


    function init() {

        //Call a handler when an click event occurs..
        $("#btnSubmit").on('click', buttonClickEvent);
    };


    function buttonClickEvent() {

        var fn = document.getElementById("firstName").value;
        var clsFn = document.getElementsByClassName("get-first-name").value;
        var jqFn = $("#firstName").val();

        var sn = document.getElementById("middleName").value;

        var ln = document.getElementById("lastName").value;

        var dob = document.getElementById("DOB").value;
        var gen = document.getElementById("Gender").value;
        var mail = document.getElementById("enterYourMailtype").value;

        var userObject = {};
        userObject.Name = fn + " " + ln;
        userObject.Email = mail;
        userObject.Gender = gen;
        userObject.Password = dob;
        document.getElementById("data").append(jqFn + " " + ln);
        console.log(fn + " " + ln);
        saveUser(fn + " " + ln);

    };


    function saveUser(fullName) {

        $.ajax({
            url: 'api/v1/users',
            method: 'POST',
            data: JSON.stringify(fullName),
            contentType: 'application/json',
            dataType: 'json',
            success: function () {

            },
            fail: function () {

            }
        });

    }

    //Expose the function
    return {
        init: init
    };

})(); 




window.onload = function () {
    var url = window.location.toString();
    M.Page.LearningWebform.init();
};