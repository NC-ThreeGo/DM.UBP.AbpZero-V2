using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.CodeBuilder
{
    public class UbpDbContextCodeBuilder : CodeBuilderBase
    {
        public override string SubCodePath { get => _RelativePath; set => _RelativePath = value; }

        public override bool IsOverrideWrite { get => false; }

        public string _RelativePath = @"01-基础设施层\DM.UBP.EF\";

        public EntityCodeBuilder EntityCodeBuilder { get; set; }

        public UbpDbContextCodeBuilder(EntityCodeBuilder entityCodeBuilder, string moduleName)
        {
            EntityCodeBuilder = entityCodeBuilder;

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["UbpDbContextCodePath"]))
            {
                _RelativePath = ConfigurationManager.AppSettings["UbpDbContextCodePath"];
            }

            ClassName = "UbpDbContext";
            FileName = ClassName + "." + moduleName;
            ModuleName = "DbContext";
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
            CodeText.AppendLine("using System.Data.Entity;");
            CodeText.AppendLine("using " + EntityCodeBuilder.FullNameSpace + ";");
            CodeText.AppendLine("");
        }

        private void WriteClass()
        {
            FullNameSpace = "DM.UBP.EF";
            CodeText.AppendLine("namespace " + FullNameSpace);
            CodeText.AppendLine("{");
            CodeText.AppendLine("/// <summary>");
            CodeText.AppendLine("/// " + ClassComments);
            CodeText.AppendLine("/// <summary>");

            CodeText.AppendLine("public partial class " + ClassName);
            CodeText.AppendLine("{");
            CodeText.AppendLine("//TODO: Define an IDbSet for each Entity...");

            this.WriteProperty();

            CodeText.AppendLine("}");
            CodeText.AppendLine("}");
        }

        private void WriteProperty()
        {
            CodeText.AppendLine("public virtual IDbSet<" + EntityCodeBuilder.ClassName + "> " + EntityCodeBuilder.ClassPluralName + " { get; set; }");

            CodeText.AppendLine("");
            CodeText.AppendLine(CodeBuilderBase.INSERT_POSITION);
        }
    }
}
