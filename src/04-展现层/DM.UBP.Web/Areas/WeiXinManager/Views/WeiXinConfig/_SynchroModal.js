(function ($) {
    app.modals.SynchroModal = function () {
        var _appService = abp.services.ubp.weiXinConfig;

        var _modalManager;
        var _$formInfo = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$formInfo = _modalManager.getModal().find('form[name=EntityOptInformationsForm]');
            _$formInfo.validate();
        };

        this.save = function () {
            if (!_$formInfo.valid()) {
                return;
            }

            var input = _$formInfo.serializeFormToObject();

            _modalManager.setBusy(true);
            //abp.message.info(input.Id, 'Id');
            //同步
            _appService.synchroTXL(input)
            .done(function (data) {
                if (data) {
                    abp.message.success(app.localize('SynchroSuccessed'));
                }
                else {
                    abp.message.error(app.localize('SynchroFailed'));
                }
                _modalManager.close();
                abp.event.trigger('app.synchroModalSaved', data);
            }).always(function () {
                _modalManager.setBusy(false);
            });

        };

    };
})(jQuery);
