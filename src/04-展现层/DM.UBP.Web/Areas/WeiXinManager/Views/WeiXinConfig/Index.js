(function () {
    $(function () {
        var _$entityTable = $('#WeiXinConfigTable');
        var _appService = abp.services.ubp.weiXinConfig;

        var _permissions = {
            create: abp.auth.hasPermission('Pages.WeiXinManager.WeiXinConfigs.Create'),
            edit: abp.auth.hasPermission('Pages.WeiXinManager.WeiXinConfigs.Edit'),
            delete: abp.auth.hasPermission('Pages.WeiXinManager.WeiXinConfigs.Delete')
        };

        var _createModal = new app.ModalManager({
            viewUrl: abp.appPath + 'WeiXinManager/WeiXinConfig/CreateModal',
            scriptUrl: abp.appPath + 'Areas/WeiXinManager/Views/WeiXinConfig/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditModal'
        });

        var _editModal = new app.ModalManager({
            viewUrl: abp.appPath + 'WeiXinManager/WeiXinConfig/EditModal',
            scriptUrl: abp.appPath + 'Areas/WeiXinManager/Views/WeiXinConfig/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditModal'
        });

        var _synchroModal = new app.ModalManager({
            viewUrl: abp.appPath + 'WeiXinManager/WeiXinConfig/SynchroModal',
            scriptUrl: abp.appPath + 'Areas/WeiXinManager/Views/WeiXinConfig/_SynchroModal.js',
            modalClass: 'SynchroModal'
        });

        var _addNewAppModal = new app.ModalManager({
            viewUrl: abp.appPath + 'WeiXinManager/WeiXinApp/CreateModal',
            scriptUrl: abp.appPath + 'Areas/WeiXinManager/Views/WeiXinApp/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditModal'
        });


        function getEntities() {
            _$entityTable.jtable('load');
        }


        abp.event.on('app.createOrEditModalSaved', function () {
            getEntities();
        });

        abp.event.on('app.synchroModalSaved', function () {
            getEntities();
        });

        $('#CreateNewWeiXinConfigButton').click(function () {
            _createModal.open();
        });


        function deleteEntity(entity) {
            abp.message.confirm(
                app.localize('DeleteRecordWarningMessage'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _appService.deleteWeiXinConfig({
                            id: entity.id
                        }).done(function () {
                            getEntities();
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        };


        _$entityTable.jtable({
            title: app.localize('WeiXinConfigList'),
            paging: true,
            sorting: true,
            multiSorting: true,
            actions: {
                listAction: {
                    method: _appService.getWeiXinConfigs
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
                            _editModal.open({ id: data.record.id });
                        }
                    },
                    {
                        text: app.localize('Delete'),
                        visible: function (data) {
                            return _permissions.delete;
                        },
                        action: function (data) {
                            deleteEntity(data.record);
                        }
                    },
                    {
                        text: app.localize('Synchro'),
                        visible: function () {
                            return _permissions.edit;
                        },
                        action: function (data) {
                            _synchroModal.open({ id: data.record.id });
                        }
                    },
                    {
                        text: app.localize('SendInfo'),
                        visible: function () {
                            return _permissions.edit;
                        },
                        action: function (data) {
                            _synchroModal.open({ id: data.record.id });
                        }
                    },
                    {
                        text: app.localize('AddNewApp'),
                        visible: function () {
                            return _permissions.edit;
                        },
                        action: function (data) {
                            _addNewAppModal.open({ corpId: data.record.corpId });
                        }
                    }]
                },
                corpName: {
                    title: app.localize('CorpName'),
                    width: '50%',
                },
                corpId: {
                    title: app.localize('CorpId'),
                    width: '50%',
                },
            }
        });

        getEntities();
    });
})();
