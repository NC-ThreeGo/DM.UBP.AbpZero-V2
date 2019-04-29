(function ($) {
    app.modals.CreateOrEditModal = function () {
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

            if (input.Id > 0) {
                //修改
                _appService.updateWeiXinConfig(input)
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
                _appService.createWeiXinConfig(input)
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
