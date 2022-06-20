$(function () {

    $("#UserRegistrationModal input[name = 'Email']").blur(function () {

        var email = $("#UserRegistrationModal input[name = 'Email']").val();

        console.log(email);

        var url = "UserAuth/UserNameExists?userName=" + email;

        $.ajax({
            type: "GET",
            url: url,
            success: function (data) {
                if (data == true) {
                    //PresentClosableBootstrapAlert("#alert_placeholder_register", "warning", "Invalid Email", "This email address has already been registered");
                    alert("This email address has already been registered")
                }
                else {
                   // CloseAlert("#alert_placeholder_register");
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                var errorText = "Status: " + xhr.status + " - " + xhr.statusText;

                //PresentClosableBootstrapAlert("#alert_placeholder_register", "danger", "Error!", errorText);

                console.error(thrownError + '\r\n' + xhr.statusText + '\r\n' + xhr.responseText);

            }
        });
    });

    var registerUserButton = $("#UserRegistrationModal button[name = 'register']").click(onUserRegisterClick);


    function onUserRegisterClick(){

        var url = "UserAuth/RegisterUser";

        var antiForgeryToken = $("#UserRegistrationModal input[name='__RequestVerificationToken']").val();
        var email = $("#UserRegistrationModal input[name='Email']").val();
        var name = $("#UserRegistrationModal input[name='Name']").val();
        var password = $("#UserRegistrationModal input[name='Password']").val();
        var confirmPassword = $("#UserRegistrationModal input[name='ConfirmPassword']").val();
        var phoneNumber = $("#UserRegistrationModal input[name='PhoneNumber']").val();
        var streetaddress = $("#UserRegistrationModal input[name='StreetAddress']").val();
        var city = $("#UserRegistrationModal input[name='City']").val();
        var postalcode = $("#UserRegistrationModal input[name='PostalCode']").val();
        var firstName = $("#UserRegistrationModal input[name='FirstName']").val();
        var lastName = $("#UserRegistrationModal input[name='LastName']").val();

        var user = {
            __RequestVerificationToken: antiForgeryToken,
            Email: email,
            FirstName: firstName,
            LastName: lastName,
            Name: name,
            Password: password,
            ConfirmPassword: confirmPassword,
            City: city,
            PostalCode: postalcode,
            PhoneNumber: phoneNumber,
            StreetAddress: streetaddress
        }

        $.ajax({
            type: "POST",
            url: url,
            data: user,
            success: function (data)
            {
                var parsed = $.parseHTML(data);
                var hasErrors = $(parsed).find("input[name='LoginInValid']").val() == "true";

                if (hasErrors) {

                    $("#UserRegistrationModal").html(data);
                    var registerUserButton = $("#UserRegistrationModal button[name = 'register']").click(onUserRegisterClick);
                    $("#UserRegistrationModal input[name = 'AcceptUserAgreement']").click(onAcceptUserAgreementClick);

                    $("#UserRegistrationForm").removeData("validator");
                    $("#UserRegistrationForm").removeData("unobtrusiveValidation");
                    $.validator.unobtrusive.parse("#UserRegistrationForm");
                }
                else
                {
                    location.href = '/Home/UserDashboard';
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                var errorText = "Status: " + xhr.status + " - " + xhr.statusText;

                console.log(errorText);

                // PresentClosableBootstrapAlert("#alert_placeholder_login", "danger", "Error!", errorText);

                console.error(thrownError + "\r\n" + xhr.statusText + "\r\n" + xhr.responseText);
            }
        })
    }

})