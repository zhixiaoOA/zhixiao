/* *
 * 请求连接生成
 * c:模块名称，b：function名称， d：是否为系统controller，a:页面是否为html后缀
 * */
function createLink(c, b, d, a) {
    if (!a) {
        a = config.defaultView
    }
    if (d) {
        d = d.split("&");
        for (i = 0; i < d.length; i++) {
            d[i] = d[i].split("=")
        }
    }
    appName = config.appName;
    router = config.router;
    if (c.indexOf(".") >= 0) {
        moduleNames = c.split(".");
        appName = moduleNames[0];
        c = moduleNames[1];
        router = router.replace(config.appName, appName)
    }
    if (config.requestType == "PATH_INFO") {
        link = config.webRoot + appName + "/" + c + config.requestFix + b;
        if (d) {
            if (config.pathType == "full") {
                for (i = 0; i < d.length; i++) {
                    link += config.requestFix + d[i][0] + config.requestFix + d[i][1]
                }
            } else {
                for (i = 0; i < d.length; i++) {
                    link += config.requestFix + d[i][1]
                }
            }
        }
        link += "." + a
    } else {
        link = router + "?" + config.moduleVar + "=" + c + "&" + config.methodVar + "=" + b + "&" + config.viewVar + "=" + a;
        if (d) {
            for (i = 0; i < d.length; i++) {
                link += "&" + d[i][0] + "=" + d[i][1]
            }
        }
    }
    return link
}