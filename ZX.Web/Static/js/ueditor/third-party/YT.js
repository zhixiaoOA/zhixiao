
var YT = {
    BaseCommon: {
        gL: function (x) { var l = 0; while (x) { l += x.offsetLeft; x = x.offsetParent; } return l },
        gT: function (x) { var t = 0; while (x) { t += x.offsetTop; x = x.offsetParent; } return t }
    },
    
    Fun: {
        /*格式化时间字符串*/
        formatDate: function (now, types) {
            if (now != null && now != "") {
                var dateN = new Date(+/\d+/.exec(now)[0]);
                var year = dateN.getFullYear();
                var month = dateN.getMonth() + 1;
                var date = dateN.getDate();
                var hour = dateN.getHours();
                var minute = dateN.getMinutes();
                var second = dateN.getSeconds();
                if (typeof (types) != "undefined" && types != null) {
                    return year + "-" + month + "-" + date;
                }
                else if (hour == 0 && minute == 0 && second == 0) {
                    return year + "-" + month + "-" + date;
                }
                else {
                    return year + "-" + month + "-" + date + "   " + hour + ":" + minute + ":" + second;
                }
            }
            else {
                return "";
            }
        },
        formatDates:function(now, types) {
        if (now != null && now != "") {
            var dateN = new Date(+/\d+/.exec(now)[0]);
            var year = dateN.getFullYear();
            var month = dateN.getMonth() + 1;
            var date = dateN.getDate();
            var hour = dateN.getHours();
            var minute = dateN.getMinutes();
            var second = dateN.getSeconds();
            var mdate="";      
            if (typeof (types) != "undefined" && types != null) {

                mdate+= year + "-" + month + "-" + date;
            }
             if (hour != "" && minute != "" && second != "") {

                 mdate += " "+hour + ":" + minute + ":" + second;
            }           
        }
        else {
            return "";
        }
        return mdate;
    }
     
       
    },
    Dirt: {
        /*绑定到列表*/
       
        /*获取值表示的意义*/
       
    }
}

/*重新定义录入框校验规则*/


/*空函数*/
function CreateGrid() { }



function myformatter(date) {
    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    var d = date.getDate();

    return y + '-' + (m < 10 ? ('0' + m) : m) + '-'
+ (d < 10 ? ('0' + d) : d);
}
function myparser(s) {
    if (!s)
        return new Date();
    var ss = (s.split('-'));
    var y = parseInt(ss[0], 10);
    var m = parseInt(ss[1], 10);
    var d = parseInt(ss[2], 10);
    if (!isNaN(y) && !isNaN(m) && !isNaN(d)) {
        return new Date(y, m - 1, d);
    } else {
        return new Date();
    }
}

function windowCenteredfeed(obj, width, height) {

    $("#shadow").show();
    $(obj).show();
    $(obj).css('height', '0px').css('width', '0px').css('top', $(window).height() / 2 + 'px').css('left', $(window).width() / 2 + 'px').css('opacity', '0.1');
    ///$(obj).html("<div class=\"winwork\">" + title + "<i id='closeIframefeed' class=\"close\">×</i></div>" + content);

    $(obj).animate({ 'height': height + 'px', 'width': width + 'px', 'top': ($(window).height() - height) / 2 + 'px', 'left': ($(window).width() - width) / 2 + 'px', 'opacity': '1' }, "slow");
}
function closediv(obj) {
    $(obj).animate({ 'top': '50%', 'left': '50%', 'height': '0px', 'width': '0px', 'opacity': '0.1' }, function () { $(obj).hide(); $("#shadow").hide(); });

}

function divFadeAlert(StrMsg,timer) {
    var hidvalue_str = $('#hidvalue').val();
    var divWidth = 100;
    var divHeight = 30;
    var iLeft = ($(window).width() - divWidth) / 2;
    var iTop = ($(window).height() - divHeight) / 2 + $(document).scrollTop();
    var divhtml = $("<div style=\"background:#000; color:#FFF;text-align:center;border-radius:5px;line-height:25px;\">" + StrMsg + "</div>").css({ position: 'absolute', top: iTop + 'px', left: iLeft + 'px', display: 'none', width: divWidth + 'px', height: divHeight + 'px' });
    divhtml.appendTo('body').fadeIn();
    divhtml.appendTo('body').fadeOut(timer);
}


function logindiv(obj, width, height) {

    var url = '/User/_uloginPra';
    $.post(url, {}, function (data) {

        $("#shadow").show();
        $(obj).show();
        $(obj).css('height', '0px').css('width', '0px').css('top', $(window).height() / 2 + 'px').css('left', $(window).width() / 2 + 'px').css('opacity', '0.1');
        $("#contextHtml").html(data);

        $(obj).animate({ 'height': height + 'px', 'width': width + 'px', 'top': ($(window).height() - height) / 2 + 'px', 'left': ($(window).width() - width) / 2 + 'px', 'opacity': '1' }, "slow");
        $("#valiCode").bind("click", function () {
            this.src = "/User/GetValidateCode?time=" + (new Date()).getTime();
        });
    });
    
}


function CheckUserIsLogin() {
    var url = '/User/CheckUserLogin';
    $.post(url, {}, function (data) {
        if (data == false) {
            logindiv("#UserloginDIV", width, height)
        }
        else {

        }
    });


}

