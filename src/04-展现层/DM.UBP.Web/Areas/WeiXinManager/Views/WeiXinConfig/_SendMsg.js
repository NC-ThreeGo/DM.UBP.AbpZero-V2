(function ($) {
    app.modals.SendMsgModal = function () {
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

            _appService.sendMsg(input)
                .done(function () {
                    abp.notify.info(app.localize('SendMsgSuccessfully'));
                    _modalManager.close();
                    abp.event.trigger('app.synchroModalSaved');
                }).always(function () {
                    _modalManager.setBusy(false);
                });

        };

    };
})(jQuery);
