var dataSet = [
];


$(function () {
   
    $(".select2").select2();
   
    $("#txtPassword").keydown(function (e)
    {
        if (e.keyCode == 13 || e.keyCode == 27)
        {
            $("#btnLogin").click();
        }
    });

    $("#txtUserName").keydown(function (e) {
        if (e.keyCode == 13 || e.keyCode == 27)
        {
            $("#btnLogin").click();
        }
    });

    $("#btnLogin").click(function ()
    {       
        LoginHelper.LogInData();
    });

    $("#txtUserName").click(function ()
    {
        $("#lblMessage").val("");
    });
});


var LoginHelper = {
    LogInData: function () {
        var UserName = $("#txtUserName").val();
        var Password = $("#txtPassword").val();

        var json = { UserName: UserName, Password: Password };
        jQuery.ajax({
            type: "POST",
            url: "/LogIn/LogIn",
            data: json,
            success: function (data) {
                if (data === true) {
                    //window.history.back();
                    //window.history.go(-1);
                    window.location.href = '/TarVsAch/TarVsAch';
                } else {
                    //alert('Chosma');
                    $("#lblMessage").html("Invalid Username or Password");
                }

            }
        });
    }
};

