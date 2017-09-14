using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.CodeBuilder
{
    public class EditModalJsCodeBuilder : CodeBuilderBase
    {
        public override string SubCodePath { get => _RelativePath; set => _RelativePath = value; }

        //public override bool AutoMakeDir { get => false;}

        public override string Suffix => ".js";

        public string _RelativePath = @"04-展现层\DM.UBP.Web\Areas\";

        public PermissionCodeBuilder PermissionCodeBuilder { get; set; }

        public ControllerCodeBuilder ControllerCodeBuilder { get; set; }

        public EditModalJsCodeBuilder(
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
            FileName = "_CreateOrEditModal";
        }

        public override void InternalCreateCode()
        {
            this.WriteJs();
        }

        private void WriteJs()
        {
            CodeText.AppendLine(@"(function ($) {
                                    app.modals.CreateOrEditModal = function() {
                                        var _appService = abp.services."+ ModuleName + "." + ControllerCodeBuilder.AppServiceInterfaceCodeBuilder.ClassName.Substring(1).ToLower().Substring(0, ControllerCodeBuilder.AppServiceInterfaceCodeBuilder.ClassName.Substring(1).ToLower().Length - 10) + @";

                                        var _modalManager;
                                        var _$formInfo = null;

                                        this.init = function(modalManager) {
                                            _modalManager = modalManager;

                                            _$formInfo = _modalManager.getModal().find('form[name=EntityOptInformationsForm]');
                                            _$formInfo.validate();
                                        };

                                        this.save = function() {
                                            if (!_$formInfo.valid())
                                            {
                                                return;
                                            }

                                            var input = _$formInfo.serializeFormToObject();

                                            _modalManager.setBusy(true);

                                            if (input.Id > 0)
                                            {
                                                //修改
                                                _appService." + ControllerCodeBuilder.AppServiceInterfaceCodeBuilder.Method_UpdateAsync_Name + @"(input)
                                                    .done(function()
                                                    {
                                                        abp.notify.info(app.localize('SavedSuccessfully'));
                                                        _modalManager.close();
                                                        abp.event.trigger('app.createOrEditModalSaved');
                                                    }).always(function ()
                                                    {
                                                        _modalManager.setBusy(false);
                                                    });
                                            }
                                            else
                                            {
                                                //新建
                                                _appService." + ControllerCodeBuilder.AppServiceInterfaceCodeBuilder.Method_CreateAsync_Name + @"(input)
                                                    .done(function ()
                                                    {
                                                        abp.notify.info(app.localize('SavedSuccessfully'));
                                                        _modalManager.close();
                                                        abp.event.trigger('app.createOrEditModalSaved');
                                                    }).always(function ()
                                                    {
                                                        _modalManager.setBusy(false);
                                                    });
                                            };
                                        };

                                    };
                                })(jQuery);");
        }
    }
}
