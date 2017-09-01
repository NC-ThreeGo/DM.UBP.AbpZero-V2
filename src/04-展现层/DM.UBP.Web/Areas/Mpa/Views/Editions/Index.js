(function () {
    $(function () {

        var _$editionsTable = $('#EditionsTable');
        var _editionService = abp.services.app.edition;

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Editions.Create'),
            edit: abp.auth.hasPermission('Pages.Editions.Edit'),
            'delete': abp.auth.hasPermission('Pages.Editions.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Mpa/Editions/CreateOrEditModal',
            scriptUrl: abp.appPath + 'Areas/Mpa/Views/Editions/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEditionModal'
        });

        _$editionsTable.jtable({

            title: app.localize('Editions'),

            actions: {
                listAction: {
                    method: _editionService.getEditions
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
                        visible: function () {
                            return _permissions.edit;
                        },
                        action: function (data) {
                            _createOrEditModal.open({ id: data.record.id });
                        }
                    }, {
                        text: app.localize('Delete'),
                        visible: function () {
                            return _permissions.delete;
                        },
                        action: function (data) {
                            deleteEdition(data.record);
                        }
                    }]
                },
                displayName: {
                    title: app.localize('EditionName'),
                    width: '35%'
                },
                creationTime: {
                    title: app.localize('CreationTime'),
                    width: '35%',
                    display: function (data) {
                        return moment(data.record.creationTime).format('L');
                    }
                }
            }

        });

        function deleteEdition(edition) {
            abp.message.confirm(
                app.localize('EditionDeleteWarningMessage', edition.displayName),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _editionService.deleteEdition({
                            id: edition.id
                        }).done(function () {
                            getEditions();
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        };

        $('#CreateNewEditionButton').click(function () {
            _createOrEditModal.open();
        });

        function getEditions() {
            _$editionsTable.jtable('load');
        }

        abp.event.on('app.createOrEditEditionModalSaved', function () {
            getEditions();
        });

        getEditions();
    });
})();