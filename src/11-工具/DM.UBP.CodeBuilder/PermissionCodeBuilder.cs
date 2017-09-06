using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.CodeBuilder
{
    public class PermissionCodeBuilder : CodeBuilderBase
    {
        public override string SubCodePath { get => _RelativePath; set => _RelativePath = value; }

        public override bool IsOverrideWrite { get => false; set => base.IsOverrideWrite = value; }

        public string _RelativePath = @"02-领域层\DM.UBP.Domain.Service\";
        public string SubNameSpace = "Domain.Service";

        public string MenuPermKey { get; set; }
        public string MenuPermValue { get; set; }
        public string PermCreateKey { get; set; }
        public string PermCreateValue { get; set; }
        public string PermEditKey { get; set; }
        public string PermEditValue { get; set; }
        public string PermDeleteKey { get; set; }
        public string PermDeleteValue { get; set; }

        public PermissionCodeBuilder(string moduleName)
        {
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["DomainServiceCodePath"]))
            {
                _RelativePath = ConfigurationManager.AppSettings["DomainServiceCodePath"];
            }

            ModuleName = moduleName;
            ClassName = "AppPermissions_" + ModuleName;
            FileName = ClassName;
            FullNameSpace = RootNameSpace + "." + SubNameSpace + "."
                + ModuleName;
        }

        public override void InternalCreateCode()
        {
            if (!FileExists)
            {
                this.WriteUsing();
                this.WriteClass();
            }
            else
            {
                this.WriteProperty();
            }
        }

        private void WriteUsing()
        {
        }

        private void WriteClass()
        {
            CodeText.AppendLine("namespace " + FullNameSpace);
            CodeText.AppendLine("{");
            CodeText.AppendLine("/// <summary>");
            CodeText.AppendLine("/// " + ClassComments);
            CodeText.AppendLine("/// <summary>");

            CodeText.AppendLine("public class " + ClassName);
            CodeText.AppendLine("{");

            this.WriteProperty();

            CodeText.AppendLine("}");
            CodeText.AppendLine("}");
        }

        private void WriteProperty()
        {
            if (!String.IsNullOrEmpty(MenuPermValue))
            {
                MenuPermKey = "Pages_" + ModuleName;
                CodeText.AppendLine("public const string " + MenuPermKey + " = \"" + MenuPermValue + "\";");
            }

            if (!String.IsNullOrEmpty(PermCreateValue))
            {
                PermCreateKey = "Pages_" + ModuleName + "_Create";
                CodeText.AppendLine("public const string " + PermCreateKey + " = \"" + PermCreateValue + "\";");
            }

            if (!String.IsNullOrEmpty(PermEditValue))
            {
                PermEditKey = "Pages_" + ModuleName + "_Edit";
                CodeText.AppendLine("public const string " + PermEditKey + " = \"" + PermEditValue + "\";");
            }

            if (!String.IsNullOrEmpty(PermDeleteValue))
            {
                PermDeleteKey = "Pages_" + ModuleName + "_Delete";
                CodeText.AppendLine("public const string " + PermDeleteKey + " = \"" + PermDeleteValue + "\";");
            }

            CodeText.AppendLine("");
            CodeText.AppendLine(CodeBuilderBase.INSERT_POSITION);
        }
    }
}
