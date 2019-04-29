//------------------------------------------------------------
// All Rights Reserved , Copyright (C)  
// 版本：1.0
/// <author>
///		<name></name>
///		<date>0001/1/1 0:00:00</date>
/// </author>
//------------------------------------------------------------

(function () {
    $(function () {
        var _$entityTable = $('#WeiXinAppTable');
        var _appService = abp.services.ubp.weiXinApp;

        var _permissions = {
            create: abp.auth.hasPermission('Pages.WeiXinManager.WeiXinApps.Create'),
            edit: abp.auth.hasPermission('Pages.WeiXinManager.WeiXinApps.Edit'),
            delete: abp.auth.hasPermission('Pages.WeiXinManager.WeiXinApps.Delete')
        };

        var _createModal = new app.ModalManager({
            viewUrl: abp.appPath + 'WeiXinManager/WeiXinApp/CreateModal',
            scriptUrl: abp.appPath + 'Areas/WeiXinManager/Views/WeiXinApp/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditModal'
        });

        var _editModal = new app.ModalManager({
            viewUrl: abp.appPath + 'WeiXinManager/WeiXinApp/EditModal',
            scriptUrl: abp.appPath + 'Areas/WeiXinManager/Views/WeiXinApp/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditModal'
        });


        function getEntities() {
            _$entityTable.jtable('load');
        }


        abp.event.on('app.createOrEditModalSaved', function () {
            getEntities();
        });


        $('#CreateNewWeiXinAppButton').click(function () {
            _createModal.open();
        });


        function deleteEntity(entity) {
            abp.message.confirm(
                app.localize('DeleteRecordWarningMessage'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _appService.deleteWeiXinApp({
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
            title: app.localize('WeiXinManager_App'),
            paging: true,
            sorting: true,
            multiSorting: true,
            actions: {
                listAction: {
                    method: _appService.getWeiXinApps
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
                        text: app.localize('SendInfo'),
                        visible: function () {
                            return _permissions.edit;
                        },
                        action: function (data) {
                            _editModal.open({ id: data.record.id });
                        }
                    },
                    {
                        text: app.localize('SetMenu'),
                        visible: function () {
                            return _permissions.edit;
                        },
                        action: function (data) {
                            _editModal.open({ id: data.record.id });
                        }
                    },
                    ]
                },
                appName: {
                    title: app.localize('AppName'),
                    width: '50%',
                },
                descriotion: {
                    title: app.localize('Descriotion'),
                    width: '50%',
                },
            }
        });

        getEntities();
    });
})();
