function handleLogin() {
    var email = $("#txtEmail").val();
    var password = $("#txtPassword").val();

    $.ajax({
        type: "POST",
        url: "/Login/UserLogin",
        data: JSON.stringify({ email: email, password: password }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.success) {
                alert("Login successful!");
                window.location.href = "/DashBoard/Index";
            } else {
                alert("Invalid email or password. Please try again.");
            }
        },
        error: function (error) {
            alert("An error occurred while logging in. Please try again.");
        }
    });
}

function registerUser() {
    var name = $("#txtName").val();
    var email = $("#txtEmail").val();
    var password = $("#txtPassword").val();
    var userData = {
        Name: name,
        Email: email,
        Password: password
    };

    $.ajax({
        type: "POST",
        url: "/UserRegistration/CheckUniqueEmail",
        data: JSON.stringify({ email: email }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.isUnique) {
                $.ajax({
                    type: "POST",
                    url: "/UserRegistration/Register",
                    data: JSON.stringify(userData),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.success) {
                            alert("Registration successful!");
                            window.location.href = "/Login/Index";
                        } else {
                            alert("Registration failed. Please try again.");
                        }
                    },
                    error: function (error) {
                        alert("An error occurred. Please try again.");
                    }
                });
            } else {
                alert("The email address is already in use. Please enter a different email.");
            }
        },
        error: function (error) {
            alert("An error occurred while checking email uniqueness. Please try again.");
        }
    });
}