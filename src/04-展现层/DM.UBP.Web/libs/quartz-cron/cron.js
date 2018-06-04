/**
 * 每周期
 */
function everyTime(dom) {
    var name = dom.name;
    dayClash(name);
    var val = "*";
    var item = $("input[name=v_" + name + "]");
    item.val(val).change();
};

/**
 * 不指定
 */
function unAppoint(dom) {
    var name = dom.name;
    var val = "?";
    var item = $("input[name=v_" + name + "]");
    item.val(val).change();
}


/**
 * 周期
 */
function cycle(dom) {
    var name = dom.name;
    dayClash(name);
    var ns = $(dom).parent().parent().children("input[type='number']");
    var start = ns[0].value;
    var end = ns[1].value;
    var item = $("input[name=v_" + name + "]");
    item.val(start + "-" + end).change();
}


/**
 * 从开始
 */
function startOn(dom) {
    var name = dom.name;
    dayClash(name);
    var ns = $(dom).parent().parent().children("input[type='number']");
    var start = ns[0].value;
    var end = ns[1].value;
    var item = $("input[name=v_" + name + "]");
    item.val(start + "/" + end).change();
}

/**
 * 最后一天
 */
function lastDay(dom) {
    var name = dom.name;
    dayClash(name);
    var item = $("input[name=v_" + name + "]");
    item.val("L").change();
}

/**
 * 第几周的星期几
 */
function weekOfDay(dom) {
    var name = dom.name;
    dayClash(name);
    var ns = $(dom).parent().parent().children("input[type='number']");
    var start = ns[0].value;
    var end = ns[1].value;
    var item = $("input[name=v_" + name + "]");
    item.val(start + "#" + end).change();
}


/**
 * 本月最后一个星期几
 */
function lastWeek(dom) {
    var name = dom.name;
    dayClash(name);
    var ns = $(dom).parent().parent().children("input[type='number']");
    var start = ns[0].value;
    var item = $("input[name=v_" + name + "]");
    item.val(start + "L").change();
}


/**
 * 每月多少号之后最近的工作日
 */
function workDay(dom) {
    var name = dom.name;
    dayClash(name);
    var ns = $(dom).parent().parent().children("input[type='number']");
    var start = ns[0].value;
    var item = $("input[name=v_" + name + "]");
    item.val(start + "W").change();
}


/**
 * 指定
 */
function appoint(dom) {

    var name = dom.name;
    dayClash(name);
    var ns = $(dom).parent().parent().children().children().children("input[type='checkbox']");

    if (dom.checked) {
        if ($(ns).filter(":checked").length == 0) {
            $(ns).eq(0).attr("checked", true);
        }
    }
    
    var vals = [];
    ns.each(function () {
        if (this.checked) {
            vals.push(this.value);
        }
    });
    var val = "?";
    if (vals.length > 0 && vals.length < ns.length) {
        val = vals.join(",");
    } else if (vals.length == ns.length) {
        val = "*";
    }
    var item = $("input[name=v_" + name + "]");
    item.val(val).change();
}

/**
 * 周和天不能同时使用
 */
function dayClash(name) {
    if (name == "day") {
        $("#week_unAppoint").click();
    }
    if (name == "week") {
        $("#day_unAppoint").click();
    }
}

/**
 * 获得最终字符串并且获取最近5次执行时间
 */
function cronStr() {
    var cron = $("#Cronstr");
    var vals = $("input[name^='v_']");

    vals.change(function () {
        var item = [];
        vals.each(function () {
            item.push(this.value);
        });
        cron.val(item.join(" ")).change();
    });


    /**
     * 最近5次运行时间
     */
    cron.change(function () {
        $.ajax({
            type: 'get',
            url: "/api/services/quartz/quartzServer/GetTaskFireTime",
            dataType: "json",
            data: { cronExpressionString: $("#Cronstr").val(), numTimes: 5 },
            success: function (data) {
                if (data && data.result.length == 5) {
                    var strHTML = "<ul>";
                    for (var i = 0; i < data.result.length; i++) {
                        strHTML += "<li>" + data.result[i] + "</li>";
                    }
                    strHTML += "</ul>"
                    $("#runTime").html(strHTML);
                } else {
                    $("#runTime").html("");
                }
            }
        });
    });
}


/**
 * 操作对应框自动选择
 */
function inputToClick() {
    $(".cronNumber").change(function () {
        $(this).closest("div.form-group").children("label").children().eq(0).click();
    });

    $(".checkbox-inline").children().change(function () {
        $(this).closest("div.form-group").children("label").children().eq(0).click();
    });
}

/**
 * 表达式反解析到ui
 */
