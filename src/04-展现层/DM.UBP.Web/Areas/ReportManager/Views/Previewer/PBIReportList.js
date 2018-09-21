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
        var _appService = abp.services.ubp.pBIReports ;

        var _previewModal = new app.ModalManager({
            viewUrl: abp.appPath + 'ReportManager/Previewer/PBIReport',
            scriptUrl: abp.appPath + 'Areas/ReportManager/Views/Previewer/_PBIReport.js',
            modalClass: 'PreviewParameterModal'
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

        function runUrl(pName) {
            var uName = $('#UserName').val();

            _appService.getPowerBIUrl(uName, pName).done(function (result) {
                openwin(result);
            });
        }

        function getEntities() {
            _$entityTable.jtable('load');
        }

        _$entityTable.jtable({
            title: app.localize('ReportList'),
            paging: true,
            sorting: true,
            //selecting: true,
            multiSorting: true,
            actions: {
                listAction: {
                    method: _appService.getPBIReportList 
                }
            },
            fields: {
                id: {
                    key: true,
                    list: false
                },
                Name: {
                    title: app.localize('ReportName'),
                    width: '100%',
                    display: function (data) {
                        var $btn = $('<a></a>');
                        $btn.append(data.record.name);
                        $btn.click(function () {
                            //_previewModal.open({ name: data.record.name });
                            var url = runUrl(data.record.name);
                        });
                        return $btn;
                    }
                }
            }
        });



        getEntities();
    });
})();
