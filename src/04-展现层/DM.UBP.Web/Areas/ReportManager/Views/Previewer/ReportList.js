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
        var _$entityTable = $('#ReportListTable');
        var _appService = abp.services.ubp.reportTemplate;

        var _previewModal = new app.ModalManager({
            viewUrl: abp.appPath + 'ReportManager/Previewer/PreviewParameterModal',
            scriptUrl: abp.appPath + 'Areas/ReportManager/Views/Previewer/_PreviewParameterModal.js',
            modalClass: 'PreviewParameterModal'
        });

        function getEntities() {
            var categoryId = $('#CategoryId').val();
            _$entityTable.jtable('load', { categoryId: categoryId });
        }

        _$entityTable.jtable({
            title: app.localize('ReportList'),
            paging: true,
            sorting: true,
            //selecting: true,
            multiSorting: true,
            actions: {
                listAction: {
                    method: _appService.getReportList
                }
            },
             fields: {
                id: {
                    key: true,
                    list: false
                },
                //actions: {
                //    sorting: false,
                //    type: 'record-actions',
                //    cssClass: 'btn btn-xs btn-primary blue',
                //    text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
                //    items: [
                //        {
                //            text: app.localize('Preview'),
                //            visible: function () {
                //                return true;
                //            },
                //            action: function (data) {
                //                _previewModal.open({ id: data.record.id });
                //            }
                //        }]
                //},
                templateName: {
                    title: app.localize('ReportName'),
                    width: '25%',
                    display: function (data) {

                        var $btn = $('<a></a>');
                        $btn.append(data.record.templateName);
                        //$btn.attr("onclick", "alert('" + data.record.templateName + "')");
                        //$btn.attr("onclick", "_previewModal.open({ id: 62 })");
                        $btn.click(function () {
                            _previewModal.open({ id: data.record.id });
                        });
                        return $btn;
                    }
                },
                description: {
                    title: app.localize('Description'),
                    width: '75%'
                }
            },
             //selectionChanged: function () {
             //    var $selectedRows = _$entityTable.jtable('selectedRows');
             //    if ($selectedRows.length > 0) {
             //        var record = $($selectedRows[0]).data('record');
             //        //alert(record.templateName);
             //        _previewModal.open({ id: record.id });
             //    }
             //}
        });

        

        getEntities();
    });
})();
