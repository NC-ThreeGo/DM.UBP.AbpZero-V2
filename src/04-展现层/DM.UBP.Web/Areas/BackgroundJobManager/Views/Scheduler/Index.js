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
                                        var _$entityTable = $('#SchedulerTable');
                                        var _appService = abp.services.BackgroundJobManager.scheduler;

                                        var _permissions = {
                                            create: abp.auth.hasPermission('Pages.BackgroundJobManager.Schedulers.Create'),
                                            edit: abp.auth.hasPermission('Pages.BackgroundJobManager.Schedulers.Edit'),
                                            delete: abp.auth.hasPermission('Pages.BackgroundJobManager.Schedulers.Delete')
                                            };

                                        var _createModal = new app.ModalManager({
                                            viewUrl: abp.appPath + 'BackgroundJobManager/Scheduler/CreateModal',
                                            scriptUrl: abp.appPath + 'Areas/BackgroundJobManager/Views/Scheduler/_CreateOrEditModal.js',
                                            modalClass: 'CreateOrEditModal'
                                        });

                                        var _editModal = new app.ModalManager({
                                            viewUrl: abp.appPath + 'BackgroundJobManager/Scheduler/EditModal',
                                            scriptUrl: abp.appPath + 'Areas/BackgroundJobManager/Views/Scheduler/_CreateOrEditModal.js',
                                            modalClass: 'CreateOrEditModal'
                                        });
                                

      function getEntities() {
                                            _$entityTable.jtable('load');
                                        }
                                

      abp.event.on('app.createOrEditModalSaved', function () {
                                            getEntities();
                                        });
                                

      $('#CreateNewSchedulerButton').click(function () {
                                            _createModal.open();
                                        });
                                

      function deleteEntity(entity) {
                                            abp.message.confirm(
                                                app.localize('DeleteRecordWarningMessage'),
                                                function (isConfirmed) {
                                                    if (isConfirmed) {
                                                        _appService.deleteScheduler({
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
                                            title: app.localize('Schedulers'),
                                            paging: true,
                                            sorting: true,
                                            multiSorting: true,
                                            actions: {
                                                listAction: {
                                                    method: _appService.getSchedulers
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
                                                        visible: function() {
                                                            return _permissions.edit;
                                                        },
                                                        action: function(data) {
                                                            _editModal.open({ id: data.record.id });
                                                        }
                                                    }, 
                                                    {
                                                        text: app.localize('Delete'),
                                                        visible: function(data) {
                                                            return _permissions.delete;
                                                        },
                                                        action: function(data) {
                                                            deleteEntity(data.record);
                                                        }
                                                    }]
                                                },
       schedulername: {
                                                    title: app.localize('schedulername'),
                                                    width: '20%',
                                                },
       status: {
                                                    title: app.localize('status'),
                                                    width: '20%',
                                                },
       lastexttime: {
                                                    title: app.localize('lastexttime'),
                                                    width: '20%',
                                                },
       description: {
                                                    title: app.localize('description'),
                                                    width: '40%',
                                                },
       }
   });

        getEntities();
   });
})();
