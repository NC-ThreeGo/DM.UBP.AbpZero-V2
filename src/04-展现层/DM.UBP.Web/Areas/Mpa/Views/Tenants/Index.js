(function () {
    $(function () {

        var _$tenantsTable = $('#TenantsTable');
        var _tenantService = abp.services.app.tenant;

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Tenants.Create'),
            edit: abp.auth.hasPermission('Pages.Tenants.Edit'),
            changeFeatures: abp.auth.hasPermission('Pages.Tenants.ChangeFeatures'),
            impersonation: abp.auth.hasPermission('Pages.Tenants.Impersonation'),
            'delete': abp.auth.hasPermission('Pages.Tenants.Delete')
        };

        var _createModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Mpa/Tenants/CreateModal',
            scriptUrl: abp.appPath + 'Areas/Mpa/Views/Tenants/_CreateModal.js',
            modalClass: 'CreateTenantModal'
        });

        var _editModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Mpa/Tenants/EditModal',
            scriptUrl: abp.appPath + 'Areas/Mpa/Views/Tenants/_EditModal.js',
            modalClass: 'EditTenantModal'
        });

        var _featuresModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Mpa/Tenants/FeaturesModal',
            scriptUrl: abp.appPath + 'Areas/Mpa/Views/Tenants/_FeaturesModal.js',
            modalClass: 'TenantFeaturesModal'
        });

        var _userLookupModal = app.modals.LookupModal.create({
            title: app.localize('SelectAUser'),
            serviceMethod: abp.services.app.commonLookup.findUsers
        });

        _$tenantsTable.jtable({

            title: app.localize('Tenants'),

            paging: true,
            sorting: true,
            multiSorting: true,

            actions: {
                listAction: {
                    method: _tenantService.getTenants
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
                    sorting: false,
                    list: _permissions.edit || _permissions.delete,

                    items: [{
                        text: app.localize('LoginAsThisTenant'),
                        visible: function () {
                            return _permissions.impersonation;
                        },
                        action: function (data) {
                            _userLookupModal.open({
                                extraFilters: {
                                    tenantId: data.record.id
                                },
                                title: app.localize('SelectAUser')
                            }, function (selectedItem) {
                                app.utils.removeCookie(abp.security.antiForgery.tokenCookieName);
                                abp.ajax({
                                    url: abp.appPath + 'Account/Impersonate',
                                    data: JSON.stringify({
                                        tenantId: data.record.id,
                                        userId: selectedItem.value
                                    })
                                });
                            });
                        }
                    }, {
                        text: app.localize('Edit'),
                        visible: function () {
                            return _permissions.edit;
                        },
                        action: function (data) {
                            _editModal.open({ id: data.record.id });
                        }
                    }, {
                        text: app.localize('Features'),
                        visible: function () {
                            return _permissions.changeFeatures;
                        },
                        action: function (data) {
                            _featuresModal.open({ id: data.record.id });
                        }
                    }, {
                        text: app.localize('Delete'),
                        visible: function () {
                            return _permissions.delete;
                        },
                        action: function (data) {
                            deleteTenant(data.record);
                        }
                    }, {
                        text: app.localize('Unlock'),
                        action: function (data) {
                            _tenantService.unlockTenantAdmin({
                                id: data.record.id
                            }).done(function () {
                                abp.notify.success(app.localize('UnlockedTenandAdmin', data.record.name));
                            });
                        }
                    }]
                },
                tenancyName: {
                    title: app.localize('TenancyCodeName'),
                    display: function (data) {
                        var $div = $('<div> ' + data.record.tenancyName + '</div>');
                        if (data.record.connectionString) {
                            $div.prepend($("<i class='fa fa-database' title=\"" + app.localize('HasOwnDatabase') + "\"></i>"));
                        }

                        return $div;
                    },
                    width: '18%'
                },
                name: {
                    title: app.localize('Name'),
                    width: '20%'
                },
                editionDisplayName: {
                    title: app.localize('Edition'),
                    width: '18%'
                },
                isActive: {
                    title: app.localize('Active'),
                    width: '9%',
                    display: function (data) {
                        if (data.record.isActive) {
                            return '<span class="label label-success">' + app.localize('Yes') + '</span>';
                        } else {
                            return '<span class="label label-default">' + app.localize('No') + '</span>';
                        }
                    }
                },
                creationTime: {
                    title: app.localize('CreationTime'),
                    width: '20%',
                    display: function (data) {
                        return moment(data.record.creationTime).format('L');
                    }
                }
            }

        });

        function getTenants(reload) {
            if (reload) {
                _$tenantsTable.jtable('reload');
            } else {
                _$tenantsTable.jtable('load', {
                    filter: $('#TenantsTableFilter').val()
                });
            }
        }

        function deleteTenant(tenant) {
            abp.message.confirm(
                app.localize('TenantDeleteWarningMessage', tenant.tenancyName),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _tenantService.deleteTenant({
                            id: tenant.id
                        }).done(function () {
                            getTenants();
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        }

        $('#CreateNewTenantButton').click(function () {
            _createModal.open();
        });

        $('#GetTenantsButton').click(function (e) {
            e.preventDefault();
            getTenants();
        });

        abp.event.on('app.editTenantModalSaved', function () {
            getTenants(true);
        });

        abp.event.on('app.createTenantModalSaved', function () {
            getTenants(true);
        });

        getTenants();

        $('#TenantsTableFilter').focus();
    });
})();