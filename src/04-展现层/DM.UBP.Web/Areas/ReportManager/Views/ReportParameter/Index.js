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
        var _$entityTable = $('#ReportParameterTable');
        var _appService = abp.services.ReportManager.reportparameter;

        var _permissions = {
            create: abp.auth.hasPermission('Pages.ReportManager.ReportParameters.Create'),
            edit: abp.auth.hasPermission('Pages.ReportManager.ReportParameters.Edit'),
            delete: abp.auth.hasPermission('Pages.ReportManager.ReportParameters.Delete')
        };

        var _createModal = new app.ModalManager({
            viewUrl: abp.appPath + 'ReportManager/ReportParameter/CreateModal',
            scriptUrl: abp.appPath + 'Areas/ReportManager/Views/ReportParameter/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditModal'
        });

        var _editModal = new app.ModalManager({
            viewUrl: abp.appPath + 'ReportManager/ReportParameter/EditModal',
            scriptUrl: abp.appPath + 'Areas/ReportManager/Views/ReportParameter/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditModal'
        });


        function getEntities() {
            _$entityTable.jtable('load');
        }


        abp.event.on('app.createOrEditModalSaved', function () {
            getEntities();
        });


        $('#CreateNewReportParameterButton').click(function () {
            _createModal.open();
        });


        function deleteEntity(entity) {
            abp.message.confirm(
                app.localize('DeleteRecordWarningMessage'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _appService.deleteReportParameter({
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
            title: app.localize('ReportParameters'),
            paging: true,
            sorting: true,
            multiSorting: true,
            actions: {
                listAction: {
                    method: _appService.getReportParameters
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
            }
        });

        getEntities();
    });
})();
