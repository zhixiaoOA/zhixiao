//用户登录
function UesrLogin() {
    var loginName = $("#LoginName").val();
    var loginPwd = $("#loginPwd").val();
    var checkCode = $("#checkCode").val();
    if (loginName == null || loginName == "" || loginName == undefined) {
        new Dialog('请输入登录账号!', { time: 2000 }).show();
        return;
    }
    if (loginPwd == null || loginPwd == "" || loginPwd == undefined) {
        
        new Dialog('请输入密码!', { time: 2000 }).show();
        return;
    }
    if (checkCode == null || checkCode == "" || checkCode == undefined) {
        //alert("请输入验证码");
        new Dialog('请输入验证码!', { time: 2000 }).show();
        return;
    }
 
    var url = '/User/ulogin';
    $.post(url, { 'loginName': loginName, 'loginPwd': loginPwd, 'checkCode': checkCode }, function (data) {
        if (data == -2) {
            
            new Dialog('验证码有证，请重新输入!', { time: 2000 }).show();
            return;
        }
        $.each(data, function (index, item) {
            if (item.UserStatus == 0) {                
                var results = getCookieValue('userName');
                window.location.href = "/User/Index";
            } else if (item.UserStatus == 999) {
              
                new Dialog('用户名或密码有误!', { time: 2000 }).show();
            }
            else if (item.UserStatus == 10) {              
                new Dialog('用户已经被冻结，请联系客户!', { time: 2000 }).show();
            }
            else if (item.UserStatus == 20)
            {
                new Dialog('用户已经被删除，请联系客户!', { time: 2000 }).show();
            }
        });
    });
}

//用户手机登录
function loginPhone() {
    var loginUserPhone = $("#LogUserPhone").val();
    var checkCode = $("#userCode").val();
    if (loginUserPhone == null || loginUserPhone == "" || loginUserPhone == undefined) {

        new Dialog('请输入手机号!', { time: 2000 }).show();
        return;
    }
    if (userCode == null || userCode == "" || userCode == undefined) {
        new Dialog('请输入验证码!', { time: 2000 }).show();
        return;
    }

    var url = '/User/phoneLogin';
    $.post(url, { 'loginUserPhone': loginUserPhone,'checkCode': checkCode }, function (data) {
        if (data == -2) {
            new Dialog('验证码有证，请重新输入!', { time: 2000 }).show();
            return;
        }
        $.each(data, function (index, item) {
            if (item.UserStatus == 0) {
                var results = getCookieValue('userName');
                window.location.href = "/User/Index";
            } else if (item.UserStatus == null && item.UserNumber == null && item.UserPhone == null) {

                new Dialog('用户名或密码有误!', { time: 2000 }).show();
            }
            else if (item.UserStatus == 10) {
                new Dialog('用户已经被冻结，请联系客户!', { time: 2000 }).show();
            }
        });
    });
}

function divUesrLogin(colseobj) {
    var loginName = $("#LoginName").val();
    var loginPwd = $("#loginPwd").val();
    var checkCode = $("#checkCode").val();
    if (loginName == null || loginName == "" || loginName == undefined) {
        new Dialog('请输入登录账号!', { time: 2000 }).show();
        return;
    }
    if (loginPwd == null || loginPwd == "" || loginPwd == undefined) {

        new Dialog('请输入密码!', { time: 2000 }).show();
        return;
    }
    if (checkCode == null || checkCode == "" || checkCode == undefined) {
        //alert("请输入验证码");
        new Dialog('请输入验证码!', { time: 2000 }).show();
        return;
    }

    var url = '/User/ulogin';
    $.post(url, { 'loginName': loginName, 'loginPwd': loginPwd, 'checkCode': checkCode }, function (data) {
        if (data == -2) {

            new Dialog('验证码有证，请重新输入!', { time: 2000 }).show();
            return;
        }
        $.each(data, function (index, item) {
            if (item.UserStatus == 0) {
                closediv(colseobj);
            } else if (item.UserStatus == 999) {

                new Dialog('用户名或密码有误!', { time: 2000 }).show();
            }
            else if (item.UserStatus == 10) {
                new Dialog('用户已经被冻结，请联系客户!', { time: 2000 }).show();
            }
            else if (item.UserStatus == 20) {
                new Dialog('用户已经被删除，请联系客户!', { time: 2000 }).show();
            }
        });
    });
}
//用户手机登录
function divloginPhone(colseobj) {
    var loginUserPhone = $("#LogUserPhone").val();
    var checkCode = $("#userCode").val();
    if (loginUserPhone == null || loginUserPhone == "" || loginUserPhone == undefined) {

        new Dialog('请输入手机号!', { time: 2000 }).show();
        return;
    }
    if (userCode == null || userCode == "" || userCode == undefined) {
        new Dialog('请输入验证码!', { time: 2000 }).show();
        return;
    }

    var url = '/User/phoneLogin';
    $.post(url, { 'loginUserPhone': loginUserPhone, 'checkCode': checkCode }, function (data) {
        if (data == -2) {
            new Dialog('验证码有证，请重新输入!', { time: 2000 }).show();
            return;
        }
        $.each(data, function (index, item) {
            if (item.UserStatus == 0) {
                closediv(colseobj);
            } else if (item.UserStatus == null && item.UserNumber == null && item.UserPhone == null) {

                new Dialog('用户名或密码有误!', { time: 2000 }).show();
            }
            else if (item.UserStatus == 10) {
                new Dialog('用户已经被冻结，请联系客户!', { time: 2000 }).show();
            }
        });
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

//用户通过手机登录
var InterValObj; //timer变量，控制时间
var count = 60; //间隔函数，1秒执行
var curCount; //当前剩余秒数
var code = ""; //验证码
var codeLength = 6; //验证码长度
function sendMessage() {

    var userPhone = $("#LogUserPhone").val();
    if (userPhone == "" || userPhone == null || userPhone == undefined) {
        new Dialog('请输入手机号!', { time: 2000 }).show();; return;
    }
    var url = "/User/UserLoginMsgToMobile";
    curCount = count;
    //设置button效果，开始计时
    $("#btnSendCode").attr("disabled", "true");
    $("#btnSendCode").val(curCount + "后重新发送");
    InterValObj = window.setInterval(SetRemainTime, 1000); //启动计时器，1秒执行一次
    //向后台发送处理数据
    $.post(url, { 'userPhone': userPhone }, function (data) {
        if (data == false) {
            //$("#labMessage1").html('<font color="red">短信发送失败</font>');
            new Dialog('短信发送失败!', { time: 2000 }).show();
            curCount = 0;
        }
        if (data == true) {
            //$("#labMessage1").html('<font color="red">短信发送成功</font>');
            new Dialog('短信发送成功!', { time: 2000 }).show();
        }
    });
}
//timer处理函数
function SetRemainTime() {
    if (curCount == 0) {
        window.clearInterval(InterValObj); //停止计时器
        $("#btnSendCode").removeAttr("disabled"); //启用按钮
        $("#btnSendCode").val("重新发送验证码");
        code = ""; //清除验证码。如果不清除，过时间后，输入收到的验证码依然有效
    }
    else {
        curCount--;
        $("#btnSendCode").val(curCount + "后重新发送");
    }
}


function ShowOrHideLoginPan(obj, showid) {
    $(obj).addClass('cur');
    $(obj).siblings().removeClass('cur');
    $(showid).show();
    $(showid).addClass('cur');
    $(showid).siblings().hide();
    $(showid).siblings().removeClass('cur');
}