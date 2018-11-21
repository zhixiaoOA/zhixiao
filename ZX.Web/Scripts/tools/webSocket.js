var webSocket = null;
if (!window.WebSocket) {
    alert("你的浏览器不支持websocket，请升级到IE10以上浏览器，或者使用谷歌、火狐、360浏览器。");
}
webSocket = new WebSocket(wsServer);
webSocket.onerror = function (event) {
    alert("websockt连接发生错误，请刷新页面重试!")
};
// 连接成功建立的回调方法
webSocket.onopen = function (event) {
    webSocket.send("_online_user_" + currentId);
};
// 接收到消息的回调方法
webSocket.onmessage = function (event) {
    var res = event.data;

    if (res.indexOf("_online_all_status_") >= 0) {
        var dd = res.substring("_online_all_status_".length);
    }
};

//发送消息的方法
function send(mine, To) {
    webSocket.send(currentId + "_msg_" + To.id + "_msg_" + mine.content + "_msg_" + mine.avatar + "_msg_" + To.type + "_msg_" + currentName + "_msg_NAN");
};
//切换在线状态的方法
function setonline() {
    webSocket.send("_online_user_" + currentId);
};
//切换离线状态的方法
function sethide() {
    webSocket.send("_leave_user_" + currentId);
};