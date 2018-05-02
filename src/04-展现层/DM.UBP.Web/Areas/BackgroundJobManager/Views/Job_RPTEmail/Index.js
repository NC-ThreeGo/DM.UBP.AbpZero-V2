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
        var _$entityTable = $('#Job_RPTEmailTable');
        var _appService = abp.services.ubp.job_RPTEmail;

        var _permissions = {
            create: abp.auth.hasPermission('Pages.BackgroundJobManager.Job_RPTEmails.Create'),
            edit: abp.auth.hasPermission('Pages.BackgroundJobManager.Job_RPTEmails.Edit'),
            delete: abp.auth.hasPermission('Pages.BackgroundJobManager.Job_RPTEmails.Delete')
        };

        var _createModal = new app.ModalManager({
            viewUrl: abp.appPath + 'BackgroundJobManager/Job_RPTEmail/CreateModal',
            scriptUrl: abp.appPath + 'Areas/BackgroundJobManager/Views/Job_RPTEmail/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditModal'
        });

        var _editModal = new app.ModalManager({
            viewUrl: abp.appPath + 'BackgroundJobManager/Job_RPTEmail/EditModal',
            scriptUrl: abp.appPath + 'Areas/BackgroundJobManager/Views/Job_RPTEmail/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditModal'
        });


        function getEntities() {
            _$entityTable.jtable('load');
        }


        abp.event.on('app.createOrEditModalSaved', function () {
            getEntities();
        });


        $('#CreateNewJob_RPTEmailButton').click(function () {
            _createModal.open();
        });


        function deleteEntity(entity) {
            abp.message.confirm(
                app.localize('DeleteRecordWarningMessage'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _appService.deleteJob_RPTEmail({
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
            title: app.localize('Job_RPTEmails'),
            paging: true,
            sorting: true,
            multiSorting: true,
            actions: {
                listAction: {
                    method: _appService.getJob_RPTEmail
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
                    }]
                },
                job_RptEmailname: {
                    title: app.localize('job_RptEmailName'),
                    width: '20%',
                },
                bgjm_Jobgroup_Id: {
                    title: app.localize('bgjm_Jobgroup_Id'),
                    width: '20%',
                },
                emails: {
                    title: app.localize('emails'),
                    width: '15%',
                },
                template_Id: {
                    title: app.localize('template_Id'),
                    width: '15%',
                },
                parameters: {
                    title: app.localize('parameters'),
                    width: '15%',
                },
                description: {
                    title: app.localize('description'),
                    width: '15%',
                },
            }
        });

        getEntities();
    });
})();
