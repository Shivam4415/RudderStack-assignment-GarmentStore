
Registration = new function () {

    var modal,
        btnRegister;

    var _data = {};

    var init = function () {
        modal = "#registrationModal",
        btnRegister = "#btnSignUp";
        registerButton = "#registerButton";
        name = "#name";
        email = "#email";
        company = "#company";
        password = "#password";
        confirmPassword = "#confirmpassword";
        labelErrorMessage = "#labelErrorMessage";
        errorMessage = "#errorMessage";
        infoIcon = "#info";

        //_bindControls();
        $(btnRegister).on('click', _show);

    };

    var _show = function () {
        UIkit.modal($(modal)).show();
    };

    var _getUserData = function () {
        _data.Name = $(name).val();
        _data.Email = $(email).val();
        _data.Phone = $(phone).val();
        _data.Password = $(password).val();
        _data.ConfirmPassword = $(confirmPassword).val();
        _data.Company = $(company).val();
    };

    var _validatePasswordOnBlur = function () {
        if (!_isValidPassword()) {
            $(password).addClass('uk-form-danger');
            $(infoIcon).attr('aria-expanded', 'true');
            //$(labelErrorMessage).removeAttr('hidden');
            //$(errorMessage).text('Invalid password combination. Please provide correct password');
        }
    };


    var _register = function () {
        _getUserData();

        if (!_isValidEmail()) {
            $(labelErrorMessage).removeAttr('hidden');
            $(errorMessage).text('Invalid email combination. Please provide correct email');
            return;
        }

        $.when(_addUser()).then(function () {
            UIkit.modal($(modal)).hide();

            UIkit.notification({
                message: 'User Added Successfully. Please verify your email address to login',
                status: 'success',
                timeout: '5000',
                pos: 'top-center'
            });
        }).fail(function () {
            UIkit.notification({
                message: 'Failed to add user. Please try again',
                status: 'danger',
                timeout: '3000',
                pos: 'top-center'
            });
        });
    };

    var _isValidPassword = function () {
        const passwordRegex = /^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$/;

        return passwordRegex.test($(password).val());
    };


    var _matchConfirmPassword = function () {
        if (_data.Password === _data.ConfirmPassword)
            return true;
        return false;
    };

    var _isValidEmail = function () {
        const _email = $(email).val();
        if (_email === null || _email === undefined || !_email.length) {
            return false;
        }

        const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(_email.toLowerCase());

    };

    var _bindControls = function () {
        $(btnRegister).on('click', _show);
        $(registerButton).on('click', _register);
        $(email).on('blur', function () {
            if (!_isValidEmail()) {
                $(this).addClass('uk-form-danger');
            }
        });
        $(password).on('blur', _validatePasswordOnBlur);

        $(password).on('focus', function () {
            $(password).removeClass('uk-form-danger');
            $(infoIcon).attr('aria-expanded', 'false');
        });

        $(confirmPassword).on('blur', _matchConfirmPassword);

    };

    var _addUser = function () {
        var d = $.Deferred();
        $.ajax({
            url: 'api/v1/users',
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            data: JSON.stringify(_data),
            success: function (data, textStatus, xhr) {
                d.resolve(data);
            },
            fail: function () {
                d.reject(false);
            }
        });
        return d.promise();
    };


    //Making Function Public
    return {
        Start: init
    };
};

window.onload = function () {

    var url = window.location.toString();
    Registration.Start();
};



