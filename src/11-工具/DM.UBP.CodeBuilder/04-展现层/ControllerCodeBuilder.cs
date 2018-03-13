using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.CodeBuilder
{
    public class ControllerCodeBuilder : CodeBuilderBase
    {
        public override string SubCodePath { get => _RelativePath; set => _RelativePath = value; }

        //public override bool AutoMakeDir { get => false;}

        public string _RelativePath = @"04-展现层\DM.UBP.Web\Areas\";
        public string SubNameSpace = "Web.Areas";

        public InputDtoCodeBuilder InputDtoCodeBuilder { get; set; }
        public OutputDtoCodeBuilder OutputDtoCodeBuilder { get; set; }

        public AppServiceInterfaceCodeBuilder AppServiceInterfaceCodeBuilder { get; set; }

        public PermissionCodeBuilder PermissionCodeBuilder { get; set; }

        public string ControllerName { get; set; }

        public ControllerCodeBuilder(
            InputDtoCodeBuilder inputDtoCodeBuilder,
            OutputDtoCodeBuilder outputDtoCodeBuilder,
            AppServiceInterfaceCodeBuilder appServiceInterfaceCodeBuilder,
            PermissionCodeBuilder permissionCodeBuilder,
            string controllerName, string moduleName)
        {
            InputDtoCodeBuilder = inputDtoCodeBuilder;
            OutputDtoCodeBuilder = outputDtoCodeBuilder;
            AppServiceInterfaceCodeBuilder = appServiceInterfaceCodeBuilder;
            PermissionCodeBuilder = permissionCodeBuilder;

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["ControllerCodePath"]))
            {
                _RelativePath = ConfigurationManager.AppSettings["ControllerCodePath"];
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["ControllerNameSpace"]))
            {
                SubNameSpace = ConfigurationManager.AppSettings["ControllerNameSpace"];
            }

            ModuleName = moduleName;
            SubModuleName = "Controllers";
            ControllerName = controllerName;
            ClassName = controllerName + "Controller";
            FileName = ClassName;
        }

        public override void InternalCreateCode()
        {
            this.WriteUsing();
            this.WriteClass();
        }

        private void WriteUsing()
        {
            CodeText.AppendLine("using System.Web.Mvc;");
            CodeText.AppendLine("using System.Threading.Tasks;");
            CodeText.AppendLine("using System.Collections.Generic;");
            CodeText.AppendLine("using Abp.Web.Models;");
            CodeText.AppendLine("using Abp.Runtime.Caching;");
            CodeText.AppendLine("using Abp.Web.Mvc.Authorization;");
            CodeText.AppendLine("using " + InputDtoCodeBuilder.FullNameSpace + ";");
            CodeText.AppendLine("using " + AppServiceInterfaceCodeBuilder.FullNameSpace + ";");
            CodeText.AppendLine("using " + PermissionCodeBuilder.FullNameSpace + ";");
            CodeText.AppendLine("using DM.UBP.Web.Controllers;");
            CodeText.AppendLine("");
        }

        private void WriteClass()
        {
            FullNameSpace = RootNameSpace + "." + SubNameSpace + "."
                + ModuleName +".Controllers";
            CodeText.AppendLine("namespace " + FullNameSpace);
            CodeText.AppendLine("{");
            CodeText.AppendLine("/// <summary>");
            CodeText.AppendLine("/// " + ClassComments);
            CodeText.AppendLine("/// <summary>");

            //当前服务所需要的菜单权限
            CodeText.AppendLine("[AbpMvcAuthorize(" + PermissionCodeBuilder.ClassName + "." + PermissionCodeBuilder.MenuPermKey + ")]");

            CodeText.AppendLine("public class " + ClassName + " : UBPControllerBase");
            CodeText.AppendLine("{");

            this.WriteProperty();
            this.WriteConstructor();
            this.WriteMethod();

            CodeText.AppendLine("}");
            CodeText.AppendLine("}");
        }

        private void WriteProperty()
        {
            CodeText.AppendLine("private " + AppServiceInterfaceCodeBuilder.ClassName + " _" + AppServiceInterfaceCodeBuilder.ClassName.Substring(1) + "; ");
        }

        private void WriteConstructor()
        {
            CodeText.AppendLine("public " + ClassName + "(");
            CodeText.AppendLine("   ICacheManager cacheManager,");
            CodeText.AppendLine("   " + AppServiceInterfaceCodeBuilder.ClassName + " " + AppServiceInterfaceCodeBuilder.ClassName.Substring(1).ToLower());
            CodeText.AppendLine("   )");
            CodeText.AppendLine("{");
            CodeText.AppendLine("_" + AppServiceInterfaceCodeBuilder.ClassName.Substring(1) + " = " + AppServiceInterfaceCodeBuilder.ClassName.Substring(1).ToLower() + "; ");
            CodeText.AppendLine("}");
            CodeText.AppendLine("");
        }

        private void WriteMethod()
        {
            //Index()
            CodeText.AppendLine("public ActionResult Index()");
            CodeText.AppendLine("{");
            CodeText.AppendLine("return View();");
            CodeText.AppendLine("}");
            CodeText.AppendLine("");

            //CreateModal
            //当前方法所需要的菜单权限
            CodeText.AppendLine("[AbpMvcAuthorize(" + PermissionCodeBuilder.ClassName + "." + PermissionCodeBuilder.PermCreateKey + ")]");
            CodeText.AppendLine("public PartialViewResult CreateModal()");
            CodeText.AppendLine("{");
            CodeText.AppendLine("var viewModel = new " + OutputDtoCodeBuilder.ClassName + "()");
            CodeText.AppendLine("{");
            CodeText.AppendLine("//给属性赋值");
            CodeText.AppendLine("};");
            CodeText.AppendLine("");
            CodeText.AppendLine("return PartialView(\"_CreateOrEditModal\", viewModel);");
            CodeText.AppendLine("}");
            CodeText.AppendLine("");

            //EditModal
            //当前方法所需要的菜单权限
            CodeText.AppendLine("[AbpMvcAuthorize(" + PermissionCodeBuilder.ClassName + "." + PermissionCodeBuilder.PermEditKey + ")]");
            CodeText.AppendLine("public async Task<PartialViewResult> EditModal(" + AppServiceInterfaceCodeBuilder.EntityCodeBuilder.PkType.ToLower() + " id)");
            CodeText.AppendLine("{");
            CodeText.AppendLine("var viewModel = await _" + AppServiceInterfaceCodeBuilder.ClassName.Substring(1) + "." + AppServiceInterfaceCodeBuilder.Method_GetByIdAsync_Name + "(id);");
            CodeText.AppendLine("return PartialView(\"_CreateOrEditModal\", viewModel);");
            CodeText.AppendLine("}");
            CodeText.AppendLine("");
        }
    }
}
