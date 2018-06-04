//------------------------------------------------------------
// All Rights Reserved , Copyright (C)  
// 版本：1.0
/// <author>
///		<name></name>
///		<date>0001/1/1 0:00:00</date>
/// </author>
//------------------------------------------------------------

(function ($) {
    app.modals.CreateOrEditModal = function () {
        var _appService = abp.services.ubp.scheduler;
        var _appJobGroupService = abp.services.ubp.jobGroup;

        var _modalManager;
        var _$formInfo = null;
        
        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$formInfo = _modalManager.getModal().find('form[name=EntityOptInformationsForm]');
            _$formInfo.validate();

            var groups = _modalManager.getModal().find("#JobGroup_Id");
            var jobs = _modalManager.getModal().find("#Job_Id");
            groups.change(function () {
                _appJobGroupService.getJobsToItem(groups.val())
                    .done(function (data) {
                        jobs.empty();
                        for (var i = 0; i < data.length; i++) {
                            var html = "<option data-icon='" + data[i].value + "' value='" + data[i].value + "'> <i class='" + data[i].value + "'></i>" + data[i].displayText +"</option >";
                            jobs.append(html);
                        }
                    });
            });
        };
       
        this.save = function () {
            if (!_$formInfo.valid()) {
                return;
            }

            var input = _$formInfo.serializeFormToObject();

            _modalManager.setBusy(true);

            if (input.Id > 0) {
                //修改
                _appService.updateScheduler(input)
                    .done(function () {
                        abp.notify.info(app.localize('SavedSuccessfully'));
                        _modalManager.close();
                        abp.event.trigger('app.createOrEditModalSaved');
                    }).always(function () {
                        _modalManager.setBusy(false);
                    });
            }
            else {
                //新建
                _appService.createScheduler(input)
                    .done(function () {
                        abp.notify.info(app.localize('SavedSuccessfully'));
                        _modalManager.close();
                        abp.event.trigger('app.createOrEditModalSaved');
                    }).always(function () {
                        _modalManager.setBusy(false);
                    });
            };
        };

    };
})(jQuery);

