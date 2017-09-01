(function () {
    $(function () {
        var _tenantSettingsService = abp.services.app.tenantSettings;
        var _initialTimeZone = $('#GeneralSettingsForm [name=Timezone]').val();
        var _usingDefaultTimeZone = $('#GeneralSettingsForm [name=TimezoneForComparison]').val() === abp.setting.values["Abp.Timing.TimeZone"];

        //Toggle form based registration options
        var _$selfRegistrationOptions = $('#FormBasedRegistrationSettingsForm')
            .find('input[name=IsNewRegisteredUserActiveByDefault],input[name=UseCaptchaOnRegistration]')
            .closest('.md-checkbox');

        function toggleSelfRegistrationOptions() {
            if ($('#Setting_AllowSelfRegistration').is(':checked')) {
                _$selfRegistrationOptions.slideDown('fast');
            } else {
                _$selfRegistrationOptions.slideUp('fast');
            }
        }

        $('#Setting_AllowSelfRegistration').change(function () {
            toggleSelfRegistrationOptions();
        });

        toggleSelfRegistrationOptions();

        //Toggle SMTP credentials
        var _$smtpCredentialFormGroups = $('#EmailSmtpSettingsForm')
            .find('input[name=SmtpDomain],input[name=SmtpUserName],input[name=SmtpPassword]')
            .closest('.form-group');

        function toggleSmtpCredentialFormGroups() {
            if ($('#Settings_SmtpUseDefaultCredentials').is(':checked')) {
                _$smtpCredentialFormGroups.slideUp('fast');
            } else {
                _$smtpCredentialFormGroups.slideDown('fast');
            }
        }

        $('#Settings_SmtpUseDefaultCredentials').change(function () {
            toggleSmtpCredentialFormGroups();
        });

        toggleSmtpCredentialFormGroups();

        //Toggle LDAP credentials
        var _$ldapCredentialFormGroups = $('#LdapSettingsForm')
            .find('input[name=Domain],input[name=UserName],input[name=Password]')
            .closest('.form-group');

        function toggleLdapCredentialFormGroups() {
            if ($('#Setting_LdapIsEnabled').is(':checked')) {
                _$ldapCredentialFormGroups.slideDown('fast');
            } else {
                _$ldapCredentialFormGroups.slideUp('fast');
            }
        }

        toggleLdapCredentialFormGroups();

        $('#Setting_LdapIsEnabled').change(function () {
            toggleLdapCredentialFormGroups();
        });

        //Toggle User lockout

        var _$userLockOutSettingsFormItems = $('#UserLockOutSettingsForm')
            .find('input')
            .not('#Setting_UserLockOut_IsEnabled')
            .closest('.form-group');

        function toggleUserLockOutSettingsFormItems() {
            if ($('#Setting_UserLockOut_IsEnabled').is(':checked')) {
                _$userLockOutSettingsFormItems.slideDown('fast');
            } else {
                _$userLockOutSettingsFormItems.slideUp('fast');
            }
        }

        toggleUserLockOutSettingsFormItems();

        $('#Setting_UserLockOut_IsEnabled').change(function () {
            toggleUserLockOutSettingsFormItems();
        });

        //Toggle two factor login

        var _$twoFactorLoginSettingsFormItems = $('#TwoFactorLoginSettingsForm')
            .find('input')
            .not('#Setting_TwoFactorLogin_IsEnabled')
            .closest('.md-checkbox');

        function toggleTwoFactorLoginSettingsFormItems() {
            if ($('#Setting_TwoFactorLogin_IsEnabled').is(':checked')) {
                _$twoFactorLoginSettingsFormItems.slideDown('fast');
            } else {
                _$twoFactorLoginSettingsFormItems.slideUp('fast');
            }
        }

        toggleTwoFactorLoginSettingsFormItems();

        $('#Setting_TwoFactorLogin_IsEnabled').change(function () {
            toggleTwoFactorLoginSettingsFormItems();
        });

        //Security
        $('#Setting_PasswordComplexity_UseDefaultSettings').change(function (val) {
            if ($('#Setting_PasswordComplexity_UseDefaultSettings').is(":checked")) {
                $('#DefaultPasswordComplexitySettingsForm').show();
                $('#PasswordComplexitySettingsForm').hide();
            } else {
                $('#DefaultPasswordComplexitySettingsForm').hide();
                $('#PasswordComplexitySettingsForm').show();
            }
        });

        function getDefaultPasswordComplexitySettings() {
            //note: this is a fix for '$('#DefaultPasswordComplexitySettingsForm').serializeFormToObject()' always returns true for checkboxes if they are disabled.
            var $disabledDefaultPasswordInputs = $('#DefaultPasswordComplexitySettingsForm input:disabled');
            $disabledDefaultPasswordInputs.removeAttr("disabled");
            var defaultPasswordComplexitySettings = $('#DefaultPasswordComplexitySettingsForm').serializeFormToObject();
            $disabledDefaultPasswordInputs.attr("disabled", "disabled");
            return defaultPasswordComplexitySettings;
        }

        //Appearance/Logo
        $('#SettingsLogoUploadForm').ajaxForm({
            beforeSubmit: function (formData, jqForm, options) {

                var $fileInput = $('#SettingsLogoUploadForm input[name=ApplicationLogoImage]');
                var files = $fileInput.get()[0].files;

                if (!files.length) {
                    return false;
                }

                var file = files[0];

                //File type check
                var type = '|' + file.type.slice(file.type.lastIndexOf('/') + 1) + '|';
                if ('|jpg|jpeg|png|gif|'.indexOf(type) === -1) {
                    abp.message.warn(app.localize('File_Invalid_Type_Error'));
                    return false;
                }

                //File size check
                if (file.size > 30720) //30KB
                {
                    abp.message.warn(app.localize('File_SizeLimit_Error'));
                    return false;
                }

                return true;
            },
            success: function (response) {
                if (response.success) {
                    refreshLogo(abp.appPath + 'TenantCustomization/GetLogo?id=' + response.result.id);
                    abp.notify.info(app.localize('SavedSuccessfully'));
                } else {
                    abp.message.error(response.error.message);
                }
            }
        });

        $('#SettingsLogoUploadForm button[type=reset]').click(function () {
            _tenantSettingsService.clearLogo().done(function () {
                refreshLogo(abp.appPath + 'Common/Images/app-logo-on-light.png');
                abp.notify.info(app.localize('ClearedSuccessfully'));
            });
        });

        function refreshLogo(url) {
            $('#AppLogo').attr('src', url);
        }

        //Appearance/Custom CSS
        $('#SettingsCustomCssUploadForm').ajaxForm({
            beforeSubmit: function (formData, jqForm, options) {

                var $fileInput = $('#SettingsCustomCssUploadForm input[name=CustomCssFile]');
                var files = $fileInput.get()[0].files;

                if (!files.length) {
                    return false;
                }

                var file = files[0];

                //File type check
                var type = '|' + file.type.slice(file.type.lastIndexOf('/') + 1) + '|';
                if ('|css|'.indexOf(type) === -1) {
                    abp.message.warn(app.localize('File_Invalid_Type_Error'));
                    return false;
                }

                //File size check
                if (file.size > 1048576) //1MB
                {
                    abp.message.warn(app.localize('File_SizeLimit_Error'));
                    return false;
                }

                return true;
            },
            success: function (response) {
                if (response.success) {
                    refreshCustomCss(abp.appPath + 'TenantCustomization/GetCustomCss?id=' + response.result.id);
                    abp.notify.info(app.localize('SavedSuccessfully'));
                } else {
                    abp.message.error(response.error.message);
                }
            }
        });

        $('#SettingsCustomCssUploadForm button[type=reset]').click(function () {
            _tenantSettingsService.clearCustomCss().done(function () {
                refreshCustomCss(null);
                abp.notify.info(app.localize('ClearedSuccessfully'));
            });
        });

        function refreshCustomCss(url) {
            $('#TenantCustomCss').remove();
            if (url) {
                $('head').append('<link id="TenantCustomCss" href="' + url + '" rel="stylesheet"/>');
            }
        }

        //Save settings
        $('#SaveAllSettingsButton').click(function () {
            _tenantSettingsService.updateAllSettings({
                general: $('#GeneralSettingsForm').serializeFormToObject(),
                userManagement: $.extend($('#FormBasedRegistrationSettingsForm').serializeFormToObject(), $('#OtherSettingsForm').serializeFormToObject()),
                email: $('#EmailSmtpSettingsForm').serializeFormToObject(),
                ldap: $('#LdapSettingsForm').serializeFormToObject(),
                security: {
                    useDefaultPasswordComplexitySettings: $('#Setting_PasswordComplexity_UseDefaultSettings').is(":checked"),
                    passwordComplexity: $('#PasswordComplexitySettingsForm').serializeFormToObject(),
                    defaultPasswordComplexity: getDefaultPasswordComplexitySettings(),
                    userLockOut: $('#UserLockOutSettingsForm').serializeFormToObject(),
                    twoFactorLogin: $('#TwoFactorLoginSettingsForm').serializeFormToObject()
                }
            }).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));

                var newTimezone = $('#GeneralSettingsForm [name=Timezone]').val();
                if (abp.clock.provider.supportsMultipleTimezone &&
                        _usingDefaultTimeZone &&
                        _initialTimeZone !== newTimezone) {
                    abp.message.info(app.localize('TimeZoneSettingChangedRefreshPageNotification')).done(function () {
                        window.location.reload();
                    });
                }

            });
        });
    });
})();