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
        var _$entityTable = $('#JobGroupTable');
        var _appService = abp.services.ubp.jobGroup;

        var _permissions = {
            create: abp.auth.hasPermission('Pages.BackgroundJobManager.JobGroups.Create'),
            edit: abp.auth.hasPermission('Pages.BackgroundJobManager.JobGroups.Edit'),
            delete: abp.auth.hasPermission('Pages.BackgroundJobManager.JobGroups.Delete')
        };

        var _createModal = new app.ModalManager({
            viewUrl: abp.appPath + 'BackgroundJobManager/JobGroup/CreateModal',
            scriptUrl: abp.appPath + 'Areas/BackgroundJobManager/Views/JobGroup/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditModal'
        });

        var _editModal = new app.ModalManager({
            viewUrl: abp.appPath + 'BackgroundJobManager/JobGroup/EditModal',
            scriptUrl: abp.appPath + 'Areas/BackgroundJobManager/Views/JobGroup/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditModal'
        });


        function getEntities() {
            _$entityTable.jtable('load');
        }


        abp.event.on('app.createOrEditModalSaved', function () {
            getEntities();
        });


        $('#CreateNewJobGroupButton').click(function () {
            _createModal.open();
        });


        function deleteEntity(entity) {
            abp.message.confirm(
                app.localize('DeleteRecordWarningMessage'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _appService.deleteJobGroup({
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
            title: app.localize('JobGroups'),
            paging: true,
            sorting: true,
            multiSorting: true,
            actions: {
                listAction: {
                    method: _appService.getJobGroups
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
                    }]
                },
                jobGroupName: {
                    title: app.localize('JobGroupName'),
                    width: '20%',
                },
                assemblyName: {
                    title: app.localize('AssemblyName'),
                    width: '20%',
                },
                className: {
                    title: app.localize('ClassName'),
                    width: '20%',
                },
                description: {
                    title: app.localize('Description'),
                    width: '20%',
                },
                typeTable: {
                    title: app.localize('TypeTable'),
                    width: '20%',
                },
            }
        });

        getEntities();
    });
})();
