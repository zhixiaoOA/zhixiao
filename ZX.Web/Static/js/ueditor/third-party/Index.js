$(function () {
    CheckUserloginStatus();
});
function CheckUserloginStatus() {
    var url = '/User/CheckUserLogin';
    $.post(url, {}, function (data) {
        if (data != false) {
            $.each(data, function (index, item) {
                var topHtml = '您好 <a href="/User/Index">' +item.UserPhone + '</a>!欢迎来到找果农网！';
                topHtml += '[<a href="/User/uloginOut">退出</a>]';
                $("#topuserinfo").html(topHtml);
            });
        }
    });
       

    }

    //获取cookie  
    function getCookieValue(cookieName) {
        var cookieValue = document.cookie;
        var cookieStartAt = cookieValue.indexOf("" + cookieName + "=");
        if (cookieStartAt == -1) {
            cookieStartAt = cookieValue.indexOf(cookieName + "=");
        }
        if (cookieStartAt == -1) {
            cookieValue = null;
        }
        else {
            cookieStartAt = cookieValue.indexOf("=", cookieStartAt) + 1;
            cookieEndAt = cookieValue.indexOf(";", cookieStartAt);
            if (cookieEndAt == -1) {
                cookieEndAt = cookieValue.length;
            }
            cookieValue = unescape(cookieValue.substring(cookieStartAt, cookieEndAt));//解码latin-1  
        }
        return cookieValue;
    }

    function userLoginOut()
    {
        var url = '/User/uloginOut';
        $.post(url, {}, function () { });
    }