function fan() {
    var txt = $("#Cronstr").val();
    if (txt) {
        var regs = txt.split(' ');
        $("input[name=v_second]").val(regs[0]);
        $("input[name=v_min]").val(regs[1]);
        $("input[name=v_hour]").val(regs[2]);
        $("input[name=v_day]").val(regs[3]);
        $("input[name=v_month]").val(regs[4]);
        $("input[name=v_week]").val(regs[5]);
        $("input[name=v_year]").val(regs[6]);

        initObj(regs[0], "second");
        initObj(regs[1], "min");
        initObj(regs[2], "hour");
        initDay(regs[3]);
        initObj(regs[4], "month");
        initWeek(regs[5]);
        initYear(regs[6]);

        if (regs.length > 6) {
            $("input[name=v_year]").val(regs[6]);
            initYear(regs[6]);
        }
    }
}

function initObj(strVal, strid) {
    var ary = null;
    var objRadio = $("input[name='" + strid + "'");
    if (strVal == "*") {
        objRadio.eq(0).attr("checked", "checked");
    } else if (strVal.split('-').length > 1) {
        ary = strVal.split('-');
        objRadio.eq(1).attr("checked", "checked");
        $("#" + strid + "Start_0").val(ary[0]);
        $("#" + strid + "End_0").val(ary[1]);
    } else if (strVal.split('/').length > 1) {
        ary = strVal.split('/');
        objRadio.eq(2).attr("checked", "checked");
        $("#" + strid + "Start_1").val(ary[0]);
        $("#" + strid + "End_1").val(ary[1]);
    } else {
        objRadio.eq(3).attr("checked", "checked");
        if (strVal != "?") {
            ary = strVal.split(",");
            for (var i = 0; i < ary.length; i++) {
                $("." + strid + "List input[value='" + ary[i] + "']").attr("checked", "checked");
            }
        }
    }
}

function initDay(strVal) {
    var ary = null;
    var objRadio = $("input[name='day'");
    if (strVal == "*") {
        objRadio.eq(0).attr("checked", "checked");
    } else if (strVal == "?") {
        objRadio.eq(1).attr("checked", "checked");
    } else if (strVal.split('-').length > 1) {
        ary = strVal.split('-');
        objRadio.eq(2).attr("checked", "checked");
        $("#dayStart_0").val(ary[0]);
        $("#dayEnd_0").val(ary[1]);
    } else if (strVal.split('/').length > 1) {
        ary = strVal.split('/');
        objRadio.eq(3).attr("checked", "checked");
        $("#dayStart_1").val(ary[0]);
        $("#dayEnd_1").val(ary[1]);
    } else if (strVal.split('W').length > 1) {
        ary = strVal.split('W');
        objRadio.eq(4).attr("checked", "checked");
        $("#dayStart_2").val(ary[0]);
    } else if (strVal == "L") {
        objRadio.eq(5).attr("checked", "checked");
    } else {
        objRadio.eq(6).attr("checked", "checked");
        ary = strVal.split(",");
        for (var i = 0; i < ary.length; i++) {
            $(".dayList input[value='" + ary[i] + "']").attr("checked", "checked");
        }
    }
}

function initWeek(strVal) {
    var ary = null;
    var objRadio = $("input[name='week'");
    if (strVal == "*") {
        objRadio.eq(0).attr("checked", "checked");
    } else if (strVal == "?") {
        objRadio.eq(1).attr("checked", "checked");
    } else if (strVal.split('-').length > 1) {
        ary = strVal.split('-');
        objRadio.eq(2).attr("checked", "checked");
        $("#weekStart_0").val(ary[0]);
        $("#weekEnd_0").val(ary[1]);
    } else if (strVal.split('/').length > 1) {
        ary = strVal.split('/');
        objRadio.eq(3).attr("checked", "checked");
        $("#weekStart_1").val(ary[0]);
        $("#weekEnd_1").val(ary[1]);
    } else if (strVal.split('#').length > 1) {
        ary = strVal.split('#');
        objRadio.eq(4).attr("checked", "checked");
        $("#weekStart_2").val(ary[0]);
        $("#weekEnd_2").val(ary[1]);
    }
    else if (strVal.split('L').length > 1) {
        ary = strVal.split('L');
        objRadio.eq(5).attr("checked", "checked");
        $("#weekStart_3").val(ary[0]);
    } else {
        objRadio.eq(6).attr("checked", "checked");
        ary = strVal.split(",");
        for (var i = 0; i < ary.length; i++) {
            $(".weekList input[value='" + ary[i] + "']").attr("checked", "checked");
        }
    }
}

function initYear(strVal) {
    var ary = null;
    var objRadio = $("input[name='year']");
    if (strVal == "*") {
        objRadio.eq(0).attr("checked", "checked");
    } else if (strVal.split('-').length > 1) {
        ary = strVal.split('-');
        objRadio.eq(1).attr("checked", "checked");
        $("#yearStart_0").val(ary[0]);
        $("#yearEnd_0").val(ary[1]);
    }
}



$(function () {
    inputToClick();
    cronStr();
    fan();
    $("#Cronstr").change();
});