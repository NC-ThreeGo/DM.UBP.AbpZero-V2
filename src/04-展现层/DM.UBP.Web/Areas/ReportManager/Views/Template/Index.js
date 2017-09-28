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
        var _$entityTable = $('#TemplateTable');
        var _appService = abp.services.ReportManager.reportTemplate;

        var _permissions = {
            create: abp.auth.hasPermission('Pages.ReportManager.ReportTemplates.Create'),
            edit: abp.auth.hasPermission('Pages.ReportManager.ReportTemplates.Edit'),
            delete: abp.auth.hasPermission('Pages.ReportManager.ReportTemplates.Delete')
        };

        var _createModal = new app.ModalManager({
            viewUrl: abp.appPath + 'ReportManager/Template/CreateModal',
            scriptUrl: abp.appPath + 'Areas/ReportManager/Views/Template/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditModal'
        });

        var _editModal = new app.ModalManager({
            viewUrl: abp.appPath + 'ReportManager/Template/EditModal',
            scriptUrl: abp.appPath + 'Areas/ReportManager/Views/Template/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditModal'
        });


        function getEntities() {
            _$entityTable.jtable('load');
        }


        abp.event.on('app.createOrEditModalSaved', function () {
            getEntities();
        });


        $('#CreateNewTemplateButton').click(function () {
            _createModal.open();
        });


        function deleteEntity(entity) {
            abp.message.confirm(
                app.localize('DeleteRecordWarningMessage'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _appService.deleteReportTemplate({
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
            title: app.localize('ReportTemplates'),
            paging: true,
            sorting: true,
            multiSorting: true,
            actions: {
                listAction: {
                    method: _appService.getReportTemplates
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
                templateName: {
                    title: app.localize('TemplateName'),
                    width: '25%',
                },
                categoryName: {
                    title: app.localize('CategoryName'),
                    width: '25%',
                },
                description: {
                    title: app.localize('Description'),
                    width: '50%',
                },
            }
        });

        getEntities();
    });
})();
