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
        var _$dataSourceTable = $('#DataSourceTable');
        var _$parameterTable = $('#ParameterTable');

        var _appService = abp.services.ReportManager.reportTemplate;
        var _appDataSourceService = abp.services.ReportManager.reportDataSource;
        var _appParameterService = abp.services.ReportManager.reportParameter;

        var _permissions = {
            create: abp.auth.hasPermission('Pages.ReportManager.Templates.Create'),
            edit: abp.auth.hasPermission('Pages.ReportManager.Templates.Edit'),
            delete: abp.auth.hasPermission('Pages.ReportManager.Templates.Delete')
        };

        var _dataSourcePermissions = {
            create: abp.auth.hasPermission('Pages.ReportManager.DataSources.Create'),
            edit: abp.auth.hasPermission('Pages.ReportManager.DataSources.Edit'),
            delete: abp.auth.hasPermission('Pages.ReportManager.DataSources.Delete')
        };

        var _parameterPermissions = {
            create: abp.auth.hasPermission('Pages.ReportManager.Parameters.Create'),
            edit: abp.auth.hasPermission('Pages.ReportManager.Parameters.Edit'),
            delete: abp.auth.hasPermission('Pages.ReportManager.Parameters.Delete')
        };

        var _createModal = new app.ModalManager({
            viewUrl: abp.appPath + 'ReportManager/Template/CreateModal',
            scriptUrl: abp.appPath + 'Areas/ReportManager/Views/Template/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditModal'
        });

        var _dataSourceCreateModal = new app.ModalManager({
            viewUrl: abp.appPath + 'ReportManager/DataSource/CreateModal',
            scriptUrl: abp.appPath + 'Areas/ReportManager/Views/DataSource/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditModal'
        });

        var _parameterCreateModal = new app.ModalManager({
            viewUrl: abp.appPath + 'ReportManager/Parameter/CreateModal',
            scriptUrl: abp.appPath + 'Areas/ReportManager/Views/Parameter/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditModal'
        });

        var _editModal = new app.ModalManager({
            viewUrl: abp.appPath + 'ReportManager/Template/EditModal',
            scriptUrl: abp.appPath + 'Areas/ReportManager/Views/Template/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditModal'
        });

        var _dataSourceEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'ReportManager/DataSource/EditModal',
            scriptUrl: abp.appPath + 'Areas/ReportManager/Views/DataSource/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditModal'
        });

        function openwin(url) {
            //window.open('about:blank', name, 'height=400, width=400, top=0, left=0, toolbar=yes, menubar=yes, scrollbars=yes, resizable=yes,location=yes, status=yes');
            var a = document.createElement("a");
            a.setAttribute("href", url);
            a.setAttribute("target", "_blank");
            a.setAttribute("id", "openwin");
            document.body.appendChild(a);
            a.click();
            document.body.removeChild(a);
        }

        var _previewModal = new app.ModalManager({
            viewUrl: abp.appPath + 'ReportManager/Preview/Index',
            scriptUrl: abp.appPath + 'Areas/ReportManager/Views/Preview/Index.js',
            modalClass: 'Index'
        });

        function getEntities() {
            _$entityTable.jtable('load');
        }

        function getDataSources() {
            var $selectedRows = _$entityTable.jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    _$dataSourceTable.jtable('load', { id: record.id });
                });
            }
            else {
                _$dataSourceTable.jtable('load');
            }
        }

        function getParameters() {
            var $selectedRows = _$entityTable.jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    _$parameterTable.jtable('load', { id: record.id });
                });
            }
            else {
                _$parameterTable.jtable('load');
            }

        }


        abp.event.on('app.createOrEditModalSaved', function () {
            getEntities();
            getDataSources();
            getParameters();
        });


        $('#CreateNewTemplateButton').click(function () {
            if (_permissions.create)
                _createModal.open();
        });

        $('#CreateNewDataSourceButton').click(function () {
            if (_dataSourcePermissions.create) {
                var $selectedRows = _$entityTable.jtable('selectedRows');
                if ($selectedRows.length > 0) {
                    $selectedRows.each(function () {
                        var record = $(this).data('record');
                        _dataSourceCreateModal.open({ template_Id: record.id });
                    });
                }
                else {
                    abp.message.warn(app.localize('PleaseCheckOneTemplate'));
                    return;
                }
            }
        });

        $('#CreateNewParameterButton').click(function () {
            if (_parameterPermissions.create) {
                var $selectedRows = _$entityTable.jtable('selectedRows');
                if ($selectedRows.length > 0) {
                    $selectedRows.each(function () {
                        var record = $(this).data('record');
                        _parameterCreateModal.open({ template_Id: record.id });
                    });
                }
                else {
                    abp.message.warn(app.localize('PleaseCheckOneTemplate'));
                    return;
                }
            }
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
                            getDataSources();
                            getParameters();
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        };

        function deleteDataSource(entity) {
            abp.message.confirm(
                app.localize('DeleteRecordWarningMessage'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _appDataSourceService.deleteReportDataSource({
                            id: entity.id
                        }).done(function () {
                            getEntities();
                            getDataSources();
                            getParameters();
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        };

        function deleteParameter(entity) {
            abp.message.confirm(
                app.localize('DeleteRecordWarningMessage'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _appParameterService.deleteReportParameter({
                            id: entity.id
                        }).done(function () {
                            getEntities();
                            getDataSources();
                            getParameters();
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        };

        _$entityTable.jtable({
            title: app.localize('ReportTemplateTable'),
            paging: true,
            sorting: true,
            multiSorting: true,
            selecting: true,
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
                    sorting: false,
                    type: 'record-actions',
                    cssClass: 'btn btn-xs btn-primary blue',
                    text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
                    items: [
                        {
                        text: app.localize('Edit'),
                        visible: function () {
                            return _permissions.edit;
                        },
                        action: function (data) {
                            _editModal.open({ id: data.record.id });
                        }
                    },
                    {
                        text: app.localize('Design'),
                        visible: function () {
                            return _permissions.edit;
                        },
                        action: function (data) {
                            //_designModal.open({ id: data.record.id });
                            openwin(abp.appPath + 'ReportManager/Designer/Index?id=' + data.record.id);
                        }
                    },
                    {
                        text: app.localize('Preview'),
                        visible: function () {
                            return _permissions.edit;
                        },
                        action: function (data) {
                            openwin(abp.appPath + 'ReportManager/Designer/Index?id=4' + data.record.id);
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
                }

                //dataSource: {
                //    title: '',
                //    width: '5%',
                //    paging: false,
                //    sorting: false,
                //    edit: false,
                //    create: false,
                //    display: function (templateData) {
                //        //Create an image that will be used to open child table
                //        var $btn = $('<button type="button" class="btn btn-primary">' + app.localize('DataSource') + '</button>');
                //        //Open child table when user clicks the image
                //        $btn.click(function () {
                //            _$entityTable.jtable('openChildTable',
                //                $btn.closest('tr'),
                //                {
                //                    title: templateData.record.templateName + app.localize('DataSource'),
                //                    actions: {
                //                        listAction: {
                //                            method: _appDataSourceService.getReportDataSourcesByTemp
                //                        }
                //                    },
                //                    fields: {
                //                        id: {
                //                            key: true,
                //                            list: false
                //                        },
                //                        connkeyName: {
                //                            title: app.localize('ConnkeyName'),
                //                            width: '25%',
                //                        },
                //                        tableName: {
                //                            title: app.localize('TableName'),
                //                            width: '50%',
                //                        },
                //                        commandType: {
                //                            title: app.localize('CommandType'),
                //                            width: '25%',
                //                        }
                //                    }
                //                }, function (data) { //opened handler
                //                    data.childTable.jtable('load');
                //                }, { template_Id: templateData.record.id });
                //        });
                //        //Return image to show on the person row
                //        return $btn;
                //    }
                //}
            },
            selectionChanged: function () {
                getDataSources();
                getParameters();
            }
        });
        _$dataSourceTable.jtable({
            title: app.localize('DataSourceTable'),
            paging: false,
            sorting: true,
            multiSorting: true,
            selecting: true,
            actions: {
                listAction: {
                    method: _appDataSourceService.getReportDataSourcesByTemplate
                }
            },
            fields: {
                id: {
                    key: true,
                    list: false
                },
                actions: {
                    sorting: false,
                    type: 'record-actions',
                    cssClass: 'btn btn-xs btn-primary blue',
                    text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
                    items: [{
                        text: app.localize('Edit'),
                        visible: function () {
                            return _dataSourcePermissions.edit;
                        },
                        action: function (data) {
                            _dataSourceEditModal.open({ id: data.record.id });
                        }
                    },
                    {
                        text: app.localize('Delete'),
                        visible: function (data) {
                            return _dataSourcePermissions.delete;
                        },
                        action: function (data) {
                            deleteDataSource(data.record);
                        }
                    }]
                },
                connkeyName: {
                    title: app.localize('ConnkeyName'),
                    width: '25%'
                },
                tableName: {
                    title: app.localize('TableName'),
                    width: '50%'
                },
                commandType: {
                    title: app.localize('CommandType'),
                    width: '25%',
                    options: { '1': 'SQL语句', '2': '存储过程' }
                }
            }
        });
        _$parameterTable.jtable({
            title: app.localize('ParameterTable'),
            paging: false,
            sorting: true,
            multiSorting: true,
            selecting: true,
            actions: {
                listAction: {
                    method: _appParameterService.getReportParametersByTemplate
                }
            },
            fields: {
                id: {
                    key: true,
                    list: false
                },
                actions: {
                    sorting: false,
                    type: 'record-actions',
                    cssClass: 'btn btn-xs btn-primary blue',
                    text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
                    items: [{
                        text: app.localize('Edit'),
                        visible: function () {
                            return _parameterPermissions.edit;
                        },
                        action: function (data) {
                            _parameterEditModal.open({ id: data.record.id });
                        }
                    },
                    {
                        text: app.localize('Delete'),
                        visible: function (data) {
                            return _parameterPermissions.delete;
                        },
                        action: function (data) {
                            deleteParameter(data.record);
                        }
                    }]
                },
                parameterName: {
                    title: app.localize('ParameterName'),
                    width: '25%',
                },
                paramterType: {
                    title: app.localize('ParamterType'),
                    width: '50%',
                    options: { '1': '字符型', '2': '整型', '3': '浮点型', '4': '日期型', '5': '布尔型', '6': 'Guid型' }
                },
                uiType: {
                    title: app.localize('UiType'),
                    width: '25%',
                    options: { '1': '文本框', '2': '多行文本', '3': '整数型', '4': '小数型', '5': '日期型', '6': '日期时间型', '7': '下拉框', '8': '多选下拉框', '9': '自动搜素下拉框', '10': '自动多选搜索下拉框' }
                }
            }
        });

        getEntities();
        getDataSources();
        getParameters();
        InitModal1();
    });
})();
