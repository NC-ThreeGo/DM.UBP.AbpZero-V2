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
            debugger;
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
            //if (input.Id > 0) {
            //    //修改
            //    _appService.updateReportTemplate(input)
            //        .done(function () {
            //            abp.notify.info(app.localize('SavedSuccessfully'));
            //            _modalManager.close();
            //            abp.event.trigger('app.createOrEditModalSaved');
            //        }).always(function () {
            //            _modalManager.setBusy(false);
            //        });
            //}
            //else {
            //    //新建
            //    _appService.createReportTemplate(input)
            //        .done(function () {
            //            abp.notify.info(app.localize('SavedSuccessfully'));
            //            _modalManager.close();
            //            abp.event.trigger('app.createOrEditModalSaved');
            //        }).always(function () {
            //            _modalManager.setBusy(false);
            //        });
            //};
        };

    };
})(jQuery);
