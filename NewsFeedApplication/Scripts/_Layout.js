$(document).ready(function () {
    $('#logoutLink').click(function (e) {
        e.preventDefault();
        $.ajax({
            type: "POST",
            url: "/Login/Logout", 
            success: function () {
                alert('Logout success');
                window.location.href = "/Login/Index";
            },
            error: function () {
                alert("An error occurred while logging out. Please try again.");
            }
        });
    });
});

