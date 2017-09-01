(function () {
    appModule.factory('appSession', [
            function () {

                var _session = {
                    user: null,
                    tenant: null
                };

                abp.services.app.session.getCurrentLoginInformations({ async: false }).done(function (result) {
                    _session.user = result.user;
                    _session.tenant = result.tenant;
                });

                if (_session.tenant && _session.tenant.customCssId) {
                    $('head').append('<link id="TenantCustomCss" href="' + abp.appPath + 'TenantCustomization/GetCustomCss?id=' + _session.tenant.customCssId + '" rel="stylesheet"/>');
                }

                return _session;
            }
    ]);
})();