using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.CodeBuilder
{
    public class EditModalCshtmlCodeBuilder : CodeBuilderBase
    {
        public override string SubCodePath { get => _RelativePath; set => _RelativePath = value; }

        //public override bool AutoMakeDir { get => false;}

        public override string Suffix => ".cshtml";

        public string _RelativePath = @"04-展现层\DM.UBP.Web\Areas\";

        public PermissionCodeBuilder PermissionCodeBuilder { get; set; }

        public ControllerCodeBuilder ControllerCodeBuilder { get; set; }

        public EditModalCshtmlCodeBuilder(
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
            this.WriteUsing();
            this.WriteHtml();
        }

        private void WriteUsing()
        {
            CodeText.AppendLine("@using Abp.Extensions");
            CodeText.AppendLine("@using DM.UBP.Web.Areas.Mpa.Models.Common.Modals");
            CodeText.AppendLine("");
            CodeText.AppendLine("@model " + ControllerCodeBuilder.OutputDtoCodeBuilder.FullNameSpace + "." + ControllerCodeBuilder.OutputDtoCodeBuilder.ClassName);
            CodeText.AppendLine("");
            CodeText.AppendLine("@Html.Partial(\"~/Areas/Mpa/Views/Common/Modals/_ModalHeader.cshtml\", new ModalHeaderViewModel(Model.IsEditMode ? (L(\"Edit" + ControllerCodeBuilder.AppServiceInterfaceCodeBuilder.EntityCodeBuilder.ClassName + "\") + \": \" + Model.Name) : L(\"Create" + ControllerCodeBuilder.AppServiceInterfaceCodeBuilder.EntityCodeBuilder.ClassName + "\")))");
            CodeText.AppendLine("@section Styles");
            CodeText.AppendLine("{");
            CodeText.AppendLine("}");
            CodeText.AppendLine("@section Scripts");
            CodeText.AppendLine("{");
            CodeText.AppendLine("}");
            CodeText.AppendLine("");
        }

        private void WriteHtml()
        {
            CodeText.AppendLine("<div class=\"modal-body\">");
            CodeText.AppendLine("   <div class=\"tabbable-line\">");
            CodeText.AppendLine("       <ul class=\"nav nav-tabs\">");
            CodeText.AppendLine("           <li class=\"active\">");
            CodeText.AppendLine("               <a href=\"#EntityInformationsTab\" data-toggle=\"tab\" aria-expanded=\"true\">");
            CodeText.AppendLine("                   @L(\"" + ControllerCodeBuilder.AppServiceInterfaceCodeBuilder.EntityCodeBuilder.ClassName + "Informations\")");
            CodeText.AppendLine("               </a>");
            CodeText.AppendLine("           </li>");
            CodeText.AppendLine("       </ul>");
            CodeText.AppendLine("       <div class=\"tab-content\">");
            CodeText.AppendLine("           <div class=\"tab - pane active\" id=\"EntityInformationsTab\">");
            CodeText.AppendLine("               <form name=\"EntityOptInformationsForm\" role=\"form\" novalidate class=\"form - validation\">");
            CodeText.AppendLine("                   @if (Model.IsEditMode)");
            CodeText.AppendLine("                   {");
            CodeText.AppendLine("                       < input type = \"hidden\" name = \"Id\" value = \"@Model.Id\" />");
            CodeText.AppendLine("                   }");
            //加载实体的字段
            foreach (Field f in ControllerCodeBuilder.AppServiceInterfaceCodeBuilder.EntityCodeBuilder.Fields)
            {
                if (f.IsEdit)
                {
                    CodeText.AppendLine("           <div class=\"form - group form - md - line - input form - md - floating - label no - hint\">");
                    CodeText.AppendLine("               <input type=\"text\" name=\"" + f.Property + "\" class=\"form - control@(Model." + f.Property + ".IsNullOrEmpty() ? \"\" : \" edited\")\" value=\"@Model." + f.Property + "\">");
                    CodeText.AppendLine("               <label>@L(\"" + f.Property + "\")</label>");
                    CodeText.AppendLine("           </div>");
                }
            }
            CodeText.AppendLine("");
            CodeText.AppendLine("               </form>");
            CodeText.AppendLine("           </div>");
            CodeText.AppendLine("       </div>");
            CodeText.AppendLine("   </div>");
            CodeText.AppendLine("</div>");

            CodeText.AppendLine("");
            CodeText.AppendLine("@Html.Partial(\"~/Areas/Mpa/Views/Common/Modals/_ModalFooterWithSaveAndCancel.cshtml\")");
        }
    }
}
