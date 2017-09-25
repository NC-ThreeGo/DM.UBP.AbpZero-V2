using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.CodeBuilder
{
    public class IndexJsCodeBuilder : CodeBuilderBase
    {
        public override string SubCodePath { get => _RelativePath; set => _RelativePath = value; }

        //public override bool AutoMakeDir { get => false;}

        public override string Suffix => ".js";

        public string _RelativePath = @"04-展现层\DM.UBP.Web\Areas\";

        public PermissionCodeBuilder PermissionCodeBuilder { get; set; }

        public ControllerCodeBuilder ControllerCodeBuilder { get; set; }

        public IndexJsCodeBuilder(
            PermissionCodeBuilder permissionCodeBuilder,
            ControllerCodeBuilder controllerCodeBuilder,
            string moduleName)
        {
            PermissionCodeBuilder = permissionCodeBuilder;
            ControllerCodeBuilder = controllerCodeBuilder;

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["ControllerCodePath"]))
            {
                _RelativePath = ConfigurationManager.AppSettings["ControllerCodePath"];
            }

            ModuleName = moduleName;
            SubModuleName = "Views";
            FunctionName = ControllerCodeBuilder.ControllerName;
            FileName = "Index";
        }

        public override void InternalCreateCode()
        {
            this.WriteJs();
        }

        private void WriteJs()
        {
            CodeText.AppendLine(@"(function () {
                                    $(function () {
                                        var _$entityTable = $('#" + ControllerCodeBuilder.ControllerName + @"Table');
                                        var _appService = abp.services."+ ModuleName + "." + ControllerCodeBuilder.AppServiceInterfaceCodeBuilder.ClassName.Substring(1).ToLower().Substring(0, ControllerCodeBuilder.AppServiceInterfaceCodeBuilder.ClassName.Substring(1).ToLower().Length - 10) + @";

                                        var _permissions = {
                                            create: abp.auth.hasPermission('" + PermissionCodeBuilder.PermCreateValue + @"'),
                                            edit: abp.auth.hasPermission('" + PermissionCodeBuilder.PermEditValue + @"'),
                                            delete: abp.auth.hasPermission('" + PermissionCodeBuilder.PermDeleteValue + @"')
                                            };

                                        var _createModal = new app.ModalManager({
                                            viewUrl: abp.appPath + '" + ControllerCodeBuilder.ModuleName + "/" + ControllerCodeBuilder.ControllerName + @"/CreateModal',
                                            scriptUrl: abp.appPath + 'Areas/" + ControllerCodeBuilder.ModuleName + "/Views/" + ControllerCodeBuilder.ControllerName + @"/_CreateOrEditModal.js',
                                            modalClass: 'CreateOrEditModal'
                                        });

                                        var _editModal = new app.ModalManager({
                                            viewUrl: abp.appPath + '" + ControllerCodeBuilder.ModuleName + "/" + ControllerCodeBuilder.ControllerName + @"/EditModal',
                                            scriptUrl: abp.appPath + 'Areas/" + ControllerCodeBuilder.ModuleName + "/Views/" + ControllerCodeBuilder.ControllerName + @"/_CreateOrEditModal.js',
                                            modalClass: 'CreateOrEditModal'
                                        });
                                ");
            CodeText.AppendLine("");
            CodeText.AppendLine(@"      function getEntities() {
                                            _$entityTable.jtable('load');
                                        }
                                ");
            CodeText.AppendLine("");
            CodeText.AppendLine(@"      abp.event.on('app.createOrEditModalSaved', function () {
                                            getEntities();
                                        });
                                ");
            CodeText.AppendLine("");
            CodeText.AppendLine(@"      $('#CreateNew" + ControllerCodeBuilder.ControllerName + @"Button').click(function () {
                                            _createModal.open();
                                        });
                                ");
            CodeText.AppendLine("");
            CodeText.AppendLine(@"      function deleteEntity(entity) {
                                            abp.message.confirm(
                                                app.localize('DeleteRecordWarningMessage'),
                                                function (isConfirmed) {
                                                    if (isConfirmed) {
                                                        _appService." + Utils.FirstWordToLower(ControllerCodeBuilder.AppServiceInterfaceCodeBuilder.Method_DeleteAsync_Name) + @"({
                                                            id: entity.id
                                                        }).done(function () {
                                                            getEntities();
                                                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                                                        });
                                                    }
                                                }
                                            );
                                        };
                                ");
            CodeText.AppendLine("");
            CodeText.AppendLine(@"      _$entityTable.jtable({
                                            title: app.localize('" + ControllerCodeBuilder.AppServiceInterfaceCodeBuilder.EntityCodeBuilder.ClassPluralName + @"'),
                                            paging: true,
                                            sorting: true,
                                            multiSorting: true,
                                            actions: {
                                                listAction: {
                                                    method: _appService." + Utils.FirstWordToLower(ControllerCodeBuilder.AppServiceInterfaceCodeBuilder.Method_GetAllAsync_Name) + @"
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
                                                    text: '<i class=" + "\"fa fa-cog\"></i> ' + app.localize('Actions') + ' <span class=\"caret\"></span>'," + @"
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
                                                },"
                                );
            //循环获取实体的字段，加载成Table的列。
            foreach (Field f in ControllerCodeBuilder.AppServiceInterfaceCodeBuilder.EntityCodeBuilder.Fields)
            {
                if (f.TableColWidth > 0)
                {
                    CodeText.AppendLine("       " + Utils.FirstWordToLower(f.Property) + @": {
                                                    title: app.localize('" + Utils.FirstWordToLower(f.Property) + @"'),
                                                    width: '" + f.TableColWidth.ToString() + @"%',
                                                },");
                }
            }
            CodeText.AppendLine("       }");
            CodeText.AppendLine("   });");
            CodeText.AppendLine("");
            CodeText.AppendLine("        getEntities();");
            CodeText.AppendLine("   });");
            CodeText.AppendLine("})();");
        }
    }
}
