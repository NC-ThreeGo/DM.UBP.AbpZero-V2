(function () {
    $(function () {

        var _$languagesTable = $('#LanguagesTable');
        var _languageService = abp.services.app.language;
        var _defaultLanguageName = null;

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration.Languages.Create'),
            edit: abp.auth.hasPermission('Pages.Administration.Languages.Edit'),
            changeTexts: abp.auth.hasPermission('Pages.Administration.Languages.ChangeTexts'),
            'delete': abp.auth.hasPermission('Pages.Administration.Languages.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Mpa/Languages/CreateOrEditModal',
            scriptUrl: abp.appPath + 'Areas/Mpa/Views/Languages/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditLanguageModal'
        });

        _$languagesTable.jtable({

            title: app.localize('Languages'),

            actions: {
                listAction: {
                    method: _languageService.getLanguages
                }
            },

            fields: {
                id: {
                    key: true,
                    list: false
                },
                actions: {
                    type: 'record-actions',
                    cssClass: 'btn btn-xs btn-primary blue',
                    text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
                    items: [{
                        text: app.localize('Edit'),
                        visible: function (data) {
                            return _permissions.edit && data.record.tenantId === abp.session.tenantId;
                        },
                        action: function (data) {
                            _createOrEditModal.open({ id: data.record.id });
                        }
                    }, {
                        text: app.localize('ChangeTexts'),
                        visible: function () {
                            return _permissions.edit;
                        },
                        action: function (data) {
                            document.location.href = abp.appPath + "Mpa/Languages/Texts?languageName=" + data.record.name;
                        }
                    }, {
                        text: app.localize('SetAsDefaultLanguage'),
                        visible: function () {
                            return _permissions.edit;
                        },
                        action: function (data) {
                            setAsDefaultLanguage(data.record);
                        }
                    }, {
                        text: app.localize('Delete'),
                        visible: function (data) {
                            return _permissions.delete && data.record.tenantId === abp.session.tenantId;
                        },
                        action: function (data) {
                            deleteLanguage(data.record);
                        }
                    }]
                },
                displayName: {
                    title: app.localize('Name'),
                    width: '30%',
                    display: function (data) {
                        var $span = $('<span></span>');

                        $span.append('<i class="' + data.record.icon + '"></i>');
                        $span.append(' &nbsp; ');
                        $span.append('<span data-language-name="' + data.record.name + '">' + data.record.displayName + "</span>");

                        return $span;
                    }
                },
                name: {
                    title: app.localize('Code'),
                    width: '10%'
                },
                tenantId: {
                    title: app.localize('Default') + '*',
                    width: '10%',
                    list: abp.session.tenantId ? true : false, //this field is visible only for tenants
                    display: function (data) {
                        var $span = $('<span></span>');

                        if (data.record.tenantId != abp.session.tenantId) {
                            $span.append('<span class="label label-default">' + app.localize('Yes') + '</span>');
                        } else {
                            $span.append('<span class="label label-success">' + app.localize('No') + '</span>');
                        }

                        return $span;
                    }
                },
                creationTime: {
                    title: app.localize('CreationTime'),
                    width: '20%',
                    display: function (data) {
                        return moment(data.record.creationTime).format('L');
                    }
                }
            },

            recordsLoaded: function (event, data) {
                _defaultLanguageName = data.serverResponse.originalResult.defaultLanguageName;
                _$languagesTable
                    .find('[data-language-name=' + _defaultLanguageName + ']')
                    .addClass('text-bold')
                    .append(' (' + app.localize('Default') + ')');
            }

        });

        function setAsDefaultLanguage(language) {
            _languageService.setDefaultLanguage({
                name: language.name
            }).done(function () {
                getLanguages();
                abp.notify.success(app.localize('SuccessfullySaved'));
            });
        };

        function deleteLanguage(language) {
            abp.message.confirm(
                app.localize('LanguageDeleteWarningMessage', language.displayName),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _languageService.deleteLanguage({
                            id: language.id
                        }).done(function () {
                            getLanguages();
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        };

        $('#CreateNewLanguageButton').click(function () {
            _createOrEditModal.open();
        });

        function getLanguages() {
            _$languagesTable.jtable('load');
        }

        abp.event.on('app.createOrEditLanguageModalSaved', function () {
            getLanguages();
        });

        getLanguages();
    });
})();