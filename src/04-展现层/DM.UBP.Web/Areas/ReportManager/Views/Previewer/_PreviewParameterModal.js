//------------------------------------------------------------
// All Rights Reserved , Copyright (C)  
// 版本：1.0
/// <author>
///		<name></name>
///		<date>0001/1/1 0:00:00</date>
/// </author>
//------------------------------------------------------------

(function ($) {
    app.modals.PreviewParameterModal = function () {
        var _appService = abp.services.ReportManager.reportTemplate;

        var _modalManager;
        var _$formInfo = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$formInfo = _modalManager.getModal().find('form[name=PreviewerForm]');

            _$formInfo.find('input.date-picker').datetimepicker({
                language: 'zh-CN',  
                format: "yyyy-mm-dd",
                minView: 2,
                //0从小时视图开始，选分
                //1从天视图开始，选小时
                //2从月视图开始，选天
                //3从年视图开始，选月
                //4从十年视图开始，选年
                //autoclose: true,
                todayBtn: true,
                //startDate: "2013-02-14 10:00",
                forceParse: true,
                minuteStep: 10
            });

            _$formInfo.find('input.date-time-picker').datetimepicker({
                language: 'zh-CN',
                format: "yyyy-mm-dd hh:ii:ss",
                minView: 0,
                //0从小时视图开始，选分
                //1从天视图开始，选小时
                //2从月视图开始，选天
                //3从年视图开始，选月
                //4从十年视图开始，选年
                //autoclose: true,
                todayBtn: true,
                //startDate: "2013-02-14 10:00",
                forceParse: true,
                minuteStep: 10
            });

            _$formInfo.validate();
        };

        function openwin(url) {
            //window.open('about:blank', name, 'height=400, width=400, top=0, left=0, toolbar=yes, menubar=yes, scrollbars=yes, resizable=yes,location=yes, status=yes');
            var a = document.createElement("a");
            a.setAttribute("href", url);
            a.setAttribute("target", "_blank");
            a.setAttribute("id", "openwin");
            document.body.appendChild(a);
            a.click();
            document.body.removeChild(a);
        }

        this.save = function () {
            if (!_$formInfo.valid()) {
                return;
            }
            var input = _$formInfo.serializeFormToObject();

            _modalManager.setBusy(true);

            var templateFileId = _$formInfo.find('#templateFileId').val();
            var url = abp.appPath + 'ReportManager/Previewer/Index?id=' + templateFileId + "&parameterValues=" + JSON.stringify(input);

            openwin(url);


            _modalManager.close();
            _modalManager.setBusy(false);
        };

        function createDateRangePickerOptions() {
            var options = {
                locale: {
                    format: 'YYYY-MM-DD HH:mm:ss',
                    applyLabel: app.localize('Apply'),
                    cancelLabel: app.localize('Cancel'),
                    customRangeLabel: app.localize('CustomRange'),
                    //separator:' 至 '
                },
                //startDate: moment().startOf('day'),
                //endDate: moment(),
                //opens: 'right',    // 日期选择框的弹出位置
                //showDropdowns : true,
                showWeekNumbers: true,     // 是否显示第几周

                //singleDatePicker: true,//显示单独时间

                //timePicker: true,
                //timePickerIncrement: 10, // 时间的增量，单位为分钟
                timePicker24Hour : true, // 是否使用24小时制来显示时间

                //dateLimit : {
                //    days : 30
                //}, //起止时间的最大间隔

                //minDate: moment('2015-05-01'),
                //maxDate: moment(), // 最大时间
                ranges: {}
            };
            options.ranges['清空'] = ["", ""];
            options.ranges[app.localize('Today')] = [moment().startOf('day'), moment().endOf('day')];
            options.ranges[app.localize('Yesterday')] = [moment().subtract(1, 'days').startOf('day'), moment().subtract(1, 'days').endOf('day')];
            options.ranges[app.localize('Last7Days')] = [moment().subtract(6, 'days').startOf('day'), moment().endOf('day')];
            options.ranges[app.localize('Last30Days')] = [moment().subtract(29, 'days').startOf('day'), moment().endOf('day')];
            options.ranges[app.localize('ThisMonth')] = [moment().startOf('month'), moment().endOf('month')];
            options.ranges[app.localize('LastMonth')] = [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')];

            return options;
        }

        function createDateRangePickerBack(start, end, label) {
            //alert(start.format('YYYY-MM-DD HH:mm:ss'));
            if (label == '清空') {
                $('#dateTimeRange').val('');
                $('#beginTime').val('');
                $('#endTime').val('');
                return;
            }
            $('#beginTime').val(start.format('YYYY-MM-DD HH:mm:ss'));
            $('#endTime').val(end.format('YYYY-MM-DD HH:mm:ss'));
        };
    };
})(jQuery);
