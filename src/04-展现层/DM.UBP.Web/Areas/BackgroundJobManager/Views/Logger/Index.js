//------------------------------------------------------------
// All Rights Reserved , Copyright (C)  
// 版本：1.0
/// <author>
///		<name></name>
///		<date>0001/1/1 0:00:00</date>
/// </author>
//------------------------------------------------------------

(function () {
    $(function () {
        var _$entityTable = $('#LoggerTable');
        var _appService = abp.services.ubp.logger;

        var _selectedDateRange = {
            startDate: moment().subtract(6, 'days').startOf('day'),
            endDate: moment().endOf('day')
        };

        $(".form-group").find('input.date-range-picker').daterangepicker(
            $.extend(true, createDateRangePickerOptions(), _selectedDateRange),
            function (start, end, label) {
                _selectedDateRange.startDate = start.format('YYYY-MM-DDT00:00:00Z');
                _selectedDateRange.endDate = end.format('YYYY-MM-DDT23:59:59.999Z');
            });


        function createDateRangePickerOptions() {
            var options = {
                locale: {
                    format: 'YYYY-MM-DD HH:mm',
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

                timePicker: true,
                //timePickerIncrement: 10, // 时间的增量，单位为分钟  10分钟会有bug（手动设置成不是10，20，30，40，50，00，会自动变成响应的时间）
                timePicker24Hour : true, // 是否使用24小时制来显示时间

                //dateLimit : {
                //    days : 30
                //}, //起止时间的最大间隔

                //minDate: moment('2015-05-01'),
                //maxDate: moment(), // 最大时间
                ranges: {}
            };
            //options.ranges['清空'] = ["", ""];
            options.ranges[app.localize('Today')] = [moment().startOf('day'), moment().endOf('day')];
            options.ranges[app.localize('Yesterday')] = [moment().subtract(1, 'days').startOf('day'), moment().subtract(1, 'days').endOf('day')];
            options.ranges[app.localize('Last7Days')] = [moment().subtract(6, 'days').startOf('day'), moment().endOf('day')];
            options.ranges[app.localize('Last30Days')] = [moment().subtract(29, 'days').startOf('day'), moment().endOf('day')];
            options.ranges[app.localize('ThisMonth')] = [moment().startOf('month'), moment().endOf('month')];
            options.ranges[app.localize('LastMonth')] = [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')];

            return options;
        }

        //日志不需要权限
        //var _permissions = {
        //    create: abp.auth.hasPermission('Pages.BackgroundJobManager.Loggers.Create'),
        //    edit: abp.auth.hasPermission('Pages.BackgroundJobManager.Loggers.Edit'),
        //    delete: abp.auth.hasPermission('Pages.BackgroundJobManager.Loggers.Delete')
        //};

        var _createModal = new app.ModalManager({
            viewUrl: abp.appPath + 'BackgroundJobManager/Logger/CreateModal',
            scriptUrl: abp.appPath + 'Areas/BackgroundJobManager/Views/Logger/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditModal'
        });

        var _editModal = new app.ModalManager({
            viewUrl: abp.appPath + 'BackgroundJobManager/Logger/EditModal',
            scriptUrl: abp.appPath + 'Areas/BackgroundJobManager/Views/Logger/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditModal'
        });

        $('#GetLoggerBtn').click(function () {
            //abp.message.info('内容', '抬头');
            getEntities();
        });

        $("#RefreshButton").click(function () {
            //abp.message.info('内容', '抬头');
            getEntities();
        });




        function getEntities() {
            _$entityTable.jtable('load', {
                filter: $('#JobNameFilter').val(),
                startDate: _selectedDateRange.startDate,
                endDate: _selectedDateRange.endDate,
                isException: $("#ExceptionSelectionCombo").val(),
                jobType: $("#JobTypeSelectionCombo").val()
            });
        }


        abp.event.on('app.createOrEditModalSaved', function () {
            getEntities();
        });


        _$entityTable.jtable({
            title: app.localize('Loggers'),
            paging: true,
            sorting: true,
            multiSorting: true,
            actions: {
                listAction: {
                    method: _appService.getLoggers
                }
            },

            fields: {
                id: {
                    key: true,
                    list: false
                },
                actions: {
                    title: '',
                    width: '5%',
                    sorting: false,
                    display: function (data) {
                        var $div = $('<div class=\"text-center\"></div>');

                        $div.append('<button class="btn btn-default btn-xs"><i class="fa fa-search"></i></button>')
                            .click(function () {
                                //showDetails(data.record);
                                _editModal.open({ id: data.record.id });
                            });

                        return $div;
                    }
                },
                isException: {
                    title: '',
                    width: '5%',
                    sorting: false,
                    display: function (data) {
                        var $div = $('<div class=\"text-center\"></div>');

                        if (data.record.isException) {
                            $div.append('<i class="fa fa-warning font-yellow-gold"></i>');
                        } else {
                            $div.append('<i class="fa fa-check-circle font-green"></i>');
                        }

                        return $div;
                    }
                },
                jobName: {
                    title: app.localize('JobName'),
                    width: '45%',
                },
                jobType: {
                    title: app.localize('JobType'),
                    width: '15%',
                },
                execStartTime: {
                    title: app.localize('ExecStartTime'),
                    width: '15%',
                    display: function (data) {
                        return moment(data.record.execStartTime).format('YYYY-MM-DD HH:mm:ss');
                    }
                },
                execEndTime: {
                    title: app.localize('ExecEndTime'),
                    width: '15%',
                    display: function (data) {
                        return moment(data.record.execEndTime).format('YYYY-MM-DD HH:mm:ss');
                    }
                }
            }
        });

        getEntities();
    });
})();
