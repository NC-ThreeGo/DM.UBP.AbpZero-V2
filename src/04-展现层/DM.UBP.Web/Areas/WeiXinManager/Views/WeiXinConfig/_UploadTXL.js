(function ($) {
    app.modals.UploadTXLModal = function () {
        var _appService = abp.services.ubp.weiXinConfig;

        var _modalManager;
        var _$formInfo = null;
        var _$tree;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$tree = new SynchorTree();
            _$tree.init(_modalManager.getModal().find('.upload-tree'));

            _$formInfo = _modalManager.getModal().find('form[name=EntityOptInformationsForm]');
            _$formInfo.validate();
        };



        this.save = function () {
            if (!_$formInfo.valid()) {
                return;
            }

            var input = _$formInfo.serializeFormToObject();
            var txlIds = _$tree.getTXLTreeIds();
            _modalManager.setBusy(true);
            //abp.message.info(input.Id, 'id');
            //abp.message.info(input.CorpId, 'CorpId');
            //abp.message.info(input.CorpName, 'CorpName');
            //abp.message.info(input.TXL_Secret, 'TXL_Secret');
            //abp.message.info(txlIds, 'txlIds');
            //_modalManager.setBusy(false);
            //return;
            //同步
            _appService.uploadTXL({
                id: input.Id,
                corpId: input.CorpId,
                corpName: input.CorpName,
                tXL_Secret: input.TXL_Secret,
                depIds: txlIds
            })
            .done(function (data) {
                if (data) {
                    abp.message.success(app.localize('SynchroSuccessed'));
                }
                else {
                    abp.message.error(app.localize('SynchroFailed'));
                }
                _modalManager.close();
                abp.event.trigger('app.synchroModalSaved', data);
            }).always(function () {
                _modalManager.setBusy(false);
            });

        };

    };
})(jQuery);

var SynchorTree = (function ($) {
    return function () {
        var $tree;

        function init($treeContainer) {
            $tree = $treeContainer;

            $tree.jstree({
                "types": {
                    "default": {
                        "icon": "fa fa-folder tree-item-icon-color icon-lg"
                    },
                    "file": {
                        "icon": "fa fa-file tree-item-icon-color icon-lg"
                    },
                    "user": {
                        "icon": "fa fa-user tree-item-icon-color icon-lg"
                    },
                },
                'checkbox': {
                    keep_selected_style: false,
                    three_state: false,
                    cascade: ''
                },
                plugins: ['checkbox', 'types']
            });

            $tree.on("changed.jstree", function (e, data) {
                if (!data.node) {
                    return;
                }

                var childrenNodes;

                if (data.node.state.selected) {
                    selectNodeAndAllParents($tree.jstree('get_parent', data.node));

                    childrenNodes = $.makeArray($tree.jstree('get_children_dom', data.node));
                    $tree.jstree('select_node', childrenNodes);

                } else {
                    childrenNodes = $.makeArray($tree.jstree('get_children_dom', data.node));
                    $tree.jstree('deselect_node', childrenNodes);
                }

            });
        };

        function selectNodeAndAllParents(node) {
            $tree.jstree('select_node', node, true);
            var parent = $tree.jstree('get_parent', node);
            if (parent) {
                selectNodeAndAllParents(parent);
            }
        };

        function getTXLTreeIds() {
            var ids = [];

            var selectedNodes = $tree.jstree('get_selected', true);
            for (var i = 0; i < selectedNodes.length; i++) {
                ids.push(selectedNodes[i].id);
            }

            return ids;
        };

        return {
            init: init,
            getTXLTreeIds: getTXLTreeIds
        }
    }
})(jQuery);