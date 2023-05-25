"use strict"

$("#timer").hide();
$(".await-code").attr("disabled", "disabled");
$("#loginMessage").attr("hidden", "hidden");

$("#codeBtn").click(function () {
    const input = $("#input").val();

    if (input) {
        var duration = 60 * 2,
            display = document.querySelector('#timer');
        startTimer(duration, display, input);

        $("#codeBtn").hide();
        $("#timer").show();
        $(".request-code").attr("disabled", "disabled");
        $(".await-code").removeAttr("disabled");
        $("#loginMessage").attr("hidden", "hidden");

        requestCode(input)

        //var data = {
        //    Mobile: input
        //};
        //var json = JSON.stringify(data);

        //$.ajax({
        //    type: "POST",
        //    url: "Account/RequestCode",
        //    data: json,
        //    contentType: "application/json",
        //    dataType: "json",
        //    success: function () {
        //        alert("asdasd");
        //    },
        //    error: function () {
        //    }
        //});
    }
    else {
        $("#loginMessage").removeAttr("hidden");
        return;
    }
});

var Interval;
function modifyInput() {
    $("#timer").hide();
    $("#codeBtn").show();
    $(".await-code").attr("disabled", "disabled");
    $(".request-code").removeAttr("disabled");
    $("#loginMessage").attr("hidden", "hidden");
    clearInterval(Interval);
}

function startTimer(duration, display, input) {
    var timer = duration, minutes, seconds;
    Interval = setInterval(function () {
        minutes = parseInt(timer / 60, 10);
        seconds = parseInt(timer % 60, 10);

        minutes = minutes < 10 ? "0" + minutes : minutes;
        seconds = seconds < 10 ? "0" + seconds : seconds;

        display.textContent = minutes + ":" + seconds;

        if (--timer < 0) {
            $.ajax({
                url: `/Account/ForgotPassword`,
                type: "GET",
            });
            modifyInput();
            clearInterval(Interval);
        }
    }, 1000);
}

const requestCode = function (mobile) {

    const currentUrl = window.location.href;
    const newUrl = currentUrl.split("Account")[0];
    //const url = newUrl + `Account/RequestCode/${mobile}`
    const url = newUrl + `Account/RequestCode`
    $.get(url,
        { mobile: mobile });

    //const data = {
    //    mobile: mobile
    //};

    //const json = JSON.stringify(data);

    //$.ajax({
    //    type: "GET",
    //    async: false,
    //    url: newUrl,
    //    //data: json,
    //    //contentType: "application/json",
    //    //dataType: "json",
    //    success: function () {
    //    },
    //    error: function (data) {
    //        alert("b");
    //    }
    //});
}