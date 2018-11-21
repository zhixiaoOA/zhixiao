$(function () {
    $(".divMenu li").click(function () {
        $(".divMenu").hide();
        window.location = $(this).attr("href");
    });
    $(".divsmenu").click(function () {
        $(this).hide();
        $(".divMenu").show();
    });
    $(".divsmenu").mouseover(function () {
        isCkHide = false;
    }).mouseout(function () {
        isCkHide = true;
    });
    $(".contents").click(function () {
        if (isCkHide) {
            $(".divMenu").hide();
            $(".divsmenu").show();
        }
    });
});
var isCkHide = false;
//获取单据明细
function OnGetOrderDetail(hid, isHistory) {
    $(".mask").show();
    $.post("/MobileReport/GetOrderDetail", { hid: hid, isHistory: isHistory }, function (json) {
        $("#tbodyDetail").html("");
        if (json != null) {
            $("#abk").show();
            $("#tbhead").hide();
            $("#tbDetail").show();
            $(".pager").hide();
            $(json).each(function (i, item) {
                var tr = "<tr><td>" + item.dishName + "</td><td>" + item.unit + "</td><td>" + item.qty + "</td><td>" + item.sellPrice + "</td><td>" + item.sumMoney + "</td></tr>";
                $("#tbodyDetail").append(tr);
            });
        }
        $(".mask").hide();
    }, "json");
}
//返回
function OnBk() {
    $("#abk").hide();
    $("#tbhead").show();
    $("#tbDetail").hide();
    $(".pager").show();
}
//获取时间差-天数
function addDate(dd, dadd) {
    var a = new Date(dd)
    a = a.valueOf()
    a = a + dadd * 24 * 60 * 60 * 1000
    a = new Date(a)
    return a;
}