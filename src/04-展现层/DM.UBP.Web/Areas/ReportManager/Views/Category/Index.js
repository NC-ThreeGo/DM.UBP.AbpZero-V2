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
        var _$entityTable = $('#CategoryTable');
        var _appService = abp.services.ReportManager.category;

        var _permissions = {
            create: abp.auth.hasPermission('Pages.ReportManager.Categories.Create'),
            edit: abp.auth.hasPermission('Pages.ReportManager.Categories.Edit'),
            delete: abp.auth.hasPermission('Pages.ReportManager.Categories.Delete')
        };

        var _createModal = new app.ModalManager({
            viewUrl: abp.appPath + 'ReportManager/Category/CreateModal',
            scriptUrl: abp.appPath + 'Areas/ReportManager/Views/Category/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditModal'
        });

        var _editModal = new app.ModalManager({
            viewUrl: abp.appPath + 'ReportManager/Category/EditModal',
            scriptUrl: abp.appPath + 'Areas/ReportManager/Views/Category/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditModal'
        });


        function getEntities() {
            _$entityTable.jtable('load');
        }


        abp.event.on('app.createOrEditModalSaved', function () {
            getEntities();
        });


        $('#CreateNewCategoryButton').click(function () {
            _createModal.open();
        });


        function deleteEntity(entity) {
            abp.message.confirm(
                app.localize('DeleteRecordWarningMessage'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _appService.deleteCategory({
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
            title: app.localize('Categories'),
            paging: true,
            sorting: true,
            multiSorting: true,
            actions: {
                listAction: {
                    method: _appService.getCategories 
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
                categoryname: {
                    title: app.localize('CategoryName'),
                    width: '50%',
                },
                parentid: {
                    title: app.localize('ParentId'),
                    width: '25%',
                },
                code: {
                    title: app.localize('Code'),
                    width: '25%',
                },
            }
        });

        getEntities();
    });
})();
