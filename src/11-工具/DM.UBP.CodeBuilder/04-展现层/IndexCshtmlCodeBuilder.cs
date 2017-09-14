using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.CodeBuilder
{
    public class IndexCshtmlCodeBuilder : CodeBuilderBase
    {
        public override string SubCodePath { get => _RelativePath; set => _RelativePath = value; }

        //public override bool AutoMakeDir { get => false;}

        public override string Suffix => ".cshtml";

        public string _RelativePath = @"04-展现层\DM.UBP.Web\Areas\";

        public PermissionCodeBuilder PermissionCodeBuilder { get; set; }

        public ControllerCodeBuilder ControllerCodeBuilder { get; set; }

        public IndexCshtmlCodeBuilder(
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
            this.WriteUsing();
            this.WriteHtml();
        }

        private void WriteUsing()
        {
            CodeText.AppendLine("@using Abp.Web.Mvc.Extensions");
            CodeText.AppendLine("@using DM.UBP.Web.Navigation");
            CodeText.AppendLine("@using " + PermissionCodeBuilder.FullNameSpace);
            CodeText.AppendLine("@{");
            CodeText.AppendLine("   ViewBag.CurrentPageName = UbpMenu" + "." + ModuleName + ";");
            CodeText.AppendLine("}");
            CodeText.AppendLine("@section Styles");
            CodeText.AppendLine("{");
            CodeText.AppendLine("   @*@Html.IncludeStyle(\"~/Areas/" + ModuleName  + "/Views/" + ControllerCodeBuilder.ControllerName + "/Index.min.css\")*@");
            CodeText.AppendLine("}");
            CodeText.AppendLine("@section Scripts");
            CodeText.AppendLine("{");
            CodeText.AppendLine("   @Html.IncludeScript(\"~/Areas/" + ModuleName + "/Views/" + ControllerCodeBuilder.ControllerName + "/Index.js\")");
            //CodeText.AppendLine("   @Html.IncludeScript(\"~/Areas/" + ModuleName + "/Views/" + ControllerCodeBuilder.ControllerName + "/_CreateOrEditModal.js\")");
            CodeText.AppendLine("}");
            CodeText.AppendLine("");
        }

        private void WriteHtml()
        {
            CodeText.AppendLine("<div class=\"row margin-bottom-5\">");
            CodeText.AppendLine("   <div class=\"col-xs-6\">");
            CodeText.AppendLine("       <div class=\"page-head\">");
            CodeText.AppendLine("           <div class=\"page-title\">");
            CodeText.AppendLine("               <h1>");
            CodeText.AppendLine("                   <span>@L(\"" + ControllerCodeBuilder.ControllerName + "\") </span> <small>@L(\"" + ControllerCodeBuilder.ControllerName + "HeaderInfo\")</small>");
            CodeText.AppendLine("               </h1>");
            CodeText.AppendLine("           </div>");
            CodeText.AppendLine("       </div>");
            CodeText.AppendLine("   </div>");
            CodeText.AppendLine("   <div class=\"col-xs-6 text-right\">");
            CodeText.AppendLine("       @if (IsGranted(" + PermissionCodeBuilder.ClassName + "." + PermissionCodeBuilder.PermCreateKey + "))");
            CodeText.AppendLine("       {");
            CodeText.AppendLine("           <button id=\"CreateNew" + ControllerCodeBuilder.ControllerName + "Button\" class=\"btn btn-primary blue\"><i class=\"fa fa-plus\"></i> @L(\"CreateNew" + ControllerCodeBuilder.ControllerName + "\")</button>");
            CodeText.AppendLine("       }");
            CodeText.AppendLine("   </div>");
            CodeText.AppendLine("</div>");

            CodeText.AppendLine("<div class=\"portlet light margin-bottom-0\">");
            CodeText.AppendLine("   <div class=\"portlet-body\">");
            CodeText.AppendLine("       <div id=\"" + ControllerCodeBuilder.ControllerName + "Table\"></div>");
            CodeText.AppendLine("   </div>");
            CodeText.AppendLine("</div>");
        }
    }
}
